using Base.kit;
using Common.log;
using OpcSvr;
using SkKit;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace SocketServer
{
    public partial class Form1 : Form
    {
        static readonly NLOG logger = new NLOG("Form1");
        private Dictionary<int, System.Windows.Forms.TextBox> allboxs = new Dictionary<int, System.Windows.Forms.TextBox>();
        private string filepath = @".\config\";
        private Dictionary<string, DevInfo> DevList = new Dictionary<string, DevInfo>();

        private Dictionary<int,string > IpToBoxIndex = new Dictionary <int, string>(); // <ip,boxid>用于得到的数据与界面box关联
        private OpcDateUpdate ServerDB = null;

        /*  add 8/2   用于获取tag name */
        private Dictionary<int, string> Everytags = new Dictionary<int, string>();
        private Dictionary<string, int> Everytagname = new Dictionary<string, int>();

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
            logger.Debug(" Form1 InitializeComponent end");

            Serverconfig thisxmlfile = new Serverconfig("192.168.0.88");
            logger.Debug("ret----------------------88------------[{}]---------------", thisxmlfile.isxml());
            thisxmlfile = new Serverconfig("192.168.0.89");
            logger.Debug("ret---------------------89-------------[{}]---------------", thisxmlfile.isxml());

        }
        public void CreateBoxLIsts()
        {
            allboxs.Add(11, this.ip1);
            allboxs.Add(12, this.tag1);
            allboxs.Add(13, this.utime1);
            allboxs.Add(14, this.dev1IDT);
            allboxs.Add(15, this.tagname1);

            allboxs.Add(21, this.ip2);
            allboxs.Add(22, this.tag2);
            allboxs.Add(23, this.utime2);
            allboxs.Add(24, this.dev2IDT);
            allboxs.Add(25, this.tagname2);

            allboxs.Add(31, this.ip3);
            allboxs.Add(32, this.tag3);
            allboxs.Add(33, this.utime3);
            allboxs.Add(34, this.dev3IDT);
            allboxs.Add(35, this.tagname3);

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
            Thread thread = new Thread(new ThreadStart(OpcServerMain));
            thread.Start();
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
            if (DevList.ContainsKey(ip) == false) {
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
                    one.mytime = Tags.CuTime;
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

                SkUpdateTime(one.myname,ip);
                logger.Debug("TagStr[{ }]", s.TagStr);
            }

                foreach (KeyValuePair<int, string> s in IpToBoxIndex)
                {
                    logger.Debug("ip,[{}]key[{}]value[{}]", ip, s.Key, s.Value);
                    if (string.Compare(ip, s.Value) == 0)
                    {
                        int num = s.Key;
                        allboxs[num + 4].Text = IDstr;
                        allboxs[num + 2].Text = Tags.TagList.Count.ToString();
                    }
                }
                ServerDB.OpcDataWriteTag(NewTagList, gotoflag);

            }
            catch (Exception ex)
            {
                logger.Error("error[{}]", ex.ToString());
            }
        }
        List<string> IPs = new List<string>();
        private void changeIpdev()
        {
            string ipstr = "IP:";
            foreach (string s in IPs)
            {
                ipstr += "--" + s;
            }
            logger.Debug("changeIpdev--ipstr[{}]", ipstr);
            conndevt.Text = ipstr;
        }
        public void DisconnedNotification(object handle, string ip)
        {
            logger.Debug("DisconnedNotification--ip[{}]", ip);
            IPs.Remove(ip);
            changeIpdev();
        }
        public void ConnedNotification(object handle,string ip)
        {
            string IsExistPath = filepath + ip + ".xml";
            logger.Debug("ConnedNotification--ip[{}]IsExistPath[{}]", ip, IsExistPath);

            if (DevList.ContainsKey(ip) == false)
            {
                IPs.Add(ip);
                DevInfo onedev = new DevInfo();
                DevList.Add(ip, onedev);
                Serverconfig thisxmlfile = new Serverconfig(ip);
                thisxmlfile.ServerConfigParseXml(ref Everytags,ref Everytagname);
                changeIpdev();
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
            logger.Debug("ConnedNotification end");
        }
        private static void OpcServerMain()
        {
            logger.Debug("OpcServerMain");
            /*open local db*/
            OpcDataSu OpcDBDatas = new OpcDataSu();

            OpcServerLib kleopcserver = new OpcServerLib("JVRPS53R5V64226N62H4");
            string exepath;
            exepath = Application.StartupPath + @"\OpcServer.exe";
            kleopcserver.ServerRegister(exepath);
            kleopcserver.ServerInit();

            /*register opc server handle*/
            OpcDBDatas.Opc_tag_data_handle += new OpcDataSu.OpcServerEventHandler(kleopcserver.OpcServerUpdateTag);

            /*set (get opc tag info) timer*/
            _ = OpcDBDatas.OpcDateRegisterCB(kleopcserver.ServerRate);
        }
        private void Ip1_TextChanged(object sender, EventArgs e)
        {
            logger.Debug("Ip1_TextChanged-----{}", this.ip1.Text);
            string tt = this.ip1.Text;
            if (SkJudgeIsIpv4(tt, this.tagname1) == true)
            {
                SkUpdateIpToBox(tt, 1);
            }
            else
            {
                logger.Debug("this is invalid ip !!");
            }
        }
        private void Ip2_TextChanged(object sender, EventArgs e)
        {
            logger.Debug("Ip2_TextChanged-----{}", this.ip2.Text);
            string tt = this.ip2.Text;
            if (SkJudgeIsIpv4(tt, this.tagname2) == true)
            {
                SkUpdateIpToBox(tt, 2);
            }
            else
            {
                logger.Debug("this is invalid ip !!");
            }
        }
        private void Ip3_TextChanged(object sender, EventArgs e)
        {
            logger.Debug("Ip3_TextChanged----{}",this.ip3.Text);
            string tt = this.ip3.Text;
            if (SkJudgeIsIpv4(tt, this.tagname3) == true)
            {
                SkUpdateIpToBox(tt, 3);
            }
            else {
                logger.Debug("this is invalid ip !!");
            }
        }
        private bool SkJudgeIsIpv4(string IPstr,in System.Windows.Forms.TextBox tb)
        {
            string ipreg = @"(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)(\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)){3}";
            if (System.Text.RegularExpressions.Regex.IsMatch(IPstr, ipreg))
            {
                tb.ReadOnly = false;
                return true;
            }
            else {
                tb.ReadOnly = true;
                return false;
            }
        }
        private void SkUpdateIpToBox(string ip,int number)
        {
            logger.Debug("this is effient ip [{}]!!", ip);

            if (number  > 3) {
                logger.Debug("wrong number---[{}]", number);
                return;
            }
            if (IpToBoxIndex.ContainsKey(number*10) == true)
            {
                IpToBoxIndex[number * 10] = ip;
                /*
                IpToBoxIndex[number * 10+1] = ip + "1";
                IpToBoxIndex[number * 10+2] = ip + "2"; ;
                IpToBoxIndex[number * 10+3] = ip + "3"; ;
                IpToBoxIndex[number * 10+4] = ip + "4"; ;
                IpToBoxIndex[number * 10+5] = ip + "5"; ;
                */
            }
            else
            {
                IpToBoxIndex.Add(number * 10,ip);
                /*
                IpToBoxIndex.Add(number * 10 + 1,ip + "1");
                IpToBoxIndex.Add(number * 10 + 2,ip + "2");
                IpToBoxIndex.Add(number * 10 + 3,ip + "3");
                IpToBoxIndex.Add(number * 10 + 4,ip + "4");
                IpToBoxIndex.Add(number * 10 + 5,ip + "5");
                */
            }

            foreach (KeyValuePair<int,string> s in IpToBoxIndex)
            {
                logger.Debug("IpToBoxIndex----Key[{}]Value[{}]", s.Key, s.Value);
            }
            if ( DevList.ContainsKey(ip) == false)
            {
                logger.Error("not need to update!!");
                return;
            }
            DevInfo OneDev = DevList[ip];
            allboxs[number * 10 + 2].Text = OneDev.TagList.Count.ToString();
        }
        private void SkUpdateTime(string tagname,string ip)
        {
            logger.Debug("SkUpdateTime  name[{}]ip[{}]", tagname, ip);
            if (DevList.ContainsKey(ip) == false || IpToBoxIndex.ContainsValue(ip) == false) {
                logger.Error("not need to update!!");
                return;
            }
            Dictionary<int, TagInfo> Alltag = DevList[ip].TagList;
            if (Everytagname.ContainsKey(tagname) == true)
            {
                logger.Debug("id[{}]", Everytagname[tagname]);
                if (Alltag.ContainsKey(Everytagname[tagname]) == true) {
                    foreach (KeyValuePair<int, string> s in IpToBoxIndex)
                    {
                        foreach (KeyValuePair<int, System.Windows.Forms.TextBox> js in allboxs) {
                        }
                        logger.Debug("-------------------------------------");

                        string tagname_temp = allboxs[s.Key + 5].Text;

                        if (string.Compare(ip, s.Value) == 0 && string.Compare(tagname_temp, tagname) == 0) {
                            int num = s.Key;
                            logger.Debug("this tag name,num[{}] time is [{}]", num,Alltag[Everytagname[tagname]].mytime);
                            allboxs[num + 3].Text = Alltag[Everytagname[tagname]].mytime;
                        }
                    }
                }

            }
            else {
                logger.Error("no have this tag name!!");
            }
        }

        /*test-----------------test---------------------test------------------test------------------test-------test------------test------*/
        void testdb()
        {
//            IpToBoxIndex.Add("192.168.0.881", 10 + 1);
   //         IpToBoxIndex.Add("192.168.0.882", 10 + 2);
      //      IpToBoxIndex.Add("192.168.0.883", 10 + 3);
         //   IpToBoxIndex.Add("192.168.0.884", 10 + 4);
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

        private void Tag3_TextChanged(object sender, EventArgs e)
        {

        }

        private void tagnameboxchange(string tagname, string ip,System.Windows.Forms.TextBox tagtimebox)
        {
            Dictionary<int, TagInfo> Alltag = DevList[ip].TagList;
            if (Everytagname.ContainsKey(tagname) == true)
            {
                logger.Debug("--id[{}]", Everytagname[tagname]);
                if (Alltag.ContainsKey(Everytagname[tagname]) == true)
                {
                    logger.Debug("this,tagname[{}] time is [{}]", tagname, Alltag[Everytagname[tagname]].mytime);
                    tagtimebox.Text = Alltag[Everytagname[tagname]].mytime;
               }

            }
            else
            {
                logger.Error("no have this tag name!!");
            }

        }
        private void Tagname1_TextChanged(object sender, EventArgs e)
        {
            logger.Debug("Tagname1_TextChanged--tagname[{}]ip[{}]",this.tagname1.Text, this.ip1.Text);
            tagnameboxchange(this.tagname1.Text, this.ip1.Text, this.utime1);

        }
        private void Tagname2_TextChanged(object sender, EventArgs e)
        {
            logger.Debug("Tagname2_TextChanged--tagname[{}][{}]", this.tagname2.Text, this.ip2.Text);
            tagnameboxchange(this.tagname2.Text, this.ip2.Text,this.utime2);
        }
        private void Tagname3_TextChanged(object sender, EventArgs e)
        {
            logger.Debug("Tagname3_TextChanged--tagname[{}][{}]", this.tagname3.Text, this.ip3.Text);
            tagnameboxchange(this.tagname3.Text, this.ip3.Text, this.utime3);

        }
    }
}
