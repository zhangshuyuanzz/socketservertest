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
        private string filepath = @".\config\";
        private Dictionary<string, TagInfo> DevList = new Dictionary<string, TagInfo>();

        private Dictionary<string ,int > IpToBoxIndex = new Dictionary < string ,int >(); // ipid  boxid
        public Form1()
        {
            InitializeComponent();
            logger.Debug("struct Form1");
            CreateBoxLIsts();

            SkServer ServerHandle = new SkServer(11220);
            ServerHandle.SkStartListen();
            ServerHandle.Server_get_handle += new SkServer.ServerDataEventHandler(UpdateDevInfo);
            ServerHandle.Server_conn_handle += new SkServer.ServerConnEventHandler(ConnedNotification);
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
        public void UpdateDevInfo(List<DevTagIndo> tagslist,string ip)
        {
            logger.Debug("updateDevInfo---ip[{}]", ip);
            if (DevList.ContainsKey(ip) == false || IpToBoxIndex.ContainsKey(ip+"1") == false) {
                return;
            }
            TagInfo onetag = DevList[ip];
            foreach (DevTagIndo s in tagslist)
            {
                if (onetag.TagList.ContainsKey(s.ID))
                {
                    onetag.TagList[s.ID] = s.value;
                }
                else
                {
                    onetag.TagList.Add(s.ID, s.value);
                }
                logger.Debug("TagStr[{ }]", s.TagStr);
            }
            onetag.CuTime = DateTime.Now.ToString();
            allboxs[IpToBoxIndex[ip + "2"]].Text = onetag.TagList.Count.ToString();
            allboxs[IpToBoxIndex[ip + "3"]].Text = onetag.CuTime;

        }
        public void ConnedNotification(object handle,string ip)
        {
            string IsExistPath = filepath + ip + ".xml";
            logger.Debug("ConnedNotification--ip[{}]IsExistPath[{}]", ip, IsExistPath);

            if (DevList.ContainsKey(ip) == false)
            {
                SkServer ServerHandle = (SkServer)handle;

                if (System.IO.File.Exists(IsExistPath))
                {
                    logger.Debug("thie file is exist!!!");
                    ServerHandle.CoSendFile(ip, IsExistPath);
                }
                else
                {
                    logger.Debug("thie file is not exist!!!");
                }

                TagInfo onedev = new TagInfo();
                DevList.Add(ip, onedev);
            }
            else {
                logger.Debug("has this ip");
            }
            foreach(KeyValuePair<string, int> s in IpToBoxIndex) {
                logger.Debug("Key[{}]Value[{}]", s.Key,s.Value);
            }
            if (IpToBoxIndex.ContainsKey(ip + "1") == false)
            {
                int count = DevList.Count;
                IpToBoxIndex.Add(ip + "1", count * 10 + 1);
                IpToBoxIndex.Add(ip + "2", count * 10 + 2);
                IpToBoxIndex.Add(ip + "3", count * 10 + 3);
                allboxs[count * 10 + 1].Text = ip;
                logger.Debug("id[{}]", count * 10 + 1);
            }
            else {
                logger.Debug("IpToBoxIndex has this ip");
            }
            logger.Debug("ConnedNotification end");
        }
    }
}
