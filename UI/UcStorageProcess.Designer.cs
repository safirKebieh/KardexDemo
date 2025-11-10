namespace UI
{
    partial class UcStorageProcess
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
            lblState = new Label();
            lblNextFree = new Label();
            btnStart = new Button();
            btnStop = new Button();
            chkAutoLoop = new CheckBox();
            lstLog = new ListBox();
            btnClearLog = new Button();
            btnAlloff = new Button();
            numSlot = new NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)numSlot).BeginInit();
            SuspendLayout();
            // 
            // lblState
            // 
            lblState.AutoSize = true;
            lblState.Location = new Point(149, 132);
            lblState.Name = "lblState";
            lblState.Size = new Size(58, 15);
            lblState.TabIndex = 0;
            lblState.Text = "State: Idle";
            // 
            // lblNextFree
            // 
            lblNextFree.AutoSize = true;
            lblNextFree.Location = new Point(546, 78);
            lblNextFree.Name = "lblNextFree";
            lblNextFree.Size = new Size(65, 15);
            lblNextFree.TabIndex = 0;
            lblNextFree.Text = "Next free: -";
            // 
            // btnStart
            // 
            btnStart.Location = new Point(149, 62);
            btnStart.Name = "btnStart";
            btnStart.Size = new Size(113, 40);
            btnStart.TabIndex = 1;
            btnStart.Text = "Start";
            btnStart.UseVisualStyleBackColor = true;
            btnStart.Click += btnStart_Click;
            // 
            // btnStop
            // 
            btnStop.Enabled = false;
            btnStop.Location = new Point(277, 62);
            btnStop.Name = "btnStop";
            btnStop.Size = new Size(113, 40);
            btnStop.TabIndex = 1;
            btnStop.Text = "Stop";
            btnStop.UseVisualStyleBackColor = true;
            btnStop.Click += btnStop_Click;
            // 
            // chkAutoLoop
            // 
            chkAutoLoop.AutoSize = true;
            chkAutoLoop.Checked = true;
            chkAutoLoop.CheckState = CheckState.Checked;
            chkAutoLoop.Location = new Point(437, 74);
            chkAutoLoop.Name = "chkAutoLoop";
            chkAutoLoop.Size = new Size(79, 19);
            chkAutoLoop.TabIndex = 2;
            chkAutoLoop.Text = "Auto loop";
            chkAutoLoop.UseVisualStyleBackColor = true;
            // 
            // lstLog
            // 
            lstLog.FormattingEnabled = true;
            lstLog.ItemHeight = 15;
            lstLog.Location = new Point(149, 150);
            lstLog.Name = "lstLog";
            lstLog.Size = new Size(787, 244);
            lstLog.TabIndex = 3;
            // 
            // btnClearLog
            // 
            btnClearLog.Location = new Point(149, 400);
            btnClearLog.Name = "btnClearLog";
            btnClearLog.Size = new Size(75, 23);
            btnClearLog.TabIndex = 4;
            btnClearLog.Text = "Clear";
            btnClearLog.UseVisualStyleBackColor = true;
            btnClearLog.Click += btnClearLog_Click;
            // 
            // btnAlloff
            // 
            btnAlloff.Location = new Point(832, 56);
            btnAlloff.Name = "btnAlloff";
            btnAlloff.Size = new Size(139, 37);
            btnAlloff.TabIndex = 5;
            btnAlloff.Text = "All Outputs off";
            btnAlloff.UseVisualStyleBackColor = true;
            btnAlloff.Click += btnAlloff_Click;
            // 
            // numSlot
            // 
            numSlot.Location = new Point(690, 72);
            numSlot.Name = "numSlot";
            numSlot.Size = new Size(120, 23);
            numSlot.TabIndex = 6;
            // 
            // UcStorageProcess
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(numSlot);
            Controls.Add(btnAlloff);
            Controls.Add(btnClearLog);
            Controls.Add(lstLog);
            Controls.Add(chkAutoLoop);
            Controls.Add(btnStop);
            Controls.Add(btnStart);
            Controls.Add(lblNextFree);
            Controls.Add(lblState);
            Name = "UcStorageProcess";
            Size = new Size(1107, 575);
            ((System.ComponentModel.ISupportInitialize)numSlot).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblState;
        private Label lblNextFree;
        private Button btnStart;
        private Button btnStop;
        private CheckBox chkAutoLoop;
        private ListBox lstLog;
        private Button btnClearLog;
        private Button btnAlloff;
        private NumericUpDown numSlot;
    }
}
