using Application.Ports;              // IClock
using Application.Warehouse;          // IWarehouseInventory, ICraneAddressEncoder
using Application.Warehouse.Io;       // IWarehouseIo, OutputCommand, RegisterWrites
using Domain;                  // ⬅ if PalletId is in `Domain`, change to: using Domain;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.UseCases.Handlers
{
    public sealed class StorePalletUseCase : IStorePalletUseCase
    {
        private readonly IWarehouseInventory _inventory;
        private readonly IWarehouseIo _io;
        private readonly IClock _clock;
        private readonly ICraneAddressEncoder _encoder;

        // keep same constants as your UC
        private const int CraneIdleSlot = 55; // Factory I/O idle/home
        private const int Rows = 6;
        private const int Cols = 9;           // 6 x 9 = 54 slots

        public StorePalletUseCase(IWarehouseIo io, IWarehouseInventory inventory, ICraneAddressEncoder encoder, IClock clock)
        {
            _io = io;
            _inventory = inventory;
            _encoder = encoder;
            _clock = clock;
        }

        public async Task<bool> RunAsync(PalletId pallet, int slotNumber, IProgress<string>? progress, CancellationToken ct)
        {
            void Log(string m) => progress?.Report(m);

            if (!IsValidSlotNumber(slotNumber))
            {
                Log("❌ Invalid slot. Use 1..54.");
                return false;
            }

            var (row, col) = DecodeSlot(slotNumber);

            // same “occupied” check
            if (_inventory.IsOccupied(row, col))
            {
                Log($"❌ Slot {slotNumber} (Row {row}, Col {col}) is FULL. Cannot store.");
                return false;
            }

            Log($"Store requested → Slot {slotNumber} (Row {row}, Col {col})");

            try
            {
                // === Stage 1: Entry + Load until AtLoad OFF ===
                Log("Stage 1: Entry+Load ON …");
                await _io.ApplyOutputsAsync(OutputCommand.None.WithInfeed(true).WithToCrane(true), ct);

                Log("Waiting AtLoad = OFF (blocked) …");
                await WaitUntilAsync(i => i.AtLoad == false, ct);

                Log("Stopping Entry+Load …");
                await ApplyOffTwiceAsync(OutputCommand.None.WithInfeed(false).WithToCrane(false), ct);

                // === Stage 2: Crane pre-position ===
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

                // === Stage 3: Move to target slot (HR0) ===
                Log($"Crane: HR0 = {slotNumber} (move to target) …");
                await _io.WriteRegistersAsync(RegisterWrites.None().WithSlotNumber(slotNumber), ct);
                await WaitUntilAsync(i => i.MovingX, ct);
                await WaitUntilAsync(i => !i.MovingX, ct);
                Log("Crane: reached target X.");

                // === Stage 4: Put-away sequence ===
                Log("Crane: RIGHT until RightLimit …");
                await _io.ApplyOutputsAsync(OutputCommand.None.WithCraneRight(true), ct);
                await Task.Delay(120, ct); // small settle for Factory I/O
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

                // === Stage 5: Mark inventory + home ===
                _inventory.MarkSlotAsOccupied(row, col);
                Log($"✅ Stored → Slot {slotNumber} marked occupied (Row {row}, Col {col}).");

                Log("Crane: go Idle (55) then clear HR0 …");
                await PulseTargetAsync(CraneIdleSlot, ct);

                return true;
            }
            catch (OperationCanceledException)
            {
                Log("Cycle canceled.");
                await SafeIdleAsync(ct);
                return false;
            }
            catch (Exception ex)
            {
                Log($"Error: {ex.Message}");
                await SafeIdleAsync(ct);
                return false;
            }
        }

        // ===== helpers migrated from UcStorageProcess (UI-free) =====

        private static bool IsValidSlotNumber(int slot) => slot >= 1 && slot <= Rows * Cols;

        private static (int row, int col) DecodeSlot(int slot)
        {
            int idx = slot - 1;       // 0-based
            int row = idx / Cols;
            int col = idx % Cols;
            return (row, col);
        }

        private async Task<InputSnapshot> WaitUntilAsync(Func<InputSnapshot, bool> predicate, CancellationToken ct)
        {
            while (true)
            {
                ct.ThrowIfCancellationRequested();
                var inputs = await _io.ReadInputsAsync(ct);

                // NOTE: in your adapter, EmergencyStop = TRUE means E-Stop ACTIVE
                if (inputs.EmergencyStop)
                    throw new OperationCanceledException("Emergency Stop.");
                if (inputs.Fault)
                    throw new OperationCanceledException("Fault detected.");

                if (predicate(inputs))
                    return inputs;

                await Task.Delay(100, ct);
            }
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

        private async Task SafeIdleAsync(CancellationToken ct)
        {
            try { await ForceStopOutputsAsync(ct); } catch { /* swallow */ }
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

            // Home to Idle and clear target (best effort)
            try { await PulseTargetAsync(CraneIdleSlot, token); } catch { }
        }
    }
}
