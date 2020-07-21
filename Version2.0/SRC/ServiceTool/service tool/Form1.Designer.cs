namespace service_tool
{
    partial class FormSetup
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormSetup));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.ComboBoxPortName = new System.Windows.Forms.ComboBox();
            this.ComboBoxBaudRate = new System.Windows.Forms.ComboBox();
            this.ButtonOpenPort = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.ButtonClosePort = new System.Windows.Forms.Button();
            this.ButtonRead = new System.Windows.Forms.Button();
            this.TextBoxRead = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.ButtonSend = new System.Windows.Forms.Button();
            this.TextBoxSend = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(49, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Port Name\r\n";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(49, 78);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 15);
            this.label2.TabIndex = 0;
            this.label2.Text = "Baud Rate";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(49, 115);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 15);
            this.label3.TabIndex = 0;
            this.label3.Text = "Status";
            // 
            // ComboBoxPortName
            // 
            this.ComboBoxPortName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBoxPortName.FormattingEnabled = true;
            this.ComboBoxPortName.Location = new System.Drawing.Point(154, 32);
            this.ComboBoxPortName.Name = "ComboBoxPortName";
            this.ComboBoxPortName.Size = new System.Drawing.Size(121, 23);
            this.ComboBoxPortName.TabIndex = 1;
            // 
            // ComboBoxBaudRate
            // 
            this.ComboBoxBaudRate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBoxBaudRate.FormattingEnabled = true;
            this.ComboBoxBaudRate.Items.AddRange(new object[] {
            "9600",
            "115200"});
            this.ComboBoxBaudRate.Location = new System.Drawing.Point(154, 71);
            this.ComboBoxBaudRate.Name = "ComboBoxBaudRate";
            this.ComboBoxBaudRate.Size = new System.Drawing.Size(121, 23);
            this.ComboBoxBaudRate.TabIndex = 1;
            // 
            // ButtonOpenPort
            // 
            this.ButtonOpenPort.Location = new System.Drawing.Point(389, 32);
            this.ButtonOpenPort.Name = "ButtonOpenPort";
            this.ButtonOpenPort.Size = new System.Drawing.Size(145, 42);
            this.ButtonOpenPort.TabIndex = 3;
            this.ButtonOpenPort.Text = "Open Port";
            this.ButtonOpenPort.UseVisualStyleBackColor = true;
            this.ButtonOpenPort.Click += new System.EventHandler(this.button1_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(154, 115);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(121, 22);
            this.progressBar1.TabIndex = 5;
            // 
            // ButtonClosePort
            // 
            this.ButtonClosePort.Enabled = false;
            this.ButtonClosePort.Location = new System.Drawing.Point(389, 91);
            this.ButtonClosePort.Name = "ButtonClosePort";
            this.ButtonClosePort.Size = new System.Drawing.Size(145, 42);
            this.ButtonClosePort.TabIndex = 3;
            this.ButtonClosePort.Text = "Close Port";
            this.ButtonClosePort.UseVisualStyleBackColor = true;
            this.ButtonClosePort.Click += new System.EventHandler(this.ButtonClosePort_Click);
            // 
            // ButtonRead
            // 
            this.ButtonRead.Location = new System.Drawing.Point(120, 169);
            this.ButtonRead.Name = "ButtonRead";
            this.ButtonRead.Size = new System.Drawing.Size(91, 35);
            this.ButtonRead.TabIndex = 3;
            this.ButtonRead.Text = "Read";
            this.ButtonRead.UseVisualStyleBackColor = true;
            this.ButtonRead.Click += new System.EventHandler(this.ButtonRead_Click);
            // 
            // TextBoxRead
            // 
            this.TextBoxRead.Location = new System.Drawing.Point(10, 27);
            this.TextBoxRead.Multiline = true;
            this.TextBoxRead.Name = "TextBoxRead";
            this.TextBoxRead.ReadOnly = true;
            this.TextBoxRead.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.TextBoxRead.Size = new System.Drawing.Size(201, 133);
            this.TextBoxRead.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.TextBoxRead);
            this.groupBox2.Controls.Add(this.ButtonRead);
            this.groupBox2.Location = new System.Drawing.Point(320, 165);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(223, 217);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Receive Here";
            // 
            // ButtonSend
            // 
            this.ButtonSend.Location = new System.Drawing.Point(120, 169);
            this.ButtonSend.Name = "ButtonSend";
            this.ButtonSend.Size = new System.Drawing.Size(91, 35);
            this.ButtonSend.TabIndex = 3;
            this.ButtonSend.Text = "send";
            this.ButtonSend.UseVisualStyleBackColor = true;
            this.ButtonSend.Click += new System.EventHandler(this.ButtonSend_Click);
            // 
            // TextBoxSend
            // 
            this.TextBoxSend.Enabled = false;
            this.TextBoxSend.Location = new System.Drawing.Point(10, 27);
            this.TextBoxSend.Multiline = true;
            this.TextBoxSend.Name = "TextBoxSend";
            this.TextBoxSend.Size = new System.Drawing.Size(201, 133);
            this.TextBoxSend.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.TextBoxSend);
            this.groupBox1.Controls.Add(this.ButtonSend);
            this.groupBox1.Location = new System.Drawing.Point(52, 165);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(223, 217);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Send Here";
            // 
            // FormSetup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 419);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.ButtonClosePort);
            this.Controls.Add(this.ButtonOpenPort);
            this.Controls.Add(this.ComboBoxBaudRate);
            this.Controls.Add(this.ComboBoxPortName);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormSetup";
            this.Text = "Serial Communication";
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox ComboBoxPortName;
        private System.Windows.Forms.ComboBox ComboBoxBaudRate;
        private System.Windows.Forms.Button ButtonOpenPort;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.IO.Ports.SerialPort serialPort1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button ButtonClosePort;
        private System.Windows.Forms.Button ButtonRead;
        private System.Windows.Forms.TextBox TextBoxRead;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button ButtonSend;
        private System.Windows.Forms.TextBox TextBoxSend;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}