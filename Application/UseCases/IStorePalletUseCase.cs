using Domain;
using System.Threading;
using System.Threading.Tasks;

namespace Application.UseCases;

public interface IStorePalletUseCase
{
    Task<bool> RunAsync(PalletId pallet, int slotNumber, IProgress<string>? progress, CancellationToken ct);
}

