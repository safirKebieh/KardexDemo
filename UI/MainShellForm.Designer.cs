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
            components = new System.ComponentModel.Container();
            panelHeader = new Panel();
            label1 = new Label();
            uiLedLabelOnline = new Sunny.UI.UILedLabel();
            ledOnline = new Sunny.UI.UILedBulb();
            btnConnect = new Button();
            lblTitle = new Label();
            btnSignOut = new Button();
            panelStatus = new Panel();
            lblDateTime = new Label();
            pictureBox1 = new PictureBox();
            panel1 = new Panel();
            btnRetrieve = new Button();
            btnConfig = new Button();
            btnStore = new Button();
            panelContent = new Panel();
            uiTimerClock = new System.Windows.Forms.Timer(components);
            panel2 = new Panel();
            panelHeader.SuspendLayout();
            panelStatus.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // panelHeader
            // 
            panelHeader.BackColor = Color.FromArgb(47, 58, 75);
            panelHeader.BackgroundImageLayout = ImageLayout.None;
            panelHeader.Controls.Add(label1);
            panelHeader.Controls.Add(uiLedLabelOnline);
            panelHeader.Controls.Add(ledOnline);
            panelHeader.Controls.Add(btnConnect);
            panelHeader.Controls.Add(lblTitle);
            panelHeader.Dock = DockStyle.Top;
            panelHeader.Location = new Point(0, 0);
            panelHeader.Name = "panelHeader";
            panelHeader.Size = new Size(1287, 64);
            panelHeader.TabIndex = 0;
            panelHeader.MouseDown += panelHeader_MouseDown;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.ForeColor = Color.White;
            label1.Location = new Point(1171, 42);
            label1.Name = "label1";
            label1.Size = new Size(46, 19);
            label1.TabIndex = 8;
            label1.Text = "v1.0.0";
            // 
            // uiLedLabelOnline
            // 
            uiLedLabelOnline.Font = new Font("Segoe UI Semibold", 12F);
            uiLedLabelOnline.ForeColor = Color.White;
            uiLedLabelOnline.Location = new Point(226, 9);
            uiLedLabelOnline.MinimumSize = new Size(1, 1);
            uiLedLabelOnline.Name = "uiLedLabelOnline";
            uiLedLabelOnline.Size = new Size(273, 35);
            uiLedLabelOnline.TabIndex = 7;
            uiLedLabelOnline.Text = "Modbus Offline";
            // 
            // ledOnline
            // 
            ledOnline.Color = Color.DarkGray;
            ledOnline.Location = new Point(202, 16);
            ledOnline.Name = "ledOnline";
            ledOnline.Size = new Size(18, 18);
            ledOnline.TabIndex = 5;
            ledOnline.Text = "uiLedBulb1";
            // 
            // btnConnect
            // 
            btnConnect.BackColor = Color.FromArgb(15, 118, 110);
            btnConnect.FlatStyle = FlatStyle.Flat;
            btnConnect.ForeColor = Color.White;
            btnConnect.Location = new Point(12, 12);
            btnConnect.Name = "btnConnect";
            btnConnect.Size = new Size(183, 32);
            btnConnect.TabIndex = 3;
            btnConnect.Text = "Connect / Disconnect";
            btnConnect.UseVisualStyleBackColor = false;
            btnConnect.Click += btnConnect_Click;
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI Semibold", 18F);
            lblTitle.ForeColor = Color.White;
            lblTitle.Location = new Point(1113, 12);
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
            panelStatus.Controls.Add(lblDateTime);
            panelStatus.Controls.Add(pictureBox1);
            panelStatus.Controls.Add(btnSignOut);
            panelStatus.Dock = DockStyle.Bottom;
            panelStatus.Location = new Point(0, 639);
            panelStatus.Name = "panelStatus";
            panelStatus.Size = new Size(1287, 51);
            panelStatus.TabIndex = 1;
            // 
            // lblDateTime
            // 
            lblDateTime.AutoSize = true;
            lblDateTime.ForeColor = Color.White;
            lblDateTime.Location = new Point(94, 15);
            lblDateTime.Name = "lblDateTime";
            lblDateTime.Size = new Size(45, 19);
            lblDateTime.TabIndex = 10;
            lblDateTime.Text = "label2";
            // 
            // pictureBox1
            // 
            pictureBox1.Image = Properties.Resources.Logo;
            pictureBox1.Location = new Point(3, -17);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(85, 85);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 9;
            pictureBox1.TabStop = false;
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
            // uiTimerClock
            // 
            uiTimerClock.Enabled = true;
            uiTimerClock.Interval = 1000;
            uiTimerClock.Tick += uiTimerClock_Tick;
            // 
            // panel2
            // 
            panel2.BackColor = Color.FromArgb(47, 58, 75);
            panel2.Dock = DockStyle.Right;
            panel2.Location = new Point(1272, 64);
            panel2.Name = "panel2";
            panel2.Size = new Size(15, 575);
            panel2.TabIndex = 0;
            // 
            // MainShellForm
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(242, 245, 247);
            ClientSize = new Size(1287, 690);
            Controls.Add(panel2);
            Controls.Add(panelContent);
            Controls.Add(panel1);
            Controls.Add(panelStatus);
            Controls.Add(panelHeader);
            Font = new Font("Segoe UI", 10F);
            FormBorderStyle = FormBorderStyle.None;
            Name = "MainShellForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "MainShellForm";
            panelHeader.ResumeLayout(false);
            panelHeader.PerformLayout();
            panelStatus.ResumeLayout(false);
            panelStatus.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            panel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel panelHeader;
        private Label lblTitle;
        private Button btnConnect;
        private Button btnSignOut;
        private Panel panelStatus;
        private Panel panel1;
        private Button btnRetrieve;
        private Button btnConfig;
        private Button btnStore;
        private Panel panelContent;
        private Sunny.UI.UILedBulb ledOnline;
        private Sunny.UI.UILedLabel uiLedLabelOnline;
        private Label label1;
        private PictureBox pictureBox1;
        private System.Windows.Forms.Timer uiTimerClock;
        private Label lblDateTime;
        private Panel panel2;
    }
}