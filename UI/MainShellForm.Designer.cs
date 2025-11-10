namespace UI
{
    partial class MainShellForm
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
            panel2 = new Panel();
            btnConnect = new Button();
            lblUser = new Label();
            lblTitle = new Label();
            btnSignOut = new Button();
            panelStatus = new Panel();
            lblClock = new Label();
            lblMsg = new Label();
            lblMode = new Label();
            panel1 = new Panel();
            btnRetrieve = new Button();
            btnConfig = new Button();
            btnStore = new Button();
            panelContent = new Panel();
            panelHeader.SuspendLayout();
            panelStatus.SuspendLayout();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // panelHeader
            // 
            panelHeader.BackColor = Color.FromArgb(47, 58, 75);
            panelHeader.BackgroundImageLayout = ImageLayout.None;
            panelHeader.Controls.Add(panel2);
            panelHeader.Controls.Add(btnConnect);
            panelHeader.Controls.Add(lblUser);
            panelHeader.Controls.Add(lblTitle);
            panelHeader.Dock = DockStyle.Top;
            panelHeader.Location = new Point(0, 0);
            panelHeader.Name = "panelHeader";
            panelHeader.Size = new Size(1287, 64);
            panelHeader.TabIndex = 0;
            panelHeader.MouseDown += panelHeader_MouseDown;
            // 
            // panel2
            // 
            panel2.BackColor = Color.DarkGray;
            panel2.Location = new Point(1145, 28);
            panel2.Name = "panel2";
            panel2.Size = new Size(14, 14);
            panel2.TabIndex = 4;
            // 
            // btnConnect
            // 
            btnConnect.BackColor = Color.FromArgb(15, 118, 110);
            btnConnect.FlatStyle = FlatStyle.Flat;
            btnConnect.ForeColor = Color.White;
            btnConnect.Location = new Point(1165, 16);
            btnConnect.Name = "btnConnect";
            btnConnect.Size = new Size(110, 32);
            btnConnect.TabIndex = 3;
            btnConnect.Text = "Connect";
            btnConnect.UseVisualStyleBackColor = false;
            btnConnect.Click += btnConnect_Click;
            // 
            // lblUser
            // 
            lblUser.AutoSize = true;
            lblUser.ForeColor = Color.White;
            lblUser.Location = new Point(340, 23);
            lblUser.Name = "lblUser";
            lblUser.Size = new Size(80, 19);
            lblUser.TabIndex = 1;
            lblUser.Text = "label1.USER";
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI Semibold", 18F);
            lblTitle.ForeColor = Color.White;
            lblTitle.Location = new Point(20, 16);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(162, 32);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Kardex Demo";
            // 
            // btnSignOut
            // 
            btnSignOut.BackColor = Color.FromArgb(15, 118, 110);
            btnSignOut.FlatStyle = FlatStyle.Flat;
            btnSignOut.ForeColor = Color.White;
            btnSignOut.Location = new Point(1165, 7);
            btnSignOut.Name = "btnSignOut";
            btnSignOut.Size = new Size(110, 32);
            btnSignOut.TabIndex = 3;
            btnSignOut.Text = "Sign out";
            btnSignOut.UseVisualStyleBackColor = false;
            btnSignOut.Click += btnSignOut_Click;
            // 
            // panelStatus
            // 
            panelStatus.BackColor = Color.FromArgb(47, 58, 75);
            panelStatus.Controls.Add(btnSignOut);
            panelStatus.Controls.Add(lblClock);
            panelStatus.Controls.Add(lblMsg);
            panelStatus.Controls.Add(lblMode);
            panelStatus.Dock = DockStyle.Bottom;
            panelStatus.Location = new Point(0, 639);
            panelStatus.Name = "panelStatus";
            panelStatus.Size = new Size(1287, 51);
            panelStatus.TabIndex = 1;
            // 
            // lblClock
            // 
            lblClock.AutoSize = true;
            lblClock.Font = new Font("Segoe UI Semibold", 10F);
            lblClock.ForeColor = Color.White;
            lblClock.Location = new Point(410, 12);
            lblClock.Name = "lblClock";
            lblClock.Size = new Size(40, 19);
            lblClock.TabIndex = 0;
            lblClock.Text = "Time";
            // 
            // lblMsg
            // 
            lblMsg.AutoSize = true;
            lblMsg.Font = new Font("Segoe UI Semibold", 10F);
            lblMsg.ForeColor = Color.White;
            lblMsg.Location = new Point(202, 12);
            lblMsg.Name = "lblMsg";
            lblMsg.Size = new Size(86, 19);
            lblMsg.TabIndex = 0;
            lblMsg.Text = "MSG.READY";
            // 
            // lblMode
            // 
            lblMode.AutoSize = true;
            lblMode.Font = new Font("Segoe UI Semibold", 10F);
            lblMode.ForeColor = Color.White;
            lblMode.Location = new Point(20, 12);
            lblMode.Name = "lblMode";
            lblMode.Size = new Size(75, 19);
            lblMode.TabIndex = 0;
            lblMode.Text = "Mode: Idle";
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(47, 58, 75);
            panel1.Controls.Add(btnRetrieve);
            panel1.Controls.Add(btnConfig);
            panel1.Controls.Add(btnStore);
            panel1.Dock = DockStyle.Left;
            panel1.Location = new Point(0, 64);
            panel1.Name = "panel1";
            panel1.Size = new Size(180, 575);
            panel1.TabIndex = 2;
            // 
            // btnRetrieve
            // 
            btnRetrieve.BackColor = Color.FromArgb(15, 118, 110);
            btnRetrieve.Cursor = Cursors.Hand;
            btnRetrieve.Dock = DockStyle.Top;
            btnRetrieve.FlatAppearance.MouseOverBackColor = Color.FromArgb(20, 150, 140);
            btnRetrieve.FlatStyle = FlatStyle.Flat;
            btnRetrieve.Font = new Font("Segoe UI Semibold", 12F);
            btnRetrieve.ForeColor = Color.White;
            btnRetrieve.Location = new Point(0, 360);
            btnRetrieve.Name = "btnRetrieve";
            btnRetrieve.Size = new Size(180, 180);
            btnRetrieve.TabIndex = 2;
            btnRetrieve.Text = "Retrieve";
            btnRetrieve.UseVisualStyleBackColor = false;
            btnRetrieve.Click += btnRetrieve_Click;
            // 
            // btnConfig
            // 
            btnConfig.BackColor = Color.FromArgb(15, 118, 110);
            btnConfig.Cursor = Cursors.Hand;
            btnConfig.Dock = DockStyle.Top;
            btnConfig.FlatAppearance.MouseOverBackColor = Color.FromArgb(20, 150, 140);
            btnConfig.FlatStyle = FlatStyle.Flat;
            btnConfig.Font = new Font("Segoe UI Semibold", 12F);
            btnConfig.ForeColor = Color.White;
            btnConfig.Location = new Point(0, 180);
            btnConfig.Name = "btnConfig";
            btnConfig.Size = new Size(180, 180);
            btnConfig.TabIndex = 1;
            btnConfig.Text = "Configuration";
            btnConfig.UseVisualStyleBackColor = false;
            btnConfig.Click += btnConfig_Click;
            // 
            // btnStore
            // 
            btnStore.BackColor = Color.FromArgb(15, 118, 110);
            btnStore.Cursor = Cursors.Hand;
            btnStore.Dock = DockStyle.Top;
            btnStore.FlatAppearance.MouseOverBackColor = Color.FromArgb(20, 150, 140);
            btnStore.FlatStyle = FlatStyle.Flat;
            btnStore.Font = new Font("Segoe UI Semibold", 12F);
            btnStore.ForeColor = Color.White;
            btnStore.ImageAlign = ContentAlignment.TopCenter;
            btnStore.Location = new Point(0, 0);
            btnStore.Name = "btnStore";
            btnStore.Size = new Size(180, 180);
            btnStore.TabIndex = 0;
            btnStore.Text = "Storage Process";
            btnStore.UseVisualStyleBackColor = false;
            btnStore.Click += btnStore_Click;
            // 
            // panelContent
            // 
            panelContent.BackColor = Color.White;
            panelContent.Dock = DockStyle.Fill;
            panelContent.Location = new Point(180, 64);
            panelContent.Name = "panelContent";
            panelContent.Size = new Size(1107, 575);
            panelContent.TabIndex = 3;
            // 
            // MainShellForm
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(242, 245, 247);
            ClientSize = new Size(1287, 690);
            Controls.Add(panelContent);
            Controls.Add(panel1);
            Controls.Add(panelStatus);
            Controls.Add(panelHeader);
            Font = new Font("Segoe UI", 10F);
            FormBorderStyle = FormBorderStyle.None;
            Name = "MainShellForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "MainShellForm";
            Load += MainShellForm_Load;
            panelHeader.ResumeLayout(false);
            panelHeader.PerformLayout();
            panelStatus.ResumeLayout(false);
            panelStatus.PerformLayout();
            panel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel panelHeader;
        private Label lblTitle;
        private Label lblUser;
        private Button btnConnect;
        private Button btnSignOut;
        private Panel panelStatus;
        private Label lblClock;
        private Label lblMsg;
        private Label lblMode;
        private Panel panel1;
        private Button btnRetrieve;
        private Button btnConfig;
        private Button btnStore;
        private Panel panelContent;
        private Panel panel2;
    }
}