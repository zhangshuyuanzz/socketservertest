using Common.log;
using OpcSvr;
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
        OpcServerLib kleopcserver;
        private void OpcSererInit()
        {
            Logger.Debug("OpcSererInit");
            kleopcserver = new OpcServerLib("JVRPS53R5V64226N62H4");
            string exepath;
            exepath = Application.StartupPath + @"\OpcServer.exe";
            kleopcserver.ServerRegister(exepath);
            kleopcserver.ServerInit();
        }
        public Form1()
        {
            InitializeComponent();
            Logger.Debug("Form1 start !!");

            /*open local db*/
            OpcData OpcDBDatas = new OpcData();
            //List<string>devIPs = OpcDBDatas.OpcDateIPList();

            /*provide opc server*/
            OpcSererInit();

            /*register opc server handle*/
            OpcDBDatas.Opc_tag_data_handle += new OpcData.OpcServerEventHandler(kleopcserver.OpcServerUpdateTag);

            /*set (get opc tag info) timer*/
            _ = OpcDBDatas.OpcDateRegisterCB(kleopcserver.ServerRate);
            Logger.Debug("Form1  init end !!");
        }
        private void Form1Closed(object sender, EventArgs e)
        {
            Logger.Debug("form1_closed");
            System.Environment.Exit(System.Environment.ExitCode);
            this.Dispose();
            this.Close();
        }
    }
}
