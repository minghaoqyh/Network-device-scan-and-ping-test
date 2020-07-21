using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.IO;

using System.Text.RegularExpressions;
namespace service_tool
{
    public partial class LogFiles : UserControl
    {
        public MainMenu mainPage;
        public System.IO.Ports.SerialPort serialPort1;

        private string STX = Char.ConvertFromUtf32(2);//Converts the specified Unicode code point into a UTF-16 encoded string.
        private string ETX = Char.ConvertFromUtf32(3);//Converts the specified Unicode code point into a UTF-16 encoded string.
        private string CMD_OPER_LOG = "$LOGO:";//Head of Operation Log
        private string CMD_ERR_LOG = "$LOGE:";//Head of Error Log
        private string CMD_ENT_RD = "ENT:RD:";//Command of read
        private string CMD_OPER_CLEAR = "$LOGO:CLR$";//Clear Operation Log
        private string CMD_ERR_CLEAR = "$LOGE:CLR$";//Clear Error Log
        private string CMD_CNT_RD = "CNT:RD$";//Tail of read
        private string RSP_OK = "$OK$";//Response of log cleared
        private string OK_HDR = "$OK:";//Response of read success
        private string NG_BADPAR = "$NG:BADPAR$";//Error response.Entry index out of range.
        private string NG_BUSY = "$NG:BUSY$";//Error response.EEPROM busy, try again later.
        private string NG_HWERR = "$NG:HWERR$";//Error response.EEPROM operation error.
        private string CMD_STYPE_GET = "$STYPE:GET$";

        private int gOperEntryIndex = 0;//Index of system operation log
        private int gErrEntryIndex = 0;//Index of system error log
        string errorInfo;
        string operationByte;
        public static string gErrSelectIndex;//Index of selected error log 
        public static string gOperSelectIndex;//Index of selected operation log
        public static int gErrInfoTotalCount;//Count of system error log 
        public static int gOperInfoTotaiCount;//Count of system operation log 
        public static int gLocalErrInfoTotalCount;//Count of local error log 
        public static int gLocalOperInfoTotalCount;//Count of local operation log 
        public static bool gErrImport = false;//Flag of import error log
        public static bool gOperImport = false;//Flag of import operation log
        private int gErrLocalEntryIndex = 0;//Index of local operation log
        private int gOperLocalEntryIndex = 0;//Index of local error log
        public static List<string> errLogList = new List<string>();
        public static List<string> operLogList = new List<string>();

        private string localLogPeak = "EXTARO300 Dental";//System Type :Peak Mode//Modify by Qin Yunhe 2020-03-20
        private string oldlocallogpeak = "EXTARO300";//modified by Qin Yunhe 20200321

        private string localLogStart = "EXTARO300 ENT(Star)";//System Type :Start Mode modified by Qin Yunhe 20200321
        private string oldlocalLogStart = "EXTARO300 ENT";//System Type :Start Mode
        
        private string localNotAvailable = "EXTARO300 N.A";//System Type :NotAvailable
        private string localErrLog = "Error Log";//Error Log Flag
        private string localOperLog = "Operation Log";//Operation Log Flag
        private static string notAvailableMode = "00";//System Type :System type not available
        private static string peakMode = "01";//System Type :Peak Mode
        private static string startMode = "02";//System Type :Start Mode
        public static string localSystemType = "";
        //add by Qin yunhe 20200321
        public string strCCBFW = "" ; //20200321
        public string strOPMIFW = "";
        public string strCCUFW = "";
        public string strsystem_SN;
        //private License license1;
        
        //end
        public LogFiles()
        {
            InitializeComponent();

            //Set style for operation listview
            listViewOper.View = View.Details;
            listViewOper.GridLines = true;
            listViewOper.Columns.Add("Item", 40, HorizontalAlignment.Left);
            listViewOper.Columns.Add("Date", 78, HorizontalAlignment.Left);
            listViewOper.Columns.Add("Time", 62, HorizontalAlignment.Left);
            listViewOper.Columns.Add("Mode", 136, HorizontalAlignment.Left);

            //Set style for error listview
            listViewErr.View = View.Details;
            listViewErr.GridLines = true;
            listViewErr.Columns.Add("Item", 45, HorizontalAlignment.Left);
            listViewErr.Columns.Add("Date", 86, HorizontalAlignment.Left);
            listViewErr.Columns.Add("Time", 80, HorizontalAlignment.Left);
            listViewErr.Columns.Add("Error Code", 105, HorizontalAlignment.Left);

            btnOperExport.Enabled = false;
            btnOperImport.Enabled = true;
            btnErrExport.Enabled = false;
            btnErrImport.Enabled = true;


            btnOperFirst.Enabled = false;
            btnOperForward.Enabled = false;
            btnOperBack.Enabled = false;
            btnOperLast.Enabled = false;
            btnOperClean.Enabled = false;
            btnErrFirst.Enabled = false;
            btnErrForward.Enabled = false;
            btnErrBack.Enabled = false;
            btnErrLast.Enabled = false;
            btnErrClean.Enabled = false;

        }


        public void InitializelogFilesComponent(MainMenu mainMenu, System.IO.Ports.SerialPort serialPort)
        {
            mainPage = mainMenu;
            serialPort1 = serialPort;
        }

        /// <summary>
        /// Previous page of operation log
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOperForward_Click(object sender, EventArgs e)
        {
            try
            {
                //Port connect
                if (!gOperImport)
                {
                    if (!serialPort1.IsOpen)
                    {
                        MessageBox.Show("Com port is not open!", "Warning!");
                        return;
                    }

                    if (textBoxOperCount.Text.Equals(""))
                    {
                        MessageBox.Show("The number of messages is empty!", "Warning!");
                        return;
                    }

                    //20180115
                    string systemType = GetSystemMode();
                    if (systemType == "")
                    {
                        MessageBox.Show("Get system type failed!");
                        return;
                    }

                    //Get previous page of system operation log
                    if (gOperEntryIndex != 0)//20180101
                        gOperEntryIndex -= 10;
                    int operInfoCount = Convert.ToInt32(textBoxOperCount.Text);
                    if (((gOperEntryIndex + 10) <= operInfoCount) && (gOperEntryIndex >= 10))
                    {
                        btnOperFirst.Enabled = true;
                        btnOperForward.Enabled = true;
                        btnOperBack.Enabled = true;
                        btnOperLast.Enabled = true;
                        OperListView(gOperEntryIndex, gOperEntryIndex + 10, systemType);
                    }
                    else if (((gOperEntryIndex + 10) <= operInfoCount) && (gOperEntryIndex == 0))
                    {
                        btnOperForward.Enabled = false;
                        btnOperFirst.Enabled = false;
                        btnOperBack.Enabled = true;
                        btnOperLast.Enabled = true;
                        OperListView(gOperEntryIndex, gOperEntryIndex + 10, systemType);
                    }
                }

                //Local operation log
                if (gOperImport)
                {
                    if (textBoxOperCount.Text.Equals(""))
                    {
                        MessageBox.Show("The number of messages is empty!", "Warning!");
                        return;
                    }
                    //Get previous page of local operation log
                    if (gOperLocalEntryIndex != 0)
                        gOperLocalEntryIndex -= 10;
                    this.listViewOper.Items.Clear();
                    this.listViewOper.BeginUpdate();
                    int operInfoCount = Convert.ToInt32(textBoxOperCount.Text);
                    if (((gOperLocalEntryIndex + 10) <= operInfoCount) && (gOperLocalEntryIndex >= 10))
                    {
                        btnOperFirst.Enabled = true;
                        btnOperForward.Enabled = true;
                        btnOperBack.Enabled = true;
                        btnOperLast.Enabled = true;

                        for (int i = 0; i < 10; i++)
                        {
                            OperImportCheck(operLogList[i + gOperLocalEntryIndex], gOperLocalEntryIndex + i, localSystemType);
                        }
                        textBoxLogInfo.Text = "Read local operation log entry data:" + gOperLocalEntryIndex.ToString() + " - " + (gOperLocalEntryIndex + 9).ToString();
                    }
                    else if (((gOperLocalEntryIndex + 10) <= operInfoCount) && (gOperLocalEntryIndex == 0))
                    {
                        btnOperForward.Enabled = false;
                        btnOperFirst.Enabled = false;
                        btnOperBack.Enabled = true;
                        btnOperLast.Enabled = true;
                        for (int i = 0; i < 10; i++)
                        {
                            OperImportCheck(operLogList[i], i, localSystemType);
                        }
                        textBoxLogInfo.Text = "Read local operation log entry data:" + gOperLocalEntryIndex.ToString() + " - " + (gOperLocalEntryIndex + 9).ToString();
                    }
                    this.listViewOper.EndUpdate();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Get operation log failed:" + ex.Message);
            }
        }

        /// <summary>
        /// Get System Mode
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
            if (response == OK_HDR + "00$")
            {
                //"System type not available"
                systemType = notAvailableMode;       //"System type not available"
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
        //this function was added by Qin yunhe to add the FW version in log file
        //2020-03-20
#if false
        private string Get_SystemFWversion(string versionCommand)
        {
            MainMenu mainMenu = new MainMenu();
            //string CCB_cmd = "ICAN:01";
            // string cmd = STX + CMD_STYPE_GET + ETX;
            string strCommand = "$VER:" + versionCommand + "$";
           // string strCommand = "$VER:ICAN:01$";
            string strRespone = mainMenu.GetVersionNum(strCommand);
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
                        string decimalVersionCan = mainMenu.GetDecimalVersionCan(strRespone); 
                        return decimalVersionCan;
                    }
                    else if (strResultFlag.Equals("NG"))
                    {
                        return Utils.GetCommandResult(strRespone);
                    }
                }
                else
                {
                    //tbox.Text = strRespone;
                    return "NON CONNECT";

                }
            }
            catch (Exception ex)
            {
                // tbox.Text = ex.ToString();
                return "ERROR";
            }
            /*try
            {
                //GetVersion.Enabled = false;
                GetVersionCan("ICAN:01", TextboxCurCentral);
                //System.Threading.Thread.Sleep(1000);
                GetVersionCan("ICAN:02", TextboxCurOpmi);
                //System.Threading.Thread.Sleep(1000);
                GetVersionCan("ICAN:06", TextboxCurCCU);
                //GetVersion.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Get version error:" + ex.ToString());
            }*/
            return "ERROR";
        }
#endif

        /// <summary>
        /// Next page of operation log
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOperBack_Click(object sender, EventArgs e)
        {
            try
            {
                //port connect
                if (!gOperImport)
                {
                    if (!serialPort1.IsOpen)
                    {
                        MessageBox.Show("Com port is not open!", "Warning!");
                        return;
                    }

                    if (textBoxOperCount.Text.Equals(""))
                    {
                        MessageBox.Show("The number of messages is empty!", "Warning!");
                        return;
                    }

                    //20180115
                    string systemType = GetSystemMode();
                    if (systemType == "")
                    {
                        MessageBox.Show("Get system type failed");
                        return;
                    }

                    //Get next page of system operation log
                    gOperEntryIndex += 10;
                    int operInfoCount = Convert.ToInt32(textBoxOperCount.Text);
                    if ((gOperEntryIndex + 10) <= operInfoCount)
                    {
                        btnOperForward.Enabled = true;
                        btnOperBack.Enabled = true;
                        btnOperFirst.Enabled = true;
                        btnOperLast.Enabled = true;
                        OperListView(gOperEntryIndex, gOperEntryIndex + 10, systemType);

                        if ((gOperEntryIndex + 10) == operInfoCount)
                        {
                            btnOperBack.Enabled = false;
                            btnOperLast.Enabled = false;
                        }
                    }
                    else if ((gOperEntryIndex <= operInfoCount) && ((gOperEntryIndex + 10) >= operInfoCount))
                    {
                        btnOperFirst.Enabled = true;
                        btnOperForward.Enabled = true;
                        btnOperBack.Enabled = false;
                        btnOperLast.Enabled = false;
                        OperListView(gOperEntryIndex, operInfoCount, systemType);
                    }
                }

                //Local operation log
                if (gOperImport)
                {
                    if (textBoxOperCount.Text.Equals(""))
                    {
                        MessageBox.Show("The number of messages is empty!", "Warning!");
                        return;
                    }

                    //Get next page of local operation log
                    gOperLocalEntryIndex += 10;
                    this.listViewOper.Items.Clear();
                    this.listViewOper.BeginUpdate();
                    int operInfoCount = Convert.ToInt32(textBoxOperCount.Text);
                    if ((gOperLocalEntryIndex + 10) <= operInfoCount)
                    {
                        btnOperForward.Enabled = true;
                        btnOperBack.Enabled = true;
                        btnOperFirst.Enabled = true;
                        btnOperLast.Enabled = true;
                        if ((gOperLocalEntryIndex + 10) == operInfoCount)
                        {
                            btnOperBack.Enabled = false;
                            btnOperLast.Enabled = false;
                        }
                        for (int i = 0; i < 10; i++)
                        {
                            OperImportCheck(operLogList[i + gOperLocalEntryIndex], gOperLocalEntryIndex + i, localSystemType);
                        }
                        textBoxLogInfo.Text = "Read local operation log entry data:" + gOperLocalEntryIndex.ToString() + " - " + (gOperLocalEntryIndex + 9).ToString();
                    }
                    else if ((gOperLocalEntryIndex <= operInfoCount) && ((gOperLocalEntryIndex + 10) >= operInfoCount))
                    {
                        btnOperFirst.Enabled = true;
                        btnOperForward.Enabled = true;
                        btnOperBack.Enabled = false;
                        btnOperLast.Enabled = false;
                        for (int i = gOperLocalEntryIndex; i < operInfoCount; i++)
                        {
                            OperImportCheck(operLogList[i], i, localSystemType);
                        }
                        textBoxLogInfo.Text = "Read local operation log entry data:" + gOperLocalEntryIndex.ToString() + " - " + (operInfoCount - 1).ToString();
                    }
                    this.listViewOper.EndUpdate();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Get operation log failed:" + ex.Message);
            }
        }

        /// <summary>
        /// Clean system operation log
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOperClean_Click(object sender, EventArgs e)
        {
            int index = 0;
            string operClearCmd;
            string operResponse;
            string operCmd;
            try
            {
                if (!serialPort1.IsOpen)
                {
                    MessageBox.Show("Com port is not open!", "Warning!");
                    return;
                }
                if (textBoxOperCount.Text.Equals(""))
                {
                    MessageBox.Show("The number of messages is empty!", "Warning!");
                    return;
                }

                if (textBoxOperCount.Text.Equals("0"))
                {
                    MessageBox.Show("The operation log is empty.", "Warning!");
                    return;
                }

                DialogResult MsgBoxResult;
                MsgBoxResult = MessageBox.Show("You are about to delete the operation log stored in the system. proceed? ", "Warning!", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                if (MsgBoxResult == DialogResult.Yes)
                {
                    //Enter password
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
                        else
                        {
                            btnOperBack.Enabled = false;
                            btnOperForward.Enabled = false;
                            btnOperFirst.Enabled = false;
                            btnOperLast.Enabled = false;
                            gOperEntryIndex = 0;
                            this.listViewOper.Items.Clear();

                            //Send command of clean system log
                            operClearCmd = STX + CMD_OPER_CLEAR + ETX;
                            serialPort1.Write(operClearCmd);
                            operResponse = serialPort1.ReadTo(ETX);
                            index = operResponse.IndexOf("$");
                            operResponse = operResponse.Substring(index);
                            //Clean success
                            if (operResponse.Substring(1, 2).Equals("OK"))
                            {
                                operResponse = operResponse.TrimStart((STX).ToCharArray());
                                if (operResponse.Equals(RSP_OK))
                                {
                                    textBoxLogInfo.Text = "Operation log cleared:" + Utils.GetCommandResult(operResponse.ToString());
                                    operCmd = STX + CMD_OPER_LOG + CMD_CNT_RD + ETX;
                                    serialPort1.Write(operCmd);
                                    operResponse = serialPort1.ReadTo(ETX);
                                    index = operResponse.IndexOf("$");
                                    operResponse = operResponse.Substring(index);

                                    if (operResponse.Substring(1, 2).Equals("OK"))
                                    {
                                        operResponse = operResponse.TrimStart((STX + OK_HDR).ToCharArray());
                                        operResponse = operResponse.TrimEnd('$');
                                        int intOperValue = Convert.ToInt32(operResponse, 16);
                                        var stringOperValue = intOperValue.ToString();
                                        textBoxOperCount.Text = stringOperValue;
                                    }
                                }
                                //Clean failed
                                else if (operResponse.Equals(NG_BUSY) || operResponse.Equals(NG_HWERR))
                                {
                                    textBoxLogInfo.Text = "Operation response:" + Utils.GetCommandResult(operResponse.ToString());
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Clear operation log failed:" + ex.Message);
            }
        }

        /// <summary>
        ///  Previous page of error log
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnErrForward_Click(object sender, EventArgs e)
        {
            try
            {
                //Port connect
                if (!gErrImport)
                {
                    if (!serialPort1.IsOpen)
                    {
                        MessageBox.Show("Com port is not open!", "Warning!");
                        return;
                    }

                    if (textBoxErrCount.Text.Equals(""))
                    {
                        MessageBox.Show("The number of messages is empty!", "Warning!");
                        return;
                    }

                    //Get previous page of system error log
                    int errInfoCount = Convert.ToInt32(textBoxErrCount.Text);
                    if (gErrEntryIndex != 0)
                        gErrEntryIndex -= 10;
                    if (((gErrEntryIndex + 10) <= errInfoCount) && (gErrEntryIndex >= 10))
                    {
                        btnErrFirst.Enabled = true;
                        btnErrForward.Enabled = true;
                        btnErrBack.Enabled = true;
                        btnErrLast.Enabled = true;
                        ErrListView(gErrEntryIndex, gErrEntryIndex + 10);
                    }
                    else if (((gErrEntryIndex + 10) <= errInfoCount) && (gErrEntryIndex == 0))
                    {
                        btnErrForward.Enabled = false;
                        btnErrFirst.Enabled = false;
                        btnErrBack.Enabled = true;
                        btnErrLast.Enabled = true;
                        ErrListView(gErrEntryIndex, gErrEntryIndex + 10);
                    }
                }

                //Local log
                if (gErrImport)
                {
                    if (textBoxErrCount.Text.Equals(""))
                    {
                        MessageBox.Show("The number of messages is empty!", "Warning!");
                        return;
                    }

                    //Get previous page of local error log
                    if (gErrLocalEntryIndex != 0)
                        gErrLocalEntryIndex -= 10;
                    this.listViewErr.Items.Clear();
                    this.listViewErr.BeginUpdate();
                    int errInfoCount = Convert.ToInt32(textBoxErrCount.Text);
                    if (((gErrLocalEntryIndex + 10) <= errInfoCount) && (gErrLocalEntryIndex >= 10))
                    {
                        btnErrFirst.Enabled = true;
                        btnErrForward.Enabled = true;
                        btnErrBack.Enabled = true;
                        btnErrLast.Enabled = true;

                        for (int i = 0; i < 10; i++)
                        {
                            ErrImportCheck(errLogList[i + gErrLocalEntryIndex], gErrLocalEntryIndex + i);
                        }
                        textBoxLogInfo.Text = "Read local error log entry data:" + gErrLocalEntryIndex.ToString() + " - " + (gErrLocalEntryIndex + 9).ToString();
                    }
                    else if (((gErrLocalEntryIndex + 10) <= errInfoCount) && (gErrLocalEntryIndex == 0))
                    {
                        btnErrForward.Enabled = false;
                        btnErrFirst.Enabled = false;
                        btnErrBack.Enabled = true;
                        btnErrLast.Enabled = true;
                        for (int i = 0; i < 10; i++)
                        {
                            ErrImportCheck(errLogList[i], i);
                        }
                        textBoxLogInfo.Text = "Read local error log entry data:" + gErrLocalEntryIndex.ToString() + " - " + (gErrLocalEntryIndex + 9).ToString();

                    }
                    this.listViewErr.EndUpdate();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Get error log failed:" + ex.Message);
            }
        }

        /// <summary>
        /// Next page of error log
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnErrBack_Click(object sender, EventArgs e)// when click the button ">>" 
        {
            try
            {

                //port connect
                if (!gErrImport)
                {
                    if (!serialPort1.IsOpen)
                    {
                        MessageBox.Show("Com port is not open!", "Warning!");
                        return;
                    }

                    if (textBoxErrCount.Text.Equals(""))
                    {
                        MessageBox.Show("The number of messages is empty!", "Warning!");
                        return;
                    }

                    //Get next page of system error log
                    int errInfoCount = Convert.ToInt32(textBoxErrCount.Text);//the count of the error in the log file
                    gErrEntryIndex += 10;

                    if ((gErrEntryIndex + 10) <= errInfoCount)
                    {
                        btnErrForward.Enabled = true;
                        btnErrBack.Enabled = true;
                        btnErrFirst.Enabled = true;
                        btnErrLast.Enabled = true;
                        ErrListView(gErrEntryIndex, gErrEntryIndex + 10);

                        if ((gErrEntryIndex + 10) == errInfoCount)
                        {
                            btnErrBack.Enabled = false;
                            btnErrLast.Enabled = false;
                        }
                    }
                    else if ((gErrEntryIndex <= errInfoCount) && ((gErrEntryIndex + 10) >= errInfoCount))
                    {
                        btnErrFirst.Enabled = true;
                        btnErrForward.Enabled = true;
                        btnErrBack.Enabled = false;
                        btnErrLast.Enabled = false;
                        ErrListView(gErrEntryIndex, errInfoCount);
                    }
                }

                //Local log
                if (gErrImport)
                {
                    if (textBoxErrCount.Text.Equals(""))
                    {
                        MessageBox.Show("The number of messages is empty!", "Warning!");
                        return;
                    }
                    //Get next page of local error log
                    gErrLocalEntryIndex += 10;
                    this.listViewErr.Items.Clear();
                    this.listViewErr.BeginUpdate();
                    int errInfoCount = Convert.ToInt32(textBoxErrCount.Text);
                    if ((gErrLocalEntryIndex + 10) <= errInfoCount)
                    {
                        btnErrForward.Enabled = true;
                        btnErrBack.Enabled = true;
                        btnErrFirst.Enabled = true;
                        btnErrLast.Enabled = true;
                        if ((gErrLocalEntryIndex + 10) == errInfoCount)
                        {
                            btnErrBack.Enabled = false;
                            btnErrLast.Enabled = false;
                        }
                        for (int i = 0; i < 10; i++)
                        {
                            ErrImportCheck(errLogList[i + gErrLocalEntryIndex], gErrLocalEntryIndex + i);
                        }
                        textBoxLogInfo.Text = "Read local error log entry data:" + gErrLocalEntryIndex.ToString() + " - " + (gErrLocalEntryIndex + 9).ToString();

                    }
                    else if ((gErrLocalEntryIndex <= errInfoCount) && ((gErrLocalEntryIndex + 10) >= errInfoCount))
                    {
                        btnErrFirst.Enabled = true;
                        btnErrForward.Enabled = true;
                        btnErrBack.Enabled = false;
                        btnErrLast.Enabled = false;
                        for (int i = gErrLocalEntryIndex; i < errInfoCount; i++)
                        {
                            ErrImportCheck(errLogList[i], i);
                        }
                        textBoxLogInfo.Text = "Read local error log entry data:" + gErrLocalEntryIndex.ToString() + " - " + (errInfoCount - 1).ToString();
                    }
                    this.listViewErr.EndUpdate();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Get error log failed:" + ex.Message);
            }
        }

        /// <summary>
        /// Clean system error log 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnErrClean_Click(object sender, EventArgs e)
        {
            int index = 0;
            string errClearCmd;
            string errResponse;
            string errCmd;

            try
            {
                if (!serialPort1.IsOpen)
                {
                    MessageBox.Show("Com port is not open!", "Warning!");
                    return;
                }

                if (textBoxErrCount.Text.Equals(""))
                {
                    MessageBox.Show("The number of messages is empty!", "Warning!");
                    return;
                }

                if (textBoxErrCount.Text.Equals("0"))
                {
                    MessageBox.Show("The error log is empty.", "Warning!");
                    return;
                }

                DialogResult MsgBoxResult;
                MsgBoxResult = MessageBox.Show("You are about to delete the Error Log stored in the system. proceed? ", "Warning!", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                if (MsgBoxResult == DialogResult.Yes)
                {
                    //Enter password
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
                        else
                        {
                            btnErrBack.Enabled = false;
                            btnErrForward.Enabled = false;
                            btnErrFirst.Enabled = false;
                            btnErrLast.Enabled = false;

                            gErrEntryIndex = 0;
                            this.listViewErr.Items.Clear();
                            //Send command of clean system error log 
                            errClearCmd = STX + CMD_ERR_CLEAR + ETX;
                            serialPort1.Write(errClearCmd);
                            errResponse = serialPort1.ReadTo(ETX);
                            index = errResponse.IndexOf("$");
                            errResponse = errResponse.Substring(index);
                            //Clean success
                            if (errResponse.Substring(1, 2).Equals("OK"))
                            {
                                errResponse = errResponse.TrimStart((STX).ToCharArray());
                                if (errResponse.Equals(RSP_OK))
                                {
                                    textBoxLogInfo.Text = "Error log cleared:" + Utils.GetCommandResult(errResponse.ToString());
                                    errCmd = STX + CMD_ERR_LOG + CMD_CNT_RD + ETX;
                                    serialPort1.Write(errCmd);
                                    errResponse = serialPort1.ReadTo(ETX);
                                    index = errResponse.IndexOf("$");
                                    errResponse = errResponse.Substring(index);

                                    if (errResponse.Substring(1, 2).Equals("OK"))
                                    {
                                        errResponse = errResponse.TrimStart((STX + OK_HDR).ToCharArray());
                                        errResponse = errResponse.TrimEnd('$');
                                        int intErrValue = Convert.ToInt32(errResponse, 16);
                                        var stringErrValue = intErrValue.ToString();
                                        textBoxErrCount.Text = stringErrValue;
                                    }
                                }
                                //Clean failed
                                else if (errResponse.Equals(NG_BUSY) || errResponse.Equals(NG_HWERR))
                                {
                                    textBoxLogInfo.Text = "Error response:" + Utils.GetCommandResult(errResponse.ToString());
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Clear error log failed:" + ex.Message);
            }
        }

#if true //add by Qin yunhe 20200321
        private string GetSystem_SN()
        {
            string cmd;
            string response;
            string CMD_SSN_GET = "$SSN:GET$";
            try
            {
                if (!serialPort1.IsOpen)
                {
                    MessageBox.Show("Com port is not open!", "Warning!");
                    return "COM NOT OPEN";
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
                    //TextboxSysIDGet.Text = response;
                    return response ;
                }
                else if (response.Substring(1, 2).Equals("NG"))
                {
                    //TextboxSysIDGet.Text = response;
                    return response;
                }

                //SystemInfo.Text = "Get Carrier Arm SN: " + Utils.GetCommandResult(response);
            }
            catch 
            {
                MessageBox.Show("Get carrier arm SN error:");
            }
            return "error";
         }
#endif
        /// <summary>
        /// First Load 10 Operation logs
        /// </summary>
        public void LoadOperLog()
        {
            string operCmd;
            string operResponse;
            int index;
            int intOperValue = 0;

            gOperImport = false;
            btnOperImport.Enabled = false;
            btnOperExport.Enabled = true;
            gOperLocalEntryIndex = 0;
            this.listViewOper.Items.Clear();
            try
            {
                if (!serialPort1.IsOpen)
                {
                    MessageBox.Show("Com port is not open!", "Warning!");
                    return;
                }
                //20180115
                string systemType = GetSystemMode();
                if (systemType == "")
                {
                    MessageBox.Show("Get system type failed");
                    return;
                }

                // add by Qin yunhe for record the FW version in logfile 20200321
                strCCBFW ="CCB_FW: " + mainPage.Get_SystemFWversion("ICAN:01");// Get_SystemFWversion("ICAN:01");
                //strOPMIFW = "OPMI_FW: " + mainPage.Get_SystemFWversion("ICAN:02");
                //strCCUFW = "CCU_FW: " + mainPage.Get_SystemFWversion("ICAN:06");
                strsystem_SN = "SN: " + GetSystem_SN();
                //DateTime sysDate = dateSystemTimePicker.Value;
                //DateTime sysTime = DateTimePickerSys.Value;

                //strTime = sysTime.ToString("HH:mm:ss");
                //strDate = sysDate.ToString("yyyy/MM/dd", System.Globalization.CultureInfo.InvariantCulture);
                //end 20200321
                textBoxOperCount.ReadOnly = true;
                btnOperFirst.Enabled = false;
                btnOperForward.Enabled = false;
                btnOperClean.Enabled = true;
                gOperEntryIndex = 0;
                //Send command for get system operation log
                operCmd = STX + CMD_OPER_LOG + CMD_CNT_RD + ETX;
                serialPort1.Write(operCmd);
                operResponse = serialPort1.ReadTo(ETX);
                index = operResponse.IndexOf("$");
                operResponse = operResponse.Substring(index);
                //groupBoxOper.Text =  "Operation Log (System) systemType";
                //add by Qin yunhe 2020-03-21
                if (systemType == peakMode)
                {
                    groupBoxOper.Text = "Operation Log (Dental)";
                }
                else if (systemType == startMode)
                {
                    groupBoxOper.Text = "Operation Log (ENT)";
                }
                //add end 2020-03-21
                //get system operation success
                if (operResponse.Substring(1, 2).Equals("OK"))
                {
                    operResponse = operResponse.TrimStart((STX + OK_HDR).ToCharArray());
                    operResponse = operResponse.TrimEnd('$');
                    intOperValue = Convert.ToInt32(operResponse, 16);

                    var stringOperValue = intOperValue.ToString();
                    textBoxOperCount.Text = stringOperValue;

                    gOperInfoTotaiCount = intOperValue;
                    if (intOperValue == 0)
                    {
                        btnOperLast.Enabled = false;
                        btnOperBack.Enabled = false;
                    }
                    else if ((intOperValue <= 10) && (intOperValue > 0)) //0-9
                    {
                        OperListView(0, intOperValue, systemType);
                        btnOperLast.Enabled = false;
                        btnOperBack.Enabled = false;
                    }
                    else if (intOperValue > 10)
                    {
                        OperListView(0, 10, systemType);
                        btnOperLast.Enabled = true;
                        btnOperBack.Enabled = true;
                    }
                }
            }
            catch
            {
                return;
            }
        }

        /// <summary>
        /// First Load 10 Error logs
        /// </summary>
        public void LoadErrorLog()
        {
            string errCmd;
            string errResponse;
            int index;
            int intErrValue = 0; //the count of the errors in error log
            gErrImport = false;
            btnErrImport.Enabled = false;
            btnErrExport.Enabled = true;
            gErrLocalEntryIndex = 0;
            this.listViewErr.Items.Clear();
            try
            {
                if (!serialPort1.IsOpen)
                {
                    MessageBox.Show("Com port is not open!", "Warning!");
                    return;
                }
                string systemType = GetSystemMode();//20200321
                textBoxErrCount.ReadOnly = true;
                btnErrFirst.Enabled = false;
                btnErrForward.Enabled = false;
                btnErrClean.Enabled = true;
                gErrEntryIndex = 0;

                //Send command for get system error log
                errCmd = STX + CMD_ERR_LOG + CMD_CNT_RD + ETX;
                serialPort1.Write(errCmd);
                errResponse = serialPort1.ReadTo(ETX);
                index = errResponse.IndexOf("$");
                errResponse = errResponse.Substring(index);
                //Get system error log success
                if (errResponse.Substring(1, 2).Equals("OK"))
                {
                    errResponse = errResponse.TrimStart((STX + OK_HDR).ToCharArray());
                    errResponse = errResponse.TrimEnd('$');
                    intErrValue = Convert.ToInt32(errResponse, 16);
                    var stringErrValue = intErrValue.ToString();
                    textBoxErrCount.Text = stringErrValue;
                    //groupBoxErr.Text = "Error Log (System)";

                    //add by Qin yunhe 2020-03-21
                    if (systemType == peakMode)
                    {
                        groupBoxErr.Text = "Error Log (Dental)";
                    }
                    else if (systemType == startMode)
                    {
                        groupBoxErr.Text = "Error Log(ENT)";
                    }
                    //add end 2020-03-21
                    gErrInfoTotalCount = intErrValue;
                    if (intErrValue == 0)
                    {
                        btnErrLast.Enabled = false;
                        btnErrBack.Enabled = false;
                    }
                    else if ((intErrValue <= 10) && (intErrValue > 0)) //0-9
                    {
                        ErrListView(0, intErrValue);
                        btnErrLast.Enabled = false;
                        btnErrBack.Enabled = false;
                    }
                    else if (intErrValue > 10)
                    {
                        ErrListView(0, 10);
                        btnErrLast.Enabled = true;
                        btnErrBack.Enabled = true;
                    }
                }
            }
            catch
            {
                return;
            }
        }

        public void RefreshErrorCount()
        {
            string errCmd = STX + CMD_ERR_LOG + CMD_CNT_RD + ETX;
            serialPort1.Write(errCmd);
            string errResponse = serialPort1.ReadTo(ETX);
            int index = errResponse.IndexOf("$");
            errResponse = errResponse.Substring(index);

            if (errResponse.Substring(1, 2).Equals("OK"))
            {
                errResponse = errResponse.TrimStart((STX + OK_HDR).ToCharArray());
                errResponse = errResponse.TrimEnd('$');
                int intErrValue = Convert.ToInt32(errResponse, 16);
                textBoxErrCount.Text = intErrValue.ToString();
            }
        }


        /// <summary>
        /// Get system operation log
        /// </summary>
        /// <param name="operIndexStart"></param>
        /// <param name="operIndexEnd"></param>
        private void OperListView(int operIndexStart, int operIndexEnd, string systemType)
        {
            string operCmd;
            string operResponse;
            int index = 0;

            this.listViewOper.Items.Clear();
            this.listViewOper.BeginUpdate();
            try
            {
                int operInfoCount = Convert.ToInt32(textBoxOperCount.Text);
                //Get system operation log
                for (int i = operIndexStart; i < operIndexEnd; i++)
                {
                    string stringI = Convert.ToString(i, 16);
                    stringI = stringI.PadLeft(4, '0');

                    operCmd = STX + CMD_OPER_LOG + CMD_ENT_RD + stringI + "$" + ETX;
                    serialPort1.Write(operCmd);
                    operResponse = serialPort1.ReadTo(ETX);
                    index = operResponse.IndexOf("$");
                    operResponse = operResponse.Substring(index);
                    if (operResponse.Substring(1, 2).Equals("OK"))
                    {
                        //Analysis System operation log
                        operResponse = operResponse.TrimStart(OK_HDR.ToCharArray());
                        operResponse = operResponse.TrimEnd('$');
                        OperLogAnalysis(operResponse, i, systemType);
                    }
                    else
                    {
                        //Get system operation log failed
                        if (operResponse.Equals(NG_BADPAR) || operResponse.Equals(NG_BUSY) || operResponse.Equals(NG_HWERR))
                        {
                            OperErrorResponse(i, operResponse);
                        }
                    }
                }
                if (operInfoCount == 1)
                {
                    textBoxLogInfo.Text = "Read system operation log entry data:" + Utils.GetCommandResult(operIndexStart.ToString());
                }
                else if (operIndexStart == (operInfoCount - 1))
                {
                    textBoxLogInfo.Text = "Read system operation log entry data:" + Utils.GetCommandResult((operIndexEnd - 1).ToString());
                }
                else
                {
                    textBoxLogInfo.Text = "Read system operation log entry data:" + Utils.GetCommandResult(operIndexStart.ToString()) + " - " + Utils.GetCommandResult((operIndexEnd - 1).ToString());
                }
            }
            catch
            {
                textBoxLogInfo.Text = "Read system operation log entry data:NG:TO";
            }
            finally
            {
                this.listViewOper.EndUpdate();
            }
        }

        /// <summary>
        /// Get system operation log failed
        /// </summary>
        /// <param name="index"></param>
        /// <param name="operResponse"></param>
        private void OperErrorResponse(int index, string operResponse)
        {
            ListViewItem listViewOperation = new ListViewItem();
            listViewOperation.Text = index.ToString("000");
            listViewOperation.SubItems.Add("--");
            listViewOperation.SubItems.Add("--");
            listViewOperation.SubItems.Add(Utils.GetCommandResult(operResponse));
            listViewOperation.ForeColor = Color.Red;
            this.listViewOper.Items.Add(listViewOperation);
        }

        /// <summary>
        /// Get system error log
        /// </summary>
        /// <param name="errIndexStart"></param>
        /// <param name="errIndexEnd"></param>
        public void ErrListView(int errIndexStart, int errIndexEnd)
        {
            string errCmd;
            string errResponse = "";
            int index = 0;

            this.listViewErr.Items.Clear();
            this.listViewErr.BeginUpdate();
            try
            {
                int errInfoCount = Convert.ToInt32(textBoxErrCount.Text);
                //Get system error log
                for (int i = errIndexStart; i < errIndexEnd; i++)
                {
                    string stringI = Convert.ToString(i, 16); // the item number in the first colum
                    stringI = stringI.PadLeft(4, '0');

                    errCmd = STX + CMD_ERR_LOG + CMD_ENT_RD + stringI + "$" + ETX;
                    serialPort1.Write(errCmd);
                    errResponse = serialPort1.ReadTo(ETX);

                    index = errResponse.IndexOf("$");
                    errResponse = errResponse.Substring(index);
                    if (errResponse.Substring(1, 2).Equals("OK"))
                    {
                        //Analysis system error log 
                        errResponse = errResponse.TrimStart(OK_HDR.ToCharArray());
                        errResponse = errResponse.TrimEnd('$');
                        ErrLogAnalysis(errResponse, i);
                    }
                    else
                    {
                        //Get system error log failed
                        if (errResponse.Equals(NG_BADPAR) || errResponse.Equals(NG_BUSY) || errResponse.Equals(NG_HWERR))
                        {
                            ErrErrorResponse(i, errResponse);
                        }
                    }
                }
                if (errInfoCount == 1)
                {
                    textBoxLogInfo.Text = "Read system error log entry data:" + Utils.GetCommandResult(errIndexStart.ToString());
                }
                else if (errIndexStart == (errInfoCount - 1))
                {
                    textBoxLogInfo.Text = "Read system error log entry data:" + Utils.GetCommandResult((errIndexEnd - 1).ToString());
                }
                else
                {
                    textBoxLogInfo.Text = "Read system error log entry data:" + Utils.GetCommandResult(errIndexStart.ToString()) + " - " + Utils.GetCommandResult((errIndexEnd - 1).ToString());
                }
            }
            catch
            {
                textBoxLogInfo.Text = "Read system error log entry data:NG:TO";
            }
            finally
            {
                this.listViewErr.EndUpdate();
            }
        }

        /// <summary>
        /// Get system error log failed
        /// </summary>
        /// <param name="index"></param>
        /// <param name="errResponse"></param>
        private void ErrErrorResponse(int index, string errResponse)
        {
            ListViewItem listViewError = new ListViewItem();
            listViewError.Text = index.ToString("000");
            listViewError.SubItems.Add("--");
            listViewError.SubItems.Add("--");
            listViewError.SubItems.Add(Utils.GetCommandResult(errResponse));
            listViewError.ForeColor = Color.Red;
            this.listViewErr.Items.Add(listViewError);
        }

        /// <summary>
        /// Analysis System operation log
        /// </summary>
        /// <param name="operResponse"></param>
        /// <param name="i"></param>
        private void OperLogAnalysis(string operResponse, int i, string systemMode)
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
            string mode;

            try
            {
                operationByte = operResponse;
                ListViewItem listViewOperation = new ListViewItem();
                listViewOperation.Text = i.ToString("000");

                //Get system time
                year = operResponse.Substring(38, 2);
                month = operResponse.Substring(36, 2);
                day = operResponse.Substring(34, 2);
                hour = operResponse.Substring(32, 2);
                minute = operResponse.Substring(30, 2);
                second = operResponse.Substring(28, 2);
                millisecond = operResponse.Substring(26, 2) + operResponse.Substring(24, 2);

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

                //Get mode 
                mode = operResponse.Substring(14, 2);
                int intMode = Convert.ToInt32(mode, 16);
                string strMode = Convert.ToString(intMode, 2);
                strMode = strMode.PadLeft(8, '0');
                string modeTemp = strMode.Substring(1, 7);

                listViewOperation.SubItems.Add(date);
                listViewOperation.SubItems.Add(time);

                if (strMode.Substring(0, 1).Equals("1"))
                {
                    listViewOperation.SubItems.Add("In Docking Position");
                }
                else
                {
                    //20180115
                    listViewOperation.SubItems.Add(OperModeAnalysis(modeTemp, systemMode));
                }
                this.listViewOper.Items.Add(listViewOperation);
                
            }
            catch
            {
                //MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Get Mode
        /// </summary>
        /// <param name="operMode"></param>
        /// <param name="systemType"></param>
        /// <returns></returns>
        public static string OperModeAnalysis(string operMode, string systemType)
        {
            string strMode = "-";

            if (operMode.Equals("0000000"))//Normal Light mode 
            {
                strMode = "Normal Light Mode";
            }
            else if (operMode.Equals("0000001"))//Fluorescence mode
            {
                //20180206
                //strMode = "Fluorescence Mode";
                if (systemType == peakMode)
                {
                    //Peak "EXTARO 300 Dental"
                    strMode = "Fluorescence Mode";
                }
                else if (systemType == startMode)
                {
                    //Start "EXTARO 300 ENT"
                    strMode = "Violet Mode";
                }
                else if (systemType == notAvailableMode)
                {
                    //System type not available
                    strMode = "Undetermined Mode";
                }
            }
            else if (operMode.Equals("0000010"))//No Glare mode
            {
                strMode = "NoGlare Mode";
            }
            else if (operMode.Equals("0000100"))//Green Light mode
            {
                strMode = "Green Light Mode";
            }
            else if (operMode.Equals("0001000"))//Orange Light mode
            {
                strMode = "Orange Light Mode";
            }
            else if (operMode.Equals("0010000"))//True Light mode 
            {
                if (systemType == peakMode)
                {
                    //Peak "EXTARO 300 Dental"
                    strMode = "True Light Mode";
                }
                else if (systemType == startMode)
                {
                    //Start "EXTARO 300 ENT"
                    strMode = "Multispectral Mode";
                }
                else if (systemType == notAvailableMode)
                {
                    //System type not available
                    strMode = "Undetermined Mode";
                }
            }
            else if (operMode.Equals("0100000"))//Manual filter operation
            {
                strMode = "Manual Filter Operation Mode";
            }
            else if (operMode.Equals("1000000"))//Mode error
            {
                strMode = "Mode Error";
            }
            else if (operMode.Equals("0001010"))//NoGlare + Orange Light mode
            {
                strMode = "NoGlare + Orange Light Mode";
            }
            else if (operMode.Equals("0010010"))//NoGlare + TrueLight mode
            {
                if (systemType == peakMode)
                {
                    //Peak "EXTARO 300 Dental"
                    strMode = "NoGlare + True Light Mode";
                }
                else if (systemType == startMode)
                {
                    //Start "EXTARO 300 ENT"
                    strMode = "NoGlare + Multispectral Mode";
                }
                else if (systemType == notAvailableMode)
                {
                    strMode = "NoGlare + Undetermined Mode";
                }
                //strMode = "NoGlare + True Light Mode";
            }
            return strMode;
        }


        /// <summary>
        /// Analysis system error log 
        /// </summary>
        /// <param name="errResponse"></param>
        /// <param name="i"></param>
        private void ErrLogAnalysis(string errResponse, int i)
        {
            try
            {
                ListViewItem listViewError = new ListViewItem();
                listViewError.Text = i.ToString("000");
                // get system time
                string year = errResponse.Substring(22, 2);
                string month = errResponse.Substring(20, 2);
                string day = errResponse.Substring(18, 2);
                string hour = errResponse.Substring(16, 2);
                string minute = errResponse.Substring(14, 2);
                string second = errResponse.Substring(12, 2);
                string millisecond = errResponse.Substring(10, 2) + errResponse.Substring(8, 2);

#if true
                year = (Convert.ToUInt32(year, 16) + 2000).ToString();
                month = Convert.ToUInt32(month, 16).ToString().PadLeft(2,'0');
                day = Convert.ToUInt32(day, 16).ToString().PadLeft(2, '0');
                hour = Convert.ToUInt32(hour, 16).ToString().PadLeft(2, '0');
                minute = Convert.ToUInt32(minute, 16).ToString().PadLeft(2, '0');
                second = Convert.ToUInt32(second, 16).ToString().PadLeft(2, '0');
                millisecond = Convert.ToUInt32(minute, 16).ToString();

                string date = (year + month + day).Insert(4, "-").Insert(7, "-");
                string time = (hour + minute + second).Insert(2, ":").Insert(5, ":");
                //date = date.Insert(4, "-").Insert(7, "-");
                //time = time.Insert(2, ":").Insert(5, ":");
#else
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
#endif
                listViewError.SubItems.Add(date);
                listViewError.SubItems.Add(time);

                string returnString = ErrorCodeAnalysis(errResponse);
                listViewError.SubItems.Add(returnString);
                this.listViewErr.Items.Add(listViewError);

            }
            catch
            {
                //MessageBox.Show(ex.Message);
            }

        }

        /// <summary>
        /// Get error type
        /// </summary>
        /// <param name="errResponse"></param>
        /// <returns></returns>
        private string ErrorCodeAnalysis(string errResponse)
        {
            string returnString = string.Empty;
            int errCount = 0;
#if false // debug by Yunhe Qin 
            string errorByte0 = errResponse.Substring(0, 2);
            string errorByte1 = errResponse.Substring(2, 2);
            string errorByte2 = errResponse.Substring(4, 2);
            string errorByte3 = errResponse.Substring(6, 2);
            string stringErrInfo = errorByte3 + errorByte2 + errorByte1 + errorByte0;
#else
            string errorByte0 = errResponse.Substring(0, 2);
            string errorByte1 = errResponse.Substring(2, 2);
            string errorByte2 = errResponse.Substring(4, 2);
            string errorByte3 = errResponse.Substring(6, 2);
            string stringErrInfo = errorByte3 + errorByte2 + errorByte0 + errorByte1;
#endif
            errorInfo = errResponse;
            int intErrlog = Convert.ToInt32((stringErrInfo), 16);
            string binaryErrLog = Convert.ToString(intErrlog, 2);

            binaryErrLog = binaryErrLog.PadLeft(32, '0');
#if false
            string knobErr = binaryErrLog.Substring(binaryErrLog.Length - 2,2);//OPMI Multi-functional knob error 
            string indicatorPCBAErr = binaryErrLog.Substring(29, 1);//Communication to indicator PCBA error
            string drivingMotorErr = binaryErrLog.Substring(27, 2);//OPMI Filter wheel driving motor error
            string sensorsErr = binaryErrLog.Substring(26, 1);//Filter position sensors error
            string LedPCBAErr = binaryErrLog.Substring(25, 1);//Communication to Panorama LED PCBA error
            string panoramaLedErr = binaryErrLog.Substring(23, 2);//Panorama LED error
            string yokePCBAErr = binaryErrLog.Substring(22, 1);//Communication to yoke PCBA error
            string tiltBalanceMotorErr = binaryErrLog.Substring(20, 2);//OPMI Star tilt axis balancing motor error
            string swivelBalanceMotorErr = binaryErrLog.Substring(18, 2);//OPMI Star swivel axis balancing motor error
            string reserved1 = binaryErrLog.Substring(17, 1);//Reserved
            string reserved2 = binaryErrLog.Substring(16, 1);//Reserved
#else
            string knobErr = binaryErrLog.Substring(binaryErrLog.Length - 2,2);//OPMI Multi-functional knob error 
            string indicatorPCBAErr = binaryErrLog.Substring(29, 1);//Communication to indicator PCBA error
            string drivingMotorErr = binaryErrLog.Substring(27, 2);//OPMI Filter wheel driving motor error
            string sensorsErr = binaryErrLog.Substring(26, 1);//Filter position sensors error
            string reserved1 = binaryErrLog.Substring(25, 1);//Reserved
            string SpotIllumMotorErr = binaryErrLog.Substring(24, 1);//Spot illumation motor error
            string swivelBalanceMotorErr = binaryErrLog.Substring(22, 2);//OPMI Star swivel axis balancing motor error
            string tiltBalanceMotorErr = binaryErrLog.Substring(20, 2);//Tilt axis balance motor error
            string yokePCBAErr = binaryErrLog.Substring(19, 1);//Communication to yoke PCBA error
            string panoPCBAErr = binaryErrLog.Substring(18, 1);//Communication to pano PCBA error
            string panoramaLedErr = binaryErrLog.Substring(16, 2);//OPMI Panorama LED error
#endif
            string heliosFatalErr = binaryErrLog.Substring(15, 1);//Helios fatal error
            string heliosRGBErr = binaryErrLog.Substring(14, 1);//Helios RGB error
            string heliosVlightErr = binaryErrLog.Substring(13, 1);//Helios V-Light unit error
            string heliosDriveVoltageErr = binaryErrLog.Substring(12, 1);//Helios driving voltage error
            string heliosIlluminationSetErr = binaryErrLog.Substring(11, 1);//Helios illumination setting error
            string heliosOverTemperatureErr = binaryErrLog.Substring(10, 1);//Helios over temperature warning(internal temperature > 66˚C)
            string heliosShutdownErr = binaryErrLog.Substring(9, 1);//Helios emergency shut-down (internal temperature > 75˚C)
            string heliosTemperatureControlErr = binaryErrLog.Substring(8, 1);//Helios temperature control error
            string CCUInternalErr = binaryErrLog.Substring(7, 1);//CCU internal error
            string CCU_EVR_CommunicateErr = binaryErrLog.Substring(6, 1);//CCU EVR communication error
            string CAN_OPMI_CommunicateErr = binaryErrLog.Substring(5, 1);//CAN communication to OPMI error
            string CAN_Helios_CommunicateErr = binaryErrLog.Substring(4, 1);//CAN communication to Helios error
            string CAN_CCU_CommunicateErr = binaryErrLog.Substring(3, 1);//CAN communication to CCU error
            string systemConfigueErr = binaryErrLog.Substring(2, 1);//System configuration error
            string reserved3 = binaryErrLog.Substring(0, 2);//Reserved


            //1 OPMI Multi-functional knob error
            if (knobErr.Equals("00"))
            {
                returnString = "No Error";
            }
            else if (knobErr.Equals("01"))
            {
                errCount++;
                //Err_OPMI_MultiKnob_Stuck
                returnString = "E020201";
            }
            else if (knobErr.Equals("10"))
            {
                errCount++;
                //Err_OPMI_MultiKnob_NoConn
                returnString = "E020202";
            }

            //2 Communication to indicator PCBA error
            if (indicatorPCBAErr.Equals("0"))
            {
                if (errCount == 0)
                {
                    returnString = "No Error";
                }
            }
            else
            {
                //Err_OPMI_IndiPCBA
                errCount++;
                if (errCount >= 2)
                {
                    returnString = "Multiple Errors";
                    return returnString;
                }
                returnString = "E020101";
            }

            //3 OPMI Filter wheel driving motor error
            if (drivingMotorErr.Equals("00"))
            {
                if (errCount == 0)
                {
                    returnString = "No Error";
                }
            }
            else if (drivingMotorErr.Equals("01"))
            {
                //Err_OPMI_FiltMotor_Open
                errCount++;
                if (errCount >= 2)
                {
                    returnString = "Multiple Errors";
                    return returnString;
                }
                returnString = "E020301";
            }
            else if (drivingMotorErr.Equals("10"))
            {
                //Err_OPMI_FiltMotor_Short
                errCount++;
                if (errCount >= 2)
                {
                    returnString = "Multiple Errors";
                    return returnString;
                }
                returnString = "E020302";
            }
            else if (drivingMotorErr.Equals("11"))
            {
                //returnString = "OPMI Filter wheel driving motor error";
                errCount++;
                if (errCount >= 2)
                {
                    returnString = "Multiple Errors";
                    return returnString;
                }
                returnString = "E020303";
            }


            //4 Filter position sensors error
            if (sensorsErr.Equals("0"))
            {
                if (errCount == 0)
                {
                    returnString = "No Error";
                }
            }
            else
            {
                //Err_OPMI_FiltSensor
                errCount++;
                if (errCount >= 2)
                {
                    returnString = "Multiple Errors";
                    return returnString;
                }
                returnString = "E020304";
            }
            //add by Yunhe Qin
            //5 spot illumation motor error 
            if (SpotIllumMotorErr.Equals("0"))
            {
                if (errCount == 0)
                {
                    returnString = "No Error";
                }
            }
            else
            {
                errCount++;
                if (errCount >= 2)
                {
                    returnString = "Multiple Errors";
                    return returnString;
                }
                returnString = "E020501";
            }
            //6 OPMI Star swivel axis balancing motor error
            if (swivelBalanceMotorErr.Equals("00"))
            {
                if (errCount == 0)
                {
                    returnString = "No Error";
                }
            }
            else if (swivelBalanceMotorErr.Equals("01"))
            {
                //Err_OPMI_FiltMotor_Open
                errCount++;
                if (errCount >= 2)
                {
                    returnString = "Multiple Errors";
                    return returnString;
                }
                //returnString = "E020301";
                returnString = "Unknown Error";
            }
            else if (swivelBalanceMotorErr.Equals("10"))
            {
                //Err_OPMI_FiltMotor_Short
                errCount++;
                if (errCount >= 2)
                {
                    returnString = "Multiple Errors";
                    return returnString;
                }
                //returnString = "E020302";
                returnString = "Unknown Error";
            }
            else if (swivelBalanceMotorErr.Equals("11"))
            {
                //returnString = "OPMI Star swivel axis balancing motor error";
                errCount++;
                if (errCount >= 2)
                {
                    returnString = "Multiple Errors";
                    return returnString;
                }
                //returnString = "Unknown Error";
                returnString = "Unknown Error";
            }
            //7 OPMI Star tilt axis balancing motor error
            if (tiltBalanceMotorErr.Equals("00"))
            {
                if (errCount == 0)
                {
                    returnString = "No Error";
                }
            }
            else if (tiltBalanceMotorErr.Equals("01"))
            {
                //Err_OPMI_FiltMotor_Open
                errCount++;
                if (errCount >= 2)
                {
                    returnString = "Multiple Errors";
                    return returnString;
                }
                //returnString = "E020301";
                returnString = "Unknown Error";
            }
            else if (tiltBalanceMotorErr.Equals("10"))
            {
                //Err_OPMI_FiltMotor_Short
                errCount++;
                if (errCount >= 2)
                {
                    returnString = "Multiple Errors";
                    return returnString;
                }
                //returnString = "E020302";
                returnString = "Unknown Error";
            }
            else if (tiltBalanceMotorErr.Equals("11"))
            {
                //returnString = "OPMI Star tilt axis balancing motor error";
                errCount++;
                if (errCount >= 2)
                {
                    returnString = "Multiple Errors";
                    return returnString;
                }
                //returnString = "Unknown Error";
                returnString = "Unknown Error";
            }
            //8 Communication to yoke PCBA error 
            if (yokePCBAErr.Equals("0"))
            {
                if (errCount == 0)
                {
                    returnString = "No Error";
                }
            }
            else
            {
                //Err_OPMI_YokePCBA
                errCount++;
                if (errCount >= 2)
                {
                    returnString = "Multiple Errors";
                    return returnString;
                }
                returnString = "E020102";
            }
            //9 Communication to Panorama LED PCBA error
            if (panoPCBAErr.Equals("0"))
            {
                if (errCount == 0)
                {
                    returnString = "No Error";
                }
            }
            else
            {
                //returnString = "Communication to Panorama LED PCBA error";
                errCount++;
                if (errCount >= 2)
                {
                    returnString = "Multiple Errors";
                    return returnString;
                }
                returnString = "Unknown Error";
            }
            //10 Panorama LED error
            if (panoramaLedErr.Equals("00"))
            {
                if (errCount == 0)
                {
                    returnString = "No Error";
                }
            }
            else
            {
                //returnString = "Panorama LED error";
                errCount++;
                if (errCount >= 2)
                {
                    returnString = "Multiple Errors";
                    return returnString;
                }
                returnString = "Unknown Error";
            }


            //11 Helios fatal error
            if (heliosFatalErr.Equals("0"))
            {
                if (errCount == 0)
                {
                    returnString = "No Error";
                }
            }
            else
            {
                //Err_LightSrc_Fatal
                errCount++;
                if (errCount >= 2)
                {
                    returnString = "Multiple Errors";
                    return returnString;
                }
                returnString = "E030101";
            }


            //12 Helios RGB error
            if (heliosRGBErr.Equals("0"))
            {
                if (errCount == 0)
                {
                    returnString = "No Error";
                }
            }
            else
            {
                errCount++;
                if (errCount >= 2)
                {
                    returnString = "Multiple Errors";
                    //returnString = "E030201";
                    return returnString;
                }
                returnString = "E030201";
            }


            //13 Helios V-Light unit error
            if (heliosVlightErr.Equals("0"))
            {
                if (errCount == 0)
                {
                    returnString = "No Error";
                }
            }
            else
            {
                //Err_LightSrc_VLight
                errCount++;
                if (errCount >= 2)
                {
                    returnString = "Multiple Errors";
                    return returnString;
                }
                returnString = "E030301";
            }


            //14 Helios driving voltage error
            if (heliosDriveVoltageErr.Equals("0"))
            {
                if (errCount == 0)
                {
                    returnString = "No Error";
                }
            }
            else
            {
                //Err_LightSrc_Voltage
                errCount++;
                if (errCount >= 2)
                {
                    returnString = "Multiple Errors";
                    return returnString;
                }
                returnString = "E030102";
            }


            //15 Helios illumination setting error
            if (heliosIlluminationSetErr.Equals("0"))
            {
                if (errCount == 0)
                {
                    returnString = "No Error";
                }
            }
            else
            {
                //Err_LightSrc_IllumCtrl
                errCount++;
                if (errCount >= 2)
                {
                    returnString = "Multiple Errors";
                    return returnString;
                }
                returnString = "E030202";
            }


            //16 Helios over temperature warning
            if (heliosOverTemperatureErr.Equals("0"))
            {
                if (errCount == 0)
                {
                    returnString = "No Error";
                }
            }
            else
            {
                //Err_LightSrc_OverTempWarn
                errCount++;
                if (errCount >= 2)
                {
                    returnString = "Multiple Errors";
                    return returnString;
                }
                returnString = "E030401";
            }

            //17 Helios emergency shut-down
            if (heliosShutdownErr.Equals("0"))
            {
                if (errCount == 0)
                {
                    returnString = "No Error";
                }
            }
            else
            {
                //Err_LightSrc_OverTempShut
                errCount++;
                if (errCount >= 2)
                {
                    returnString = "Multiple Errors";
                    return returnString;
                }
                returnString = "E030402";
            }


            //18 Helios temperature control error
            if (heliosTemperatureControlErr.Equals("0"))
            {
                if (errCount == 0)
                {
                    returnString = "No Error";
                }
            }
            else
            {
                //Err_LightSrc_TempCtrl
                errCount++;
                if (errCount >= 2)
                {
                    returnString = "Multiple Errors";
                    return returnString;
                }
                returnString = "E030403";
            }


            //19 CCU internal error
            if (CCUInternalErr.Equals("0"))
            {
                if (errCount == 0)
                {
                    returnString = "No Error";
                }
            }
            else
            {
                //Err_CCU_Internal
                errCount++;
                if (errCount >= 2)
                {
                    returnString = "Multiple Errors";
                    return returnString;
                }
                returnString = "E060101";
            }

            //20 CCU EVR communication error
            if (CCU_EVR_CommunicateErr.Equals("0"))
            {
                if (errCount == 0)
                {
                    returnString = "No Error";
                }
            }
            else
            {
                //Err_CCU_EVR
                errCount++;
                if (errCount >= 2)
                {
                    returnString = "Multiple Errors";
                    return returnString;
                }
                returnString = "E060102";
            }

            //21 CAN communication to OPMI error
            if (CAN_OPMI_CommunicateErr.Equals("0"))
            {
                if (errCount == 0)
                {
                    returnString = "No Error";
                }
            }
            else
            {
                //Err_CANComm_OPMI
                errCount++;
                if (errCount >= 2)
                {
                    returnString = "Multiple Errors";
                    return returnString;
                }
                returnString = "E010201";
            }

            //22 CAN communication to Helios error
            if (CAN_Helios_CommunicateErr.Equals("0"))
            {
                if (errCount == 0)
                {
                    returnString = "No Error";
                }
            }
            else
            {
                //Err_CANComm_LightSrc
                errCount++;
                if (errCount >= 2)
                {
                    returnString = "Multiple Errors";
                    return returnString;
                }
                returnString = "E010202";
            }

            //23 CAN communication to CCU error
            if (CAN_CCU_CommunicateErr.Equals("0"))
            {
                if (errCount == 0)
                {
                    returnString = "No Error";
                }
            }
            else
            {
                //Err_CANComm_CCU
                errCount++;
                if (errCount >= 2)
                {
                    returnString = "Multiple Errors";
                    return returnString;
                }
                returnString = "E010203";
            }


            //24 System configuration error
            if (systemConfigueErr.Equals("0"))
            {
                if (errCount == 0)
                {
                    returnString = "No Error";
                }
            }
            else
            {
                //Err_Sys_Configuration
                errCount++;
                if (errCount >= 2)
                {
                    returnString = "Multiple Errors";
                    return returnString;
                }
                returnString = "E010101";
            }
            return returnString;
        }

        /// <summary>
        /// Details of error log 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listViewErr_DoubleClick(object sender, EventArgs e)
        {
            int index;
            try
            {
                int errInfoCount = Convert.ToInt32(textBoxErrCount.Text);
                //Port connect
                if (!gErrImport)
                {
                    if (!serialPort1.IsOpen)
                    {
                        MessageBox.Show("Com port is not open!", "Warning!");
                        //this.listViewOper.Items.Clear();
                        //this.listViewErr.Items.Clear();
                        //textBoxOperCount.Text = string.Empty;
                        //textBoxErrCount.Text = string.Empty;
                        textBoxLogInfo.Text = "Com port is not open";
                        return;
                    }
                    int selectCount = listViewErr.SelectedItems.Count;
                    if (selectCount > 0)
                    {
                        if (this.listViewErr.SelectedItems != null)
                        {
                            gErrSelectIndex = this.listViewErr.SelectedItems[0].SubItems[0].Text;
                            index = gErrSelectIndex.IndexOf("0");
                            if (index == 0)
                            {
                                gErrSelectIndex = gErrSelectIndex.Substring(index + 1);
                            }
                            index = gErrSelectIndex.IndexOf("0");
                            if (index == 0)
                            {
                                gErrSelectIndex = gErrSelectIndex.Substring(index + 1);
                            }

                            ErrLogDefineForm errLogDefineForm = new ErrLogDefineForm(mainPage, serialPort1);
                            errLogDefineForm.StartPosition = FormStartPosition.CenterParent;
                            errLogDefineForm.ErrInfoSelected(gErrSelectIndex);

                            if (errLogDefineForm.ShowDialog() == DialogResult.Cancel)
                            {
                                if (!serialPort1.IsOpen)
                                {
                                    textBoxOperCount.Text = "";
                                    textBoxErrCount.Text = "";
                                    this.listViewOper.Items.Clear();
                                    MessageBox.Show("Com port is not open!", "Warning!");
                                    return;
                                }

                                int errCurrentIndex = Convert.ToInt32(LogFiles.gErrSelectIndex, 10);
                                errCurrentIndex = errCurrentIndex / 10 * 10;
                                gErrEntryIndex = errCurrentIndex;

                                if ((errCurrentIndex + 10) <= errInfoCount)
                                {
                                    ErrListView(errCurrentIndex, errCurrentIndex + 10);
                                    //int inttemp = Convert.ToInt32(LogFiles.gErrSelectIndex, 10);
                                    //inttemp = inttemp / 10 * 10;
                                    //gErrEntryIndex = inttemp;

                                    if (gErrEntryIndex == 0)
                                    {
                                        btnErrFirst.Enabled = false;
                                        btnErrForward.Enabled = false;
                                    }

                                    if (gErrEntryIndex >= 10)
                                    {
                                        btnErrFirst.Enabled = true;
                                        btnErrForward.Enabled = true;
                                    }


                                    if (gErrEntryIndex < (errInfoCount - 1) / 10 * 10)
                                    {
                                        btnErrBack.Enabled = true;
                                        btnErrLast.Enabled = true;
                                    }

                                    if (gErrEntryIndex == (errInfoCount - 1) / 10 * 10)
                                    {
                                        btnErrBack.Enabled = false;
                                        btnErrLast.Enabled = false;
                                    }
                                }
                                else
                                {
                                    if (errInfoCount > 10)
                                    {
                                        btnErrFirst.Enabled = true;
                                        btnErrForward.Enabled = true;
                                    }
                                    btnErrBack.Enabled = false;
                                    btnErrLast.Enabled = false;

                                    ErrListView(errCurrentIndex, errInfoCount);
                                    //gErrEntryIndex = errCurrentIndex;
                                }
                            }
                        }
                    }
                }


                //Local
                if (gErrImport)
                {
                    int selectCount = listViewErr.SelectedItems.Count;
                    if (selectCount > 0)
                    {
                        if (this.listViewErr.SelectedItems != null)
                        {
                            gErrSelectIndex = this.listViewErr.SelectedItems[0].SubItems[0].Text;
                            index = gErrSelectIndex.IndexOf("0");
                            if (index == 0)
                            {
                                gErrSelectIndex = gErrSelectIndex.Substring(index + 1);
                            }
                            index = gErrSelectIndex.IndexOf("0");
                            if (index == 0)
                            {
                                gErrSelectIndex = gErrSelectIndex.Substring(index + 1);
                            }

                            int intErrIndex = Convert.ToInt32(gErrSelectIndex);

                            ErrLogDefineForm errLogDefineForm = new ErrLogDefineForm(mainPage, serialPort1);
                            errLogDefineForm.StartPosition = FormStartPosition.CenterParent;

                            string stringCheck = errLogList[intErrIndex].Substring(errLogList[intErrIndex].Length - 2);
                            string errResponse = errLogList[intErrIndex].Substring(0, errLogList[intErrIndex].Length - 2);
                            string checkSum = CheckSum(errResponse);
                            if (stringCheck == checkSum)
                            {
                                errLogDefineForm.ErrInfoMean(errLogList[intErrIndex], gErrSelectIndex);
                            }
                            else
                            {
                                return;
                            }
                            // ErrLogDefineForm.textBoxErrInfo.Text = "Read Error Log entry data:OK. Index = " + gErrSelectIndex;

                            if (errLogDefineForm.ShowDialog() == DialogResult.Cancel)
                            {
                                int errCurrentIndex = Convert.ToInt32(LogFiles.gErrSelectIndex, 10);
                                errCurrentIndex = errCurrentIndex / 10 * 10;
                                gErrLocalEntryIndex = errCurrentIndex;

                                this.listViewErr.Items.Clear();
                                this.listViewErr.BeginUpdate();

                                if ((errCurrentIndex + 10) <= errInfoCount)
                                {
                                    for (int i = errCurrentIndex; i < (errCurrentIndex + 10); i++)
                                    {
                                        //ErrLogAnalysis(errLogList[i], i);
                                        ErrImportCheck(errLogList[i], i);
                                    }

                                    //int inttemp = Convert.ToInt32(LogFiles.gErrSelectIndex, 10);
                                    //inttemp = inttemp / 10 * 10;
                                    //gErrLocalEntryIndex = inttemp;
                                    textBoxLogInfo.Text = "Read local error log entry data:" + Utils.GetCommandResult(gErrLocalEntryIndex.ToString()) + " - " + Utils.GetCommandResult((gErrLocalEntryIndex + 9).ToString());

                                    if (gErrLocalEntryIndex == 0)
                                    {
                                        btnErrFirst.Enabled = false;
                                        btnErrForward.Enabled = false;
                                    }

                                    if (gErrLocalEntryIndex >= 10)
                                    {
                                        btnErrFirst.Enabled = true;
                                        btnErrForward.Enabled = true;
                                    }

                                    if (gErrLocalEntryIndex < (errInfoCount - 1) / 10 * 10)
                                    {
                                        btnErrBack.Enabled = true;
                                        btnErrLast.Enabled = true;
                                    }

                                    if (gErrLocalEntryIndex == (errInfoCount - 1) / 10 * 10)
                                    {
                                        btnErrBack.Enabled = false;
                                        btnErrLast.Enabled = false;
                                    }
                                }
                                else
                                {
                                    //gErrLocalEntryIndex = errCurrentIndex;
                                    if (errInfoCount > 10)
                                    {
                                        btnErrFirst.Enabled = true;
                                        btnErrForward.Enabled = true;
                                    }
                                    btnErrBack.Enabled = false;
                                    btnErrLast.Enabled = false;

                                    for (int i = errCurrentIndex; i < errInfoCount; i++)
                                    {
                                        ErrImportCheck(errLogList[i], i);
                                    }
                                    textBoxLogInfo.Text = "Read local operation log entry data:" + Utils.GetCommandResult(errCurrentIndex.ToString()) + " - " + Utils.GetCommandResult((errInfoCount - 1).ToString());
                                }
                                this.listViewErr.EndUpdate();
                            }
                        }
                    }
                }

            }
            catch
            {
                //MessageBox.Show(ex.Message);
            }
        }


        /// <summary>
        /// Details of operation log
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listViewOper_DoubleClick(object sender, EventArgs e)
        {
            int index;
            try
            {
                int operInfoCount = Convert.ToInt32(textBoxOperCount.Text);
                //Port connect
                if (!gOperImport)
                {
                    if (!serialPort1.IsOpen)
                    {
                        MessageBox.Show("Com port is not open!", "Warning!");
                        textBoxLogInfo.Text = "Com port is not open";
                        return;
                    }
                    //20180115
                    string systemType = GetSystemMode();
                    if (systemType == "")
                    {
                        MessageBox.Show("Get system type failed");
                        return;
                    }

                    int selectCount = listViewOper.SelectedItems.Count;
                    if (selectCount > 0)
                    {
                        if (this.listViewOper.SelectedItems != null)
                        {
                            gOperSelectIndex = this.listViewOper.SelectedItems[0].SubItems[0].Text;
                            index = gOperSelectIndex.IndexOf("0");
                            if (index == 0)
                            {
                                gOperSelectIndex = gOperSelectIndex.Substring(index + 1);
                            }
                            index = gOperSelectIndex.IndexOf("0");
                            if (index == 0)
                            {
                                gOperSelectIndex = gOperSelectIndex.Substring(index + 1);
                            }

                            OperLogDefineForm operLogDefineForm = new OperLogDefineForm(mainPage, serialPort1);
                            operLogDefineForm.StartPosition = FormStartPosition.CenterParent;
                            operLogDefineForm.OperInfoSelected(gOperSelectIndex);

                            if (operLogDefineForm.ShowDialog() == DialogResult.Cancel)
                            {
                                if (!serialPort1.IsOpen)
                                {
                                    textBoxOperCount.Text = "";
                                    textBoxErrCount.Text = "";
                                    this.listViewOper.Items.Clear();
                                    MessageBox.Show("Com port is not open!", "Warning!");
                                    return;
                                }

                                int operCurrentIndex = Convert.ToInt32(LogFiles.gOperSelectIndex, 10);
                                operCurrentIndex = operCurrentIndex / 10 * 10;
                                gOperEntryIndex = operCurrentIndex;

                                if ((operCurrentIndex + 10) <= operInfoCount)
                                {
                                    OperListView(operCurrentIndex, operCurrentIndex + 10, systemType);
                                    //int inttemp = Convert.ToInt32(LogFiles.gOperSelectIndex, 10);
                                    //inttemp = inttemp / 10 * 10;
                                    //gOperEntryIndex = inttemp;
                                    if (gOperEntryIndex == 0)
                                    {
                                        btnOperFirst.Enabled = false;
                                        btnOperForward.Enabled = false;
                                    }

                                    if (gOperEntryIndex >= 10)
                                    {
                                        btnOperFirst.Enabled = true;
                                        btnOperForward.Enabled = true;
                                    }

                                    if (gOperEntryIndex < (operInfoCount - 1) / 10 * 10)
                                    {
                                        btnOperBack.Enabled = true;
                                        btnOperLast.Enabled = true;
                                    }

                                    if (gOperEntryIndex == (operInfoCount - 1) / 10 * 10)
                                    {
                                        btnOperBack.Enabled = false;
                                        btnOperLast.Enabled = false;
                                    }
                                }
                                else
                                {
                                    if (operInfoCount > 10)
                                    {
                                        btnOperFirst.Enabled = true;
                                        btnOperForward.Enabled = true;
                                    }
                                    btnOperBack.Enabled = false;
                                    btnOperLast.Enabled = false;
                                    OperListView(operCurrentIndex, operInfoCount, systemType);
                                    //gOperEntryIndex = operCurrentIndex;
                                }
                            }
                        }
                    }
                }

                //Local
                if (gOperImport)
                {
                    int selectCount = listViewOper.SelectedItems.Count;
                    if (selectCount > 0)
                    {
                        if (this.listViewOper.SelectedItems != null)
                        {
                            gOperSelectIndex = this.listViewOper.SelectedItems[0].SubItems[0].Text;
                            index = gOperSelectIndex.IndexOf("0");
                            if (index == 0)
                            {
                                gOperSelectIndex = gOperSelectIndex.Substring(index + 1);
                            }
                            index = gOperSelectIndex.IndexOf("0");
                            if (index == 0)
                            {
                                gOperSelectIndex = gOperSelectIndex.Substring(index + 1);
                            }

                            int intOperIndex = Convert.ToInt32(gOperSelectIndex);
                            OperLogDefineForm operLogDefineForm = new OperLogDefineForm(mainPage, serialPort1);
                            operLogDefineForm.StartPosition = FormStartPosition.CenterParent;


                            string stringCheck = operLogList[intOperIndex].Substring(operLogList[intOperIndex].Length - 2);
                            string operResponse = operLogList[intOperIndex].Substring(0, operLogList[intOperIndex].Length - 2);
                            string checkSum = CheckSum(operResponse);
                            if (stringCheck == checkSum)
                            {
                                operLogDefineForm.OperInfoMean(operLogList[intOperIndex], gOperSelectIndex, localSystemType);
                            }
                            else
                            {
                                return;
                            }

                            if (operLogDefineForm.ShowDialog() == DialogResult.Cancel)
                            {
                                int operCurrentIndex = Convert.ToInt32(LogFiles.gOperSelectIndex, 10);
                                operCurrentIndex = operCurrentIndex / 10 * 10;
                                gOperLocalEntryIndex = operCurrentIndex;

                                this.listViewOper.Items.Clear();
                                this.listViewOper.BeginUpdate();
                                if ((operCurrentIndex + 10) <= operInfoCount)
                                {
                                    for (int i = operCurrentIndex; i < (operCurrentIndex + 10); i++)
                                    {
                                        OperImportCheck(operLogList[i], i, localSystemType);//OperLogAnalysis(operLogList[i], i);
                                    }
                                    //int inttemp = Convert.ToInt32(LogFiles.gOperSelectIndex, 10);
                                    //inttemp = inttemp / 10 * 10;
                                    //gOperLocalEntryIndex = inttemp;
                                    textBoxLogInfo.Text = "Read local operation log entry data:" + Utils.GetCommandResult(gOperLocalEntryIndex.ToString()) + " - " + Utils.GetCommandResult((gOperLocalEntryIndex + 9).ToString());
                                    if (gOperLocalEntryIndex == 0)
                                    {
                                        btnOperFirst.Enabled = false;
                                        btnOperForward.Enabled = false;
                                    }

                                    if (gOperLocalEntryIndex >= 10)
                                    {
                                        btnOperFirst.Enabled = true;
                                        btnOperForward.Enabled = true;
                                    }

                                    if (gOperLocalEntryIndex < (operInfoCount - 1) / 10 * 10)
                                    {
                                        btnOperBack.Enabled = true;
                                        btnOperLast.Enabled = true;
                                    }

                                    if (gOperLocalEntryIndex == (operInfoCount - 1) / 10 * 10)
                                    {
                                        btnOperBack.Enabled = false;
                                        btnOperLast.Enabled = false;
                                    }
                                }
                                else
                                {
                                    if (operInfoCount > 10)
                                    {
                                        btnOperFirst.Enabled = true;
                                        btnOperForward.Enabled = true;
                                    }

                                    btnOperBack.Enabled = false;
                                    btnOperLast.Enabled = false;
                                    for (int i = operCurrentIndex; i < operInfoCount; i++)
                                    {
                                        OperImportCheck(operLogList[i], i, localSystemType); //OperLogAnalysis(operLogList[i], i);
                                    }
                                    textBoxLogInfo.Text = "Read local operation log entry data:" + Utils.GetCommandResult(operCurrentIndex.ToString()) + " - " + Utils.GetCommandResult((operInfoCount - 1).ToString());
                                }
                                this.listViewOper.EndUpdate();
                            }
                        }
                    }
                }

            }
            catch
            {
                //MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Back to the home page of operation log
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOperFirst_Click(object sender, EventArgs e)
        {
            try
            {
                //Port connect
                if (!gOperImport)
                {
                    if (!serialPort1.IsOpen)
                    {
                        MessageBox.Show("Com port is not open!", "Warning!");
                        return;
                    }

                    if (textBoxOperCount.Text.Equals(""))
                    {
                        MessageBox.Show("The number of messages is empty!", "Warning!");
                        return;
                    }

                    //20180115
                    string systemType = GetSystemMode();
                    if (systemType == "")
                    {
                        MessageBox.Show("Get system type failed");
                        return;
                    }

                    gOperEntryIndex = 0;
                    btnOperFirst.Enabled = false;
                    btnOperForward.Enabled = false;

                    int operInfoCount = Convert.ToInt32(textBoxOperCount.Text);
                    if (operInfoCount > 10)
                    {
                        OperListView(0, 10, systemType);
                        btnOperBack.Enabled = true;
                        btnOperLast.Enabled = true;
                    }
                    else if ((operInfoCount > 0) && (operInfoCount <= 10))
                    {
                        OperListView(0, operInfoCount, systemType);
                        btnOperBack.Enabled = false;
                        btnOperLast.Enabled = false;
                    }
                    else if (operInfoCount == 0)
                    {
                        btnOperBack.Enabled = false;
                        btnOperLast.Enabled = false;
                    }
                }

                //Local
                if (gOperImport)
                {
                    if (textBoxOperCount.Text.Equals(""))
                    {
                        MessageBox.Show("The number of messages is empty!", "Warning!");
                        return;
                    }
                    gOperLocalEntryIndex = 0;
                    btnOperFirst.Enabled = false;
                    btnOperForward.Enabled = false;
                    this.listViewOper.Items.Clear();
                    this.listViewOper.BeginUpdate();
                    int operInfoCount = Convert.ToInt32(textBoxOperCount.Text);
                    if (operInfoCount > 10)
                    {
                        for (int i = 0; i < 10; i++)
                        {
                            OperImportCheck(operLogList[i], i, localSystemType);//OperLogAnalysis(operLogList[i], i);
                        }
                        btnOperBack.Enabled = true;
                        btnOperLast.Enabled = true;
                        textBoxLogInfo.Text = "Read local operation log entry data:" + gOperLocalEntryIndex.ToString() + " - " + (gOperLocalEntryIndex + 9).ToString();
                    }
                    else if ((operInfoCount > 0) && (operInfoCount <= 10))
                    {
                        for (int i = 0; i < operInfoCount; i++)
                        {
                            OperImportCheck(operLogList[i], i, localSystemType); //OperLogAnalysis(operLogList[i], i);
                        }
                        btnOperBack.Enabled = false;
                        btnOperLast.Enabled = false;
                        textBoxLogInfo.Text = "Read Local Operation Log Entry Data:" + gOperLocalEntryIndex.ToString() + " - " + (operInfoCount - 1).ToString();
                    }
                    else if (operInfoCount == 0)
                    {
                        btnOperBack.Enabled = false;
                        btnOperLast.Enabled = false;
                    }
                    this.listViewOper.EndUpdate();
                }

            }
            catch(Exception ex)
            {
                MessageBox.Show("Get operation log failed:" + ex.Message);
            }
        }


        /// <summary>
        /// Back to the home page of error log
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnErrFirst_Click(object sender, EventArgs e)
        {
            try
            {

                //Port connect
                if (!gErrImport)
                {
                    if (!serialPort1.IsOpen)
                    {
                        MessageBox.Show("Com port is not open!", "Warning!");
                        return;
                    }

                    if (textBoxErrCount.Text.Equals(""))
                    {
                        MessageBox.Show("The number of messages is empty!", "Warning!");
                        return;
                    }
                    int errInfoCount = Convert.ToInt32(textBoxErrCount.Text);
                    gErrEntryIndex = 0;
                    btnErrFirst.Enabled = false;
                    btnErrForward.Enabled = false;
                    if (errInfoCount > 10)
                    {
                        ErrListView(0, 10);
                        btnErrBack.Enabled = true;
                        btnErrLast.Enabled = true;
                    }
                    else if ((errInfoCount > 0) && (errInfoCount <= 10))
                    {
                        ErrListView(0, errInfoCount);
                        btnErrBack.Enabled = false;
                        btnErrLast.Enabled = false;
                    }
                    else if (errInfoCount == 0)
                    {
                        btnErrBack.Enabled = false;
                        btnErrLast.Enabled = false;
                    }
                }


                //Local log
                if (gErrImport)
                {
                    if (textBoxErrCount.Text.Equals(""))
                    {
                        MessageBox.Show("The number of messages is empty!", "Warning!");
                        return;
                    }
                    gErrLocalEntryIndex = 0;
                    btnErrFirst.Enabled = false;
                    btnErrForward.Enabled = false;
                    this.listViewErr.Items.Clear();
                    this.listViewErr.BeginUpdate();

                    int errInfoCount = Convert.ToInt32(textBoxErrCount.Text);
                    if (errInfoCount > 10)
                    {
                        for (int i = 0; i < 10; i++)
                        {
                            //ErrLogAnalysis(errLogList[i], i);
                            ErrImportCheck(errLogList[i], i);
                        }
                        btnErrBack.Enabled = true;
                        btnErrLast.Enabled = true;
                        textBoxLogInfo.Text = "Read local error log entry data:" + gErrLocalEntryIndex.ToString() + " - " + (gErrLocalEntryIndex + 9).ToString();
                    }
                    else if ((errInfoCount > 0) && (errInfoCount <= 10))
                    {
                        for (int i = 0; i < errInfoCount; i++)
                        {
                            //ErrLogAnalysis(errLogList[i], i);
                            ErrImportCheck(errLogList[i], i);
                        }
                        btnErrBack.Enabled = false;
                        btnErrLast.Enabled = false;
                        textBoxLogInfo.Text = "Read local error log entry data:" + gErrLocalEntryIndex.ToString() + " - " + (errInfoCount - 1).ToString();
                    }
                    else if (errInfoCount == 0)
                    {
                        btnErrBack.Enabled = false;
                        btnErrLast.Enabled = false;
                    }
                    this.listViewErr.EndUpdate();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Get error log failed:" + ex.Message);
            }
        }

        /// <summary>
        /// Back to the last page of error log
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnErrLast_Click(object sender, EventArgs e)
        {
            try
            {
                //Port connect
                if (!gErrImport)
                {
                    if (!serialPort1.IsOpen)
                    {
                        MessageBox.Show("Com port is not open!", "Warning!");
                        return;
                    }

                    if (textBoxErrCount.Text.Equals(""))
                    {
                        MessageBox.Show("The number of messages is empty!", "Warning!");
                        return;
                    }

                    btnErrBack.Enabled = false;
                    btnErrLast.Enabled = false;
                    int intErrInfoCount = Convert.ToInt32(textBoxErrCount.Text);
                    if (intErrInfoCount > 10)
                    {
                        btnErrForward.Enabled = true;
                        btnErrFirst.Enabled = true;
                        ErrListView((intErrInfoCount - 1) / 10 * 10, intErrInfoCount);
                        gErrEntryIndex = (intErrInfoCount - 1) / 10 * 10;
                    }
                    else if ((intErrInfoCount > 0) && (intErrInfoCount <= 10))
                    {
                        btnErrForward.Enabled = false;
                        btnErrFirst.Enabled = false;
                        ErrListView(0, intErrInfoCount);
                        gErrEntryIndex = 0;
                    }
                    else if (intErrInfoCount == 0)
                    {
                        btnErrForward.Enabled = false;
                        btnErrFirst.Enabled = false;
                        gErrEntryIndex = 0;
                    }
                }
                //Local
                if (gErrImport)
                {
                    if (textBoxErrCount.Text.Equals(""))
                    {
                        MessageBox.Show("The number of messages is empty!", "Warning!");
                        return;
                    }
                    btnErrBack.Enabled = false;
                    btnErrLast.Enabled = false;
                    this.listViewErr.Items.Clear();
                    this.listViewErr.BeginUpdate();
                    int intErrInfoCount = Convert.ToInt32(textBoxErrCount.Text);
                    if (intErrInfoCount > 10)
                    {
                        btnErrForward.Enabled = true;
                        btnErrFirst.Enabled = true;
                        for (int i = (intErrInfoCount - 1) / 10 * 10; i < intErrInfoCount; i++)
                        {
                            //ErrLogAnalysis(errLogList[i], i);
                            ErrImportCheck(errLogList[i], i);
                        }
                        gErrLocalEntryIndex = (intErrInfoCount - 1) / 10 * 10;
                        textBoxLogInfo.Text = "Read local error log entry data:" + gErrLocalEntryIndex.ToString() + " - " + (intErrInfoCount - 1).ToString();
                    }
                    else if ((intErrInfoCount > 0) && (intErrInfoCount <= 10))
                    {
                        btnErrForward.Enabled = false;
                        btnErrFirst.Enabled = false;

                        for (int i = 0; i < intErrInfoCount; i++)
                        {
                            //ErrLogAnalysis(errLogList[i], i);
                            ErrImportCheck(errLogList[i], i);
                        }
                        gErrLocalEntryIndex = 0;
                        textBoxLogInfo.Text = "Read local error log entry data:" + gErrLocalEntryIndex.ToString() + " - " + (intErrInfoCount - 1).ToString();
                    }
                    else if (intErrInfoCount == 0)
                    {
                        btnErrForward.Enabled = false;
                        btnErrFirst.Enabled = false;
                        gErrLocalEntryIndex = 0;
                    }
                    this.listViewErr.EndUpdate();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Get error log failed:" + ex.Message);
            }
        }

        /// <summary>
        /// Back to the last page of operation log
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOperLast_Click(object sender, EventArgs e)
        {
            try
            {
                //Port connect
                if (!gOperImport)
                {
                    if (!serialPort1.IsOpen)
                    {
                        MessageBox.Show("Com port is not open!", "Warning!");
                        return;
                    }

                    if (textBoxOperCount.Text.Equals(""))
                    {
                        MessageBox.Show("The number of messages is empty!", "Warning!");
                        return;
                    }

                    //20180115
                    string systemType = GetSystemMode();
                    if (systemType == "")
                    {
                        MessageBox.Show("Get system type failed");
                        return;
                    }

                    btnOperBack.Enabled = false;
                    btnOperLast.Enabled = false;
                    int intOperInfoCount = Convert.ToInt32(textBoxOperCount.Text);
                    if (intOperInfoCount > 10)
                    {
                        btnOperForward.Enabled = true;
                        btnOperFirst.Enabled = true;
                        OperListView((intOperInfoCount - 1) / 10 * 10, intOperInfoCount, systemType);
                        gOperEntryIndex = (intOperInfoCount - 1) / 10 * 10;
                    }
                    else if ((intOperInfoCount > 0) && (intOperInfoCount <= 10))
                    {
                        btnOperForward.Enabled = false;
                        btnOperFirst.Enabled = false;
                        OperListView(0, intOperInfoCount, systemType);
                        gOperEntryIndex = 0;
                    }
                    else if (intOperInfoCount == 0)
                    {
                        btnOperForward.Enabled = false;
                        btnOperFirst.Enabled = false;
                        gOperEntryIndex = 0;
                    }
                }
                //Local
                if (gOperImport)
                {
                    if (textBoxOperCount.Text.Equals(""))
                    {
                        MessageBox.Show("The number of messages is empty!", "Warning!");
                        return;
                    }
                    btnOperBack.Enabled = false;
                    btnOperLast.Enabled = false;
                    this.listViewOper.Items.Clear();
                    this.listViewOper.BeginUpdate();
                    int intOperInfoCount = Convert.ToInt32(textBoxOperCount.Text);
                    if (intOperInfoCount > 10)
                    {
                        btnOperForward.Enabled = true;
                        btnOperFirst.Enabled = true;
                        for (int i = (intOperInfoCount - 1) / 10 * 10; i < intOperInfoCount; i++)
                        {
                            OperImportCheck(operLogList[i], i, localSystemType);//OperLogAnalysis(operLogList[i], i);
                        }
                        gOperLocalEntryIndex = (intOperInfoCount - 1) / 10 * 10;
                        textBoxLogInfo.Text = "Read local operation log entry data:" + gOperLocalEntryIndex.ToString() + " - " + (intOperInfoCount - 1).ToString();
                    }
                    else if ((intOperInfoCount > 0) && (intOperInfoCount <= 10))
                    {
                        btnOperForward.Enabled = false;
                        btnOperFirst.Enabled = false;

                        for (int i = 0; i < intOperInfoCount; i++)
                        {
                            OperImportCheck(operLogList[i], i, localSystemType);// OperLogAnalysis(operLogList[i], i);
                        }
                        gOperLocalEntryIndex = 0;
                        textBoxLogInfo.Text = "Read local operation log entry data:" + gOperLocalEntryIndex.ToString() + " - " + (intOperInfoCount - 1).ToString();
                    }
                    else if (intOperInfoCount == 0)
                    {
                        btnOperForward.Enabled = false;
                        btnOperFirst.Enabled = false;
                        gOperLocalEntryIndex = 0;
                    }
                    this.listViewOper.EndUpdate();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Get operation log failed:" + ex.Message);
            }
        }

        /// <summary>
        /// Initialize button after close port
        /// </summary>
        public void ClosePortLogFormInit()
        {
            textBoxOperCount.Text = "";
            textBoxErrCount.Text = "";
            textBoxLogInfo.Text = "";
            this.listViewOper.Items.Clear();
            this.listViewErr.Items.Clear();
            btnOperFirst.Enabled = false;
            btnOperForward.Enabled = false;
            btnOperBack.Enabled = false;
            btnOperLast.Enabled = false;
            btnOperClean.Enabled = false;

            btnErrFirst.Enabled = false;
            btnErrForward.Enabled = false;
            btnErrBack.Enabled = false;
            btnErrLast.Enabled = false;
            btnErrClean.Enabled = false;

            btnErrImport.Enabled = true;
            btnErrExport.Enabled = false;
            btnOperImport.Enabled = true;
            btnOperExport.Enabled = false;

            groupBoxOper.Text = "Operation Log";
            groupBoxErr.Text = "Error Log";
        }

        /// <summary>
        /// Export operation log
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOperExport_Click(object sender, EventArgs e)
        {
            try
            {
                int operInfoCount = Convert.ToInt32(textBoxOperCount.Text);
                if (listViewOper.Items.Count == 0)
                {
                    MessageBox.Show("Operation log empty!");
                }
                else
                {
                    SaveAllOperLog(operInfoCount);
                }
            }
            catch
            {
                textBoxLogInfo.Text = "Export operation log failed";
                MessageBox.Show("Export operation log failed!");
            }
        }



        /// <summary>
        /// Export error log
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnErrExport_Click(object sender, EventArgs e)
        {
            try
            {
                int errInfoCount = Convert.ToInt32(textBoxErrCount.Text);
                if (listViewErr.Items.Count == 0)
                {
                    MessageBox.Show("Error log empty!");
                }
                else
                { 
                    SaveAllErrorLog(errInfoCount);
                }
            }
            catch
            {
                textBoxLogInfo.Text = "Export error log failed";
                MessageBox.Show("Export error log failed!");
            }
        }
        //the following function was added by Qin yunhe for recored the lic in log files.
        private string Convert_Str(string constr)
        {
            string done_str = "";
            string[] bufferArray = Regex.Split(constr, "\n");
            string[] licenseInfo ;

            done_str = done_str + bufferArray.Length.ToString() + "\n" ; /*sizeof(bufferArray) / sizeof(bufferArray[0]);*/
            foreach (string bufferLicense in bufferArray)
            {
                if (bufferLicense.Contains("MED_EXTARO_Fluorescence_mode")//MED_EXTARO_Fluorescence_mode 0
                    || bufferLicense.Contains("MED_EXTARO_Demo_FL_mode"))
                {
                    licenseInfo = Regex.Split(bufferLicense, " ");
                    done_str = done_str + licenseInfo[1] + " " + licenseInfo[3] + "\n";
                }
                if (bufferLicense.Contains("MED_EXTARO_NoGlare_mode")
                    || bufferLicense.Contains("MED_EXTARO_Demo_NoGlare_mode"))//MED_EXTARO_NoGlare_mode 1
                {
                    licenseInfo = Regex.Split(bufferLicense, " ");
                    done_str = done_str + licenseInfo[1] + " " + licenseInfo[3] + "\n";
                }
                if (bufferLicense.Contains("MED_EXTARO_LightBoost")
                    || bufferLicense.Contains("MED_EXTARO_Demo_LightBoost"))//MED_EXTARO_LightBoost 2
                {
                    licenseInfo = Regex.Split(bufferLicense, " ");
                    done_str = done_str + licenseInfo[1] + " " + licenseInfo[3] + "\n";
                }
                if (bufferLicense.Contains("MED_EXTARO_TrueLight_mode")
                    || bufferLicense.Contains("MED_EXTARO_Demo_TrueLight_mode"))//MED_EXTARO_TrueLight_mode 3
                {
                    licenseInfo = Regex.Split(bufferLicense, " ");
                    done_str = done_str + licenseInfo[1] + " " + licenseInfo[3] + "\n";
                }
                if (bufferLicense.Contains("MED_EXTARO_Multispectral_mode")
                    || bufferLicense.Contains("MED_EXTARO_Demo_Multispec_mode"))//MED_EXTARO_Multispectral_mode 4
                {
                    licenseInfo = Regex.Split(bufferLicense, " ");
                    done_str = done_str + licenseInfo[1] + " " + licenseInfo[3] + "\n";
                }
              
                if (bufferLicense.Contains("DICOM"))//MED_EXTARO_DICOM 6
                {
                    licenseInfo = Regex.Split(bufferLicense, " ");
                    done_str = done_str + licenseInfo[1] + " " + licenseInfo[3] + "\n";  
                }
            }
                return done_str;
        }
        // add end
#if true
        public Thread m_thReadshowprogress = null;
        public bool threadShowEndFlag = true;
        public void ShowPrograss()
        {
            threadShowEndFlag = false;
            mainPage.IsCommandSending = true;
            try
            {
               // if (string.IsNullOrEmpty(license_log.commandProgress))
                   // this.textBoxLogInfo.Invoke(new Action(() => this.textBoxLogInfo.Text = license_log.commandProgress));
               // else
                    this.textBoxLogInfo.Invoke(new Action(() => this.textBoxLogInfo.Text = license_log.commandProgress));
            }
            catch
            {
                ;
            }
            mainPage.IsCommandSending = false;
            threadShowEndFlag = true;
        }
#endif

        /// <summary>
        /// Save system error log
        /// </summary>
        /// <param name="errInfoCount"></param>
        public void SaveAllErrorLog(int errInfoCount)
        {
            string errCmd;
            string errResponse = "";
            int index = 0;
            string path = "";
            string activate_lic = "";
            //int errLogCount = (int)errInfoCount;
            try
            {
                SaveFileDialog fileDialog = new SaveFileDialog();
                fileDialog.Title = "Please select the file path";
                fileDialog.Filter = "exlog Files|*.exlog";
                if (fileDialog.ShowDialog() == DialogResult.OK)
                {
                    path = fileDialog.FileName;
                }
                else
                {
                    return;
                }
#if true   //   get license features add by Yunhe Qin
                // License.ReadFromBoard();
                if (License.Activate_Lic.Equals(""))
                {
                    textBoxLogInfo.Text = "Reading License BLK ... ";
                   // m_thReadshowprogress = new Thread(ShowPrograss);
                    //m_thReadshowprogress.Start();
                    license_log.ReadFromBoard();
                    textBoxLogInfo.Text = "Reading License BLK has done wait record ... ";
                }
               
#endif
                activate_lic = Convert_Str(License.Activate_Lic); // add by Qinyunhe 20200526
                List<string> list = new List<string>();
                
                //20180201
                string systemType = GetSystemMode();

                //add here for avoid click button long time by Qin yunhe 20200321
                strOPMIFW = "OPMI_FW: " + mainPage.Get_SystemFWversion("ICAN:02");
                strCCUFW = "CCU_FW: " + mainPage.Get_SystemFWversion("ICAN:06");

                if (systemType == "")
                {
                    MessageBox.Show("Get system type failed");
                    return;
                }
                if (systemType == peakMode)
                {
                    //Peak "EXTARO 300 Dental"
                    list.Add(localLogPeak);        //"EXTARO 300"
                }
                else if (systemType == startMode)
                {
                    //Star "EXTARO 300 ENT";
                    list.Add(localLogStart);        //"EXTARO 300 ENT"
                }
                else if (systemType == notAvailableMode)
                {
                    list.Add(localNotAvailable);    //"Not Available"
                }
                
                //list.Add(localLogPeak);//Error log Count
                list.Add(localErrLog);//Error log 
                //add the CCB, OPMI, CCU FW Version in here Qin Yunhe 2020-03-20
                list.Add(strsystem_SN);
                list.Add(strCCBFW);
                list.Add(strOPMIFW);
                list.Add(strCCUFW);
                list.Add(activate_lic);//add for record the lic20200525
                //add end
                list.Add(textBoxErrCount.Text);//Error log Count
                for (int i = 0; i < errInfoCount; i++)
                {
                    string stringI = Convert.ToString(i, 16);
                    stringI = stringI.PadLeft(4, '0');

                    errCmd = STX + CMD_ERR_LOG + CMD_ENT_RD + stringI + "$" + ETX;
                    serialPort1.Write(errCmd);
                    errResponse = serialPort1.ReadTo(ETX);
                    index = errResponse.IndexOf("$");
                    errResponse = errResponse.Substring(index);

                    if (errResponse.Substring(1, 2).Equals("OK"))
                    {
                        errResponse = errResponse.TrimStart(OK_HDR.ToCharArray());
                        errResponse = errResponse.TrimEnd('$');
                        string checkSum = CheckSum(errResponse);
                        //int stringLength = errResponse.Length / 2;
                        //string stringLengthHex = stringLength.ToString("X2");
                        list.Add(errResponse + checkSum);
                    }

                    textBoxLogInfo.Text = "Save error log.Index = " + i.ToString();
                }
#if false   //   get license features
                // License.ReadFromBoard();
                license_log.ReadFromBoard();
#endif
                StringBuilder stringLog = new StringBuilder();
                foreach (string lists in list)
                {
                    stringLog.AppendLine(lists);
                }
                System.IO.File.WriteAllText(path, stringLog.ToString(), ASCIIEncoding.ASCII);

                textBoxLogInfo.Text = "Save error log end";
                MessageBox.Show("The error log was saved successfully!");
            }
            catch
            {
                MessageBox.Show("some error");
            }
        }

        /// <summary>
        /// Check Log
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        public string CheckSum(string response)
        {
            string sum = "";
            int intSum = 0;
            int length = response.Length;
            try
            {
                for (int i = 0; i < length / 2; i++)
                {
                    string strTemp = response.Substring(2 * i, 2);
                    int intTmep = Convert.ToInt32(strTemp, 16);
                    intSum = intSum + intTmep;

                }
                sum = intSum.ToString("X8");
                sum = sum.Substring(sum.Length - 2);
                return sum;
            }
            catch
            {
                return "Error";
            }

        }

        /// <summary>
        /// Save operation log
        /// </summary>
        /// <param name="operInfoCount"></param>
        private void SaveAllOperLog(int operInfoCount)
        {
            string operCmd;
            string operResponse = "";
            int index = 0;
            string path = "";
            //string lic_raw = "";
            MainMenu mainMenu = new MainMenu();//2020-0321
            try
            {
                SaveFileDialog fileDialog = new SaveFileDialog();
                fileDialog.Title = "Please select the file path";
                fileDialog.Filter = "exlog Files|*.exlog";
                if (fileDialog.ShowDialog() == DialogResult.OK)
                {
                    path = fileDialog.FileName;
                }
                else
                {
                    return;
                }
#if true   //   get license features add by Yunhe Qin
                // License.ReadFromBoard();
                if (License.Activate_Lic.Equals(""))
                {
                    textBoxLogInfo.Text = "Reading License BLK ... ";
                    // m_thReadshowprogress = new Thread(ShowPrograss);
                    //m_thReadshowprogress.Start();
                    license_log.ReadFromBoard();
                    textBoxLogInfo.Text = "Reading License BLK has done wait record ... ";
                }

#endif
                List<string> list = new List<string>();

                //20180115
                string systemType = GetSystemMode();// get system type is dental or ent
                //add here for avoid click button long time  by Qin yunhe 20200321
                strOPMIFW = "OPMI_FW: " + mainPage.Get_SystemFWversion("ICAN:02");
                strCCUFW = "CCU_FW: " + mainPage.Get_SystemFWversion("ICAN:06");

                if (systemType == "")
                {
                    MessageBox.Show("Get system type failed");
                    return;
                }
                if (systemType == peakMode)
                {
                    //Peak "EXTARO 300 Dental"
                    list.Add(localLogPeak);        //"EXTARO 300"
                }
                else if (systemType == startMode)
                {
                    //Start "EXTARO 300 ENT";
                    list.Add(localLogStart);        //"EXTARO 300 ENT"
                }
                else if (systemType == notAvailableMode)
                {
                    list.Add(localNotAvailable);//type
                }

                list.Add(localOperLog);    //"Operation Log"
                //add the CCB, OPMI, CCU FW Version in here Qin Yunhe 2020-03-20
                list.Add(strsystem_SN);
                list.Add(strCCBFW);
                list.Add(strOPMIFW);
                list.Add(strCCUFW);
                //list.Add(lic_raw);//add for record the lic20200525
                list.Add(Convert_Str(License.Activate_Lic));//add for record the lic20200525;//
                //add end
                list.Add(textBoxOperCount.Text);
                for (int i = 0; i < operInfoCount; i++)
                {
                    string stringI = Convert.ToString(i, 16);
                    stringI = stringI.PadLeft(4, '0');

                    operCmd = STX + CMD_OPER_LOG + CMD_ENT_RD + stringI + "$" + ETX;
                    serialPort1.Write(operCmd);
                    operResponse = serialPort1.ReadTo(ETX);
                    index = operResponse.IndexOf("$");
                    operResponse = operResponse.Substring(index);

                    if (operResponse.Substring(1, 2).Equals("OK"))
                    {
                        operResponse = operResponse.TrimStart(OK_HDR.ToCharArray());
                        operResponse = operResponse.TrimEnd('$');
                        //int stringLength = operResponse.Length / 2;
                        //string stringLengthHex = stringLength.ToString("X2");
                        string checkSum = CheckSum(operResponse);
                        list.Add(operResponse + checkSum);
                    }
                    textBoxLogInfo.Text = "Save operation log.Index = " + i.ToString();
                }
                StringBuilder stringLog = new StringBuilder();
                foreach (string lists in list)
                {
                    stringLog.AppendLine(lists);
                }
                System.IO.File.WriteAllText(path, stringLog.ToString(), ASCIIEncoding.ASCII);
                textBoxLogInfo.Text = "Save operation log end";
                MessageBox.Show("The operation log was saved successfully!");
            }
            catch
            {
                ;
            }
        }

        /// <summary>
        /// Import error log
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnErrImport_Click(object sender, EventArgs e)
        {
            string path = "";
            int lic_raw = 0;
            try
            {
                OpenFileDialog fileDialog = new OpenFileDialog();
                fileDialog.Title = "Please select a file";
                fileDialog.Filter = "exlog Files(*exlog)|*.exlog";
                if (fileDialog.ShowDialog() == DialogResult.OK)
                {
                    path = fileDialog.FileName;
                    FileStream logFileStream = new FileStream(path, FileMode.Open, FileAccess.Read);
                    List<string> logList = new List<string>();
                    StreamReader logStreamReader = new StreamReader(logFileStream);
                    //Read file
                    logStreamReader.BaseStream.Seek(0, SeekOrigin.Begin);
                    // read file end
                    string tmp = logStreamReader.ReadLine();
                    while (tmp != null)
                    {
                        logList.Add(tmp);
                        tmp = logStreamReader.ReadLine();
                    }
                    //close StreamReader
                    logStreamReader.Close();
                    logFileStream.Close();
                    int errInfoCount = 0 ;//20200321

                    //the following modified by Qinyunhe added the sentence logList[0].Equals(oldlocallogpeak) 20200321
                    //if ( logList[0].Equals(oldlocallogpeak) || (logList[0].Equals(localLogPeak)) || (logList[0].Equals(localLogStart) || logList[0].Equals(localNotAvailable)))
                    if ((logList[0].Equals(oldlocallogpeak)) || (logList[0].Equals(localLogPeak)) ||
                       (logList[0].Equals(localLogStart)) || (logList[0].Equals(oldlocalLogStart)) ||
                       (logList[0].Equals(localNotAvailable)))
                    {
                        
                        if (logList[1].Equals(localErrLog))//"Error Log"
                        {
                            //Error count 
                            //modified by Qin yunhe for lasted version
                            if (logList[0].Equals(oldlocallogpeak) || logList[0].Equals(oldlocalLogStart))
                            {
                                errInfoCount = Convert.ToInt32(logList[2]);
                            }
                            if (logList[0].Equals(localLogPeak) || logList[0].Equals(localLogStart))
                            {
                                lic_raw = Convert.ToInt32(logList[6]) + 1;
                                errInfoCount = Convert.ToInt32(logList[6 + lic_raw]);
                            }
                            //modified end 20200321
                            //20180201
                            if (logList[0].Equals(localLogPeak) || logList[0].Equals(oldlocallogpeak))
                            {
                                localSystemType = peakMode;
                            }
                            else if (logList[0].Equals(localLogStart) || logList[0].Equals(oldlocalLogStart))
                            {
                                localSystemType = startMode;
                            }
                            else if (logList[0].Equals(localNotAvailable))
                            {
                                localSystemType = notAvailableMode;
                            }
                            //modify by Qin yunhe for the lasted version
                            if (logList[0].Equals(oldlocallogpeak) || logList[0].Equals(oldlocalLogStart))
                                logList.RemoveRange(0, 3);//Remove 3 element
                            if (logList[0].Equals(localLogPeak) || logList[0].Equals(localLogStart))
                                logList.RemoveRange(0,7+ lic_raw);
                            //modified end
                            //20180126
                            if (!(errInfoCount == logList.Count))
                            {
                                MessageBox.Show(ErrorMsg.LogFileDataError + ".", "Warning!");
                                textBoxLogInfo.Text = ErrorMsg.LogFileDataError;
                                return;
                            }

                            btnErrFirst.Enabled = false;
                            btnErrForward.Enabled = false;
                            btnErrBack.Enabled = false;
                            btnErrLast.Enabled = false;
                            btnErrClean.Enabled = false;
                            gErrImport = true;
                            gErrLocalEntryIndex = 0;
                            //modified by Qin yunhe20200321
                            if (localSystemType == peakMode)
                            {
                                groupBoxErr.Text = "Error Log (Local Dental)";
                            }
                            else if (localSystemType == startMode)
                            {
                                groupBoxErr.Text = "Error Log (Local ENT)";//modified by Qin yunhe20200321
                            }
                            //end
                           // groupBoxErr.Text = "Error Log (Local)" + logList[0]; //modified by Qin yunhe added the logList[0]

                            errLogList = logList;
                            textBoxErrCount.Text = errInfoCount.ToString();
                            gLocalErrInfoTotalCount = errInfoCount;
                            this.listViewErr.Items.Clear();
                            this.listViewErr.BeginUpdate();

                            if (errInfoCount == 0)
                            {
                                btnErrLast.Enabled = false;
                                btnErrBack.Enabled = false;
                                textBoxLogInfo.Text = "Read local error log entry success,The local error log entry is 0";
                            }
                            else if ((errInfoCount <= 10) && (errInfoCount > 0)) //0-9
                            {
                                for (int i = 0; i < errInfoCount; i++)
                                {
                                    ErrImportCheck(logList[i], i);
                                }

                                btnErrLast.Enabled = false;
                                btnErrBack.Enabled = false;
                                textBoxLogInfo.Text = "Read local error log entry data:" + gErrLocalEntryIndex.ToString() + " - " + (errInfoCount - 1).ToString();
                            }
                            else if (errInfoCount > 10)
                            {
                                for (int i = 0; i < 10; i++)
                                {
                                    ErrImportCheck(logList[i], i);
                                }
                                btnErrLast.Enabled = true;
                                btnErrBack.Enabled = true;
                                textBoxLogInfo.Text = "Read local error log entry data:" + gErrLocalEntryIndex.ToString() + " - " + (gErrLocalEntryIndex + 9).ToString();
                            }
                            this.listViewErr.EndUpdate();
                        }
                        else
                        {
                            MessageBox.Show(ErrorMsg.LogFileDataError + ".", "Warning!");
                            textBoxLogInfo.Text = ErrorMsg.LogFileDataError;
                            return;
                        }
                    }
                    else
                    {

                        MessageBox.Show(ErrorMsg.LogFileDataError + ".", "Warning!");
                        textBoxLogInfo.Text = ErrorMsg.LogFileDataError;
                        return;
                    }
                }
            }
            catch
            {
                MessageBox.Show("Import Error Log failed.");
                textBoxLogInfo.Text = "Import error log failed";
            }
        }

        /// <summary>
        /// Import error log
        /// </summary>
        /// <param name="logInfo"></param>
        /// <param name="i"></param>
        private void ErrImportCheck(string logInfo, int i)
        {
            string stringCheck = logInfo.Substring(logInfo.Length - 2);
            //int intCheck = Convert.ToInt32(stringCheck, 16);
            //double stringLength = (logInfo.Length - 2) / 2.0;
            string errResponse = logInfo.Substring(0, logInfo.Length - 2);
            string checkSum = CheckSum(errResponse);

            if (stringCheck == checkSum)
            {
                ErrLogAnalysis(logInfo, i);
            }
            else
            {
                ListViewItem listViewError = new ListViewItem();
                listViewError.Text = i.ToString("000");
                listViewError.SubItems.Add("--");
                listViewError.SubItems.Add("--");
                listViewError.SubItems.Add("Data Corrupted");
                listViewError.ForeColor = Color.Red;
                this.listViewErr.Items.Add(listViewError);
            }
        }



        /// <summary>
        /// Import Operation log
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOperImport_Click(object sender, EventArgs e)
        {
            string path = "";
            int lic_raw = 0 ; // add by Yunhe Qin
            try
            {
                OpenFileDialog fileDialog = new OpenFileDialog();
                fileDialog.Title = "Please select a file";
                fileDialog.Filter = "exlog Files(*exlog)|*.exlog";
                if (fileDialog.ShowDialog() == DialogResult.OK)
                {
                    path = fileDialog.FileName;
                    FileStream logFileStream = new FileStream(path, FileMode.Open, FileAccess.Read);
                    List<string> logList = new List<string>();
                    StreamReader logStreamReader = new StreamReader(logFileStream);
                    //Read file
                    logStreamReader.BaseStream.Seek(0, SeekOrigin.Begin);
                    // read file end
                    string tmp = logStreamReader.ReadLine();
                    while (tmp != null)
                    {
                        logList.Add(tmp);
                        tmp = logStreamReader.ReadLine();
                    }
                    //close StreamReader
                    logStreamReader.Close();
                    logFileStream.Close();
                    int operInfoCount = 0;//20200321

                    if ((logList[0].Equals(oldlocallogpeak)) || (logList[0].Equals(localLogPeak)) || 
                        (logList[0].Equals(localLogStart)) || (logList[0].Equals(oldlocalLogStart) )|| 
                       (logList[0].Equals(localNotAvailable)))
                    {
                        if (logList[1].Equals(localOperLog))
                        {
                            //operation log count
                            //the flowing sentence added by Qin yunhe for laseted version 20200321
                            if (logList[0].Equals(oldlocallogpeak) || logList[0].Equals(oldlocalLogStart))
                            { 
                                 operInfoCount = Convert.ToInt32(logList[2]);
                            }
                            if (logList[0].Equals(localLogPeak) || logList[0].Equals(localLogStart))
                            {
                                lic_raw = Convert.ToInt32(logList[6]) + 1;
                                operInfoCount = Convert.ToInt32(logList[6 + lic_raw]);

                            }
                            //added end
                            //20180115
                            if (logList[0].Equals(localLogPeak) || logList[0].Equals(oldlocallogpeak))
                            {
                                localSystemType = peakMode;
                            }
                            else if (logList[0].Equals(localLogStart) || logList[0].Equals(oldlocalLogStart))
                            {
                                localSystemType = startMode;
                            }
                            else if (logList[0].Equals(localNotAvailable))
                            {
                                localSystemType = notAvailableMode;
                            }
                            //the flowing sentence modified by Qin yunhe 20200321
                            if (logList[0].Equals(oldlocallogpeak) || logList[0].Equals(oldlocalLogStart))
                            { 
                                logList.RemoveRange(0, 3);
                            }
                            if (logList[0].Equals(localLogPeak) || logList[0].Equals(localLogStart))
                            { 
                                logList.RemoveRange(0, 7 + lic_raw); 
                            }
                            //modified end
                            //20180126
                            if (!(operInfoCount == logList.Count))
                            {
                                MessageBox.Show(ErrorMsg.LogFileDataError + ".", "Warning!");
                                textBoxLogInfo.Text = ErrorMsg.LogFileDataError;
                                return;
                            }

                            btnOperFirst.Enabled = false;
                            btnOperForward.Enabled = false;
                            btnOperBack.Enabled = false;
                            btnOperLast.Enabled = false;
                            btnOperClean.Enabled = false;
                            gOperImport = true;
                            gOperLocalEntryIndex = 0;
                            //modified by Qin yunhe20200321
                            if (localSystemType == peakMode)
                            {
                                groupBoxOper.Text = "Operation Log (Local Dental)" ;
                            }
                            else if (localSystemType == startMode)
                            {
                                groupBoxOper.Text = "Operation Log (Local ENT)" ;//modified by Qin yunhe20200321
                            }
                            //end
                            operLogList = logList;
                            textBoxOperCount.Text = operInfoCount.ToString();
                            gLocalOperInfoTotalCount = operInfoCount;
                            this.listViewOper.Items.Clear();
                            this.listViewOper.BeginUpdate();
                            if (operInfoCount == 0)
                            {
                                btnOperLast.Enabled = false;
                                btnOperBack.Enabled = false;
                                textBoxLogInfo.Text = "Read local operation log entry success,The local operation log entry is 0";
                            }
                            else if ((operInfoCount <= 10) && (operInfoCount > 0)) //0-9
                            {
                                for (int i = 0; i < operInfoCount; i++)
                                {
                                    OperImportCheck(logList[i], i, localSystemType);
                                }

                                btnOperLast.Enabled = false;
                                btnOperBack.Enabled = false;
                                textBoxLogInfo.Text = "Read local operation log entry data:" + gOperLocalEntryIndex.ToString() + " - " + (operInfoCount - 1).ToString();
                            }
                            else if (operInfoCount > 10)
                            {
                                for (int i = 0; i < 10; i++)
                                {
                                    OperImportCheck(logList[i], i, localSystemType);
                                }
                                btnOperLast.Enabled = true;
                                btnOperBack.Enabled = true;
                                textBoxLogInfo.Text = "Read local operation log entry data:" + gOperLocalEntryIndex.ToString() + " - " + (gOperLocalEntryIndex + 9).ToString();
                            }
                            this.listViewOper.EndUpdate();
                        }
                        else
                        {
                            MessageBox.Show(ErrorMsg.LogFileDataError + ".", "Warning!");
                            textBoxLogInfo.Text = ErrorMsg.LogFileDataError;
                            return;
                        }
                    }
                    else
                    {
                        MessageBox.Show(ErrorMsg.LogFileDataError, "Warning!");
                        textBoxLogInfo.Text = ErrorMsg.LogFileDataError;
                        return;
                    }
                }

            }
            catch
            {
                MessageBox.Show("Import Operation Log failed.");
                textBoxLogInfo.Text = "Import operation log failed";
            }
        }

        /// <summary>
        /// Import operation log
        /// </summary>
        /// <param name="logInfo"></param>
        /// <param name="i"></param>
        private void OperImportCheck(string logInfo, int i, string systemMode)
        {
            string stringCheck = logInfo.Substring(logInfo.Length - 2);
            string operResponse = logInfo.Substring(0, logInfo.Length - 2);
            string checkSum = CheckSum(operResponse);
            if (stringCheck == checkSum)
            {
                OperLogAnalysis(logInfo, i, systemMode);
            }
            else
            {
                ListViewItem listViewOperation = new ListViewItem();
                listViewOperation.Text = i.ToString("000");
                listViewOperation.SubItems.Add("--");
                listViewOperation.SubItems.Add("--");
                listViewOperation.SubItems.Add("Data Corrupted");
                listViewOperation.ForeColor = Color.Red;
                this.listViewOper.Items.Add(listViewOperation);
            }
        }

        /// <summary>
        /// Set the appearance of the listview
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listViewOper_ColumnWidthChanging(object sender, ColumnWidthChangingEventArgs e)
        {
            e.Cancel = true;
            e.NewWidth = this.listViewOper.Columns[e.ColumnIndex].Width;
        }

        /// <summary>
        /// Set the appearance of the listview
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listViewErr_ColumnWidthChanging(object sender, ColumnWidthChangingEventArgs e)
        {
            e.Cancel = true;
            e.NewWidth = this.listViewErr.Columns[e.ColumnIndex].Width;
        }
    }
}
