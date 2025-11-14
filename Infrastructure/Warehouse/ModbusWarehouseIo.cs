using Application.Ports;
using Application.Warehouse.Io;

namespace Infrastructure.Warehouse;

/// Maps Factory I/O Modbus tags to the clean I/O abstraction.
public sealed class ModbusWarehouseIo : IWarehouseIo
{
    private readonly IModbusService _modbus;

    // ---- Address map ----

    // Discrete Inputs - "Input" in Factory I/O
    private const ushort DI_AtEntry = 0;   
    private const ushort DI_AtLoad = 1;
    private const ushort DI_CraneLeftLimit = 2;
    private const ushort DI_CraneMiddleLimit = 3;
    private const ushort DI_CraneRightLimit = 4;
    private const ushort DI_AtExit = 6;
    private const ushort DI_MovingX = 7;  
    private const ushort DI_MovingZ = 8; 
    private const ushort DI_EStop = 12;  
 
    // Coils (DO) - "Coil" in Factory I/O
    private const ushort DO_EntryConv = 0;   
    private const ushort DO_LoadConv = 1;
    private const ushort DO_CraneLeft = 2;
    private const ushort DO_CraneLift = 3;
    private const ushort DO_CraneRight = 4;
    private const ushort DO_UnloadConv = 5;


    // Holding Registers
    private const ushort HR_StackerCraneTargetPosition = 0;  

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

        if (command.UnloadConveyor.HasValue)                
            await SetCoilAsync(DO_UnloadConv, command.UnloadConveyor.Value);

        if (command.CraneLeft.HasValue)
            await SetCoilAsync(DO_CraneLeft, command.CraneLeft.Value);

        if (command.CraneLift.HasValue)
            await SetCoilAsync(DO_CraneLift, command.CraneLift.Value);

        if (command.CraneRight.HasValue)                
            await SetCoilAsync(DO_CraneRight, command.CraneRight.Value);
    }

    public async Task WriteRegistersAsync(RegisterWrites writes, CancellationToken ct = default)
    {
        if (writes is null) return;

        if (writes.SlotNumber.HasValue)
        {
            ushort value = (ushort)writes.SlotNumber.Value;
            await _modbus.WriteHoldingRegisterAsync(HR_StackerCraneTargetPosition, value, ct);
        }
    }

    private static bool Safe(bool[] arr, int index) =>
        index >= 0 && index < arr.Length && arr[index];
}
