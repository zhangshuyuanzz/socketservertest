using Base.kit;
using Common.log;
using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KJtest
{
    public partial class Form1 : Form
    {
        readonly NLOG logger = new NLOG("Form1");
        private SQLiteHelper TsServerDBHandle = null;

        public Form1()
        {
            InitializeComponent();
            CreateDb();
        }
        private void list1_selected(object sender, EventArgs e)
        {
            int a = this.list.SelectedIndex;
        }

        private void List_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private string DbPath = @".\DB\OpcServer.db";
        void CreateDb()
        {
            this.TsServerDBHandle = new SQLiteHelper(DbPath);
            this.TsServerDBHandle.Open();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void Certern_Click(object sender, EventArgs e)
        {
            logger.Debug("Certern_Click--[{}]-", this.tcount.Text);
            int num = int.Parse(this.tcount.Text) ;
            List<DataItem> insda = new List<DataItem>();
            int cunum = 1;
            while (num != 0) {

                DataItem oo = new DataItem("zz"+ cunum.ToString(), cunum, 0,0,"000","123");
                insda.Add(oo);
                num--;
                cunum++;
            }
            logger.Debug("srtart insert dbdata--sss-");
            this.TsServerDBHandle.Replace("opc_tag", insda);
            logger.Debug("srtart insert dbdata--eee-");
        }

        private void Del_Click(object sender, EventArgs e)
        {
            logger.Debug("Del_Click---sss");
            this.TsServerDBHandle.DeleteAll("opc_tag", "123");
            logger.Debug("Del_Click---eee");

        }

        private void Read_Click(object sender, EventArgs e)
        {
            logger.Debug("Read_Click---sss");
            string insql = "select * from opc_tag where devip in ( {0} )";
            List<string> paramArr = new List<string>();
            string aa = "123";
            paramArr.Add(aa);
            List<object> list = this.TsServerDBHandle.ExecuteRow(insql, paramArr);
            logger.Debug("Read_Click---eee--Count[{}]", list.Count);

        }
    }
}
