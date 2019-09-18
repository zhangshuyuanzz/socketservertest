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
using System.Windows.Forms;

namespace OpcClientForMetering
{
    public partial class Form1 : Form
    {
        readonly NLOG logger = new NLOG("Form1");

        public Form1()
        {
            InitializeComponent();
            //this.tagName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tagName.Width = this.listView1.Width * 7 / 16-10;
            this.tagValue.Width = this.listView1.Width * 4 / 16-10;
            this.tagTime.Width  = this.listView1.Width * 5 / 16-2;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        void OpcClientInsetData(DataItem onett)
        {
            DataItem tt = onett;
            string name = tt.TagName ;
            string CuTime = DateTime.Now.ToString("yyyy-MM-dd HH：mm：ss", DateTimeFormatInfo.InvariantInfo);
            ListViewItem li = new ListViewItem();
            li.Text = name;
            li.SubItems.Add(name);
            li.SubItems.Add("1.50");
            li.SubItems.Add(CuTime);

            listView1.Items.Add(li);
            logger.Debug("OpcClientInsetData");

        }
        private void AddItem_Click(object sender, EventArgs e)
        {
            logger.Debug("AddItem_Click  [{}]",this.inputitem.Text);
            if (this.inputitem.Text == null || this.inputitem.Text.Length == 0) {
                return;
            }
            DataItem tt = new DataItem();
            tt.TagName = this.inputitem.Text;
            OpcClientInsetData(tt);
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
            logger.Debug("Index--[{}]", onetag.Index);
            if (onetag.Index >= 1) {
                this.listView1.Items.RemoveAt(onetag.Index);
            }
        }
        void testinsertdata()
        {

         //   this.listView1.BeginUpdate();

            for (int i = 0; i < 10; i++)
            {
                ListViewItem lvi = new ListViewItem();

                lvi.ImageIndex = i;

                lvi.Text = "subitem" + i;

                lvi.SubItems.Add("第2列,第" + i + "行");

                lvi.SubItems.Add("第3列,第" + i + "行");

                this.listView1.Items.Add(lvi);
            }


            logger.Debug("listView1.Width[{}]", this.listView1.Width);
            logger.Debug("tagName.Width[{}]", this.tagName.Width);
            logger.Debug("tagValue.Width[{}]", this.tagValue.Width);
            logger.Debug("tagTime.Width[{}]", this.tagTime.Width);

        //    this.listView1.EndUpdate();
        }

        private void DelitemBtn_Click(object sender, EventArgs e)
        {
            logger.Debug("DelitemBtn_Click");
            if (this.DelInputItem.Text.Length == 0 | this.DelInputItem.Text == null) {
                return;
            }
            OpcSetFindTagIndex(this.DelInputItem.Text);
        }
    }
}
