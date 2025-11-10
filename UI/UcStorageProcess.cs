using Application.Warehouse.Io;
using Application.Warehouse;      // ICraneAddressEncoder (kept if you later switch to Row/Col)
using Application.StorageProcess;
using Application.Ports;          // IClock

namespace UI
{
    public partial class UcStorageProcess : UserControl
    {
        #region Fields & Constructor

        private readonly IWarehouseInventory _inventory;
        private readonly IWarehouseIo _io;
        private readonly IClock _clock;
        private readonly ICraneAddressEncoder _encoder;

        private StorageState _state = StorageState.Idle;
        private CancellationTokenSource? _cts;

        private const int CraneIdleSlot = 55;   // Factory I/O idle/home
        private const int Rows = 6;
        private const int Cols = 9;             // 6 x 9 = 54 slots

        public UcStorageProcess(
            IWarehouseInventory inventory,
            IWarehouseIo io,
            ICraneAddressEncoder encoder,
            IClock clock)
        {
            _inventory = inventory;
            _io = io;
            _encoder = encoder;
            _clock = clock;

            InitializeComponent();
            DoubleBuffered = true;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            UpdateUi();
        }

        #endregion


        #region UI Updates

        private void UpdateUi()
        {
            lblState.Text = $"State: {_state}";

            var next = _inventory.GetNextFreeSlot();
            lblNextFree.Text = next is null
                ? "Next free: none"
                : $"Next free: Row {next.Value.row}, Column {next.Value.column}";

            btnStart.Enabled = (_state == StorageState.Idle);
            btnStop.Enabled = (_state != StorageState.Idle);
        }

        #endregion


        #region Buttons: Store / Stop / All Off / Reset

        // Store ONE item to the numeric slot (manual)
        private async void btnStart_Click(object sender, EventArgs e)
        {
            if (_state != StorageState.Idle) return;

            int slot = (int)numSlot.Value; // 1..54
            if (!IsValidSlotNumber(slot))
            {
                Log("❌ Invalid slot. Use 1..54.");
                return;
            }

            var (row, col) = DecodeSlot(slot);

            // ✅ Prevent double-store into an occupied slot
            if (_inventory.IsOccupied(row, col))
            {
                Log($"❌ Slot {slot} (Row {row}, Col {col}) is FULL. Cannot store.");
                return;
            }

            Log($"Store requested → Slot {slot} (Row {row}, Col {col})");

            _cts?.Cancel();
            _cts = new CancellationTokenSource();

            try
            {
                btnStart.Enabled = false;
                btnStop.Enabled = true;

                await StoreOneAsync(slot, _cts.Token);

                _state = StorageState.Idle;
                UpdateUi();
                Log("✅ Store cycle complete.");
            }
            catch (OperationCanceledException)
            {
                Log("Cycle canceled.");
            }
            catch (Exception ex)
            {
                Log($"Error: {ex.Message}");
                _state = StorageState.Fault;
            }
            finally
            {
                btnStart.Enabled = true;
                btnStop.Enabled = false;
                UpdateUi();
            }
        }

        private async void btnStop_Click(object sender, EventArgs e)
        {
            Log("Stop requested");
            btnStop.Enabled = false;
            try { _cts?.Cancel(); } catch { }
            await ForceStopOutputsAsync();
            _state = StorageState.Idle;
            UpdateUi();
        }

        private async void btnAlloff_Click(object sender, EventArgs e)
        {
            Log("All Off requested");
            try { _cts?.Cancel(); } catch { }
            await ForceStopOutputsAsync();
            _state = StorageState.Idle;
            UpdateUi();
            Log("All outputs OFF. Crane homed.");
        }

        private async void btnReset_Click(object sender, EventArgs e)
        {
            Log("Reset requested");
            try { _cts?.Cancel(); } catch { }
            await ForceStopOutputsAsync();
            _state = StorageState.Idle;
            UpdateUi();

            try
            {
                var snap = await _io.ReadInputsAsync(CancellationToken.None);
                Log($"Post-reset: EStop={(snap.EmergencyStop ? "ACTIVE" : "OK")}, " +
                    $"AtEntry={OnOff(snap.AtEntry)}, AtLoad={OnOff(snap.AtLoad)}, " +
                    $"MovX={OnOff(snap.MovingX)}, MovZ={OnOff(snap.MovingZ)}");
            }
            catch (Exception ex) { Log($"Reset snapshot failed: {ex.Message}"); }
        }

        #endregion


        #region Manual Store Sequence

        private async Task StoreOneAsync(int slot, CancellationToken ct)
        {
            // Convert once and reuse
            var (row, col) = DecodeSlot(slot);

            // 1) Entry + Load until box blocks AtLoad (OFF)
            _state = StorageState.Infeed;
            UpdateUi();
            Log("Stage 1: Entry+Load ON …");
            await _io.ApplyOutputsAsync(OutputCommand.None.WithInfeed(true).WithToCrane(true), ct);

            Log("Waiting AtLoad = OFF (blocked) …");
            await WaitUntilAsync(i => i.AtLoad == false, ct);

            Log("Stopping Entry+Load …");
            await ApplyOffTwiceAsync(OutputCommand.None.WithInfeed(false).WithToCrane(false), ct);

            // 2) Crane pre-position: Left → Lift UP → Left OFF → (Middle)
            _state = StorageState.PalletToCrane;
            UpdateUi();

            Log("Crane: LEFT until LeftLimit …");
            await _io.ApplyOutputsAsync(OutputCommand.None.WithCraneLeft(true), ct);
            await WaitUntilAsync(i => i.CraneLeftLimit, ct);

            Log("Crane: LIFT UP (MovingZ true→false) …");
            await _io.ApplyOutputsAsync(OutputCommand.None.WithCraneLift(true), ct);
            await WaitUntilAsync(i => i.MovingZ, ct);      // start up
            await WaitUntilAsync(i => !i.MovingZ, ct);     // end up

            Log("Crane: LEFT OFF (retract forks) …");
            await _io.ApplyOutputsAsync(OutputCommand.None.WithCraneLeft(false), ct);
            await WaitUntilAsync(i => i.CraneMiddleLimit, ct); // centered
            Log("Crane: centered (MiddleLimit).");

            // 3) Move to target slot (write HR0)
            _state = StorageState.CraneHandshake;
            UpdateUi();

            Log($"Crane: HR0 = {slot} (move to target) …");
            await _io.WriteRegistersAsync(RegisterWrites.None().WithSlotNumber(slot), ct);
            await WaitUntilAsync(i => i.MovingX, ct);
            await WaitUntilAsync(i => !i.MovingX, ct);
            Log("Crane: reached target X.");

            // 4) Put-away: RIGHT ON → then LIFT OFF (down) → then RIGHT OFF → back to Middle
            Log("Crane: RIGHT until RightLimit …");
            await _io.ApplyOutputsAsync(OutputCommand.None.WithCraneRight(true), ct);
            await Task.Delay(120, ct); // small settle helps Factory I/O latch coil
            await WaitUntilAsync(i => i.CraneRightLimit, ct);
            Log("Crane: Right limit reached.");

            Log("Crane: LIFT DOWN (turn Lift OFF; wait MovingZ true→false) …");
            await _io.ApplyOutputsAsync(OutputCommand.None.WithCraneLift(false), ct);
            await WaitUntilAsync(i => i.MovingZ, ct);      // start down
            await WaitUntilAsync(i => !i.MovingZ, ct);     // end down
            Log("Crane: lift down complete.");

            Log("Crane: RIGHT OFF (retract) …");
            await _io.ApplyOutputsAsync(OutputCommand.None.WithCraneRight(false), ct);
            await Task.Delay(120, ct);
            await WaitUntilAsync(i => i.CraneMiddleLimit, ct);
            Log("Crane: back to Middle.");

            // 5) Mark inventory and return home
            _inventory.MarkSlotAsOccupied(row, col);
            Log($"✅ Stored → Slot {slot} marked occupied (Row {row}, Col {col}).");

            Log("Crane: go Idle (55) then clear HR0 …");
            await PulseTargetAsync(CraneIdleSlot, ct);
        }

        #endregion


        #region Safety / Emergency Handling

        // NOTE: EmergencyStop == TRUE means emergency ACTIVE (adapter inverted NC).
        private async Task<InputSnapshot> WaitUntilAsync(Func<InputSnapshot, bool> predicate, CancellationToken ct)
        {
            while (true)
            {
                ct.ThrowIfCancellationRequested();
                var inputs = await _io.ReadInputsAsync(ct);

                if (inputs.EmergencyStop)
                {
                    await HandleEmergencyAsync("Emergency Stop activated");
                    throw new OperationCanceledException("Emergency Stop.");
                }
                if (inputs.Fault)
                {
                    await HandleEmergencyAsync("Fault detected");
                    throw new OperationCanceledException("Fault.");
                }

                if (predicate(inputs))
                    return inputs;

                await Task.Delay(100, ct);
            }
        }

        private async Task HandleEmergencyAsync(string reason)
        {
            Log($"!!! EMERGENCY: {reason}. Stopping outputs …");
            await ForceStopOutputsAsync();
            _cts?.Cancel();
            _state = StorageState.Idle;
            UpdateUi();
        }

        #endregion


        #region Low-level helpers

        private static bool IsValidSlotNumber(int slot) => slot >= 1 && slot <= Rows * Cols;

        private static (int row, int col) DecodeSlot(int slot)
        {
            // Numerical → row/col (row-major), slot 1..54
            int idx = slot - 1;       // 0-based
            int row = idx / Cols;
            int col = idx % Cols;
            return (row, col);
        }

        private async Task ApplyOffTwiceAsync(OutputCommand cmd, CancellationToken ct)
        {
            await _io.ApplyOutputsAsync(cmd, ct);
            await Task.Delay(120, ct);
            await _io.ApplyOutputsAsync(cmd, ct);
        }

        private async Task PulseTargetAsync(int slot, CancellationToken ct)
        {
            await _io.WriteRegistersAsync(RegisterWrites.None().WithSlotNumber(slot), ct);

            // wait start (X or Z), then stop
            var (started, _) = await WaitUntilOrTimeoutAsync(i => i.MovingX || i.MovingZ, TimeSpan.FromSeconds(1.5), ct);
            if (started)
                await WaitUntilOrTimeoutAsync(i => !i.MovingX && !i.MovingZ, TimeSpan.FromSeconds(8), ct);

            // clear to avoid holding target
            await _io.WriteRegistersAsync(RegisterWrites.None().WithSlotNumber(0), ct);
        }

        private async Task<(bool met, InputSnapshot last)> WaitUntilOrTimeoutAsync(
            Func<InputSnapshot, bool> predicate, TimeSpan timeout, CancellationToken ct)
        {
            var end = _clock.UtcNow + timeout;
            InputSnapshot last = await _io.ReadInputsAsync(ct);
            while (_clock.UtcNow < end)
            {
                ct.ThrowIfCancellationRequested();
                last = await _io.ReadInputsAsync(ct);
                if (last.EmergencyStop || last.Fault) break;
                if (predicate(last)) return (true, last);
                await Task.Delay(100, ct);
            }
            return (false, last);
        }

        private async Task ForceStopOutputsAsync(CancellationToken? ct = null)
        {
            var token = ct ?? CancellationToken.None;

            // OFF all actuators (double assert)
            await _io.ApplyOutputsAsync(
                OutputCommand.None
                    .WithInfeed(false)
                    .WithToCrane(false)
                    .WithCraneLeft(false)
                    .WithCraneLift(false)
                    .WithCraneRight(false)
                    .WithExit(false),
                token);
            await Task.Delay(150, token);
            await _io.ApplyOutputsAsync(
                OutputCommand.None
                    .WithInfeed(false)
                    .WithToCrane(false)
                    .WithCraneLeft(false)
                    .WithCraneLift(false)
                    .WithCraneRight(false)
                    .WithExit(false),
                token);

            // Home to Idle and clear target (non-forced)
            try { await PulseTargetAsync(CraneIdleSlot, token); }
            catch (Exception ex) { Log($"Idle pulse failed: {ex.Message}"); }
        }

        #endregion


        #region Inventory helpers (numeric slots 1..54 → row/col 6x9)

        // NOTE: we use DecodeSlot + IsOccupied/MarkSlotAsOccupied on the inventory.
        // If you later add retrieval, also add MarkSlotAsEmpty(row,col).

        #endregion


        #region Utils

        private void Log(string message)
        {
            var t = _clock.UtcNow.ToLocalTime().ToString("HH:mm:ss");
            lstLog.Items.Add($"{t}  {message}");
            lstLog.TopIndex = lstLog.Items.Count - 1;
        }

        private static string OnOff(bool v) => v ? "ON" : "OFF";

        private void btnClearLog_Click(object sender, EventArgs e) => lstLog.Items.Clear();

        #endregion
    }
}
