using System.Net.Sockets;
using NModbus;
using Application.Ports;

namespace Infrastructure.Communication
{
    public sealed class ModbusService : IModbusService, IDisposable
    {
        private TcpClient? _tcp;
        private IModbusMaster? _master;
        private byte _unitId;

        public async Task ConnectAsync(string host, int port, byte unitId, CancellationToken ct = default)
        {
            _unitId = unitId;
            _tcp = new TcpClient { NoDelay = true };

            await _tcp.ConnectAsync(host, port, ct);

            var factory = new ModbusFactory();
            _master = factory.CreateMaster(_tcp);
        }

        public Task<bool> IsConnectedAsync() =>
            Task.FromResult(_tcp is not null && _tcp.Connected && _master is not null);

        public Task DisconnectAsync()
        {
            try { _tcp?.Close(); } catch { /* ignore */ }
            _master = null;
            _tcp = null;
            return Task.CompletedTask;
        }

        // READ
        public Task<bool[]> ReadDiscreteInputsAsync(ushort start, ushort count, CancellationToken ct = default)
        {
            ct.ThrowIfCancellationRequested();
            EnsureConnected();
            return Task.FromResult(_master!.ReadInputs(_unitId, start, count));
        }

        public Task<bool[]> ReadCoilsAsync(ushort start, ushort count, CancellationToken ct = default)
        {
            ct.ThrowIfCancellationRequested();
            EnsureConnected();
            return Task.FromResult(_master!.ReadCoils(_unitId, start, count));
        }

        public Task<ushort[]> ReadHoldingRegistersAsync(ushort start, ushort count, CancellationToken ct = default)
        {
            ct.ThrowIfCancellationRequested();
            EnsureConnected();
            return Task.FromResult(_master!.ReadHoldingRegisters(_unitId, start, count));
        }

        public Task<ushort[]> ReadInputRegistersAsync(ushort start, ushort count, CancellationToken ct = default)
        {
            ct.ThrowIfCancellationRequested();
            EnsureConnected();
            return Task.FromResult(_master!.ReadInputRegisters(_unitId, start, count));
        }

        // WRITE
        public Task WriteCoilAsync(ushort address, bool value, CancellationToken ct = default)
        {
            EnsureConnected();
            _master!.WriteSingleCoil(_unitId, address, value);
            return Task.CompletedTask;
        }

        public Task WriteCoilsAsync(ushort start, bool[] values, CancellationToken ct = default)
        {
            ct.ThrowIfCancellationRequested();
            EnsureConnected();
            _master!.WriteMultipleCoils(_unitId, start, values);
            return Task.CompletedTask;
        }

        public Task WriteHoldingRegisterAsync(ushort address, ushort value, CancellationToken ct = default)
        {
            ct.ThrowIfCancellationRequested();
            EnsureConnected();
            _master!.WriteSingleRegister(_unitId, address, value);
            return Task.CompletedTask;
        }

        public Task WriteHoldingRegistersAsync(ushort start, ushort[] values, CancellationToken ct = default)
        {
            ct.ThrowIfCancellationRequested();
            EnsureConnected();
            _master!.WriteMultipleRegisters(_unitId, start, values);
            return Task.CompletedTask;
        }

        private void EnsureConnected()
        {
            if (_master is null || _tcp is null || !_tcp.Connected)
                throw new InvalidOperationException("Modbus is not connected.");
        }

        public void Dispose() => _tcp?.Close();
    }
}
