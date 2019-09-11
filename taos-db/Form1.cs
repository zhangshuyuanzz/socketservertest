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

        public Form1()
        {
            logger.Debug("---Form1------------- - ");
            InitializeComponent();
            TDengineStart();
        }
        public void TDengineStart()
        {
            TDengineTest tester = new TDengineTest();
            tester.ReadArgument();

            logger.Debug("---------------------------------------------------------------------------------------------------- - ");
            logger.Debug("Starting TDengine...");
            logger.Debug("-------------------------------------------------------------------------------------------------------");

            tester.InitTDengine();
            tester.ConnectTDengine();
            tester.CreateDbAndTable();
            tester.ExecuteInsert();
            tester.ExecuteQuery();
            tester.CloseConnection();

            logger.Debug("---------------------------------------------------------------");
            logger.Debug("---------------------------------------------------------------");
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
