namespace UI
{
    partial class UcRetrievePallet
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
            lblSelected = new Label();
            numSlot = new NumericUpDown();
            btnRetrieve = new Button();
            btnStop = new Button();
            btnAlloff = new Button();
            btnReset = new Button();
            btnClearLog = new Button();
            lstLog = new ListBox();
            ((System.ComponentModel.ISupportInitialize)numSlot).BeginInit();
            SuspendLayout();
            // 
            // lblState
            // 
            lblState.AutoSize = true;
            lblState.Location = new Point(183, 100);
            lblState.Name = "lblState";
            lblState.Size = new Size(58, 15);
            lblState.TabIndex = 0;
            lblState.Text = "State: Idle";
            // 
            // lblSelected
            // 
            lblSelected.AutoSize = true;
            lblSelected.Location = new Point(183, 130);
            lblSelected.Name = "lblSelected";
            lblSelected.Size = new Size(51, 15);
            lblSelected.TabIndex = 0;
            lblSelected.Text = "Selected";
            // 
            // numSlot
            // 
            numSlot.Location = new Point(188, 160);
            numSlot.Name = "numSlot";
            numSlot.Size = new Size(120, 23);
            numSlot.TabIndex = 1;
            // 
            // btnRetrieve
            // 
            btnRetrieve.Location = new Point(188, 206);
            btnRetrieve.Name = "btnRetrieve";
            btnRetrieve.Size = new Size(114, 29);
            btnRetrieve.TabIndex = 2;
            btnRetrieve.Text = "Retrieve";
            btnRetrieve.UseVisualStyleBackColor = true;
            btnRetrieve.Click += btnRetrieve_Click;
            // 
            // btnStop
            // 
            btnStop.Location = new Point(188, 252);
            btnStop.Name = "btnStop";
            btnStop.Size = new Size(114, 29);
            btnStop.TabIndex = 2;
            btnStop.Text = "Stop";
            btnStop.UseVisualStyleBackColor = true;
            btnStop.Click += btnStop_Click;
            // 
            // btnAlloff
            // 
            btnAlloff.Location = new Point(188, 307);
            btnAlloff.Name = "btnAlloff";
            btnAlloff.Size = new Size(114, 29);
            btnAlloff.TabIndex = 2;
            btnAlloff.Text = "All off";
            btnAlloff.UseVisualStyleBackColor = true;
            btnAlloff.Click += btnAlloff_Click;
            // 
            // btnReset
            // 
            btnReset.Location = new Point(188, 362);
            btnReset.Name = "btnReset";
            btnReset.Size = new Size(114, 29);
            btnReset.TabIndex = 2;
            btnReset.Text = "Reset";
            btnReset.UseVisualStyleBackColor = true;
            btnReset.Click += btnReset_Click;
            // 
            // btnClearLog
            // 
            btnClearLog.Location = new Point(586, 206);
            btnClearLog.Name = "btnClearLog";
            btnClearLog.Size = new Size(114, 29);
            btnClearLog.TabIndex = 2;
            btnClearLog.Text = "Clear Log";
            btnClearLog.UseVisualStyleBackColor = true;
            btnClearLog.Click += btnClearLog_Click;
            // 
            // lstLog
            // 
            lstLog.FormattingEnabled = true;
            lstLog.ItemHeight = 15;
            lstLog.Location = new Point(569, 257);
            lstLog.Name = "lstLog";
            lstLog.Size = new Size(509, 289);
            lstLog.TabIndex = 3;
            // 
            // UcRetrievePallet
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(lstLog);
            Controls.Add(btnClearLog);
            Controls.Add(btnReset);
            Controls.Add(btnAlloff);
            Controls.Add(btnStop);
            Controls.Add(btnRetrieve);
            Controls.Add(numSlot);
            Controls.Add(lblSelected);
            Controls.Add(lblState);
            Name = "UcRetrievePallet";
            Size = new Size(1107, 575);
            ((System.ComponentModel.ISupportInitialize)numSlot).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblState;
        private Label lblSelected;
        private NumericUpDown numSlot;
        private Button btnRetrieve;
        private Button btnStop;
        private Button btnAlloff;
        private Button btnReset;
        private Button btnClearLog;
        private ListBox lstLog;
    }
}
