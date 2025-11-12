using Microsoft.Extensions.DependencyInjection;

namespace UI
{
    public partial class AuthForm : Form
    {
        private readonly IServiceProvider _sp;
        public AuthForm(IServiceProvider sp)
        {
            _sp = sp;
            InitializeComponent();
            DoubleBuffered = true;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUser.Text) || string.IsNullOrWhiteSpace(txtPass.Text))
            {
                lblError.Text = "Please enter username and password.";
                lblError.Visible = true;
                return;
            }

            // Simple hardcoded login check
            var isOk = (txtUser.Text == "admin" && txtPass.Text == "1234");
            if (!isOk)
            {
                lblError.Text = "Invalid username or password.";
                lblError.Visible = true;
                return;
            }

            lblError.Visible = false;

            var main = _sp.GetRequiredService<MainShellForm>();

            main.FormClosed += (_, __) =>
            {
                ClearLogin();
                this.Show();
            };

            this.Hide();
            main.Show();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void panelHeader_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                UI.DllImport.DragMove(this);
        }

        private void ClearLogin()
        {
            txtUser.Clear();
            txtPass.Clear();
            lblError.Visible = false;
            this.ActiveControl = null;
        }
    }
}
