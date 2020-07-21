using System;
using System.Collections.Generic;
using System.Collections;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using System.Xml;
using System.Management;
using System.Diagnostics;
using Timer = System.Windows.Forms.Timer;

namespace PingTestTool
{
    public delegate void UpdateDelegate(string info);
    public partial class Form1 : Form
    {
        private string path = AppDomain.CurrentDomain.BaseDirectory;
        private UpdateDelegate updateUI;

        public string IPAddr_Path = "";
        public string IPAddr_File = "";
        public class Device
        {
            public string Ip;
            public string Name;
            public PingResult pingResult;
            public Device(string ip, string name)
            {
                Ip = ip;
                Name = name;
                pingResult = new PingResult();
            }
        }

        private List<Device> deviceList = new List<Device>();
        private Thread[] thread;
        private Thread wait;
        private int time_out;
        private int Package_size;
        private int try_times;
        private int interval;
        public enum PingResultEntryStatus
        {
            Success,
            GenericFailureSeeReplyStatus,
            PingAbortedForHighNetworkUsage,
            PingAbortedUnableToGetNetworkUsage,
            ExceptionRaisedDuringPing
        }

        public class PingResultEntry
        {
            public double? Rtt { get; private set; }
            public IPStatus? IpStatus { get; private set; }
            public PingResultEntryStatus Status { get; private set; }
            public DateTime Time { get; private set; }

            public PingResultEntry(double? rtt, IPStatus? ipStatus, PingResultEntryStatus status, DateTime time)
            {
                IpStatus = ipStatus;
                Rtt = rtt;
                Status = status;
                Time = time;
            }
        }

        public class PingResult
        {
            public List<PingResultEntry> results;
            public int? err_num;
            public int? total_num;
            private double? avg;
            private double? speed;
            public PingResult()
            {
                results = new List<PingResultEntry>();
                err_num = 0;
                total_num = 0;
            }

            public void addPingResultEntry(PingResultEntry newEntry)
            {
                // Adding a new entry
                results.Add(newEntry);
                // Reset any previously calculated stats (they where probably already null but who knows...)
                avg = null;
                speed = null;
            }

            public double? getSpeed(int length)
            {
                // Calculating speed if not yet calculated
                if (avg <= 0) return 1024.0F;
                return length / avg * 1000 / 1024;
            }

            public double? getAvg()
            {
                // Calculating avg if not yet calculated
                if (avg == null && results.Count(res => res.Status == PingResultEntryStatus.Success) > 0)
                {
                    avg = 0;
                    int cont = 0;
                    foreach (var e in results.Where(x => x.Status == PingResultEntryStatus.Success))
                    {
                        cont++;
                        avg += e.Rtt;
                    }
                    if (cont > 0)
                    {
                        avg = avg / cont;
                    }
                }
                return avg;
            }
        }

        public Form1()
        {
            InitializeComponent();
            //textBox1.Text = path + "ConfigData.xml";
            textBox_timeout.Text = "300";
            textBox_trytimes.Text = "1000";
            textBox_packetsize.Text = "4096";
            textBox_port.Text = "62626";
            checkBox1.Checked = false;
            textBox_interval.Text = "100";
            textBox_IP1.Text = " 192.168.0.1 ";
            textBox_IP2.Text = " 192.168.0.2 ";
            textBox_IP3.Text = " 192.168.0.3 ";
            textBox_IP4.Text = " 192.168.0.4 ";
            textBox_IP5.Text = " 192.168.0.5 ";
            textBox_IP6.Text = " 192.168.0.6 ";
            IPAddr_Path = System.AppDomain.CurrentDomain.BaseDirectory + @"IPaddress\";
            IPAddr_File = IPAddr_Path + "IPAddr.xml";
            textBox1.Text = IPAddr_File;
            Dictionary<string, string> DicIPAddrs = new Dictionary<string, string>();
            // LoadXmlFile(IPAddr_File);
            updateUI = UpdateList;
            // GetIP();
            textBox_IP.Text = GetLocalIp(); // get hostname ip address
            textBox_GW.Text = GetGateWayAddress(); // get default gateway address

            textBox_IP.Enabled = false;
            textBox_GW.Enabled = false;
            timerdraw.Enabled = true;
            timerdraw.Interval = 1000;
            timerdraw.Tick += SetTextBox;
            tcp_sw.AutoFlush = true;
            udp_sw.AutoFlush = true;
            
        }

        private void button1_Click(object sender, EventArgs e) // the browser button
        {
            if (DialogResult.OK == openFileDialog1.ShowDialog())
            {
                textBox1.Text = openFileDialog1.FileName;
                LoadXmlFile(textBox1.Text);
            }
        }
        private void GetIP()
        {
            string hostName = Dns.GetHostName();//本机名
                                                //System.Net.IPAddress[] addressList = Dns.GetHostByName(hostName).AddressList;//会警告GetHostByName()已过期，我运行时且只返回了一个IPv4的地址
            System.Net.IPAddress[] addressList = Dns.GetHostAddresses(hostName);//会返回所有地址，包括IPv4和IPv6
            foreach (IPAddress ip in addressList)
            {
                //listBox1.Items.Add(ip.ToString());
                // textBox_Host.Items.Add(ip.ToString());
            }
        }
#if true

        // 1.先调用系统API判断网络是否处于连接状态
        [DllImport("wininet.dll")]
        private static extern bool InternetGetConnectedState(out int connectionDescription, int reservedValue);
        [DllImport("Iphlpapi.dll")]
        private static extern int SendARP(Int32 dest, Int32 host, ref Int64 mac, ref Int32 length);

        public static bool IsLocalConnection()
        {
            int connectionDescription = 0;
            return InternetGetConnectedState(out connectionDescription, 0);
        }

        //根据主机名获取远程主机IP
        public string[] getRemoteIP(string RemoteHostName)
        {
            IPHostEntry ipEntry = Dns.GetHostByName(RemoteHostName);
            IPAddress[] IpAddr = ipEntry.AddressList;
            string[] strAddr = new string[IpAddr.Length];
            for (int i = 0; i < IpAddr.Length; i++)
            {
                strAddr[i] = IpAddr[i].ToString();
            }
            return strAddr;
        }

        //根据ip获取远程主机名
        public string GetRemoteHostName(string ip)
        {
            var d = Dns.GetHostEntry(ip);
            return d.HostName;
        }

        //获取本机的IP
        public static byte[] GetLocalIP()
        {
            //得到本机的主机名
            string strHostName = Dns.GetHostName();
            try
            {
                //取得本机所有IP(IPV4 IPV6 ...)
                IPAddress[] ipAddress = Dns.GetHostEntry(strHostName).AddressList;
                byte[] host = null;
                foreach (var ip in ipAddress)
                {
                    while (ip.GetAddressBytes().Length == 4)
                    {
                        host = ip.GetAddressBytes();
                        break;
                    }
                    if (host != null)
                        break;
                }
                return host;
            }
            catch (Exception)
            {
                return null;
            }

        }
        // 获取本地主机MAC地址
        public static string GetLocalMac(byte[] ip)
        {

            if (ip == null)
                return null;

            int host = (int)((ip[0]) + (ip[1] << 8) + (ip[2] << 16) + (ip[3] << 24));
            try
            {
                Int64 macInfo = new Int64();
                Int32 len = 6;
                int res = SendARP(host, 0, ref macInfo, ref len);
                return Convert.ToString(macInfo, 16);
            }
            catch (Exception err)
            {
                Console.WriteLine("Error:{0}", err.Message);
            }
            return null;
        }


        //2.再调用底层硬件获取本地网关地址信息
        static string GetGateWayAddress()
        {
            ManagementObjectCollection moc = new ManagementClass("Win32_NetworkAdapterConfiguration").GetInstances();
            foreach (ManagementObject mo in moc)
            {
                foreach (PropertyData p in mo.Properties)
                {
                    if (p.Name.Equals("DefaultIPGateway") && (p.Value != null))
                    {
                        string[] strs = p.Value as string[];

                        string[] str_gtw = strs;
                        int cnt_gtw = 0;
                        while (cnt_gtw < str_gtw.Length)
                        {
                            return str_gtw[cnt_gtw];
                        }
                        //return "192.168.1.1" ;
                    }
                }
            }
            return "";
        }
        //3.分别向本地网关内机器发送ICMP数据包
#if false
        bool Pinging(string addr, int id, uint taskid)
        {
            try
            {
                this.m_id = id;
                this.m_taskid = taskid;
                byte[] byReq = this.FillEchoReq();
                IPEndPoint lep = new IPEndPoint(IPAddress.Parse(addr), 0);
                this.socket.SendTo(byReq, lep);
            }
            catch (Exception e)
            {
                Console.WriteLine("Send error:" + e.ToString());
                return false;
            }
            return true;
        }
#endif
       // 4.定义本地机器节点信息类

        public class LocalMachine
        {
            // Fields   
            private string machineIP;
            private string machineMAC;
            private string machineName;

            // Methods   
            //public LocalMachine();
            // Properties   
            public string MachineIP { get; set; }
            public string MachineMAC { get; set; }
            public string MachineName { get; set; }
        }
        //5.根据arp原理，最后通过以下方式读取arp列表节点信息，其实这里还可以IMCP包响应来获取主机响应，
        //不过我个人认为用直接读取列表的方式更加快速有效。
        static /*ArrayList*/ List<string>  GetAllLocalMachines()
        {
            Process p = new Process();
            p.StartInfo.FileName = "cmd.exe";
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardInput = true;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.CreateNoWindow = true;
            p.Start();
            p.StandardInput.WriteLine("arp -a");
            p.StandardInput.WriteLine("exit");
           // ArrayList list = new ArrayList();
            List<string> listMachines = new List<string>();
            StreamReader reader = p.StandardOutput;
            string IPHead = Dns.GetHostByName(Dns.GetHostName()).AddressList[0].ToString().Substring(0, 3);
            for (string line = reader.ReadLine(); line != null; line = reader.ReadLine())
            {
                line = line.Trim();
                if (line.StartsWith(IPHead) && (line.IndexOf("dynamic") != -1))
                {
                    string IP = line.Substring(0, 15).Trim();
                    string Mac = line.Substring(line.IndexOf("-") - 2, 0x11).Trim();
                    LocalMachine localMachine = new LocalMachine();
                    localMachine.MachineIP = IP;
                    localMachine.MachineMAC = Mac;
                    localMachine.MachineName = "";
                    //list.Add(localMachine);
                    listMachines.Add(IP);
                }
            }
            return listMachines;
        }
#endif
        public static string GetLocalIp()
        {
            //得到本机名 
            string hostname = Dns.GetHostName();
            //解析主机名称或IP地址的system.net.iphostentry实例。
            IPHostEntry localhost = Dns.GetHostEntry(hostname);
            if (localhost != null)
            {
                foreach (IPAddress item in localhost.AddressList)
                {
                    //判断是否是内网IPv4地址
                    if (item.AddressFamily == AddressFamily.InterNetwork)
                    {
                        return item.MapToIPv4().ToString();
                    }
                }
            }
            return "can't obtain";
        }
        private void LoadXmlFile(string fileName)
        {
            Dictionary<string, string> IPAddr = new Dictionary<string, string>();
            try
            {
                if (File.Exists(IPAddr_File))
                {
                    XmlDocument doc = new XmlDocument();
                    doc.Load(IPAddr_File);
                    //doc.Load(fileName);
                    //deviceList.Clear();
                    //XmlNode xn = doc.SelectSingleNode("IPAddrs");
                    XmlNodeList list = doc.SelectNodes("/Root/Project/DeviceList/Device");
                    //XmlNodeList list = xn.ChildNodes;
                    if (list != null)
                    {
                        deviceList.Clear();
                        foreach (XmlNode n in list)
                        {
                            deviceList.Add(new Device(n.Attributes["ip"].Value, n.Attributes["name"].Value));
                        }
                     }
                }

                //  XmlNodeList list = doc.SelectNodes("/Root/Project/DeviceList/Device");

                foreach (var dev in deviceList)
                {
                    int i = dataGridView1.Rows.Add();
                    dataGridView1.Rows[i].Cells[0].Value = dev.Name;
                    dataGridView1.Rows[i].Cells[1].Value = dev.Ip;
                    
                    //if (i == 0) textBox_IP1.Text = dev.Ip;
                    //if (i == 1) textBox_IP2.Text = dev.Ip;
                    //if (i == 2) textBox_IP3.Text = dev.Ip;
                    //if (i == 3) textBox_IP4.Text = dev.Ip;
                    //if (i == 4) textBox_IP5.Text = dev.Ip;
                    //if (i == 5) textBox_IP6.Text = dev.Ip;
                    comboBox1.Items.Add(dev.Ip);
                }
                dataGridView1.ClearSelection();
               // updateUI = UpdateList;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private string WriteXmlFile(List<string> listIPAddrs)
        {
            XmlDocument doc = new XmlDocument();
            XmlDeclaration xmlDeclaration = doc.CreateXmlDeclaration("1.0", "utf-8", null);
            doc.InsertBefore(xmlDeclaration, doc.DocumentElement);

            XmlElement root = doc.CreateElement("Root");
            doc.AppendChild(root);
            XmlElement Project = doc.CreateElement("Project");
            root.AppendChild(Project);
            Project.SetAttribute("name", "XXXX");
            XmlElement DeviceList = doc.CreateElement("DeviceList");
            Project.AppendChild(DeviceList);

            for (int i = 0; i < listIPAddrs.Count; i++)
            {
               // XmlNode node = doc.CreateNode(XmlNodeType.Element, "Device", null );
                //book.SetAttribute("genre", "autobiography");
                XmlElement node = doc.CreateElement("Device");
                node.SetAttribute("name","IP"+ (i+1).ToString());
                node.SetAttribute("ip", listIPAddrs[i]);
                //node.InnerText = listIPAddrs[i];
                DeviceList.AppendChild(node);
            }
            if (!Directory.Exists(IPAddr_Path))
                Directory.CreateDirectory(IPAddr_Path);
            if (File.Exists(IPAddr_File))
                File.Delete(IPAddr_File);
            doc.Save(IPAddr_File);
            return "success";
            }
        private void button_begin_Click(object sender, EventArgs e)
        {
            List<string> listIPAddrs = new List<string>();
            if (deviceList.Count == 0)
            {
                MessageBox.Show("Please Input ip address and again click！");
                return;
            }

            if (button_begin.Text == "Stop")
            {
                wait?.Abort();
                wait = null;
                for (var i = 0; i < thread.Length; i++)
                {
                    if (thread[i] == null || !thread[i].IsAlive) continue;
                    thread[i].Abort();
                    thread[i] = null;
                }
                button_begin.Text = "Start";
                textBox_packetsize.Enabled = true;
                textBox_timeout.Enabled = true;
                textBox_trytimes.Enabled = true;
                checkBox1.Enabled = true;
                button_clear.Enabled = true;
                textBox_interval.Enabled = true;
                textBox_IP1.Enabled = true;
                textBox_IP2.Enabled = true;
                textBox_IP3.Enabled = true;
                textBox_IP4.Enabled = true;
                textBox_IP5.Enabled = true;
                textBox_IP6.Enabled = true;
            }
            else // begin
            {
                button_begin.Text = "Stop";
                textBox_packetsize.Enabled = false;
                textBox_timeout.Enabled = false;
                textBox_trytimes.Enabled = false;
                textBox_interval.Enabled = false;
                checkBox1.Enabled = false;
                button_clear.Enabled = false;
                textBox_IP1.Enabled = false;
                textBox_IP2.Enabled = false;
                textBox_IP3.Enabled = false;
                textBox_IP4.Enabled = false;
                textBox_IP5.Enabled = false;
                textBox_IP6.Enabled = false;

                Package_size = Convert.ToInt32(textBox_packetsize.Text);
                time_out = Convert.ToInt32(textBox_timeout.Text);
                try_times = Convert.ToInt32(textBox_trytimes.Text);
                interval = Convert.ToInt32(textBox_interval.Text);
#if false
                for(var i = 0; i <= 5; i++)
                {
                    string IPAddr = "";
                    switch (i)
                    {
                        case 0:
                            IPAddr = this.textBox_IP1.Text.Trim();
                            break;
                        case 1:
                            IPAddr = this.textBox_IP2.Text.Trim();
                            break;
                        case 2:
                            IPAddr = this.textBox_IP3.Text.Trim();
                            break;
                        case 3:
                            IPAddr = this.textBox_IP4.Text.Trim();
                            break;
                        case 4:
                            IPAddr = this.textBox_IP5.Text.Trim();
                            break;
                        case 5:
                            IPAddr = this.textBox_IP6.Text.Trim();
                            break;

                     }
                        
                    listIPAddrs.Add(IPAddr);
                }
                dataGridView1.ClearSelection();
                WriteXmlFile(listIPAddrs);
                LoadXmlFile(IPAddr_File);
                dataGridView1.Refresh();
                IsLocalConnection();
#endif
                //updateUI = UpdateList;
                //LoadXmlFile(IPAddr_File);
 
                thread = new Thread[deviceList.Count];
                for (var i = 0; i < deviceList.Count; i++)
                {
                    thread[i] = new Thread(Threadhandler) { IsBackground = true };
                    thread[i].Start(i);
                }

                wait = new Thread(waitforallthread) { IsBackground = true };
                wait.Start();
            }
        }

        private void waitforallthread()
        {
            bool flag = true;
            Thread.Sleep(200);
            while (flag)
            {
                flag = false;
                foreach (var thr in thread.Where(thr => thr.IsAlive))
                {
                    Thread.Sleep(2000);
                    flag = true;
                }
            }

            BeginInvoke(updateUI, "button");
        }

        private void Threadhandler(object index)
        {
            Ping pingSender = new Ping();
            PingOptions options = new PingOptions();
            byte[] buffer = new byte[Package_size];
            options.DontFragment = checkBox1.Checked;
            for (var i = 0; i < try_times; i++)
            {
                Device dev = deviceList[(int)index];
                try
                {
                    PingReply reply = pingSender.Send(dev.Ip, time_out, buffer, options);
                    if (reply.Status == IPStatus.Success)
                    {
                        // All has gone well
                        dev.pingResult.addPingResultEntry(new PingResultEntry(
                            reply.RoundtripTime, reply.Status, PingResultEntryStatus.Success, DateTime.Now));

                    }
                    else
                    {
                        // Something went wrong, wrong but "expected"
                        dev.pingResult.addPingResultEntry(new PingResultEntry(
                            reply.RoundtripTime, reply.Status, PingResultEntryStatus.GenericFailureSeeReplyStatus,
                            DateTime.Now));
                        dev.pingResult.err_num++;
                    }
                    dev.pingResult.total_num++;
                    dev.pingResult.getAvg();
                    dev.pingResult.getSpeed(Package_size);
                }
                catch
                {
                    dev.pingResult.addPingResultEntry(new PingResultEntry(
                                    null, null, PingResultEntryStatus.ExceptionRaisedDuringPing, DateTime.Now));
                }

                BeginInvoke(updateUI, "list" + index);

                Thread.Sleep(interval);
            }
        }

        private void UpdateList(string info)
        {
            if (info.Contains("list"))
            {
                int i = Convert.ToInt32(info.Remove(0, 4));
                Device dev = deviceList[i];
                dataGridView1.Rows[i].Cells[0].Value = dev.Name;
                dataGridView1.Rows[i].Cells[1].Value = dev.Ip;
                var ipStatus = dev.pingResult.results.Last().IpStatus;
                if (ipStatus != null)
                    dataGridView1.Rows[i].Cells[2].Value = ipStatus.ToString();

                var avg = dev.pingResult.getAvg();
                if (avg != null)
                    dataGridView1.Rows[i].Cells[3].Value = Math.Round((double)avg, 2).ToString();
                var speed = dev.pingResult.getSpeed(Package_size);
                if (speed != null)
                    dataGridView1.Rows[i].Cells[4].Value = Math.Round((double)speed, 2).ToString();
                dataGridView1.Rows[i].Cells[5].Value = dev.pingResult.err_num.ToString();
                dataGridView1.Rows[i].Cells[6].Value = dev.pingResult.total_num.ToString();

                //dataGridView1.Refresh();
                dataGridView1.ClearSelection();
            }
            else if (info == "button")
            {
                button_begin.Text = "Begin";
                textBox_packetsize.Enabled = true;
                textBox_timeout.Enabled = true;
                textBox_trytimes.Enabled = true;
                checkBox1.Enabled = true;
                button_clear.Enabled = true;
                textBox_interval.Enabled = true;
            }
        }

        private void button2_Click(object sender, EventArgs e) // saveip
        {
#if false
            List<string> listIPAddrs = new List<string>();
            for (var i = 0; i <= 5; i++)
            {
                string IPAddr = "";
                switch (i)
                {
                    case 0:
                        IPAddr = this.textBox_IP1.Text.Trim();
                        break;
                    case 1:
                        IPAddr = this.textBox_IP2.Text.Trim();
                        break;
                    case 2:
                        IPAddr = this.textBox_IP3.Text.Trim();
                        break;
                    case 3:
                        IPAddr = this.textBox_IP4.Text.Trim();
                        break;
                    case 4:
                        IPAddr = this.textBox_IP5.Text.Trim();
                        break;
                    case 5:
                        IPAddr = this.textBox_IP6.Text.Trim();
                        break;

                }

                listIPAddrs.Add(IPAddr);
            }
            dataGridView1.ClearSelection();
            WriteXmlFile(listIPAddrs);
            //LoadXmlFile(IPAddr_File);
            //dataGridView1.Refresh();
#endif
            string Mac = "";
            Mac = GetLocalMac(GetLocalIP());
            string[] strAddr_IP = new string[6];
            strAddr_IP =  getRemoteIP(GetRemoteHostName("192.168.0.101"));
        }

        private void button_ScanIP_Click(object sender, EventArgs e) // Scan all machine in the network
        {

            //ArrayList listMachines = new ArrayList();
            List<string> listIPs = new List<string>();
            listIPs = GetAllLocalMachines();
            if (WriteXmlFile(listIPs).Equals("success"))
            {
                dataGridView1.Rows.Clear();
                MessageBox.Show("ScanIP Success!");

                LoadXmlFile(IPAddr_File);
                dataGridView1.Refresh();
            }
        }
        private void button_clear_Click(object sender, EventArgs e)
        {
            foreach (var dev in deviceList)
            {
                dev.pingResult.results.Clear();
                dev.pingResult.err_num = 0;
                dev.pingResult.total_num = 0;

            }
            //clear the data shown in the list
            for (var i = 0 ; i < deviceList.Count; i++)
            {
                for (var j = 2; j < 7; j++) // from cell 3 begin to clear
                    dataGridView1.Rows[i].Cells[j].Value = " "; // use the white_button to replace the data.
            }
            dataGridView1.ResetText();
            //DataGridView dt = (DataGridView)dataGridView1.DataSource;
            //deviceList.Clear(); // clear
            //dataGridView1.Rows.Clear(); // clear the data window

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView1.ClearSelection();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            wait?.Abort();
            wait = null;
            if (thread != null)
            {
                for (var i = 0; i < thread.Length; i++)
                {
                    if (thread[i] == null || !thread[i].IsAlive) continue;
                    thread[i].Abort();
                    thread[i] = null;
                }
            }
            tcpthread?.Abort();
            udpthread?.Abort();
            tcp_sw.Close();
            fTcpLog.Close();
            udp_sw.Close();
            fUdpLog.Close();
        }

        enum RCVSTATUS
        {
            RCV_OK = 0,
            RCV_DATAERROR,
            RCV_TIMEOUT,
            RCV_CONNERROR,
        }

        public static string server_IP = "10.16.1.16";
        public static ushort server_Port = 62626;

        public static bool mode = true;    // 0 rising mode 1 constant mode
        public static ushort constLen = 0xc582;

        public bool udpRunning;
        public bool tcpRunning;

        public static bool tcp_cond;
        public static bool udp_cond;

        public static FileStream fUdpLog = new FileStream("ClientUdpLog.txt", FileMode.Create);
        public static FileStream fTcpLog = new FileStream("ClientTcpLog.txt", FileMode.Create);
        public static StreamWriter tcp_sw = new StreamWriter(fTcpLog);
        public static StreamWriter udp_sw = new StreamWriter(fUdpLog);

        public Thread tcpthread;
        public Thread udpthread;

        public static List<string> Strinfo = new List<string>();

        public const bool ModeRising = false;
        public const bool ModeConstant = true;

        public Timer timerdraw = new Timer();

        public void SetTextBox(object sender, EventArgs eventArgs)
        {
            richTextBox1.Text = string.Join(Environment.NewLine, Strinfo);
        }

        //调用API函数
        [DllImport("kernel32.dll")]
        extern static short QueryPerformanceCounter(ref long x);

        public class CTcpNet
        {
            Socket TCPsocket;
            int eData;
            int eTimeout;
            int iFrame;

            public bool NetInit()
            {
                TCPsocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                //----------------------
                // The sockaddr_in structure specifies the address family,
                // IP address, and port for the socket that is being bound.
                try
                {
                    TCPsocket.Connect(new IPEndPoint(IPAddress.Parse(server_IP), server_Port)); //配置服务器IP与端口
                }
                catch (SocketException)
                {
                    Strinfo.Add(@"[TCP]--Error at Connect()");
                    return false;
                }

                Strinfo.Add(@"[TCP] -- Start TCP communication...");
                int optval_buflen = 0x200000;
                TCPsocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveBuffer, optval_buflen);
                TCPsocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout, 5000);
                iFrame = 0;
                return true;
            }

            public void NetEcho()
            {
                byte[] rcvbuf = new byte[65524];
                byte[] sndbuf = new byte[65524];

                ushort sndlength;
                ushort rcvlength = 0;

                sndbuf[0] = 0x7;
                sndbuf[1] = 0;

                RCVSTATUS m_rcvStatus = RCVSTATUS.RCV_OK;

                int curRcvLen;
                int tmpSndLen;
                int tmpRcvLen;
                tmpRcvLen = 0;

                long nStart = 0;
                long nEnd = 0;
                int tDelt;
                int bWide;

                DateTime tNow;
                tNow = DateTime.Now;

                tcp_sw.WriteLine(tNow + "[TCP]--Communication Start");

                while ((RCVSTATUS.RCV_CONNERROR != m_rcvStatus) && tcp_cond)
                {
                    if (ModeRising == mode)
                        sndlength = (ushort)(iFrame % 0xfe00 + 9);
                    else
                        sndlength = constLen;

                    iFrame++;
                    m_rcvStatus = RCVSTATUS.RCV_OK;

                    QueryPerformanceCounter(ref nStart);

                    try
                    {
                        tmpSndLen = TCPsocket.Send(sndbuf, sndlength, 0);
                        curRcvLen = 0;
                        while (curRcvLen < tmpSndLen)
                        {
                            try
                            {
                                tmpRcvLen = TCPsocket.Receive(rcvbuf, curRcvLen, tmpSndLen - curRcvLen, 0);
                                if (tmpRcvLen > 0)
                                {
                                    curRcvLen += tmpRcvLen;
                                }
                                else if (tmpRcvLen == 0 || -1 == tmpRcvLen)
                                {
                                    tNow = DateTime.Now;
                                    tcp_sw.WriteLine(tNow + "[TCP]--recv tmpRcvLen error");
                                    m_rcvStatus = RCVSTATUS.RCV_CONNERROR;
                                    break;
                                }
                            }
                            catch (SocketException ex)
                            {
                                if (ex.SocketErrorCode == SocketError.TimedOut)
                                {
                                    tNow = DateTime.Now;

                                    tcp_sw.WriteLine(tNow + $"[TCP]--select {iFrame:X} timeout");
                                    tcp_sw.WriteLine("curRcvLen = {0:X}, tmpSndLen = {1:X} sndAllLength= {2:X}, sndBuf= {3:X}", curRcvLen, tmpSndLen, sndlength, sndbuf[2]);
                                    m_rcvStatus = RCVSTATUS.RCV_TIMEOUT;
                                    break;
                                }
                                tNow = DateTime.Now;
                                tcp_sw.WriteLine(tNow + $"[TCP]--select {iFrame:X}" + ex.SocketErrorCode);
                                m_rcvStatus = RCVSTATUS.RCV_CONNERROR;
                                break;
                            }
                        }
                        QueryPerformanceCounter(ref nEnd);
                        bool res = true;
                        for (var i = 0; i < tmpSndLen; i++)
                        {
                            if (sndbuf[i] == rcvbuf[i]) continue;
                            res = false;
                            break;
                        }

                        if (m_rcvStatus == RCVSTATUS.RCV_OK && (curRcvLen != tmpSndLen || res))
                        {
                            tNow = DateTime.Now;
                            tcp_sw.WriteLine(tNow + @"[TCP]-- {0:x} data error\r\n", iFrame);
                            m_rcvStatus = RCVSTATUS.RCV_DATAERROR;
                        }
                    }
                    catch (SocketException ex)
                    {
                        tNow = DateTime.Now;
                        tcp_sw.WriteLine(tNow + $@"[TCP]--send {iFrame:x} error " + ex.SocketErrorCode + @"\r\n");
                        m_rcvStatus = RCVSTATUS.RCV_CONNERROR;
                    }

                    

                    switch (m_rcvStatus)
                    {
                        case RCVSTATUS.RCV_OK:
                            tDelt = (int)(nEnd - nStart);
                            bWide = (int)(sndlength * 2 * 8 * 1000 / (double)tDelt);    //kbps
                            Strinfo.Add($@"[TCP] -- length: {sndlength:X6}, data_error={eData:d}, time_out={eTimeout:d}, {tDelt:d3}us /{bWide:d}kbps");
                            break;
                        case RCVSTATUS.RCV_DATAERROR:
                            eData++;
                            Strinfo.Add($"[TCP] -- length:{sndlength:X6}, data_error={eData:d}, time_out={eTimeout:d}");
                            break;
                        case RCVSTATUS.RCV_TIMEOUT:
                            eTimeout++;
                            Strinfo.Add($"[TCP] -- length:{sndlength:X6}, data_error={eData:d}, time_out={eTimeout:d}");
                            break;
                        case RCVSTATUS.RCV_CONNERROR:
                            Strinfo.Add("[TCP] -- connection error.");
                            goto ret;
                    }
                }
                ret:
                tNow = DateTime.Now;
                tcp_sw.WriteLine(tNow + "[TCP]--Communication Stop");
                tcp_sw.WriteLine("data_error = {0:d}, time_out = {1:d}", eData, eTimeout);
            }

            public void NetClose()
            {
                TCPsocket.Close();
            }
        }

        public class CUdpNet
        {
            Socket UDPsocket;
            int eData;
            int eTimeout;
            int iFrame;

            public bool NetInit()
            {
                UDPsocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                //----------------------
                // The sockaddr_in structure specifies the address family,
                // IP address, and port for the socket that is being bound.
                int optval_buflen = 0x200000;
                Strinfo.Add(@"[UDP] -- Start Udp communication...");
                UDPsocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout, 5000);
                UDPsocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveBuffer, optval_buflen);
                UDPsocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.SendBuffer, optval_buflen);
                iFrame = 0;
                return true;
            }

            public void NetEcho()
            {
                byte[] rcvbuf = new byte[65524];
                byte[] sndbuf = new byte[65524];

                ushort sndlength;
                ushort rcvlength = 0;

                sndbuf[0] = 0x7;
                sndbuf[1] = 0;

                RCVSTATUS m_rcvStatus = RCVSTATUS.RCV_OK;

                for (int cnt = 2; cnt < 65524; cnt++)
                    sndbuf[cnt] = 0x7;

                EndPoint sendpoint = new IPEndPoint(IPAddress.Parse(server_IP), server_Port);
                EndPoint recvpoint = new IPEndPoint(IPAddress.Any, 0);
                int curRcvLen;
                int tmpSndLen;
                int tmpRcvLen;

                long nStart = 0;
                long nEnd = 0;
                int tDelt;
                int bWide;

                DateTime tNow;
                tNow = DateTime.Now;

                udp_sw.WriteLine(tNow + "[UDP]--Communication Start");

                while ((RCVSTATUS.RCV_CONNERROR != m_rcvStatus) && udp_cond)
                {
                    if (ModeRising == mode)
                        sndlength = (ushort)(iFrame % 0xfe00 + 9);
                    else
                        sndlength = constLen;

                    iFrame++;
                    m_rcvStatus = RCVSTATUS.RCV_OK;

                    QueryPerformanceCounter(ref nStart);

                    try
                    {
                        tmpSndLen = UDPsocket.SendTo(sndbuf, sndlength, 0, sendpoint);

                        curRcvLen = 0;

                        while (curRcvLen < tmpSndLen)
                        {
                            try
                            {
                                tmpRcvLen = UDPsocket.ReceiveFrom(rcvbuf, curRcvLen, tmpSndLen - curRcvLen, 0, ref recvpoint);
                                if (tmpRcvLen > 0)
                                {
                                    curRcvLen += tmpRcvLen;
                                }
                                else if (tmpRcvLen == 0 || -1 == tmpRcvLen)
                                {
                                    tNow = DateTime.Now;
                                    udp_sw.WriteLine(tNow + "[UDP]--recv tmpRcvLen error");
                                    m_rcvStatus = RCVSTATUS.RCV_CONNERROR;
                                    break;
                                }
                            }
                            catch (SocketException ex)
                            {
                                if (ex.SocketErrorCode == SocketError.TimedOut)
                                {
                                    tNow = DateTime.Now;

                                    udp_sw.WriteLine(tNow + $"[UDP]--select {iFrame:X} timeout");
                                    udp_sw.WriteLine("curRcvLen = {0:X}, tmpSndLen = {1:X} sndAllLength= {2:X}, sndBuf= {3:X}", curRcvLen, tmpSndLen, sndlength, sndbuf[2]);
                                    m_rcvStatus = RCVSTATUS.RCV_TIMEOUT;
                                    break;
                                }
                                tNow = DateTime.Now;
                                udp_sw.WriteLine(tNow + $"[UDP]--select {iFrame:X}" + ex.SocketErrorCode);
                                m_rcvStatus = RCVSTATUS.RCV_CONNERROR;
                                break;
                            }
                        }

                        QueryPerformanceCounter(ref nEnd);
                        bool res = true;
                        for (var i = 0; i < tmpSndLen; i++)
                        {
                            if (sndbuf[i] == rcvbuf[i]) continue;
                            res = false;
                            break;
                        }

                        if (m_rcvStatus == RCVSTATUS.RCV_OK && (curRcvLen != tmpSndLen || res))
                        {
                            tNow = DateTime.Now;
                            udp_sw.WriteLine(tNow + "[UDP]-- {0:x} data error", iFrame);
                            m_rcvStatus = RCVSTATUS.RCV_DATAERROR;
                        }
                    }
                    catch (SocketException ex)
                    {
                        tNow = DateTime.Now;
                        udp_sw.WriteLine(tNow + $"[UDP]--send {iFrame:x} error " + ex.SocketErrorCode);
                        m_rcvStatus = RCVSTATUS.RCV_CONNERROR;
                    }

                    

                    switch (m_rcvStatus)
                    {
                        case RCVSTATUS.RCV_OK:
                            tDelt = (int)(nEnd - nStart);
                            bWide = (int)(sndlength * 2 * 8 * 1000 / (double)tDelt);    //kbps
                            Strinfo.Add($@"[UDP] -- length: {sndlength:X6}, data_error={eData:d}, time_out={eTimeout:d}, {tDelt:d3}us /{bWide:d}kbps");
                            break;
                        case RCVSTATUS.RCV_DATAERROR:
                            eData++;
                            Strinfo.Add($"[UDP] -- length:{sndlength:X6}, data_error={eData:d}, time_out={eTimeout:d}");
                            break;
                        case RCVSTATUS.RCV_TIMEOUT:
                            eTimeout++;
                            Strinfo.Add($"[UDP] -- length:{sndlength:X6}, data_error={eData:d}, time_out={eTimeout:d}");
                            break;
                        case RCVSTATUS.RCV_CONNERROR:
                            Strinfo.Add("[UDP] -- connection error.");
                            goto ret;
                    }
                }
                ret:
                tNow = DateTime.Now;
                udp_sw.WriteLine(tNow + "[UDP]--Communication Stop");
                udp_sw.WriteLine("data_error = {0:d}, time_out = {1:d}", eData, eTimeout);
            }

            public void NetClose()
            {
                UDPsocket.Close();
            }
        }

        public void ThreadTcpEcho()
        {
            CTcpNet mTcpClient = new CTcpNet();
            while (tcp_cond)
            {
                if (mTcpClient.NetInit())
                    mTcpClient.NetEcho();
                mTcpClient.NetClose();
            }
        }

        public void ThreadUdpEcho()
        {
            CUdpNet mUdpClient = new CUdpNet();

            while (udp_cond)
            {
                if (mUdpClient.NetInit())
                    mUdpClient.NetEcho();
                mUdpClient.NetClose();
            }
        }

        public void buttonStart_Click(object sender, EventArgs e)
        {

            if (buttonStart.Text == "Start")
            {
                buttonStart.Text = "Stop";

                mode = !checkBox2.Checked;
                tcp_cond = rdb_tcp.Checked;
                udp_cond = rdb_udp.Checked;
                server_Port = Convert.ToUInt16(textBox_port.Text);
                if (comboBox1.SelectedItem != null) server_IP = comboBox1.SelectedItem.ToString();
                timerdraw.Start();
                tcpthread = new Thread(ThreadTcpEcho);
                tcpthread.Start();
                udpthread = new Thread(ThreadUdpEcho);
                udpthread.Start();
            }
            else
            {
                buttonStart.Text = "Start";
                tcp_cond = false;
                udp_cond = false;

                tcpthread?.Abort();
                tcpthread = null;
                udpthread?.Abort();
                udpthread = null;
                timerdraw.Stop();
            }
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            richTextBox1.SelectionStart = richTextBox1.Text.Length;
            richTextBox1.ScrollToCaret();
        }

    }
}
