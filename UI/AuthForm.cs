using Microsoft.Extensions.DependencyInjection;

namespace UI
{

    public partial class AuthForm : Form
    {
        private readonly Func<MainShellForm> _mainFactory;

        public AuthForm(Func<MainShellForm> mainFactory)
        {
            _mainFactory = mainFactory;
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

            // TODO: real auth later
            var isOk = (txtUser.Text == "admin" && txtPass.Text == "1234");
            if (!isOk)
            {
                lblError.Text = "Invalid username or password.";
                lblError.Visible = true;
                return;
            }

            lblError.Visible = false;

            var main = _mainFactory();
            
            main.FormClosed += (_, __) =>
            {
                // when user signs out (MainShellForm.Close), show login again & reset fields
                ClearLogin();
                this.Show();
            };

            this.Hide();
            main.Show();
        }

        private void btnCancel_Click(object sender, EventArgs e) => Close();
        

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
            this.ActiveControl = null;  // no caret in textboxes
        }
    }
}
