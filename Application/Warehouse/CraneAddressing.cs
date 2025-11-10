namespace Application.Warehouse;

// Which addressing mode the crane uses.
public enum CraneAddressMode
{
    Numerical, // SlotNumber 1..N
    Analog,    // X/Z axes (future)
    Digital    // 5-bit (future)
}

// Unified logical address for any mode.
public sealed class CraneAddress
{
    public int? SlotNumber { get; init; }       // Numerical
    public int? X { get; init; }                // Analog (future)
    public int? Z { get; init; }                // Analog (future)
    public bool[]? Bits { get; init; }          // Digital 5-bit (future)

    public CraneAddress(int slotNumber)
    {
        SlotNumber = slotNumber;
    }

    public CraneAddress(int x, int z, bool _forAnalog)
    {
        X = x; Z = z;
    }

    public CraneAddress(bool[] bits)
    {
        Bits = bits;
    }
}

// Encodes (row, column) → a hardware-ready address based on the selected mode.
public interface ICraneAddressEncoder
{
    CraneAddressMode Mode { get; }
    CraneAddress Encode(int row, int column);
}

// Numerical encoder: (row, col) -> slotNumber using row-major numbering.
// (0,0)=1, (0,1)=2, …, (1,0)=ColumnCount+1, etc.
public sealed class NumericalAddressEncoder : ICraneAddressEncoder
{
    private readonly int _rowCount;
    private readonly int _columnCount;

    public CraneAddressMode Mode => CraneAddressMode.Numerical;

    public NumericalAddressEncoder(int rowCount, int columnCount)
    {
        _rowCount = rowCount;
        _columnCount = columnCount;
    }

    public CraneAddress Encode(int row, int column)
    {
        // Validate bounds (optional but good)
        if ((uint)row >= (uint)_rowCount || (uint)column >= (uint)_columnCount)
            throw new ArgumentOutOfRangeException($"Slot ({row},{column}) out of range.");

        int slotNumber = row * _columnCount + column + 1;
        return new CraneAddress(slotNumber);
    }
}
