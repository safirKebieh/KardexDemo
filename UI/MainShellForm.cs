using Application.Ports;
using Microsoft.Extensions.DependencyInjection;

namespace UI
{
    public partial class MainShellForm : Form
    {
        private readonly IServiceProvider _sp;
        private readonly IModbusService _modbus;

        public MainShellForm(IServiceProvider sp, IModbusService modbus)
        {
            _sp = sp;
            _modbus = modbus;
            InitializeComponent();
        }

        private void LoadContent(UserControl uc)
        {
            foreach (Control c in panelContent.Controls)
                c.Dispose();                    

            panelContent.Controls.Clear();
            uc.Dock = DockStyle.Fill;
            panelContent.Controls.Add(uc);

            panelContent.ResumeLayout();
        }

        private void btnStore_Click(object sender, EventArgs e)
        {
            Navigate<UcStorageProcess>(btnStore);
        }

        private void btnConfig_Click(object sender, EventArgs e)
        {
            Navigate<UcManualConfig>(btnConfig);
        }

        private void btnRetrieve_Click(object sender, EventArgs e)
        {
            Navigate<UcRetrievePallet>(btnRetrieve);
        }

        private void btnSignOut_Click(object sender, EventArgs e)
        {
            this.Close(); // AuthForm will Show() itself via its handler
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
                    panel2.BackColor = UiTheme.Disconnected;

                }
                else
                {
                    await _modbus.ConnectAsync(ModbusConnectionOptions.IpAddress , ModbusConnectionOptions.TcpPort, ModbusConnectionOptions.SlaveAddress);
                    panel2.BackColor = UiTheme.Connected;
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private void MainShellForm_Load(object sender, EventArgs e)
        {
            Navigate<UcStorageProcess>(btnStore);
        }

        private void SetActive(Button activeBtn)
        {
            btnStore.BackColor = UiTheme.ButtonNormal;
            btnConfig.BackColor = UiTheme.ButtonNormal;
            btnRetrieve.BackColor = UiTheme.ButtonNormal;

            activeBtn.BackColor = UiTheme.ButtonActive;
        }

        private void Navigate<T>(Button btn) where T : UserControl
        {
            LoadContent(_sp.GetRequiredService<T>());
            SetActive(btn);
        }
    }
}
