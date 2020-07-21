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

namespace service_tool
{
    public partial class OperLogDefineForm : Form
    {
        private string STX = Char.ConvertFromUtf32(2);//Converts the specified Unicode code point into a UTF-16 encoded string.
        private string ETX = Char.ConvertFromUtf32(3);//Converts the specified Unicode code point into a UTF-16 encoded string.
        private string CMD_OPER_LOG = "$LOGO:";//Head of operation Log
        private string CMD_ENT_RD = "ENT:RD:";////Command of read
        private string OK_HDR = "$OK:";//Response of read success
        private string NG_BADPAR = "$NG:BADPAR$";//Error response.Entry index out of range.
        private string NG_BUSY = "$NG:BUSY$";//Error response.EEPROM busy, try again later.
        private string NG_HWERR = "$NG:HWERR$";//Error response.EEPROM operation error.
        private string CMD_STYPE_GET = "$STYPE:GET$";//Get System Type
        private static string notAvailableMode = "00";
        private static string peakMode = "01";//peak Mode
        private static string startMode = "02";//start Mode

        public MainMenu mainPage;
        private System.IO.Ports.SerialPort serialPort1;

        /// <summary>
        /// Constructed function
        /// </summary>
        /// <param name="mainMenu"></param>
        /// <param name="serialPort"></param>
        public OperLogDefineForm(MainMenu mainMenu, System.IO.Ports.SerialPort serialPort)
        {
            InitializeComponent();
            this.MaximizeBox = false;
            this.Text = "Content of Operation Log Entry # " + LogFiles.gOperSelectIndex;
            mainPage = mainMenu;
            serialPort1 = serialPort;
        }

        /// <summary>
        /// Set form style and Initialize the form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OperLogDefineForm_Load(object sender, EventArgs e)
        {
            listViewOperLogDefine.View = View.Details;
            listViewOperLogDefine.GridLines = true;
            listViewOperLogDefine.Columns.Add("Log Definition", 250, HorizontalAlignment.Left);
            listViewOperLogDefine.Columns.Add("Comments", 250, HorizontalAlignment.Left);

            OperLogDate.ReadOnly = true;
            OperLogTime.ReadOnly = true;

            if (LogFiles.gOperSelectIndex.Equals("0"))
            {
                btnOperPrevious.Enabled = false;
            }

            if (Convert.ToUInt32(LogFiles.gOperSelectIndex, 10) == LogFiles.gOperInfoTotaiCount - 1)
            {
                btnOperNext.Enabled = false;
            }

            if (Convert.ToUInt32(LogFiles.gOperSelectIndex, 10) == LogFiles.gLocalOperInfoTotalCount - 1)
            {
                btnOperNext.Enabled = false;
            }
        }

        /// <summary>
        /// Select an operation log ,and get operation log details
        /// </summary>
        /// <param name="gOperIndex"></param>
        public void OperInfoSelected(string gOperIndex)
        {
            string operCmd;
            string operResponse;
            int index = 0;

            try
            {
                if (!serialPort1.IsOpen)
                {
                    MessageBox.Show("Com port is not open!", "Warning!");
                    return;
                }

                string systemType = GetSystemMode();
                if (systemType == "")
                {
                    MessageBox.Show("Get system type failed");
                    return;
                }

                int intOperIndex = Convert.ToInt32(gOperIndex);
                string stringOperIndex = Convert.ToString(intOperIndex, 16);
                stringOperIndex = stringOperIndex.PadLeft(4, '0');
                operCmd = STX + CMD_OPER_LOG + CMD_ENT_RD + stringOperIndex + "$" + ETX;
                serialPort1.Write(operCmd);
                operResponse = serialPort1.ReadTo(ETX);

                index = operResponse.IndexOf("$");
                operResponse = operResponse.Substring(index);
                if (operResponse.Substring(1, 2).Equals("OK"))
                {
                    operResponse = operResponse.TrimStart(OK_HDR.ToCharArray());
                    operResponse = operResponse.TrimEnd('$');
                    OperInfoMean(operResponse, gOperIndex, systemType);
                    textBoxOperInfo.Text = "Read system operation log entry data:" + "OK." + " Index = " + gOperIndex;
                }
                else
                {
                    if (operResponse.Equals(NG_BADPAR))
                    {
                        OperErrorRespon(operResponse);
                    }
                    else if (operResponse.Equals(NG_BUSY))
                    {
                        OperErrorRespon(operResponse);
                    }
                    else if (operResponse.Equals(NG_HWERR))
                    {
                        OperErrorRespon(operResponse);
                    }
                }


            }
            catch
            {
                ;
            }
        }

        /// <summary>
        /// Details of operation log
        /// </summary>
        /// <param name="operationByte"></param>
        /// <param name="gOperIndex"></param>
        public void OperInfoMean(string operationByte, string gOperIndex,string systemType)
        {
            string RGB_Intensity;
            string V_LightIntensity;
            string RGB_ColorType;
            string comAppIP;
            string ModeAndState;
            string FilterLightMode;
            string DockingState;

            string year;
            string month;
            string day;
            string hour;
            string minute;
            string second;
            string millisecond;
            string date;
            string time;

            try
            {
                if (LogFiles.gOperImport)
                {
                    textBoxOperInfo.Text = "Read local operation log entry data:OK. Index = " + LogFiles.gOperSelectIndex;
                }
                ListViewItem listViewOperation = new ListViewItem();

                // get system time
                year = operationByte.Substring(38, 2);
                month = operationByte.Substring(36, 2);
                day = operationByte.Substring(34, 2);
                hour = operationByte.Substring(32, 2);
                minute = operationByte.Substring(30, 2);
                second = operationByte.Substring(28, 2);
                millisecond = operationByte.Substring(26, 2) + operationByte.Substring(24, 2);

                uint year1 = Convert.ToUInt32(year, 16);
                uint year2 = year1 + 2000;
                string year3 = year2 + "";

                uint month1 = Convert.ToUInt32(month, 16);
                string month2 = month1 + "";
                string month3 = month2.PadLeft(2, '0');

                uint day1 = Convert.ToUInt32(day, 16);
                string day2 = day1 + "";
                string day3 = day2.PadLeft(2, '0');

                uint hour1 = Convert.ToUInt32(hour, 16);
                string hour2 = hour1 + "";
                string hour3 = hour2.PadLeft(2, '0');

                uint minute1 = Convert.ToUInt32(minute, 16);
                string minute2 = minute1 + "";
                string minute3 = minute2.PadLeft(2, '0');

                uint second1 = Convert.ToUInt32(second, 16);
                string second2 = second1 + "";
                string second3 = second2.PadLeft(2, '0');

                uint millisecond1 = Convert.ToUInt32(millisecond, 16);
                string millisecond2 = millisecond1 + "";

                date = year3 + month3 + day3;
                time = hour3 + minute3 + second3;
                date = date.Insert(4, "-").Insert(7, "-");
                time = time.Insert(2, ":").Insert(5, ":");

                OperLogDate.Text = date;
                OperLogTime.Text = time;

                this.listViewOperLogDefine.Items.Clear();
                this.listViewOperLogDefine.BeginUpdate();

                uint intOperIndex = Convert.ToUInt32(LogFiles.gOperSelectIndex, 16);
                this.Text = "Content of Operation Log Entry # " + gOperIndex;

                //Payload byte 0:Bit 0~6:RGB Intensity (5~100), Bit7: RGB On/Off (0b1: On, 0b0: Off) 
                RGB_Intensity = operationByte.Substring(0, 2);
                int intRGB_Intensity = Convert.ToInt32((RGB_Intensity), 16);
                string binaryRGB_Intensity = Convert.ToString(intRGB_Intensity, 2);
                binaryRGB_Intensity = binaryRGB_Intensity.PadLeft(8, '0');
                if (binaryRGB_Intensity.Substring(0, 1) == "1")
                {
                    ListViewItem listViewLog = new ListViewItem();
                    listViewLog.Text = "RGB On/Off";
                    listViewLog.SubItems.Add("On");
                    this.listViewOperLogDefine.Items.Add(listViewLog);

                    binaryRGB_Intensity = binaryRGB_Intensity.Substring(binaryRGB_Intensity.Length - 7, 7);
                    binaryRGB_Intensity = binaryRGB_Intensity.PadLeft(8, '0');

                    int intIntensityValue = Convert.ToInt32(binaryRGB_Intensity, 2);
                    string stringIntersityValue = intIntensityValue.ToString();

                    ListViewItem listViewLog1 = new ListViewItem();
                    listViewLog1.Text = "RGB Intensity";
                    listViewLog1.SubItems.Add(stringIntersityValue);
                    this.listViewOperLogDefine.Items.Add(listViewLog1);
                }
                else if (binaryRGB_Intensity.Substring(0, 1) == "0")
                {
                    ListViewItem listViewLog2 = new ListViewItem();
                    listViewLog2.Text = "RGB On/Off";
                    listViewLog2.SubItems.Add("Off");
                    this.listViewOperLogDefine.Items.Add(listViewLog2);

                    ListViewItem listViewLog3 = new ListViewItem();
                    listViewLog3.Text = "RGB Intensity";
                    listViewLog3.SubItems.Add("0");
                    this.listViewOperLogDefine.Items.Add(listViewLog3);
                }


                //Payload byte 1:Bit  0~6:V-Light Intensity (5~100), Bit7: RGB On/Off (0b1: On, 0b0: Off) 
                V_LightIntensity = operationByte.Substring(2, 2);
                int intV_LightIntensity = Convert.ToInt32((V_LightIntensity), 16);
                string binaryV_LightIntensity = Convert.ToString(intV_LightIntensity, 2);
                binaryV_LightIntensity = binaryV_LightIntensity.PadLeft(8, '0');

                if (binaryV_LightIntensity.Substring(0, 1) == "1")
                {
                    ListViewItem listViewLog4 = new ListViewItem();
                    listViewLog4.Text = "V-Light On/Off";
                    listViewLog4.SubItems.Add("On");
                    this.listViewOperLogDefine.Items.Add(listViewLog4);

                    binaryV_LightIntensity = binaryV_LightIntensity.Substring(binaryV_LightIntensity.Length - 7, 7);
                    binaryV_LightIntensity = binaryV_LightIntensity.PadLeft(8, '0');
                    int intIntensityValue = Convert.ToInt32(binaryV_LightIntensity, 2);
                    string stringIntersityValue = intIntensityValue.ToString();

                    ListViewItem listViewLog5 = new ListViewItem();
                    listViewLog5.Text = "V-Light Intensity";
                    listViewLog5.SubItems.Add(stringIntersityValue);
                    this.listViewOperLogDefine.Items.Add(listViewLog5);
                }
                else if (binaryRGB_Intensity.Substring(0, 1) == "0")
                {
                    ListViewItem listViewLog6 = new ListViewItem();
                    listViewLog6.Text = "V-Light On/Off";
                    listViewLog6.SubItems.Add("Off");
                    this.listViewOperLogDefine.Items.Add(listViewLog6);

                    ListViewItem listViewLog7 = new ListViewItem();
                    listViewLog7.Text = "V-Light Intensity";
                    listViewLog7.SubItems.Add("0");
                    this.listViewOperLogDefine.Items.Add(listViewLog7);
                }


                //Payload byte 2:RGB Color Type
                RGB_ColorType = operationByte.Substring(4, 2);
                ListViewItem listViewLog8 = new ListViewItem();
                listViewLog8.Text = "RGB Color Type";

                if (RGB_ColorType.Equals("00"))
                {
                    listViewLog8.SubItems.Add("Invalid");
                    this.listViewOperLogDefine.Items.Add(listViewLog8);
                }
                else if (RGB_ColorType.Equals("01"))
                {
                    listViewLog8.SubItems.Add("Color temperature");
                    this.listViewOperLogDefine.Items.Add(listViewLog8);

                    string colorTempValue1 = operationByte.Substring(6, 2);
                    string colorTempValue2 = operationByte.Substring(8, 2);
                    string colorTempValue = colorTempValue2 + colorTempValue1;
                    int intColorTempValue = Convert.ToInt32(colorTempValue, 16);
                    string stringColorTempValue = Convert.ToString(intColorTempValue);

                    ListViewItem listViewLog9 = new ListViewItem();
                    listViewLog9.Text = "Color temperature value";
                    listViewLog9.SubItems.Add(stringColorTempValue);
                    this.listViewOperLogDefine.Items.Add(listViewLog9);
                }
                else if (RGB_ColorType.Equals("02"))
                {
                    listViewLog8.SubItems.Add("RGB intensity");
                    this.listViewOperLogDefine.Items.Add(listViewLog8);

                    string redIntensityValue = operationByte.Substring(6, 2);
                    string greenIntensityValue = operationByte.Substring(8, 2);
                    string blueIntensityValue = operationByte.Substring(10, 2);
                    int intRedIntensityValue = Convert.ToInt32(redIntensityValue, 16);
                    string stringRedIntensityValue = Convert.ToString(intRedIntensityValue);

                    int intGreenIntensityValue = Convert.ToInt32(greenIntensityValue, 16);
                    string stringGreenIntensityValue = Convert.ToString(intGreenIntensityValue);

                    int intBlueIntensityValue = Convert.ToInt32(blueIntensityValue, 16);
                    string stringBlueIntensityValue = Convert.ToString(intBlueIntensityValue);

                    ListViewItem listViewLog10 = new ListViewItem();
                    listViewLog10.Text = "Red intensity value";
                    listViewLog10.SubItems.Add(stringRedIntensityValue);
                    this.listViewOperLogDefine.Items.Add(listViewLog10);

                    ListViewItem listViewLog11 = new ListViewItem();
                    listViewLog11.Text = "Green intensity value";
                    listViewLog11.SubItems.Add(stringGreenIntensityValue);
                    this.listViewOperLogDefine.Items.Add(listViewLog11);

                    ListViewItem listViewLog12 = new ListViewItem();
                    listViewLog12.Text = "Blue intensity value";
                    listViewLog12.SubItems.Add(stringBlueIntensityValue);
                    this.listViewOperLogDefine.Items.Add(listViewLog12);

                }
                else if (RGB_ColorType.Equals("03"))
                {
                    listViewLog8.SubItems.Add("Color coordinate");
                    this.listViewOperLogDefine.Items.Add(listViewLog8);

                    string Xcoordinate1 = operationByte.Substring(6, 2);
                    string Xcoordinate2 = operationByte.Substring(8, 2);
                    string Xcoordinate = Xcoordinate2 + Xcoordinate1;

                    string Ycoordinate1 = operationByte.Substring(10, 2);
                    string Ycoordinate2 = operationByte.Substring(12, 2);
                    string Ycoordinate = Ycoordinate2 + Ycoordinate1;

                    float intXValue = (Convert.ToInt32(Xcoordinate, 16));
                    intXValue = intXValue / 1000;
                    string stringXValue = Convert.ToString(intXValue);

                    float intYValue = (Convert.ToInt32(Ycoordinate, 16));
                    intYValue = intYValue / 1000;
                    string stringYValue = Convert.ToString(intYValue);

                    ListViewItem listViewLog13 = new ListViewItem();
                    listViewLog13.Text = "X coordinate";
                    listViewLog13.SubItems.Add(stringXValue);
                    this.listViewOperLogDefine.Items.Add(listViewLog13);

                    ListViewItem listViewLog14 = new ListViewItem();
                    listViewLog14.Text = "Y coordinate";
                    listViewLog14.SubItems.Add(stringYValue);
                    this.listViewOperLogDefine.Items.Add(listViewLog14);
                }

                //Payload byte 7:Filter & Light mode & Docking state
                ModeAndState = operationByte.Substring(14, 2);
                int intModeAndState = Convert.ToInt32((ModeAndState), 16);
                string binaryModeAndState = Convert.ToString(intModeAndState, 2);
                binaryModeAndState = binaryModeAndState.PadLeft(8, '0');
                FilterLightMode = binaryModeAndState.Substring(1, 7);
                DockingState = binaryModeAndState.Substring(0, 1);

                ListViewItem listViewLog15 = new ListViewItem();
                listViewLog15.Text = "Filter&Light Mode";

                //20180115
                if (peakMode == systemType)
                {
                    //Peak "EXTARO 300 Dental"
                    listViewLog15.SubItems.Add(LogFiles.OperModeAnalysis(FilterLightMode, peakMode));
                }
                if (startMode == systemType)
                {
                    //Peak "EXTARO 300 Dental"
                    listViewLog15.SubItems.Add(LogFiles.OperModeAnalysis(FilterLightMode, startMode));
                }
                if (notAvailableMode == systemType)
                {
                    //Peak "EXTARO 300 Dental"
                    listViewLog15.SubItems.Add(LogFiles.OperModeAnalysis(FilterLightMode, notAvailableMode));
                }
                    this.listViewOperLogDefine.Items.Add(listViewLog15);

                //Docking State
                ListViewItem listViewLog16 = new ListViewItem();
                listViewLog16.Text = "Docking State";
                if (DockingState.Equals("0"))
                {
                    listViewLog16.SubItems.Add("Not in Docking Position");
                }
                else if (DockingState.Equals("1"))
                {
                    listViewLog16.SubItems.Add("In Docking Position");
                }
                this.listViewOperLogDefine.Items.Add(listViewLog16);


                //IP
                int comAppIPFir = Convert.ToInt32(operationByte.Substring(22, 2), 16);
                string stringcomAppIPFir = Convert.ToString(comAppIPFir);
                int comAppIPSec = Convert.ToInt32(operationByte.Substring(20, 2), 16);
                string stringcomAppIPSec = Convert.ToString(comAppIPSec);
                int comAppIPThi = Convert.ToInt32(operationByte.Substring(18, 2), 16);
                string stringcomAppIPThi = Convert.ToString(comAppIPThi);
                int comAppIPFou = Convert.ToInt32(operationByte.Substring(16, 2), 16);
                string stringcomAppIPFou = Convert.ToString(comAppIPFou);
                comAppIP = comAppIPFou + ". " + comAppIPThi + ". " + comAppIPSec + ". " + comAppIPFir;
                ListViewItem listViewLog17 = new ListViewItem();
                listViewLog17.Text = "Connected APP IP";
                listViewLog17.SubItems.Add(comAppIP);
                this.listViewOperLogDefine.Items.Add(listViewLog17);
                this.listViewOperLogDefine.EndUpdate();
            }
            catch
            {
                ;
            }
        }

        /// <summary>
        /// Previous error log
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOperPrevious_Click(object sender, EventArgs e)
        {
            string operCmd;
            string operResponse;
            int index = 0;
            try
            {
                btnOperNext.Enabled = true;
                int intOperIndex = Convert.ToInt32(LogFiles.gOperSelectIndex, 10);
                intOperIndex = intOperIndex - 1;
                
                //Port connect
                if (!LogFiles.gOperImport)
                {
                    if (!serialPort1.IsOpen)
                    {
                        MessageBox.Show("Com port is not open!", "Warning!");
                        return;
                    }

                    string systemType = GetSystemMode();
                    if (systemType == "")
                    {
                        MessageBox.Show("Get system type failed");
                        return;
                    }

                    if (intOperIndex >= 0)
                    {
                        LogFiles.gOperSelectIndex = Convert.ToString(intOperIndex, 16);
                        string stringOperIndex = LogFiles.gOperSelectIndex.PadLeft(4, '0');

                        int inttemp = Convert.ToInt32(LogFiles.gOperSelectIndex, 16);
                        string stringtemp = inttemp.ToString();
                        LogFiles.gOperSelectIndex = stringtemp;

                        operCmd = STX + CMD_OPER_LOG + CMD_ENT_RD + stringOperIndex + "$" + ETX;
                        serialPort1.Write(operCmd);
                        operResponse = serialPort1.ReadTo(ETX);

                        index = operResponse.IndexOf("$");
                        operResponse = operResponse.Substring(index);
                        if (operResponse.Substring(1, 2).Equals("OK"))
                        {
                            operResponse = operResponse.TrimStart(OK_HDR.ToCharArray());
                            operResponse = operResponse.TrimEnd('$');
                            OperInfoMean(operResponse, stringtemp, systemType);
                            textBoxOperInfo.Text = "Read system operation log entry data:" + "OK." + " Index = " + LogFiles.gOperSelectIndex;
                        }
                        else
                        {
                            if (operResponse.Equals(NG_BADPAR))
                            {
                                OperErrorRespon(operResponse);
                            }
                            else if (operResponse.Equals(NG_BUSY))
                            {
                                OperErrorRespon(operResponse);
                            }
                            else if (operResponse.Equals(NG_HWERR))
                            {
                                OperErrorRespon(operResponse);
                            }
                        }
                        if (intOperIndex == 0)
                        {
                            btnOperPrevious.Enabled = false;
                        }
                    }

                }
                //Local
                if (LogFiles.gOperImport)
                {
                    if (intOperIndex >= 0)
                    {
                        string stringIndextemp = intOperIndex.ToString();
                        LogFiles.gOperSelectIndex = stringIndextemp;
                        //OperInfoMean(LogFiles.operLogList[intOperIndex], LogFiles.gOperSelectIndex);
                        //textBoxOperInfo.Text = "Read Local Operation Log Entry Data:" + "OK." + " Index = " + LogFiles.gOperSelectIndex;
                        ImportCheck(LogFiles.operLogList[intOperIndex], LogFiles.gOperSelectIndex);
                    }
                    if (intOperIndex == 0)
                    {
                        btnOperPrevious.Enabled = false;
                    }
                }

            }
            catch
            {
                ;
            }
        }

        /// <summary>
        /// Get system mode
        /// </summary>
        /// <returns></returns>
        private string GetSystemMode()
        {
            //20180115
            string cmd = STX + CMD_STYPE_GET + ETX;
            serialPort1.Write(cmd);
            string response = serialPort1.ReadTo(ETX);
            response = response.Substring(response.IndexOf(STX) + 1);
            string systemType = "";
            if(response == OK_HDR + "00$")
            {
                systemType = notAvailableMode;       
            }
            else if (response == OK_HDR + "01$")
            {
                //Peak "EXTARO 300 Dental"
                systemType = peakMode;       //"EXTARO 300"
            }
            else if (response == OK_HDR + "02$")
            {
                //Start "EXTARO 300 ENT";
                systemType = startMode;      //"EXTARO 300 ENT"
            }

            return systemType;
        }


        /// <summary>
        /// Check
        /// </summary>
        /// <param name="logInfo"></param>
        /// <param name="gOperSelectIndex"></param>
        private void ImportCheck(string logInfo, string gOperSelectIndex)
        {

            string stringCheck = logInfo.Substring(logInfo.Length - 2);
            string operResponse = logInfo.Substring(0, logInfo.Length - 2);
            LogFiles logFiles = new LogFiles();
            string checkSum = logFiles.CheckSum(operResponse);
            if (stringCheck == checkSum)
            {
                OperInfoMean(logInfo, LogFiles.gOperSelectIndex, LogFiles.localSystemType);
                textBoxOperInfo.Text = "Read local operation log entry data:" + "OK." + " Index = " + LogFiles.gOperSelectIndex;
            }
            else
            {
                //this.dataGridViewDownload.Rows.Clear();
                this.listViewOperLogDefine.Items.Clear();
                OperLogDate.Text = "--";
                OperLogTime.Text = "--";
                textBoxOperInfo.Text = "Data Corrupted";
                this.Text = "Content of Operation Log Entry # " + LogFiles.gOperSelectIndex;
            }
        }

        /// <summary>
        /// Next error log
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOperNext_Click(object sender, EventArgs e)
        {
            string operCmd;
            string operResponse;
            int index = 0;

            try
            {
                btnOperPrevious.Enabled = true;
                int intOperIndex = Convert.ToInt32(LogFiles.gOperSelectIndex, 10);
                intOperIndex = intOperIndex + 1;

                //Port connect
                if (!LogFiles.gOperImport)
                {
                    if (!serialPort1.IsOpen)
                    {
                        MessageBox.Show("Com port is not open!", "Warning!");
                        return;
                    }
                    //20170115
                    string systemType = GetSystemMode();
                    if(systemType == "")
                    {
                        MessageBox.Show("Get system type failed");
                        return;
                    }

                    if (intOperIndex < LogFiles.gOperInfoTotaiCount)
                    {
                        LogFiles.gOperSelectIndex = Convert.ToString(intOperIndex, 16);
                        string stringOperIndex = LogFiles.gOperSelectIndex.PadLeft(4, '0');

                        int inttemp = Convert.ToInt32(LogFiles.gOperSelectIndex, 16);
                        string stringIndextemp = inttemp.ToString();
                        LogFiles.gOperSelectIndex = stringIndextemp;


                        operCmd = STX + CMD_OPER_LOG + CMD_ENT_RD + stringOperIndex + "$" + ETX;
                        serialPort1.Write(operCmd);
                        operResponse = serialPort1.ReadTo(ETX);

                        index = operResponse.IndexOf("$");
                        operResponse = operResponse.Substring(index);
                        if (operResponse.Substring(1, 2).Equals("OK"))
                        {
                            operResponse = operResponse.TrimStart(OK_HDR.ToCharArray());
                            operResponse = operResponse.TrimEnd('$');
                            OperInfoMean(operResponse, stringIndextemp, systemType);
                            textBoxOperInfo.Text = "Read system operation log entry data:" + "OK." + "Index = " + LogFiles.gOperSelectIndex;
                        }
                        else
                        {
                            if (operResponse.Equals(NG_BADPAR))
                            {
                                OperErrorRespon(operResponse);
                            }
                            else if (operResponse.Equals(NG_BUSY))
                            {
                                OperErrorRespon(operResponse);
                            }
                            else if (operResponse.Equals(NG_HWERR))
                            {
                                OperErrorRespon(operResponse);
                            }
                        }

                        if (intOperIndex == LogFiles.gOperInfoTotaiCount - 1)
                        {
                            btnOperNext.Enabled = false;
                        }


                    }
                }
                //Local
                if (LogFiles.gOperImport)
                {
                    if (intOperIndex < LogFiles.gLocalOperInfoTotalCount)
                    {
                        string stringIndextemp = intOperIndex.ToString();
                        LogFiles.gOperSelectIndex = stringIndextemp;
                        // OperInfoMean(LogFiles.operLogList[intOperIndex], LogFiles.gOperSelectIndex);
                        //textBoxOperInfo.Text = "Read Local Operation Log Entry Data:" + "OK." + " Index = " + LogFiles.gOperSelectIndex;
                        ImportCheck(LogFiles.operLogList[intOperIndex], LogFiles.gOperSelectIndex);
                    }
                    if (intOperIndex == LogFiles.gLocalOperInfoTotalCount - 1)
                    {
                        btnOperNext.Enabled = false;
                    }
                }

            }
            catch
            {
                ;
            }
        }


        /// <summary>
        /// Get operation logfailed
        /// </summary>
        /// <param name="operResponse"></param>
        private void OperErrorRespon(string operResponse)
        {
            this.listViewOperLogDefine.Items.Clear();
            OperLogDate.Text = "--";
            OperLogTime.Text = "--";
            textBoxOperInfo.Text = "Read system operation log entry data:" + Utils.GetCommandResult(operResponse) + ". Index = " + LogFiles.gOperSelectIndex;
            this.Text = "Content of Operation Log Entry # " + LogFiles.gOperSelectIndex;
        }

        /// <summary>
        /// Set the appearance of the listview
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listViewOperLogDefine_ColumnWidthChanging(object sender, ColumnWidthChangingEventArgs e)
        {
            e.Cancel = true;
            e.NewWidth = this.listViewOperLogDefine.Columns[e.ColumnIndex].Width;
        }
    }
}
