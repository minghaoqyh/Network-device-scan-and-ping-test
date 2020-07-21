using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using System.Text.RegularExpressions;
using Timer = System.Timers.Timer;
using System.Management;
using System.IO;
using System.Threading;
using System.Collections;


namespace service_tool
{
    public partial class MainMenu : Form
    {
        /* define the strings for serial communication here */

        // 1. begin and end character
        public static string STX = Char.ConvertFromUtf32(2);
        public static string ETX = Char.ConvertFromUtf32(3);

        // 2. commands
        public string CMD_SERV_VN = "$SERV:VN$"; //20200321

        public string CMD_SERV_ON = "$SERV:ON$";
        public string CMD_SERV_OFF = "$SERV:OFF$";
        public string CMD_SSN_GET = "$SSN:GET$";
        public string CMD_SSN_SET = "$SSN:SET:";
        public string CMD_STYPE_GET = "$STYPE:GET$";
        public string CMD_STYPE_SET = "$STYPE:SET:";
        public string CMD_STYPE_SET1 = "02$";
        //string CMD_SFEAT_GET = "$SFEAT:GET$";
        //string CMD_SFEAT_SET = "$SFEAT:SET:";
        //string CMD_SFEAT_SET1 = "000001$";
        public string CMD_VER_ICAN = "$VER:ICAN:";
        public string CMD_VER_ICAN1 = "01$";
        //string CMD_VER_ECAN = "$VER:ECAN:";
        //string CMD_VER_ECAN1 = "02$";
        //string CMD_SSTA_GET = "$SSTA:GET$";
        //string CMD_ILLU_ON = "$ILLU:ON:00$";
        //string CMD_ILLU_OFF = "$ILLU:OFF:01$";
        //string CMD_ILLU_INT_SET = "$ILLU:INT:SET:";
        //string CMD_ILLU_INT_SET1 = "00:30$";
        //string CMD_ILLU_CLR_SET = "$ILLU:CLR:SET:";
        //string CMD_ILLU_CLR_SET1 = "00:9A019A01$";
        //string CMD_MODE_SET = "$MODE:SET:";
        //string CMD_MODE_SET1 = "05$";
        //string CMD_FILT_SET = "$FILT:SET:";
        //string CMD_FILT_SET1 = "01$";
        public string CMD_STIME_GET = "$STIME:GET$";
        public string CMD_STIME_SET = "$STIME:SET:";
        public string CMD_SID_GET = "$SID:GET$";
        public string CMD_SID_SET = "$SID:SET:";
        public string CMD_ILLUM_RGB_GET = "$ILLU:LIFE:RGB$";
        public string CMD_ILLUM_V_GET = "$ILLU:LIFE:V$";
        public string CMD_MDN_GET = "$MDN:GET$";
#if true
        public string CMD_MDN_SET = "$MDN:SET:";
#else
        string CMD_MDN_SET = "$NMD:SET:";
#endif
        public string CMD_MDN_SET1 = "100505$";
        public string CMD_MDP_GET = "$MDP:GET$";
        public string CMD_MD_NOTIFY_ON = "$MDNOTIFY:ON$";
        public string CMD_MD_NOTIFY_OFF = "$MDNOTIFY:OFF$";

        // 3. response strings
        public string RSP_OK = "$OK$";
        public string OK_HDR = "$OK:";
        public static string NG_BADPAR = "$NG:BADPAR$";
        public static string NG_HWERR = "$NG:HWERR$";
        public static string NG_BUSY = "$NG:BUSY$";
        /* end definition of serial communication commands */

        string formatYear = "yyyy/MM/dd";
        DateTime sysTime = DateTime.MinValue;

        //private string CENTRAL = "UNO3CentralCtrl_CORE";
        //private string OPMI = "UNO3OPMICtrl_CORE";
        //private string CCU = "UNO3CCUGateway_CORE";
        //private string CENTRAL_FILE_FORMAT = "UNO3CentralCtrl_CORE.xx.xx.xx.APP.xx.xx.xx.hex";
        //private string OPMI_FILE_FORMAT = "UNO3OPMICtrl_CORE.xx.xx.xx.APP.xx.xx.xx.hex";
        //private string CCU_FILE_FORMAT = "UNO3CCUGateway_CORE.xx.xx.xx.APP.xx.xx.xx.hex";

        string LogFileName = "DownloadLog.txt";
        string downloadTempFolder = System.Environment.CurrentDirectory + "\\DownloadFiles";

        public MainMenu()
        {
            InitializeComponent();
            getAvailablePorts();
            cmbPCBName.SelectedIndex = 0;//20180222
            this.timerPerSecond = new Timer();
            this.timerPerSecond.Interval = 1000;    // 1 Second
            this.timerPerSecond.Elapsed += timerPerSecond_Elapsed;

            //this.timerPerSecond.Start();
#if false//20170323
            SystemTypeList.Items.Add("Peak");
            SystemTypeList.Items.Add("Star");
            SystemTypeList.Items.Add("Panorama");
            SystemTypeList.Items.Add("System type not available");
            SystemTypeList.SelectedIndex = 0;
#else
            SystemTypeList.Items.Add("EXTARO 300 Dental");
            SystemTypeList.Items.Add("EXTARO 300 ENT");
            SystemTypeList.Items.Add("System type not available");
            SystemTypeList.SelectedIndex = 0;
#endif
        }


        private void timerPerSecond_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            string textTime = TextboxTime.Text;
            if (DateTime.TryParse(textTime, out sysTime))
            {
                sysTime = sysTime.AddSeconds(1);
            }


            DateTimePickerSys.BeginInvoke(new Action(() =>
            {
                DateTimePickerSys.Value = DateTimePickerSys.Value.AddSeconds(1);
            }));

            TextboxTime.BeginInvoke(new Action(() =>
            {
                if (sysTime != DateTime.MinValue)
                    TextboxTime.Text = sysTime.ToString("yyyy/MM/dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
            }));
        }
        private void ButtonOpenPort_Click(object sender, EventArgs e)
        {
            try
            {
                IsCommandSending = false;
                if (ComboBoxPortName.Text.Equals(""))
                {
                    MessageBox.Show("Please choose port!", "Warning!");
                    return;
                }
                string cmd;
                string response;
                try
                {
                    serialPort1.PortName = ComboBoxPortName.Text;
                    serialPort1.BaudRate = 115200;
                    serialPort1.ReadTimeout = 3000;
                    // now set service mode to on
                    serialPort1.Open();

                    //check version
                     cmd = STX + CMD_SERV_VN + ETX;//"$SERV:VN$"
                    serialPort1.Write(cmd);
                    response = serialPort1.ReadTo(ETX);

                    cmd = STX + CMD_SERV_ON + ETX;
                    serialPort1.Write(cmd);
                    //response = serialPort1.ReadExisting();
                    response = serialPort1.ReadTo(ETX);
                    response = response.TrimStart('\u0002');
                    if (response != RSP_OK)
                    {
                        ;
                    }
                    ButtonOpenPort.Enabled = false;
                    ButtonClosePort.Enabled = true;
                    TextboxSerial.Text = "Serial port connected.";
                    license2.EnableControls(true);
                    ButtonClosePort.Enabled = true;

                    logFiles1.LoadOperLog();//upload the Operlog while the port open 20200321
                    logFiles1.LoadErrorLog();
                }
                catch
                {
                    //MessageBox.Show(ex.Message);
                    MessageBox.Show("Operation time out!");
                    serialPort1.Close();
                }
                return;

#if false
                // get system serial number
                cmd = STX + CMD_SSN_GET + ETX;
                serialPort1.Write(cmd);
                response = serialPort1.ReadTo(ETX);
                response = response.TrimStart((STX + OK_HDR).ToCharArray());
                response = response.TrimEnd('$');
                TextboxSysIDGet.Text = response;


                // set system type
                cmd = STX + CMD_STYPE_SET + CMD_STYPE_SET1 + ETX;
                serialPort1.Write(cmd);
                response = serialPort1.ReadTo(ETX);
                response = response.Substring(response.IndexOf(STX) + 1);


                // get system type   
                cmd = STX + CMD_STYPE_GET + ETX;
                serialPort1.Write(cmd);
                response = serialPort1.ReadTo(ETX);
                response = response.Substring(response.IndexOf(STX) + 1);
                if (response == OK_HDR + "00$")
                    TextboxSysType.Text = "System type not available";
#if false //20170323
                if (response == OK_HDR + "01$")
                    TextboxSysType.Text = "Peak";
                if (response == OK_HDR + "02$")
                    TextboxSysType.Text = "Star";
                if (response == OK_HDR + "03$")
                    TextboxSysType.Text = "Panorama";
#else
                if (response == OK_HDR + "01$")
                    TextboxSysType.Text = "EXTARO 300 Dental";
                if (response == OK_HDR + "02$")
                    TextboxSysType.Text = "EXTARO 300 ENT";
#endif

                // get feature list
                cmd = STX + CMD_SFEAT_GET + ETX;
                serialPort1.Write(cmd);
                response = serialPort1.ReadTo(ETX);
                response = response.Substring(response.IndexOf(STX) + 1);


                // set feature list
                cmd = STX + CMD_SFEAT_SET + CMD_SFEAT_SET1 + ETX;
                serialPort1.Write(cmd);
                response = serialPort1.ReadTo(ETX);
                response = response.TrimStart('\u0002');


                // get FW version of node on the internal CAN bus
                cmd = STX + CMD_VER_ICAN + CMD_VER_ICAN1 + ETX;
                serialPort1.Write(cmd);
                response = serialPort1.ReadTo(ETX);
                response = response.TrimStart('\u0002');


                // get FW version of node on the external CAN bus
                cmd = STX + CMD_VER_ECAN + CMD_VER_ECAN1 + ETX;
                serialPort1.Write(cmd);
                response = serialPort1.ReadTo(ETX);
                response = response.TrimStart('\u0002');


                // get system status
                cmd = STX + CMD_SSTA_GET + ETX;
                serialPort1.Write(cmd);
                response = serialPort1.ReadTo(ETX);
                response = response.TrimStart('\u0002');


                // set illumination on
                cmd = STX + CMD_ILLU_ON + ETX;
                serialPort1.Write(cmd);
                response = serialPort1.ReadTo(ETX);
                response = response.TrimStart('\u0002');


                // set illumination oFF
                cmd = STX + CMD_ILLU_OFF + ETX;
                serialPort1.Write(cmd);
                response = serialPort1.ReadTo(ETX);
                response = response.TrimStart('\u0002');


                // set illumination intensity
                cmd = STX + CMD_ILLU_INT_SET + CMD_ILLU_INT_SET1 + ETX;
                serialPort1.Write(cmd);
                response = serialPort1.ReadTo(ETX);
                response = response.TrimStart('\u0002');


                // set illumination color
                cmd = STX + CMD_ILLU_CLR_SET + CMD_ILLU_CLR_SET1 + ETX;
                serialPort1.Write(cmd);
                response = serialPort1.ReadTo(ETX);
                response = response.TrimStart('\u0002');


                // mode control
                cmd = STX + CMD_MODE_SET + CMD_MODE_SET1 + ETX;
                serialPort1.Write(cmd);
                response = serialPort1.ReadTo(ETX);
                response = response.Substring(response.IndexOf(STX) + 1);


                // direct filter control
                cmd = STX + CMD_FILT_SET + CMD_FILT_SET1 + ETX;
                serialPort1.Write(cmd);
                response = serialPort1.ReadTo(ETX);
                response = response.TrimStart('\u0002');



                // get system time
                string s1;
                string s2;
                string s3;
                string s4;
                string s5;
                string s6;
                string s;
                cmd = STX + CMD_STIME_GET + ETX;
                serialPort1.Write(cmd);
                response = serialPort1.ReadTo(ETX);
                response = response.TrimStart((STX + OK_HDR).ToCharArray());
                response = response.TrimEnd('$');
                s1 = response.Substring(0, 2);
                s2 = response.Substring(2, 2);
                s3 = response.Substring(4, 2);
                s4 = response.Substring(6, 2);
                s5 = response.Substring(8, 2);
                s6 = response.Substring(10, 2);
                uint year1 = Convert.ToUInt32(s1, 16);
                uint year11 = year1 + 2000;
                string year12 = year11 + "";
                uint mouth1 = Convert.ToUInt32(s2, 16);
                string s21 = mouth1 + "";
                string s22 = s21.PadLeft(2, '0');
                uint day1 = Convert.ToUInt32(s3, 16);
                string s31 = day1 + "";
                string s32 = s31.PadLeft(2, '0');
                uint time = Convert.ToUInt32(s4, 16);
                string s41 = time + "";
                string s42 = s41.PadLeft(2, '0');
                uint minute = Convert.ToUInt32(s5, 16);
                string s51 = minute + "";
                string s52 = s51.PadLeft(2, '0');
                uint second = Convert.ToUInt32(s6, 16);
                string s61 = second + "";
                string s62 = s61.PadLeft(2, '0');
                s = year12 + s22 + s32 + s42 + s52 + s62;
                s = s.Insert(4, "/").Insert(7, "/").Insert(10, " ").Insert(13, ":").Insert(16, ":");
                TextboxTime.Text = s;


                // get helios light source RGB used time
                cmd = STX + CMD_ILLUM_RGB_GET + ETX;
                serialPort1.Write(cmd);
                response = serialPort1.ReadTo(ETX);
                response = response.TrimStart((STX + OK_HDR).ToCharArray());
                response = response.TrimEnd('$');
                string ledTime1 = response + "m";



                // get helios light source V-light used time
                cmd = STX + CMD_ILLUM_V_GET + ETX;
                serialPort1.Write(cmd);
                response = serialPort1.ReadTo(ETX);
                response = response.TrimStart((STX + OK_HDR).ToCharArray());
                response = response.TrimEnd('$');
                string ledTime2 = response + "m";
                TextboxLedTimeRGB.Text = ledTime1 + "\r\n" + ledTime2;



                // get system installation date
                cmd = STX + CMD_SID_GET + ETX;
                serialPort1.Write(cmd);
                response = serialPort1.ReadTo(ETX);
                response = response.Substring(response.IndexOf(STX) + 1);
                response = response.TrimStart((STX + OK_HDR).ToCharArray());
                response = response.TrimEnd('$');
                s1 = response.Substring(0, 2);
                s2 = response.Substring(2, 2);
                s3 = response.Substring(4, 2);
                uint year2 = Convert.ToUInt32(s1, 16);
                uint year21 = year2 + 2000;
                string year22 = year21 + "";
                uint mouth2 = Convert.ToUInt32(s2, 16);
                string mouth21 = mouth2 + "";
                string mouth22 = mouth21.PadLeft(2, '0');
                uint day2 = Convert.ToUInt32(s3, 16);
                string day21 = day2 + "";
                string day22 = day21.PadLeft(2, '0');
                s = year22 + mouth22 + day22;
                s = s.Insert(4, "/").Insert(7, "/");
                TextboxFirstTime.Text = s;


                // get next periodic maintenance date
                cmd = STX + CMD_MDN_GET + ETX;
                serialPort1.Write(cmd);
                response = serialPort1.ReadTo(ETX);
                response = response.Substring(response.IndexOf(STX) + 1);

                // set next periodic maintenance date
                cmd = STX + CMD_MDN_SET + CMD_MDN_SET1 + ETX;
                serialPort1.Write(cmd);
                response = serialPort1.ReadTo(ETX);
                response = response.TrimStart('\u0002');
                response = response.TrimEnd('$');


                // get previous maintenance date
                cmd = STX + CMD_MDP_GET + ETX;
                serialPort1.Write(cmd);
                response = serialPort1.ReadTo(ETX);
                response = response.TrimStart((STX + OK_HDR).ToCharArray());
                response = response.TrimEnd('$');


                // post Sytem Info here
                // TextboxSysSN.Text = "31415720160527";
                //TextboxChgSN.Text = "2234231131752";
                //TextboxLedTime.Text = "120 hours";
                // TextboxFirstTime.Text = "01/31/2012";

                // read FW version()
                TextboxCurCentral.Text = "CORE.1.0.2r.APP.2.0.3d";
                TextboxCurOpmi.Text = "CORE.1.0.2r.APP.2.0.3d";
                TextboxCurCCU.Text = "CORE.1.0.2r.APP.2.0.3d";

                // read Maintenance Info
                TextboxLastService.Text = "05/05/2015";
#endif
            }
            catch (UnauthorizedAccessException)
            {
                TextboxSerial.Text = "Unauthorized Access";
            }
        }

        public string SetComment(string str)
        {
            string cmd;
            string response = "";
            // read license header
            cmd = STX + str + ETX;
            try
            {
                serialPort1.Write(cmd);
                response = serialPort1.ReadTo(ETX);
            }
            catch
            {
                try
                {
                    response = "$NG:COMERR$";
                    serialPort1.Close();
                    serialPort1.Open();
                }
                catch
                { }
            }
            response = response.TrimStart('\u0002');
            return response;
        }

        public string GetVersionNum(string str)
        {
            string cmd;
            string response = "";
            // read license header
            cmd = STX + str + ETX;
            try
            {
                serialPort1.Write(cmd);
                if (cmd.Contains ("06"))
                    Thread.Sleep(100);//20170508
                //while (serialPort1.BytesToRead == 0) { }
                response = serialPort1.ReadTo(ETX);
            }
            catch
            {
                try
                {
                    response = "$NG:COMERR$";
                    serialPort1.Close();
                    serialPort1.Open();
                }
                catch
                { }
            }
            response = response.TrimStart('\u0002');
            return response;
        }
#if true    //add by Qin yunhe 20200320
        public string Get_SystemFWversion(string versionCommand)
        {
            string strCommand = "$VER:" + versionCommand + "$";
            // string strCommand = "$VER:ICAN:01$";
            if (!serialPort1.IsOpen)
            {
                MessageBox.Show("Com port is not open!", "Warning!");
                return "error";
            }
            string strRespone = GetVersionNum(strCommand);
            try
            {
                if (strRespone.IndexOf("$") > -1)
                {
                    int indexOfStart = strRespone.IndexOf("$");
                    strRespone = strRespone.Substring(indexOfStart);
                    string strResultFlag = strRespone.Substring(1, 2);

                    if (strResultFlag.Equals("OK"))
                    {
                        int indexOfEnd = strRespone.LastIndexOf("$");
                        strRespone = strRespone.Substring(indexOfStart + 4, indexOfEnd - indexOfStart - 4);
                        string decimalVersionCan = GetDecimalVersionCan(strRespone); 
                        return decimalVersionCan;
                    }
                    else if (strResultFlag.Equals("NG"))
                    {
                        return Utils.GetCommandResult(strRespone);
                    }
                }
                else
                {
                    return "NON CONNECT";

                }
            }
            catch (Exception ex)
            {
                return "ERROR";
            }

            return "ERROR";
        }
#endif

        void getAvailablePorts()
        {
            //String[] ports = SerialPort.GetPortNames();
            //通过WMI获取COM端口
            string[] ss = MulGetHardwareInfo(HardwareEnum.Win32_PnPEntity, "Name");
            System.Collections.ArrayList portArray = new System.Collections.ArrayList();
            try
            {
                for (var i = 0; i < ss.Length; i++)
                {
                    if (ss[i].IndexOf("(") > -1 && ss[i].IndexOf(")") > -1)
                    {
                        portArray.Add(ss[i].Substring(ss[i].IndexOf("(") + 1, ss[i].IndexOf(")") - ss[i].IndexOf("(") - 1));
                    }
                }
            }
            catch
            {
                MessageBox.Show("Get com ports error!");
            }
            if (portArray.Count > 0)
            {
                ComboBoxPortName.Items.AddRange(portArray.ToArray());
                ComboBoxPortName.SelectedIndex = 0;
            }
        }
        public enum HardwareEnum
        {
            // 硬件
            Win32_Processor, // CPU 处理器
            Win32_PhysicalMemory, // 物理内存条
            Win32_Keyboard, // 键盘
            Win32_PointingDevice, // 点输入设备，包括鼠标。
            Win32_FloppyDrive, // 软盘驱动器
            Win32_DiskDrive, // 硬盘驱动器
            Win32_CDROMDrive, // 光盘驱动器
            Win32_BaseBoard, // 主板
            Win32_BIOS, // BIOS 芯片
            Win32_ParallelPort, // 并口
            Win32_SerialPort, // 串口
            Win32_SerialPortConfiguration, // 串口配置
            Win32_SoundDevice, // 多媒体设置，一般指声卡。
            Win32_SystemSlot, // 主板插槽 (ISA & PCI & AGP)
            Win32_USBController, // USB 控制器
            Win32_NetworkAdapter, // 网络适配器
            Win32_NetworkAdapterConfiguration, // 网络适配器设置
            Win32_Printer, // 打印机
            Win32_PrinterConfiguration, // 打印机设置
            Win32_PrintJob, // 打印机任务
            Win32_TCPIPPrinterPort, // 打印机端口
            Win32_POTSModem, // MODEM
            Win32_POTSModemToSerialPort, // MODEM 端口
            Win32_DesktopMonitor, // 显示器
            Win32_DisplayConfiguration, // 显卡
            Win32_DisplayControllerConfiguration, // 显卡设置
            Win32_VideoController, // 显卡细节。
            Win32_VideoSettings, // 显卡支持的显示模式。

            // 操作系统
            Win32_TimeZone, // 时区
            Win32_SystemDriver, // 驱动程序
            Win32_DiskPartition, // 磁盘分区
            Win32_LogicalDisk, // 逻辑磁盘
            Win32_LogicalDiskToPartition, // 逻辑磁盘所在分区及始末位置。
            Win32_LogicalMemoryConfiguration, // 逻辑内存配置
            Win32_PageFile, // 系统页文件信息
            Win32_PageFileSetting, // 页文件设置
            Win32_BootConfiguration, // 系统启动配置
            Win32_ComputerSystem, // 计算机信息简要
            Win32_OperatingSystem, // 操作系统信息
            Win32_StartupCommand, // 系统自动启动程序
            Win32_Service, // 系统安装的服务
            Win32_Group, // 系统管理组
            Win32_GroupUser, // 系统组帐号
            Win32_UserAccount, // 用户帐号
            Win32_Process, // 系统进程
            Win32_Thread, // 系统线程
            Win32_Share, // 共享
            Win32_NetworkClient, // 已安装的网络客户端
            Win32_NetworkProtocol, // 已安装的网络协议
            Win32_PnPEntity,//all device
        }

        /// <summary>
        /// WMI取硬件信息
        /// </summary>
        /// <param name="hardType"></param>
        /// <param name="propKey"></param>
        /// <returns></returns>
        public static string[] MulGetHardwareInfo(HardwareEnum hardType, string propKey)
        {

            List<string> strs = new List<string>();
            try
            {
                using (ManagementObjectSearcher searcher = new ManagementObjectSearcher("select * from " + hardType))
                {
                    var hardInfos = searcher.Get();
                    foreach (var hardInfo in hardInfos)
                    {
                        if (hardInfo["PNPDeviceID"].ToString().Contains("FTDIBUS"))
                        {
                            strs.Add(hardInfo.Properties[propKey].Value.ToString());
                        }
                    }
                    searcher.Dispose();
                }
                return strs.ToArray();
            }
            catch
            {
                return null;
            }
            finally
            { strs = null; }
        }
        public bool IsCommandSending { get; set; }
        private void ButtonClosePort_Click(object sender, EventArgs e)
        {
            if (IsCommandSending)
            {
                MessageBox.Show("Some commands are sent, please try again later!", "Warning!");
                return;
            }
            try
            {
                // now set service mode to oFF
                string cmd;
                string response;
                cmd = STX + CMD_SERV_OFF + ETX;
                serialPort1.Write(cmd);
                response = serialPort1.ReadTo(ETX);

                serialPort1.Close();
                ButtonClosePort.Enabled = false;
                ButtonOpenPort.Enabled = true;
                TextboxSerial.Text = "Serial port not connected.";
                license2.EnableControls(false);
                ButtonClosePort.Enabled = false;

                logFiles1.ClosePortLogFormInit();
            }
            catch
            {
                MessageBox.Show("Operation time out!");
                serialPort1.Close();
                this.ButtonClosePort.Enabled = false;
                this.ButtonOpenPort.Enabled = true;
                logFiles1.ClosePortLogFormInit();
            }
        }

        private void TextBoxSysSN_TextChanged(object sender, EventArgs e)
        {

        }


        private void RadiobuttonByDate_CheckedChanged(object sender, EventArgs e)
        {
            ComboboxInterval.Enabled = false;
            DateTimePickerService.Enabled = true;
        }

        private void RadiobuttonByInterval_CheckedChanged(object sender, EventArgs e)
        {
            ComboboxInterval.Enabled = true;
            DateTimePickerService.Enabled = false;
        }

        private void ComboboxInterval_SelectedIndexChanged(object sender, EventArgs e)
        {
            DateTime dt = DateTime.Now;

            switch (ComboboxInterval.SelectedIndex)
            {
                case 0:
                    dt = dt.AddMonths(6);
                    break;
                case 1:
                    dt = dt.AddMonths(12);
                    break;
                case 2:
                    dt = dt.AddMonths(24);
                    break;
                case 3:
                    dt = dt.AddMonths(36);
                    break;
                default:
                    break;
            }

            // tellTheFw(dt);
            strNextService = dt.ToString(formatYear, System.Globalization.CultureInfo.InvariantCulture);
        }

        public string strNextService = "";
        private void DateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            DateTime dt = DateTimePickerService.Value;

            // tellTheFw(dt);

            //TextboxNextService.Text = dt.ToString(formatYear,System.Globalization.CultureInfo.InvariantCulture);
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (tabControl1.SelectedIndex)
            {
                case 0:
                    // do nothing. The sys info was initialized right after comm port connected.
                    break;
                case 1:
                    break;
                case 2:
                    break;
                case 3:
                    break;
                case 4:
                    break;
                case 5:
                    break;
                default:
                    break;
            }
        }

        private void CheckboxMaintenanceEn_CheckedChanged(object sender, EventArgs e)
        {
            string cmd;
            string response;

            if (!serialPort1.IsOpen)
            {
                MessageBox.Show("Com port is not open!", "Warning!");
                return;
            }


            if (CheckboxMaintenanceEn.Checked == true)
            {
                RadiobuttonByDate.Enabled = true;
                RadiobuttonByInterval.Enabled = true;
                DateTimePickerService.Enabled = true;
                SetNextServiceDate.Enabled = true;
                ComboboxInterval.Enabled = true;
                RadiobuttonByDate.Checked = true;
                cmd = STX + CMD_MD_NOTIFY_ON + ETX;
                serialPort1.Write(cmd);
                response = serialPort1.ReadTo(ETX);
                response = response.Substring(response.IndexOf(STX) + 1);
                MaintenanceInfo.Text = "Enable notification of Next Periodic Maintenance :" + Utils.GetCommandResult(response);
            }
            else
            {
                SetNextServiceDate.Enabled = false;
                RadiobuttonByDate.Enabled = false;
                RadiobuttonByInterval.Enabled = false;
                DateTimePickerService.Enabled = false;
                ComboboxInterval.Enabled = false;
                RadiobuttonByDate.Checked = false;
                RadiobuttonByInterval.Checked = false;

                cmd = STX + CMD_MD_NOTIFY_OFF + ETX;
                serialPort1.Write(cmd);
                response = serialPort1.ReadTo(ETX);
                response = response.Substring(response.IndexOf(STX) + 1);
                MaintenanceInfo.Text = "Disable notification of Next Periodic Maintenance :" + Utils.GetCommandResult(response);
            }
        }

        private Timer timerPerSecond;

        private void ButtonSetFirstDate_Click(object sender, EventArgs e)
        {
            try
            {
                if (!serialPort1.IsOpen)
                {
                    MessageBox.Show("Com port is not open!", "Warning!");
                    return;
                }
                string cmd;
                string response;
                string t;
                string time;
                string t1;
                string t2;
                string t3;
                int t11;
                int t21;
                int t31;

                DialogResult MsgBoxResult;
                MsgBoxResult = MessageBox.Show("You're reseting the first installation time. Proceed?", "Warning!", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                if (MsgBoxResult == DialogResult.Yes)
                {
                    DateTime firstInstallTime = DateTimePickerFirst.Value;
                    time = firstInstallTime.ToString("yyyy/MM/dd", System.Globalization.CultureInfo.InvariantCulture).Replace("/", "");
                    t1 = time.Substring(0, 4);
                    t11 = int.Parse(t1);
                    t2 = time.Substring(4, 2);
                    t21 = int.Parse(t2);
                    t3 = time.Substring(6, 2);
                    t31 = int.Parse(t3);
                    int t12 = t11 - 2000;
                    string t13 = Convert.ToString(t12, 16);
                    string t14 = t13.PadLeft(2, '0');
                    string t22 = Convert.ToString(t21, 16);
                    string t23 = t22.PadLeft(2, '0');
                    string t32 = Convert.ToString(t31, 16);
                    string t33 = t32.PadLeft(2, '0');
                    t = t14 + t23 + t33;
                    cmd = STX + CMD_SID_SET + t + "$" + ETX;
                    serialPort1.Write(cmd);
                    response = serialPort1.ReadTo(ETX);
                    response = response.TrimStart('\u0002');
                    response = response.TrimEnd('$');
                    SystemInfo.Text = "Set First install date: " + Utils.GetCommandResult(response);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Set First install date error:" + ex.ToString());
            }

        }

        private void ButtonSetSysTime_Click(object sender, EventArgs e)
        {
            string errStr = "";
            try
            {
                if (!serialPort1.IsOpen)
                {
                    MessageBox.Show("Com port is not open!", "Warning!");
                    return;
                }
                DialogResult MsgBoxResult;
                MsgBoxResult = MessageBox.Show("You're reseting the system time. Proceed?", "Warning!", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                if (MsgBoxResult == DialogResult.Yes)
                {
                    string strTime;
                    string strDate;
                    string cmd;
                    string response;
                    string time;
                    string time1;
                    string time2;
                    string time3;
                    string t;
                    string t1;
                    string t2;
                    string t3;
                    string t4;
                    string t5;
                    string t6;
                    int t11;
                    int t21;
                    int t31;
                    int t41;
                    int t51;
                    int t61;

                    DateTime sysDate = dateSystemTimePicker.Value;
                    DateTime sysTime = DateTimePickerSys.Value;

                    strTime = sysTime.ToString("HH:mm:ss");
                    strDate = sysDate.ToString("yyyy/MM/dd", System.Globalization.CultureInfo.InvariantCulture);


                    time1 = strDate + strTime;
                    errStr += time1;


                    time2 = time1.Replace("/", "");
                    time3 = time2.Replace(":", "");
                    time = time3.Replace(" ", "");
                    t1 = time.Substring(0, 4);
                    t11 = int.Parse(t1);
                    t2 = time.Substring(4, 2);
                    t21 = int.Parse(t2);
                    t3 = time.Substring(6, 2);
                    t31 = int.Parse(t3);
                    t4 = time.Substring(8, 2);
                    t41 = int.Parse(t4);
                    t5 = time.Substring(10, 2);
                    t51 = int.Parse(t5);
                    t6 = time.Substring(12, 2);
                    t61 = int.Parse(t6);
                    int t12 = t11 - 2000;
                    string t13 = Convert.ToString(t12, 16);
                    string t14 = t13.PadLeft(2, '0');
                    string t22 = Convert.ToString(t21, 16);
                    string t23 = t22.PadLeft(2, '0');
                    string t32 = Convert.ToString(t31, 16);
                    string t33 = t32.PadLeft(2, '0');
                    string t42 = Convert.ToString(t41, 16);
                    string t43 = t42.PadLeft(2, '0');
                    string t52 = Convert.ToString(t51, 16);
                    string t53 = t52.PadLeft(2, '0');
                    string t62 = Convert.ToString(t61, 16);
                    string t63 = t62.PadLeft(2, '0');
                    t = t14 + t23 + t33 + t43 + t53 + t63;
                    cmd = STX + CMD_STIME_SET + t + "$" + ETX;
                    serialPort1.Write(cmd);
                    response = serialPort1.ReadTo(ETX);
                    response = response.TrimStart('\u0002');
                    response = response.TrimEnd('$');
                    SystemInfo.Text = "Set system time: " + Utils.GetCommandResult(response);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Set system time error:" + ex.ToString());
            }
        }

        private void TextboxChgSN_TextChanged(object sender, EventArgs e)
        {

        }

        private void buttonChgSN_Click(object sender, EventArgs e)
        {

        }

        private void TextboxSysType_TextChanged(object sender, EventArgs e)
        {

        }

        private void ModifyID_Click(object sender, EventArgs e)
        {
            try
            {

                if (!serialPort1.IsOpen)
                {
                    MessageBox.Show("Com port is not open!", "Warning!");
                    return;
                }

                if (ModifyID.Text == "Modify")
                {
                    DialogResult MsgBoxResult;
                    MsgBoxResult = MessageBox.Show("Carrier Arm SN is assigned in the factory. Be cautious and use this function for diagnosis purpose only.\nproceed?", "Warning!", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                    if (MsgBoxResult == DialogResult.Yes)
                    {
                        Password passwordForm = new Password();
                        passwordForm.StartPosition = FormStartPosition.CenterParent;
                        passwordForm.ShowDialog();
                        if (passwordForm.DialogResult == DialogResult.OK)
                        {
                            if (!passwordForm.strPassword.Equals(BaseCmd.SysPassword))
                            {
                                MessageBox.Show("The password is not correct!", "Warning!");
                                return;
                            }

                            ModifyID.Text = "ChangeID";
                            TextboxSysIDSet.ReadOnly = false;
                        }
                    }
                }
                else if (ModifyID.Text == "ChangeID")
                {
                    DialogResult MsgBoxResult;
                    MsgBoxResult = MessageBox.Show("You're reseting the Carrier Arm SN. Proceed?", "Warning!", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                    if (MsgBoxResult == DialogResult.Yes)
                    {

                        string changesn = TextboxSysIDSet.Text;
                        string cmd;
                        string response;
                        cmd = STX + CMD_SSN_SET + changesn + "$" + ETX; //CMD_SSN_SET1
                        serialPort1.Write(cmd);
                        response = serialPort1.ReadTo(ETX);
                        response = response.TrimStart('\u0002');
                        SystemInfo.Text = "Change Carrier Arm SN: " + Utils.GetCommandResult(response);
                        ModifyID.Text = "Modify";
                        //TextboxSysIDSet.Clear();
                        TextboxSysIDSet.ReadOnly = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Modify carrier arm SN error:" + ex.ToString());
            }

        }

        private void TabPageSysInfo_Click(object sender, EventArgs e)
        {

        }

        private void GetSystemType_Click(object sender, EventArgs e)
        {
            string cmd;
            string response;
            try
            {
                if (!serialPort1.IsOpen)
                {
                    MessageBox.Show("Com port is not open!", "Warning!");
                    return;
                }
                // get system type   
                cmd = STX + CMD_STYPE_GET + ETX;
                serialPort1.Write(cmd);
                response = serialPort1.ReadTo(ETX);
                response = response.Substring(response.IndexOf(STX) + 1);
                if (response == OK_HDR + "00$")
                {
                    TextboxSysType.Text = "System type not available";
                }
#if false
            if (response == OK_HDR + "01$")
            {
                TextboxSysType.Text = "Peak";
            }
            if (response == OK_HDR + "02$")
            {
                TextboxSysType.Text = "Star";
            }
            if (response == OK_HDR + "03$")
            {
                TextboxSysType.Text = "Panorama";
            }
#else
                if (response == OK_HDR + "01$")
                {
                    TextboxSysType.Text = "EXTARO 300 Dental";
                }
                if (response == OK_HDR + "02$")
                {
                    TextboxSysType.Text = "EXTARO 300 ENT";
                }
#endif

                SystemInfo.Text = "Get System Type: " + TextboxSysType.Text;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Get system type error:" + ex.ToString());
            }

        }

        private void SystemTypeList_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void SystemTypeSet_Click(object sender, EventArgs e)
        {
            string cmd;
            string response;

            if (!serialPort1.IsOpen)
            {
                MessageBox.Show("Com port is not open!", "Warning!");
                return;
            }
            try
            {
                //20170406
                DialogResult MsgBoxResult;
                MsgBoxResult = MessageBox.Show("System Type is meant to be auto-detected during startup. Be cautious and use this function for diagnosis purpose only.\nproceed?", "Warning!", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                if (MsgBoxResult == DialogResult.Yes)
                {

                    Password passwordForm = new Password();
                    passwordForm.StartPosition = FormStartPosition.CenterParent;
                    passwordForm.ShowDialog();
                    if (passwordForm.DialogResult == DialogResult.OK)
                    {
                        if (!passwordForm.strPassword.Equals(BaseCmd.SysPassword))
                        {
                            MessageBox.Show("The password is not correct!", "Warning!");
                            return;
                        }

                        if (SystemTypeList.SelectedIndex == 0)
                        {
                            CMD_STYPE_SET1 = "01$";
                        }
                        else if (SystemTypeList.SelectedIndex == 1)
                        {
                            CMD_STYPE_SET1 = "02$";
                        }
#if false //20170323
            else if (SystemTypeList.SelectedIndex == 2)
            {
                CMD_STYPE_SET1 = "03$";
            }
            else if (SystemTypeList.SelectedIndex == 3)
            {
                CMD_STYPE_SET1 = "00$";
            }
#else
                        else if (SystemTypeList.SelectedIndex == 2)
                        {
                            CMD_STYPE_SET1 = "00$";
                        }
#endif

                        // set system type
                        cmd = STX + CMD_STYPE_SET + CMD_STYPE_SET1 + ETX;
                        serialPort1.Write(cmd);
                        response = serialPort1.ReadTo(ETX);
                        response = response.Substring(response.IndexOf(STX) + 1);

                        SystemInfo.Text = "Set System Type: " + Utils.GetCommandResult(response);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Set system type error:" + ex.ToString());
            }
        }

        private void GetLedLifeRGB_Click(object sender, EventArgs e)
        {
            string cmd;
            string response;
            string ledTime = "";
            int index = 0;

            try
            {
                if (!serialPort1.IsOpen)
                {
                    MessageBox.Show("Com port is not open!", "Warning!");
                    return;
                }

                // get helios light source RGB used time
                cmd = STX + CMD_ILLUM_RGB_GET + ETX;
                serialPort1.Write(cmd);
                response = serialPort1.ReadTo(ETX);

                index = response.IndexOf("$");
                response = response.Substring(index);
                if (index > -1)
                {
                    if (response.Substring(1, 2).Equals("OK"))
                    {
                        response = response.TrimStart((STX + OK_HDR).ToCharArray());
                        response = response.TrimEnd('$');


                        string[] strTimes = response.Split('.');
                        string strTime = "";
                        for (var i = strTimes.Length; i > 0; i--)
                        {
                            strTime += strTimes[i - 1];
                        }
                        int intTime = 0;
                        try
                        {
                            intTime = Int32.Parse(strTime, System.Globalization.NumberStyles.HexNumber);
#if true

#endif
                        }
                        catch
                        {
                            MessageBox.Show("the result :" + response + " can not change to int!");
                        }


                        ledTime = Utils.GetHourMinutes(intTime) + " minutes";
                        TextboxLedTimeRGB.Text = ledTime;
                    }
                    else if (response.Substring(1, 2).Equals("NG"))
                    {
                        ledTime = response.Trim('$');
                        TextboxLedTimeRGB.Text = ledTime;
                    }
                }
                SystemInfo.Text = "Get LED Used Time RGB: " + ledTime;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Get LED used time RGB error:" + ex.ToString());
            }
        }

        private void GetLifeLedVlight_Click(object sender, EventArgs e)
        {
            string cmd;
            string response;
            string ledTime = "";
            int index = 0;

            try
            {
                if (!serialPort1.IsOpen)
                {
                    MessageBox.Show("Com port is not open!", "Warning!");
                    return;
                }

                // get helios light source V-light used time
                cmd = STX + CMD_ILLUM_V_GET + ETX;
                serialPort1.Write(cmd);
                response = serialPort1.ReadTo(ETX);
                index = response.IndexOf("$");
                response = response.Substring(index);
                if (index > -1)
                {
                    if (response.Substring(1, 2).Equals("OK"))
                    {
                        response = response.TrimStart((STX + OK_HDR).ToCharArray());
                        response = response.TrimEnd('$');

                        string[] strTimes = response.Split('.');
                        string strTime = "";
                        for (var i = strTimes.Length; i > 0; i--)
                        {
                            strTime += strTimes[i - 1];
                        }
                        int intTime = 0;
                        try
                        {
                            intTime = Int32.Parse(strTime, System.Globalization.NumberStyles.HexNumber);
                        }
                        catch
                        {
                            MessageBox.Show("the result :" + response + " can not change to int!");
                        }
                        ledTime = Utils.GetHourMinutes(intTime) + " minutes";
                        TextboxLedTimeVlight.Text = ledTime;
                    }
                    else if (response.Substring(1, 2).Equals("NG"))
                    {
                        ledTime = response.Trim('$');
                        TextboxLedTimeVlight.Text = ledTime;
                    }
                }
                SystemInfo.Text = "Get LED Used Time V-Light: " + ledTime;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Get LED used time V-Light error:" + ex.ToString());
            }

        }

        private void GetSystemID_Click(object sender, EventArgs e)
        {
            string cmd;
            string response;
            try
            {
                if (!serialPort1.IsOpen)
                {
                    MessageBox.Show("Com port is not open!", "Warning!");
                    return;
                }

                cmd = STX + CMD_SSN_GET + ETX;

                int index = 0;

                serialPort1.Write(cmd);
                response = serialPort1.ReadTo(ETX);

                index = response.IndexOf("$");
                response = response.Substring(index);
                if (response.Substring(1, 2).Equals("OK"))
                {
                    response = response.TrimStart((STX + OK_HDR).ToCharArray());
                    response = response.TrimEnd('$');
                    TextboxSysIDGet.Text = response;
                }
                else if (response.Substring(1, 2).Equals("NG"))
                {
                    TextboxSysIDGet.Text = response;
                }

                SystemInfo.Text = "Get Carrier Arm SN: " + Utils.GetCommandResult(response);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Get carrier arm SN error:" + ex.ToString());
            }

        }

        private void GetSystemTime_Click(object sender, EventArgs e)
        {
            // get system time
            string cmd;
            string response;
            int index = 0;
            string s1;
            string s2;
            string s3;
            string s4;
            string s5;
            string s6;
            string s;

            try
            {
                if (!serialPort1.IsOpen)
                {
                    MessageBox.Show("Com port is not open!", "Warning!");
                    return;
                }
                cmd = STX + CMD_STIME_GET + ETX;
                serialPort1.Write(cmd);
                response = serialPort1.ReadTo(ETX);

                index = response.IndexOf("$");
                response = response.Substring(index);
                if (response.Substring(1, 2).Equals("OK"))
                {
                    response = response.TrimStart((STX + OK_HDR).ToCharArray());
                    response = response.TrimEnd('$');
                    s1 = response.Substring(0, 2);
                    s2 = response.Substring(2, 2);
                    s3 = response.Substring(4, 2);
                    s4 = response.Substring(6, 2);
                    s5 = response.Substring(8, 2);
                    s6 = response.Substring(10, 2);
                    uint year1 = Convert.ToUInt32(s1, 16);
                    uint year11 = year1 + 2000;
                    string year12 = year11 + "";
                    uint mouth1 = Convert.ToUInt32(s2, 16);
                    string s21 = mouth1 + "";
                    string s22 = s21.PadLeft(2, '0');
                    uint day1 = Convert.ToUInt32(s3, 16);
                    string s31 = day1 + "";
                    string s32 = s31.PadLeft(2, '0');
                    uint time = Convert.ToUInt32(s4, 16);
                    string s41 = time + "";
                    string s42 = s41.PadLeft(2, '0');
                    uint minute = Convert.ToUInt32(s5, 16);
                    string s51 = minute + "";
                    string s52 = s51.PadLeft(2, '0');
                    uint second = Convert.ToUInt32(s6, 16);
                    string s61 = second + "";
                    string s62 = s61.PadLeft(2, '0');
                    s = year12 + s22 + s32 + s42 + s52 + s62;
                    s = s.Insert(4, "/").Insert(7, "/").Insert(10, " ").Insert(13, ":").Insert(16, ":");
                    TextboxTime.Text = s;
                    SystemInfo.Text = "Get System Time: " + s;
                }
                else if (response.Substring(1, 2).Equals("NG"))
                {
                    TextboxTime.Text = response;
                    SystemInfo.Text = "Get System Time: " + Utils.GetCommandResult(response);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Get system time error:" + ex.ToString());
            }

        }

        private void GetFirstInstallDate_Click(object sender, EventArgs e)
        {
            int index = 0;
            string s;
            string s1;
            string s2;
            string s3;
            string cmd;
            string response;

            try
            {
                if (!serialPort1.IsOpen)
                {
                    MessageBox.Show("Com port is not open!", "Warning!");
                    return;
                }
                cmd = STX + CMD_SID_GET + ETX;
                serialPort1.Write(cmd);
                response = serialPort1.ReadTo(ETX);

                index = response.IndexOf("$");
                response = response.Substring(index);
                if (response.Substring(1, 2).Equals("OK"))
                {

                    response = response.Substring(response.IndexOf(STX) + 1);
                    response = response.TrimStart((STX + OK_HDR).ToCharArray());
                    response = response.TrimEnd('$');
                    s1 = response.Substring(0, 2);
                    s2 = response.Substring(2, 2);
                    s3 = response.Substring(4, 2);
                    uint year2 = Convert.ToUInt32(s1, 16);
                    uint year21 = year2 + 2000;
                    string year22 = year21 + "";
                    uint mouth2 = Convert.ToUInt32(s2, 16);
                    string mouth21 = mouth2 + "";
                    string mouth22 = mouth21.PadLeft(2, '0');
                    uint day2 = Convert.ToUInt32(s3, 16);
                    string day21 = day2 + "";
                    string day22 = day21.PadLeft(2, '0');
                    s = year22 + mouth22 + day22;
                    s = s.Insert(4, "/").Insert(7, "/");
                    TextboxFirstTime.Text = s;
                    SystemInfo.Text = "Get First install date: " + s;
                }
                else if (response.Substring(1, 2).Equals("NG"))
                {
                    TextboxTime.Text = response;
                    SystemInfo.Text = "Get First install date: " + Utils.GetCommandResult(response);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Get First install date error:" + ex.ToString());
            }

        }

        private void GetVersion_Click(object sender, EventArgs e)
        {
            try
            {
                if (!serialPort1.IsOpen)
                {
                    MessageBox.Show("Com port is not open!", "Warning!");
                    return;
                }
                //GetVersion.Enabled = false;
                GetVersionCan("ICAN:01", TextboxCurCentral);
                //System.Threading.Thread.Sleep(1000);
                GetVersionCan("ICAN:02", TextboxCurOpmi);
                //System.Threading.Thread.Sleep(1000);
                GetVersionCan("ICAN:06", TextboxCurCCU);
                //GetVersion.Enabled = true;
            }
            catch(Exception ex)
            {
                MessageBox.Show("Get version error:" + ex.ToString ());
            }
        }

        private void GetVersionCan(string versionCommand, TextBox tbox)
        {
            string strCommand = "$VER:" + versionCommand + "$";
            string strRespone = GetVersionNum(strCommand);
            try
            {
                if (strRespone.IndexOf("$") > -1)
                {
                    int indexOfStart = strRespone.IndexOf("$");
                    strRespone = strRespone.Substring(indexOfStart);
                    string strResultFlag = strRespone.Substring(1, 2);

                    if (strResultFlag.Equals("OK"))
                    {
                        int indexOfEnd = strRespone.LastIndexOf("$");
                        strRespone = strRespone.Substring(indexOfStart + 4, indexOfEnd - indexOfStart - 4);
                        string decimalVersionCan = GetDecimalVersionCan(strRespone);
                        tbox.Text = decimalVersionCan;
                    }
                    else if (strResultFlag.Equals("NG"))
                    {
                        tbox.Text = Utils.GetCommandResult(strRespone);
                    }
                }
                else
                {
                    tbox.Text = strRespone;
                }
            }
            catch (Exception ex)
            {
                tbox.Text = ex.ToString();
            }
        }


        public string GetDecimalVersionCan(string strRespone)
        {
            //strRespone = "CORE.0A.0B.0C.APP.0E.0F.01";
            string decimalVersionCan = "";
            try
            {
                string strCore = strRespone.Substring(0, 4);
                string strVersionFir = strRespone.Substring(5, 2);
                string strVsionSec = strRespone.Substring(8, 2);
                string strVsionThi = strRespone.Substring(11, 2);
                string strApp = strRespone.Substring(14, 3);
                string strVersionFou = strRespone.Substring(18, 2);
                string strVersionFif = strRespone.Substring(21, 2);
                string strVersionSix = strRespone.Substring(24, 2);

                int intVersionFir = Convert.ToInt32(strVersionFir, 16);
                int intVsionSec = Convert.ToInt32(strVsionSec, 16);
                int intVsionThi = Convert.ToInt32(strVsionThi, 16);
                int intVersionFou = Convert.ToInt32(strVersionFou, 16);
                int intVersionFif = Convert.ToInt32(strVersionFif, 16);
                int intVersionSix = Convert.ToInt32(strVersionSix, 16);

                decimalVersionCan = strCore + "." + intVersionFir.ToString().PadLeft(2, '0') + "." + intVsionSec.ToString().PadLeft(2, '0') + "." + intVsionThi.ToString().PadLeft(2, '0') + "."
                    + strApp + "." + intVersionFou.ToString().PadLeft(2, '0') + "." + intVersionFif.ToString().PadLeft(2, '0') + "." + intVersionSix.ToString().PadLeft(2, '0');

            }
            catch
            {
                decimalVersionCan = "";
                return decimalVersionCan;
            }
            return decimalVersionCan;
        }


        private void SetNextServiceDate_Click(object sender, EventArgs e)
        {
            try
            {
                string cmd;
                string response;
                string strDate;
                string t;
                string time;
                string t1;
                string t2;
                string t3;
                int t11;
                int t21;
                int t31;

                if (!serialPort1.IsOpen)
                {
                    MessageBox.Show("Com port is not open!", "Warning!");
                    return;
                }

                if (RadiobuttonByDate.Checked == true)
                {
                    DateTime sysDate = DateTimePickerService.Value;
                    strDate = sysDate.ToString("yyyy/MM/dd", System.Globalization.CultureInfo.InvariantCulture);
                    time = strDate.Replace("/", "");
                    t1 = time.Substring(0, 4);
                    t11 = int.Parse(t1);
                    t2 = time.Substring(4, 2);
                    t21 = int.Parse(t2);
                    t3 = time.Substring(6, 2);
                    t31 = int.Parse(t3);
                    int t12 = t11 - 2000;
                    string t13 = Convert.ToString(t12, 16);
                    string t14 = t13.PadLeft(2, '0');
                    string t22 = Convert.ToString(t21, 16);
                    string t23 = t22.PadLeft(2, '0');
                    string t32 = Convert.ToString(t31, 16);
                    string t33 = t32.PadLeft(2, '0');
                    t = t14 + t23 + t33;
                    CMD_MDN_SET1 = t;
                }
                else if (RadiobuttonByInterval.Checked == true)
                {
                    DateTime dt = DateTime.Now;

                    switch (ComboboxInterval.SelectedIndex)
                    {
                        case 0:
                            dt = dt.AddMonths(6);
                            break;
                        case 1:
                            dt = dt.AddMonths(12);
                            break;
                        case 2:
                            dt = dt.AddMonths(24);
                            break;
                        case 3:
                            dt = dt.AddMonths(36);
                            break;
                        default:
                            break;
                    }

                    // tellTheFw(dt);
                    strNextService = dt.ToString(formatYear, System.Globalization.CultureInfo.InvariantCulture);
                    time = strNextService.Replace("/", "");
                    t1 = time.Substring(0, 4);
                    t11 = int.Parse(t1);
                    t2 = time.Substring(4, 2);
                    t21 = int.Parse(t2);
                    t3 = time.Substring(6, 2);
                    t31 = int.Parse(t3);
                    int t12 = t11 - 2000;
                    string t13 = Convert.ToString(t12, 16);
                    string t14 = t13.PadLeft(2, '0');
                    string t22 = Convert.ToString(t21, 16);
                    string t23 = t22.PadLeft(2, '0');
                    string t32 = Convert.ToString(t31, 16);
                    string t33 = t32.PadLeft(2, '0');
                    t = t14 + t23 + t33;
                    CMD_MDN_SET1 = t;
                }

                CMD_MDN_SET1 = CMD_MDN_SET1 + "$";
                // set next periodic maintenance date
                cmd = STX + CMD_MDN_SET + CMD_MDN_SET1 + ETX;
                serialPort1.Write(cmd);
                response = serialPort1.ReadTo(ETX);
                response = response.TrimStart('\u0002');
                response = response.TrimEnd('$');
                MaintenanceInfo.Text = "Set Next Periodic Maintenance Date :" + Utils.GetCommandResult(response);
            }
            catch(Exception ex)
            {
                MessageBox.Show("Set service date error:" + ex.ToString());
            }
        }

        private void GetLastServiceDate_Click(object sender, EventArgs e)
        {
            string s;
            string s1;
            string s2;
            string s3;

            string cmd;
            string response;
            try
            {
                if (!serialPort1.IsOpen)
                {
                    MessageBox.Show("Com port is not open!", "Warning!");
                    return;
                }
                // get previous maintenance date
                cmd = STX + CMD_MDP_GET + ETX;
                serialPort1.Write(cmd);
                response = serialPort1.ReadTo(ETX);
                int okIndex = response.IndexOf("$OK");
                if (okIndex > -1)
                {
                    response = response.Substring(okIndex);
                    response = response.TrimStart((STX + OK_HDR).ToCharArray());
                    response = response.TrimEnd('$');
                    s1 = response.Substring(0, 2);
                    s2 = response.Substring(2, 2);
                    s3 = response.Substring(4, 2);
                    uint year2 = Convert.ToUInt32(s1, 16);
                    uint year21 = year2 + 2000;
                    string year22 = year21 + "";
                    uint mouth2 = Convert.ToUInt32(s2, 16);
                    string mouth21 = mouth2 + "";
                    string mouth22 = mouth21.PadLeft(2, '0');
                    uint day2 = Convert.ToUInt32(s3, 16);
                    string day21 = day2 + "";
                    string day22 = day21.PadLeft(2, '0');
                    s = year22 + mouth22 + day22;
                    s = s.Insert(4, "/").Insert(7, "/");

                    TextboxLastService.Text = s;
                    MaintenanceInfo.Text = "Get Previous Maintenance Date :" + s;
                }
                else
                {
                    MaintenanceInfo.Text = "Get Previous Maintenance Date :" + Utils.GetCommandResult(response);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Get last service date error:"+ ex.ToString());
            }

        }

        private void GetnextServiceDate_Click(object sender, EventArgs e)
        {
            string s;
            string s1;
            string s2;
            string s3;

            string cmd;
            string response;

            try
            {
                if (!serialPort1.IsOpen)
                {
                    MessageBox.Show("Com port is not open!", "Warning!");
                    return;
                }
                // get previous maintenance date
                cmd = STX + CMD_MDN_GET + ETX;
                serialPort1.Write(cmd);
                response = serialPort1.ReadTo(ETX);
                int okIndex = response.IndexOf("$OK");
                if (okIndex > -1)
                {
                    int index = response.IndexOf("$");
                    response = response.Substring(index);
                    response = response.TrimStart((STX + OK_HDR).ToCharArray());
                    response = response.TrimEnd('$');

                    s1 = response.Substring(0, 2);
                    s2 = response.Substring(2, 2);
                    s3 = response.Substring(4, 2);
                    uint year2 = Convert.ToUInt32(s1, 16);
                    uint year21 = year2 + 2000;
                    string year22 = year21 + "";
                    uint mouth2 = Convert.ToUInt32(s2, 16);
                    string mouth21 = mouth2 + "";
                    string mouth22 = mouth21.PadLeft(2, '0');
                    uint day2 = Convert.ToUInt32(s3, 16);
                    string day21 = day2 + "";
                    string day22 = day21.PadLeft(2, '0');
                    s = year22 + mouth22 + day22;
                    s = s.Insert(4, "/").Insert(7, "/");

                    TextboxNextService.Text = s;
                    MaintenanceInfo.Text = "Get Next Maintenance Date :" + s;
                }
                else
                    MaintenanceInfo.Text = "Get Next Maintenance Date :" + Utils.GetCommandResult(response);
            }
            catch(Exception ex)
            {
                MessageBox.Show("Get next service date error:" + ex.ToString());
            }
        }

        private void MainMenu_Load(object sender, EventArgs e)
        {
            if (license2 != null)
            {
                license2.InitializeLicenseComponent(this, serialPort1);
            }
            if (parameters != null)
            {
                parameters.InitializeParameterComponent(this, serialPort1);
            }
            if (logFiles1 != null)
            {
                logFiles1.InitializelogFilesComponent(this, serialPort1);
            }
        }

        private void CheckboxMaintenanceEn_Click(object sender, EventArgs e)
        {
            bool isChecked = CheckboxMaintenanceEn.Checked;
            if (!serialPort1.IsOpen)
            {
                CheckboxMaintenanceEn.Checked = !isChecked;
                MessageBox.Show("Com port is not open!", "Warning!");
                return;
            }
            Password passwordForm = new Password();
            passwordForm.StartPosition = FormStartPosition.CenterParent;
            passwordForm.ShowDialog();
            if (passwordForm.DialogResult == DialogResult.OK)
            {
                try
                {
                    if (!passwordForm.strPassword.Equals(BaseCmd.SysPassword))
                    {
                        CheckboxMaintenanceEn.Checked = !isChecked;
                        MessageBox.Show("The password is not correct!", "Warning!");
                        return;
                    }
                    else
                    {
                        string cmd;
                        string response;
                        if (CheckboxMaintenanceEn.Checked == true)
                        {
                            RadiobuttonByDate.Enabled = true;
                            RadiobuttonByInterval.Enabled = true;
                            DateTimePickerService.Enabled = true;
                            SetNextServiceDate.Enabled = true;
                            ComboboxInterval.Enabled = true;
                            RadiobuttonByDate.Checked = true;
                            cmd = STX + CMD_MD_NOTIFY_ON + ETX;
                            serialPort1.Write(cmd);
                            response = serialPort1.ReadTo(ETX);
                            response = response.Substring(response.IndexOf(STX) + 1);
                            MaintenanceInfo.Text = "Enable notification of Next Periodic Maintenance :" + Utils.GetCommandResult(response);
                        }
                        else
                        {
                            SetNextServiceDate.Enabled = false;
                            RadiobuttonByDate.Enabled = false;
                            RadiobuttonByInterval.Enabled = false;
                            DateTimePickerService.Enabled = false;
                            ComboboxInterval.Enabled = false;
                            RadiobuttonByDate.Checked = false;
                            RadiobuttonByInterval.Checked = false;

                            cmd = STX + CMD_MD_NOTIFY_OFF + ETX;
                            serialPort1.Write(cmd);
                            response = serialPort1.ReadTo(ETX);
                            response = response.Substring(response.IndexOf(STX) + 1);
                            MaintenanceInfo.Text = "Disable notification of Next Periodic Maintenance :" + Utils.GetCommandResult(response);
                        }
                    }
                }
                catch
                {
                    MessageBox.Show("Com port is not open!", "Warning!");
                }
            }
            else
            {
                passwordForm.Close();
                CheckboxMaintenanceEn.Checked = !isChecked;
                return;
            }
        }

        private void btnBrowseHex_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Title = "Please select a file";
                ofd.Filter = "Hex File|*.zip;";
                ofd.Multiselect = false;
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    txtHexFilePath.Text = ofd.FileName;
                }
            }
            catch
            {
                MessageBox.Show("Open file error!");
            }
        }

        /// <summary>
        /// Start download
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDownload_Click(object sender, EventArgs e)
        {
            try
            {
                Utils.DeleteLogFile(LogFileName);
                Connectivity.Enabled = false;
                tabControl1.Enabled = false;
                string selectFile = txtHexFilePath.Text.Trim();
                if (selectFile == "")
                {
                    MessageBox.Show("Please select a file.", "Warning!");
                    Connectivity.Enabled = true;
                    tabControl1.Enabled = true;
                    return;
                }

#if true //20180425
                if (!serialPort1.IsOpen)
                {
                    MessageBox.Show("Com port is not open!", "Warning!");
                    Connectivity.Enabled = true;
                    tabControl1.Enabled = true;
                    return;
                }
#endif

                string coreVersion = "";
                string appVersion = "";

                string response = SetComment(CMD_VER_ICAN + CMD_VER_ICAN1);
                string replyMessage = Utils.GetCommandResult(response);

                if (replyMessage.Contains("OK:"))
                {
                    #region validate version
                    int indexOK = replyMessage.IndexOf("OK");
                    coreVersion = replyMessage.Substring(indexOK + 8, 8);
                    appVersion = replyMessage.Substring(indexOK + 21, 8);
                    List<string> listCoreVersion = coreVersion.Split(new char[] { '.' }).ToList();
                    List<string> listAppVersion = appVersion.Split(new char[] { '.' }).ToList();
                    bool matchCoreVersion = true;
                    bool matchAppVersion = true;


                    //Core Version 01.01.00 
                    if (Convert.ToInt32(listCoreVersion[0], 16) < 1)
                    {
                        matchCoreVersion = false;
                    }
                    else if (Convert.ToInt32(listCoreVersion[0], 16) == 1)
                    {
                        if (Convert.ToInt32(listCoreVersion[1], 16) < 1)
                        {
                            matchCoreVersion = false;
                        }
                    }

                    //App Version 02.13.00
                    if (Convert.ToInt32(listAppVersion[0], 16) < 2)
                    {
                        matchAppVersion = false;
                    }
                    else if (Convert.ToInt32(listAppVersion[0], 16) == 2)
                    {
                        if (Convert.ToInt32(listAppVersion[1], 16) < 13)
                        {
                            matchAppVersion = false;
                        }
                    }
#if true
                    if (!matchCoreVersion || !matchAppVersion)
                    {
                        Utils.SetTextboxValue(txtFwInfo, "Download function is not supported by this machine.");
                        MessageBox.Show("Sorry, download function is not supported by this machine.", "Warning!");
                        Connectivity.Enabled = true;
                        tabControl1.Enabled = true;
                        return;
                    }
#endif
                    #endregion


                    if (!Utils.UnZip(selectFile, downloadTempFolder, ""))
                    {
                        Utils.DelectDir(downloadTempFolder);
                        Connectivity.Enabled = true;
                        tabControl1.Enabled = true;
                        return;
                    }
                    string[] files = Directory.GetFiles(downloadTempFolder);

#if false
                //Analytic package for 3 files
                List<DownloadOption> listDownloads = new List<DownloadOption>();
                List<PCBAType> listTypeFile = new List<PCBAType>();
                List<PCBAType> listPcbaType = new List<PCBAType>();
                files.ToList().ForEach(x =>
                {
                    int fileIndex = x.LastIndexOf(@"\");
                    string fileName = x.Substring(fileIndex + 1);
                    if (fileName.Contains("CentralCtrl"))
                    {
                        if (!listTypeFile.Contains(PCBAType.CentralControl))
                        {
                            listTypeFile.Add(PCBAType.CentralControl);
                        }
                        listDownloads.Add(new DownloadOption { PcbaType = PCBAType.CentralControl, FilePath = x, FileName = fileName, DownloadIndex = 3 });
                    }
                    else if (fileName.Contains("OPMICtrl"))
                    {
                        if (!listTypeFile.Contains(PCBAType.OPMIControl))
                        {
                            listTypeFile.Add(PCBAType.OPMIControl);
                        }
                        listDownloads.Add(new DownloadOption { PcbaType = PCBAType.OPMIControl, FilePath = x, FileName = fileName, DownloadIndex = 2 });
                    }
                    else if (fileName.Contains("CCUGateway"))
                    {
                        if (!listTypeFile.Contains(PCBAType.CCUGateway))
                        {
                            listTypeFile.Add(PCBAType.CCUGateway);
                        }
                        listDownloads.Add(new DownloadOption { PcbaType = PCBAType.CCUGateway, FilePath = x, FileName = fileName, DownloadIndex = 1 });
                    }
                });

                //check 3 files
                if (listTypeFile.Count != 3)
                {
                    MessageBox.Show(InfoMessage.DownloadFileCountError, "Warning!");
                    Connectivity.Enabled = true;
                    tabControl1.Enabled = true;
                    return;
                }
                else if (listTypeFile.Count == 3)
                {
                    List<PCBAType> wholeFiles = new List<PCBAType>() { PCBAType.CCUGateway, PCBAType.OPMIControl, PCBAType.CentralControl };
                    listTypeFile.ForEach(m =>
                    {
                        wholeFiles.Remove(m);
                    });

                    if (wholeFiles.Count > 0)
                    {
                        string errMsg = "";
                        int errDecodecount = 0;
                        wholeFiles.ForEach(m =>
                        {
                            if (errDecodecount == 0)
                                errMsg += m.ToString() + ".hex decode error!";
                            else
                                errMsg += "\r\n" + m.ToString() + ".hex decode error!";
                            errDecodecount++;
                        });
                        MessageBox.Show(errMsg, "Warning!");
                        Connectivity.Enabled = true;
                        tabControl1.Enabled = true;
                        return;
                    }
                }
#else
                    List<DownloadOption> listDownloads = new List<DownloadOption>();
                    List<PCBAType> listTypeFile = new List<PCBAType>();
                    List<PCBAType> listPcbaType = new List<PCBAType>();
                    //Utils.LogDebug(LogFileName, "download start");

                    if (files.Length != 3)
                    {
                        MessageBox.Show(InfoMessage.DownloadFileCountError, "Warning!");
                        Utils.DelectDir(downloadTempFolder);
                        Connectivity.Enabled = true;
                        tabControl1.Enabled = true;
                        return;
                    }
                    else
                    {
                        //20180417
                        DialogResult MsgBoxResult;
                        MsgBoxResult = MessageBox.Show("The download operation is an irreversible operation. Proceed?", "Warning!", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                        if (MsgBoxResult == DialogResult.No)
                        {
                            Utils.DelectDir(downloadTempFolder);
                            Connectivity.Enabled = true;
                            tabControl1.Enabled = true;
                            return;
                        }
                        else
                        {
                            bool read3files = true;
                            files.ToList().ForEach(x =>
                            {
                                int fileIndex = x.LastIndexOf(@"\");
                                string fileName = x.Substring(fileIndex + 1);

                                string strBinFile = x;
                                try
                                {
                                    if (File.Exists(strBinFile))
                                    {
                                        //DLD_Hex
                                        string strReadline = "";
                                        string updateTime = "";
                                        string appName = "";
                                        string apphexVersion = "";
                                        string coreName = "";
                                        string corehexVersion = "";
                                        string lineData = "";
                                        string realData = "";


                                        using (FileStream fs = new FileStream(strBinFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                                        {
                                            using (StreamReader read = new StreamReader(fs, Encoding.Default))
                                            {
                                                //check version
                                                while ((strReadline = read.ReadLine()) != null)
                                                {
                                                    lineData = Encoding.ASCII.GetString(Utils.StrToByte(strReadline));
                                                    realData = lineData.Substring(9, lineData.Length - 11);
                                                    if (lineData.StartsWith(":108100"))
                                                    { //update time
                                                        updateTime = Utils.HexStringToASCII(realData);
                                                    }
                                                    else if (lineData.StartsWith(":108110"))
                                                    { //app name
                                                        appName = Utils.HexStringToASCII(realData);
                                                    }
                                                    else if (lineData.StartsWith(":108120"))
                                                    { //app version
                                                        apphexVersion = Utils.GetVersionFromByteArray(realData);
                                                    }
                                                    else if (lineData.StartsWith(":108140"))
                                                    { //core name
                                                        coreName = Utils.HexStringToASCII(realData); ;
                                                    }
                                                    else if (lineData.StartsWith(":108150"))
                                                    { //core version 
                                                        corehexVersion = Utils.GetVersionFromByteArray(realData);
                                                        break;
                                                    }
                                                }
                                                //read.Close();
                                                //read.Dispose();
                                            }
                                            //fs.Close();
                                            //fs.Dispose();
                                            GC.Collect();
                                        }

                                        //Utils.LogDebug(LogFileName, "AppName=" + appName + ",CoreName=" + coreName);

                                        if (coreName.Contains("LPC17xx-Core"))
                                        {
                                            if (appName.Contains("UNO3CentralCtrl"))
                                            {
                                                if (!listTypeFile.Contains(PCBAType.CentralControl))
                                                {
                                                    listTypeFile.Add(PCBAType.CentralControl);
                                                }
                                                listDownloads.Add(new DownloadOption
                                                {
                                                    PcbaType = PCBAType.CentralControl,
                                                    FilePath = x,
                                                    FileName = fileName,
                                                    DownloadIndex = 3,
                                                    AppVersion = apphexVersion,
                                                    CoreVersion = corehexVersion
                                                });
                                            }
                                            else if (appName.Contains("UNO3OPMICtrl"))
                                            {
                                                if (!listTypeFile.Contains(PCBAType.OPMIControl))
                                                {
                                                    listTypeFile.Add(PCBAType.OPMIControl);
                                                }
                                                listDownloads.Add(new DownloadOption
                                                {
                                                    PcbaType = PCBAType.OPMIControl,
                                                    FilePath = x,
                                                    FileName = fileName,
                                                    DownloadIndex = 2,
                                                    AppVersion = apphexVersion,
                                                    CoreVersion = corehexVersion
                                                });
                                            }
                                            else if (appName.Contains("UNO3CCUGateway"))
                                            {
                                                if (!listTypeFile.Contains(PCBAType.CCUGateway))
                                                {
                                                    listTypeFile.Add(PCBAType.CCUGateway);
                                                }
                                                listDownloads.Add(new DownloadOption
                                                {
                                                    PcbaType = PCBAType.CCUGateway,
                                                    FilePath = x,
                                                    FileName = fileName,
                                                    DownloadIndex = 1,
                                                    AppVersion = apphexVersion,
                                                    CoreVersion = corehexVersion
                                                });
                                            }
                                        }
                                    }
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show("Read file error:" + ex.ToString());
                                    Connectivity.Enabled = true;
                                    tabControl1.Enabled = true;
                                    read3files = false;
                                    return;
                                }

                            });
                            if (!read3files)
                            {
                                return;
                            }
                            List<PCBAType> wholeFiles = new List<PCBAType>() { PCBAType.CCUGateway, PCBAType.OPMIControl, PCBAType.CentralControl };
                            listTypeFile.ForEach(m =>
                            {
                                wholeFiles.Remove(m);
                            });

                            if (wholeFiles.Count > 0)
                            {
                                string errMsg = "";
                                int errDecodecount = 0;
                                wholeFiles.ForEach(m =>
                                {
                                    if (errDecodecount == 0)
                                        errMsg += m.ToString() + ".hex decode error!";
                                    else
                                        errMsg += "\r\n" + m.ToString() + ".hex decode error!";
                                    errDecodecount++;
                                });
                                Utils.DelectDir(downloadTempFolder);
                                MessageBox.Show(errMsg, "Warning!");
                                Connectivity.Enabled = true;
                                tabControl1.Enabled = true;
                                return;
                            }
                            Task.Factory.StartNew(() => { DownloadAllPcba(listDownloads); });
                        }
                    }
#endif
                }
                else
                {
                    MessageBox.Show("Get CentralControl version error." + replyMessage, "Warning!");
                    Connectivity.Enabled = true;
                    tabControl1.Enabled = true;
                }
            }
            catch { }
        }

        private void GetPCBAVersion(PCBAType pcbaType, ref string coreVersion, ref string appVersion)
        {
            string icanIndex = "";
            if (pcbaType == PCBAType.CCUGateway)
            {
                icanIndex = "06$";
            }
            else if (pcbaType == PCBAType.OPMIControl)
            {
                icanIndex = "02$";
            }
            else if (pcbaType == PCBAType.CentralControl)
            {
                icanIndex = "01$";
            }
            string response = SetComment(CMD_VER_ICAN + icanIndex);
            string replyMessage = Utils.GetCommandResult(response);

            if (replyMessage.Contains("OK:"))
            {
                int indexOK = replyMessage.IndexOf("OK");
                coreVersion = replyMessage.Substring(indexOK + 8, 8);
                appVersion = replyMessage.Substring(indexOK + 21, 8);
                List<string> listCoreVersion = coreVersion.Split(new char[] { '.' }).ToList();
                List<string> listAppVersion = appVersion.Split(new char[] { '.' }).ToList();

                //Core Version
                coreVersion = Convert.ToInt32(listCoreVersion[0], 16).ToString()
                    +"."+Convert.ToInt32(listCoreVersion[1], 16).ToString()
                    +"."+Convert.ToInt32(listCoreVersion[2], 16).ToString();
                

                //App Version
                appVersion = Convert.ToInt32(listAppVersion[0], 16).ToString()
                    + "." + Convert.ToInt32(listAppVersion[1], 16).ToString()
                    + "." + Convert.ToInt32(listAppVersion[2], 16).ToString();
            }
            else
            {
                coreVersion = "";
                appVersion = "";
            }
        }
        private void DownloadAllPcba(List<DownloadOption> downloads)
        {
            downloadResult.Clear();
            DownloadControlStatus(false); //disable buttons
            downloads.OrderBy(m => m.DownloadIndex).ToList().ForEach(m => {
                DownloadPCBA(m, ChecksumType.ADD32);
            });

            string strmsg = "Download completed.\r\nResult：";
            //string strmsg = "Download completed.\r\nRESULT：";
            int msgRow=0;
            foreach (var item in downloadResult)
            {
                if (msgRow == 0)
                {
                    strmsg += item.Value;
                }
                else
                {
                    strmsg += "\r\n            " + item.Value;
                }
                msgRow++;
            }
            if (strmsg.Contains(InfoMessage.ErrorInDownloading))
            {
                strmsg += "\r\n\r\nDownload failure detected, please retry!";
            }
            else 
            {
                strmsg += "\r\n\r\nPlease restart the system!";
            }
            Utils.DelectDir(downloadTempFolder);
            MessageBox.Show(strmsg, "Info");
            DownloadControlStatus(true); //enable buttons
            BeginInvoke(new Action(() => { Connectivity.Enabled = true; tabControl1.Enabled = true; }));   
        }

        public string DLDInitCommand(byte InitType)
        {
            byte[] byteData = new byte[8] { InitType, 0x00, 0x00, 0x00, 0x41, 0xB1, 0x8D, 0x87 };
            byte checksum = (byte)Utils.Add32Checksum(byteData);
            return "$DLD:INIT:" + Utils.byteToHexStr(byteData) + ":" + checksum.ToString("X2") + "$";
        }

        public string DLDHexCommand(string hexData)
        {
            var hexDataLength = (byte)(Utils.StrToByte(hexData).Length);
            byte checksum = (byte)Utils.Add32Checksum(hexData);
            return "$DLD:HEX:" + Utils.byteToHexStr(new byte[] { hexDataLength }) + ":" + hexData + ":" + checksum.ToString("X2") + "$";
        }

        public string DLDCSCommand(string hexData)
        {
            byte[] byteAllData=Utils.StrToHexByte(hexData);
            var dataLength = byteAllData.Length;
            var byteDataLength = new byte[4];
#if true
            byteDataLength[0] = (byte)(dataLength & 0xFFFFFF);
            byteDataLength[1] = (byte)((dataLength >> 8) & 0xFFFFFF);
            byteDataLength[2] = (byte)((dataLength >> 16) & 0xFFFFFF);
            byteDataLength[3] = (byte)((dataLength >> 24) & 0xFFFFFF);
#else
            byteDataLength[0] = (byte)((dataLength >> 24) & 0xFFFFFF);
            byteDataLength[1] = (byte)((dataLength >> 16) & 0xFFFFFF);
            byteDataLength[2] = (byte)((dataLength >> 8) & 0xFFFFFF);
            byteDataLength[3] = (byte)(dataLength & 0xFFFFFF);
#endif

            var dataChecksum = Utils.Add32Checksum4Bytes(byteAllData);
            var csData = Utils.byteToHexStr(byteDataLength) + Utils.byteToHexStr(dataChecksum);
            var csDataChecksum = Utils.Add32Checksum(csData);
            return "$DLD:CS:" + csData + ":" + csDataChecksum.ToString("X2") + "$";
        }

        public string DLDCloseCommand()
        {
            return "$DLD:CLOSE$";
        }

        public string ServModeCommand()
        {
            return "$SERV:ON$";
        }

        public void SendDownloadCommand(string command, out string replyMessage, out bool sendSuccess)
        {
            replyMessage = "";
            string strReponse = SetComment(command);
            replyMessage = Utils.GetCommandResult(strReponse);
            if (strReponse.IndexOf("$OK") == -1)
            {
                sendSuccess = false;
            }
            else
            {
                //command send success
                sendSuccess = true;
            }
        }
        private Dictionary<PCBAType, string> downloadResult = new Dictionary<PCBAType, string>();
        private bool isDownloading = false;
        private void DownloadPCBA(DownloadOption downloadOption, ChecksumType checksumType)
        {
            var pcbaType = downloadOption.PcbaType;
            string softAppVersion = "";
            string softCoreVersion = "";
            string appVersion = downloadOption.AppVersion;
            string coreVersion = downloadOption.CoreVersion;
            GetPCBAVersion(pcbaType, ref softCoreVersion, ref softAppVersion);
            string prefixLogInfo = "";
            if (pcbaType == PCBAType.CCUGateway)
            {
                prefixLogInfo = "Step1/3 CCUGateway:";
            }
            else if (pcbaType == PCBAType.OPMIControl)
            {
                prefixLogInfo = "Step2/3 OPMICtrl:";
            }
            else if (pcbaType == PCBAType.CentralControl)
            {
                prefixLogInfo = "Step3/3 CentralCtrl:";
            }

            string strBinFile = downloadOption.FilePath;
            int setRetryTimes = Convert.ToInt32(BaseCmd.SendHexRetryTimes);
            try
            {
                isDownloading = true;                
                bool downloadSuccess = false;
                string commandMessage = "";
                if (File.Exists(strBinFile))
                {
                    //DLD_Hex
                    

#if true //for debug
                    if (softAppVersion.Equals(appVersion) && softCoreVersion.Equals(coreVersion))
#else
                    if (pcbaType==PCBAType.OPMIControl)
#endif
                    {//version is same,do not download
                        Utils.LogDebug(LogFileName, pcbaType.ToString() + " version is same with pcba");
                        SaveDownloadResult(pcbaType, "updated to CORE." + coreVersion + ".APP." + appVersion + ".");
                        return;
                    }
#if true
                    //DLD_Init
                    byte initType = (byte)((byte)pcbaType | (byte)checksumType);
                    Utils.SetTextboxValue(txtFwInfo, prefixLogInfo + "DLD_Init...");

                    //retry init for 3 times
                    string strResponse = "";
                    int initSendCount = 0;
                    bool initSendSuccess = false;
                    do
                    {
                        strResponse = SetComment(DLDInitCommand(initType));
                        
                        if (!strResponse.Contains("$NG"))
                        {
                            initSendSuccess = true;
                            break;
                        }
                        else
                        {
                            initSendSuccess = false;
                            initSendCount++;
                            Thread.Sleep(2000);
                        }
                    } while (!initSendSuccess && initSendCount < setRetryTimes);


                    if (!initSendSuccess)
                    {
                        Utils.SetTextboxValue(txtFwInfo, prefixLogInfo + "Initial failed");
                        Utils.LogDebug(LogFileName, pcbaType.ToString() + " Initial failed." + strResponse);
                        SaveDownloadResult(pcbaType, InfoMessage.NotDetected);
                        return;
                    }
                    else
                    {
                        try
                        {
                            int initReplyIndex = strResponse.IndexOf("$INIT:") + 6;
                            commandMessage = strResponse.Substring(initReplyIndex, 16);
                            byte[] byteInitAnswer = new byte[8];
                            byteInitAnswer = Utils.StrToHexByte(commandMessage);

                            if ((byteInitAnswer[0] & 0x01) == 0x01)
                            {
                                Utils.SetTextboxValue(txtFwInfo, prefixLogInfo + "Initial successful.");
                            }
                            else
                            {
                                Utils.SetTextboxValue(txtFwInfo, prefixLogInfo + "Initial failed，Response message = " + strResponse + ".");
                                Utils.LogDebug(LogFileName, pcbaType.ToString() + " Initial failed，Response message = " + strResponse + ".");
                                SaveDownloadResult(pcbaType, InfoMessage.ErrorInDownloading);
                                return;

                            }
                        }
                        catch(Exception ex)
                        {
                            Utils.SetTextboxValue(txtFwInfo, prefixLogInfo + "Parse Initial answer failed，Response message = " + strResponse + "." + ex.ToString());
                            Utils.LogDebug(LogFileName, pcbaType.ToString() + " Parse Initial answer failed，Response message = " + strResponse + "." + ex.ToString());
                            SaveDownloadResult(pcbaType, InfoMessage.ErrorInDownloading);
                            return;
                        }
                    }
#endif
                    //DLD_Init end

                   FileStream fs = new FileStream(strBinFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                    StreamReader  read = new StreamReader(fs, Encoding.Default);
                    string strReadline = "";

                    // init progress bar
                    int fsLength = (int)fs.Length;
                    BeginInvoke(new Action(() =>
                    {
                        progressBar.Value = 0;
                        progressBar.Maximum = fsLength + 3;
                    }));


                    //transfer hex data 
                    int dataLength = 0;
                    var rowNumber = 0;
                    int readedLength = 0;
                    StringBuilder builderHexData = new StringBuilder();
                    while ((strReadline = read.ReadLine()) != null)
                    {
                        string fsPos = fs.Position.ToString();
                        rowNumber++;

                        Utils.SetTextboxValue(txtFwInfo, prefixLogInfo + "DLD_Hex line number=" + rowNumber.ToString());
                        byte[] SendArray = Utils.StrToByte(strReadline);
                        dataLength = SendArray.Length;
                        string dataType = "";
                        if (strReadline.Length > 8)
                        {
                            dataType = strReadline.Substring(7, 2);
                        }

                        var lineNumber = new byte[2];
                        lineNumber[0] = (byte)(rowNumber & 0xFF);
                        lineNumber[1] = (byte)(rowNumber >> 8);

                        string strLineNumber = Utils.byteToHexStr(lineNumber);
                        var hexLineData = strLineNumber + "00" + strReadline;

                        var hexRetryTime = 0;
                        var hexSendSuccess = false;
                        string hexCommandReply = "";
                        byte[] byteHexAnswer = new byte[8];
                        int hexReplyIndex = 0;

                        do
                        {
                            if (hexRetryTime >= setRetryTimes)
                            {
                                break;
                            }
                            hexCommandReply = SetComment(DLDHexCommand(hexLineData));
                            if (hexCommandReply.Contains("$NG"))
                            {
                                hexSendSuccess = false;
                                commandMessage = Utils.GetCommandResult(hexCommandReply);
                                Utils.SetTextboxValue(txtFwInfo, prefixLogInfo + "Send hex data failed times=" + hexRetryTime + "." + commandMessage);
                            }
                            else
                            {
                                try
                                {
                                    hexReplyIndex = hexCommandReply.IndexOf("$HEX:") + 5;
                                    commandMessage = hexCommandReply.Substring(hexReplyIndex, 2);
                                    byteHexAnswer = Utils.StrToHexByte(commandMessage);

                                    if ((byteHexAnswer[0] & 0x01) == 0x01)
                                    {
                                        hexSendSuccess = true;
                                        readedLength += strReadline.Length + 2;
                                        ProgressShow(readedLength);//20180307
                                        break;
                                    }
                                    else
                                    {
                                        hexSendSuccess = false;
                                        Utils.SetTextboxValue(txtFwInfo, prefixLogInfo + "Send hex data failed times=" + hexRetryTime + "." + commandMessage);
                                    }
                                }
                                catch
                                {
                                    hexSendSuccess = false;
                                    Utils.SetTextboxValue(txtFwInfo, prefixLogInfo + "Parse Initial answer failed，Response message = " + hexCommandReply + "." + commandMessage);
                                    //BeginInvoke(new Action(() => { Connectivity.Enabled = true; tabControl1.Enabled = true; }));
                                    break;
                                }
                            }
                            hexRetryTime++;
                            Thread.Sleep(5);

                        } while (!hexSendSuccess);

                        if (!hexSendSuccess)
                        {
                            //failure
                            ProgressShow(0);//20180307
                            string sendHexError = prefixLogInfo + "Send hex data failed times=" + hexRetryTime + "." + commandMessage;
                            Utils.SetTextboxValue(txtFwInfo, sendHexError);
                            Utils.LogDebug(LogFileName, sendHexError);                   
                            SaveDownloadResult(pcbaType, InfoMessage.ErrorInDownloading);
                            break;
                        }


                        if (dataType.Equals("00"))
                        {//data line 
                            //get data
                            try
                            {
                                if (strReadline.Length > 11)
                                {
                                    string data = strReadline.Substring(9, strReadline.Length - 11);
                                    builderHexData.Append(data);
                                }
                            }
                            catch
                            {
                                Utils.SetTextboxValue(this.txtFwInfo, prefixLogInfo + "Get data from hex line failed,and line is " + strReadline);
                                Utils.LogDebug(LogFileName, pcbaType.ToString() + " Get data from hex line failed,and line is " + strReadline);    
                                SaveDownloadResult(pcbaType, InfoMessage.ErrorInDownloading);
                                break;
                            }
                        }
                        else if (dataType.Equals("01"))
                        {//end line 
                            Utils.SetTextboxValue(txtFwInfo, prefixLogInfo + "DLD_CS");
                            string csCommandReply = SetComment(DLDCSCommand(builderHexData.ToString()));

                            if (csCommandReply.Contains("$NG"))
                            {
                                ProgressShow(0);//20180307
                                Utils.SetTextboxValue(this.txtFwInfo, prefixLogInfo + "Send checksum failed." + Utils.GetCommandResult(csCommandReply));
                                Utils.LogDebug(LogFileName, pcbaType.ToString() + " Send checksum failed." + Utils.GetCommandResult(csCommandReply)); 
                                SaveDownloadResult(pcbaType, InfoMessage.ErrorInDownloading);
                                break;
                            }
                            else
                            {
                                try
                                {
                                    int csReplyIndex = csCommandReply.IndexOf("$CS:") + 4;
                                    commandMessage = csCommandReply.Substring(csReplyIndex, 2);

                                    byte[] byteCSAnswer = Utils.StrToHexByte(commandMessage);

                                    if ((byteCSAnswer[0] & 0x01) == 0x01)
                                    {
                                        readedLength++;
                                        ProgressShow(readedLength);
                                    }
                                    else
                                    {
                                        ProgressShow(0);//20180307
                                        Utils.SetTextboxValue(this.txtFwInfo, prefixLogInfo + "Send checksum failed，Response byte[0] = " + byteCSAnswer[0].ToString());//20180507
                                        Utils.LogDebug(LogFileName, pcbaType.ToString() + " Send checksum failed，Response byte[0] = " + byteCSAnswer[0].ToString()); 
                                        SaveDownloadResult(pcbaType, InfoMessage.ErrorInDownloading);
                                        break;
                                    }
                                }
                                catch (Exception ex)
                                {
                                    ProgressShow(0);//20180307
                                    Utils.SetTextboxValue(this.txtFwInfo, prefixLogInfo + "Send checksum failed，Response message = " + csCommandReply + "." + ex.ToString());
                                    Utils.LogDebug(LogFileName, pcbaType.ToString() + " Send checksum failed，Response message = " + csCommandReply + "." + ex.ToString()); 
                                    SaveDownloadResult(pcbaType, InfoMessage.ErrorInDownloading);
                                    break;
                                }
                            }
                            Thread.Sleep(10);
                            Utils.SetTextboxValue(txtFwInfo, prefixLogInfo + "DLD_Close");
                            string closeReply = SetComment(DLDCloseCommand());


                            if (closeReply.Contains("$NG"))
                            {
                                ProgressShow(0);//20180307
                                Utils.SetTextboxValue(this.txtFwInfo, prefixLogInfo + "Send close failed." + Utils.GetCommandResult(closeReply));
                                Utils.LogDebug(LogFileName, pcbaType.ToString() + " Send close failed." + Utils.GetCommandResult(closeReply)); 
                                SaveDownloadResult(pcbaType, InfoMessage.ErrorInDownloading);
                                break;
                            }
                            else
                            {
                                try
                                {
                                    int closeReplyIndex = closeReply.IndexOf("$CLOSE:") + 7;
                                    commandMessage = closeReply.Substring(closeReplyIndex, 2);

                                    byte[] byteCloseAnswer = Utils.StrToHexByte(commandMessage);

                                    if ((byteCloseAnswer[0] & 0x01) == 0x01)
                                    {
                                        //success
                                        readedLength++;
                                        ProgressShow(readedLength);//20180307
                                        downloadSuccess = true;
                                    }
                                    else
                                    {
                                        //failure
                                        ProgressShow(0);//20180307
                                        Utils.SetTextboxValue(this.txtFwInfo, prefixLogInfo + "Send close failed.");
                                        Utils.LogDebug(LogFileName, pcbaType.ToString() + " Send close failed."); 
                                        SaveDownloadResult(pcbaType, InfoMessage.ErrorInDownloading);
                                        break;
                                    }
                                }
                                catch (Exception ex)
                                {
                                    //failure
                                    ProgressShow(0);//20180307
                                    Utils.SetTextboxValue(this.txtFwInfo, prefixLogInfo + "Send close failed." + ex.ToString());
                                    Utils.LogDebug(LogFileName, pcbaType.ToString() + " Send close failed." + ex.ToString()); 
                                    SaveDownloadResult(pcbaType, InfoMessage.ErrorInDownloading);
                                    break;
                                }
                            }
                            break;
                        }

                    }
                    read.Close();
                    fs.Close();
                    GC.Collect();

                    if (downloadSuccess)
                    {
                        //other pcba show DLD Ok
                        if (pcbaType == PCBAType.CCUGateway)
                        {
                            Utils.SetTextboxValue(txtFwInfo, prefixLogInfo + "DLD Ok.");//20180507
                            ProgressShow(readedLength + 1);//20180307
                            SaveDownloadResult(pcbaType, "updated to CORE." + coreVersion + ".APP." + appVersion + ".");
                            ProgressShow(0);//20180417
                        }
                        else if (pcbaType == PCBAType.CentralControl)
                        {
                            Utils.SetTextboxValue(txtFwInfo, prefixLogInfo + "DLD Ok.");//20180507
                            ProgressShow(readedLength + 1);//20180307
                            Thread.Sleep(3000);
                            Utils.SetTextboxValue(txtFwInfo, prefixLogInfo + "ServMode On");
#if true
                            string servModeReply = SetComment(ServModeCommand());
#else
                        string servModeReply = "$OK$";
#endif
                            try
                            {
                                //$OK$
                                if (servModeReply.ToLower().Contains("ok"))
                                {
                                    Utils.SetTextboxValue(txtFwInfo, prefixLogInfo + " DLD Ok.");
                                    SaveDownloadResult(pcbaType, "updated to CORE." + coreVersion + ".APP." + appVersion + ".");
                                    ProgressShow(0);//20180417
                                }
                                else
                                {
                                    Utils.SetTextboxValue(txtFwInfo, prefixLogInfo + "DLD Reset Error!");
                                    Utils.LogDebug(LogFileName, pcbaType.ToString() + " DLD Reset Error!"); 
                                    SaveDownloadResult(pcbaType, InfoMessage.ErrorInDownloading);
                                    ProgressShow(0);//20180307
                                }
                            }
                            catch
                            {
                                Utils.SetTextboxValue(txtFwInfo, prefixLogInfo + "DLD Reset Error!");
                                Utils.LogDebug(LogFileName, pcbaType.ToString() + " DLD Reset Error!"); 
                                SaveDownloadResult(pcbaType, InfoMessage.ErrorInDownloading);
                                ProgressShow(0);//20180307
                            }
                        }
                        else if (pcbaType == PCBAType.OPMIControl) 
                        {
                            //Utils.SetTextboxValue(txtFwInfo, PCBAType.CCUGateway.ToString() + PCBAType.CentralControl.ToString() + " DLD Ok.");
                            Utils.SetTextboxValue(txtFwInfo, prefixLogInfo + " DLD Ok.");//20180507
                            ProgressShow(readedLength + 1);//20180307
                            SaveDownloadResult(pcbaType, "updated to CORE." + coreVersion + ".APP." + appVersion + ".");
                            ProgressShow(0);//20180417
                        }
                    }
                }
            }
            catch(Exception ex2)
            {
                //failure
                ProgressShow(0);//20180307
                Utils.LogDebug(LogFileName, pcbaType.ToString() + " " + ex2.ToString());
                SaveDownloadResult(pcbaType, InfoMessage.ErrorInDownloading);
            }
            finally
            {             
                isDownloading = false;
            }
        }

        private void SaveDownloadResult(PCBAType type, string strMsg)
        {
            strMsg = type.ToString() + " " + strMsg;
            if (downloadResult.ContainsKey(type))
            {
                downloadResult[type] += strMsg;
            }
            else
            {
                downloadResult.Add(type, strMsg);
            }
        }

        /// <summary>
        /// Set control status
        /// </summary>
        /// <param name="isEnable"></param>
        private void DownloadControlStatus(bool isEnable)
        {
            BeginInvoke(new Action(() =>
           {
               cmbPCBName.Enabled = isEnable;
               btnBrowseHex.Enabled = isEnable;
               btnDownload.Enabled = isEnable;
           }));
        }

        /// <summary>
        /// Show progress bar 
        /// </summary>
        /// <param name="progressValue"></param>
        private void ProgressShow(int progressValue)
        {
            BeginInvoke(new Action(() =>
            {
                progressBar.Value = progressValue;
            }));
        }


        delegate void MessageBoxShow(string msg);
        void MessageBoxDownload(string msg)
        {
            MessageBox.Show(msg, "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void MainMenu_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (isDownloading)
            {
                MessageBox.Show("PCBA is downloading,please wait...", "Warning!");
                e.Cancel = true;
            }
        }

    }
}