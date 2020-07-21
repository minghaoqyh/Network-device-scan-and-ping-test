using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace service_tool
{
    public partial class Parameters : UserControl
    {
        public MainMenu mainPage;
        private System.IO.Ports.SerialPort serialPort1;
        public Dictionary<string, string> DicParameters = new Dictionary<string, string>();
        public Parameters()
        {
            InitializeComponent();

            DicParameters = new Dictionary<string, string>();
            DicParameters.Add("00", "Orange mode-Basic Configuration");
            DicParameters.Add("01", "TrueLight mode-Basic Configuration");
            DicParameters.Add("02", "Orange mode-LightBoost Configuration");
            DicParameters.Add("03", "TrueLight mode-LightBoost Configuration");
        }
        public void InitializeParameterComponent(MainMenu mainMenu, System.IO.Ports.SerialPort serialPort)
        {
            mainPage = mainMenu;
            serialPort1 = serialPort;
        }

        private void btnGet_Click(object sender, EventArgs e)
        {
            try
            {
                if (!serialPort1.IsOpen)
                {
                    MessageBox.Show("Com port is not open!", "Warning!");
                    return;
                }
                GetParameter("00", this.tbOB);
                GetParameter("01", this.tbTB);
                GetParameter("02", this.tbOL);
                GetParameter("03", this.tbTL);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Get delay curing max intensity erroe:" + ex.Message);
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            try
            {
                if (!serialPort1.IsOpen)
                {
                    MessageBox.Show("Com port is not open!", "Warning!");
                    return;
                }
                DialogResult MsgBoxResult;
                MsgBoxResult = MessageBox.Show("You are about to reset the parameters to factory default values.\nproceed?", "Warning!", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                if (MsgBoxResult == DialogResult.Yes)
                {
                    ResetParameter("00", this.tbOB, this.tbOB2);
                    ResetParameter("01", this.tbTB, this.tbTB2);
                    ResetParameter("02", this.tbOL, this.tbOL2);
                    ResetParameter("03", this.tbTL, this.tbTL2);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Reset delay curing max intensity erroe:" + ex.Message);
            }
           
        }
        private void ResetParameter(string paraIndex,TextBox tb,TextBox tb2)
        {
            string paraName = GetParameterNameByIndex(paraIndex);
            ShowPrograss("Reset " + paraName + " start.");
            string strCmd = "$PARA:RST:" + paraIndex + "$";
            string strReponse = mainPage.SetComment(strCmd);

            System.Threading.Thread.Sleep(100);
            try
            {
                if (strReponse.IndexOf("$OK") > -1)
                {
                    GetParameter(paraIndex, tb);
                    tb2.Text = "";
                }
            }
            catch 
            {
            }

            ShowPrograss("Reset " + paraName + ": " + Utils.GetCommandResult(strReponse));
        }
        private void btnSetOB_Click(object sender, EventArgs e)
        {
            SetParameter("00", this.tbOB2.Text);
        }

        private void btnSetTB_Click(object sender, EventArgs e)
        {
            SetParameter("01", this.tbTB2.Text);
        }

        private void btnSetOL_Click(object sender, EventArgs e)
        {
            SetParameter("02", this.tbOL2.Text);
        }

        private void btnSetTL_Click(object sender, EventArgs e)
        {
            SetParameter("03", this.tbTL2.Text);
        }

        private void SetParameter(string strIndex,string strVal)
        {
            if (!serialPort1.IsOpen)
            {
                MessageBox.Show("Com port is not open!", "Warning!");
                return;
            }
            string paraName = GetParameterNameByIndex(strIndex);
            ShowPrograss("Set " + paraName + " start.");
            try
            {
                strVal = strVal.Trim();
                if (string.IsNullOrEmpty(strVal))
                {
                    MessageBox.Show("Please input value!");
                    return;
                }
                int intValue = 0;
                try
                {
                    intValue = Convert.ToInt32(strVal);
                }
                catch
                {
                    intValue = 0;
                }
                if (intValue < 1 || intValue > 99)
                {
                    MessageBox.Show("Please input value 1-99!");
                    return;
                }
                //byte[] inpVal = Encoding.UTF8.GetBytes(strVal);

                //if (inpVal.Length != 1)
                //{
                //    MessageBox.Show("Please input one ASCII!");
                //    return;
                //}
                //for (var m = 0; m < inpVal.Length; m++)
                //{
                //    if (inpVal[m] > 99)
                //    {
                //        MessageBox.Show("Please input ASCII!");
                //        return;
                //    }
                //}





                string str = "$PARA:SET:" + strIndex + ":" + intValue.ToString("X2") + "$";
                string strReponse = mainPage.SetComment(str);
                ShowPrograss("Set " + paraName + " :" + Utils.GetCommandResult(strReponse));
            }
            catch (Exception ex)
            {
                ShowPrograss("Set " + paraName + " failed!" + ex.ToString());
            }
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

        private void GetParameter(string paraIndex,TextBox tb)
        {

            string paraName = GetParameterNameByIndex(paraIndex);
            ShowPrograss("Get "+paraName+" start.");
            string strCmd = "$PARA:GET:" + paraIndex + "$";
            string strReponse = mainPage.SetComment(strCmd);

            try
            {
                if (strReponse.IndexOf("$OK") > -1)
                {
                    string retValue = strReponse.Substring(strReponse.IndexOf("$OK") + 4, 2);
                    byte[] byteValue = strToToHexByte(retValue);
                    var getVal = Convert.ToString(byteValue[0]);
                    tb.Text = getVal;// Encoding.ASCII.GetString(byteValue);
                }
                else
                    tb.Text = " -- ";
            }
            catch (Exception ex)
            {
                tb.Text = " -- ";
                MessageBox.Show("Get " + paraName + " fail," + ex.ToString());
                return;
            }

            ShowPrograss("Get " + paraName + ":" + Utils.GetCommandResult(strReponse));

        }
        private string GetParameterNameByIndex(string key)
        {
            if (DicParameters.ContainsKey(key))
                return DicParameters[key];
            else
                return "";
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
        private void ClearSetTextbox()
        {
            this.tbOB2.Text = "";
            this.tbTB2.Text = "";
            this.tbOL2.Text = "";
            this.tbTL2.Text = "";            
        }
        private void cbEnable_Click(object sender, EventArgs e)
        {
            bool isChecked = this.cbEnable.Checked;
            if (!serialPort1.IsOpen)
            {
                cbEnable.Checked = !isChecked;
                MessageBox.Show("Com port is not open!", "Warning!");
                return;
            }
            if (!isChecked)
            {
                CheckSet(false);
                ClearSetTextbox();
                return;
            }
            DialogResult MsgBoxResult;
            MsgBoxResult = MessageBox.Show("These parameters should be adjusted only when the max intensity value @ 300 WD in any of the Delay Curing modes gets over 15Klux or below 13.5Klux.\nproceed?", "Warning!", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
            if (MsgBoxResult == DialogResult.Yes)
            {
                Password passwordForm = new Password();
                passwordForm.StartPosition = FormStartPosition.CenterParent;
                passwordForm.ShowDialog();
                if (passwordForm.DialogResult == DialogResult.OK)
                {
                    if (!passwordForm.strPassword.Equals(BaseCmd.SysPassword))
                    {
                        cbEnable.Checked = !isChecked;
                        MessageBox.Show("The password is not correct!", "Warning!");
                        return;
                    }
                    else
                    {

                        if (cbEnable.Checked == true)
                        {
                            CheckSet(true);
                        }
                        else
                        {
                            CheckSet(false);
                        }
                    }
                }
                else
                {
                    passwordForm.Close();
                    cbEnable.Checked = !isChecked;
                    return;
                }
            }
            else
            {
                cbEnable.Checked = !isChecked;
            }
        }

        private void CheckSet(bool isChecked)
        {
            this.tbOB2.Enabled = isChecked;
            this.tbOL2.Enabled = isChecked;
            this.tbTB2.Enabled = isChecked;
            this.tbTL2.Enabled = isChecked;

            this.btnSetOB.Enabled = isChecked;
            this.btnSetOL.Enabled = isChecked;
            this.btnSetTB.Enabled = isChecked;
            this.btnSetTL.Enabled = isChecked;

            this.btnReset.Enabled = isChecked;

        }

        public void ShowPrograss(string txtMsg = "")
        {
            this.tbPrograss.Text = txtMsg;
            this.tbPrograss.Refresh();
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }
    }
}
