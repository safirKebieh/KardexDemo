namespace Application.Warehouse.Io;

public sealed class RegisterWrites
{
    public int? SlotNumber { get; private set; }    

    public static RegisterWrites None() => new();

    public RegisterWrites WithSlotNumber(int slot)
    {
        SlotNumber = slot;
        return this;
    }
}
