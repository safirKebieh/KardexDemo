using Application.Warehouse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Handlers
{
    public sealed class ClearAllSlotsUseCase : IClearAllSlotsUseCase
    {
        private readonly IWarehouseInventory _inventory;

        public ClearAllSlotsUseCase(IWarehouseInventory inventory)
        {
            _inventory = inventory;
        }

        public Task RunAsync()
        {
            for (int r = 0; r < _inventory.RowCount; r++)
            {
                for (int c = 0; c < _inventory.ColumnCount; c++)
                {
                    _inventory.MarkSlotAsEmpty(r, c);
                }
            }

            return Task.CompletedTask;
        }
    }
}
