using Application.Ports;
using Microsoft.Extensions.DependencyInjection;

namespace UI
{
    public partial class MainShellForm : Form
    {
        #region Fields

        private readonly IServiceProvider _sp;
        private readonly IModbusService _modbus;
        private readonly IClock _clock;

        private readonly Dictionary<Type, UserControl> _pages = new();

        #endregion

        #region Constructor

        public MainShellForm(IServiceProvider sp, IModbusService modbus, IClock clock)
        {
            _sp = sp;
            _modbus = modbus;
            _clock = clock;

            InitializeComponent();

            lblDateTime.Text = _clock.UtcNow.ToString("dd.MM.yyyy  HH:mm:ss");
            Navigate<UcDashboard>(btnDashboard);

            var dashboard = GetDashboard();
            dashboard.LoadSystemInformation();
        }

        #endregion

        #region Navigation

        private UcDashboard GetDashboard()
        {
            return GetPage<UcDashboard>();
        }

        private T GetPage<T>() where T : UserControl
        {
            var type = typeof(T);

            if (!_pages.TryGetValue(type, out var uc))
            {
                uc = _sp.GetRequiredService<T>();
                uc.Dock = DockStyle.Fill;
                _pages[type] = uc;
            }

            return (T)uc;
        }

        private void Navigate<T>(Button btn) where T : UserControl
        {
            var uc = GetPage<T>();
            LoadContent(uc);
            SetActive(btn);
        }

        private void LoadContent(UserControl uc)
        {
            panelContent.Controls.Clear();
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

        #endregion

        #region Event Handlers

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

                    var dashboard = GetDashboard();
                    dashboard.SetSystemStatus(false, "--", 0, 0);
                }
                else
                {
                    await _modbus.ConnectAsync(
                        ModbusConnectionOptions.IpAddress,
                        ModbusConnectionOptions.TcpPort,
                        ModbusConnectionOptions.SlaveAddress);

                    ledOnline.Color = UiTheme.Connected;
                    uiLedLabelOnline.Text = "Modbus Online";

                    var dashboard = GetDashboard();
                    dashboard.SetSystemStatus(
                        true,
                        ModbusConnectionOptions.IpAddress,
                        ModbusConnectionOptions.TcpPort,
                        ModbusConnectionOptions.SlaveAddress);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private void uiTimerClock_Tick(object sender, EventArgs e)
        {
            lblDateTime.Text = _clock.UtcNow.ToString("dd.MM.yyyy  HH:mm:ss");
            var dashboard = GetDashboard();
            dashboard.UpdateAppRam();
        }

        #endregion
    }
}
