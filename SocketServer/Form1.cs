using Base.kit;
using Common.log;
using OpcSvr;
using SkKit;
using SkKit.FileWatch;
using System;
using System.Collections.Concurrent;
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
        private Dictionary<string, DevInfo> DevList = new Dictionary<string, DevInfo>();   //保存有当前可更新的所有的设备数据

        private Dictionary<int,string > IpToBoxIndex = new Dictionary <int, string>(); // <ip,boxid>用于得到的数据与界面box关联
        private OpcDateUpdate ServerDB = null;

        //private XmlFileWatch tt;
        private BindingList<string> BindIplist = new BindingList<string>();    //动态绑定界面的list控件
        private reboot RebootDialog = new reboot();

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
            ServerHandle.Server_disconn_handle += new SkServer.ServerDisconnEventHandler(DisconnedNotification);

            this.colist1.DataSource = BindIplist;
            this.colist2.DataSource = BindIplist;
            this.colist3.DataSource = BindIplist;

            logger.Debug(" Form1 InitializeComponent end");

            // this.tt = new XmlFileWatch(this.filepath);
            //this.tt.OnChanged += new XmlFileWatch.xmlFileSystemEventHandler(OpcServerTransferXmlfile);

            AppDomain currentDomain = AppDomain.CurrentDomain;
            currentDomain.UnhandledException += new UnhandledExceptionEventHandler(MyHandler);
            RebootDialog.OnIpChange += new reboot.IpEventHandler(GetRebootIp);
        }
        static void MyHandler(object sender, UnhandledExceptionEventArgs args)
        {
            Exception e = (Exception)args.ExceptionObject;
            logger.Fatal("**************MyHandler caught : " , e.Message);
            logger.Fatal("**************error caught : [{}]" , e.ToString());
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
        private void Form1_click(object sender, EventArgs e)
        {
            logger.Debug("Form1_click");
            this.colist1.Visible = false;

            this.colist2.Visible = false;

            this.colist3.Visible = false;

        }
        private void Form1Closed(object sender, EventArgs e)
        {
            logger.Debug("form1_closed");
            System.Environment.Exit(System.Environment.ExitCode);
            this.Dispose();
            this.Close();
        }
        private object GetTrueValue(int datatype,byte[]mdta)
        {
            object retdata = 0;
            if (datatype == 2) {
                retdata = BitConverter.ToInt32(mdta, 0);
            }
            else {
                retdata = BitConverter.ToSingle(mdta, 0);
            }
            logger.Debug("GetTrueValue---retdata[{}]type[{}]", retdata,retdata.GetType());
            return retdata;
        }
        public void UpdateDevInfo(List<DevTagIndo> tagslist,string UDip)
        {
            logger.Debug("updateDevInfo---ip[{}]", UDip);
            if (DevList.ContainsKey(UDip) == false) {
                return;
            }
          try
          {
                DevInfo Tags = DevList[UDip];                   //DevList:私有成员，记录所有采集机的所有的tag信息,用于显示显示信息，并且用于判断是初次得到的tag信息
                Tags.CuTime = DateTime.Now.ToString();

            List<DataItem> NewTagList = new List<DataItem>();
            bool gotoflag = false;
            foreach (DevTagIndo s in tagslist)
            {
                TagInfo one;
                if (Tags.TagList.ContainsKey(s.ID))
                {
                    one = Tags.TagList[s.ID];
                    
                    one.myvalue = GetTrueValue(one.myvalueType, s.TagMetadata);
                    one.mytime = Tags.CuTime;
                }
                else
                {
                        byte datatype = 1;
                        if (Tags.TagList_LongType.Contains(s.ID)){
                            datatype = 2;
                        }
                        if (Tags.TagListWithID.ContainsKey(s.ID) == false)
                        {
                            logger.Debug("no has this tagid!!");
                            continue;
                        }
                        gotoflag = true;
                        one = new TagInfo
                        {
                            myvalue = GetTrueValue(datatype,s.TagMetadata),
                            myname = Tags.TagListWithID[s.ID],
                            myvalueType = datatype
                        };
                        Tags.TagList.TryAdd(s.ID, one);
                }

                    DataItem Atag = new DataItem(one.myname,s.ID, one.myvalueType, one.myvalue, Tags.CuTime, UDip);

                    NewTagList.Add(Atag);

                SkUpdateTime(one.myname, UDip);
            }

                foreach (KeyValuePair<int, string> s in IpToBoxIndex)
                {
                    logger.Debug("ip,[{}]key[{}]value[{}]", UDip, s.Key, s.Value);
                    if (string.Compare(UDip, s.Value) == 0)
                    {
                        int num = s.Key;
                        allboxs[num + 2].Text = Tags.TagList.Count.ToString();  //更新总tag数
                    }
                }
                ServerDB.OpcDataWriteTag(NewTagList, gotoflag);

            }
            catch (Exception ex)
            {
                logger.Error("error[{}]", ex.ToString());
            }
            logger.Debug("end !!UpdateDevInfo  UDip[{ }]", UDip);

        }
        private List<string> IPs = new List<string>();
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
            ServerDB.OpcDataDelTagWithIp(ip);
            IPs.Remove(ip);
            changeIpdev();

            if (DevList.ContainsKey(ip) == true)
            {
                DevInfo buffer = DevList[ip];
                buffer.TagList.Clear();
                DevList.Remove(ip);
                BindIplist.Remove(ip);
                foreach ( string s  in DevList.Keys) {
                    logger.Debug("sssss[{}]",s);
                }
            }
        }
        public void ConnedNotification(object handle,string ip)
        { 
            logger.Debug("ConnedNotification--ip[{}]", ip);
            DevInfo onedev;

            if (DevList.ContainsKey(ip) == false)
            {
                IPs.Add(ip);
                onedev = new DevInfo(handle);
                DevList.Add(ip, onedev);
                changeIpdev();
                BindIplist.Add(ip);

            }
            else
            {
                logger.Debug("has this ip");
                onedev = DevList[ip];
            }
            /*更新tagtoid 和tagtoname，两个字典*/
            Serverconfig thisxmlfile = new Serverconfig(ip);
            thisxmlfile.ServerConfigParseXml(ref onedev);

            OpcSendFile(handle,ip);
            logger.Debug("ConnedNotification end");
        }
        private void OpcSendFile(object sender,string ip)
        {
            string IsExistPath = filepath + ip + ".xml";
            logger.Debug("OpcSendFile---IsExistPath[{}]!!", IsExistPath);
            SkServer ServerHandle = (SkServer)sender;
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
        }
        private void OpcServerTransferXmlfile(XmlFileWatch inhandle)
        {
            XmlFileWatch bufferxml = inhandle;
            logger.Debug("OpcServerTransferXmlfile!!Count[{}]", bufferxml.ChangFileList.Count);
            foreach (string s in bufferxml.ChangFileList) {
                logger.Debug("s===[{}]",s);
            }
            Queue<string> OpcChangFileList = bufferxml.ChangFileList;
            while (OpcChangFileList.Count > 0)
            {
                string ip = bufferxml.GetandDeltQueue();
                if (DevList.ContainsKey(ip) == true) {
                    DevInfo onedev = DevList[ip];
                    OpcSendFile(onedev.SkClientHandle,ip);
                }
            }
        }

        private  void OpcServerMain()
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
            _ = OpcDBDatas.OpcDateRegisterCB(kleopcserver.ServerRate,this.IPs);
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
            }
            else
            {
                IpToBoxIndex.Add(number * 10,ip);
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
            ConcurrentDictionary<int, TagInfo> Alltag = DevList[ip].TagList;
            if (DevList[ip].TagListWithName.ContainsKey(tagname) == true)
            {
                int thetagid = DevList[ip].TagListWithName[tagname];
                logger.Debug("id[{}]", thetagid);
                if (Alltag.ContainsKey(thetagid) == true) {                  //当前以获取数据有值
                    foreach (KeyValuePair<int, string> s in IpToBoxIndex)
                    {
                        string tagname_temp = allboxs[s.Key + 5].Text;
                        logger.Debug("---------------tbox tagname[{}]----------------------", tagname_temp);

                        if (string.Compare(ip, s.Value) == 0 && string.Compare(tagname_temp, tagname) == 0) {
                            int num = s.Key;
                            logger.Debug("this tag name,num[{}] time is [{}]myvalue[{}]", num,Alltag[thetagid].mytime, Alltag[thetagid].myvalue);
                            allboxs[num + 3].Text = Alltag[thetagid].mytime;
                            allboxs[num + 4].Text = Alltag[thetagid].myvalue.ToString();
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
        void testlist()
        {
            string a1 = "zhasng";
            string a2 = "yong";
            string a3 = "qiang";
            string a4 = "shu";
            string a5 = "yuan";
            BindIplist.Add(a1);
            BindIplist.Add(a2);
            BindIplist.Add(a3);
            BindIplist.Add(a4);
            BindIplist.Add(a5);
        }
        private void Label2_Click(object sender, EventArgs e)
        {

        }
        private void Tag3_TextChanged(object sender, EventArgs e)
        {

        }
        private void tagnameboxchange(string tagname, string ip,int num)
        {
            ConcurrentDictionary<int, TagInfo> Alltag = DevList[ip].TagList;
            if (DevList[ip].TagListWithName.ContainsKey(tagname) == true)
            {
                int ohtagid = DevList[ip].TagListWithName[tagname];
                logger.Debug("-tagnameboxchange-id[{}]", ohtagid);
                if (Alltag.ContainsKey(ohtagid) == true)
                {
                    logger.Debug("tagnameboxchange,num[{}] time is [{}]myvalue[{}]", num, Alltag[ohtagid].mytime, Alltag[ohtagid].myvalue);
                    allboxs[num + 3].Text = Alltag[ohtagid].mytime;
                    allboxs[num + 4].Text = Alltag[ohtagid].myvalue.ToString();
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
            tagnameboxchange(this.tagname1.Text, this.ip1.Text, 10);

        }
        private void Tagname2_TextChanged(object sender, EventArgs e)
        {
            logger.Debug("Tagname2_TextChanged--tagname[{}][{}]", this.tagname2.Text, this.ip2.Text);
            tagnameboxchange(this.tagname2.Text, this.ip2.Text,20);
        }
        private void Tagname3_TextChanged(object sender, EventArgs e)
        {
            logger.Debug("Tagname3_TextChanged--tagname[{}][{}]", this.tagname3.Text, this.ip3.Text);
            tagnameboxchange(this.tagname3.Text, this.ip3.Text, 30);

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
            logger.Debug("Ip3_TextChanged----{}", this.ip3.Text);
            string tt = this.ip3.Text;
            if (SkJudgeIsIpv4(tt, this.tagname3) == true)
            {
                SkUpdateIpToBox(tt, 3);
            }
            else
            {
                logger.Debug("this is invalid ip !!");
            }
        }


        private void Ip1_TextClicked(object sender, EventArgs e)
        {
           // testlist();
            logger.Debug("Ip1_TextClicked--");
            this.colist1.Visible = !this.colist1.Visible;
        }

        private void Ip2_TextClicked(object sender, EventArgs e)
        {
            logger.Debug("Ip2_TextClicked--");
            this.colist2.Visible = !this.colist2.Visible;
        }

        private void Ip3_TextClicked(object sender, EventArgs e)
        {
            logger.Debug("Ip3_TextClicked--");
            this.colist3.Visible = !this.colist3.Visible;
        }
        private void Ip1_mouse_double_Clicked(object sender, MouseEventArgs e)
        {
            logger.Debug("Ip1_mouse_double_Clicked--");
            logger.Debug("Ip1_mouse_double_Clicked  [{}]", this.colist1.SelectedIndex);
            logger.Debug("Ip1_mouse_double_Clicked  [{}]", this.colist1.Text);
            logger.Debug("Ip1_mouse_double_Clicked  SelectedItem[{}]", this.colist1.SelectedItem.ToString());
            logger.Debug("Ip1_mouse_double_Clicked[{}]", e.Button.ToString());
            this.ip1.Text = this.colist1.SelectedItem.ToString();

            this.colist1.Visible = !this.colist1.Visible;
        }

        private void Ip2_mouse_double_Clicked(object sender, EventArgs e)
        {
            logger.Debug("Ip2_mouse_double_Clicked-[{}]-", this.colist2.SelectedItem.ToString());
            this.ip2.Text = this.colist2.SelectedItem.ToString();

            this.colist2.Visible = !this.colist2.Visible;
        }

        private void Ip3_mouse_double_Clicked(object sender, EventArgs e)
        {
            logger.Debug("Ip3_mouse_double_Clicked-[{}]-", this.colist3.SelectedItem.ToString());
            this.ip3.Text = this.colist3.SelectedItem.ToString();

            this.colist3.Visible = !this.colist3.Visible;
        }
        private void reboot_TextClicked(object sender, EventArgs e)
        {
            logger.Debug("reboot_TextClicked--");
            RebootDialog.ShowDialog();
        }
        public void GetRebootIp(object sender, string RebIp)
        {
            logger.Debug("GetRebootIp--RebIp[{}]", RebIp);
            if (DevList.ContainsKey(RebIp) == false)
            {
                MessageBox.Show(this, "没有此设备，请在检查一遍IP!!", "warning!!");
            }
            else
            {
                SkSendRebootInfo(RebIp);
            }
        }
        private void SkSendRebootInfo(string  SendIp)
        {
            logger.Debug("SkSendIPToCo!!ip[{}]", SendIp);
            DevInfo onedev = DevList[SendIp];
            SkServer ServerHandle = (SkServer)onedev.SkClientHandle;
            ServerHandle.CoSendString(SendIp,"reboot!!");
        }
    }
}
