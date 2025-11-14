namespace Application.Warehouse.Io;

public interface IWarehouseIo
{
   
    Task<InputSnapshot> ReadInputsAsync(CancellationToken ct = default);

 
    Task ApplyOutputsAsync(OutputCommand command, CancellationToken ct = default);

  
    Task WriteRegistersAsync(RegisterWrites writes, CancellationToken ct = default);
}
