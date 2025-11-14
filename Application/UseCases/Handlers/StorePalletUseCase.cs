using Application.Ports;
using Application.Warehouse;
using Application.Warehouse.Io;
using Application.UseCases.Base;

namespace Application.UseCases.Handlers
{
    public sealed class StorePalletUseCase : CraneUseCaseBase, IStorePalletUseCase
    {
        private readonly IWarehouseInventory _inventory;

        private const int CraneIdleSlot = 55;
        private const int TotalRows = 6;
        private const int TotalColumns = 9;

        public StorePalletUseCase(IWarehouseIo io, IWarehouseInventory inventory, IClock clock): base(io, clock)
        {
            _inventory = inventory;
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
                await _io.ApplyOutputsAsync(
                    OutputCommand.None
                        .WithInfeed(true)
                        .WithToCrane(true),
                    ct);

                Log("Waiting AtLoad = OFF (blocked) …");
                await WaitUntilAsync(i => i.AtLoad == false, ct);

                Log("Stopping Entry+Load …");
                await ApplyOffTwiceAsync(
                    OutputCommand.None
                        .WithInfeed(false)
                        .WithToCrane(false),
                    ct);

                // === Stage 2: Crane pre-position ===
                Log("Crane: LEFT until LeftLimit …");
                await _io.ApplyOutputsAsync(
                    OutputCommand.None.WithCraneLeft(true),
                    ct);
                await WaitUntilAsync(i => i.CraneLeftLimit, ct);

                Log("Crane: LIFT UP (MovingZ true→false) …");
                await _io.ApplyOutputsAsync(
                    OutputCommand.None.WithCraneLift(true),
                    ct);
                await WaitUntilAsync(i => i.MovingZ, ct);      // start up
                await WaitUntilAsync(i => !i.MovingZ, ct);     // end up

                Log("Crane: LEFT OFF (retract forks) …");
                await _io.ApplyOutputsAsync(
                    OutputCommand.None.WithCraneLeft(false),
                    ct);
                await WaitUntilAsync(i => i.CraneMiddleLimit, ct);
                Log("Crane: centered (MiddleLimit).");

                // === Stage 3: Move to target slot (HR0) ===
                Log($"Crane: HR0 = {slotNumber} (move to target) …");
                await _io.WriteRegistersAsync(
                    RegisterWrites.None().WithSlotNumber(slotNumber),
                    ct);
                await WaitUntilAsync(i => i.MovingX, ct);
                await WaitUntilAsync(i => !i.MovingX, ct);
                Log($"Crane: reached Slot {slotNumber}.");

                // === Stage 4: Put-away sequence ===
                Log("Crane: Moving RIGHT until RightLimit ON …");
                await _io.ApplyOutputsAsync(
                    OutputCommand.None.WithCraneRight(true),
                    ct);
                await Task.Delay(120, ct);
                await WaitUntilAsync(i => i.CraneRightLimit, ct);
                Log("Crane: Right limit reached.");

                Log("Crane: LIFT DOWN (turn Lift OFF; wait MovingZ true→false) …");
                await _io.ApplyOutputsAsync(
                    OutputCommand.None.WithCraneLift(false),
                    ct);
                await WaitUntilAsync(i => i.MovingZ, ct);      // start down
                await WaitUntilAsync(i => !i.MovingZ, ct);     // end down
                Log("Crane: lift down complete.");

                Log("Crane: RIGHT OFF (retract) …");
                await _io.ApplyOutputsAsync(
                    OutputCommand.None.WithCraneRight(false),
                    ct);
                await Task.Delay(120, ct);
                await WaitUntilAsync(i => i.CraneMiddleLimit, ct);
                Log("Crane: back to Middle.");

                // === Stage 5: Mark inventory + home ===
                _inventory.MarkSlotAsOccupied(row, col);
                Log($"✅ Stored → Slot {slotNumber} marked occupied (Row {row}, Col {col}).");

                Log("Crane: go Idle then clear HR0 …");
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

        private static bool IsValidSlotNumber(int slot) =>
            slot >= 1 && slot <= TotalRows * TotalColumns;

        private static (int row, int col) DecodeSlot(int slot)
        {
            int idx = slot - 1;            // 0-based
            int row = idx / TotalColumns;
            int col = idx % TotalColumns;
            return (row, col);
        }
    }
}
