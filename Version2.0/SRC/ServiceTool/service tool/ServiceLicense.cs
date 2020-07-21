using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace service_tool
{
    public partial class ServiceLicense : Form
    {
        public License mainPage;
        public string activationPath = "";
        public string localActivationFile = "";
        public bool isContected = true;
        public ServiceLicense()
        {
            InitializeComponent();
        }
        public ServiceLicense(License pageLicense,string systemId)
        {
            InitializeComponent();
            mainPage = pageLicense;
            if (systemId.Equals("err"))
            {
                this.tbDeviceID.Enabled = true;
                isContected = false;
            }
            else
            {
                this.tbDeviceID.Text = systemId;
                isContected = true;
            }
            this.cbServer.SelectedIndex = 0;
            activationPath = System.AppDomain.CurrentDomain.BaseDirectory + @"activation\";
            localActivationFile = activationPath + "ActivationID.xml";
            Dictionary<string, string> DicActivationIDs = new Dictionary<string, string>();
            ReadActivationIDXML();
        }
        private void cbServer_DrawItem(object sender, DrawItemEventArgs e)
        {
            string s = this.cbServer.Items[e.Index].ToString();
            // 
            SizeF ss = e.Graphics.MeasureString(s, e.Font);
            // 
            float left = (float)(e.Bounds.Width - ss.Width) / 2;
            if (left < 0) left = 0f;
            float top = (float)(e.Bounds.Height - ss.Height) / 2;
            // 
            if (top < 0) top = 0f;
            top = top + this.cbServer.ItemHeight * e.Index;
            //
            e.DrawBackground();
            e.DrawFocusRectangle();
            e.Graphics.DrawString(
                s,
                e.Font,
                new SolidBrush(e.ForeColor),
                left, top);
        }
        private void LogInfo(string strMsg)
        {
            this.tbLogs.Text = strMsg;
            this.tbLogs.Refresh();
            //this.tbLogs.Invoke(new Action(() => this.tbLogs.Text = strMsg));
        }
        private void EnableButtons(bool isEnable)
        {
            this.btnAddActivate.Enabled = isEnable;
            this.btnUpload.Enabled = isEnable;
            this.btnRemove.Enabled = isEnable;
        }

        private bool WriteActivationIDXML(List<string> listActivationIds)
        {
            bool isSuccess = false;
            try
            {
                XmlDocument doc = new XmlDocument();

                XmlNode nodeDeclare = doc.CreateXmlDeclaration("1.0", "utf-8", "");
                doc.AppendChild(nodeDeclare);  


                XmlElement root = doc.CreateElement("ActivationIDs");
                doc.AppendChild(root);

                for (int i = 0; i < listActivationIds.Count; i++)
                {
                    XmlNode node = doc.CreateNode(XmlNodeType.Element, "ActivationID" + (i + 1).ToString(), null);
                    node.InnerText = listActivationIds[i];  
                    root.AppendChild(node);
                }
                
                if (!Directory.Exists(activationPath))
                {
                    Directory.CreateDirectory(activationPath);
                }
                if (File.Exists(localActivationFile))
                    File.Delete(localActivationFile);
                doc.Save(localActivationFile);
                isSuccess = true;
            }
            catch
            {
                //MessageBox.Show(ex.ToString());
            }
            return isSuccess;
        }
        private void ReadActivationIDXML()
        {
            Dictionary<string, string> DicActivationID = new Dictionary<string, string>();
            try
            {
                if (File.Exists(localActivationFile))
                {
                    XmlDocument doc = new XmlDocument();
                    doc.Load(localActivationFile);

                    XmlNode xn = doc.SelectSingleNode("ActivationIDs");
                    XmlNodeList xnl = xn.ChildNodes;
                    foreach (XmlNode xn1 in xnl)
                    {
                        XmlElement xe = (XmlElement)xn1;
                        DicActivationID.Add(xe.Name, xe.InnerText);
                    }
                }
                if (DicActivationID.Count > 0)
                {
                    foreach (var d in DicActivationID)
                    {
                        switch (d.Key)
                        {
                            case "ActivationID1":
                                SetActivationIDText(tbActivationID1, d.Value);
                                break;
                            case "ActivationID2":
                                SetActivationIDText(tbActivationID2, d.Value);
                                break;
                            case "ActivationID3":
                                SetActivationIDText(tbActivationID3, d.Value);
                                break;
                            case "ActivationID4":
                                SetActivationIDText(tbActivationID4, d.Value);
                                break;
                            case "ActivationID5":
                                SetActivationIDText(tbActivationID5, d.Value);
                                break;
                            case "ActivationID6":
                                SetActivationIDText(tbActivationID6, d.Value);
                                break;
                            case "ActivationID7":
                                SetActivationIDText(tbActivationID7, d.Value);
                                break;
                            case "ActivationID8":
                                SetActivationIDText(tbActivationID8, d.Value);
                                break;
                            case "ActivationID9":
                                SetActivationIDText(tbActivationID9, d.Value);
                                break;
                            case "ActivationID10":
                                SetActivationIDText(tbActivationID10, d.Value);
                                break;
                        }
                    }
                    this.btnRemove.Enabled = true;
                    activateIdIndex = DicActivationID.Count - 1;
                }

            }
            catch 
            {
                MessageBox.Show("Read Activation ID xml file error!");
            }
        }

        private void SetActivationIDText(TextBox tb,string txt)
        {
            tb.Text = txt;
            tb.Visible = true;
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

        private void btnUpload_Click(object sender, EventArgs e)
        {
            try
            {
                if (!isContected)
                {
                    DialogResult MsgBoxResult;
                    MsgBoxResult = MessageBox.Show("Invalid license file may be downloaded if incorrect Carrier Arm SN is provided. Please double check the Carrier Arm SN!\nProceed?", "Warning!", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                    if (MsgBoxResult != DialogResult.Yes)
                    {
                        return;
                    }
                }
                //Production, Test, Development
                LogInfo("Downloading license from server!");
                EnableButtons(false);

                string selectedServer = this.cbServer.Text.ToString();
                string strToken = "Zs9mdJHKOviJSpK84ufsopB5xROfM8WncLm3Zejh6ulBymfFILwT";
                string deviceId = this.tbDeviceID.Text.Trim();//1234567890
                if (deviceId.Equals(""))
                {
                    EnableButtons(true);
                    MessageBox.Show("Carrier Arm SN can not be empty!");
                    return;
                }
                string activationId = this.tbActivationID1.Text.Trim();
                //Pa0C4RwyaehQ            No valid activation Ids in the request
                //vqILUSEjPUQh            
                //3FQxWaB2NGiy            
                //MCh1HCOT1Awp            
                //2x4M5uZiwl7f            No valid activation Ids in the request
                //u98qlZdfV39e            
                List<string> listActivationIds = new List<string>();

#if true
                for (var j = 0; j <= activateIdIndex; j++)
#else
            for (var j = 0; j <= 5; j++)
#endif
                {
                    string thisActivation = "";
                    switch (j)
                    {
                        case 0:
                            thisActivation = this.tbActivationID1.Text.Trim();
                            break;
                        case 1:
                            thisActivation = this.tbActivationID2.Text.Trim();
                            break;
                        case 2:
                            thisActivation = this.tbActivationID3.Text.Trim();
                            break;
                        case 3:
                            thisActivation = this.tbActivationID4.Text.Trim();
                            break;
                        case 4:
                            thisActivation = this.tbActivationID5.Text.Trim();
                            break;
                        case 5:
                            thisActivation = this.tbActivationID6.Text.Trim();
                            break;
                        case 6:
                            thisActivation = this.tbActivationID7.Text.Trim();
                            break;
                        case 7:
                            thisActivation = this.tbActivationID8.Text.Trim();
                            break;
                        case 8:
                            thisActivation = this.tbActivationID9.Text.Trim();
                            break;
                        case 9:
                            thisActivation = this.tbActivationID10.Text.Trim();
                            break;

                    }
                    if (!thisActivation.Equals(""))
                    {
                        try
                        {

                            byte[] byteActivationID = System.Text.Encoding.UTF8.GetBytes(thisActivation);
                            if (byteActivationID.Length > 16)
                            {
                                EnableButtons(true);
                                MessageBox.Show("Activation id length is less than 16 byte!");
                                return;
                            }
                            for (var m = 0; m < byteActivationID.Length; m++)
                            {
                                if (byteActivationID[m] > 127)
                                {
                                    EnableButtons(true);
                                    MessageBox.Show("Please input ASCII!");
                                    return;
                                }
                            }

                        }
                        catch
                        {
                            EnableButtons(true);
                            MessageBox.Show("Please input ASCII!");
                            return;
                        }
                        if (listActivationIds.Contains(thisActivation))
                        {
                            EnableButtons(true);
                            MessageBox.Show("Duplicated Activation IDs detected. Please verify input!");
                            return;
                        }

                        listActivationIds.Add(thisActivation);
                    }
                    else
                    {
                        EnableButtons(true);
                        MessageBox.Show("Activation id can not be empty!");
                        return;
                    }
                }
                if (!WriteActivationIDXML(listActivationIds))
                {
                    //EnableButtons(true);
                    //MessageBox.Show("test!");
                    //return;
                }
#if false
            if (listActivationIds.Count == 0)
            {
                EnableButtons(true);
                MessageBox.Show("Must have a ActivationID!");
                return;
            }
#else
                //if (listActivationIds.Count < 6)
                //{
                //    EnableButtons(true);
                //    MessageBox.Show("Must have 6 ActivationIDs!");
                //    return;
                //}
#endif
                bool responseSuccess = false;
                string responseLicense = string.Empty;
                string responseErrCode = string.Empty;
                string responseErrMessage = string.Empty;

                try
                {
                    if (selectedServer.Equals("Development"))
                    {

                        ServiceDevelopment.ActivationUtilityServiceSoapClient curService = new ServiceDevelopment.ActivationUtilityServiceSoapClient("ActivationUtilityServiceSoap");

                        ServiceDevelopment.ActivateEmbeddedLicenseRequest licenseRequest = new ServiceDevelopment.ActivateEmbeddedLicenseRequest();
                        licenseRequest.DeviceID = deviceId;
                        ServiceDevelopment.ActivateEmbeddedLicenseResponse licenseResponse = new ServiceDevelopment.ActivateEmbeddedLicenseResponse();
#if false
                    var requestCount = 0;
                    //if faild ,use next activateId
                    while (!responseSuccess && requestCount < listActivationIds.Count)
                    {
                        //MessageBox.Show((requestCount+1).ToString());
                        licenseRequest.ActivationID = listActivationIds[requestCount].ToString();
                        licenseResponse = curService.ActivateEmbeddedLicense(strToken, licenseRequest);
                        responseSuccess = licenseResponse.Success;
                        if (responseSuccess)
                            break;
                        else
                            requestCount++;
                    }
#else
                        var requestCount = 0;
                        while (requestCount < listActivationIds.Count)
                        {
#if false
                        MessageBox.Show(listActivationIds[requestCount]);
                        requestCount++;
                        continue;
#endif
                            licenseRequest.ActivationID = listActivationIds[requestCount].ToString();
                            licenseResponse = curService.ActivateEmbeddedLicense(strToken, licenseRequest);
                            responseSuccess = licenseResponse.Success;
                            responseErrCode = licenseResponse.ErrorCode;
                            responseErrMessage = licenseResponse.ErrorMessage;
                            if (!responseSuccess)
                            {
#if false
                            EnableButtons(true);
                            LogInfo("Download license error!");
                            MessageBox.Show("Download license error!" + responseErrMessage + ",and activation id is " + licenseRequest.ActivationID + ".");
#else
                                ShowDownloadError(responseErrCode, responseErrMessage, licenseRequest.ActivationID);
#endif
                                return;
                            }
                            requestCount++;
                        }
#endif
                        responseLicense = licenseResponse.License;


                    }
                    else if (selectedServer.Equals("Production"))
                    {
                        ServiceProduction.ActivationUtilityServiceSoapClient curService = new ServiceProduction.ActivationUtilityServiceSoapClient("ActivationUtilityServiceSoap2");

                        ServiceProduction.ActivateEmbeddedLicenseRequest licenseRequest = new ServiceProduction.ActivateEmbeddedLicenseRequest();
                        licenseRequest.DeviceID = deviceId;


                        ServiceProduction.ActivateEmbeddedLicenseResponse licenseResponse = new ServiceProduction.ActivateEmbeddedLicenseResponse();
                        var requestCount = 0;
                        while (requestCount < listActivationIds.Count)
                        {
                            licenseRequest.ActivationID = listActivationIds[requestCount].ToString();
                            licenseResponse = curService.ActivateEmbeddedLicense(strToken, licenseRequest);
                            responseSuccess = licenseResponse.Success;
                            responseErrCode = licenseResponse.ErrorCode;
                            responseErrMessage = licenseResponse.ErrorMessage;
                            if (!responseSuccess)
                            {
                                ShowDownloadError(responseErrCode, responseErrMessage, licenseRequest.ActivationID);
                                return;
                            }
                            requestCount++;
                        }

                        responseLicense = licenseResponse.License;

                    }
                    else if (selectedServer.Equals("Test"))
                    {
                        LicenseServiceTest.ActivationUtilityServiceSoapClient curService = new LicenseServiceTest.ActivationUtilityServiceSoapClient("ActivationUtilityServiceSoap1");

                        LicenseServiceTest.ActivateEmbeddedLicenseRequest licenseRequest = new LicenseServiceTest.ActivateEmbeddedLicenseRequest();



                        licenseRequest.DeviceID = deviceId;
                        LicenseServiceTest.ActivateEmbeddedLicenseResponse licenseResponse = new LicenseServiceTest.ActivateEmbeddedLicenseResponse();
                        var requestCount = 0;
                        while (requestCount < listActivationIds.Count)
                        {
                            licenseRequest.ActivationID = listActivationIds[requestCount].ToString();
                            licenseResponse = curService.ActivateEmbeddedLicense(strToken, licenseRequest);
                            responseSuccess = licenseResponse.Success;
                            responseErrCode = licenseResponse.ErrorCode;
                            responseErrMessage = licenseResponse.ErrorMessage;
                            if (!responseSuccess)
                            {
                                ShowDownloadError(responseErrCode, responseErrMessage, licenseRequest.ActivationID);
                                return;
                            }
                            requestCount++;
                        }

                        responseLicense = licenseResponse.License;

                    }
                }
                catch
                {
                    EnableButtons(true);
                    LogInfo("Connect with server failed!");
#if true
                    MessageBox.Show("Can not connect with " + selectedServer + " server!");
#else
                MessageBox.Show(ex.ToString());
#endif
                    return;
                }

                if (responseSuccess)
                {
                    saveFileDialog1.FileName = "license_service.bin";
                    DialogResult dr = saveFileDialog1.ShowDialog();
                    if (dr == DialogResult.OK && !string.IsNullOrEmpty(responseLicense))
                    {
                        try
                        {
                            LogInfo("Saving license file!");
#if true
                            byte[] byteArray = Convert.FromBase64String(responseLicense);
#else
                        byte[] byteArray = Convert.FromBase64String(responseLicense);
                        string cont=Convert.ToBase64String(byteArray);
                        MessageBox.Show(cont.Equals(responseLicense).ToString());
#endif


                            string localFilePath = saveFileDialog1.FileName.ToString();

                            mainPage.serviceLicensePath = localFilePath;
                            if (File.Exists(localFilePath))
                            {
                                File.Delete(localFilePath);
                            }
                            using (FileStream fs = new FileStream(localFilePath, FileMode.OpenOrCreate))
                            {
                                fs.Write(byteArray, 0, byteArray.Length);
                            }
                            this.DialogResult = DialogResult.OK;
                            this.Close();
                        }
                        catch
                        {
                            EnableButtons(true);
                            MessageBox.Show("Save license file error!");
                            return;
                        }
                    }
                    EnableButtons(true);
                }
                else
                {
                    EnableButtons(true);
                    LogInfo("Download license file failed!");
                    MessageBox.Show("Download license file failed!");
                }
            }
            catch { }
        }

        private void ShowDownloadError(string responseErrCode, string responseErrMessage, string theActivationID)
        {
            EnableButtons(true);
            LogInfo("Download license error!");
            string strMsg = "Download license error! Server response:\n" + responseErrMessage;

            if (responseErrCode.Equals("CZ-CIT-EHA-0001"))
            {
                strMsg += "\nActivation ID: " + theActivationID;
            }
            MessageBox.Show(strMsg);
        }

        private int activateIdIndex = 0;
        private void btnAddActivate_Click(object sender, EventArgs e)
        {
            try
            {
                if (activateIdIndex >= 9)
                {
                    //MessageBox.Show("Support only 10!");
                    this.btnAddActivate.Enabled = false;
                    return;
                }
                this.btnRemove.Enabled = true;
                activateIdIndex++;
                switch (activateIdIndex)
                {
                    case 1:
                        this.tbActivationID2.Visible = true;
                        break;
                    case 2:
                        this.tbActivationID3.Visible = true;
                        break;
                    case 3:
                        this.tbActivationID4.Visible = true;
                        break;
                    case 4:
                        this.tbActivationID5.Visible = true;
                        break;
                    case 5:
                        this.tbActivationID6.Visible = true;
                        break;
                    case 6:
                        this.tbActivationID7.Visible = true;
                        break;
                    case 7:
                        this.tbActivationID8.Visible = true;
                        break;
                    case 8:
                        this.tbActivationID9.Visible = true;
                        break;
                    case 9:
                        this.tbActivationID10.Visible = true;
                        break;
                    default:
                        break;
                }
            }
            catch { }
        }
        public static string DecodeBase64(Encoding encode, string result)
        {
            string decode = "";
            byte[] bytes = Convert.FromBase64String(result);
            try
            {
                decode = encode.GetString(bytes);
            }
            catch
            {
                decode = result;
            }
            return decode;
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            try
            {
                if (activateIdIndex < 1)
                {
                    //MessageBox.Show("Keep at least 1!");
                    this.btnRemove.Enabled = false;
                    return;
                }
                this.btnAddActivate.Enabled = true;
                switch (activateIdIndex)
                {
                    case 1:
                        this.tbActivationID2.Visible = false;
                        this.tbActivationID2.Text = "";
                        break;
                    case 2:
                        this.tbActivationID3.Visible = false;
                        this.tbActivationID3.Text = "";
                        break;
                    case 3:
                        this.tbActivationID4.Visible = false;
                        this.tbActivationID4.Text = "";
                        break;
                    case 4:
                        this.tbActivationID5.Visible = false;
                        this.tbActivationID5.Text = "";
                        break;
                    case 5:
                        this.tbActivationID6.Visible = false;
                        this.tbActivationID6.Text = "";
                        break;
                    case 6:
                        this.tbActivationID7.Visible = false;
                        this.tbActivationID7.Text = "";
                        break;
                    case 7:
                        this.tbActivationID8.Visible = false;
                        this.tbActivationID8.Text = "";
                        break;
                    case 8:
                        this.tbActivationID9.Visible = false;
                        this.tbActivationID9.Text = "";
                        break;
                    case 9:
                        this.tbActivationID10.Visible = false;
                        this.tbActivationID10.Text = "";
                        break;
                    default:
                        break;
                }
                activateIdIndex--;
            }
            catch { }
        } 
    }
}
