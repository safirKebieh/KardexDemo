using Application.Warehouse.Io;
using Application.Warehouse;      // ICraneAddressEncoder (kept if you later switch to Row/Col)
using Application.StorageProcess;
using Application.Ports;          // IClock

namespace UI
{
    public partial class UcRetrievePallet : UserControl
    {
        #region Fields & Constructor

        private readonly IWarehouseInventory _inventory;
        private readonly IWarehouseIo _io;
        private readonly IClock _clock;
        private readonly ICraneAddressEncoder _encoder;

        private CancellationTokenSource? _cts;
        private StorageState _state = StorageState.Idle;

        private const int Rows = 6;
        private const int Cols = 9;
        private const int CraneIdleSlot = 55;   // Factory I/O "home"

        public UcRetrievePallet(
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
            numSlot.Minimum = 1;
            numSlot.Maximum = Rows * Cols;  // 54
            UpdateUi();
        }

        #endregion


        #region UI updates

        private void UpdateUi()
        {
            lblState.Text = $"State: {_state}";
            var slot = (int)numSlot.Value;
            var (row, col) = DecodeSlot(slot);
            lblSelected.Text = $"Selected: Slot {slot}  (Row {row}, Col {col})";

            btnRetrieve.Enabled = (_state == StorageState.Idle);
            btnStop.Enabled = (_state != StorageState.Idle);
        }

        #endregion


        #region Buttons

        private async void btnRetrieve_Click(object sender, EventArgs e)
        {
            if (_state != StorageState.Idle) return;

            int slot = (int)numSlot.Value;
            if (!IsValidSlotNumber(slot))
            {
                Log("❌ Invalid slot. Use 1..54.");
                return;
            }

            var (row, col) = DecodeSlot(slot);
            if (!_inventory.IsOccupied(row, col))
            {
                Log($"❌ Slot {slot} (Row {row}, Col {col}) is EMPTY. Nothing to retrieve.");
                return;
            }

            Log($"Retrieve requested → Slot {slot} (Row {row}, Col {col})");

            _cts?.Cancel();
            _cts = new CancellationTokenSource();

            try
            {
                btnRetrieve.Enabled = false;
                btnStop.Enabled = true;

                await RetrieveOneAsync(slot, _cts.Token);

                _state = StorageState.Idle;
                UpdateUi();
                Log("✅ Retrieve cycle complete.");
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
                btnRetrieve.Enabled = true;
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
                    $"AtExit={OnOff(snap.AtExit)}, MovX={OnOff(snap.MovingX)}, MovZ={OnOff(snap.MovingZ)}");
            }
            catch (Exception ex) { Log($"Reset snapshot failed: {ex.Message}"); }
        }

        private void btnClearLog_Click(object sender, EventArgs e) => lstLog.Items.Clear();

        private void numSlot_ValueChanged(object sender, EventArgs e) => UpdateUi();

        #endregion


        #region Core: Manual Retrieve Sequence

        // Process you asked for:
        // 1) Move crane to target slot (HR0 = slot, wait MovingX true→false)
        // 2) RIGHT ON (extend forks) → LIFT UP → RIGHT OFF (retract to middle)
        // 3) Move crane to Idle 55
        // 4) RIGHT ON → LIFT DOWN → RIGHT OFF
        // 5) Unload conveyor ON until AtExit = ON → conveyors OFF
        // 6) Mark slot empty
        private async Task RetrieveOneAsync(int slot, CancellationToken ct)
        {
            var (row, col) = DecodeSlot(slot);

            // Stage A: Move to slot
            _state = StorageState.CraneHandshake;
            UpdateUi();

            Log($"Crane: HR0 = {slot} (move to slot) …");
            await _io.WriteRegistersAsync(RegisterWrites.None().WithSlotNumber(slot), ct);
            await WaitUntilAsync(i => i.MovingX, ct);
            await WaitUntilAsync(i => !i.MovingX, ct);
            Log("Crane: at target slot.");

            // Stage B: pick — Right ON → Lift UP → Right OFF back to Middle
            Log("Crane: RIGHT extend (wait RightLimit) …");
            await _io.ApplyOutputsAsync(OutputCommand.None.WithCraneRight(true), ct);
            await Task.Delay(120, ct);
            await WaitUntilAsync(i => i.CraneRightLimit, ct);
            Log("Crane: Right limit reached.");

            Log("Crane: LIFT UP (MovingZ true→false) …");
            await _io.ApplyOutputsAsync(OutputCommand.None.WithCraneLift(true), ct);
            await WaitUntilAsync(i => i.MovingZ, ct);      // Z start
            await WaitUntilAsync(i => !i.MovingZ, ct);     // Z stop
            Log("Crane: lifted pallet.");

            Log("Crane: RIGHT OFF (retract) …");
            await _io.ApplyOutputsAsync(OutputCommand.None.WithCraneRight(false), ct);
            await Task.Delay(120, ct);
            await WaitUntilAsync(i => i.CraneMiddleLimit, ct);
            Log("Crane: back to Middle.");

            // Stage C: move to Idle/Home 55
            Log("Crane: go Idle (55) …");
            await _io.WriteRegistersAsync(RegisterWrites.None().WithSlotNumber(CraneIdleSlot), ct);
            await WaitUntilAsync(i => i.MovingX, ct);
            await WaitUntilAsync(i => !i.MovingX, ct);
            // Clear target so not latched
            await _io.WriteRegistersAsync(RegisterWrites.None().WithSlotNumber(0), ct);

            // Stage D: drop — Right ON → Lift DOWN → Right OFF
            Log("Crane: RIGHT extend (to unload area) …");
            await _io.ApplyOutputsAsync(OutputCommand.None.WithCraneRight(true), ct);
            await Task.Delay(120, ct);
            await WaitUntilAsync(i => i.CraneRightLimit, ct);

            Log("Crane: LIFT DOWN (MovingZ true→false) …");
            await _io.ApplyOutputsAsync(OutputCommand.None.WithCraneLift(false), ct);
            await WaitUntilAsync(i => i.MovingZ, ct);      // Z start down
            await WaitUntilAsync(i => !i.MovingZ, ct);     // Z stop down
            Log("Crane: dropped pallet.");

            Log("Crane: RIGHT OFF (retract) …");
            await _io.ApplyOutputsAsync(OutputCommand.None.WithCraneRight(false), ct);
            await Task.Delay(120, ct);
            await WaitUntilAsync(i => i.CraneMiddleLimit, ct);
            Log("Crane: back to Middle.");

            // Stage E: Unload conveyor until AtExit detects → OFF
            Log("Unload conveyor ON → wait AtExit = ON …");
            await _io.ApplyOutputsAsync(OutputCommand.None.WithExit(true).WithUnload(true), ct);
            await WaitUntilAsync(i => i.AtExit, ct);
            await _io.ApplyOutputsAsync(OutputCommand.None.WithExit(false).WithUnload(false), ct);
            Log("Unload complete. Conveyors OFF.");

            // Stage F: mark slot empty
            _inventory.MarkSlotAsEmpty(row, col); // ensure your inventory has this API
            Log($"✅ Retrieved → Slot {slot} marked EMPTY (Row {row}, Col {col}).");
        }

        #endregion


        #region Safety / Emergency

        // EmergencyStop == TRUE means emergency ACTIVE (adapter inverted NC).
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
            int idx = slot - 1;
            int row = idx / Cols;
            int col = idx % Cols;
            return (row, col);
        }

        private async Task ForceStopOutputsAsync(CancellationToken? ct = null)
        {
            var token = ct ?? CancellationToken.None;

            // OFF everything (double assert)
            await _io.ApplyOutputsAsync(
                OutputCommand.None
                    .WithInfeed(false)
                    .WithToCrane(false)
                    .WithCraneLeft(false)
                    .WithCraneLift(false)
                    .WithCraneRight(false)
                    .WithUnload(false)
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
                    .WithUnload(false)
                    .WithExit(false),
                token);

            // Home to Idle and clear HR0
            try
            {
                await _io.WriteRegistersAsync(RegisterWrites.None().WithSlotNumber(CraneIdleSlot), token);
                await WaitUntilOrTimeoutAsync(i => i.MovingX, TimeSpan.FromSeconds(1.5), token);
                await WaitUntilOrTimeoutAsync(i => !i.MovingX, TimeSpan.FromSeconds(8), token);
                await _io.WriteRegistersAsync(RegisterWrites.None().WithSlotNumber(0), token);
            }
            catch (Exception ex)
            {
                Log($"Home pulse failed: {ex.Message}");
            }
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

        #endregion


        #region Utils

        private void Log(string message)
        {
            var t = _clock.UtcNow.ToLocalTime().ToString("HH:mm:ss");
            lstLog.Items.Add($"{t}  {message}");
            lstLog.TopIndex = lstLog.Items.Count - 1;
        }

        private static string OnOff(bool v) => v ? "ON" : "OFF";

        #endregion
    }
}
