namespace Application.Warehouse.Io;

/// Logical register writes (strongly typed), extend as needed.
public sealed class RegisterWrites
{
    public int? SlotNumber { get; private set; }     // Numerical addressing
    public (int X, int Z)? XzTarget { get; private set; } // Analog mode (future)
    public bool[]? AddressBits { get; private set; } // Digital 5-bit (future)

    public static RegisterWrites None() => new();

    public RegisterWrites WithSlotNumber(int slot)
    {
        SlotNumber = slot;
        return this;
    }

    public RegisterWrites WithXzTarget(int x, int z)
    {
        XzTarget = (x, z);
        return this;
    }

    public RegisterWrites WithAddressBits(bool[] bits)
    {
        AddressBits = bits;
        return this;
    }
}
