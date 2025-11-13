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
            label1 = new Label();
            label3 = new Label();
            label4 = new Label();
            btnQuitterung = new Button();
            txtLog = new RichTextBox();
            ((System.ComponentModel.ISupportInitialize)numSlot).BeginInit();
            SuspendLayout();
            // 
            // cmbMode
            // 
            cmbMode.FormattingEnabled = true;
            cmbMode.Items.AddRange(new object[] { "Store", "Retrieve" });
            cmbMode.Location = new Point(88, 23);
            cmbMode.Name = "cmbMode";
            cmbMode.Size = new Size(121, 23);
            cmbMode.TabIndex = 0;
            // 
            // numSlot
            // 
            numSlot.Location = new Point(430, 23);
            numSlot.Name = "numSlot";
            numSlot.Size = new Size(120, 23);
            numSlot.TabIndex = 2;
            // 
            // btnStart
            // 
            btnStart.Location = new Point(13, 100);
            btnStart.Name = "btnStart";
            btnStart.Size = new Size(160, 30);
            btnStart.TabIndex = 3;
            btnStart.Text = "Start Operation";
            btnStart.UseVisualStyleBackColor = true;
            btnStart.Click += btnStart_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(13, 27);
            label1.Name = "label1";
            label1.Size = new Size(60, 15);
            label1.TabIndex = 5;
            label1.Text = "Operation";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(348, 27);
            label3.Name = "label3";
            label3.Size = new Size(63, 15);
            label3.TabIndex = 7;
            label3.Text = "Target Slot";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(3, 202);
            label4.Name = "label4";
            label4.Size = new Size(83, 15);
            label4.TabIndex = 8;
            label4.Text = "Operation Log";
            // 
            // btnQuitterung
            // 
            btnQuitterung.Location = new Point(13, 136);
            btnQuitterung.Name = "btnQuitterung";
            btnQuitterung.Size = new Size(160, 30);
            btnQuitterung.TabIndex = 3;
            btnQuitterung.Text = "Acknowledge / Reset";
            btnQuitterung.UseVisualStyleBackColor = true;
            btnQuitterung.Click += btnQuitterung_Click;
            // 
            // txtLog
            // 
            txtLog.Location = new Point(3, 220);
            txtLog.Name = "txtLog";
            txtLog.ReadOnly = true;
            txtLog.Size = new Size(860, 222);
            txtLog.TabIndex = 9;
            txtLog.Text = "";
            // 
            // UcWarehouseOp
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(txtLog);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label1);
            Controls.Add(btnQuitterung);
            Controls.Add(btnStart);
            Controls.Add(numSlot);
            Controls.Add(cmbMode);
            Name = "UcWarehouseOp";
            Size = new Size(866, 445);
            ((System.ComponentModel.ISupportInitialize)numSlot).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox cmbMode;
        private NumericUpDown numSlot;
        private Button btnStart;
        private Label label1;
        private Label label3;
        private Label label4;
        private Button btnQuitterung;
        private RichTextBox txtLog;
    }
}
