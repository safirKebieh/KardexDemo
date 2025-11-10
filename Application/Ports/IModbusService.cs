namespace Application.Ports
{
    public interface IModbusService
    {
        // Connection
        Task ConnectAsync(string host, int port, byte unitId, CancellationToken ct = default);
        Task<bool> IsConnectedAsync();
        Task DisconnectAsync();

        // Read
        Task<bool[]> ReadDiscreteInputsAsync(ushort start, ushort count, CancellationToken ct = default);
        Task<bool[]> ReadCoilsAsync(ushort start, ushort count, CancellationToken ct = default);
        Task<ushort[]> ReadHoldingRegistersAsync(ushort start, ushort count, CancellationToken ct = default);
        Task<ushort[]> ReadInputRegistersAsync(ushort start, ushort count, CancellationToken ct = default);

        // Write
        Task WriteCoilAsync(ushort address, bool value, CancellationToken ct = default);
        Task WriteCoilsAsync(ushort start, bool[] values, CancellationToken ct = default);
        Task WriteHoldingRegisterAsync(ushort address, ushort value, CancellationToken ct = default);
        Task WriteHoldingRegistersAsync(ushort start, ushort[] values, CancellationToken ct = default);
    }
}
