using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace service_tool
{
    public partial class Password : Form
    {
        public Password()
        {
            InitializeComponent();
        }
        public string strPassword="";
        private void PasswordOK_Click(object sender, EventArgs e)
        {
            strPassword = textBoxPassword.Text;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void PasswordCancel_Click(object sender, EventArgs e)
        {
            textBoxPassword.Clear();
            this.Close();
        }
    }
}
