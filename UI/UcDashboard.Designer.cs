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
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            lblConnectionValue = new Label();
            lblIpValue = new Label();
            lblPortValue = new Label();
            lblSlaveValue = new Label();
            label1 = new Label();
            groupBox2 = new GroupBox();
            groupBox3 = new GroupBox();
            groupBox4 = new GroupBox();
            tableLayoutPanel1.SuspendLayout();
            groupBox1.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Controls.Add(groupBox1, 0, 0);
            tableLayoutPanel1.Controls.Add(groupBox2, 0, 1);
            tableLayoutPanel1.Controls.Add(groupBox3, 1, 0);
            tableLayoutPanel1.Controls.Add(groupBox4, 1, 1);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Size = new Size(1107, 575);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(tableLayoutPanel2);
            groupBox1.Dock = DockStyle.Fill;
            groupBox1.Font = new Font("Segoe UI", 12F);
            groupBox1.Location = new Point(3, 3);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(547, 281);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "System Status";
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 2;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60F));
            tableLayoutPanel2.Controls.Add(label2, 0, 1);
            tableLayoutPanel2.Controls.Add(label3, 0, 2);
            tableLayoutPanel2.Controls.Add(label4, 0, 3);
            tableLayoutPanel2.Controls.Add(lblConnectionValue, 1, 0);
            tableLayoutPanel2.Controls.Add(lblIpValue, 1, 1);
            tableLayoutPanel2.Controls.Add(lblPortValue, 1, 2);
            tableLayoutPanel2.Controls.Add(lblSlaveValue, 1, 3);
            tableLayoutPanel2.Controls.Add(label1, 0, 0);
            tableLayoutPanel2.Dock = DockStyle.Fill;
            tableLayoutPanel2.Location = new Point(3, 25);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 4;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tableLayoutPanel2.Size = new Size(541, 253);
            tableLayoutPanel2.TabIndex = 0;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(3, 63);
            label2.Name = "label2";
            label2.Size = new Size(83, 21);
            label2.TabIndex = 1;
            label2.Text = "IP Address";
            label2.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(3, 126);
            label3.Name = "label3";
            label3.Size = new Size(38, 21);
            label3.TabIndex = 2;
            label3.Text = "Port";
            label3.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(3, 189);
            label4.Name = "label4";
            label4.Size = new Size(66, 21);
            label4.TabIndex = 3;
            label4.Text = "Slave ID";
            label4.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblConnectionValue
            // 
            lblConnectionValue.AutoSize = true;
            lblConnectionValue.Location = new Point(219, 0);
            lblConnectionValue.Name = "lblConnectionValue";
            lblConnectionValue.Size = new Size(57, 21);
            lblConnectionValue.TabIndex = 4;
            lblConnectionValue.Text = "Offline";
            // 
            // lblIpValue
            // 
            lblIpValue.AutoSize = true;
            lblIpValue.Location = new Point(219, 63);
            lblIpValue.Name = "lblIpValue";
            lblIpValue.Size = new Size(22, 21);
            lblIpValue.TabIndex = 5;
            lblIpValue.Text = "--";
            // 
            // lblPortValue
            // 
            lblPortValue.AutoSize = true;
            lblPortValue.Location = new Point(219, 126);
            lblPortValue.Name = "lblPortValue";
            lblPortValue.Size = new Size(22, 21);
            lblPortValue.TabIndex = 6;
            lblPortValue.Text = "--";
            // 
            // lblSlaveValue
            // 
            lblSlaveValue.AutoSize = true;
            lblSlaveValue.Location = new Point(219, 189);
            lblSlaveValue.Name = "lblSlaveValue";
            lblSlaveValue.Size = new Size(22, 21);
            lblSlaveValue.TabIndex = 7;
            lblSlaveValue.Text = "--";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(3, 0);
            label1.Name = "label1";
            label1.Size = new Size(89, 21);
            label1.TabIndex = 0;
            label1.Text = "Connection";
            label1.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // groupBox2
            // 
            groupBox2.Dock = DockStyle.Fill;
            groupBox2.Font = new Font("Segoe UI", 12F);
            groupBox2.Location = new Point(3, 290);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(547, 282);
            groupBox2.TabIndex = 1;
            groupBox2.TabStop = false;
            groupBox2.Text = "Last Operation";
            // 
            // groupBox3
            // 
            groupBox3.Dock = DockStyle.Fill;
            groupBox3.Font = new Font("Segoe UI", 12F);
            groupBox3.Location = new Point(556, 3);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(548, 281);
            groupBox3.TabIndex = 2;
            groupBox3.TabStop = false;
            groupBox3.Text = "System Information";
            // 
            // groupBox4
            // 
            groupBox4.Dock = DockStyle.Fill;
            groupBox4.Font = new Font("Segoe UI", 12F);
            groupBox4.Location = new Point(556, 290);
            groupBox4.Name = "groupBox4";
            groupBox4.Size = new Size(548, 282);
            groupBox4.TabIndex = 3;
            groupBox4.TabStop = false;
            groupBox4.Text = "Messages";
            // 
            // UcDashboard
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(tableLayoutPanel1);
            Name = "UcDashboard";
            Size = new Size(1107, 575);
            tableLayoutPanel1.ResumeLayout(false);
            groupBox1.ResumeLayout(false);
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel2.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private GroupBox groupBox3;
        private GroupBox groupBox4;
        private TableLayoutPanel tableLayoutPanel2;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label lblConnectionValue;
        private Label lblIpValue;
        private Label lblPortValue;
        private Label lblSlaveValue;
        private Label label1;
    }
}
