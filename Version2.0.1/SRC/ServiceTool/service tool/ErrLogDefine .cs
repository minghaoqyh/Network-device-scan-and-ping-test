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
    public partial class ErrLogDefineForm : Form
    {
        string STX = Char.ConvertFromUtf32(2);//Converts the specified Unicode code point into a UTF-16 encoded string.
        string ETX = Char.ConvertFromUtf32(3);//Converts the specified Unicode code point into a UTF-16 encoded string.
        string CMD_ERR_LOG = "$LOGE:";//Head of Error Log
        string CMD_ENT_RD = "ENT:RD:";//Command of read
        string OK_HDR = "$OK:";//Response of read success
        string NG_BADPAR = "$NG:BADPAR$";//Error response.Entry index out of range.
        string NG_BUSY = "$NG:BUSY$";//Error response.EEPROM busy, try again later.
        string NG_HWERR = "$NG:HWERR$";//Error response.EEPROM operation error.

        public MainMenu mainPage;
        private System.IO.Ports.SerialPort serialPort1;

        /// <summary>
        /// Constructed function
        /// </summary>
        /// <param name="mainMenu"></param>
        /// <param name="serialPort"></param>
        public ErrLogDefineForm(MainMenu mainMenu, System.IO.Ports.SerialPort serialPort)
        {
            InitializeComponent();
            this.MaximizeBox = false;
            this.Text = "Content of Error Log Entry # " + LogFiles.gErrSelectIndex;
            mainPage = mainMenu;
            serialPort1 = serialPort;
        }

        /// <summary>
        /// Set form style and Initialize the form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LogDefine_Load(object sender, EventArgs e)
        {
            ErrLogDate.ReadOnly = true;
            ErrLogTime.ReadOnly = true;

            if (LogFiles.gErrSelectIndex.Equals("0"))
            {
                btnErrPrevious.Enabled = false;
            }

            if (Convert.ToUInt32(LogFiles.gErrSelectIndex, 10) == LogFiles.gErrInfoTotalCount - 1)
            {
                btnErrNext.Enabled = false;
            }
            if (Convert.ToUInt32(LogFiles.gErrSelectIndex, 10) == LogFiles.gLocalErrInfoTotalCount - 1)
            {
                btnErrNext.Enabled = false;
            }

            //20170713
            this.dataGridViewDownload.AllowUserToAddRows = false;//Do not allow add a line
            this.dataGridViewDownload.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft; //Header middle center   
            this.dataGridViewDownload.MultiSelect = false;//Do not allow multiple rows to be selected
            this.dataGridViewDownload.SelectionMode = DataGridViewSelectionMode.FullRowSelect;//Choose a line
            this.dataGridViewDownload.AllowUserToResizeRows = false;//Can not change the line height
            this.dataGridViewDownload.DefaultCellStyle.Font = new Font("Microsoft Sans Serif", 9);
            this.dataGridViewDownload.ClearSelection();
            this.dataGridViewDownload.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            this.dataGridViewDownload.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            this.dataGridViewDownload.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
        }

        /// <summary>
        /// Select an error log ,and get error log details
        /// </summary>
        /// <param name="gErrIndex"></param>
        public void ErrInfoSelected(string gErrIndex)
        {
            string errCmd;
            string errResponse;
            int index = 0;

            try
            {
                if (!serialPort1.IsOpen)
                {
                    MessageBox.Show("Com port is not open!", "Warning!");
                    return;
                }

                int intErrIndex = Convert.ToInt32(gErrIndex);
                string stringErrIndex = Convert.ToString(intErrIndex, 16);
                stringErrIndex = stringErrIndex.PadLeft(4, '0');
                errCmd = STX + CMD_ERR_LOG + CMD_ENT_RD + stringErrIndex + "$" + ETX;
                serialPort1.Write(errCmd);
                errResponse = serialPort1.ReadTo(ETX);
                index = errResponse.IndexOf("$");
                errResponse = errResponse.Substring(index);

                if (errResponse.Substring(1, 2).Equals("OK"))
                {
                    errResponse = errResponse.TrimStart(OK_HDR.ToCharArray());
                    errResponse = errResponse.TrimEnd('$');
                    ErrInfoMean(errResponse, gErrIndex);
                    textBoxErrInfo.Text = "Read system error log entry data:OK. Index = " + gErrIndex;
                }
                else
                {
                    if (errResponse.Equals(NG_BADPAR))
                    {
                        ErrErrorRespon(errResponse);

                    }
                    else if (errResponse.Equals(NG_BUSY))
                    {
                        ErrErrorRespon(errResponse);

                    }
                    else if (errResponse.Equals(NG_HWERR))
                    {
                        ErrErrorRespon(errResponse);

                    }
                }
            }
            catch
            {
                //MessageBox.Show(ex.ToString());
            }
        }


        /// <summary>
        /// Details of error log
        /// </summary>
        /// <param name="errorInfo"></param>
        /// <param name="gErrIndex"></param>
        public void ErrInfoMean(string errorInfo, string gErrIndex)
        {
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
                if (LogFiles.gErrImport)
                {
                    textBoxErrInfo.Text = "Read local error log entry data:OK. Index = " + LogFiles.gErrSelectIndex;
                }

                // get system time
                year = errorInfo.Substring(22, 2);
                month = errorInfo.Substring(20, 2);
                day = errorInfo.Substring(18, 2);
                hour = errorInfo.Substring(16, 2);
                minute = errorInfo.Substring(14, 2);
                second = errorInfo.Substring(12, 2);
                millisecond = errorInfo.Substring(10, 2) + errorInfo.Substring(8, 2);

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

                ErrLogDate.Text = date;
                ErrLogTime.Text = time;

                //Error define
                string errorByte0 = errorInfo.Substring(0, 2);
                string errorByte1 = errorInfo.Substring(2, 2);
                string errorByte2 = errorInfo.Substring(4, 2);
                string errorByte3 = errorInfo.Substring(6, 2);
                string errorByte = errorByte3 + errorByte2 + errorByte1 + errorByte0;

                //errorByte = "FFFFFFFF";//test Delete
                //errorByte = errorInfo.Substring(0, 8);

                int intErrlog = Convert.ToInt32(errorByte, 16);
                string binaryErrLog = Convert.ToString(intErrlog, 2);
                binaryErrLog = binaryErrLog.PadLeft(32, '0');

                uint intErrIndex = Convert.ToUInt32(LogFiles.gErrSelectIndex, 16);
                this.Text = "Content of Error Log Entry # " + gErrIndex;
                //binaryErrLog = "1234567890qwertyuiopasdfghjklzxc"; // for test

                //1 OPMI Multi-functional knob error
                string knobErr = binaryErrLog.Substring(binaryErrLog.Length - 2, 2);//OPMI Multi-functional knob error 
                if (knobErr.Equals("00"))
                {
                    ;
                }
                else if (knobErr.Equals("01"))
                {
                    dataGridViewDownload.Rows.Add("E020201", "OPMI multi-functional knob stuck",
                        "Check whether OPMI multi-fuctional knob gets physically stuck."                   
                        + System.Environment.NewLine + "Check whether multi-functional knob cable gets clamped by metal parts."
                        + System.Environment.NewLine + "Replace the multi-functional knob if necessary.");
                }
                else if (knobErr.Equals("10"))
                {
                    dataGridViewDownload.Rows.Add("E020202", "OPMI multi-functional knob not connected or not powered",
                        "If OPMI LED indicators are visible:"
                        + System.Environment.NewLine + "- Check cable connection between OPMI Control PCBA and multi-functional knob."
                        + System.Environment.NewLine + "If OPMI LED indicators are invisible, +5V DC power is not supplied to OPMI:"
                        + System.Environment.NewLine + "- Check cable connections between Yoke Coupling PCBA and OPMI Control PCBA."
                        + System.Environment.NewLine + "- If error persists after restoring connection, replace the Yoke Coupling PCBA.");
                }
                else if (knobErr.Equals("11"))
                {
                    ;
                }

                //2 Communication to indicator PCBA error
                string indicatorPCBAErr = binaryErrLog.Substring(29, 1);//Communication to indicator PCBA error
                if (indicatorPCBAErr.Equals("0"))
                {
                    ;
                }
                else if (indicatorPCBAErr.Equals("1"))
                {
                    dataGridViewDownload.Rows.Add("E020101", "Communication error between OPMI Control PCBA and the Indicator PCBA",
                        "Check cable connection between OPMI Control PCBA and Indicator PCBA."
                        + System.Environment.NewLine + "If error persists after restoring connection, replace Indicator PCBA.");
                }

                //3 OPMI Filter wheel driving motor error
                string drivingMotorErr = binaryErrLog.Substring(27, 2);//OPMI Filter wheel driving motor error
                if (drivingMotorErr.Equals("00"))
                {
                    ;
                }
                else if (drivingMotorErr.Equals("01"))
                {
                    dataGridViewDownload.Rows.Add("E020301", "OPMI filter wheel driving motor open circuit",
                        "Check cable connection of the filter wheel driving motor."
                        + System.Environment.NewLine + "Replace the filter wheel driving motor if necessary.");
                }
                else if (drivingMotorErr.Equals("10"))
                {
                    dataGridViewDownload.Rows.Add("E020302", "OPMI filter wheel driving motor over current or short circuit",
                        "Check whether OPMI filter wheel structure is physically blocked."
                        + System.Environment.NewLine + "Check cable connection of the filter wheel driving motor."
                        + System.Environment.NewLine + "Replace the filter wheel driving motor and/or the filter wheel assembly if necessary.");
                }
                else if (drivingMotorErr.Equals("11"))
                {
                    dataGridViewDownload.Rows.Add("E020303", "OPMI filter wheel driving motor stalls",
                        "Check whether OPMI filter wheel structure is physically blocked."
                        + System.Environment.NewLine + "Replace the filter wheel assembly if necessary.");
                }

                //4 Filter position sensors error
                string sensorsErr = binaryErrLog.Substring(26, 1);//Filter position sensors error
                if (sensorsErr.Equals("0"))
                {
                    ;
                }
                else if (sensorsErr.Equals("1"))
                {
                    dataGridViewDownload.Rows.Add("E020304", "OPMI filter wheel position sensors get unexpected readings",
                        "Check whether filter wheel driving gears and transmission get loose."
                        + System.Environment.NewLine + "Check cable connection of the filter wheel assembly."
                        + System.Environment.NewLine + "Blow away the dust on the filter wheel position sensors (opto-switch)."
                        + System.Environment.NewLine + "Replace the filter wheel assembly if necessary.");
                }
                
                //5 this bit should be reserved according the FW code  /*bit 6*/
                string LedPCBAErr = binaryErrLog.Substring(25, 1);//Communication to Panorama LED PCBA error// this comment should be ignore
                if (LedPCBAErr.Equals("0"))
                {
                    ;
                }
                else if (LedPCBAErr.Equals("1"))
                {
                    dataGridViewDownload.Rows.Add("Unknown", "--", "--");
                }
                // 6 OPMI Spot Illumination motor error /* bit 7 */
                string SpotIllumMotorErr = binaryErrLog.Substring(16, 1);
                if (SpotIllumMotorErr.Equals("0"))
                {
                    ;
                }
                else if (SpotIllumMotorErr.Equals("1"))
                {
                    dataGridViewDownload.Rows.Add("E020501", "OPMI Illumination aperture motor has some problem",
                        "Check cable connection between OPMI Control PCBA and illumination aperture  motor."
                        + System.Environment.NewLine + "If error persists after restoring connection, replace OPMI control PCBA.");
                }
                //7 OPMI Star swivel axis balancing motor error  /* bit 8 , 9 */
                //this error bit sequence was modified by Yunhe Qin according the FW Code
                string swivelBalanceMotorErr = binaryErrLog.Substring(21, 2);//OPMI Star swivel axis balancing motor error
                if (swivelBalanceMotorErr.Equals("00"))
                {
                    ;
                }
                else if (swivelBalanceMotorErr.Equals("01"))
                {
#if true
                    dataGridViewDownload.Rows.Add("Unknown", "--", "--");
#else
                    dataGridViewDownload.Rows.Add("E020301", "OPMI filter wheel driving motor open circuit",
                        "Check cable connection of the filter wheel driving motor."
                        + System.Environment.NewLine + "Replace the filter wheel driving motor if necessary.");
#endif
                }
                else if (swivelBalanceMotorErr.Equals("10"))
                {
#if true
                    dataGridViewDownload.Rows.Add("Unknown", "--", "--");
#else
                    dataGridViewDownload.Rows.Add("E020302", "OPMI filter wheel driving motor over current or short circuit",
                        "Check whether OPMI filter wheel structure is physically blocked."
                        + System.Environment.NewLine + "Check cable connection of the filter wheel driving motor."
                        + System.Environment.NewLine + "Replace the filter wheel driving motor and/or the filter wheel assembly if necessary.");
#endif
                }
                else if (swivelBalanceMotorErr.Equals("11"))
                {
                    dataGridViewDownload.Rows.Add("Unknown", "--", "--");
                }

                //8 Tilt axis balance motor error this error bit define modified by Yunhe Qin according the FW code /* bit 10 , 11 */
                string tiltBalanceMotorErr = binaryErrLog.Substring(23, 2);//Tilt axis balance motor error
                if (tiltBalanceMotorErr.Equals("00"))
                {
                    ;
                }
                else if (tiltBalanceMotorErr.Equals("01"))
                {
                    dataGridViewDownload.Rows.Add("Unknown", "--", "--");
                }
                else if (tiltBalanceMotorErr.Equals("10"))
                {
                    dataGridViewDownload.Rows.Add("Unknown", "--", "--");
                }
                else if (tiltBalanceMotorErr.Equals("11"))
                {
                    dataGridViewDownload.Rows.Add("Unknown", "--", "--");
                }
               
                // 9 Communication to yoke PCBA error /* bit 12 */
                //this error bit sequence was modified by Yunhe Qin according the FW Code
                string yokePCBAErr = binaryErrLog.Substring(20, 1);//Communication to yoke PCBA error
                if (yokePCBAErr.Equals("0"))
                {
                    ;
                }
                else if (yokePCBAErr.Equals("1"))
                {
                    dataGridViewDownload.Rows.Add("E020102", "Communication error between OPMI Control PCBA and the Yoke Coupling PCBA",
                        "Check cable connections between Yoke Coupling PCBA and OPMI Control PCBA.");
                }
                //10  OPMI Panorama LED PCBA communication loss /* bit 13 */
                string panoPCBAErr = binaryErrLog.Substring(19, 1);//Communication to yoke PCBA error
                if (panoPCBAErr.Equals("0"))
                {
                    ;
                }
                else if (panoPCBAErr.Equals("1"))
                {
                    dataGridViewDownload.Rows.Add("Unknown", "--", "--");
                }
                //11 OPMI Panorama LED error /*bit 14, 15 */
                string panoramaLedErr = binaryErrLog.Substring(17, 2);//OPMI Panorama LED error
                if (panoramaLedErr.Equals("00"))
                {
                    ;
                }
                else if (panoramaLedErr.Equals("01"))
                {
                    dataGridViewDownload.Rows.Add("Unknown", "--", "--");
                }
                else if (panoramaLedErr.Equals("10"))
                {
                    dataGridViewDownload.Rows.Add("Unknown", "--", "--");
                }
                else if (panoramaLedErr.Equals("11"))
                {
                    dataGridViewDownload.Rows.Add("Unknown", "--", "--");
                }

                //12 Helios fatal error
                string heliosFatalErr = binaryErrLog.Substring(15, 1);//Helios fatal error
                if (heliosFatalErr.Equals("0"))
                {
                    ;
                }
                else if (heliosFatalErr.Equals("1"))
                {
                    dataGridViewDownload.Rows.Add("E030101", "LED light source initialization error or fatal error",
                        "If V-Light module is not installed, make sure that the dip switch on the LED light source is set to \"V-Light not implemented\" position."
                        + System.Environment.NewLine + "If error persists after rectifying setting, replace the LED light source RGB module or V-Light module.");
                }

                //13 Helios RGB error
                string heliosRGBErr = binaryErrLog.Substring(14, 1);//Helios RGB error
                if (heliosRGBErr.Equals("0"))
                {
                    ;
                }
                else if (heliosRGBErr.Equals("1"))
                {
                    dataGridViewDownload.Rows.Add("E030201", "Error with LED light source RGB module", "Replace light source RGB module.");
                }

                //14 Helios V-Light unit error
                string heliosVlightErr = binaryErrLog.Substring(13, 1);//Helios V-Light unit error
                if (heliosVlightErr.Equals("0"))
                {
                    ;
                }
                else if (heliosVlightErr.Equals("1"))
                {
                    dataGridViewDownload.Rows.Add("E030301", "Error with LED light source V-Light module", "Replace light source V-Light module.");
                }

                //15 Helios driving voltage error
                string heliosDriveVoltageErr = binaryErrLog.Substring(12, 1);//Helios driving voltage error
                if (heliosDriveVoltageErr.Equals("0"))
                {
                    ;
                }
                else if (heliosDriveVoltageErr.Equals("1"))
                {
                    dataGridViewDownload.Rows.Add("E030102", "LED light source driving voltage (+24V) gets out of range",
                        "Ignore if error happens intermittently."
                        + System.Environment.NewLine + "If error persists, check system DC voltage (+24V) while disconnecting LED light source:"
                        + System.Environment.NewLine + "- If +24V voltage is within range (+/-1V), replace the LED light source RGB module."
                        + System.Environment.NewLine + "- If +24V voltage is not within range, replace the AC/DC module.");
                }

                //16 Helios illumination setting error
                ListViewItem listViewLog15 = new ListViewItem();

                string heliosIlluminationSetErr = binaryErrLog.Substring(11, 1);//Helios illumination setting error
                if (heliosIlluminationSetErr.Equals("0"))
                {
                    ;
                }
                else if (heliosIlluminationSetErr.Equals("1"))
                {
                    dataGridViewDownload.Rows.Add("E030202", "LED light source cannot achieve designated illumination setting",
                        "Ignore."
                        + System.Environment.NewLine + "If error occurs frequently and causes mode transition error, replace LED light source RGB module.");
                }

                //17 Helios over temperature warning (internal temperature > 66˚C)
                string heliosOverTemperatureErr = binaryErrLog.Substring(10, 1);
                if (heliosOverTemperatureErr.Equals("0"))
                {
                    ;
                }
                else if (heliosOverTemperatureErr.Equals("1"))
                {
                    dataGridViewDownload.Rows.Add("E030401", "LED light source over-temperature warning(Internal temperature > 66˚C)",
                       "Turn off system and wait for LED light source to cool down."
                        + System.Environment.NewLine + "Check light source ventilation."
                        + System.Environment.NewLine + "If error persists after restoring ventilation, replace LED light source RGB module.");
                }

                //18 Helios emergency shut-down (internal temperature > 75˚C)
                string heliosShutdownErr = binaryErrLog.Substring(9, 1);
                if (heliosShutdownErr.Equals("0"))
                {
                    ;
                }
                else if (heliosShutdownErr.Equals("1"))
                {
                    dataGridViewDownload.Rows.Add("E030402", "LED light source emergency shut-down(Internal temperature > 75˚C)",
                       "Turn off system and wait for LED light source to cool down."
                        + System.Environment.NewLine + "Check light source ventilation."
                        + System.Environment.NewLine + "If error persists after restoring ventilation, replace LED light source RGB module.");
                }

                //19 Helios temperature control error
                string heliosTemperatureControlErr = binaryErrLog.Substring(8, 1);//Helios temperature control error
                if (heliosTemperatureControlErr.Equals("0"))
                {
                    ;
                }
                else if (heliosTemperatureControlErr.Equals("1"))
                {
                    dataGridViewDownload.Rows.Add("E030403", "LED light source temperature management error",
                       "If V-Light module is not installed, make sure that the dip switch on the LED light source is set to \"V-Light not implemented\" position."
                        + System.Environment.NewLine + "If error persists after rectifying setting, replace the LED light source RGB module.");
                }

                //20 CCU internal error
                string CCUInternalErr = binaryErrLog.Substring(7, 1);//CCU internal error
                if (CCUInternalErr.Equals("0"))
                {
                    ;
                }
                else if (CCUInternalErr.Equals("1"))
                {
                    dataGridViewDownload.Rows.Add("E060101", "CCU internal error", "Replace CCU if error persists.");
                }

                //21 CCU EVR communication error
                string CCU_EVR_CommunicateErr = binaryErrLog.Substring(6, 1);//CCU EVR communication error
                if (CCU_EVR_CommunicateErr.Equals("0"))
                {
                    ;
                }
                else if (CCU_EVR_CommunicateErr.Equals("1"))
                {
                    dataGridViewDownload.Rows.Add("E060102", "CCU internal EVR communication error", "Replace CCU if error persists.");
                }

                //22 CAN communication to OPMI error
                string CAN_OPMI_CommunicateErr = binaryErrLog.Substring(5, 1);//CAN communication to OPMI error
                if (CAN_OPMI_CommunicateErr.Equals("0"))
                {
                    ;
                }
                else if (CAN_OPMI_CommunicateErr.Equals("1"))
                {
                    dataGridViewDownload.Rows.Add("E010201", "CAN bus communication to OPMI lost",
                        "Check cable connection between Central Control PCBA and Yoke Coupling PCBA."
                        + System.Environment.NewLine + "Check cable connection between Yoke Coupling PCBA and OPMI Control PCBA."
                        + System.Environment.NewLine + "Replace OPMI Control PCBA if error persists after restoring connections.");

                }

                //23 CAN communication to Helios error
                string CAN_Helios_CommunicateErr = binaryErrLog.Substring(4, 1);//CAN communication to Helios error
                if (CAN_Helios_CommunicateErr.Equals("0"))
                {
                    ;
                }
                else if (CAN_Helios_CommunicateErr.Equals("1"))
                {
                    dataGridViewDownload.Rows.Add("E010202", "CAN bus communication to LED light source lost",
                        "Check cable connection between Quick Connect PCBA and LED light source."
                        + System.Environment.NewLine + "If there is a 2-pin dip switch on the LED light source CAN-Power connector board, make sure the dip switch is set to \"terminator resistor activated\" position."
                        + System.Environment.NewLine + "Replace LED light source RGB module if error persists after restoring connection and rectifying dip switch setting.");
                }

                //24 CAN communication to CCU error
                string CAN_CCU_CommunicateErr = binaryErrLog.Substring(3, 1);//CAN communication to CCU error
                if (CAN_CCU_CommunicateErr.Equals("0"))
                {
                    ;
                }
                else if (CAN_CCU_CommunicateErr.Equals("1"))
                {
                    dataGridViewDownload.Rows.Add("E010203", "CAN bus communication to CCU lost",
                        "Check cable connection to CCU."
                        + System.Environment.NewLine + "Replace CCU if necessary.");
                }

                //25 System configuration error
                string systemConfigueErr = binaryErrLog.Substring(2, 1);//System configuration error
                if (systemConfigueErr.Equals("0"))
                {
                    ;
                }
                else if (systemConfigueErr.Equals("1"))
                {
                    //20180206
                    dataGridViewDownload.Rows.Add("E010101", "System configuration error",
                       "Make sure that the 2-pin dip switch on the LED light source is set to OFF position."
                        + System.Environment.NewLine + "Make sure there is no confliction between installed license and HW module(Confliction example: License for \"Fluorescence mode\" is installed, but there is no V-Light module).");
                }
                //the bit position 0 and bit position 1 were reserved. //add 20200330
                //MessageBox.Show(dataGridViewDownload.Rows.Count.ToString());
                //this.listViewErrLogDefine.EndUpdate();
                //this.dataGridViewDownload.ClearSelection();
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
        private void btnErrPrevious_Click(object sender, EventArgs e)
        {
            string errCmd;
            string errResponse;
            int index = 0;
            try
            {
                btnErrNext.Enabled = true;
                int intErrIndex = Convert.ToInt32(LogFiles.gErrSelectIndex, 10);
                intErrIndex = intErrIndex - 1;
                //system
                if (!LogFiles.gErrImport)
                {
                    if (!serialPort1.IsOpen)
                    {
                        MessageBox.Show("Com port is not open!", "Warning!");
                        return;
                    }
                    if (intErrIndex >= 0)
                    {
                        this.dataGridViewDownload.Rows.Clear();
                        this.dataGridViewDownload.DataSource = null;
                        LogFiles.gErrSelectIndex = Convert.ToString(intErrIndex, 16);
                        string stringErrIndex = LogFiles.gErrSelectIndex.PadLeft(4, '0');

                        int inttemp = Convert.ToInt32(LogFiles.gErrSelectIndex, 16);
                        string stringtemp = inttemp.ToString();
                        LogFiles.gErrSelectIndex = stringtemp;

                        errCmd = STX + CMD_ERR_LOG + CMD_ENT_RD + stringErrIndex + "$" + ETX;
                        serialPort1.Write(errCmd);
                        errResponse = serialPort1.ReadTo(ETX);
                        index = errResponse.IndexOf("$");
                        errResponse = errResponse.Substring(index);
                        if (errResponse.Substring(1, 2).Equals("OK"))
                        {
                            errResponse = errResponse.TrimStart(OK_HDR.ToCharArray());
                            errResponse = errResponse.TrimEnd('$');
                            ErrInfoMean(errResponse, stringtemp);
                            textBoxErrInfo.Text = "Read system error log entry data:" + "OK." + " Index = " + LogFiles.gErrSelectIndex;
                        }
                        else if (errResponse.Equals(NG_BADPAR) || errResponse.Equals(NG_BUSY) || errResponse.Equals(NG_HWERR))
                        {
                            ErrErrorRespon(errResponse);
                        }

                        if (intErrIndex == 0)
                        {
                            btnErrPrevious.Enabled = false;
                        }
                    }
                }

                //Local
                if (LogFiles.gErrImport)
                {
                    if (intErrIndex >= 0)
                    {
                        this.dataGridViewDownload.Rows.Clear();
                        this.dataGridViewDownload.DataSource = null;
                        string stringIndextemp = intErrIndex.ToString();
                        LogFiles.gErrSelectIndex = stringIndextemp;
                        ImportCheck(LogFiles.errLogList[intErrIndex], stringIndextemp);
                        //ErrInfoMean(LogFiles.errLogList[intErrIndex], LogFiles.gErrSelectIndex);
                        //textBoxErrInfo.Text = "Read Local Error Log Entry Data:" + "OK." + " Index = " + LogFiles.gErrSelectIndex;
                    }
                    if (intErrIndex == 0)
                    {
                        btnErrPrevious.Enabled = false;
                    }
                }
            }
            catch
            {
                ;
            }
            this.dataGridViewDownload.ClearSelection();
        }

        /// <summary>
        /// Next error log
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnErrNext_Click(object sender, EventArgs e)
        {
            string errCmd;
            string errResponse;
            int index = 0;

            try
            {
                btnErrPrevious.Enabled = true;
                int intErrIndex = Convert.ToInt32(LogFiles.gErrSelectIndex, 10);
                intErrIndex = intErrIndex + 1;
                //system
                if (!LogFiles.gErrImport)
                {
                    if (!serialPort1.IsOpen)
                    {
                        MessageBox.Show("Com port is not open!", "Warning!");
                        return;
                    }

                    if (intErrIndex < LogFiles.gErrInfoTotalCount)
                    {
                        this.dataGridViewDownload.Rows.Clear();
                        this.dataGridViewDownload.DataSource = null;
                        LogFiles.gErrSelectIndex = Convert.ToString(intErrIndex, 16);
                        string stringErrIndex = LogFiles.gErrSelectIndex.PadLeft(4, '0');

                        int inttemp = Convert.ToInt32(LogFiles.gErrSelectIndex, 16);
                        string stringIndextemp = inttemp.ToString();
                        LogFiles.gErrSelectIndex = stringIndextemp;

                        errCmd = STX + CMD_ERR_LOG + CMD_ENT_RD + stringErrIndex + "$" + ETX;
                        serialPort1.Write(errCmd);
                        errResponse = serialPort1.ReadTo(ETX);
                        index = errResponse.IndexOf("$");
                        errResponse = errResponse.Substring(index);

                        if (errResponse.Substring(1, 2).Equals("OK"))
                        {
                            errResponse = errResponse.TrimStart(OK_HDR.ToCharArray());
                            errResponse = errResponse.TrimEnd('$');
                            ErrInfoMean(errResponse, stringIndextemp);
                            textBoxErrInfo.Text = "Read system error log entry data:" + "OK" + ". Index = " + LogFiles.gErrSelectIndex;
                        }
                        else if (errResponse.Equals(NG_BADPAR) || errResponse.Equals(NG_BUSY) || errResponse.Equals(NG_HWERR))
                        {
                            ErrErrorRespon(errResponse);
                        }

                        if (intErrIndex == LogFiles.gErrInfoTotalCount - 1)
                        {
                            btnErrNext.Enabled = false;
                        }
                    }
                }

                //Local
                if (LogFiles.gErrImport)
                {
                    if (intErrIndex < LogFiles.gLocalErrInfoTotalCount)
                    {
                        this.dataGridViewDownload.Rows.Clear();
                        this.dataGridViewDownload.DataSource = null;
                        string stringIndextemp = intErrIndex.ToString();
                        LogFiles.gErrSelectIndex = stringIndextemp;
                        ImportCheck(LogFiles.errLogList[intErrIndex], stringIndextemp);
                        //ErrInfoMean(LogFiles.errLogList[intErrIndex], LogFiles.gErrSelectIndex);
                    }
                    if (intErrIndex == LogFiles.gLocalErrInfoTotalCount - 1)
                    {
                        btnErrNext.Enabled = false;
                    }
                }
                this.dataGridViewDownload.ClearSelection();
            }
            catch
            {
                ;
            }
        }

        /// <summary>
        /// Check
        /// </summary>
        /// <param name="logInfo"></param>
        /// <param name="gErrSelectIndex"></param>
        private void ImportCheck(string logInfo, string gErrSelectIndex)
        {
            dataGridViewDownload.ClearSelection();
            string stringCheck = logInfo.Substring(logInfo.Length - 2);
            string errResponse = logInfo.Substring(0, logInfo.Length - 2);
            LogFiles logFiles = new LogFiles();
            string checkSum = logFiles.CheckSum(errResponse);
            if (stringCheck == checkSum)
            {
                ErrInfoMean(logInfo, LogFiles.gErrSelectIndex);
                textBoxErrInfo.Text = "Read local error log entry data:" + "OK." + " Index = " + LogFiles.gErrSelectIndex;
            }
            else
            {
                this.dataGridViewDownload.Rows.Clear();
                dataGridViewDownload.Rows.Add("--", "--", "--");
                //this.listViewErrLogDefine.Items.Clear();
                ErrLogDate.Text = "--";
                ErrLogTime.Text = "--";
                textBoxErrInfo.Text = "Data Corrupted";
                this.Text = "Content of Error Log Entry # " + LogFiles.gErrSelectIndex;
            }
        }

        /// <summary>
        /// Get error logfailed
        /// </summary>
        /// <param name="errResponse"></param>
        private void ErrErrorRespon(string errResponse)
        {
            //this.listViewErrLogDefine.Items.Clear();
            ErrLogDate.Text = "--";
            ErrLogTime.Text = "--";
            textBoxErrInfo.Text = "Read system error log entry data:" + Utils.GetCommandResult(errResponse) + ". Index = " + LogFiles.gErrSelectIndex;
            this.Text = "Content of Error Log Entry # " + LogFiles.gErrSelectIndex;
        }
    }
}
