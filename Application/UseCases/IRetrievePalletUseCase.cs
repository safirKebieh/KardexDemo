using Domain;

namespace Application.UseCases
{
    public interface IRetrievePalletUseCase
    {
        Task<bool> RunAsync(PalletId pallet, int slotNumber, IProgress<string>? progress, CancellationToken ct);
    }

}
