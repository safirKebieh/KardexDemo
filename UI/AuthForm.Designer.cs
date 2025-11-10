namespace UI
{
    partial class AuthForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            panelHeader = new Panel();
            lblTitle = new Label();
            panelCard = new Panel();
            lblError = new Label();
            btnCancel = new Button();
            btnLogin = new Button();
            lblPass = new Label();
            txtPass = new TextBox();
            txtUser = new TextBox();
            lblUser = new Label();
            panelHeader.SuspendLayout();
            panelCard.SuspendLayout();
            SuspendLayout();
            // 
            // panelHeader
            // 
            panelHeader.BackColor = Color.FromArgb(47, 58, 75);
            panelHeader.Controls.Add(lblTitle);
            panelHeader.Dock = DockStyle.Top;
            panelHeader.Location = new Point(0, 0);
            panelHeader.Name = "panelHeader";
            panelHeader.Size = new Size(464, 52);
            panelHeader.TabIndex = 0;
            panelHeader.MouseDown += panelHeader_MouseDown;
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            lblTitle.ForeColor = Color.White;
            lblTitle.Location = new Point(24, 10);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(87, 32);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "LOGIN";
            // 
            // panelCard
            // 
            panelCard.BackColor = Color.White;
            panelCard.BorderStyle = BorderStyle.FixedSingle;
            panelCard.Controls.Add(lblError);
            panelCard.Controls.Add(btnCancel);
            panelCard.Controls.Add(btnLogin);
            panelCard.Controls.Add(lblPass);
            panelCard.Controls.Add(txtPass);
            panelCard.Controls.Add(txtUser);
            panelCard.Controls.Add(lblUser);
            panelCard.Location = new Point(40, 80);
            panelCard.Name = "panelCard";
            panelCard.Padding = new Padding(20);
            panelCard.Size = new Size(400, 242);
            panelCard.TabIndex = 1;
            // 
            // lblError
            // 
            lblError.AutoSize = true;
            lblError.Font = new Font("Segoe UI", 9F, FontStyle.Italic);
            lblError.ForeColor = Color.FromArgb(220, 38, 38);
            lblError.Location = new Point(103, 215);
            lblError.Name = "lblError";
            lblError.Size = new Size(164, 15);
            lblError.TabIndex = 4;
            lblError.Text = "Invalid username or password";
            lblError.Visible = false;
            // 
            // btnCancel
            // 
            btnCancel.BackColor = Color.FromArgb(15, 118, 110);
            btnCancel.FlatAppearance.BorderSize = 0;
            btnCancel.FlatAppearance.MouseOverBackColor = Color.FromArgb(11, 94, 88);
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.Font = new Font("Segoe UI Semibold", 11F);
            btnCancel.ForeColor = Color.White;
            btnCancel.Location = new Point(200, 174);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(160, 38);
            btnCancel.TabIndex = 4;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = false;
            btnCancel.Click += btnCancel_Click;
            // 
            // btnLogin
            // 
            btnLogin.BackColor = Color.FromArgb(15, 118, 110);
            btnLogin.FlatAppearance.BorderSize = 0;
            btnLogin.FlatAppearance.MouseOverBackColor = Color.FromArgb(11, 94, 88);
            btnLogin.FlatStyle = FlatStyle.Flat;
            btnLogin.Font = new Font("Segoe UI Semibold", 11F);
            btnLogin.ForeColor = Color.White;
            btnLogin.Location = new Point(20, 174);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(160, 38);
            btnLogin.TabIndex = 3;
            btnLogin.Text = "Sign In";
            btnLogin.UseVisualStyleBackColor = false;
            btnLogin.Click += btnLogin_Click;
            // 
            // lblPass
            // 
            lblPass.AutoSize = true;
            lblPass.ForeColor = Color.FromArgb(31, 41, 55);
            lblPass.Location = new Point(20, 95);
            lblPass.Name = "lblPass";
            lblPass.Size = new Size(67, 19);
            lblPass.TabIndex = 2;
            lblPass.Text = "Password";
            // 
            // txtPass
            // 
            txtPass.BackColor = Color.FromArgb(233, 238, 242);
            txtPass.BorderStyle = BorderStyle.FixedSingle;
            txtPass.ForeColor = Color.FromArgb(31, 41, 55);
            txtPass.Location = new Point(20, 120);
            txtPass.Name = "txtPass";
            txtPass.Size = new Size(340, 25);
            txtPass.TabIndex = 2;
            txtPass.UseSystemPasswordChar = true;
            // 
            // txtUser
            // 
            txtUser.BackColor = Color.FromArgb(233, 238, 242);
            txtUser.BorderStyle = BorderStyle.FixedSingle;
            txtUser.ForeColor = Color.FromArgb(31, 41, 55);
            txtUser.Location = new Point(20, 45);
            txtUser.Name = "txtUser";
            txtUser.Size = new Size(340, 25);
            txtUser.TabIndex = 1;
            // 
            // lblUser
            // 
            lblUser.AutoSize = true;
            lblUser.ForeColor = Color.FromArgb(31, 41, 55);
            lblUser.Location = new Point(20, 20);
            lblUser.Name = "lblUser";
            lblUser.Size = new Size(77, 19);
            lblUser.TabIndex = 0;
            lblUser.Text = "User Name";
            // 
            // AuthForm
            // 
            AcceptButton = btnLogin;
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(242, 245, 247);
            CancelButton = btnCancel;
            ClientSize = new Size(464, 334);
            Controls.Add(panelCard);
            Controls.Add(panelHeader);
            Font = new Font("Segoe UI", 10F);
            FormBorderStyle = FormBorderStyle.None;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "AuthForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Login";
            panelHeader.ResumeLayout(false);
            panelHeader.PerformLayout();
            panelCard.ResumeLayout(false);
            panelCard.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel panelHeader;
        private Label lblTitle;
        private Panel panelCard;
        private Label lblPass;
        private TextBox txtUser;
        private Label lblUser;
        private Button btnCancel;
        private Button btnLogin;
        private TextBox txtPass;
        private Label lblError;
    }
}