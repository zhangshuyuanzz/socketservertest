using Base.kit;
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
        private Dictionary<string, DevInfo> DevList = new Dictionary<string, DevInfo>();

        private Dictionary<string ,int > IpToBoxIndex = new Dictionary < string ,int >(); // ipid  boxid
        private OpcDateUpdate ServerDB = null;

        /*  add 8/2  */
        private Dictionary<int, string> Everytags = new Dictionary<int, string>();
        public Form1()
        {
            InitializeComponent();
            logger.Debug("struct Form1");
            CreateBoxLIsts();

            ServerDB = new OpcDateUpdate();
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
            allboxs.Add(14, this.dev1IDT);

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
            //updatetestdb();
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
          try
          {
                DevInfo Tags = DevList[ip];                   //DevList:私有成员，记录所有采集机的所有的tag信息,用于显示显示信息，并且用于判断是初次得到的tag信息
                Tags.CuTime = DateTime.Now.ToString();

            string IDstr = "this";
            List<DataItem> NewTagList = new List<DataItem>();
            bool gotoflag = false;
            foreach (DevTagIndo s in tagslist)
            {
                TagInfo one;
                if (Tags.TagList.ContainsKey(s.ID))
                {
                    one = Tags.TagList[s.ID];
                    one.myvalue = s.value;
                }
                else
                {
                        gotoflag = true;
                        one = new TagInfo
                        {
                            myvalue = s.value
                        };
                        if (Everytags.ContainsKey(s.ID) == true)
                        {
                            one.myname = Everytags[s.ID];
                        }
                        else
                        {
                            logger.Debug("no has this tag!!");
                        }
                        Tags.TagList.Add(s.ID, one);
                }

                    DataItem Atag = new DataItem();
                    Atag.TagId = s.ID;
                    Atag.Value = s.value;
                    Atag.TagName = one.myname;
                    Atag.DataTime = Tags.CuTime;
                    NewTagList.Add(Atag);

                IDstr += ":" + s.ID.ToString();
                allboxs[IpToBoxIndex[ip + "4"]].Text = IDstr;
                logger.Debug("TagStr[{ }]", s.TagStr);
            }
                ServerDB.OpcDataWriteTag(NewTagList, gotoflag);

            allboxs[IpToBoxIndex[ip + "2"]].Text = Tags.TagList.Count.ToString();
            allboxs[IpToBoxIndex[ip + "3"]].Text = Tags.CuTime;

            }
            catch (Exception ex)
            {
                logger.Error("error[{}]", ex.ToString());
            }
        }
        public void ConnedNotification(object handle,string ip)
        {
            string IsExistPath = filepath + ip + ".xml";
            logger.Debug("ConnedNotification--ip[{}]IsExistPath[{}]", ip, IsExistPath);

            if (DevList.ContainsKey(ip) == false)
            {
                DevInfo onedev = new DevInfo();
                DevList.Add(ip, onedev);
                Serverconfig thisxmlfile = new Serverconfig(ip);
                Everytags = thisxmlfile.ServerConfigParseXml();
            }
            else
            {
                logger.Debug("has this ip");
            }
            SkServer ServerHandle = (SkServer)handle;

            if (System.IO.File.Exists(IsExistPath))
            {
                logger.Debug("thie file is exist!!!");
                System.Threading.Thread.Sleep(1000);
                ServerHandle.CoSendFile(ip, IsExistPath);
            }
            else
            {
                logger.Debug("thie file is not exist!!!");
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
                IpToBoxIndex.Add(ip + "4", count * 10 + 4);

                allboxs[count * 10 + 1].Text = ip;
                logger.Debug("id[{}]", count * 10 + 1);
            }
            else {
                logger.Debug("IpToBoxIndex has this ip");
            }
            logger.Debug("ConnedNotification end");
        }

        /*test-----------------test---------------------test------------------test------------------test-------test------------test------*/
        void testdb()
        {
            IpToBoxIndex.Add("192.168.0.881", 10 + 1);
            IpToBoxIndex.Add("192.168.0.882", 10 + 2);
            IpToBoxIndex.Add("192.168.0.883", 10 + 3);
            IpToBoxIndex.Add("192.168.0.884", 10 + 4);
            DevInfo onedev = new DevInfo();
            DevList.Add("192.168.0.88", onedev);


            List<DevTagIndo> tagslist = new List<DevTagIndo>();
            DevTagIndo one = new DevTagIndo(1, (float)1.1);
            DevTagIndo two = new DevTagIndo(2, (float)2.2);
            DevTagIndo th = new DevTagIndo(3, (float)3.3);
            DevTagIndo fo = new DevTagIndo(4, (float)4.4);
            DevTagIndo fi = new DevTagIndo(5, (float)5.5);

            tagslist.Add(one);
            tagslist.Add(two);
            tagslist.Add(th);
            tagslist.Add(fo);
            tagslist.Add(fi);
            UpdateDevInfo(tagslist, "192.168.0.88");
        }
        void updatetestdb()
        {

            List<DevTagIndo> tagslist = new List<DevTagIndo>();
            DevTagIndo one = new DevTagIndo(1, (float)3333);
            DevTagIndo two = new DevTagIndo(2, (float)5555);
            DevTagIndo th = new DevTagIndo(3, (float)888888);
            DevTagIndo fo = new DevTagIndo(4, (float)9999999);
            DevTagIndo fi = new DevTagIndo(5, (float)444444);

            tagslist.Add(one);
            tagslist.Add(two);
            tagslist.Add(th);
            tagslist.Add(fo);
            tagslist.Add(fi);
            UpdateDevInfo(tagslist, "192.168.0.88");
        }

        private void Label2_Click(object sender, EventArgs e)
        {

        }
    }
}
