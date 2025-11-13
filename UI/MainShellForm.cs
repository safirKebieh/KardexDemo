using Application.Ports;
using Microsoft.Extensions.DependencyInjection;
using Sunny.UI;

namespace UI
{
    public partial class MainShellForm : Form
    {
        private readonly IServiceProvider _sp;
        private readonly IModbusService _modbus;
        private readonly IClock _clock;


        public MainShellForm(IServiceProvider sp, IModbusService modbus, IClock clock)
        {
            _sp = sp;
            _modbus = modbus;
            _clock = clock;

            InitializeComponent();

            lblDateTime.Text = _clock.UtcNow.ToString("dd.MM.yyyy  HH:mm:ss");

        }
        private void btnDashboard_Click(object sender, EventArgs e)
        {
            if (btnDashboard.BackColor == UiTheme.ButtonActive) return;
            Navigate<UcDashboard>(btnDashboard);
        }
        private void btnWarehouseOperations_Click(object sender, EventArgs e)
        {
            if (btnWarehouseOperations.BackColor == UiTheme.ButtonActive) return;
            Navigate<UcWarehouseOp>(btnWarehouseOperations);
        }
        private void btnManualControl_Click(object sender, EventArgs e)
        {
            if (btnManualControl.BackColor == UiTheme.ButtonActive) return;
            Navigate<UcManualControl>(btnManualControl);
        }

        private void btnSignOut_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void panelHeader_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                UI.DllImport.DragMove(this);
        }

        private async void btnConnect_Click(object sender, EventArgs e)
        {
            try
            {
                if (await _modbus.IsConnectedAsync())
                {
                    await _modbus.DisconnectAsync();
                    ledOnline.Color = UiTheme.Disconnected;
                    uiLedLabelOnline.Text = "Modbus Offline";
                }
                else
                {
                    await _modbus.ConnectAsync(ModbusConnectionOptions.IpAddress, ModbusConnectionOptions.TcpPort, ModbusConnectionOptions.SlaveAddress);
                    ledOnline.Color = UiTheme.Connected;
                    uiLedLabelOnline.Text = "Modbus Online";
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show($"Error: {ex.Message}");
            }
        }
        private void Navigate<T>(Button btn) where T : UserControl
        {
            LoadContent(_sp.GetRequiredService<T>());
            SetActive(btn);
        }
        private void LoadContent(UserControl uc)
        {
            foreach (Control c in panelContent.Controls)
                c.Dispose();

            panelContent.Controls.Clear();
            uc.Dock = DockStyle.Fill;
            panelContent.Controls.Add(uc);
        }

        private void SetActive(Button activeBtn)
        {
            btnDashboard.Enabled = true;
            btnWarehouseOperations.Enabled = true;
            btnManualControl.Enabled = true;

            btnDashboard.BackColor = UiTheme.ButtonNormal;
            btnWarehouseOperations.BackColor = UiTheme.ButtonNormal;
            btnManualControl.BackColor = UiTheme.ButtonNormal;

            activeBtn.BackColor = UiTheme.ButtonActive;
        }

        private void uiTimerClock_Tick(object sender, EventArgs e)
        {
            lblDateTime.Text = _clock.UtcNow.ToString("dd.MM.yyyy  HH:mm:ss");
        }
    }
}
