using Common.log;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace OpcServer
{
    public partial class Form1 : Form
    {
        private static readonly NLOG Logger = new NLOG("Form1");

        public Form1()
        {
            InitializeComponent();
            Logger.Debug("Form1 start !!");
            OpcData OpcDBDatas = new OpcData();
            OpcDBDatas.OpcDataGet();
            Logger.Debug("Form1 end !!");
        }
    }
}
