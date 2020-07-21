using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Collections;

namespace service_tool
{
    public partial class AdvancedForm : Form
    {
        public MainMenu mainPage;
        public AdvancedForm(MainMenu mainMenu)
        {
            InitializeComponent();
            mainPage = mainMenu;
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

        private void btnGet_Click(object sender, EventArgs e)
        {
            var str = "$" + BaseCmd.FeatGet + "$";//"$SCONF:GET$";
            var strReponse = SendCommand(str);

            string retValue = strReponse.Substring(strReponse.IndexOf("$OK") + 4, 2);
            byte[] byteValue = strToToHexByte(retValue);

            BitArray arr = new BitArray(byteValue);



            Features feat = new Features();
            feat.FluorescenceMode = Convert.ToInt32(arr[3]);
            feat.NoGlareMode = Convert.ToInt32(arr[4]);
            feat.LightBoost = Convert.ToInt32(arr[5]);
            feat.TrueLightMode = Convert.ToInt32(arr[2]);
                        
            RefreshFeatureControls(feat);

        }

        public string SendCommand(string cmdName)
        {
            string strReponse = "";
            //StreamWriter myWriter = new StreamWriter("result.txt");
            strReponse = mainPage.SetComment(cmdName);
            //myWriter.WriteLine(cmdName);
            //myWriter.WriteLine(strReponse);
            //myWriter.Close();

            return strReponse;
        }

        private void RefreshFeatureControls(Features feat)
        {
            DisplayCheckBox(cbCaries, feat.FluorescenceMode);
            DisplayCheckBox(cbCross, feat.NoGlareMode);
            DisplayCheckBox(cbLight, feat.LightBoost);
            DisplayCheckBox(cbAdvancedDelay, feat.TrueLightMode);
            //DisplayCheckBox(cbAdvancedImage, feat.ClassroomStreaming);
            DisplayCheckBox(cbDicom, feat.DICOMConnectivity);
        }

        private void DisplayCheckBox(CheckBox chbox, int checkValue)
        {
            if (checkValue == 1)
                chbox.Checked = true;
            else
                chbox.Checked = false;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            uint featureControl = 0;

            if (this.cbCaries.Checked)
            {
                featureControl = featureControl | 8;
            }

            if (this.cbCross.Checked)
            {
                featureControl = featureControl | 16;
            }

            if (this.cbLight.Checked)
            {
                featureControl = featureControl | 32;
            }

            if (this.cbAdvancedDelay.Checked)
            {
                featureControl = featureControl | 4;
            }
            featureControl = featureControl | 1;
            featureControl = featureControl | 2;

            var str = "$" + BaseCmd.FeatSet + ":" + Convert.ToString(featureControl, 16).PadLeft(2, '0') + "$";
            var strReponse = SendCommand(str);
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
    }
}
