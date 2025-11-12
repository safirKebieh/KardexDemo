namespace Application.UseCases;

public interface IStorePalletUseCase
{
    Task<bool> RunAsync(int slotNumber, IProgress<string>? progress, CancellationToken ct);
}

