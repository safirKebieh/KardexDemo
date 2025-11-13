using Application.Ports;              
using Application.Warehouse;          
using Application.Warehouse.Io;       

namespace Application.UseCases.Handlers
{
    public sealed class RetrievePalletUseCase : IRetrievePalletUseCase
    {
        private readonly IWarehouseInventory _inventory;
        private readonly IWarehouseIo _io;
        private readonly IClock _clock;

        private const int CraneIdleSlot = 55;
        private const int TotalRows = 6;
        private const int totalColumns = 9;           

        public RetrievePalletUseCase(IWarehouseIo io, IWarehouseInventory inventory, IClock clock)
        {
            _io = io;
            _inventory = inventory;
            _clock = clock;
        }

        public async Task<bool> RunAsync(int slotNumber, IProgress<string>? progress, CancellationToken ct)
        {
            void Log(string m) => progress?.Report(m);

            if (!IsValidSlotNumber(slotNumber))
            {
                Log("❌ Invalid slot. Use 1..54.");
                return false;
            }

            var (row, col) = DecodeSlot(slotNumber);

            if (!_inventory.IsOccupied(row, col))
            {
                Log($"❌ Slot {slotNumber} (Row {row}, Col {col}) is EMPTY. Cannot retrieve.");
                return false;
            }

            Log($"Retrieve requested → Slot {slotNumber} (Row {row}, Col {col})");

            try
            {
                // Stage A: Move to target slot (HR0 = slot)
                Log($"Crane: HR0 = {slotNumber} (move to slot) …");
                await _io.WriteRegistersAsync(RegisterWrites.None().WithSlotNumber(slotNumber), ct);
                await WaitUntilAsync(i => i.MovingX, ct);
                await WaitUntilAsync(i => !i.MovingX, ct);
                Log("Crane: at target slot.");

                // Stage B: pick — RIGHT ON → LIFT UP → RIGHT OFF back to Middle
                Log("Crane: RIGHT extend (wait RightLimit) …");
                await _io.ApplyOutputsAsync(OutputCommand.None.WithCraneRight(true), ct);
                await Task.Delay(120, ct);
                await WaitUntilAsync(i => i.CraneRightLimit, ct);
                Log("Crane: Right limit reached.");

                Log("Crane: LIFT UP (MovingZ true→false) …");
                await _io.ApplyOutputsAsync(OutputCommand.None.WithCraneLift(true), ct);
                await WaitUntilAsync(i => i.MovingZ, ct);      // start up
                await WaitUntilAsync(i => !i.MovingZ, ct);     // end up
                Log("Crane: lift up complete.");

                Log("Crane: RIGHT OFF (retract) …");
                await _io.ApplyOutputsAsync(OutputCommand.None.WithCraneRight(false), ct);
                await Task.Delay(120, ct);
                await WaitUntilAsync(i => i.CraneMiddleLimit, ct);
                Log("Crane: back to Middle.");

                // Stage C: move to Idle 55 (home)
                Log("Crane: move to Idle (55) …");
                await _io.WriteRegistersAsync(RegisterWrites.None().WithSlotNumber(CraneIdleSlot), ct);
                await WaitUntilAsync(i => i.MovingX, ct);
                await WaitUntilAsync(i => !i.MovingX, ct);
                Log("Crane: at Idle 55.");
                // (اختياري) صفّر الهدف بعد الوصول
                await _io.WriteRegistersAsync(RegisterWrites.None().WithSlotNumber(0), ct);

                // Stage D: place onto unload at home — RIGHT ON → LIFT DOWN → RIGHT OFF
                Log("Crane: RIGHT extend at unload …");
                await _io.ApplyOutputsAsync(OutputCommand.None.WithCraneRight(true), ct);
                await Task.Delay(120, ct);
                await WaitUntilAsync(i => i.CraneRightLimit, ct);

                Log("Crane: LIFT DOWN (turn Lift OFF; MovingZ true→false) …");
                await _io.ApplyOutputsAsync(OutputCommand.None.WithCraneLift(false), ct);
                await WaitUntilAsync(i => i.MovingZ, ct);      // start down
                await WaitUntilAsync(i => !i.MovingZ, ct);     // end down
                Log("Crane: lift down complete.");

                Log("Crane: RIGHT OFF (retract to middle) …");
                await _io.ApplyOutputsAsync(OutputCommand.None.WithCraneRight(false), ct);
                await Task.Delay(120, ct);
                await WaitUntilAsync(i => i.CraneMiddleLimit, ct);
                Log("Crane: back to Middle.");

                // Stage E: Unload + Exit conveyors until AtExit
                Log("Unload & Exit conveyors: ON …");
                await _io.ApplyOutputsAsync(OutputCommand.None.WithUnload(true).WithExit(true), ct);
                await WaitUntilAsync(i => i.AtExit, ct);
                await _io.ApplyOutputsAsync(OutputCommand.None.WithUnload(false).WithExit(false), ct);
                Log("Unload complete. Conveyors OFF.");

                // Stage F: mark inventory empty
                _inventory.MarkSlotAsEmpty(row, col);
                Log($"✅ Retrieved → Slot {slotNumber} marked EMPTY (Row {row}, Col {col}).");

                return true;
            }
            catch (OperationCanceledException)
            {
                Log("Retrieve canceled.");
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

        private static bool IsValidSlotNumber(int slot) => slot >= 1 && slot <= TotalRows * totalColumns;

        private static (int row, int col) DecodeSlot(int slot)
        {
            int index = slot - 1; // 0-based
            int row = index / totalColumns;
            int column = index % totalColumns;
            return (row, column);
        }

        private async Task<InputSnapshot> WaitUntilAsync(Func<InputSnapshot, bool> predicate, CancellationToken ct)
        {
            while (true)
            {
                ct.ThrowIfCancellationRequested();
                var inputs = await _io.ReadInputsAsync(ct);

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
            try { await ForceStopOutputsAsync(ct); } catch { /* ignore */ }
        }

        private async Task ForceStopOutputsAsync(CancellationToken? ct = null)
        {
            var token = ct ?? CancellationToken.None;

            await _io.ApplyOutputsAsync(
                OutputCommand.None
                    .WithInfeed(false)
                    .WithToCrane(false)
                    .WithCraneLeft(false)
                    .WithCraneLift(false)
                    .WithCraneRight(false)
                    .WithExit(false)
                    .WithUnload(false),
                token);
            await Task.Delay(150, token);
            await _io.ApplyOutputsAsync(
                OutputCommand.None
                    .WithInfeed(false)
                    .WithToCrane(false)
                    .WithCraneLeft(false)
                    .WithCraneLift(false)
                    .WithCraneRight(false)
                    .WithExit(false)
                    .WithUnload(false),
                token);

            try { await PulseTargetAsync(CraneIdleSlot, token); } catch { }
        }
    }
}
