using System.Diagnostics;
using System.Runtime.InteropServices;
using Microsoft.VisualBasic.Devices;

namespace UI
{
    public partial class UcDashboard : UserControl
    {
        public UcDashboard()
        {
            InitializeComponent();
        }

        public void SetSystemStatus(bool isConnected, string ip, int port, int slaveId)
        {
            lblConnectionValue.Text = isConnected ? "Online" : "Offline";
            lblConnectionValue.ForeColor = isConnected ? Color.Lime : Color.DarkGray;

            lblIpValue.Text = ip;
            lblPortValue.Text = port.ToString();
            lblSlaveValue.Text = slaveId.ToString();
        }

        public void LoadSystemInformation()
        {

            lblOsValue.Text = RuntimeInformation.OSDescription;
            lblArchitectureValue.Text = RuntimeInformation.OSArchitecture.ToString();
            lblRuntimeValue.Text = RuntimeInformation.FrameworkDescription;

            // Total RAM
            var computerInfo = new ComputerInfo();
            double totalRamGB = Math.Round(computerInfo.TotalPhysicalMemory / 1024d / 1024d / 1024d, 1);
            lblTotalRam.Text = $"{totalRamGB} GB";


            lblMachineName.Text = Environment.MachineName;

            // Initial app RAM
            UpdateAppRam();
        }

        public void UpdateAppRam()
        {
            var proc = Process.GetCurrentProcess();
            double mb = proc.WorkingSet64 / 1024d / 1024d;
            lblAppRam.Text = $"{mb:F1} MB";
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            linkGithub.LinkVisited = true;
            Process.Start(new ProcessStartInfo
            {
                FileName = "https://github.com/safirKebieh/KardexDemo",
                UseShellExecute = true
            });
        }

        private void LinkCompany_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            linkCompany.LinkVisited = true;
            System.Diagnostics.Process.Start(new ProcessStartInfo
            {
                FileName = "https://www.kardex.com/de-de/", 
                UseShellExecute = true
            });
        }
    }
}
