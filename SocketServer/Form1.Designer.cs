using System.Collections.Generic;

namespace SocketServer
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.label1 = new System.Windows.Forms.Label();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.dev1 = new System.Windows.Forms.Label();
            this.dev2 = new System.Windows.Forms.Label();
            this.dev3 = new System.Windows.Forms.Label();
            this.ip1 = new System.Windows.Forms.TextBox();
            this.ip2 = new System.Windows.Forms.TextBox();
            this.ip3 = new System.Windows.Forms.TextBox();
            this.ip = new System.Windows.Forms.Label();
            this.tagtotal = new System.Windows.Forms.Label();
            this.tag3 = new System.Windows.Forms.TextBox();
            this.tag2 = new System.Windows.Forms.TextBox();
            this.tag1 = new System.Windows.Forms.TextBox();
            this.utime = new System.Windows.Forms.Label();
            this.utime3 = new System.Windows.Forms.TextBox();
            this.utime2 = new System.Windows.Forms.TextBox();
            this.utime1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.tag_name = new System.Windows.Forms.Label();
            this.tagname3 = new System.Windows.Forms.TextBox();
            this.tagname2 = new System.Windows.Forms.TextBox();
            this.tagname1 = new System.Windows.Forms.TextBox();
            this.conndevt = new System.Windows.Forms.TextBox();
            this.conndev = new System.Windows.Forms.Label();
            this.dev3IDT = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.dev2IDT = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dev1Id = new System.Windows.Forms.Label();
            this.dev1IDT = new System.Windows.Forms.TextBox();
            this.colist1 = new System.Windows.Forms.ListBox();
            this.colist2 = new System.Windows.Forms.ListBox();
            this.colist3 = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Lucida Handwriting", 42F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(92, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(594, 91);
            this.label1.TabIndex = 0;
            this.label1.Text = "server console";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.label1.Click += new System.EventHandler(this.Label1_Click);
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.CausesValidation = false;
            this.dateTimePicker1.Location = new System.Drawing.Point(632, 462);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(156, 25);
            this.dateTimePicker1.TabIndex = 1;
            // 
            // dev1
            // 
            this.dev1.AutoSize = true;
            this.dev1.Font = new System.Drawing.Font("隶书", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dev1.Location = new System.Drawing.Point(36, 129);
            this.dev1.Name = "dev1";
            this.dev1.Size = new System.Drawing.Size(83, 20);
            this.dev1.TabIndex = 2;
            this.dev1.Text = "采集机1";
            // 
            // dev2
            // 
            this.dev2.AutoSize = true;
            this.dev2.Font = new System.Drawing.Font("隶书", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dev2.Location = new System.Drawing.Point(36, 235);
            this.dev2.Name = "dev2";
            this.dev2.Size = new System.Drawing.Size(83, 20);
            this.dev2.TabIndex = 3;
            this.dev2.Text = "采集机2";
            // 
            // dev3
            // 
            this.dev3.AutoSize = true;
            this.dev3.Font = new System.Drawing.Font("隶书", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dev3.Location = new System.Drawing.Point(36, 334);
            this.dev3.Name = "dev3";
            this.dev3.Size = new System.Drawing.Size(83, 20);
            this.dev3.TabIndex = 4;
            this.dev3.Text = "采集机3";
            this.dev3.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // ip1
            // 
            this.ip1.Location = new System.Drawing.Point(136, 126);
            this.ip1.Name = "ip1";
            this.ip1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.ip1.Size = new System.Drawing.Size(122, 25);
            this.ip1.TabIndex = 5;
            this.ip1.Text = "0";
            this.ip1.Click += new System.EventHandler(this.Ip1_TextClicked);
            this.ip1.TextChanged += new System.EventHandler(this.Ip1_TextChanged);
            // 
            // ip2
            // 
            this.ip2.Location = new System.Drawing.Point(136, 230);
            this.ip2.Name = "ip2";
            this.ip2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.ip2.Size = new System.Drawing.Size(122, 25);
            this.ip2.TabIndex = 6;
            this.ip2.Text = "0";
            this.ip2.Click += new System.EventHandler(this.Ip2_TextClicked);
            this.ip2.TextChanged += new System.EventHandler(this.Ip2_TextChanged);
            // 
            // ip3
            // 
            this.ip3.Location = new System.Drawing.Point(136, 329);
            this.ip3.Name = "ip3";
            this.ip3.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.ip3.Size = new System.Drawing.Size(122, 25);
            this.ip3.TabIndex = 7;
            this.ip3.Text = "0";
            this.ip3.Click += new System.EventHandler(this.Ip3_TextClicked);
            this.ip3.TextChanged += new System.EventHandler(this.Ip3_TextChanged);
            // 
            // ip
            // 
            this.ip.AutoSize = true;
            this.ip.Location = new System.Drawing.Point(178, 91);
            this.ip.Name = "ip";
            this.ip.Size = new System.Drawing.Size(23, 15);
            this.ip.TabIndex = 8;
            this.ip.Text = "IP";
            // 
            // tagtotal
            // 
            this.tagtotal.AutoSize = true;
            this.tagtotal.Location = new System.Drawing.Point(281, 91);
            this.tagtotal.Name = "tagtotal";
            this.tagtotal.Size = new System.Drawing.Size(79, 15);
            this.tagtotal.TabIndex = 12;
            this.tagtotal.Text = "tag count";
            // 
            // tag3
            // 
            this.tag3.Location = new System.Drawing.Point(274, 329);
            this.tag3.Name = "tag3";
            this.tag3.ReadOnly = true;
            this.tag3.Size = new System.Drawing.Size(67, 25);
            this.tag3.TabIndex = 11;
            this.tag3.Text = "0";
            this.tag3.TextChanged += new System.EventHandler(this.Tag3_TextChanged);
            // 
            // tag2
            // 
            this.tag2.Location = new System.Drawing.Point(274, 230);
            this.tag2.Name = "tag2";
            this.tag2.ReadOnly = true;
            this.tag2.Size = new System.Drawing.Size(67, 25);
            this.tag2.TabIndex = 10;
            this.tag2.Text = "0";
            // 
            // tag1
            // 
            this.tag1.Location = new System.Drawing.Point(274, 126);
            this.tag1.Name = "tag1";
            this.tag1.ReadOnly = true;
            this.tag1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.tag1.Size = new System.Drawing.Size(67, 25);
            this.tag1.TabIndex = 9;
            this.tag1.Text = "0";
            this.tag1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // utime
            // 
            this.utime.AutoSize = true;
            this.utime.Location = new System.Drawing.Point(629, 91);
            this.utime.Name = "utime";
            this.utime.Size = new System.Drawing.Size(67, 15);
            this.utime.TabIndex = 16;
            this.utime.Text = "更新时间";
            // 
            // utime3
            // 
            this.utime3.Location = new System.Drawing.Point(572, 329);
            this.utime3.Name = "utime3";
            this.utime3.ReadOnly = true;
            this.utime3.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.utime3.Size = new System.Drawing.Size(206, 25);
            this.utime3.TabIndex = 15;
            this.utime3.Text = "---";
            this.utime3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // utime2
            // 
            this.utime2.Location = new System.Drawing.Point(572, 230);
            this.utime2.Name = "utime2";
            this.utime2.ReadOnly = true;
            this.utime2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.utime2.Size = new System.Drawing.Size(206, 25);
            this.utime2.TabIndex = 14;
            this.utime2.Text = "---";
            this.utime2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // utime1
            // 
            this.utime1.Location = new System.Drawing.Point(572, 124);
            this.utime1.Name = "utime1";
            this.utime1.ReadOnly = true;
            this.utime1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.utime1.Size = new System.Drawing.Size(206, 25);
            this.utime1.TabIndex = 13;
            this.utime1.Text = "---";
            this.utime1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBox2
            // 
            this.textBox2.AllowDrop = true;
            this.textBox2.BackColor = System.Drawing.Color.Black;
            this.textBox2.Location = new System.Drawing.Point(1, 205);
            this.textBox2.MinimumSize = new System.Drawing.Size(4, 4);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(800, 4);
            this.textBox2.TabIndex = 19;
            // 
            // textBox1
            // 
            this.textBox1.AllowDrop = true;
            this.textBox1.BackColor = System.Drawing.SystemColors.InfoText;
            this.textBox1.Location = new System.Drawing.Point(0, 316);
            this.textBox1.MinimumSize = new System.Drawing.Size(4, 4);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(800, 4);
            this.textBox1.TabIndex = 20;
            // 
            // tag_name
            // 
            this.tag_name.AutoSize = true;
            this.tag_name.Location = new System.Drawing.Point(443, 91);
            this.tag_name.Name = "tag_name";
            this.tag_name.Size = new System.Drawing.Size(37, 15);
            this.tag_name.TabIndex = 28;
            this.tag_name.Text = "位号";
            // 
            // tagname3
            // 
            this.tagname3.Location = new System.Drawing.Point(373, 329);
            this.tagname3.Name = "tagname3";
            this.tagname3.ReadOnly = true;
            this.tagname3.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.tagname3.Size = new System.Drawing.Size(169, 25);
            this.tagname3.TabIndex = 27;
            this.tagname3.Text = "0";
            this.tagname3.TextChanged += new System.EventHandler(this.Tagname3_TextChanged);
            // 
            // tagname2
            // 
            this.tagname2.Location = new System.Drawing.Point(373, 230);
            this.tagname2.Name = "tagname2";
            this.tagname2.ReadOnly = true;
            this.tagname2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.tagname2.Size = new System.Drawing.Size(169, 25);
            this.tagname2.TabIndex = 26;
            this.tagname2.Text = "0";
            this.tagname2.TextChanged += new System.EventHandler(this.Tagname2_TextChanged);
            // 
            // tagname1
            // 
            this.tagname1.Location = new System.Drawing.Point(373, 126);
            this.tagname1.Name = "tagname1";
            this.tagname1.ReadOnly = true;
            this.tagname1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.tagname1.Size = new System.Drawing.Size(169, 25);
            this.tagname1.TabIndex = 25;
            this.tagname1.Text = "0";
            this.tagname1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tagname1.TextChanged += new System.EventHandler(this.Tagname1_TextChanged);
            // 
            // conndevt
            // 
            this.conndevt.Location = new System.Drawing.Point(12, 431);
            this.conndevt.Name = "conndevt";
            this.conndevt.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.conndevt.Size = new System.Drawing.Size(778, 25);
            this.conndevt.TabIndex = 29;
            this.conndevt.Text = "0";
            // 
            // conndev
            // 
            this.conndev.AutoSize = true;
            this.conndev.Font = new System.Drawing.Font("幼圆", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.conndev.ForeColor = System.Drawing.Color.Green;
            this.conndev.Location = new System.Drawing.Point(12, 407);
            this.conndev.Name = "conndev";
            this.conndev.Size = new System.Drawing.Size(139, 19);
            this.conndev.TabIndex = 30;
            this.conndev.Text = "已连接设备---";
            // 
            // dev3IDT
            // 
            this.dev3IDT.Location = new System.Drawing.Point(609, 381);
            this.dev3IDT.Name = "dev3IDT";
            this.dev3IDT.ReadOnly = true;
            this.dev3IDT.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.dev3IDT.Size = new System.Drawing.Size(169, 25);
            this.dev3IDT.TabIndex = 23;
            this.dev3IDT.Text = "0";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.ForeColor = System.Drawing.Color.Blue;
            this.label3.Location = new System.Drawing.Point(486, 381);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(112, 27);
            this.label3.TabIndex = 24;
            this.label3.Text = "当前位号值";
            // 
            // dev2IDT
            // 
            this.dev2IDT.Location = new System.Drawing.Point(609, 272);
            this.dev2IDT.Name = "dev2IDT";
            this.dev2IDT.ReadOnly = true;
            this.dev2IDT.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.dev2IDT.Size = new System.Drawing.Size(169, 25);
            this.dev2IDT.TabIndex = 21;
            this.dev2IDT.Text = "0";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.ForeColor = System.Drawing.Color.Blue;
            this.label2.Location = new System.Drawing.Point(486, 272);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(112, 27);
            this.label2.TabIndex = 22;
            this.label2.Text = "当前位号值";
            // 
            // dev1Id
            // 
            this.dev1Id.AutoSize = true;
            this.dev1Id.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dev1Id.ForeColor = System.Drawing.Color.Blue;
            this.dev1Id.Location = new System.Drawing.Point(486, 174);
            this.dev1Id.Name = "dev1Id";
            this.dev1Id.Size = new System.Drawing.Size(112, 27);
            this.dev1Id.TabIndex = 18;
            this.dev1Id.Text = "当前位号值";
            this.dev1Id.Click += new System.EventHandler(this.Label2_Click);
            // 
            // dev1IDT
            // 
            this.dev1IDT.Location = new System.Drawing.Point(609, 174);
            this.dev1IDT.Name = "dev1IDT";
            this.dev1IDT.ReadOnly = true;
            this.dev1IDT.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.dev1IDT.Size = new System.Drawing.Size(169, 25);
            this.dev1IDT.TabIndex = 17;
            this.dev1IDT.Text = "0";
            // 
            // colist1
            // 
            this.colist1.FormattingEnabled = true;
            this.colist1.ItemHeight = 15;
            this.colist1.Location = new System.Drawing.Point(136, 157);
            this.colist1.Name = "colist1";
            this.colist1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.colist1.ScrollAlwaysVisible = true;
            this.colist1.Size = new System.Drawing.Size(120, 109);
            this.colist1.TabIndex = 31;
            this.colist1.Visible = false;
            this.colist1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(Ip1_mouse_double_Clicked);
            // 
            // colist2
            // 
            this.colist2.FormattingEnabled = true;
            this.colist2.ItemHeight = 15;
            this.colist2.Location = new System.Drawing.Point(136, 261);
            this.colist2.Name = "colist2";
            this.colist2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.colist2.ScrollAlwaysVisible = true;
            this.colist2.Size = new System.Drawing.Size(120, 109);
            this.colist2.TabIndex = 32;
            this.colist2.Visible = false;
            this.colist2.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(Ip2_mouse_double_Clicked);
            // 
            // colist3
            // 
            this.colist3.FormattingEnabled = true;
            this.colist3.ItemHeight = 15;
            this.colist3.Location = new System.Drawing.Point(136, 360);
            this.colist3.Name = "colist3";
            this.colist3.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.colist3.ScrollAlwaysVisible = true;
            this.colist3.Size = new System.Drawing.Size(120, 109);
            this.colist3.TabIndex = 33;
            this.colist3.Visible = false;
            this.colist3.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(Ip3_mouse_double_Clicked);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 499);
            this.Controls.Add(this.colist3);
            this.Controls.Add(this.colist2);
            this.Controls.Add(this.colist1);
            this.Controls.Add(this.conndev);
            this.Controls.Add(this.conndevt);
            this.Controls.Add(this.tag_name);
            this.Controls.Add(this.tagname3);
            this.Controls.Add(this.tagname2);
            this.Controls.Add(this.tagname1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dev3IDT);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dev2IDT);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.dev1Id);
            this.Controls.Add(this.dev1IDT);
            this.Controls.Add(this.utime);
            this.Controls.Add(this.utime3);
            this.Controls.Add(this.utime2);
            this.Controls.Add(this.utime1);
            this.Controls.Add(this.tagtotal);
            this.Controls.Add(this.tag3);
            this.Controls.Add(this.tag2);
            this.Controls.Add(this.tag1);
            this.Controls.Add(this.ip);
            this.Controls.Add(this.ip3);
            this.Controls.Add(this.ip2);
            this.Controls.Add(this.ip1);
            this.Controls.Add(this.dev3);
            this.Controls.Add(this.dev2);
            this.Controls.Add(this.dev1);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Text = "琪朗(dl)-科技";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1Closed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Click += new System.EventHandler(this.Form1_click);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Label dev1;
        private System.Windows.Forms.Label dev2;
        private System.Windows.Forms.Label dev3;
        private System.Windows.Forms.TextBox ip1;
        private System.Windows.Forms.TextBox ip2;
        private System.Windows.Forms.TextBox ip3;
        private System.Windows.Forms.Label ip;
        private System.Windows.Forms.Label tagtotal;
        private System.Windows.Forms.TextBox tag3;
        private System.Windows.Forms.TextBox tag2;
        private System.Windows.Forms.TextBox tag1;
        private System.Windows.Forms.Label utime;
        private System.Windows.Forms.TextBox utime3;
        private System.Windows.Forms.TextBox utime2;
        private System.Windows.Forms.TextBox utime1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label tag_name;
        private System.Windows.Forms.TextBox tagname3;
        private System.Windows.Forms.TextBox tagname2;
        private System.Windows.Forms.TextBox tagname1;
        private System.Windows.Forms.TextBox conndevt;
        private System.Windows.Forms.Label conndev;
        private System.Windows.Forms.TextBox dev3IDT;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox dev2IDT;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label dev1Id;
        private System.Windows.Forms.TextBox dev1IDT;
        private System.Windows.Forms.ListBox colist1;
        private System.Windows.Forms.ListBox colist2;
        private System.Windows.Forms.ListBox colist3;
    }
}

