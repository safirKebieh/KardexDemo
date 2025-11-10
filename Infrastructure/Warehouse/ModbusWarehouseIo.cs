using Application.Ports;
using Application.Warehouse.Io;

namespace Infrastructure.Warehouse;

/// Maps Factory I/O Modbus tags to the clean I/O abstraction.
public sealed class ModbusWarehouseIo : IWarehouseIo
{
    private readonly IModbusService _modbus;

    // ---- Address map (adjust if your scene differs) ----
    // Discrete Inputs (DI) - "Input n" in Factory I/O
    private const ushort DI_AtEntry = 0;   // "At Entry"
    private const ushort DI_AtLoad = 1;   // "At Load"
    private const ushort DI_MovingX = 7;   // "Moving X"
    private const ushort DI_MovingZ = 8;   // "Moving Z"
    private const ushort DI_Start = 9;   // "Start"  (not used here)
    private const ushort DI_Reset = 10;  // "Reset"  (not used here)
    private const ushort DI_Stop = 11;  // "Stop"   (not used here)
    private const ushort DI_EStop = 12;  // "Emergency stop"
    private const ushort DI_AtExit = 6;   // NEW
    private const ushort DI_Auto = 13;  // "Auto"   (optional)
    private const ushort DI_FactoryRun = 14;  // "FACTORY I/O (Running)" (optional)
    private const ushort DI_CraneLeftLimit = 2;  // "Stacker Crane 1 Left Limit"
    private const ushort DI_CraneMiddleLimit = 3;  // "Stacker Crane 1 Middle Limit"
    private const ushort DI_CraneRightLimit = 4;

    // Coils (DO) - "Coil n" in Factory I/O
    private const ushort DO_EntryConv = 0;   // "Entry Conveyor"
    private const ushort DO_LoadConv = 1;   // "Load Conveyor"
    private const ushort DO_ExitConv = 6;   // "Exit Conveyor"
    private const ushort DO_CraneLeft = 2;  // "Stacker Crane 1 (Left)"
    private const ushort DO_CraneLift = 3;  // "Stacker Crane 1 Lift"
    private const ushort DO_CraneRight = 4;
    private const ushort DO_UnloadConv = 5;  // NEW

    // The rest (Forks/Lift/Start light/Reset light/Stop light) are available if needed.

    // Holding Registers
    private const ushort HR_TargetSlot = 0;   // "Target Position" (Numerical mode)

    public ModbusWarehouseIo(IModbusService modbus) => _modbus = modbus;

    public async Task<InputSnapshot> ReadInputsAsync(CancellationToken ct = default)
    {
        // Read 0..14 in one shot (15 bits)
        var di = await _modbus.ReadDiscreteInputsAsync(0, 15, ct);
        bool atEntry = Safe(di, DI_AtEntry);
        bool atLoad = Safe(di, DI_AtLoad);
        bool movingX = Safe(di, DI_MovingX);
        bool movingZ = Safe(di, DI_MovingZ);
        bool eStop = !Safe(di, DI_EStop);
        bool atExit = Safe(di, DI_AtExit);          // NEW
        bool leftLim = Safe(di, DI_CraneLeftLimit);
        bool midLim = Safe(di, DI_CraneMiddleLimit);
        bool rightLim = Safe(di, DI_CraneRightLimit);

        bool craneBusy = movingX || movingZ;
        bool craneReady = !craneBusy && !eStop;
        bool craneDone = !craneBusy;
        bool fault = false;

        return new InputSnapshot(
            AtEntry: atEntry,
            AtLoad: atLoad,
            CraneReady: craneReady,
            CraneBusy: craneBusy,
            CraneDone: craneDone,
            EmergencyStop: eStop,
            Fault: fault,
            CraneLeftLimit: leftLim,
            CraneMiddleLimit: midLim,
            CraneRightLimit: rightLim,
            MovingX: movingX,
            MovingZ: movingZ,
            AtExit: atExit                
        );
    }

    public async Task ApplyOutputsAsync(OutputCommand command, CancellationToken ct = default)
    {
        async Task SetCoilAsync(ushort addr, bool value)
        {
            await _modbus.WriteCoilAsync(addr, value, ct);
            await Task.Delay(50, ct);
        }

        if (command.InfeedConveyor.HasValue)
            await SetCoilAsync(DO_EntryConv, command.InfeedConveyor.Value);

        if (command.ToCraneConveyor.HasValue)
            await SetCoilAsync(DO_LoadConv, command.ToCraneConveyor.Value);

        if (command.ExitConveyor.HasValue)
            await SetCoilAsync(DO_ExitConv, command.ExitConveyor.Value);

        if (command.UnloadConveyor.HasValue)                 // NEW
            await SetCoilAsync(DO_UnloadConv, command.UnloadConveyor.Value);

        if (command.CraneLeft.HasValue)
            await SetCoilAsync(DO_CraneLeft, command.CraneLeft.Value);

        if (command.CraneLift.HasValue)
            await SetCoilAsync(DO_CraneLift, command.CraneLift.Value);

        if (command.CraneRight.HasValue)                 // NEW
            await SetCoilAsync(DO_CraneRight, command.CraneRight.Value);
    }

    public async Task WriteRegistersAsync(RegisterWrites writes, CancellationToken ct = default)
    {
        if (writes is null) return;

        if (writes.SlotNumber.HasValue)
        {
            ushort value = (ushort)writes.SlotNumber.Value;
            await _modbus.WriteHoldingRegisterAsync(HR_TargetSlot, value, ct);
        }

        // Future: support analog X/Z or 5-bit addressing here if you change modes.
    }

    private static bool Safe(bool[] arr, int index) =>
        index >= 0 && index < arr.Length && arr[index];
}
