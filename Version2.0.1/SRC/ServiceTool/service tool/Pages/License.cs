using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using System.Resources;

using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Collections;

namespace service_tool
{
    public partial class License : UserControl
    {
        [DllImport(@"FlexnetDecode.dll", EntryPoint = "FlexDecoder", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
        extern static int FlexDecoder(int argc, string[] argv, StringBuilder buffer);

        [DllImport(@"MD5Generator.dll", EntryPoint = "MD5_Calculate", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
        extern static int MD5_Calculate(byte[] result, byte[] buffer, int fileSize);

        public uint fControl1 = 0;
        public uint fControl2 = 0;
        public uint fControl3 = 0;
        public bool canUpload = false;
        public static MainMenu mainPage;
        public string checkSnError = "Carrier Arm SN mismatch!";
        public string licenseFilePath = System.AppDomain.CurrentDomain.BaseDirectory + "license\\";
        private static System.IO.Ports.SerialPort serialPort1;
        public int FW_VERSION = 21306;
        private string PERMANENT_DATE = "permanent";
        public static int rowCount = 0;//Number of merged cells
        public static string Activate_Lic = ""; // add by Yunhe Qin 20200520

        public License()
        {
            InitializeComponent();
            if (!Directory.Exists(licenseFilePath))
            {
                Directory.CreateDirectory(licenseFilePath);
            }
        }

        public void InitializeLicenseComponent(MainMenu mainMenu, System.IO.Ports.SerialPort serialPort)
        {
            mainPage = mainMenu;
            serialPort1 = serialPort;
        }

        public Features featFromFile = new Features();

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            //if (!serialPort1.IsOpen)
            //{
            //    MessageBox.Show("Com port is not open!", "Warning!");
            //    return;
            //}
            //btnUpdate.Enabled = false;
            var currentDirectory = System.Environment.CurrentDirectory;
            string realFileName = "license_upload.bin";
            string realFilePath = licenseFilePath + realFileName;
            try
            {
                if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                    return;
                string fileName = openFileDialog1.FileName;
                this.tbFile.Text = fileName;

                if (!fileName.Equals("") && File.Exists(fileName))
                {
                    File.Copy(fileName, realFilePath, true);
                    // File.Copy(fileName, currentDirectory + @"\" + realFileName, true);
                    featFromFile = new Features();
                    if (ReadFromCard(realFilePath, ref featFromFile))
                    {
                        canUpload = true;
                        RefreshLocalControls(featFromFile, true);
                    }
                    else
                    {
                        canUpload = false;
                        RefreshLocalControls(featFromFile, false);
                    }
                }
            }
            finally
            {
                if (File.Exists(realFilePath))
                {
                    File.Delete(realFilePath);
                }
            }
            if (canUpload)
                btnUpdate.Enabled = true;
        }

        public void EnableControls(bool isEnable)
        {
            //this.btnBrowse.Enabled = isEnable;
            //this.btnServer.Enabled = isEnable;
        }
        private void RefreshLocalControls(Features feat, bool checkSn)
        {
            //DisplayTextBox(tbLocalClassroom, feat.ClassroomStreaming, checkSn);
            DisplayGridViewValue(DgvLocalLicense, feat.FluorescenceMode, feat.FluorescenceModeValidDate, checkSn, 0, 3);
            DisplayGridViewValue(DgvLocalLicense, feat.NoGlareMode, feat.NoGlareModeValidDate, checkSn, 1, 3);
            DisplayGridViewValue(DgvLocalLicense, feat.LightBoost, feat.LightBoostValidDate, checkSn, 2, 3);
            DisplayGridViewValue(DgvLocalLicense, feat.TrueLightMode, feat.TrueLightModeValidDate, checkSn, 3, 3);
            DisplayGridViewValue(DgvLocalLicense, feat.MultispectralMode, feat.MultispectralModeValidDate, checkSn, 4, 3);
            //DisplayGridViewValue(DgvLocalLicense, feat.VioletMode, feat.TrueLightModeValidDate, checkSn, 5, 3);
            DisplayGridViewValue(DgvLocalLicense, feat.DICOMConnectivity, feat.DICOMConnectivityValidDate, checkSn, 5, 3);
        }

        private bool CheckCmdReponse(string strReponse)
        {
            if (strReponse.IndexOf("$OK") == -1)
            {
                commandProgress = Utils.GetCommandResult(strReponse);
                ShowPrograss();
                return false;
            }
            return true;
        }

        private bool RefreshBoardControls(Features feat, bool supportDemoLic,bool checkSn)
        {
            if (checkSn)
            {
                Dictionary<int, string> featureDictionary = GetFeaturesDictionary(feat);

                //StreamWriter myWriter = new StreamWriter("result.txt");
                string str = "$" + BaseCmd.FeatGet + "$";//"$SCONF:GET$";
                commandProgress = "Feature get ...";
                ShowPrograss();
                string strReponse = mainPage.SetComment(str);
                //myWriter.WriteLine(str);
                //myWriter.WriteLine(strReponse);
                //myWriter.Close();
                if (CheckCmdReponse(strReponse))
                {
                    string retValue = strReponse.Substring(strReponse.IndexOf("$OK") + 4, 2);
                    byte[] byteValue = strToToHexByte(retValue);

                    BitArray arr = new BitArray(byteValue);
#if false
                    var f2 = feat.TrueLightMode == Convert.ToInt32(arr[2]);
                    var f3 = feat.FluorescenceMode == Convert.ToInt32(arr[3]);
                    var f4 = feat.NoGlareMode == Convert.ToInt32(arr[4]);
                    var f5 = feat.LightBoost == Convert.ToInt32(arr[5]);
                    //20180125
                    var f6 = feat.MultispectralMode == Convert.ToInt32(arr[6]);
                    var f7 = feat.VioletMode == Convert.ToInt32(arr[7]);
#else

                    if (supportDemoLic)
                    {
                        var f2 = feat.TrueLightMode == Convert.ToInt32(arr[2]);
                        var f3 = feat.FluorescenceMode == Convert.ToInt32(arr[3]);
                        var f4 = feat.NoGlareMode == Convert.ToInt32(arr[4]);
                        var f5 = feat.LightBoost == Convert.ToInt32(arr[5]);
                        //20180125
                        var f6 = feat.MultispectralMode == Convert.ToInt32(arr[6]);
#if false//20190321 VioletMode  and  DICOM Connectivity  is 7bit ,Reserved 
                        var f7 = feat.VioletMode == Convert.ToInt32(arr[7]);
                        if (!(f2 && f3 && f4 && f5 && f6 && f7))
                        {
                            MessageBox.Show("Invalid Feature Name detected!");
                        }
                        else
                            MessageBox.Show("Read successful!");
#else
                        if (!(f2 && f3 && f4 && f5 && f6))
                        {
                            MessageBox.Show("Invalid Feature Name detected!");
                        }
                        /* the following sentence were disabled by Qin yunhe 20200526*/
                        //else 
                        //    MessageBox.Show("");
#endif

                        DisplaySystemLicenseValue(supportDemoLic, feat.FluorescenceMode, feat.FluorescenceModeValidDate, checkSn, 0, 3);
                        DisplaySystemLicenseValue(supportDemoLic, feat.NoGlareMode, feat.NoGlareModeValidDate, checkSn, 1, 3);
                        DisplaySystemLicenseValue(supportDemoLic, feat.LightBoost, feat.LightBoostValidDate, checkSn, 2, 3);
                        DisplaySystemLicenseValue(supportDemoLic, feat.TrueLightMode, feat.TrueLightModeValidDate, checkSn, 3, 3);
                        DisplaySystemLicenseValue(supportDemoLic, feat.MultispectralMode, feat.MultispectralModeValidDate, checkSn, 4, 3);
                        DisplaySystemLicenseValue(supportDemoLic, feat.DICOMConnectivity, feat.DICOMConnectivityValidDate, checkSn, 5, 3);
                    }
                    else
                    {
                        var f2 = Convert.ToInt32(arr[2]);
                        var f3 = Convert.ToInt32(arr[3]);
                        var f4 = Convert.ToInt32(arr[4]);
                        var f5 = Convert.ToInt32(arr[5]);
                        //20180125
                        var f6 = Convert.ToInt32(arr[6]);
                        //var f7 = Convert.ToInt32(arr[7]);

                        bool checkFlag = true;
                        if (f2 == 1 && feat.TrueLightMode != 1)
                        {
                            checkFlag = false;
                        }
                        if (f3 == 1 && feat.FluorescenceMode != 1)
                        {
                            checkFlag = false;
                        }
                        if (f4 == 1 && feat.NoGlareMode != 1)
                        {
                            checkFlag = false;
                        }
                        if (f5 == 1 && feat.LightBoost != 1)
                        {
                            checkFlag = false;
                        }
                        if (f6 == 1 && feat.MultispectralMode != 1)
                        {
                            checkFlag = false;
                        }
#if false //20190327
                        if (f7 == 1 && feat.VioletMode != 1)
                        {
                            checkFlag = false;
                        }
#endif
                        if (!checkFlag)
                        {
                            MessageBox.Show("Invalid Feature Name detected!");
                        }
                        else
                            MessageBox.Show("Read successful!");


                        DisplaySystemLicenseValue(supportDemoLic, f3, feat.FluorescenceModeValidDate, checkSn, 0, 3);
                        DisplaySystemLicenseValue(supportDemoLic, f4, feat.NoGlareModeValidDate, checkSn, 1, 3);
                        DisplaySystemLicenseValue(supportDemoLic, f5, feat.LightBoostValidDate, checkSn, 2, 3);
                        DisplaySystemLicenseValue(supportDemoLic, f2, feat.TrueLightModeValidDate, checkSn, 3, 3);
                        DisplaySystemLicenseValue(supportDemoLic, f6, feat.MultispectralModeValidDate, checkSn, 4, 3);
                        DisplaySystemLicenseValue(supportDemoLic, feat.DICOMConnectivity, feat.DICOMConnectivityValidDate, checkSn, 5, 3);
                    }
#endif

#if false
                    //20180125
                    if (!(f2 && f3 && f4 && f5 && f6 && f7))
                    {
                        MessageBox.Show("Invalid Feature Name detected!");
                    }
                    else
                        MessageBox.Show("Read successful!");
#endif
                }
                else
                {
                    checkSn = false;
                }
            }
#if false
            //DisplayTextBox(tbBoardClassroom, feat.ClassroomStreaming, checkSn);
            DisplaySystemLicenseValue(supportDemoLic, feat.FluorescenceMode, feat.FluorescenceModeValidDate, checkSn, 0, 3);
            DisplaySystemLicenseValue(supportDemoLic, feat.NoGlareMode, feat.NoGlareModeValidDate, checkSn, 1, 3);
            DisplaySystemLicenseValue(supportDemoLic, feat.LightBoost, feat.LightBoostValidDate, checkSn, 2, 3);
            DisplaySystemLicenseValue(supportDemoLic, feat.TrueLightMode, feat.TrueLightModeValidDate, checkSn, 3, 3);
            DisplaySystemLicenseValue(supportDemoLic, feat.MultispectralMode, feat.MultispectralModeValidDate, checkSn, 4, 3);
            //DisplayGridViewValue(DgvSystemLicense, feat.VioletMode, feat.VioletModeValidDate, checkSn, 5, 3);
            DisplaySystemLicenseValue(supportDemoLic, feat.DICOMConnectivity, feat.DICOMConnectivityValidDate, checkSn, 5, 3);
#endif
            if (!checkSn)
                return false;
            return true;
        }


        private void DisplayTextBox(TextBox textBox, int checkValue, bool checkSn)
        {
            try
            {
                if (!checkSn)
                {
                    textBox.Invoke(new Action(() =>
                    {
                        textBox.BackColor = Color.LightGray;
                        textBox.Text = "--";
                    }));
                    return;
                }
                if (checkValue == 1)
                {
                    textBox.Invoke(new Action(() =>
                    {
                        textBox.BackColor = Color.Green;
                        textBox.Text = "ON";
                    }));
                }
                else
                {
                    textBox.Invoke(new Action(() =>
                    {
                        textBox.BackColor = Color.Red;
                        textBox.Text = "OFF";
                    }));
                }
            }
            catch
            {
                ;
            }
        }

        private void DisplayGridViewValue(DataGridView dataGridView, int checkValue, string ValidDate,bool checkSn, int axis_X, int axis_Y)
        {
            try
            {
                // dataGridView.Invoke(new Action(() =>
                // {
                if (!checkSn)
                {
                    dataGridView.Rows[axis_X].Cells[axis_Y].Style.BackColor = Color.LightGray;
                    dataGridView.Rows[axis_X].Cells[axis_Y].Value = "--";
                    dataGridView.Rows[axis_X].Cells[axis_Y - 1].Value = "--";
                    return;
                }
                if (checkValue == 1)
                {
                    dataGridView.Rows[axis_X].Cells[axis_Y].Style.BackColor = Color.Green;
                    dataGridView.Rows[axis_X].Cells[axis_Y].Value = "ON";
                    if (ValidDate == PERMANENT_DATE)
                    {
                        dataGridView.Rows[axis_X].Cells[axis_Y - 1].Value = "Permanent";
                    }
                    else
                        dataGridView.Rows[axis_X].Cells[axis_Y - 1].Value = ValidDate;
                }
                else
                {
                    dataGridView.Rows[axis_X].Cells[axis_Y].Style.BackColor = Color.Red;
                    dataGridView.Rows[axis_X].Cells[axis_Y].Value = "OFF";
                    dataGridView.Rows[axis_X].Cells[axis_Y - 1].Value = "--";
                }
                //}));
            }
            catch
            {
                ;
            }
        }

        private void DisplaySystemLicenseValue(bool supportDemoLic,int checkValue, string ValidDate, bool checkSn, int axis_X, int axis_Y)
        {
            try
            {
                // dataGridView.Invoke(new Action(() =>
                // {
                if (!checkSn)
                {
                    DgvSystemLicense.Rows[axis_X].Cells[axis_Y].Style.BackColor = Color.LightGray;
                    DgvSystemLicense.Rows[axis_X].Cells[axis_Y].Value = "--";
                    DgvSystemLicense.Rows[axis_X].Cells[axis_Y - 1].Value = "--";
                    return;
                }
                if (checkValue == 1)
                {
                    DgvSystemLicense.Rows[axis_X].Cells[axis_Y].Style.BackColor = Color.Green;
                    DgvSystemLicense.Rows[axis_X].Cells[axis_Y].Value = "ON";
                    if (supportDemoLic)
                        DgvSystemLicense.Rows[axis_X].Cells[axis_Y - 1].Value = ValidDate;
                    else
                        DgvSystemLicense.Rows[axis_X].Cells[axis_Y - 1].Value = "Permanent";
                }
                else
                {
                    DgvSystemLicense.Rows[axis_X].Cells[axis_Y].Style.BackColor = Color.Red;
                    DgvSystemLicense.Rows[axis_X].Cells[axis_Y].Value = "OFF";
                    DgvSystemLicense.Rows[axis_X].Cells[axis_Y - 1].Value = "--";
                }
                //}));
            }
            catch
            {
                ;
            }
        }



        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (!serialPort1.IsOpen)
                {
                    MessageBox.Show("Com port is not open!", "Warning!");
                    return;
                }

                DialogResult MsgBoxResult = MessageBox.Show("You are about to install a new license file, proceed?", "Warning!", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                if (MsgBoxResult == DialogResult.Yes)
                {
                    if (!File.Exists(tbFile.Text))
                    {
                        MessageBox.Show("License file does not exist.");
                        return;
                    }

                    if (!canUpload)
                    {
                        MessageBox.Show(checkSnError);
                        return;
                    }

                    if (threadUploadEndFlag)
                        StartThread();
                    else
                    {
                        MessageBox.Show("License file uploading now! ");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Update failed, please try again.\r\n\r\n" + ex.ToString(), "Warning!");
            }
        }

        public bool GetFWVersion()
        {
            MainMenu mainMenu = new MainMenu();
            string strCommand = "$VER:ICAN:01$"; 
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
                        string decimalVer = mainMenu.GetDecimalVersionCan(strRespone);
                        int index = decimalVer.IndexOf("APP.");
                        decimalVer = decimalVer.Substring(index + 4, decimalVer.Length -index - 4);
                        decimalVer = Regex.Replace(decimalVer, @"[^\d]*", "");
                        int intVer = Convert.ToInt32(decimalVer);
                        if (intVer >= FW_VERSION)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else if (strResultFlag.Equals("NG"))
                    {
                        MessageBox.Show(Utils.GetCommandResult(strRespone));
                    }
                }
                else
                {
                    MessageBox.Show(strRespone);
                }
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }
        }

        public string GetVersionNum(string str)
        {
            string cmd;
            string response = "";

            // read license header
            cmd = MainMenu.STX + str + MainMenu.ETX;
            try
            {
                serialPort1.Write(cmd);
                if (cmd.Contains("06"))
                    Thread.Sleep(100);//20170508
                //while (serialPort1.BytesToRead == 0) { }
                response = serialPort1.ReadTo(MainMenu.ETX);
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

        public static byte[] StrToHexByte(string hexString)
        {
            hexString = hexString.Replace(" ", "");
            if ((hexString.Length % 2) != 0)
                hexString += " ";
            byte[] returnBytes = new byte[hexString.Length / 2];
            for (int i = 0; i < returnBytes.Length; i++)
                returnBytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
            return returnBytes;
        }
        //the following function added by Yunhe Qin 
         public void LoadActiveLicense()
        {
            if (!serialPort1.IsOpen)
            {
                MessageBox.Show("Com port is not open!", "Warning!");
                return;
            }
            if (threadReadEndFlag)
                StartReadBoardThread();
            else
            {
                MessageBox.Show("License file reading now!");
            }
        }
        //end 
 
        private void btnGet_Click(object sender, EventArgs e)
        {
            try
            {
                if (!serialPort1.IsOpen)
                {
                    MessageBox.Show("Com port is not open!", "Warning!");
                    return;
                }
                if (threadReadEndFlag)
                    StartReadBoardThread();
                else
                {
                    MessageBox.Show("License file reading now!");
                }
            }
            catch { }
        }



        public void ReadFromBoard()
        {
            //Get FW version  20190317
            bool supportDemoLic = GetFWVersion();

            string readFileName = licenseFilePath + "license_read.bin";

            commandProgress = "Read Head Start...";
            ShowPrograss();
            Features features = new Features();
            try
            {
                string strReadHead = mainPage.SetComment("$LIC:HEAD:RD$");
                strReadHead = strReadHead.Substring(strReadHead.IndexOf("$OK"));
                if (strReadHead.StartsWith("$OK"))
                {
                    int lastMhIndex = strReadHead.LastIndexOf(":");
                    string aaCode = strReadHead.Substring(lastMhIndex - 2, 2);
                    if (!aaCode.ToUpper().Equals("AA"))
                    {
                        MessageBox.Show("License is invalid!");
                        return;
                    }
                    string lowOrder = strReadHead.Substring(36, 2);
                    string highOrder = strReadHead.Substring(38, 2);

                    byte[] byteData = StrToHexByte(highOrder + lowOrder);


                    int blksize = 26;
                    int restLen = (int)(byteData[1] | byteData[0] << 8);
                    if (restLen > 1024 * 10)
                    {
                        MessageBox.Show("License file size invalid!");
                        return;
                    }
                    int index = 0;
                    int transLen = 0;
                    string str = "";
                    string strReponse = "";
                    if (File.Exists(readFileName))
                        File.Delete(readFileName);
                    using (FileStream fs = new FileStream(readFileName, FileMode.OpenOrCreate))
                    {
                        commandProgress = "Read BLK start ...";
                        ShowPrograss();
                        do
                        {
                            str = "$LIC:BLK:RD:";
                            str += Convert.ToString(index, 16).PadLeft(4, '0') + ":";

                            if (restLen > blksize)
                            {
                                blksize = 26;
                            }
                            else
                            {
                                blksize = restLen;
                            }
                            str += Convert.ToString(blksize, 16).PadLeft(2, '0') + "$";
                            commandProgress = "Read BLK at index = " + index.ToString() + " ...";
                            ShowPrograss();
                            strReponse = mainPage.SetComment(str);

                            var okIndex = strReponse.IndexOf("$OK") + 4;
                            var endIndex = strReponse.LastIndexOf(":");
                            byte[] byteReponseData;
                            if (okIndex > 0 && endIndex > 0 && endIndex > okIndex)
                            {
                                byteReponseData = strToToHexByte(strReponse.Substring(okIndex, endIndex - okIndex));
                                fs.Write(byteReponseData, 0, byteReponseData.Length);
                            }
                            transLen += blksize;
                            restLen -= blksize;
                            index++;
                            if (restLen <= 0)
                            {
                                break;
                            }

                        } while (true);
                        fs.Close();
                        commandProgress = "Read BLK end ...";

                        ShowPrograss();

                    }


                    if (ReadFromCard(readFileName, ref features))
                    {
                        //WriteActivateLic(Activate_Lic);//add for debug 20200525
                        if (RefreshBoardControls(features, supportDemoLic, true))
                        {
                            commandProgress = "Read successful ...";
                            ShowPrograss();
                        }

                       
                    }
                    else
                        RefreshBoardControls(features, supportDemoLic, false);
                }
            }
            catch
            {
                MessageBox.Show("read license error!", "Warning!");
                RefreshBoardControls(features, supportDemoLic, false);
            }
#if false    //add debug by Qin yunhe for solve the license reuse problem 20200321
            finally
            {
                if (File.Exists(readFileName))
                    File.Delete(readFileName);
            }
#endif
        }

        public static byte[] strToToHexByte(string hexString)
        {
            hexString = hexString.Replace(" ", "");
            if ((hexString.Length % 2) != 0)
                hexString += " ";
            byte[] returnBytes = new byte[hexString.Length / 2];
            for (int i = 0; i < returnBytes.Length; i++)
                returnBytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
            return returnBytes;
        }

        public string byteToHexStr(byte[] bytes)
        {
            string returnStr = "";
            if (bytes != null)
            {
                for (int i = 0; i < bytes.Length; i++)
                {
                    returnStr += bytes[i].ToString("X2");
                }
            }
            return returnStr;
        }

        private void btnAdvanced_Click(object sender, EventArgs e)
        {
            AdvancedForm advancedForm = new AdvancedForm(mainPage);
            advancedForm.ShowDialog();
        }

        public bool ReadFromCard(string binFileName, ref Features feat)
        {
            int argc = 2;
            string[] argv = new String[2];
            argv[0] = "fake";
            if (!binFileName.Equals(""))
            {
                argv[1] = binFileName;
                StringBuilder buffer = new StringBuilder("", 4096 * 2);
                try
                {
                    FlexDecoder(argc, argv, buffer);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                    return false;
                }

                string strBuffer = buffer.ToString();

                if (strBuffer.Equals(""))
                {
                    MessageBox.Show("Can not read the license file :" + binFileName + " in the loading local license file!");
                    return false;
                }
                if (serialPort1.IsOpen)
                {
                    if (!CheckSN(strBuffer))
                    {
                        return false;
                    }
                }
                Activate_Lic = strBuffer;// for debug by Qin yunhe 2020-05-25
                feat = GetFeatures(strBuffer);
#if false
                //HW Features
                if (strBuffer.IndexOf("MED_EXTARO_Fluorescence_mode") > -1)
                    feat.FluorescenceMode = 1;
                if (strBuffer.IndexOf("MED_EXTARO_NoGlare_mode") > -1)
                    feat.NoGlareMode = 1;
                if (strBuffer.IndexOf("MED_EXTARO_LightBoost") > -1)
                    feat.LightBoost = 1;
                if (strBuffer.IndexOf("MED_EXTARO_TrueLight_mode") > -1)
                    feat.TrueLightMode = 1;
                //20180125
                if (strBuffer.IndexOf("MED_EXTARO_Multispectral_mode") > -1)
                    feat.MultispectralMode = 1;
                if (strBuffer.IndexOf("MED_EXTARO_Violet_mode") > -1)
                    feat.VioletMode = 1;
                //SW Features
                //if (strBuffer.IndexOf("MED_EXTARO_Classroom") > -1)
                //    feat.ClassroomStreaming = 1;
                if (strBuffer.IndexOf("MED_EXTARO_DICOM") > -1)
                    feat.DICOMConnectivity = 1;
#else

#endif
            }
            return true;
        }

        private Features GetFeatures(string strBuffer)
        {
            Features feat = new Features();  
            string[] bufferArray = Regex.Split(strBuffer, "\n", RegexOptions.IgnoreCase);

            string[] licenseInfo;
            foreach (string bufferLicense in bufferArray)
            {
                if (bufferLicense.Contains("MED_EXTARO_Fluorescence_mode")//MED_EXTARO_Fluorescence_mode
                    || bufferLicense.Contains("MED_EXTARO_Demo_FL_mode"))
                {
                    licenseInfo = Regex.Split(bufferLicense, " ", RegexOptions.IgnoreCase);
                    if (feat.FluorescenceMode == 1)
                    {
                        if (licenseInfo[3] != PERMANENT_DATE)
                        {
                            continue;
                        }
                    }
                    feat.FluorescenceMode = 1;
                    feat.FluorescenceModeValidDate = licenseInfo[3];
                }
                if (bufferLicense.Contains("MED_EXTARO_NoGlare_mode")
                    || bufferLicense.Contains("MED_EXTARO_Demo_NoGlare_mode"))//MED_EXTARO_NoGlare_mode
                {
                    licenseInfo = Regex.Split(bufferLicense, " ", RegexOptions.IgnoreCase);
                    if (feat.NoGlareMode == 1)
                    {
                        if (licenseInfo[3] != PERMANENT_DATE)
                        {
                            continue;
                        }
                    }
                    feat.NoGlareMode = 1;
                    feat.NoGlareModeValidDate = licenseInfo[3];
                }
                if (bufferLicense.Contains("MED_EXTARO_LightBoost")
                    || bufferLicense.Contains("MED_EXTARO_Demo_LightBoost"))//MED_EXTARO_LightBoost
                {
                    licenseInfo = Regex.Split(bufferLicense, " ", RegexOptions.IgnoreCase);
                    if (feat.LightBoost == 1)
                    {
                        if (licenseInfo[3] != PERMANENT_DATE)
                        {
                            continue;
                        }
                    }
                    feat.LightBoost = 1;
                    feat.LightBoostValidDate = licenseInfo[3];
                }
                if (bufferLicense.Contains("MED_EXTARO_TrueLight_mode")
                    || bufferLicense.Contains("MED_EXTARO_Demo_TrueLight_mode"))//MED_EXTARO_TrueLight_mode
                {
                    licenseInfo = Regex.Split(bufferLicense, " ", RegexOptions.IgnoreCase);
                    if (feat.TrueLightMode == 1)
                    {
                        if (licenseInfo[3] != PERMANENT_DATE)
                        {
                            continue;
                        }
                    }
                    feat.TrueLightMode = 1;
                    feat.TrueLightModeValidDate = licenseInfo[3];
                }
                if (bufferLicense.Contains("MED_EXTARO_Multispectral_mode")
                    || bufferLicense.Contains("MED_EXTARO_Demo_Multispec_mode"))//MED_EXTARO_Multispectral_mode
                {
                    licenseInfo = Regex.Split(bufferLicense, " ", RegexOptions.IgnoreCase);
                    if (feat.MultispectralMode == 1)
                    {
                        if (licenseInfo[3] != PERMANENT_DATE)
                        {
                            continue;
                        }
                    }
                    feat.MultispectralMode = 1;
                    feat.MultispectralModeValidDate = licenseInfo[3];
                }
                if (bufferLicense.Contains("Violet"))//MED_EXTARO_Violet_mode
                {
                    licenseInfo = Regex.Split(bufferLicense, " ", RegexOptions.IgnoreCase);
                    if (feat.VioletMode == 1)
                    {
                        if (licenseInfo[3] != PERMANENT_DATE)
                        {
                            continue;
                        }
                    }
                    feat.VioletMode = 1;
                    feat.VioletModeValidDate = licenseInfo[3];
                }
                if (bufferLicense.Contains("DICOM"))//MED_EXTARO_DICOM
                {
                    licenseInfo = Regex.Split(bufferLicense, " ", RegexOptions.IgnoreCase);
                    if (feat.DICOMConnectivity == 1)
                    {
                        if (licenseInfo[3] != PERMANENT_DATE)
                        {
                            continue;
                        }
                    }
                    feat.DICOMConnectivity = 1;
                    feat.DICOMConnectivityValidDate = licenseInfo[3];
                }
                 
            }
            return feat;
        }

        private Dictionary<int, string> GetFeaturesDictionary(Features feature)
        {
            Dictionary<int, string> featureDictionary = new Dictionary<int, string>();
            if (feature.GreenLightMode == 1)
            {//0x00
                featureDictionary.Add((int)FeatureID.GreenLightMode, feature.GreenLightModeValidDate);
            }
            if (feature.OrangecolorMode == 1)
            {//0x01
                featureDictionary.Add((int)FeatureID.orangeColorMode, feature.OrangecolorModeValidDate);
            }
            if (feature.TrueLightMode == 1)
            {//0x02
                featureDictionary.Add((int)FeatureID.TrueLightMode, feature.TrueLightModeValidDate);
            }
            if (feature.FluorescenceMode == 1)
            {//0x03
                featureDictionary.Add((int)FeatureID.FluorescenceMode, feature.FluorescenceModeValidDate);
            }
            if (feature.NoGlareMode == 1)
            {//0x04
                featureDictionary.Add((int)FeatureID.NoGlareMode, feature.NoGlareModeValidDate);
            }
            if (feature.LightBoost == 1)
            {//0x05
                featureDictionary.Add((int)FeatureID.LightBoost, feature.LightBoostValidDate);
            }
            if (feature.MultispectralMode == 1)
            {//0x06
                featureDictionary.Add((int)FeatureID.MultispectralMode, feature.MultispectralModeValidDate);
            }
            if (feature.DICOMConnectivity == 1)
            {//0x07
                featureDictionary.Add((int)FeatureID.DICOMConnectivity, feature.DICOMConnectivityValidDate);
            }
            return featureDictionary;
        }

        public bool CheckSN(string strBuffer)
        {
            commandProgress = "Check Carrier Arm SN start!";
            ShowPrograss();
            var strReponse = "";
            int hostIdIndex = strBuffer.IndexOf("HOSTID=ID_STRING");
            var hostIdVal = "";
            //StreamWriter myWriter = new StreamWriter("result.txt");
            try
             {
                var strBufferHost = strBuffer.Substring(hostIdIndex);
                int hostIdEndIndex = strBufferHost.IndexOf("\n");

                var hostIdTxt = strBufferHost.Substring(0, hostIdEndIndex);
                var hostIdArray = hostIdTxt.Split('=');

                if (hostIdArray.Length > 2)
                    hostIdVal = Convert.ToString(hostIdArray[2]);
                if (hostIdVal.IndexOf(' ') > -1)
                {
                    hostIdVal = hostIdVal.Substring(0, hostIdVal.IndexOf(' '));
                }

                var str = "$SSN:GET$";
                strReponse = mainPage.SetComment(str);
                //myWriter.WriteLine(str);
                //myWriter.WriteLine(strReponse);
                var okIndex = strReponse.IndexOf("$OK");
                strReponse = strReponse.Substring(okIndex + 4).TrimEnd('$');

                if (!hostIdVal.Equals(strReponse))
                {
                    MessageBox.Show(checkSnError);
                    return false;
                }
            }
            catch
            {
                MessageBox.Show("Check Carrier Arm SN error!", "Warning!");
                return false;
            }
            finally
            {
                commandProgress = "Ckeck Carrier Arm SN end!";
                ShowPrograss();
                //myWriter.Close();
            }

            return true;
        }

        public string commandProgress = "";

        private void WriteDataToEeprom(string fileName)
        {
            try
            {
                if (fileName.Equals(""))
                {
                    MessageBox.Show("load file before upload!");
                    return;
                }

                //Get FW version  20190317
                bool supportDemoLic = GetFWVersion();

#if true //20170401 //check Carrier Arm SN
                int argc = 2;
                string[] argv = new String[2];
                argv[0] = "fake";
                argv[1] = fileName;
                StringBuilder buffer = new StringBuilder("", 4096 * 2);
                try
                {
                    FlexDecoder(argc, argv, buffer);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                    return;
                }

                string strBuffer = buffer.ToString();
                Features featList = GetFeatures(strBuffer);
                Dictionary<int, string> featureDictionary = GetFeaturesDictionary(featList);
#if true
                bool isValidDate = true;
                foreach (var featureElement in featureDictionary)
                {
                    string strValidDate = featureElement.Value;
                    isValidDate = isValidDate && ((featureElement.Value == PERMANENT_DATE) ? true : false);
                }

                if (isValidDate == false)//unValid Date
                {
                    if (supportDemoLic == false)//unsupport Demo
                    {
                        DialogResult MsgBoxResult = MessageBox.Show("Demo license is not supported on the current EXTARO 300 system,"
                        + " only permanent features will be activated,"
                        + " please update the system's firmware to version 2.13.6 or higher to support demo license.", "Warning!", MessageBoxButtons.YesNo,
                        MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);

                        if (MsgBoxResult != DialogResult.Yes)
                        {
                            return;
                        }
                    }
                }
#endif
                if (strBuffer.Equals(""))
                {
                    MessageBox.Show("Can not read the license file :" + fileName + " in the updating license file!");
                    return;
                }
                if (!CheckSN(strBuffer))
                {
                    return;
                }
#endif
                //StreamWriter myWriter = new StreamWriter("result.txt");
                FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                byte cc = 0;
                long len = fs.Length;
                byte[] buff = new byte[len];
                char[] head = new char[18];
                fs.Read(buff, 0, (int)buff.Length);
                fs.Close();

                byte[] md5 = new byte[16];
                MD5_Calculate(md5, buff, (int)buff.Length);

                string str = "$LIC:HEAD:WR:";
                string temp = "";
                for (byte i = 0; i < 16; i++)
                {
                    temp += Convert.ToString(md5[i], 16).PadLeft(2, '0');
                    cc += md5[i];
                }

                temp += Convert.ToString(len & 0x000000FF, 16).PadLeft(2, '0');
                cc += (byte)(len & 0x00FF);
                temp += Convert.ToString(len >> 8, 16).PadLeft(2, '0');
                cc += (byte)(len >> 8);
                temp += ":";
                temp += Convert.ToString(cc, 16).PadLeft(2, '0');
                str += temp += "$";
                commandProgress = "Write Head Start...";
                ShowPrograss();
                string strReponse = mainPage.SetComment(str);
                commandProgress = "Write Head End...";
                ShowPrograss();
                //myWriter.WriteLine(str);
                //myWriter.WriteLine(strReponse);

                int blksize = 26;
                int totalCount = 0;
                if (len % 26 == 0)
                {
                    totalCount = (int)len / 26;
                }
                else
                {
                    totalCount = (int)len / 26 + 1;
                }

                int index = 0;
                int transLen = 0;
                int restLen = (int)len;
#if true
                commandProgress = "Write BLK Start...";
                ShowPrograss();
                do
                {
                    str = "$LIC:BLK:WR:";
                    str += Convert.ToString(index, 16).PadLeft(4, '0') + ":";

                    if (restLen > blksize)
                    {
                        blksize = 26;
                    }
                    else
                    {
                        blksize = restLen;
                    }
                    cc = 0;
                    for (int i = 0; i < blksize; i++)
                    {
                        str += Convert.ToString(buff[transLen + i], 16).PadLeft(2, '0');
                        cc += buff[transLen + i];
                    }
                    str += ":";
                    str += Convert.ToString(cc, 16).PadLeft(2, '0');
                    str += "$";
                    commandProgress = "Write BLK at index = " + index.ToString() + "...";
                    ShowPrograss();
                    strReponse = mainPage.SetComment(str);
                    //myWriter.WriteLine(str);
                    //myWriter.WriteLine(strReponse);
                    //Thread.Sleep(200);
                    transLen += blksize;
                    restLen -= blksize;
                    index++;
                    if (restLen <= 0)
                    {
                        break;
                    }

                } while (true);
                commandProgress = "Write BLK End...";
                ShowPrograss();
#endif
                //Thread.Sleep(100);
                str = "$LIC:VALIDATE$";
                commandProgress = "Validate Start...";
                ShowPrograss();
                strReponse = mainPage.SetComment(str);
                commandProgress = "Validate end...";
                ShowPrograss();
                //myWriter.WriteLine(str);
                //myWriter.WriteLine(strReponse);


                str = "$" + BaseCmd.FeatGet + "$";// "$SCONF:GET$";
                commandProgress = "Feature get ...";
                ShowPrograss();
                strReponse = mainPage.SetComment(str);
                //myWriter.WriteLine(str);
                //myWriter.WriteLine(strReponse);
                if (CheckCmdReponse(strReponse))
                {
                    string retValue = strReponse.Substring(strReponse.IndexOf("$OK") + 4, 2);
                    byte[] byteValue = strToToHexByte(retValue);

                    BitArray arr = new BitArray(byteValue);

                    uint featureControl = 0;

                    if (arr[0])//Bit 0: Green Light mode
                        featureControl = featureControl | 1;
                    if (arr[1])//Bit 1: Basic Delay Curing mode
                        featureControl = featureControl | 2;

                    //20180125
#if true

#else
                if (arr[6])
                    featureControl = featureControl | 64;
                if (arr[7])
                    featureControl = featureControl | 128;
#endif
                    if (featFromFile.TrueLightMode == 1)//Bit 2: Advanced Delay Curing mode
                    {
                        if (supportDemoLic)
                        {
                            featureControl = featureControl | 4;
                        }
                        else
                        {
                            if (featureDictionary[(int)FeatureID.TrueLightMode] == PERMANENT_DATE)
                            {
                                featureControl = featureControl | 4;
                            }
                        }
                        //featureControl = featureControl | 4;
                    }

                    if (featFromFile.FluorescenceMode == 1)//Bit 3: Fluorescence mode
                    {
                        if (supportDemoLic)
                        {
                            featureControl = featureControl | 8;
                        }
                        else
                        {
                            if (featureDictionary[(int)FeatureID.FluorescenceMode] == PERMANENT_DATE)
                            {
                                featureControl = featureControl | 8;
                            }
                        }
                        //featureControl = featureControl | 8;
                    }

                    if (featFromFile.NoGlareMode == 1)//Bit 4: Cross Polarization mode
                    {
                        if (supportDemoLic)
                        {
                            featureControl = featureControl | 16;
                        }
                        else
                        {
                            if (featureDictionary[(int)FeatureID.NoGlareMode] == PERMANENT_DATE)
                            {
                                featureControl = featureControl | 16;
                            }
                        }
                        //featureControl = featureControl | 16;
                    }

                    if (featFromFile.LightBoost == 1)//Bit 5: Light Boost
                    {
                        if (supportDemoLic)
                        {
                            featureControl = featureControl | 32;
                        }
                        else
                        {
                            if (featureDictionary[(int)FeatureID.LightBoost] == PERMANENT_DATE)
                            {
                                featureControl = featureControl | 32;
                            }
                        }
                        //featureControl = featureControl | 32;
                    }

                    if (featFromFile.MultispectralMode == 1)//Bit 6: Multispectral mode
                    {
                        if (supportDemoLic)
                        {
                            featureControl = featureControl | 64;
                        }
                        else
                        {
                            if (featureDictionary[(int)FeatureID.MultispectralMode] == PERMANENT_DATE)
                            {
                                featureControl = featureControl | 64;
                            }
                        }
                        //featureControl = featureControl | 64;
                    }

#if false//20190327
                    //
                    if (featFromFile.VioletMode == 1)//Bit 7: Violet mode or DICOM Connectivity Reserved
                    {
                        featureControl = featureControl | 128;
                    }
#endif

                    str = "$" + BaseCmd.FeatSet + ":" + Convert.ToString(featureControl, 16).PadLeft(2, '0') + "$";

                    commandProgress = "Feature set ...";
                    ShowPrograss();
                    strReponse = mainPage.SetComment(str);
                    //myWriter.WriteLine(str);
                    //myWriter.WriteLine(strReponse);

                    //myWriter.Close();
                    Thread.Sleep(1000);
                    if (CheckCmdReponse(strReponse))
                    {
                        if (supportDemoLic == true)
                        {
                            foreach (KeyValuePair<int, string> featureElement in featureDictionary)
                            {
                                //Todo
                                string strExpiryDateCmd = GetExpiryDateCmd(featureElement);
                                if (strExpiryDateCmd.Equals(string.Empty))
                                {
                                    MessageBox.Show("Update failed, please try again.\r\n", "Warning!");
                                    return;
                                }
                                else
                                {
                                    serialPort1.Write(strExpiryDateCmd);
                                    
                                    string strResponse = serialPort1.ReadTo(MainMenu.ETX);
                                    index = strResponse.IndexOf("$");
                                    strResponse = strResponse.Substring(index);

                                    if (!strResponse.Substring(1, 2).Equals("OK"))
                                    {
                                        if (strResponse.Equals(MainMenu.NG_BADPAR))
                                        {
                                            commandProgress = "Set expiry date failed,The expiry date is out of range.";
                                            ShowPrograss();
                                        }
                                        else if (strResponse.Equals(MainMenu.NG_HWERR))
                                        {
                                            commandProgress = "Set expiry date failed,EEPROM operation failed.";
                                            ShowPrograss();
                                        }
                                        else if (strResponse.Equals(MainMenu.NG_BUSY))
                                        {
                                            commandProgress = "Set expiry date failed,EEPROM operation on going.";
                                            ShowPrograss();
                                        }
                                        MessageBox.Show("Set expiry date failed");
                                        return;
                                    }
                                }
                            }
                        }
#if false
                        bool isValidDate = true;
                        foreach (var featureElement in featureDictionary)
                        {
                            string strValidDate = featureElement.Value;
                            isValidDate = isValidDate && ((featureElement.Value == PERMANENT_DATE) ? true : false);
                        }

                        if (isValidDate == false)//unValid Date
                        {
                            if (supportDemoLic == false)//unsupport Demo
                            {
                                Invoke(new Action(() =>
                                {
                                    MessageBox.Show("Demo license is not supported on the current EXTARO 300 system,\r\n"
                                    + "please update the system's firmware to version 2.13.6 or higher.");
                                }));
                                //return;
                            }
                        }
#endif
                        commandProgress = "Update successful!";
                        ShowPrograss();
                        MessageBox.Show("Update successful!");
                    }
                    else
                    {
                        commandProgress = Utils.GetCommandResult(strReponse);
                        ShowPrograss();
                    }
                }
                else
                {
                    commandProgress = Utils.GetCommandResult(strReponse);
                    ShowPrograss();
                }
            }
            catch
            {
                MessageBox.Show("Update failed, please try again.\r\n", "Warning!");
            }
        }

        private string GetExpiryDateCmd(KeyValuePair<int, string> featureElement )
        {
            try
            {
                string cmd = MainMenu.STX + "$LIC:EXP:SET:";
                int featureID = featureElement.Key;
                string strFeatureID = featureElement.Key.ToString("X").PadLeft(2, '0');
                string expiryDate = featureElement.Value;

                if (expiryDate == PERMANENT_DATE)
                {
                    
                    string strChecksum = ((featureID + 0xFF + 0xFF + 0xFF) & 0xFF).ToString("X").PadLeft(2, '0');
                    cmd = cmd + strFeatureID + "FFFFFF:" + strChecksum + "$" + MainMenu.ETX;
                }
                else
                {
                    string[] dateArray = Regex.Split(expiryDate, "-", RegexOptions.IgnoreCase);

                    int year = Convert.ToInt32(dateArray[2]) - 2000;
                    int month = 0;
                    int date = Convert.ToInt32(dateArray[0]);
                    switch (dateArray[1])
                    {
                        case "jan":
                            month = 1;
                            break;
                        case "feb":
                            month = 2;
                            break;
                        case "mar":
                            month = 3;
                            break;
                        case "apr":
                            month = 4;
                            break;
                        case "may":
                            month = 5;
                            break;
                        case "jun":
                            month = 6;
                            break;
                        case "jul":
                            month = 7;
                            break;
                        case "aug":
                            month = 8;
                            break;
                        case "sep"://sept
                            month = 9;
                            break;
                        case "sept":
                            month = 9;
                            break;
                        case "oct":
                            month = 10;
                            break;
                        case "nov":
                            month = 11;
                            break;
                        case "dec":
                            month = 12;
                            break;
                        default:
                            break;

                    }

                    string strYear = year.ToString("X").PadLeft(2, '0');
                    string strMonth = month.ToString("X").PadLeft(2, '0');
                    string strDate = date.ToString("X").PadLeft(2, '0');
                    string strChecksum = (featureID + year + month + date).ToString("X").PadLeft(2, '0');

                    cmd = cmd + strFeatureID + strYear + strMonth + strDate + ":" + strChecksum + "$" + MainMenu.ETX;
                }
                return cmd;
            }
            catch
            {
                return string.Empty;
            }
        }

        public void ShowPrograss(string txtMsg = "")
        {
            try
            {
                if (string.IsNullOrEmpty(txtMsg))
                    this.tbPrograss.Invoke(new Action(() => this.tbPrograss.Text = commandProgress));
                else
                    this.tbPrograss.Invoke(new Action(() => this.tbPrograss.Text = txtMsg));
            }
            catch
            {
                ;
            }
        }
        public Thread m_thUploadData = null;
        public Thread m_thReadFromBoard = null;

        private bool threadUploadEndFlag = true;
        private bool threadReadEndFlag = true;

        public void StartThread()
        {
            m_thUploadData = new Thread(ThreadWriteToEeprom);
            m_thUploadData.Start();
        }

        public void ThreadWriteToEeprom()
        {
            try
            {
                threadUploadEndFlag = false;
                mainPage.IsCommandSending = true;
                WriteDataToEeprom(this.tbFile.Text);
                mainPage.IsCommandSending = false;
                threadUploadEndFlag = true;
            }
            catch { }
        }

        public void StartReadBoardThread()
        {
            m_thReadFromBoard = new Thread(TreadReadFromBoard);
            m_thReadFromBoard.Start();
        }

        public void TreadReadFromBoard()
        {
            threadReadEndFlag = false;
            mainPage.IsCommandSending = true;
            ReadFromBoard();
            mainPage.IsCommandSending = false;
            threadReadEndFlag = true;
        }
        public string GetSystemID()
        {
            try
            {
                string cmd;
                string response;
                if (!serialPort1.IsOpen)
                {
                    MessageBox.Show("Com port is not open!", "Warning!");
                    //return ""; 20200401
                    return "err"; //
                }
                string STX = Char.ConvertFromUtf32(2);
                string CMD_SSN_GET = "$SSN:GET$";
                string ETX = Char.ConvertFromUtf32(3);
                string OK_HDR = "$OK:";

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
                    ShowPrograss("Get Carrier Arm SN: " + response);
                    return response;
                }
                else if (response.Substring(1, 2).Equals("NG"))
                {
                    ShowPrograss("Get Carrier Arm SN: " + response);
                    return "err";
                }
                return "err";
            }
            catch
            {
                return "err";
            }
        }
        private void ReadServiceLicenseFile()
        {
            string fileName = serviceLicensePath;
            string realFileName = fileName;// "license_upload.bin";
                                           // var currentDirectory = System.Environment.CurrentDirectory;
            try
            {
                ShowPrograss("Read service license!");
                this.tbFile.Text = fileName;
                if (!fileName.Equals("") && File.Exists(fileName))
                {
                    //File.Copy(fileName, currentDirectory + @"\" + realFileName, true);
                    featFromFile = new Features();
                    if (ReadFromCard(realFileName, ref featFromFile))
                    {
                        canUpload = true;
                        RefreshLocalControls(featFromFile, true);
                    }
                    else
                    {
                        canUpload = false;
                        RefreshLocalControls(featFromFile, false);
                    }
                }
                btnUpdate.Enabled = canUpload;
            }
            catch
            {
                ShowPrograss("Read service license failed!");
            }
            finally
            {
                //if (File.Exists(currentDirectory + @"\" + realFileName))
                //{
                //    File.Delete(currentDirectory + @"\" + realFileName);
                //}
            }
        }
        public string serviceLicensePath { get; set; }
        private void btnServer_Click(object sender, EventArgs e)
        {
            try
            {
                //if (!serialPort1.IsOpen)
                //{
                //    MessageBox.Show("Com port is not open!", "Warning!");
                //    return;
                //}
                string systemId = GetSystemID();
                if (!systemId.Equals(""))
                {
                    ServiceLicense serviceLicenseForm = new ServiceLicense(this, systemId);
                    serviceLicenseForm.StartPosition = FormStartPosition.CenterParent;
                    if (serviceLicenseForm.ShowDialog() == DialogResult.OK)
                    {
                        ReadServiceLicenseFile();
                    }
                }
            }
            catch { }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (!threadUploadEndFlag)
                {
                    MessageBox.Show("Uploading license,please try again later!", "Warning!");
                    return;
                }
                if (!serialPort1.IsOpen)
                {
                    MessageBox.Show("Com port is not open!", "Warning!");
                    return;
                }
                DialogResult MsgBoxResult;
                MsgBoxResult = MessageBox.Show("You are about to delete the installed license and disable all features. proceed?", "Warning!", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
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


                        if (WriteDataHeadToEeprom())
                        {
                            string str = "$" + BaseCmd.FeatSet + ":00$";//“$SYS:CONF:SET”命令+参数0x00;
                            ShowPrograss("Set system config,to disable all the feature");
                            string strReponse = mainPage.SetComment(str);
                            ShowPrograss(Utils.GetCommandResult(strReponse));
                            if (Utils.GetCommandResult(strReponse).IndexOf("OK") > -1)
                            {
                                Features feat = new Features();
                                //DisplayTextBox(tbBoardClassroom, feat.ClassroomStreaming, false);
                                //20180125

                                DisplayGridViewValue(DgvSystemLicense, feat.FluorescenceMode, feat.FluorescenceModeValidDate,false, 0, 3);
                                DisplayGridViewValue(DgvSystemLicense, feat.NoGlareMode, feat.NoGlareModeValidDate, false, 1, 3);
                                DisplayGridViewValue(DgvSystemLicense, feat.LightBoost, feat.LightBoostValidDate, false, 2, 3);
                                DisplayGridViewValue(DgvSystemLicense, feat.TrueLightMode, feat.TrueLightModeValidDate, false, 3, 3);
                                DisplayGridViewValue(DgvSystemLicense, feat.MultispectralMode, feat.MultispectralModeValidDate, false, 4, 3);
                                //DisplayGridViewValue(DgvSystemLicense, feat.VioletMode, feat.NoGlareModeValidDate, false, 5, 3);
                                DisplayGridViewValue(DgvSystemLicense, feat.DICOMConnectivity, feat.DICOMConnectivityValidDate, false, 5, 3);

                                MessageBox.Show("License deleted!");
                            }
                        }
                    }
                }
            }
            catch { }
        }

        private bool WriteDataHeadToEeprom()
        {
#if false
            ShowPrograss("Read Head Start...");
            string strReadHead = mainPage.SetComment("$LIC:HEAD:RD$");
            strReadHead = strReadHead.Substring(strReadHead.IndexOf("$OK"));
            int restLen = 0;
            if (strReadHead.StartsWith("$OK"))
            {
                int lastMhIndex = strReadHead.LastIndexOf(":");
                string aaCode = strReadHead.Substring(lastMhIndex - 2, 2);
                //if (!aaCode.ToUpper().Equals("AA"))
                //{
                //    MessageBox.Show("License is invalid!");
                //    return false;
                //}
                string lowOrder = strReadHead.Substring(36, 2);
                string highOrder = strReadHead.Substring(38, 2);

                byte[] byteData = StrToHexByte(highOrder + lowOrder);

                restLen = (int)(byteData[1] | byteData[0] << 8);
                //if (restLen > 1024 * 10)
                //{
                //    MessageBox.Show("License file size invalid!");
                //    return false;
                //}
            }

            byte cc = 0;
            long len = restLen;
            byte[] buff = new byte[len];
            byte[] md5 = new byte[16];
            MD5_Calculate(md5, buff, (int)buff.Length);

            string str = "$LIC:HEAD:WR:";
            string temp = "";
            for (byte i = 0; i < 16; i++)
            {
                temp += Convert.ToString(md5[i], 16).PadLeft(2, '0');
                cc += md5[i];
            }

            temp += Convert.ToString(len & 0x000000FF, 16).PadLeft(2, '0');
            cc += (byte)(len & 0x00FF);
            temp += Convert.ToString(len >> 8, 16).PadLeft(2, '0');
            cc += (byte)(len >> 8);
            temp += ":";
            temp += Convert.ToString(cc, 16).PadLeft(2, '0');
            str += temp += "$";
#else
            string STX = Char.ConvertFromUtf32(2);
            string ETX = Char.ConvertFromUtf32(3);
            string str = "$LIC:HEAD:WR:000000000000000000000000000000000000:00$";

#endif
            commandProgress = "Write Head Start...";
            ShowPrograss();
            string strReponse = mainPage.SetComment(str);
            commandProgress = "Write Head End...";
            ShowPrograss();
            if (Utils.GetCommandResult(strReponse).IndexOf("OK") > -1)
            {
                ShowPrograss(Utils.GetCommandResult("Write head:" + strReponse));
                return true;
            }
            else
            {
                ShowPrograss(Utils.GetCommandResult("Write head:" + strReponse));
                return false;
            }
        }

        private void License_Load(object sender, EventArgs e)
        {
            DataGridViewInit(DgvLocalLicense);
            DgvLocalLicense.Enabled = false;
            DataGridViewInit(DgvSystemLicense);
            DgvSystemLicense.Enabled = false;
        }

        private void DataGridViewInit(DataGridView dataGridView)
        {
            dataGridView.AllowUserToResizeColumns = false;
            dataGridView.AllowUserToResizeRows = false;

            dataGridView.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridView.RowTemplate.Height = 27;
            for (int i = 0; i < 6; i++)
            {
                dataGridView.Rows.Add();
            }
            dataGridView.Rows[0].Cells[0].Value = "Hardware";
            dataGridView.Rows[1].Cells[0].Value = "Hardware";
            dataGridView.Rows[2].Cells[0].Value = "Hardware";
            dataGridView.Rows[3].Cells[0].Value = "Hardware";
            dataGridView.Rows[4].Cells[0].Value = "Hardware";
            //dataGridView.Rows[5].Cells[0].Value = "Hardware";
            dataGridView.Rows[5].Cells[0].Value = "Software";

            dataGridView.Rows[0].Cells[1].Value = "Fluorescence Mode";
            dataGridView.Rows[1].Cells[1].Value = "NoGlare Mode";
            dataGridView.Rows[2].Cells[1].Value = "Light Boost";
            dataGridView.Rows[3].Cells[1].Value = "TrueLight Mode";
            dataGridView.Rows[4].Cells[1].Value = "Multispectral Mode";
            //dataGridView.Rows[5].Cells[1].Value = "Violet Mode";
            dataGridView.Rows[5].Cells[1].Value = "DICOM Connectivity";
        }

        private void DgvLocalLicense_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            try
            {
                //Vertical merger
                if (this.DgvLocalLicense.Columns["Category"].Index == e.ColumnIndex && e.RowIndex >= 0)
                {
                    //Initialize two brushes
                    using (
                              Brush gridBrush = new SolidBrush(Color.Gray),//Line
                              backColorBrush = new SolidBrush(e.CellStyle.BackColor)//BackGround
                          )
                    {
                        using (Pen gridLinePen = new Pen(gridBrush))
                        {
                            // Erase the original cell background
                            e.Graphics.FillRectangle(backColorBrush, e.CellBounds);
                            if (e.RowIndex != this.DgvLocalLicense.RowCount - 1)
                            {
                                if (e.Value.ToString() != Convert.ToString(this.DgvLocalLicense.Rows[e.RowIndex + 1].Cells[e.ColumnIndex].Value))
                                {
                                    e.Graphics.DrawLine(gridLinePen, e.CellBounds.Left, e.CellBounds.Bottom - 1,
                                    e.CellBounds.Right - 1, e.CellBounds.Bottom - 1);//Draw the line of the edge
                                    //Write value
                                    if (e.Value != null)
                                    {
                                        int heighty = e.CellBounds.Height / 2 * rowCount;
                                        e.Graphics.DrawString(
                                            (String)e.Value,
                                            e.CellStyle.Font, Brushes.Black,
                                            e.CellBounds.X + 2,
                                            e.CellBounds.Y + 2 - heighty,
                                            StringFormat.GenericDefault);
                                        rowCount = 0;
                                    }
                                }
                                else
                                {
                                    rowCount++;
                                }
                            }
                            else
                            {
                                e.Graphics.DrawLine(gridLinePen, e.CellBounds.Left, e.CellBounds.Bottom - 1,
                                    e.CellBounds.Right - 1, e.CellBounds.Bottom - 1);//Draw the line of the edge
                                //Write value
                                if (e.Value != null)
                                {
                                    e.Graphics.DrawString((String)e.Value, e.CellStyle.Font,
                                        Brushes.Black, e.CellBounds.X + 2,
                                        e.CellBounds.Y + 2, StringFormat.GenericDefault);
                                }
                            }
                            //Draw the line on the right
                            e.Graphics.DrawLine(
                                gridLinePen,
                                e.CellBounds.Right - 1,
                                e.CellBounds.Top,
                                e.CellBounds.Right - 1,
                                e.CellBounds.Bottom - 1);
                            e.Handled = true;
                        }
                    }
                }
            }
            catch 
            {
                ;
            }
        }


    }
}
