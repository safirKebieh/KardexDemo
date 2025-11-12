using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases
{
    public interface IRetrievePalletUseCase
    {
        Task<bool> RunAsync(PalletId pallet, int slotNumber, IProgress<string>? progress, CancellationToken ct);
    }

}
