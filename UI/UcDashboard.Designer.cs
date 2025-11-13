namespace UI
{
    partial class UcDashboard
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            tableLayoutPanel1 = new TableLayoutPanel();
            groupBox1 = new GroupBox();
            tableLayoutPanel2 = new TableLayoutPanel();
            label4 = new Label();
            lblSlaveValue = new Label();
            lblPortValue = new Label();
            label3 = new Label();
            label2 = new Label();
            lblIpValue = new Label();
            label1 = new Label();
            lblConnectionValue = new Label();
            label12 = new Label();
            lblMachineName = new Label();
            groupBox3 = new GroupBox();
            tableLayoutPanel3 = new TableLayoutPanel();
            label8 = new Label();
            lblTotalRam = new Label();
            lblRuntimeValue = new Label();
            lblArchitectureValue = new Label();
            lblOsValue = new Label();
            label7 = new Label();
            label6 = new Label();
            label5 = new Label();
            label9 = new Label();
            lblAppRam = new Label();
            label10 = new Label();
            label11 = new Label();
            linkGithub = new LinkLabel();
            linkCompany = new LinkLabel();
            tableLayoutPanel1.SuspendLayout();
            groupBox1.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            groupBox3.SuspendLayout();
            tableLayoutPanel3.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Controls.Add(groupBox1, 0, 4);
            tableLayoutPanel1.Controls.Add(groupBox3, 1, 4);
            tableLayoutPanel1.Controls.Add(label10, 0, 1);
            tableLayoutPanel1.Controls.Add(label11, 0, 0);
            tableLayoutPanel1.Controls.Add(linkGithub, 0, 2);
            tableLayoutPanel1.Controls.Add(linkCompany, 0, 3);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 5;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 36.5853577F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 36.58537F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 13.4146357F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 13.4146357F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.Size = new Size(866, 445);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(tableLayoutPanel2);
            groupBox1.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            groupBox1.ForeColor = Color.FromArgb(150, 150, 150);
            groupBox1.Location = new Point(3, 241);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(427, 199);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "System Status";
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 3;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            tableLayoutPanel2.Controls.Add(label4, 0, 4);
            tableLayoutPanel2.Controls.Add(lblSlaveValue, 1, 4);
            tableLayoutPanel2.Controls.Add(lblPortValue, 1, 3);
            tableLayoutPanel2.Controls.Add(label3, 0, 3);
            tableLayoutPanel2.Controls.Add(label2, 0, 2);
            tableLayoutPanel2.Controls.Add(lblIpValue, 1, 2);
            tableLayoutPanel2.Controls.Add(label1, 0, 1);
            tableLayoutPanel2.Controls.Add(lblConnectionValue, 1, 1);
            tableLayoutPanel2.Controls.Add(label12, 0, 5);
            tableLayoutPanel2.Controls.Add(lblMachineName, 1, 5);
            tableLayoutPanel2.Dock = DockStyle.Fill;
            tableLayoutPanel2.Location = new Point(3, 25);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 6;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 16.666666F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 16.666666F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 16.666666F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 16.666666F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 16.666666F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 16.666666F));
            tableLayoutPanel2.Size = new Size(421, 171);
            tableLayoutPanel2.TabIndex = 0;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.ForeColor = Color.FromArgb(80, 80, 80);
            label4.Location = new Point(3, 112);
            label4.Name = "label4";
            label4.Size = new Size(72, 21);
            label4.TabIndex = 3;
            label4.Text = "Slave ID";
            label4.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblSlaveValue
            // 
            lblSlaveValue.AutoSize = true;
            lblSlaveValue.Font = new Font("Segoe UI", 12F);
            lblSlaveValue.ForeColor = Color.FromArgb(80, 80, 80);
            lblSlaveValue.Location = new Point(163, 112);
            lblSlaveValue.Name = "lblSlaveValue";
            lblSlaveValue.Size = new Size(22, 21);
            lblSlaveValue.TabIndex = 7;
            lblSlaveValue.Text = "--";
            // 
            // lblPortValue
            // 
            lblPortValue.AutoSize = true;
            lblPortValue.Font = new Font("Segoe UI", 12F);
            lblPortValue.ForeColor = Color.FromArgb(80, 80, 80);
            lblPortValue.Location = new Point(163, 84);
            lblPortValue.Name = "lblPortValue";
            lblPortValue.Size = new Size(22, 21);
            lblPortValue.TabIndex = 6;
            lblPortValue.Text = "--";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.ForeColor = Color.FromArgb(80, 80, 80);
            label3.Location = new Point(3, 84);
            label3.Name = "label3";
            label3.Size = new Size(42, 21);
            label3.TabIndex = 2;
            label3.Text = "Port";
            label3.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.ForeColor = Color.FromArgb(80, 80, 80);
            label2.Location = new Point(3, 56);
            label2.Name = "label2";
            label2.Size = new Size(89, 21);
            label2.TabIndex = 1;
            label2.Text = "IP Address";
            label2.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblIpValue
            // 
            lblIpValue.AutoSize = true;
            lblIpValue.Font = new Font("Segoe UI", 12F);
            lblIpValue.ForeColor = Color.FromArgb(80, 80, 80);
            lblIpValue.Location = new Point(163, 56);
            lblIpValue.Name = "lblIpValue";
            lblIpValue.Size = new Size(22, 21);
            lblIpValue.TabIndex = 5;
            lblIpValue.Text = "--";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.ForeColor = Color.FromArgb(80, 80, 80);
            label1.Location = new Point(3, 28);
            label1.Name = "label1";
            label1.Size = new Size(98, 21);
            label1.TabIndex = 0;
            label1.Text = "Connection";
            label1.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblConnectionValue
            // 
            lblConnectionValue.AutoSize = true;
            lblConnectionValue.Font = new Font("Segoe UI", 12F);
            lblConnectionValue.Location = new Point(163, 28);
            lblConnectionValue.Name = "lblConnectionValue";
            lblConnectionValue.Size = new Size(57, 21);
            lblConnectionValue.TabIndex = 4;
            lblConnectionValue.Text = "Offline";
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.ForeColor = Color.FromArgb(80, 80, 80);
            label12.Location = new Point(3, 140);
            label12.Name = "label12";
            label12.Size = new Size(126, 21);
            label12.TabIndex = 0;
            label12.Text = "Machine Name";
            label12.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblMachineName
            // 
            lblMachineName.AutoSize = true;
            lblMachineName.Font = new Font("Segoe UI", 12F);
            lblMachineName.ForeColor = Color.FromArgb(80, 80, 80);
            lblMachineName.Location = new Point(163, 140);
            lblMachineName.Name = "lblMachineName";
            lblMachineName.Size = new Size(22, 21);
            lblMachineName.TabIndex = 0;
            lblMachineName.Text = "--";
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(tableLayoutPanel3);
            groupBox3.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            groupBox3.ForeColor = Color.FromArgb(150, 150, 150);
            groupBox3.Location = new Point(436, 241);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(427, 199);
            groupBox3.TabIndex = 2;
            groupBox3.TabStop = false;
            groupBox3.Text = "System Information";
            // 
            // tableLayoutPanel3
            // 
            tableLayoutPanel3.ColumnCount = 2;
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F));
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60F));
            tableLayoutPanel3.Controls.Add(label8, 0, 4);
            tableLayoutPanel3.Controls.Add(lblTotalRam, 1, 4);
            tableLayoutPanel3.Controls.Add(lblRuntimeValue, 1, 3);
            tableLayoutPanel3.Controls.Add(lblArchitectureValue, 1, 2);
            tableLayoutPanel3.Controls.Add(lblOsValue, 1, 1);
            tableLayoutPanel3.Controls.Add(label7, 0, 3);
            tableLayoutPanel3.Controls.Add(label6, 0, 2);
            tableLayoutPanel3.Controls.Add(label5, 0, 1);
            tableLayoutPanel3.Controls.Add(label9, 0, 5);
            tableLayoutPanel3.Controls.Add(lblAppRam, 1, 5);
            tableLayoutPanel3.Dock = DockStyle.Fill;
            tableLayoutPanel3.Location = new Point(3, 25);
            tableLayoutPanel3.Name = "tableLayoutPanel3";
            tableLayoutPanel3.RowCount = 6;
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 11.9318171F));
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 11.9318171F));
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 11.9318171F));
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 11.9318171F));
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 11.9318171F));
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 11.9318171F));
            tableLayoutPanel3.Size = new Size(421, 171);
            tableLayoutPanel3.TabIndex = 0;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.ForeColor = Color.FromArgb(80, 80, 80);
            label8.Location = new Point(3, 112);
            label8.Name = "label8";
            label8.Size = new Size(88, 21);
            label8.TabIndex = 0;
            label8.Text = "Total RAM";
            label8.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblTotalRam
            // 
            lblTotalRam.AutoSize = true;
            lblTotalRam.Font = new Font("Segoe UI", 12F);
            lblTotalRam.ForeColor = Color.FromArgb(80, 80, 80);
            lblTotalRam.Location = new Point(171, 112);
            lblTotalRam.Name = "lblTotalRam";
            lblTotalRam.Size = new Size(22, 21);
            lblTotalRam.TabIndex = 0;
            lblTotalRam.Text = "--";
            // 
            // lblRuntimeValue
            // 
            lblRuntimeValue.AutoSize = true;
            lblRuntimeValue.Font = new Font("Segoe UI", 12F);
            lblRuntimeValue.ForeColor = Color.FromArgb(80, 80, 80);
            lblRuntimeValue.Location = new Point(171, 84);
            lblRuntimeValue.Name = "lblRuntimeValue";
            lblRuntimeValue.Size = new Size(22, 21);
            lblRuntimeValue.TabIndex = 0;
            lblRuntimeValue.Text = "--";
            // 
            // lblArchitectureValue
            // 
            lblArchitectureValue.AutoSize = true;
            lblArchitectureValue.Font = new Font("Segoe UI", 12F);
            lblArchitectureValue.ForeColor = Color.FromArgb(80, 80, 80);
            lblArchitectureValue.Location = new Point(171, 56);
            lblArchitectureValue.Name = "lblArchitectureValue";
            lblArchitectureValue.Size = new Size(22, 21);
            lblArchitectureValue.TabIndex = 0;
            lblArchitectureValue.Text = "--";
            // 
            // lblOsValue
            // 
            lblOsValue.AutoSize = true;
            lblOsValue.Font = new Font("Segoe UI", 12F);
            lblOsValue.ForeColor = Color.FromArgb(80, 80, 80);
            lblOsValue.Location = new Point(171, 28);
            lblOsValue.Name = "lblOsValue";
            lblOsValue.Size = new Size(22, 21);
            lblOsValue.TabIndex = 0;
            lblOsValue.Text = "--";
            lblOsValue.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.ForeColor = Color.FromArgb(80, 80, 80);
            label7.Location = new Point(3, 84);
            label7.Name = "label7";
            label7.Size = new Size(122, 21);
            label7.TabIndex = 0;
            label7.Text = ".NET RUNTIME";
            label7.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.ForeColor = Color.FromArgb(80, 80, 80);
            label6.Location = new Point(3, 56);
            label6.Name = "label6";
            label6.Size = new Size(104, 21);
            label6.TabIndex = 0;
            label6.Text = "Architecture";
            label6.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.ForeColor = Color.FromArgb(80, 80, 80);
            label5.Location = new Point(3, 28);
            label5.Name = "label5";
            label5.Size = new Size(31, 21);
            label5.TabIndex = 0;
            label5.Text = "OS";
            label5.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.ForeColor = Color.FromArgb(80, 80, 80);
            label9.Location = new Point(3, 140);
            label9.Name = "label9";
            label9.Size = new Size(81, 21);
            label9.TabIndex = 0;
            label9.Text = "App RAM";
            label9.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblAppRam
            // 
            lblAppRam.AutoSize = true;
            lblAppRam.Font = new Font("Segoe UI", 12F);
            lblAppRam.ForeColor = Color.FromArgb(80, 80, 80);
            lblAppRam.Location = new Point(171, 140);
            lblAppRam.Name = "lblAppRam";
            lblAppRam.Size = new Size(22, 21);
            lblAppRam.TabIndex = 0;
            lblAppRam.Text = "--";
            // 
            // label10
            // 
            label10.AutoSize = true;
            tableLayoutPanel1.SetColumnSpan(label10, 2);
            label10.Dock = DockStyle.Top;
            label10.Font = new Font("Segoe UI", 10F);
            label10.Location = new Point(3, 87);
            label10.Name = "label10";
            label10.Size = new Size(860, 38);
            label10.TabIndex = 3;
            label10.Text = "This application demonstrates clean-code principles using a lightweight warehouse simulation with seamless \r\nModbus TCP integration.";
            label10.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label11
            // 
            label11.AutoSize = true;
            tableLayoutPanel1.SetColumnSpan(label11, 2);
            label11.Dock = DockStyle.Top;
            label11.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            label11.Location = new Point(3, 0);
            label11.Name = "label11";
            label11.Padding = new Padding(0, 20, 0, 0);
            label11.Size = new Size(860, 45);
            label11.TabIndex = 3;
            label11.Text = "Welcome to the Kardex Demo Application";
            label11.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // linkGithub
            // 
            linkGithub.AutoSize = true;
            tableLayoutPanel1.SetColumnSpan(linkGithub, 2);
            linkGithub.Cursor = Cursors.Hand;
            linkGithub.Dock = DockStyle.Top;
            linkGithub.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            linkGithub.LinkColor = Color.SteelBlue;
            linkGithub.Location = new Point(3, 174);
            linkGithub.Name = "linkGithub";
            linkGithub.Size = new Size(860, 19);
            linkGithub.TabIndex = 4;
            linkGithub.TabStop = true;
            linkGithub.Text = "Source Code (GitHub)";
            linkGithub.TextAlign = ContentAlignment.MiddleCenter;
            linkGithub.LinkClicked += linkLabel1_LinkClicked;
            // 
            // linkCompany
            // 
            linkCompany.AutoSize = true;
            tableLayoutPanel1.SetColumnSpan(linkCompany, 2);
            linkCompany.Cursor = Cursors.Hand;
            linkCompany.Dock = DockStyle.Top;
            linkCompany.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            linkCompany.LinkColor = Color.SteelBlue;
            linkCompany.Location = new Point(3, 206);
            linkCompany.Name = "linkCompany";
            linkCompany.Size = new Size(860, 19);
            linkCompany.TabIndex = 5;
            linkCompany.TabStop = true;
            linkCompany.Text = "Company Website";
            linkCompany.TextAlign = ContentAlignment.MiddleCenter;
            linkCompany.LinkClicked += LinkCompany_LinkClicked;
            // 
            // UcDashboard
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(tableLayoutPanel1);
            Name = "UcDashboard";
            Size = new Size(866, 445);
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            groupBox1.ResumeLayout(false);
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel2.PerformLayout();
            groupBox3.ResumeLayout(false);
            tableLayoutPanel3.ResumeLayout(false);
            tableLayoutPanel3.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private GroupBox groupBox1;
        private GroupBox groupBox3;
        private TableLayoutPanel tableLayoutPanel2;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label lblConnectionValue;
        private Label lblIpValue;
        private Label lblPortValue;
        private Label lblSlaveValue;
        private Label label1;
        private TableLayoutPanel tableLayoutPanel3;
        private Label label5;
        private Label label6;
        private Label label7;
        private Label label8;
        private Label lblTotalRam;
        private Label lblRuntimeValue;
        private Label lblArchitectureValue;
        private Label lblOsValue;
        private Label label9;
        private Label lblAppRam;
        private Label lblMachineName;
        private Label label12;
        private Label label10;
        private Label label11;
        private LinkLabel linkGithub;
        private LinkLabel linkCompany;
    }
}
