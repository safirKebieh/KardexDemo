namespace Application.Warehouse;

public sealed class WarehouseInventory : IWarehouseInventory
{
    private readonly bool[,] _occupied;

    public int RowCount { get; }
    public int ColumnCount { get; }

    public WarehouseInventory(int rowCount, int columnCount)
    {
        if (rowCount <= 0 || columnCount <= 0)
            throw new ArgumentOutOfRangeException();

        RowCount = rowCount;
        ColumnCount = columnCount;

        _occupied = new bool[rowCount, columnCount];
    }

    public bool IsOccupied(int row, int column)
    {
        Validate(row, column);
        return _occupied[row, column];
    }

    public (int row, int column)? GetNextFreeSlot()
    {
        for (int r = 0; r < RowCount; r++)
            for (int c = 0; c < ColumnCount; c++)
                if (!_occupied[r, c])
                    return (r, c);

        return null; 
    }

    public void MarkSlotAsOccupied(int row, int column)
    {
        Validate(row, column);
        _occupied[row, column] = true;
    }

    public void MarkSlotAsEmpty(int row, int column)
    {
        Validate(row, column);
        _occupied[row, column] = false;
    }

    public void ResetAllSlotsToEmpty()
    {
        Array.Clear(_occupied, 0, _occupied.Length);
    }

    private void Validate(int row, int column)
    {
        if ((uint)row >= RowCount || (uint)column >= ColumnCount)
            throw new ArgumentOutOfRangeException(
                $"Slot ({row},{column}) is outside the warehouse boundaries."
            );
    }
}
