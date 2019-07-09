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

namespace onesocketserver
{
    public partial class Form1 : Form
    {
        NLOG logger = new NLOG("Form1");
        public Form1()
        {
            logger.Debug("9999999999999999999999999999");
            InitializeComponent();
            SkServer ServerHandle = new SkServer(11220);
            ServerHandle.SkStartListen();
        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {
            logger.Debug("TextBox1_TextChanged");
        }

        private void Label1_Click(object sender, EventArgs e)
        {
            logger.Debug("Label1_Click");
        }

        private void TextBox15_TextChanged(object sender, EventArgs e)
        {
            logger.Debug("TextBox15_TextChanged");

        }

        private void TextBox13_TextChanged(object sender, EventArgs e)
        {
            logger.Debug("TextBox13_TextChanged");

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            logger.Debug("Form1_Load");
        }
    }
}
