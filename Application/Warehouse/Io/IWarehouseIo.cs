namespace Application.Warehouse.Io;

public interface IWarehouseIo
{
    // Read all inputs in one go (coherent snapshot).
    Task<InputSnapshot> ReadInputsAsync(CancellationToken ct = default);

    // Set/clear outputs; only non-null fields are applied.
    Task ApplyOutputsAsync(OutputCommand command, CancellationToken ct = default);

    // Write logical registers (e.g., slot number). Only provided fields are applied.
    Task WriteRegistersAsync(RegisterWrites writes, CancellationToken ct = default);
}
