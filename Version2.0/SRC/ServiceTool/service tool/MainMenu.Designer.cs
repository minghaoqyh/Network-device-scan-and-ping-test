namespace service_tool
{
    partial class MainMenu
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainMenu));
            this.Connectivity = new System.Windows.Forms.GroupBox();
            this.label12 = new System.Windows.Forms.Label();
            this.TextboxSerial = new System.Windows.Forms.TextBox();
            this.ButtonClosePort = new System.Windows.Forms.Button();
            this.ButtonOpenPort = new System.Windows.Forms.Button();
            this.ComboBoxPortName = new System.Windows.Forms.ComboBox();
            this.LablePort = new System.Windows.Forms.Label();
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.OpenFileDialogFW = new System.Windows.Forms.OpenFileDialog();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.TabPageSysInfo = new System.Windows.Forms.TabPage();
            this.label14 = new System.Windows.Forms.Label();
            this.SystemInfo = new System.Windows.Forms.TextBox();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.GetFirstInstallDate = new System.Windows.Forms.Button();
            this.ButtonFirstDate = new System.Windows.Forms.Button();
            this.TextboxFirstTime = new System.Windows.Forms.TextBox();
            this.DateTimePickerFirst = new System.Windows.Forms.DateTimePicker();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.GetLifeLedVlight = new System.Windows.Forms.Button();
            this.GetLedLifeRGB = new System.Windows.Forms.Button();
            this.label13 = new System.Windows.Forms.Label();
            this.TextboxLedTimeVlight = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.TextboxLedTimeRGB = new System.Windows.Forms.TextBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.GetSystemTime = new System.Windows.Forms.Button();
            this.dateSystemTimePicker = new System.Windows.Forms.DateTimePicker();
            this.ButtonSettime = new System.Windows.Forms.Button();
            this.TextboxTime = new System.Windows.Forms.TextBox();
            this.DateTimePickerSys = new System.Windows.Forms.DateTimePicker();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.TextboxSysIDSet = new System.Windows.Forms.TextBox();
            this.ModifyID = new System.Windows.Forms.Button();
            this.TextboxSysIDGet = new System.Windows.Forms.TextBox();
            this.GetSystemID = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.SystemTypeSet = new System.Windows.Forms.Button();
            this.SystemTypeList = new System.Windows.Forms.ComboBox();
            this.TextboxSysType = new System.Windows.Forms.TextBox();
            this.GetSystemType = new System.Windows.Forms.Button();
            this.labelSysType = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.LabelSysSn = new System.Windows.Forms.Label();
            this.TabPageFw = new System.Windows.Forms.TabPage();
            this.label11 = new System.Windows.Forms.Label();
            this.txtFwInfo = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.cmbPCBName = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnDownload = new System.Windows.Forms.Button();
            this.btnBrowseHex = new System.Windows.Forms.Button();
            this.txtHexFilePath = new System.Windows.Forms.TextBox();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.GetVersion = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.TextboxCurCentral = new System.Windows.Forms.TextBox();
            this.TextboxCurOpmi = new System.Windows.Forms.TextBox();
            this.TextboxCurCCU = new System.Windows.Forms.TextBox();
            this.TabPageMaintenance = new System.Windows.Forms.TabPage();
            this.label15 = new System.Windows.Forms.Label();
            this.MaintenanceInfo = new System.Windows.Forms.TextBox();
            this.groupBox10 = new System.Windows.Forms.GroupBox();
            this.GetnextServiceDate = new System.Windows.Forms.Button();
            this.GetLastServiceDate = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.TextboxNextService = new System.Windows.Forms.TextBox();
            this.TextboxLastService = new System.Windows.Forms.TextBox();
            this.groupBox9 = new System.Windows.Forms.GroupBox();
            this.SetNextServiceDate = new System.Windows.Forms.Button();
            this.CheckboxMaintenanceEn = new System.Windows.Forms.CheckBox();
            this.label7 = new System.Windows.Forms.Label();
            this.RadiobuttonByDate = new System.Windows.Forms.RadioButton();
            this.RadiobuttonByInterval = new System.Windows.Forms.RadioButton();
            this.DateTimePickerService = new System.Windows.Forms.DateTimePicker();
            this.ComboboxInterval = new System.Windows.Forms.ComboBox();
            this.dateTimePickerLast = new System.Windows.Forms.DateTimePicker();
            this.TabPageLog = new System.Windows.Forms.TabPage();
            this.logFiles1 = new service_tool.LogFiles();
            this.TabPageParameter = new System.Windows.Forms.TabPage();
            this.parameters = new service_tool.Parameters();
            this.TabPageLicense = new System.Windows.Forms.TabPage();
            this.license2 = new service_tool.License();
            this.tabAbout = new System.Windows.Forms.TabPage();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.tbVersion = new System.Windows.Forms.TextBox();
            this.label21 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.TimerSecond = new System.Windows.Forms.Timer(this.components);
            this.Connectivity.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.TabPageSysInfo.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.TabPageFw.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.TabPageMaintenance.SuspendLayout();
            this.groupBox10.SuspendLayout();
            this.groupBox9.SuspendLayout();
            this.TabPageLog.SuspendLayout();
            this.TabPageParameter.SuspendLayout();
            this.TabPageLicense.SuspendLayout();
            this.tabAbout.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // Connectivity
            // 
            this.Connectivity.Controls.Add(this.label12);
            this.Connectivity.Controls.Add(this.TextboxSerial);
            this.Connectivity.Controls.Add(this.ButtonClosePort);
            this.Connectivity.Controls.Add(this.ButtonOpenPort);
            this.Connectivity.Controls.Add(this.ComboBoxPortName);
            this.Connectivity.Controls.Add(this.LablePort);
            this.Connectivity.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(134)));
            this.Connectivity.Location = new System.Drawing.Point(29, 12);
            this.Connectivity.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Connectivity.Name = "Connectivity";
            this.Connectivity.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Connectivity.Size = new System.Drawing.Size(965, 72);
            this.Connectivity.TabIndex = 2;
            this.Connectivity.TabStop = false;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(583, 31);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(70, 16);
            this.label12.TabIndex = 15;
            this.label12.Text = "Serial Info:";
            // 
            // TextboxSerial
            // 
            this.TextboxSerial.Location = new System.Drawing.Point(677, 27);
            this.TextboxSerial.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.TextboxSerial.Name = "TextboxSerial";
            this.TextboxSerial.ReadOnly = true;
            this.TextboxSerial.Size = new System.Drawing.Size(245, 21);
            this.TextboxSerial.TabIndex = 4;
            this.TextboxSerial.Text = "Serial port not connected.";
            // 
            // ButtonClosePort
            // 
            this.ButtonClosePort.Enabled = false;
            this.ButtonClosePort.Location = new System.Drawing.Point(381, 26);
            this.ButtonClosePort.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ButtonClosePort.Name = "ButtonClosePort";
            this.ButtonClosePort.Size = new System.Drawing.Size(96, 27);
            this.ButtonClosePort.TabIndex = 3;
            this.ButtonClosePort.Text = "Close Port";
            this.ButtonClosePort.UseVisualStyleBackColor = true;
            this.ButtonClosePort.Click += new System.EventHandler(this.ButtonClosePort_Click);
            // 
            // ButtonOpenPort
            // 
            this.ButtonOpenPort.Location = new System.Drawing.Point(267, 26);
            this.ButtonOpenPort.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ButtonOpenPort.Name = "ButtonOpenPort";
            this.ButtonOpenPort.Size = new System.Drawing.Size(96, 27);
            this.ButtonOpenPort.TabIndex = 3;
            this.ButtonOpenPort.Text = "Open Port";
            this.ButtonOpenPort.UseVisualStyleBackColor = true;
            this.ButtonOpenPort.Click += new System.EventHandler(this.ButtonOpenPort_Click);
            // 
            // ComboBoxPortName
            // 
            this.ComboBoxPortName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBoxPortName.FormattingEnabled = true;
            this.ComboBoxPortName.Location = new System.Drawing.Point(107, 27);
            this.ComboBoxPortName.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ComboBoxPortName.Name = "ComboBoxPortName";
            this.ComboBoxPortName.Size = new System.Drawing.Size(121, 23);
            this.ComboBoxPortName.TabIndex = 2;
            // 
            // LablePort
            // 
            this.LablePort.AutoSize = true;
            this.LablePort.Location = new System.Drawing.Point(14, 30);
            this.LablePort.Name = "LablePort";
            this.LablePort.Size = new System.Drawing.Size(73, 16);
            this.LablePort.TabIndex = 1;
            this.LablePort.Text = "Serial Port:";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.TabPageSysInfo);
            this.tabControl1.Controls.Add(this.TabPageFw);
            this.tabControl1.Controls.Add(this.TabPageMaintenance);
            this.tabControl1.Controls.Add(this.TabPageLog);
            this.tabControl1.Controls.Add(this.TabPageParameter);
            this.tabControl1.Controls.Add(this.TabPageLicense);
            this.tabControl1.Controls.Add(this.tabAbout);
            this.tabControl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(134)));
            this.tabControl1.Location = new System.Drawing.Point(29, 104);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(969, 562);
            this.tabControl1.TabIndex = 3;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // TabPageSysInfo
            // 
            this.TabPageSysInfo.BackColor = System.Drawing.Color.White;
            this.TabPageSysInfo.Controls.Add(this.label14);
            this.TabPageSysInfo.Controls.Add(this.SystemInfo);
            this.TabPageSysInfo.Controls.Add(this.groupBox8);
            this.TabPageSysInfo.Controls.Add(this.groupBox7);
            this.TabPageSysInfo.Controls.Add(this.groupBox6);
            this.TabPageSysInfo.Controls.Add(this.groupBox5);
            this.TabPageSysInfo.Controls.Add(this.groupBox4);
            this.TabPageSysInfo.Controls.Add(this.labelSysType);
            this.TabPageSysInfo.Controls.Add(this.label4);
            this.TabPageSysInfo.Controls.Add(this.label3);
            this.TabPageSysInfo.Controls.Add(this.LabelSysSn);
            this.TabPageSysInfo.Location = new System.Drawing.Point(4, 24);
            this.TabPageSysInfo.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.TabPageSysInfo.Name = "TabPageSysInfo";
            this.TabPageSysInfo.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.TabPageSysInfo.Size = new System.Drawing.Size(961, 534);
            this.TabPageSysInfo.TabIndex = 0;
            this.TabPageSysInfo.Text = "System Info";
            this.TabPageSysInfo.Click += new System.EventHandler(this.TabPageSysInfo_Click);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(41, 479);
            this.label14.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(32, 16);
            this.label14.TabIndex = 15;
            this.label14.Text = "Info:";
            // 
            // SystemInfo
            // 
            this.SystemInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(134)));
            this.SystemInfo.Location = new System.Drawing.Point(99, 475);
            this.SystemInfo.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.SystemInfo.Name = "SystemInfo";
            this.SystemInfo.ReadOnly = true;
            this.SystemInfo.Size = new System.Drawing.Size(823, 21);
            this.SystemInfo.TabIndex = 4;
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.GetFirstInstallDate);
            this.groupBox8.Controls.Add(this.ButtonFirstDate);
            this.groupBox8.Controls.Add(this.TextboxFirstTime);
            this.groupBox8.Controls.Add(this.DateTimePickerFirst);
            this.groupBox8.Location = new System.Drawing.Point(30, 375);
            this.groupBox8.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox8.Size = new System.Drawing.Size(892, 75);
            this.groupBox8.TabIndex = 16;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "First Install Date";
            // 
            // GetFirstInstallDate
            // 
            this.GetFirstInstallDate.Location = new System.Drawing.Point(268, 28);
            this.GetFirstInstallDate.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.GetFirstInstallDate.Name = "GetFirstInstallDate";
            this.GetFirstInstallDate.Size = new System.Drawing.Size(100, 27);
            this.GetFirstInstallDate.TabIndex = 14;
            this.GetFirstInstallDate.Text = "Get";
            this.GetFirstInstallDate.UseVisualStyleBackColor = true;
            this.GetFirstInstallDate.Click += new System.EventHandler(this.GetFirstInstallDate_Click);
            // 
            // ButtonFirstDate
            // 
            this.ButtonFirstDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(134)));
            this.ButtonFirstDate.Location = new System.Drawing.Point(763, 28);
            this.ButtonFirstDate.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ButtonFirstDate.Name = "ButtonFirstDate";
            this.ButtonFirstDate.Size = new System.Drawing.Size(100, 27);
            this.ButtonFirstDate.TabIndex = 3;
            this.ButtonFirstDate.Text = "Set Date";
            this.ButtonFirstDate.UseVisualStyleBackColor = true;
            this.ButtonFirstDate.Click += new System.EventHandler(this.ButtonSetFirstDate_Click);
            // 
            // TextboxFirstTime
            // 
            this.TextboxFirstTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(134)));
            this.TextboxFirstTime.Location = new System.Drawing.Point(25, 31);
            this.TextboxFirstTime.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.TextboxFirstTime.Name = "TextboxFirstTime";
            this.TextboxFirstTime.ReadOnly = true;
            this.TextboxFirstTime.Size = new System.Drawing.Size(209, 21);
            this.TextboxFirstTime.TabIndex = 0;
            // 
            // DateTimePickerFirst
            // 
            this.DateTimePickerFirst.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(134)));
            this.DateTimePickerFirst.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.DateTimePickerFirst.Location = new System.Drawing.Point(617, 32);
            this.DateTimePickerFirst.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.DateTimePickerFirst.MinDate = new System.DateTime(2010, 1, 1, 0, 0, 0, 0);
            this.DateTimePickerFirst.Name = "DateTimePickerFirst";
            this.DateTimePickerFirst.Size = new System.Drawing.Size(111, 21);
            this.DateTimePickerFirst.TabIndex = 2;
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.GetLifeLedVlight);
            this.groupBox7.Controls.Add(this.GetLedLifeRGB);
            this.groupBox7.Controls.Add(this.label13);
            this.groupBox7.Controls.Add(this.TextboxLedTimeVlight);
            this.groupBox7.Controls.Add(this.label2);
            this.groupBox7.Controls.Add(this.TextboxLedTimeRGB);
            this.groupBox7.Location = new System.Drawing.Point(30, 151);
            this.groupBox7.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox7.Size = new System.Drawing.Size(892, 110);
            this.groupBox7.TabIndex = 15;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "LED Used Time";
            // 
            // GetLifeLedVlight
            // 
            this.GetLifeLedVlight.Location = new System.Drawing.Point(763, 65);
            this.GetLifeLedVlight.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.GetLifeLedVlight.Name = "GetLifeLedVlight";
            this.GetLifeLedVlight.Size = new System.Drawing.Size(100, 27);
            this.GetLifeLedVlight.TabIndex = 14;
            this.GetLifeLedVlight.Text = "Get";
            this.GetLifeLedVlight.UseVisualStyleBackColor = true;
            this.GetLifeLedVlight.Click += new System.EventHandler(this.GetLifeLedVlight_Click);
            // 
            // GetLedLifeRGB
            // 
            this.GetLedLifeRGB.Location = new System.Drawing.Point(763, 25);
            this.GetLedLifeRGB.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.GetLedLifeRGB.Name = "GetLedLifeRGB";
            this.GetLedLifeRGB.Size = new System.Drawing.Size(100, 27);
            this.GetLedLifeRGB.TabIndex = 13;
            this.GetLedLifeRGB.Text = "Get";
            this.GetLedLifeRGB.UseVisualStyleBackColor = true;
            this.GetLedLifeRGB.Click += new System.EventHandler(this.GetLedLifeRGB_Click);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(30, 72);
            this.label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(222, 16);
            this.label13.TabIndex = 3;
            this.label13.Text = "Helios light source V-light used time:";
            // 
            // TextboxLedTimeVlight
            // 
            this.TextboxLedTimeVlight.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(134)));
            this.TextboxLedTimeVlight.Location = new System.Drawing.Point(462, 68);
            this.TextboxLedTimeVlight.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.TextboxLedTimeVlight.Multiline = true;
            this.TextboxLedTimeVlight.Name = "TextboxLedTimeVlight";
            this.TextboxLedTimeVlight.ReadOnly = true;
            this.TextboxLedTimeVlight.Size = new System.Drawing.Size(262, 24);
            this.TextboxLedTimeVlight.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(30, 32);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(214, 16);
            this.label2.TabIndex = 1;
            this.label2.Text = "Helios light source RGB used time:";
            // 
            // TextboxLedTimeRGB
            // 
            this.TextboxLedTimeRGB.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(134)));
            this.TextboxLedTimeRGB.Location = new System.Drawing.Point(462, 29);
            this.TextboxLedTimeRGB.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.TextboxLedTimeRGB.Multiline = true;
            this.TextboxLedTimeRGB.Name = "TextboxLedTimeRGB";
            this.TextboxLedTimeRGB.ReadOnly = true;
            this.TextboxLedTimeRGB.Size = new System.Drawing.Size(262, 24);
            this.TextboxLedTimeRGB.TabIndex = 0;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.GetSystemTime);
            this.groupBox6.Controls.Add(this.dateSystemTimePicker);
            this.groupBox6.Controls.Add(this.ButtonSettime);
            this.groupBox6.Controls.Add(this.TextboxTime);
            this.groupBox6.Controls.Add(this.DateTimePickerSys);
            this.groupBox6.Location = new System.Drawing.Point(30, 280);
            this.groupBox6.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox6.Size = new System.Drawing.Size(892, 75);
            this.groupBox6.TabIndex = 14;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "System Time";
            // 
            // GetSystemTime
            // 
            this.GetSystemTime.Location = new System.Drawing.Point(268, 30);
            this.GetSystemTime.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.GetSystemTime.Name = "GetSystemTime";
            this.GetSystemTime.Size = new System.Drawing.Size(100, 27);
            this.GetSystemTime.TabIndex = 13;
            this.GetSystemTime.Text = "Get";
            this.GetSystemTime.UseVisualStyleBackColor = true;
            this.GetSystemTime.Click += new System.EventHandler(this.GetSystemTime_Click);
            // 
            // dateSystemTimePicker
            // 
            this.dateSystemTimePicker.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(134)));
            this.dateSystemTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateSystemTimePicker.Location = new System.Drawing.Point(462, 33);
            this.dateSystemTimePicker.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dateSystemTimePicker.MinDate = new System.DateTime(2010, 1, 1, 0, 0, 0, 0);
            this.dateSystemTimePicker.Name = "dateSystemTimePicker";
            this.dateSystemTimePicker.Size = new System.Drawing.Size(111, 21);
            this.dateSystemTimePicker.TabIndex = 4;
            // 
            // ButtonSettime
            // 
            this.ButtonSettime.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(134)));
            this.ButtonSettime.Location = new System.Drawing.Point(763, 30);
            this.ButtonSettime.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ButtonSettime.Name = "ButtonSettime";
            this.ButtonSettime.Size = new System.Drawing.Size(100, 27);
            this.ButtonSettime.TabIndex = 3;
            this.ButtonSettime.Text = "Set Time";
            this.ButtonSettime.UseVisualStyleBackColor = true;
            this.ButtonSettime.Click += new System.EventHandler(this.ButtonSetSysTime_Click);
            // 
            // TextboxTime
            // 
            this.TextboxTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(134)));
            this.TextboxTime.Location = new System.Drawing.Point(25, 33);
            this.TextboxTime.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.TextboxTime.Name = "TextboxTime";
            this.TextboxTime.ReadOnly = true;
            this.TextboxTime.Size = new System.Drawing.Size(209, 21);
            this.TextboxTime.TabIndex = 0;
            // 
            // DateTimePickerSys
            // 
            this.DateTimePickerSys.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(134)));
            this.DateTimePickerSys.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.DateTimePickerSys.Location = new System.Drawing.Point(617, 33);
            this.DateTimePickerSys.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.DateTimePickerSys.MinDate = new System.DateTime(2010, 1, 1, 0, 0, 0, 0);
            this.DateTimePickerSys.Name = "DateTimePickerSys";
            this.DateTimePickerSys.ShowUpDown = true;
            this.DateTimePickerSys.Size = new System.Drawing.Size(107, 21);
            this.DateTimePickerSys.TabIndex = 2;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.TextboxSysIDSet);
            this.groupBox5.Controls.Add(this.ModifyID);
            this.groupBox5.Controls.Add(this.TextboxSysIDGet);
            this.groupBox5.Controls.Add(this.GetSystemID);
            this.groupBox5.Location = new System.Drawing.Point(469, 16);
            this.groupBox5.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox5.Size = new System.Drawing.Size(453, 115);
            this.groupBox5.TabIndex = 13;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Carrier Arm SN";
            // 
            // TextboxSysIDSet
            // 
            this.TextboxSysIDSet.Location = new System.Drawing.Point(23, 76);
            this.TextboxSysIDSet.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.TextboxSysIDSet.Name = "TextboxSysIDSet";
            this.TextboxSysIDSet.ReadOnly = true;
            this.TextboxSysIDSet.Size = new System.Drawing.Size(262, 21);
            this.TextboxSysIDSet.TabIndex = 13;
            // 
            // ModifyID
            // 
            this.ModifyID.Location = new System.Drawing.Point(325, 74);
            this.ModifyID.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.ModifyID.Name = "ModifyID";
            this.ModifyID.Size = new System.Drawing.Size(100, 27);
            this.ModifyID.TabIndex = 12;
            this.ModifyID.Text = "Modify";
            this.ModifyID.UseVisualStyleBackColor = true;
            this.ModifyID.Click += new System.EventHandler(this.ModifyID_Click);
            // 
            // TextboxSysIDGet
            // 
            this.TextboxSysIDGet.Location = new System.Drawing.Point(23, 36);
            this.TextboxSysIDGet.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.TextboxSysIDGet.Name = "TextboxSysIDGet";
            this.TextboxSysIDGet.ReadOnly = true;
            this.TextboxSysIDGet.Size = new System.Drawing.Size(262, 21);
            this.TextboxSysIDGet.TabIndex = 10;
            // 
            // GetSystemID
            // 
            this.GetSystemID.Location = new System.Drawing.Point(325, 33);
            this.GetSystemID.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.GetSystemID.Name = "GetSystemID";
            this.GetSystemID.Size = new System.Drawing.Size(100, 27);
            this.GetSystemID.TabIndex = 9;
            this.GetSystemID.Text = "Get";
            this.GetSystemID.UseVisualStyleBackColor = true;
            this.GetSystemID.Click += new System.EventHandler(this.GetSystemID_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.SystemTypeSet);
            this.groupBox4.Controls.Add(this.SystemTypeList);
            this.groupBox4.Controls.Add(this.TextboxSysType);
            this.groupBox4.Controls.Add(this.GetSystemType);
            this.groupBox4.Location = new System.Drawing.Point(30, 16);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox4.Size = new System.Drawing.Size(402, 115);
            this.groupBox4.TabIndex = 7;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "System Type";
            // 
            // SystemTypeSet
            // 
            this.SystemTypeSet.Location = new System.Drawing.Point(268, 74);
            this.SystemTypeSet.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.SystemTypeSet.Name = "SystemTypeSet";
            this.SystemTypeSet.Size = new System.Drawing.Size(100, 27);
            this.SystemTypeSet.TabIndex = 12;
            this.SystemTypeSet.Text = "Set";
            this.SystemTypeSet.UseVisualStyleBackColor = true;
            this.SystemTypeSet.Click += new System.EventHandler(this.SystemTypeSet_Click);
            // 
            // SystemTypeList
            // 
            this.SystemTypeList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.SystemTypeList.FormattingEnabled = true;
            this.SystemTypeList.Location = new System.Drawing.Point(25, 72);
            this.SystemTypeList.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.SystemTypeList.Name = "SystemTypeList";
            this.SystemTypeList.Size = new System.Drawing.Size(209, 23);
            this.SystemTypeList.TabIndex = 11;
            this.SystemTypeList.SelectedIndexChanged += new System.EventHandler(this.SystemTypeList_SelectedIndexChanged);
            // 
            // TextboxSysType
            // 
            this.TextboxSysType.Location = new System.Drawing.Point(25, 35);
            this.TextboxSysType.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.TextboxSysType.Name = "TextboxSysType";
            this.TextboxSysType.ReadOnly = true;
            this.TextboxSysType.Size = new System.Drawing.Size(209, 21);
            this.TextboxSysType.TabIndex = 10;
            // 
            // GetSystemType
            // 
            this.GetSystemType.Location = new System.Drawing.Point(268, 33);
            this.GetSystemType.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.GetSystemType.Name = "GetSystemType";
            this.GetSystemType.Size = new System.Drawing.Size(100, 27);
            this.GetSystemType.TabIndex = 9;
            this.GetSystemType.Text = "Get";
            this.GetSystemType.UseVisualStyleBackColor = true;
            this.GetSystemType.Click += new System.EventHandler(this.GetSystemType_Click);
            // 
            // labelSysType
            // 
            this.labelSysType.AutoSize = true;
            this.labelSysType.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(134)));
            this.labelSysType.Location = new System.Drawing.Point(135, 61);
            this.labelSysType.Name = "labelSysType";
            this.labelSysType.Size = new System.Drawing.Size(0, 16);
            this.labelSysType.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(135, 441);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(0, 16);
            this.label4.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(136, 355);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(0, 16);
            this.label3.TabIndex = 1;
            // 
            // LabelSysSn
            // 
            this.LabelSysSn.AutoSize = true;
            this.LabelSysSn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(134)));
            this.LabelSysSn.Location = new System.Drawing.Point(140, 175);
            this.LabelSysSn.Name = "LabelSysSn";
            this.LabelSysSn.Size = new System.Drawing.Size(0, 16);
            this.LabelSysSn.TabIndex = 1;
            // 
            // TabPageFw
            // 
            this.TabPageFw.BackColor = System.Drawing.Color.White;
            this.TabPageFw.Controls.Add(this.label11);
            this.TabPageFw.Controls.Add(this.txtFwInfo);
            this.TabPageFw.Controls.Add(this.label10);
            this.TabPageFw.Controls.Add(this.cmbPCBName);
            this.TabPageFw.Controls.Add(this.groupBox2);
            this.TabPageFw.Controls.Add(this.progressBar);
            this.TabPageFw.Controls.Add(this.groupBox1);
            this.TabPageFw.Location = new System.Drawing.Point(4, 24);
            this.TabPageFw.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.TabPageFw.Name = "TabPageFw";
            this.TabPageFw.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.TabPageFw.Size = new System.Drawing.Size(961, 534);
            this.TabPageFw.TabIndex = 1;
            this.TabPageFw.Text = "FW Info";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(41, 479);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(32, 16);
            this.label11.TabIndex = 17;
            this.label11.Text = "Info:";
            // 
            // txtFwInfo
            // 
            this.txtFwInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(134)));
            this.txtFwInfo.Location = new System.Drawing.Point(99, 475);
            this.txtFwInfo.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtFwInfo.Name = "txtFwInfo";
            this.txtFwInfo.ReadOnly = true;
            this.txtFwInfo.Size = new System.Drawing.Size(831, 21);
            this.txtFwInfo.TabIndex = 16;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.label10.Location = new System.Drawing.Point(18, 429);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(66, 16);
            this.label10.TabIndex = 13;
            this.label10.Text = "Progress:";
            // 
            // cmbPCBName
            // 
            this.cmbPCBName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPCBName.FormattingEnabled = true;
            this.cmbPCBName.Items.AddRange(new object[] {
            "Central Control",
            "OPMI Control",
            "CCU Gateway"});
            this.cmbPCBName.Location = new System.Drawing.Point(685, 335);
            this.cmbPCBName.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cmbPCBName.Name = "cmbPCBName";
            this.cmbPCBName.Size = new System.Drawing.Size(120, 23);
            this.cmbPCBName.TabIndex = 0;
            this.cmbPCBName.Visible = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnDownload);
            this.groupBox2.Controls.Add(this.btnBrowseHex);
            this.groupBox2.Controls.Add(this.txtHexFilePath);
            this.groupBox2.Location = new System.Drawing.Point(471, 45);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox2.Size = new System.Drawing.Size(455, 225);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Download";
            // 
            // btnDownload
            // 
            this.btnDownload.Location = new System.Drawing.Point(284, 87);
            this.btnDownload.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnDownload.Name = "btnDownload";
            this.btnDownload.Size = new System.Drawing.Size(149, 27);
            this.btnDownload.TabIndex = 4;
            this.btnDownload.Text = "Download";
            this.btnDownload.UseVisualStyleBackColor = true;
            this.btnDownload.Click += new System.EventHandler(this.btnDownload_Click);
            // 
            // btnBrowseHex
            // 
            this.btnBrowseHex.Location = new System.Drawing.Point(389, 39);
            this.btnBrowseHex.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnBrowseHex.Name = "btnBrowseHex";
            this.btnBrowseHex.Size = new System.Drawing.Size(44, 27);
            this.btnBrowseHex.TabIndex = 2;
            this.btnBrowseHex.Text = "...";
            this.btnBrowseHex.UseVisualStyleBackColor = true;
            this.btnBrowseHex.Click += new System.EventHandler(this.btnBrowseHex_Click);
            // 
            // txtHexFilePath
            // 
            this.txtHexFilePath.Location = new System.Drawing.Point(27, 42);
            this.txtHexFilePath.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtHexFilePath.Name = "txtHexFilePath";
            this.txtHexFilePath.ReadOnly = true;
            this.txtHexFilePath.Size = new System.Drawing.Size(346, 21);
            this.txtHexFilePath.TabIndex = 1;
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(100, 427);
            this.progressBar.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(830, 24);
            this.progressBar.TabIndex = 14;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.GetVersion);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.TextboxCurCentral);
            this.groupBox1.Controls.Add(this.TextboxCurOpmi);
            this.groupBox1.Controls.Add(this.TextboxCurCCU);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(134)));
            this.groupBox1.Location = new System.Drawing.Point(30, 45);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Size = new System.Drawing.Size(414, 225);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Current  Version";
            // 
            // GetVersion
            // 
            this.GetVersion.Location = new System.Drawing.Point(233, 180);
            this.GetVersion.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.GetVersion.Name = "GetVersion";
            this.GetVersion.Size = new System.Drawing.Size(156, 27);
            this.GetVersion.TabIndex = 2;
            this.GetVersion.Text = "Get Version";
            this.GetVersion.UseVisualStyleBackColor = true;
            this.GetVersion.Click += new System.EventHandler(this.GetVersion_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(23, 136);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(95, 16);
            this.label6.TabIndex = 0;
            this.label6.Text = "CCU Gateway:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(25, 91);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(89, 16);
            this.label5.TabIndex = 0;
            this.label5.Text = "OPMI Control:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(14, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Central Control:";
            // 
            // TextboxCurCentral
            // 
            this.TextboxCurCentral.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(134)));
            this.TextboxCurCentral.Location = new System.Drawing.Point(141, 42);
            this.TextboxCurCentral.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.TextboxCurCentral.Name = "TextboxCurCentral";
            this.TextboxCurCentral.ReadOnly = true;
            this.TextboxCurCentral.Size = new System.Drawing.Size(248, 21);
            this.TextboxCurCentral.TabIndex = 1;
            // 
            // TextboxCurOpmi
            // 
            this.TextboxCurOpmi.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(134)));
            this.TextboxCurOpmi.Location = new System.Drawing.Point(141, 87);
            this.TextboxCurOpmi.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.TextboxCurOpmi.Name = "TextboxCurOpmi";
            this.TextboxCurOpmi.ReadOnly = true;
            this.TextboxCurOpmi.Size = new System.Drawing.Size(248, 21);
            this.TextboxCurOpmi.TabIndex = 1;
            // 
            // TextboxCurCCU
            // 
            this.TextboxCurCCU.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(134)));
            this.TextboxCurCCU.Location = new System.Drawing.Point(141, 134);
            this.TextboxCurCCU.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.TextboxCurCCU.Name = "TextboxCurCCU";
            this.TextboxCurCCU.ReadOnly = true;
            this.TextboxCurCCU.Size = new System.Drawing.Size(248, 21);
            this.TextboxCurCCU.TabIndex = 1;
            // 
            // TabPageMaintenance
            // 
            this.TabPageMaintenance.BackColor = System.Drawing.Color.White;
            this.TabPageMaintenance.Controls.Add(this.label15);
            this.TabPageMaintenance.Controls.Add(this.MaintenanceInfo);
            this.TabPageMaintenance.Controls.Add(this.groupBox10);
            this.TabPageMaintenance.Controls.Add(this.groupBox9);
            this.TabPageMaintenance.Controls.Add(this.dateTimePickerLast);
            this.TabPageMaintenance.Location = new System.Drawing.Point(4, 24);
            this.TabPageMaintenance.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.TabPageMaintenance.Name = "TabPageMaintenance";
            this.TabPageMaintenance.Size = new System.Drawing.Size(961, 534);
            this.TabPageMaintenance.TabIndex = 2;
            this.TabPageMaintenance.Text = "Maintenance";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(41, 479);
            this.label15.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(32, 16);
            this.label15.TabIndex = 9;
            this.label15.Text = "Info:";
            // 
            // MaintenanceInfo
            // 
            this.MaintenanceInfo.Location = new System.Drawing.Point(99, 475);
            this.MaintenanceInfo.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaintenanceInfo.Name = "MaintenanceInfo";
            this.MaintenanceInfo.Size = new System.Drawing.Size(816, 21);
            this.MaintenanceInfo.TabIndex = 8;
            // 
            // groupBox10
            // 
            this.groupBox10.Controls.Add(this.GetnextServiceDate);
            this.groupBox10.Controls.Add(this.GetLastServiceDate);
            this.groupBox10.Controls.Add(this.label8);
            this.groupBox10.Controls.Add(this.label9);
            this.groupBox10.Controls.Add(this.TextboxNextService);
            this.groupBox10.Controls.Add(this.TextboxLastService);
            this.groupBox10.Location = new System.Drawing.Point(40, 264);
            this.groupBox10.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox10.Name = "groupBox10";
            this.groupBox10.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox10.Size = new System.Drawing.Size(875, 159);
            this.groupBox10.TabIndex = 8;
            this.groupBox10.TabStop = false;
            this.groupBox10.Text = "Get Service Date";
            // 
            // GetnextServiceDate
            // 
            this.GetnextServiceDate.Location = new System.Drawing.Point(632, 98);
            this.GetnextServiceDate.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.GetnextServiceDate.Name = "GetnextServiceDate";
            this.GetnextServiceDate.Size = new System.Drawing.Size(116, 27);
            this.GetnextServiceDate.TabIndex = 7;
            this.GetnextServiceDate.Text = "Get";
            this.GetnextServiceDate.UseVisualStyleBackColor = true;
            this.GetnextServiceDate.Click += new System.EventHandler(this.GetnextServiceDate_Click);
            // 
            // GetLastServiceDate
            // 
            this.GetLastServiceDate.Location = new System.Drawing.Point(632, 46);
            this.GetLastServiceDate.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.GetLastServiceDate.Name = "GetLastServiceDate";
            this.GetLastServiceDate.Size = new System.Drawing.Size(116, 27);
            this.GetLastServiceDate.TabIndex = 6;
            this.GetLastServiceDate.Text = "Get";
            this.GetLastServiceDate.UseVisualStyleBackColor = true;
            this.GetLastServiceDate.Click += new System.EventHandler(this.GetLastServiceDate_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(134)));
            this.label8.Location = new System.Drawing.Point(91, 55);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(211, 16);
            this.label8.TabIndex = 1;
            this.label8.Text = "Last Service Date (MM/DD/YYYY)";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(134)));
            this.label9.Location = new System.Drawing.Point(91, 102);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(213, 16);
            this.label9.TabIndex = 1;
            this.label9.Text = "Next Service Date (MM/DD/YYYY)";
            // 
            // TextboxNextService
            // 
            this.TextboxNextService.Location = new System.Drawing.Point(428, 96);
            this.TextboxNextService.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.TextboxNextService.Name = "TextboxNextService";
            this.TextboxNextService.Size = new System.Drawing.Size(140, 21);
            this.TextboxNextService.TabIndex = 5;
            // 
            // TextboxLastService
            // 
            this.TextboxLastService.Location = new System.Drawing.Point(428, 50);
            this.TextboxLastService.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.TextboxLastService.Name = "TextboxLastService";
            this.TextboxLastService.Size = new System.Drawing.Size(140, 21);
            this.TextboxLastService.TabIndex = 5;
            // 
            // groupBox9
            // 
            this.groupBox9.Controls.Add(this.SetNextServiceDate);
            this.groupBox9.Controls.Add(this.CheckboxMaintenanceEn);
            this.groupBox9.Controls.Add(this.label7);
            this.groupBox9.Controls.Add(this.RadiobuttonByDate);
            this.groupBox9.Controls.Add(this.RadiobuttonByInterval);
            this.groupBox9.Controls.Add(this.DateTimePickerService);
            this.groupBox9.Controls.Add(this.ComboboxInterval);
            this.groupBox9.Location = new System.Drawing.Point(40, 27);
            this.groupBox9.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox9.Size = new System.Drawing.Size(875, 210);
            this.groupBox9.TabIndex = 7;
            this.groupBox9.TabStop = false;
            this.groupBox9.Text = "Set Service Date";
            // 
            // SetNextServiceDate
            // 
            this.SetNextServiceDate.Enabled = false;
            this.SetNextServiceDate.Location = new System.Drawing.Point(632, 151);
            this.SetNextServiceDate.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.SetNextServiceDate.Name = "SetNextServiceDate";
            this.SetNextServiceDate.Size = new System.Drawing.Size(116, 27);
            this.SetNextServiceDate.TabIndex = 5;
            this.SetNextServiceDate.Text = "Set";
            this.SetNextServiceDate.UseVisualStyleBackColor = true;
            this.SetNextServiceDate.Click += new System.EventHandler(this.SetNextServiceDate_Click);
            // 
            // CheckboxMaintenanceEn
            // 
            this.CheckboxMaintenanceEn.AutoSize = true;
            this.CheckboxMaintenanceEn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(134)));
            this.CheckboxMaintenanceEn.Location = new System.Drawing.Point(53, 40);
            this.CheckboxMaintenanceEn.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.CheckboxMaintenanceEn.Name = "CheckboxMaintenanceEn";
            this.CheckboxMaintenanceEn.Size = new System.Drawing.Size(222, 20);
            this.CheckboxMaintenanceEn.TabIndex = 0;
            this.CheckboxMaintenanceEn.Text = "Enable Maintenance Notification";
            this.CheckboxMaintenanceEn.UseVisualStyleBackColor = true;
            this.CheckboxMaintenanceEn.Click += new System.EventHandler(this.CheckboxMaintenanceEn_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(134)));
            this.label7.Location = new System.Drawing.Point(50, 98);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(57, 16);
            this.label7.TabIndex = 1;
            this.label7.Text = "Service:";
            // 
            // RadiobuttonByDate
            // 
            this.RadiobuttonByDate.AutoSize = true;
            this.RadiobuttonByDate.Enabled = false;
            this.RadiobuttonByDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(134)));
            this.RadiobuttonByDate.Location = new System.Drawing.Point(162, 98);
            this.RadiobuttonByDate.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.RadiobuttonByDate.Name = "RadiobuttonByDate";
            this.RadiobuttonByDate.Size = new System.Drawing.Size(77, 20);
            this.RadiobuttonByDate.TabIndex = 2;
            this.RadiobuttonByDate.TabStop = true;
            this.RadiobuttonByDate.Text = "By Date";
            this.RadiobuttonByDate.UseVisualStyleBackColor = true;
            this.RadiobuttonByDate.CheckedChanged += new System.EventHandler(this.RadiobuttonByDate_CheckedChanged);
            // 
            // RadiobuttonByInterval
            // 
            this.RadiobuttonByInterval.AutoSize = true;
            this.RadiobuttonByInterval.Enabled = false;
            this.RadiobuttonByInterval.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(134)));
            this.RadiobuttonByInterval.Location = new System.Drawing.Point(430, 98);
            this.RadiobuttonByInterval.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.RadiobuttonByInterval.Name = "RadiobuttonByInterval";
            this.RadiobuttonByInterval.Size = new System.Drawing.Size(91, 20);
            this.RadiobuttonByInterval.TabIndex = 2;
            this.RadiobuttonByInterval.TabStop = true;
            this.RadiobuttonByInterval.Text = "By Interval";
            this.RadiobuttonByInterval.UseVisualStyleBackColor = true;
            this.RadiobuttonByInterval.CheckedChanged += new System.EventHandler(this.RadiobuttonByInterval_CheckedChanged);
            // 
            // DateTimePickerService
            // 
            this.DateTimePickerService.Enabled = false;
            this.DateTimePickerService.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.DateTimePickerService.Location = new System.Drawing.Point(162, 153);
            this.DateTimePickerService.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.DateTimePickerService.Name = "DateTimePickerService";
            this.DateTimePickerService.Size = new System.Drawing.Size(200, 21);
            this.DateTimePickerService.TabIndex = 4;
            // 
            // ComboboxInterval
            // 
            this.ComboboxInterval.Enabled = false;
            this.ComboboxInterval.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(134)));
            this.ComboboxInterval.FormattingEnabled = true;
            this.ComboboxInterval.Items.AddRange(new object[] {
            "6 months",
            "12 months",
            "24 months",
            "36 months"});
            this.ComboboxInterval.Location = new System.Drawing.Point(428, 151);
            this.ComboboxInterval.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ComboboxInterval.Name = "ComboboxInterval";
            this.ComboboxInterval.Size = new System.Drawing.Size(140, 23);
            this.ComboboxInterval.TabIndex = 3;
            this.ComboboxInterval.SelectedIndexChanged += new System.EventHandler(this.ComboboxInterval_SelectedIndexChanged);
            // 
            // dateTimePickerLast
            // 
            this.dateTimePickerLast.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(134)));
            this.dateTimePickerLast.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePickerLast.Location = new System.Drawing.Point(683, 2);
            this.dateTimePickerLast.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dateTimePickerLast.MinDate = new System.DateTime(2010, 1, 1, 0, 0, 0, 0);
            this.dateTimePickerLast.Name = "dateTimePickerLast";
            this.dateTimePickerLast.Size = new System.Drawing.Size(141, 21);
            this.dateTimePickerLast.TabIndex = 6;
            this.dateTimePickerLast.Visible = false;
            // 
            // TabPageLog
            // 
            this.TabPageLog.BackColor = System.Drawing.Color.White;
            this.TabPageLog.Controls.Add(this.logFiles1);
            this.TabPageLog.Location = new System.Drawing.Point(4, 24);
            this.TabPageLog.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.TabPageLog.Name = "TabPageLog";
            this.TabPageLog.Size = new System.Drawing.Size(961, 534);
            this.TabPageLog.TabIndex = 3;
            this.TabPageLog.Text = "Log Files";
            // 
            // logFiles1
            // 
            this.logFiles1.AutoSize = true;
            this.logFiles1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.logFiles1.Location = new System.Drawing.Point(25, 12);
            this.logFiles1.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.logFiles1.Name = "logFiles1";
            this.logFiles1.Size = new System.Drawing.Size(683, 395);
            this.logFiles1.TabIndex = 0;
            // 
            // TabPageParameter
            // 
            this.TabPageParameter.Controls.Add(this.parameters);
            this.TabPageParameter.Location = new System.Drawing.Point(4, 24);
            this.TabPageParameter.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.TabPageParameter.Name = "TabPageParameter";
            this.TabPageParameter.Size = new System.Drawing.Size(961, 534);
            this.TabPageParameter.TabIndex = 4;
            this.TabPageParameter.Text = "Parameter";
            this.TabPageParameter.UseVisualStyleBackColor = true;
            // 
            // parameters
            // 
            this.parameters.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(134)));
            this.parameters.Location = new System.Drawing.Point(13, 5);
            this.parameters.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.parameters.Name = "parameters";
            this.parameters.Size = new System.Drawing.Size(955, 537);
            this.parameters.TabIndex = 0;
            // 
            // TabPageLicense
            // 
            this.TabPageLicense.Controls.Add(this.license2);
            this.TabPageLicense.Location = new System.Drawing.Point(4, 24);
            this.TabPageLicense.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.TabPageLicense.Name = "TabPageLicense";
            this.TabPageLicense.Size = new System.Drawing.Size(961, 534);
            this.TabPageLicense.TabIndex = 4;
            this.TabPageLicense.Text = "License";
            this.TabPageLicense.UseVisualStyleBackColor = true;
            // 
            // license2
            // 
            this.license2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(134)));
            this.license2.Location = new System.Drawing.Point(1, 8);
            this.license2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.license2.Name = "license2";
            this.license2.serviceLicensePath = null;
            this.license2.Size = new System.Drawing.Size(955, 497);
            this.license2.TabIndex = 0;
            // 
            // tabAbout
            // 
            this.tabAbout.Controls.Add(this.groupBox3);
            this.tabAbout.Location = new System.Drawing.Point(4, 24);
            this.tabAbout.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tabAbout.Name = "tabAbout";
            this.tabAbout.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tabAbout.Size = new System.Drawing.Size(961, 534);
            this.tabAbout.TabIndex = 5;
            this.tabAbout.Text = "About";
            this.tabAbout.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label17);
            this.groupBox3.Controls.Add(this.label18);
            this.groupBox3.Controls.Add(this.label19);
            this.groupBox3.Controls.Add(this.label20);
            this.groupBox3.Controls.Add(this.tbVersion);
            this.groupBox3.Controls.Add(this.label21);
            this.groupBox3.Controls.Add(this.textBox1);
            this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(134)));
            this.groupBox3.Location = new System.Drawing.Point(96, 77);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox3.Size = new System.Drawing.Size(765, 285);
            this.groupBox3.TabIndex = 8;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "EXTARO Service Tooling Software";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Image = ((System.Drawing.Image)(resources.GetObject("label17.Image")));
            this.label17.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.label17.Location = new System.Drawing.Point(13, 225);
            this.label17.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(595, 32);
            this.label17.TabIndex = 8;
            this.label17.Text = "      This software is intended to be used only by service personnel authorized b" +
    "y Carl Zeiss Meditec.\nDistribution of this software to non-authorized persons or" +
    " parties is strictly prohibited.";
            this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(239, 153);
            this.label18.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(269, 16);
            this.label18.TabIndex = 7;
            this.label18.Text = "Carl Zeiss China Innovation and R&&D Center";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(161, 81);
            this.label19.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(57, 16);
            this.label19.TabIndex = 1;
            this.label19.Text = "Version:";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(143, 153);
            this.label20.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(75, 16);
            this.label20.TabIndex = 6;
            this.label20.Text = "Developer:";
            // 
            // tbVersion
            // 
            this.tbVersion.Enabled = false;
            this.tbVersion.Location = new System.Drawing.Point(239, 76);
            this.tbVersion.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tbVersion.Name = "tbVersion";
            this.tbVersion.Size = new System.Drawing.Size(303, 21);
            this.tbVersion.TabIndex = 0;
            this.tbVersion.Text = "2.0";//modified by Qin yunhe 20200321
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(123, 117);
            this.label21.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(95, 16);
            this.label21.TabIndex = 3;
            this.label21.Text = "Release Date:";
            // 
            // textBox1
            // 
            this.textBox1.Enabled = false;
            this.textBox1.Location = new System.Drawing.Point(239, 114);
            this.textBox1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(303, 21);
            this.textBox1.TabIndex = 4;
            this.textBox1.Text = "2020-03-20";//modified by Qin yunhe 20200321
            // 
            // MainMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1029, 699);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.Connectivity);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximizeBox = false;
            this.Name = "MainMenu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "EXTARO Service Tool";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainMenu_FormClosing);
            this.Load += new System.EventHandler(this.MainMenu_Load);
            this.Connectivity.ResumeLayout(false);
            this.Connectivity.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.TabPageSysInfo.ResumeLayout(false);
            this.TabPageSysInfo.PerformLayout();
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.TabPageFw.ResumeLayout(false);
            this.TabPageFw.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.TabPageMaintenance.ResumeLayout(false);
            this.TabPageMaintenance.PerformLayout();
            this.groupBox10.ResumeLayout(false);
            this.groupBox10.PerformLayout();
            this.groupBox9.ResumeLayout(false);
            this.groupBox9.PerformLayout();
            this.TabPageLog.ResumeLayout(false);
            this.TabPageLog.PerformLayout();
            this.TabPageParameter.ResumeLayout(false);
            this.TabPageLicense.ResumeLayout(false);
            this.tabAbout.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox Connectivity;
        private System.Windows.Forms.Label LablePort;
        private System.Windows.Forms.Button ButtonClosePort;
        private System.Windows.Forms.Button ButtonOpenPort;
        private System.Windows.Forms.ComboBox ComboBoxPortName;
        private System.IO.Ports.SerialPort serialPort1;
        private System.Windows.Forms.OpenFileDialog OpenFileDialogFW;
        private System.Windows.Forms.TextBox TextboxSerial;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage TabPageSysInfo;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label LabelSysSn;
        private System.Windows.Forms.TextBox TextboxFirstTime;
        private System.Windows.Forms.TextBox TextboxLedTimeRGB;
        private System.Windows.Forms.TextBox TextboxTime;
        private System.Windows.Forms.TabPage TabPageFw;
        private System.Windows.Forms.TabPage TabPageMaintenance;
        private System.Windows.Forms.TabPage TabPageLog;
        private System.Windows.Forms.TabPage TabPageLicense;
        private System.Windows.Forms.TabPage TabPageParameter;
        private System.Windows.Forms.TextBox TextboxCurCCU;
        private System.Windows.Forms.TextBox TextboxCurOpmi;
        private System.Windows.Forms.TextBox TextboxCurCentral;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox ComboboxInterval;
        private System.Windows.Forms.RadioButton RadiobuttonByInterval;
        private System.Windows.Forms.RadioButton RadiobuttonByDate;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.CheckBox CheckboxMaintenanceEn;
        private System.Windows.Forms.DateTimePicker DateTimePickerService;
        private System.Windows.Forms.TextBox TextboxNextService;
        private System.Windows.Forms.TextBox TextboxLastService;
        private System.Windows.Forms.Button ButtonSettime;
        private System.Windows.Forms.DateTimePicker DateTimePickerSys;
        private System.Windows.Forms.Timer TimerSecond;
        private System.Windows.Forms.Button ButtonFirstDate;
        private System.Windows.Forms.DateTimePicker DateTimePickerFirst;
        private System.Windows.Forms.Label labelSysType;
        private System.Windows.Forms.DateTimePicker dateTimePickerLast;

        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox TextboxSysType;
        private System.Windows.Forms.Button GetSystemType;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Button ModifyID;
        private System.Windows.Forms.TextBox TextboxSysIDGet;
        private System.Windows.Forms.Button GetSystemID;
        private System.Windows.Forms.Button SystemTypeSet;
        private System.Windows.Forms.ComboBox SystemTypeList;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.TextBox TextboxSysIDSet;
        private System.Windows.Forms.Button GetLedLifeRGB;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox TextboxLedTimeVlight;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button GetLifeLedVlight;
        private System.Windows.Forms.DateTimePicker dateSystemTimePicker;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox SystemInfo;
        private System.Windows.Forms.Button GetFirstInstallDate;
        private System.Windows.Forms.Button GetSystemTime;
        private System.Windows.Forms.Button GetVersion;
        private System.Windows.Forms.GroupBox groupBox9;
        private System.Windows.Forms.GroupBox groupBox10;
        private System.Windows.Forms.Button GetnextServiceDate;
        private System.Windows.Forms.Button GetLastServiceDate;
        private System.Windows.Forms.Button SetNextServiceDate;
        private System.Windows.Forms.TextBox MaintenanceInfo;
        private System.Windows.Forms.Label label15;
        private License license2;
        private Parameters parameters;
        private System.Windows.Forms.TabPage tabAbout;
        private LogFiles logFiles1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox cmbPCBName;
        private System.Windows.Forms.TextBox txtHexFilePath;
        private System.Windows.Forms.Button btnBrowseHex;
        private System.Windows.Forms.Button btnDownload;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtFwInfo;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.TextBox tbVersion;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.TextBox textBox1;
    }
}