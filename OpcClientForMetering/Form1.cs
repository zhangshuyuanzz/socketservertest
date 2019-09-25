using Base.kit;
using Common.log;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace OpcClientForMetering
{
    public partial class Form1 : Form
    {
        static public readonly NLOG logger = new NLOG("Form1");
        OpcSetConfig OpcSetCfg ;
        OpcClientMain OpcSetClientH = null;
        // OpcSetOracle OpcSetOracleH;
        OpcSetSocketSver OpcSetSKServer;
        public Form1()
        {
            InitializeComponent();
            this.tagName.Width = this.listView1.Width * 7 / 16-10;
            this.tagValue.Width = this.listView1.Width * 4 / 16-10;
            this.tagTime.Width  = this.listView1.Width * 5 / 16-2;
            OpcSetCfg = new OpcSetConfig();
            OpcSetCfg.OpcSetConfigParseXml();
            //  OpcSetOracleH = new OpcSetOracle("ip","zhang","x","z","AAA");
            OpcSetSKServer = new OpcSetSocketSver(OpcSetCfg);
            this.trueversion.Text = "V." + Application.ProductVersion;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            logger.Debug("Form1_Load");
            OpcSetClientH = new OpcClientMain(OpcSetCfg.OpcHandle);

            Thread scanThread = new Thread(new ThreadStart(OnlyOnceThread));
            scanThread.IsBackground = true;
            scanThread.Start();
        }
        void OnlyOnceThread()
        {
            logger.Debug("OnlyOnceThread");
            if (OpcSetClientH.OClient == null)
            {
                MessageBox.Show("invalid opc server hangdle!!", "warn!!");
                return;
            }
            this.OpcSetClientH.OpcClientMainRead(ref OpcSetCfg.TagListAll);

            var result3 = from v in OpcSetCfg.TagListAll orderby v.Key select v;

            List<string> AllTagList = new List<string>();
          
            this.listView1.BeginUpdate();
            foreach (KeyValuePair<string, DataItem> d in result3)
            {
                DataItem ooo = d.Value; ;
                OpcClientInsetData(ooo);
                AllTagList.Add(d.Key);
            }
            foreach (KeyValuePair<string, DataItem> h in OpcSetCfg.TagBannerList)
            {
                DataItem ooo = h.Value; ;
                AllTagList.Add(h.Key);
            }
            logger.Debug("Count[{}]", AllTagList.Count);
            this.listView1.EndUpdate();
            string[] mmmsf = AllTagList.ToArray();

            this.OpcSetClientH.OpcSetTagChanged += new SkKit.kit.TagEventHandler(OpcClientChangeTag);
            this.OpcSetClientH.OpcClientMainSubscription(mmmsf);
        }
        void OpcClientChangeTag(List<DataItem>Taglist)
        {
            logger.Debug("get opc server Taglist---count[{}]", Taglist.Count);
            foreach (DataItem tl in Taglist)
            {
                if (this.OpcSetCfg.TagBannerList.ContainsKey(tl.TagName) == true)
                {
                    logger.Debug("this is oracle tag!!TagName[{}]", tl.TagName);
                    DataItem oracleone = this.OpcSetCfg.TagBannerList[tl.TagName];
                    oracleone.Value = tl.Value;
                    oracleone.DataTime = tl.DataTime;
                    oracleone.Active = true;
                }
                else if (this.OpcSetCfg.TagListAll.ContainsKey(tl.TagName) == true)
                {
                    logger.Debug("TagName[{}]", tl.TagName);
                    DataItem utimeone = this.OpcSetCfg.TagListAll[tl.TagName];
                    utimeone.Value = tl.Value;
                    utimeone.DataTime = tl.DataTime;
                    utimeone.Active = true;

                    ListViewItem listview = this.listView1.FindItemWithText(tl.TagName);
                    if (listview != null)
                    {
                        logger.Debug("Indexe [{}]", listview.Index);
                        this.listView1.Items[listview.Index].SubItems[2].Text = tl.Value.ToString();
                        this.listView1.Items[listview.Index].SubItems[3].Text = tl.DataTime;
                        logger.Debug("opc client name[{}] value[{}] time [{}]", tl.TagName, tl.Value, tl.DataTime);
                    }
                    else
                    {
                        logger.Debug("listview find none");
                    }
                }
                else {
                    logger.Debug("unkown tag name");
                }

            }
        }
        private void Form1Closed(object sender, EventArgs e)
        {
            logger.Debug("form1_closed");
            System.Environment.Exit(System.Environment.ExitCode);
            this.Dispose();
            this.Close();
        }
        void OpcClientInsetData(DataItem onett)
        {
            ListViewItem li = new ListViewItem();
            li.Text = onett.TagName;
            li.SubItems.Add(onett.TagName);
            li.SubItems.Add(onett.Value.ToString());
            li.SubItems.Add(onett.DataTime);

            this.listView1.Items.Add(li);
            logger.Debug("OpcClient  display window---name[{}]Value[{}]", onett.TagName, onett.Value);

        }
        private void AddItem_Click(object sender, EventArgs e)
        {
            logger.Debug("AddItem_Click  [{}]",this.inputitem.Text);
            if (this.inputitem.Text == null || this.inputitem.Text.Length == 0) {
                return;
            }
            DataItem tt = new DataItem();
            tt.TagName = this.inputitem.Text;
            tt.Value = "-";
            if (OpcSetCfg.OpcAddIntoTagList(tt) == true)    //更新内存
            {
                this.OpcSetClientH.OpcClientMainReadOneTag(ref tt);
                logger.Debug("now insert taglist now!!");
                OpcClientInsetData(tt);    //更新界面

                string[] onetag = { tt.TagName };
                this.OpcSetClientH.OpcClientMainSubscription(onetag);

            }
            else
            {
                logger.Debug("now have this tag,return now!!");
            }
            //  testinsertdata();
        }

        private void Inputitem_TextChanged(object sender, EventArgs e)
        {

        }
        private void ListView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            logger.Debug("ListView1_MouseDoubleClick");
            if (this.listView1.SelectedIndices.Count == 0)
            {
                logger.Debug("none select item");
                return;
            }
            try
            {
                ListViewItem sectag =  this.listView1.SelectedItems[0];
                logger.Debug("-index[{}]--Text[{}]", this.listView1.SelectedIndices[0], sectag.Text);
                this.DelInputItem.Text = sectag.Text;

            }
            catch (Exception ex)
            {
                logger.Debug("error [{}]", ex.ToString());
            }
        }
        private void ListView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.listView1.SelectedIndices.Count == 0) {
                logger.Debug("none select item");
                return;
            }
            try
            {
                logger.Debug("-index[{}]--", this.listView1.SelectedIndices[0]);
            }
            catch (Exception ex)
            {
                logger.Debug("error [{}]",ex.ToString());
            }

        }
        void OpcSetFindTagIndex(string tagname)
        {
            logger.Debug("OpcSetFindTagIndex--[{}]",tagname);
            ListViewItem onetag = this.listView1.FindItemWithText(tagname);
            if (onetag == null) {
                MessageBox.Show("无此条目，请再次检查！！","WARN", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                return;
            }
            logger.Debug("Index--[{}]", onetag.Index);
            if (onetag.Index >= 1) {
                this.listView1.Items.RemoveAt(onetag.Index);
            }
        }
        private void DelitemBtn_Click(object sender, EventArgs e)
        {
            try
            {
                logger.Debug("DelitemBtn_Click");
                if (this.DelInputItem.Text.Length == 0 | this.DelInputItem.Text == null)
                {
                    return;
                }
                OpcSetFindTagIndex(this.DelInputItem.Text);
            }
            catch (Exception ex)
            {
                logger.Debug("error insert [{}]",ex.ToString());
            }
        }

        private void ListView1_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }
    }
}
