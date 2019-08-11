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
        public Form1()
        {
            InitializeComponent();
            string[] inin = { "ssss","dddd","qqqq"};
            this.list.Items.Add("ssss");
            this.list.Items.Add("ggggggg");
            this.list.Items.Add("eeeeeeee");

            this.list.Items.Add("uuuuu");


        }
        private void list1_selected(object sender, EventArgs e)
        {
            int a = this.list.SelectedIndex;
        }
    }
}
