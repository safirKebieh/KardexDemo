using Application.Ports;
using Application.Warehouse.Io;

namespace Application.UseCases.Base
{
    public abstract class CraneUseCaseBase
    {
        protected readonly IWarehouseIo _io;
        protected readonly IClock _clock;

        protected CraneUseCaseBase(IWarehouseIo io, IClock clock)
        {
            _io = io;
            _clock = clock;
        }

        // ======== COMMON HELPERS FOR STORE & RETRIEVE ========
        protected async Task<InputSnapshot> WaitUntilAsync(
            Func<InputSnapshot, bool> predicate, CancellationToken ct)
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

        protected async Task ApplyOffTwiceAsync(OutputCommand cmd, CancellationToken ct)
        {
            await _io.ApplyOutputsAsync(cmd, ct);
            await Task.Delay(120, ct);
            await _io.ApplyOutputsAsync(cmd, ct);
        }

        protected async Task PulseTargetAsync(int slot, CancellationToken ct)
        {
            await _io.WriteRegistersAsync(RegisterWrites.None().WithSlotNumber(slot), ct);

            // wait for movement start
            var (started, _) = await WaitUntilOrTimeoutAsync(
                i => i.MovingX || i.MovingZ,
                TimeSpan.FromSeconds(1.5),
                ct);

            // wait for movement end
            if (started)
            {
                await WaitUntilOrTimeoutAsync(
                    i => !i.MovingX && !i.MovingZ,
                    TimeSpan.FromSeconds(8),
                    ct);
            }

            // clear target after reaching
            await _io.WriteRegistersAsync(RegisterWrites.None().WithSlotNumber(0), ct);
        }

        protected async Task<(bool met, InputSnapshot last)> WaitUntilOrTimeoutAsync(
            Func<InputSnapshot, bool> predicate, TimeSpan timeout, CancellationToken ct)
        {
            var end = _clock.UtcNow + timeout;
            InputSnapshot last = await _io.ReadInputsAsync(ct);

            while (_clock.UtcNow < end)
            {
                ct.ThrowIfCancellationRequested();
                last = await _io.ReadInputsAsync(ct);

                if (last.EmergencyStop || last.Fault)
                    break;

                if (predicate(last))
                    return (true, last);

                await Task.Delay(100, ct);
            }

            return (false, last);
        }

        protected async Task SafeIdleAsync(CancellationToken ct)
        {
            try { await ForceStopOutputsAsync(ct); } catch { /* ignore */ }
        }

        protected async Task ForceStopOutputsAsync(CancellationToken? ct = null)
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
        }
    }
}
