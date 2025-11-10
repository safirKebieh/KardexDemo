using Application.Ports;

namespace UI
{
    public partial class Form1 : Form
    {
        private readonly IModbusService _modbus;
        private readonly IClock _clock;
        private readonly System.Windows.Forms.Timer _pollTimer = new() { Interval = 50 };

        public Form1(IModbusService modbus, IClock clock)
        {
            _modbus = modbus;
            _clock = clock;
            InitializeComponent();

            _pollTimer.Tick += async (_, __) =>
            {
                try
                {
                    var di = await _modbus.ReadDiscreteInputsAsync(0, 1);
                    lblSensor.Text = "Sensor: " + (di[0] ? "ON" : "OFF") +
                                     $"  ({_clock.UtcNow:HH:mm:ss})";
                }
                catch
                {
                    // ignore short communication errors
                }
            };
        }

        private async void btnConnect_Click(object sender, EventArgs e)
        {
            try
            {
                await _modbus.ConnectAsync("127.0.0.1", 502, 1);
                _pollTimer.Start();
                MessageBox.Show("Connected");
            }
            catch (Exception ex)
            {

                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private async void btnOn_Click(object sender, EventArgs e)
        {
            try
            {
                await _modbus.WriteCoilAsync(0, true);
            }
            catch { }
        }

        private async void btnOff_Click(object sender, EventArgs e)
        {
            try
            {
                await _modbus.WriteCoilAsync(0, false);
            }
            catch { }
        }

        private async void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            await _modbus.DisconnectAsync();
        }

        private async void btnDisconnect_Click(object sender, EventArgs e)
        {
            _pollTimer.Stop();
            await _modbus.DisconnectAsync();
            MessageBox.Show("Disconnected");
        }
    }
}

