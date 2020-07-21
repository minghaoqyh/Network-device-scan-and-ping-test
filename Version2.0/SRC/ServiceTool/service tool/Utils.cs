using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.IO;
using ICSharpCode.SharpZipLib.Zip;
using System.Threading;

namespace service_tool
{
    public class Utils
    {
        public static string GetCommandResult(string strCommoands)
        {
            if (string.IsNullOrEmpty(strCommoands))
                return "";
            string strResult = "";
            try
            {
                if (strCommoands.IndexOf("$") >= 0)
                {
                    int startIndex = strCommoands.IndexOf("$");
                    if (strCommoands.Length > 1)
                    {
                        strCommoands = strCommoands.Substring(startIndex + 1);
                        int endIndex = strCommoands.IndexOf("$");
                        if (endIndex >= 0)
                            strCommoands = strCommoands.Substring(0, endIndex);
                    }
                }
                strResult = strCommoands;
            }
            catch
            {
                strResult = strCommoands;
            }
            return strResult;
        }

        public static string GetHourMinutes(int minutes)
        {
            StringBuilder strResult = new StringBuilder();
            int hour = 0;
            int minute = 0;
            try
            {
                if (minutes > 60)
                {
                    hour = minutes / 60;
                    minute = minutes % 60;
                }
                else
                    minute = minutes;
            }
            catch
            { 
                
            }
            if (hour > 0)
            {
                strResult.Append(hour.ToString() + " hours and ");
            }
            if (minute > 0)
                strResult.Append(minute.ToString());
            if (strResult.ToString().Equals(""))
                return "0";
            return strResult.ToString();
        }        

        public static byte Add32Checksum(byte[] byteData)
        {
            string strData = byteToHexStr(byteData);
            return Add32Checksum(strData);
        }

        public static byte Add32Checksum(string hexStr)
        {
            int checksum = 0;
            byte[] bStringValue = Encoding.ASCII.GetBytes(hexStr);
            foreach (byte bCharValue in bStringValue)
            {
                checksum += bCharValue;
            }
            return (byte)(checksum & 0xFF);
        }

        public static byte[] Add32Checksum4Bytes(byte[] hexData)
        {
            int checksum = 0;
            foreach (byte bCharValue in hexData)
            {
                checksum += bCharValue;
            }
            var byteTotalDataLength = new byte[4];
#if true 
            byteTotalDataLength[0] = (byte)(checksum & 0xFFFFFF);
            byteTotalDataLength[1] = (byte)((checksum >> 8) & 0xFFFFFF);
            byteTotalDataLength[2] = (byte)((checksum >> 16) & 0xFFFFFF);
            byteTotalDataLength[3] = (byte)((checksum >> 24) & 0xFFFFFF);
#else
            byteTotalDataLength[0] = (byte)((checksum >> 24) & 0xFFFFFF);
            byteTotalDataLength[1] = (byte)((checksum >> 16) & 0xFFFFFF);
            byteTotalDataLength[2] = (byte)((checksum >> 8) & 0xFFFFFF);
            byteTotalDataLength[3] = (byte)(checksum & 0xFFFFFF);
#endif
            return byteTotalDataLength;
        }

        public static void SetTextboxValue(TextBox tb,string txtMsg)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtMsg))
                    tb.Invoke(new Action(() => tb.Text = txtMsg));
            }
            catch
            {
                ;
            }
        }

        public static string GetVersionFromByteArray(string strVersion)
        {
            try
            {
                byte[] byteVersion = StrToHexByte(strVersion);
                int version0 = Convert.ToInt32(byteVersion[0]);
                int version1 = Convert.ToInt32(byteVersion[1]);
                int version2 = Convert.ToInt32(byteVersion[2]);

                return version0.ToString() + "." + version1.ToString() + "." + version2.ToString();
            }
            catch
            {
                return "";
            }
        }


        public static string byteToHexStr(byte[] bytes)
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

        public static string HexStringToASCII(string hexstring)
        {
            byte[] bt = StrToHexByte(hexstring);
            string lin = "";
            for (int i = 0; i < bt.Length; i++)
            {
                lin = lin + bt[i] + " ";
            }


            string[] ss = lin.Trim().Split(new char[] { ' ' });
            char[] c = new char[ss.Length];
            int a;
            for (int i = 0; i < c.Length; i++)
            {
                a = Convert.ToInt32(ss[i]);
                c[i] = Convert.ToChar(a);
            }

            string b = new string(c);
            return b;
        }

        /// <summary>
        /// String to Byte
        /// </summary>
        /// <param name="strTemp"></param>
        /// <returns></returns>
        public static byte[] StrToByte(string strTemp)
        {
            return Encoding.Default.GetBytes(strTemp);
        }        

        public static void DeleteLogFile(string logFileName)
        {
            if (File.Exists(logFileName))
            {
                File.Delete(logFileName);
            }
        }

        private static string CENTRAL = "UNO3CentralCtrl_CORE";
        private static string OPMI = "UNO3OPMICtrl_CORE";
        private static string CCU = "UNO3CCUGateway_CORE";

        public static bool CheckPcbaFileName(PCBAType pcbaType, string fileName,out string StatusMessage)
        {
            if (pcbaType == PCBAType.Default)
            {
                StatusMessage = "Pcba type error:" + pcbaType.ToString();
                return false;
            }
            string regexPrefix = "";
            switch (pcbaType)
            { 
                case PCBAType.CentralControl:
                    regexPrefix = CENTRAL;
                    break;
                case PCBAType.OPMIControl:
                    regexPrefix = OPMI;
                    break;
                case PCBAType.CCUGateway:
                    regexPrefix = CCU;
                    break;
            }
            string strMessage = regexPrefix + ".xx.xx.xx.APP.xx.xx.xx.hex";

            Regex validipregex = new Regex(@"^" + regexPrefix + @".(([0-9]|[0-9][0-9])\.){2}([0-9]|[0-9][0-9]).APP.(([0-9]|[0-9][0-9])\.){2}([0-9]|[0-9][0-9]).hex$");
            if (fileName != "" && validipregex.IsMatch(fileName.Trim()))
            {
                StatusMessage = "";
                return true;
            }
            else
            {
                StatusMessage = "File name error,the correct format should be:\n" + strMessage;
                return false;
            }
        }

        public static void LogDebug(string logFileName, string strMsg)
        {
            try
            {
                Task.Factory.StartNew(new Action(() =>
                {
                    try
                    {
                        using (FileStream fs = new FileStream(logFileName, FileMode.OpenOrCreate))
                        {
                            byte[] msg = Encoding.Default.GetBytes(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff") + " " + strMsg + " \r\n");
                            fs.Position = fs.Length;
                            fs.Write(msg, 0, msg.Length);
                            fs.Flush();
                            fs.Close();
                        }
                    }
                    catch
                    {
                        ;
                    }
                }));
            }
            catch
            { }
        }

        public static void DelectDir(string srcPath)
        {
            try
            {
                DirectoryInfo dir = new DirectoryInfo(srcPath);
                FileSystemInfo[] fileinfo = dir.GetFileSystemInfos();
                if (fileinfo.Length > 0)
                {
                    foreach (FileSystemInfo i in fileinfo)
                    {
                        if (i is DirectoryInfo)
                        {
                            DirectoryInfo subdir = new DirectoryInfo(i.FullName);
                            subdir.Delete(true);
                        }
                        else
                        {
                            File.Delete(i.FullName);
                        }
                    }
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                GC.Collect();
            }
        }

        public static bool UnZip(string fileToUnZip, string zipedFolder, string password)
        {
            bool result = true;
            FileStream fs = null;
            ZipInputStream zipStream = null;
            ZipEntry ent = null;
            string fileName;

            if (!File.Exists(fileToUnZip))
                return false;

            if (Directory.Exists(zipedFolder))
            {
                DelectDir(zipedFolder);
            }
            Directory.CreateDirectory(zipedFolder);
            
            try
            {
                zipStream = new ZipInputStream(File.OpenRead(fileToUnZip));
                if (!string.IsNullOrEmpty(password)) zipStream.Password = password;
                while ((ent = zipStream.GetNextEntry()) != null)
                {
                    if (!string.IsNullOrEmpty(ent.Name))
                    {
                        fileName = Path.Combine(zipedFolder, ent.Name);
                        fileName = fileName.Replace('/', '\\');//change by Mr.HopeGi   

                        int index = ent.Name.LastIndexOf('/');
                        if (index != -1 || fileName.EndsWith("\\"))
                        {
                            string tmpDir = (index != -1 ? fileName.Substring(0, fileName.LastIndexOf('\\')) : fileName) + "\\";
                            if (!Directory.Exists(tmpDir))
                            {
                                Directory.CreateDirectory(tmpDir);
                            }
                            if (tmpDir == fileName)
                            {
                                continue;
                            }
                        }

                        fs = File.Create(fileName);
                        int size = 2048;
                        byte[] data = new byte[size];
                        while (true)
                        {
                            size = zipStream.Read(data, 0, data.Length);
                            if (size > 0)
                                fs.Write(data, 0, data.Length);
                            else
                                break;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Unzip file error:"+e.ToString());
                return false;
            }
            finally
            {
                if (fs != null)
                {
                    fs.Close();
                    fs.Dispose();
                }
                if (zipStream != null)
                {
                    zipStream.Close();
                    zipStream.Dispose();
                    zipStream = null;
                }
                if (ent != null)
                {
                    ent = null;
                }
                GC.Collect();
                GC.Collect(1);
                //Thread.Sleep(500);
            }
            return result;
        }
    }

    public enum PCBAType
    {
        Default = 0x00,
        CentralControl = 0x10,
        OPMIControl = 0x20,
        CCUGateway = 0x30
    }

    public enum ChecksumType
    {
        ADD32 = 0x00,
        CRC16 = 0x40,
        ADD32X = 0x80
    }



}
