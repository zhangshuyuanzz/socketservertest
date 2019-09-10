using Common.log;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TDengineDriver;

namespace taos_db
{
    public partial class Form1 : Form
    {
        private NLOG logger = new NLOG("SkServer");

        public Form1(String[] argv)
        {
            logger.Debug("---Form1------------- - ");
            InitializeComponent();
            TDengineStart(argv);
        }
        public void TDengineStart(String[] argv)
        {
            TDengineTest tester = new TDengineTest();
            tester.ReadArgument(argv);

            logger.Debug("-------------------------------------------------------------- - ");
            logger.Debug("---------------------------------------------------------------");
            logger.Debug("Starting Testing...");
            logger.Debug("---------------------------------------------------------------");

            tester.InitTDengine();
            tester.ConnectTDengine();
            tester.CreateDbAndTable();
            tester.ExecuteInsert();
            tester.ExecuteQuery();
            tester.CloseConnection();

            logger.Debug("---------------------------------------------------------------");
            logger.Debug("Stop Testing...");
            logger.Debug("---------------------------------------------------------------");
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
