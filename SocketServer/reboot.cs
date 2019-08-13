using Common.log;
using SkKit;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SocketServer
{
    public partial class reboot : Form
    {
        static readonly NLOG logger = new NLOG("reboot");
        private LittleTools reboottl = new LittleTools();
        public reboot()
        {
            InitializeComponent();
        }
        public void RebootFormClosingEventHandler(object sender, FormClosingEventArgs e)
        {
            logger.Debug("FormClosingEventHandler");
            this.Dispose();
            this.Close();
        }
        public void RebootEventHandler(object sender, EventArgs e)
        {
            logger.Debug("RebootEventHandler--ip[{}]", this.rebootip.Text);
        }
        public void RebootConfirmHandler(object sender, EventArgs e)
        {
            logger.Debug("RebootConfirmHandler--ip[{}]", this.rebootip.Text);
            if (reboottl.LTJudgeIsIpv4(this.rebootip.Text) == false) {
                MessageBox.Show(this,"this is invalid ip!!,please back to check again!!");
            }
        }
    }
}
