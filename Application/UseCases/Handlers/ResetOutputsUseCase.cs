using Application.Warehouse.Io;

namespace Application.UseCases.Handlers
{
    public class ResetOutputsUseCase : IResetOutputsUseCase
    {
        private readonly IWarehouseIo _io;

        public ResetOutputsUseCase(IWarehouseIo io)
        {
            _io = io;
        }

        public async Task RunAsync(IProgress<string>? progress, CancellationToken ct)
        {
            progress?.Report("Turning all outputs OFF...");
            await _io.ApplyOutputsAsync(
                OutputCommand.None
                    .WithInfeed(false)
                    .WithToCrane(false)
                    .WithCraneLeft(false)
                    .WithCraneLift(false)
                    .WithCraneRight(false)
                    .WithExit(false),
                ct);

            await Task.Delay(100, ct);

            await _io.WriteRegistersAsync(RegisterWrites.None().WithSlotNumber(55), ct);
            progress?.Report("All outputs are now OFF.");
        }
    }
}
