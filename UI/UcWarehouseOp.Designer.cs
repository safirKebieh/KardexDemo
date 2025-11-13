namespace UI
{
    partial class UcWarehouseOp
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
            cmbMode = new ComboBox();
            numSlot = new NumericUpDown();
            btnStart = new Button();
            txtLog = new TextBox();
            label1 = new Label();
            label3 = new Label();
            label4 = new Label();
            btnQuitterung = new Button();
            ((System.ComponentModel.ISupportInitialize)numSlot).BeginInit();
            SuspendLayout();
            // 
            // cmbMode
            // 
            cmbMode.FormattingEnabled = true;
            cmbMode.Items.AddRange(new object[] { "Store", "Retrieve" });
            cmbMode.Location = new Point(64, 206);
            cmbMode.Name = "cmbMode";
            cmbMode.Size = new Size(121, 23);
            cmbMode.TabIndex = 0;
            // 
            // numSlot
            // 
            numSlot.Location = new Point(335, 209);
            numSlot.Name = "numSlot";
            numSlot.Size = new Size(120, 23);
            numSlot.TabIndex = 2;
            // 
            // btnStart
            // 
            btnStart.Location = new Point(95, 45);
            btnStart.Name = "btnStart";
            btnStart.Size = new Size(119, 31);
            btnStart.TabIndex = 3;
            btnStart.Text = "Start";
            btnStart.UseVisualStyleBackColor = true;
            btnStart.Click += btnStart_Click;
            // 
            // txtLog
            // 
            txtLog.Location = new Point(64, 265);
            txtLog.Multiline = true;
            txtLog.Name = "txtLog";
            txtLog.Size = new Size(772, 164);
            txtLog.TabIndex = 4;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(64, 188);
            label1.Name = "label1";
            label1.Size = new Size(93, 15);
            label1.TabIndex = 5;
            label1.Text = "Store or Retrieve";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(335, 191);
            label3.Name = "label3";
            label3.Size = new Size(27, 15);
            label3.TabIndex = 7;
            label3.Text = "Slot";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(64, 247);
            label4.Name = "label4";
            label4.Size = new Size(27, 15);
            label4.TabIndex = 8;
            label4.Text = "Log";
            // 
            // btnQuitterung
            // 
            btnQuitterung.Location = new Point(335, 45);
            btnQuitterung.Name = "btnQuitterung";
            btnQuitterung.Size = new Size(143, 31);
            btnQuitterung.TabIndex = 3;
            btnQuitterung.Text = "Acknowledgment";
            btnQuitterung.UseVisualStyleBackColor = true;
            btnQuitterung.Click += btnQuitterung_Click;
            // 
            // UcWarehouseOp
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label1);
            Controls.Add(txtLog);
            Controls.Add(btnQuitterung);
            Controls.Add(btnStart);
            Controls.Add(numSlot);
            Controls.Add(cmbMode);
            Name = "UcWarehouseOp";
            Size = new Size(870, 451);
            ((System.ComponentModel.ISupportInitialize)numSlot).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox cmbMode;
        private NumericUpDown numSlot;
        private Button btnStart;
        private TextBox txtLog;
        private Label label1;
        private Label label3;
        private Label label4;
        private Button btnQuitterung;
    }
}
