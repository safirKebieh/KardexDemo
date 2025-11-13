using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
    }
}
