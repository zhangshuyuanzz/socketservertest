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
    public partial class Form1 : Form
    {
        readonly NLOG logger = new NLOG("Form1");
        private Dictionary<int, System.Windows.Forms.TextBox> allboxs = new Dictionary<int, System.Windows.Forms.TextBox>();
        private string filepath = @".\config\zhang.txt";
        public Form1()
        {
            InitializeComponent();
            logger.Debug("struct Form1");
            CreateBoxLIsts();

            SkServer ServerHandle = new SkServer(11220);
            ServerHandle.SkStartListen();
            ServerHandle.Server_get_handle += new SkServer.ServerDataEventHandler(UpdateDevInfo);
            ServerHandle.Server_conn_handle += new SkServer.ServerConnEventHandler(SendFile);
            logger.Debug("struct Form1  end");
            
        }

        public void CreateBoxLIsts()
        {
            allboxs.Add(11, this.ip1);
            allboxs.Add(12, this.tag1);
            allboxs.Add(13, this.utime1);
            allboxs.Add(21, this.ip2);
            allboxs.Add(22, this.tag2);
            allboxs.Add(23, this.utime2);
            allboxs.Add(31, this.ip3);
            allboxs.Add(32, this.tag3);
            allboxs.Add(33, this.utime3);
        }
        private void TextBox6_TextChanged(object sender, EventArgs e)
        {
            logger.Debug("TextBox6_TextChanged");
        }
        private void Label1_Click(object sender, EventArgs e)
        {
            logger.Debug("Label1_Click");
            foreach (KeyValuePair<int , System.Windows.Forms.TextBox> s in allboxs)
            {
                logger.Debug("Key[{ }]",s.Key);
                allboxs[s.Key].Text = s.Key.ToString();
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            logger.Debug("Form1_Load");
        }
        private void Form1Closed(object sender, EventArgs e)
        {
            logger.Debug("form1_closed");
            System.Environment.Exit(System.Environment.ExitCode);
            this.Dispose();
            this.Close();
        }
        public void UpdateDevInfo(List<DevTagIndo> tagslist)
        {
            logger.Debug("updateDevInfo");
            foreach (DevTagIndo s in tagslist)
            {
                logger.Debug("TagStr[{ }]", s.TagStr);
                allboxs[s.ID].Text = s.TagStr;
            }
        }
        public void SendFile(object handle)
        {
            SkServer ServerHandle = (SkServer)handle;
            logger.Debug("SendFile");
            SkKit.COconfig filefd = new COconfig();
            logger.Debug("file [{}]", filefd.CoGetFileStr(filepath));
            ServerHandle.CoSendFile("192.168.0.88", filepath);

        }
    }
}
