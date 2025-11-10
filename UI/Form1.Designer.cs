namespace UI
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btnConnect = new Button();
            btnOn = new Button();
            btnOff = new Button();
            lblSensor = new Label();
            btnDisconnect = new Button();
            SuspendLayout();
            // 
            // btnConnect
            // 
            btnConnect.Location = new Point(12, 12);
            btnConnect.Name = "btnConnect";
            btnConnect.Size = new Size(161, 23);
            btnConnect.TabIndex = 0;
            btnConnect.Text = "Connect";
            btnConnect.UseVisualStyleBackColor = true;
            btnConnect.Click += btnConnect_Click;
            // 
            // btnOn
            // 
            btnOn.Location = new Point(12, 41);
            btnOn.Name = "btnOn";
            btnOn.Size = new Size(161, 23);
            btnOn.TabIndex = 0;
            btnOn.Text = "Turn On";
            btnOn.UseVisualStyleBackColor = true;
            btnOn.Click += btnOn_Click;
            // 
            // btnOff
            // 
            btnOff.Location = new Point(12, 70);
            btnOff.Name = "btnOff";
            btnOff.Size = new Size(161, 23);
            btnOff.TabIndex = 0;
            btnOff.Text = "Turn Off";
            btnOff.UseVisualStyleBackColor = true;
            btnOff.Click += btnOff_Click;
            // 
            // lblSensor
            // 
            lblSensor.AutoSize = true;
            lblSensor.Location = new Point(279, 16);
            lblSensor.Name = "lblSensor";
            lblSensor.Size = new Size(38, 15);
            lblSensor.TabIndex = 1;
            lblSensor.Text = "label1";
            // 
            // btnDisconnect
            // 
            btnDisconnect.Location = new Point(12, 99);
            btnDisconnect.Name = "btnDisconnect";
            btnDisconnect.Size = new Size(161, 23);
            btnDisconnect.TabIndex = 0;
            btnDisconnect.Text = "Disconnect";
            btnDisconnect.UseVisualStyleBackColor = true;
            btnDisconnect.Click += btnDisconnect_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(lblSensor);
            Controls.Add(btnOff);
            Controls.Add(btnOn);
            Controls.Add(btnDisconnect);
            Controls.Add(btnConnect);
            Name = "Form1";
            Text = "Form1";
            FormClosed += Form1_FormClosed;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnConnect;
        private Button btnOn;
        private Button btnOff;
        private Label lblSensor;
        private Button btnDisconnect;
    }
}
