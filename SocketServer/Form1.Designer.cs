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
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("隶书", 42F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(124, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(534, 70);
            this.label1.TabIndex = 0;
            this.label1.Text = "server console";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.label1.Click += new System.EventHandler(this.Label1_Click);
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.CausesValidation = false;
            this.dateTimePicker1.Location = new System.Drawing.Point(632, 413);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(146, 25);
            this.dateTimePicker1.TabIndex = 1;
            // 
            // dev1
            // 
            this.dev1.AutoSize = true;
            this.dev1.Location = new System.Drawing.Point(36, 129);
            this.dev1.Name = "dev1";
            this.dev1.Size = new System.Drawing.Size(60, 15);
            this.dev1.TabIndex = 2;
            this.dev1.Text = "采集机1";
            // 
            // dev2
            // 
            this.dev2.AutoSize = true;
            this.dev2.Location = new System.Drawing.Point(36, 214);
            this.dev2.Name = "dev2";
            this.dev2.Size = new System.Drawing.Size(60, 15);
            this.dev2.TabIndex = 3;
            this.dev2.Text = "采集机2";
            // 
            // dev3
            // 
            this.dev3.AutoSize = true;
            this.dev3.Location = new System.Drawing.Point(36, 296);
            this.dev3.Name = "dev3";
            this.dev3.Size = new System.Drawing.Size(60, 15);
            this.dev3.TabIndex = 4;
            this.dev3.Text = "采集机3";
            this.dev3.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // ip1
            // 
            this.ip1.Location = new System.Drawing.Point(136, 126);
            this.ip1.Name = "ip1";
            this.ip1.Size = new System.Drawing.Size(100, 25);
            this.ip1.TabIndex = 5;
            this.ip1.Text = "0";
            // 
            // ip2
            // 
            this.ip2.Location = new System.Drawing.Point(136, 204);
            this.ip2.Name = "ip2";
            this.ip2.Size = new System.Drawing.Size(100, 25);
            this.ip2.TabIndex = 6;
            this.ip2.Text = "0";
            // 
            // ip3
            // 
            this.ip3.Location = new System.Drawing.Point(136, 286);
            this.ip3.Name = "ip3";
            this.ip3.Size = new System.Drawing.Size(100, 25);
            this.ip3.TabIndex = 7;
            this.ip3.Text = "0";
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
            this.tagtotal.Location = new System.Drawing.Point(346, 91);
            this.tagtotal.Name = "tagtotal";
            this.tagtotal.Size = new System.Drawing.Size(79, 15);
            this.tagtotal.TabIndex = 12;
            this.tagtotal.Text = "tag count";
            // 
            // tag3
            // 
            this.tag3.Location = new System.Drawing.Point(337, 289);
            this.tag3.Name = "tag3";
            this.tag3.Size = new System.Drawing.Size(100, 25);
            this.tag3.TabIndex = 11;
            this.tag3.Text = "0";
            // 
            // tag2
            // 
            this.tag2.Location = new System.Drawing.Point(337, 207);
            this.tag2.Name = "tag2";
            this.tag2.Size = new System.Drawing.Size(100, 25);
            this.tag2.TabIndex = 10;
            this.tag2.Text = "0";
            // 
            // tag1
            // 
            this.tag1.Location = new System.Drawing.Point(337, 129);
            this.tag1.Name = "tag1";
            this.tag1.Size = new System.Drawing.Size(100, 25);
            this.tag1.TabIndex = 9;
            this.tag1.Text = "0";
            // 
            // utime
            // 
            this.utime.AutoSize = true;
            this.utime.Location = new System.Drawing.Point(629, 91);
            this.utime.Name = "utime";
            this.utime.Size = new System.Drawing.Size(67, 15);
            this.utime.TabIndex = 16;
            this.utime.Text = "当前时间";
            // 
            // utime3
            // 
            this.utime3.Location = new System.Drawing.Point(538, 286);
            this.utime3.Name = "utime3";
            this.utime3.Size = new System.Drawing.Size(240, 25);
            this.utime3.TabIndex = 15;
            this.utime3.Text = "0";
            // 
            // utime2
            // 
            this.utime2.Location = new System.Drawing.Point(538, 204);
            this.utime2.Name = "utime2";
            this.utime2.Size = new System.Drawing.Size(240, 25);
            this.utime2.TabIndex = 14;
            this.utime2.Text = "0";
            // 
            // utime1
            // 
            this.utime1.Location = new System.Drawing.Point(538, 126);
            this.utime1.Name = "utime1";
            this.utime1.Size = new System.Drawing.Size(240, 25);
            this.utime1.TabIndex = 13;
            this.utime1.Text = "0";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
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
            this.Name = "Form1";
            this.Text = "大狗熊";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1Closed);
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
    }
}

