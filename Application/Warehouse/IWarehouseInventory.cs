namespace Application.Warehouse;

public interface IWarehouseInventory
{
    int RowCount { get; }
    int ColumnCount { get; }

    bool IsOccupied(int row, int column);
    (int row, int column)? GetNextFreeSlot();
    void MarkSlotAsOccupied(int row, int column);
    void MarkSlotAsEmpty(int row, int column);
    void ResetAllSlotsToEmpty();
}
