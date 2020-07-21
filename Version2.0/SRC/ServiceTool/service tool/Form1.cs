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
using Timer = System.Timers.Timer;

namespace service_tool
{
    public partial class FormSetup : Form
    {
        public FormSetup()
        {
            InitializeComponent();
            getAvailablePorts();
            this.ComboBoxBaudRate.SelectedIndex = 1;
            this.timerReadSerial = new Timer();
            this.timerReadSerial.Interval = 100;    // leilc: in ms?
            this.timerReadSerial.Elapsed += timerReadSerial_Elapsed;
            this.timerReadSerial.Start();
        }

        private void timerReadSerial_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            this.timerReadSerial.Stop();
        
            try
            {
                if (serialPort1.IsOpen == true)
                {
                    string line = serialPort1.ReadLine() + Environment.NewLine;

                    TextBoxRead.BeginInvoke(new Action(() =>
                    {
                        // better performance than '+=' operation
                        TextBoxRead.AppendText(line);
                    }));
                }
                this.timerReadSerial.Start();
            }
            catch (Exception ex)
            {
                TextBoxRead.BeginInvoke(new Action(() =>
                {
                    TextBoxRead.AppendText(ex.ToString());
                }));
            }
            
            // we do not need restart the timer in case exception occurs, so moved the line below in the work loop
//            this.timerReadSerial.Start();
        }

        void getAvailablePorts()
        {
            String[] ports = SerialPort.GetPortNames();
            ComboBoxPortName.Items.AddRange(ports);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try 
            {
                if (ComboBoxPortName.Text == "" || ComboBoxBaudRate.Text == "")
                {
                    TextBoxRead.Text = "Please select port settings";
                }
                else
                {
                    TextBoxRead.Text = "";
                    serialPort1.PortName = ComboBoxPortName.Text;
                    serialPort1.BaudRate = Convert.ToInt32(ComboBoxBaudRate.Text);
                    serialPort1.Open();
                    progressBar1.Value = 100;
                    ButtonOpenPort.Enabled = false;
                    ButtonClosePort.Enabled = true;
                    ButtonSend.Enabled = true;
                    ButtonRead.Enabled = true;
                    TextBoxSend.Enabled = true;
                    
                }
            }
            catch(UnauthorizedAccessException)
            {
                TextBoxRead.Text = "Unauthorized Access";
            }
        }

        private void ButtonClosePort_Click(object sender, EventArgs e)
        {
            serialPort1.Close();
            progressBar1.Value = 0;
            ButtonClosePort.Enabled = false;
            ButtonOpenPort.Enabled = true;
            ButtonRead.Enabled = false;
            ButtonSend.Enabled = false;
            TextBoxSend.Enabled = false;
        }

        private void ButtonSend_Click(object sender, EventArgs e)
        {
            serialPort1.WriteLine(TextBoxSend.Text);
        }

        private void ButtonRead_Click(object sender, EventArgs e)
        {
            try
            {
                TextBoxRead.Text = serialPort1.ReadLine();
            }
            catch(TimeoutException)
            {
                TextBoxRead.Text = "Operation time out!";
            }
        }
        private Timer timerReadSerial;
    }
}
