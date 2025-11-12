
namespace Application.UseCases
{
    public interface IRetrievePalletUseCase
    {
        Task<bool> RunAsync(int slotNumber, IProgress<string>? progress, CancellationToken ct);
    }

}
