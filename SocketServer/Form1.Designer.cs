using System.Collections.Generic;

namespace SocketServer
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private Dictionary<int, System.Windows.Forms.TextBox> allboxs = new Dictionary<int, System.Windows.Forms.TextBox>();
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
            this.textBox11 = new System.Windows.Forms.TextBox();
            this.textBox10 = new System.Windows.Forms.TextBox();
            this.textBox9 = new System.Windows.Forms.TextBox();
            this.textBox8 = new System.Windows.Forms.TextBox();
            this.textBox7 = new System.Windows.Forms.TextBox();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox12 = new System.Windows.Forms.TextBox();
            this.textBox13 = new System.Windows.Forms.TextBox();
            this.textBox14 = new System.Windows.Forms.TextBox();
            this.textBox15 = new System.Windows.Forms.TextBox();
            this.textBox16 = new System.Windows.Forms.TextBox();
            this.textBox17 = new System.Windows.Forms.TextBox();
            this.textBox18 = new System.Windows.Forms.TextBox();
            this.textBox19 = new System.Windows.Forms.TextBox();
            this.textBox20 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();

            // 
            // add allboxs
            // 
            allboxs.Add(1, this.textBox1);
            allboxs.Add(2, this.textBox2);
            allboxs.Add(3, this.textBox3);
            allboxs.Add(4, this.textBox4);
            allboxs.Add(5, this.textBox5);
            allboxs.Add(6, this.textBox6);
            allboxs.Add(7, this.textBox7);
            allboxs.Add(8, this.textBox8);
            allboxs.Add(9, this.textBox9);
            allboxs.Add(10, this.textBox10);
            allboxs.Add(11, this.textBox11);
            allboxs.Add(12, this.textBox12);
            allboxs.Add(13, this.textBox13);
            allboxs.Add(14, this.textBox14);
            allboxs.Add(15, this.textBox15);
            allboxs.Add(16, this.textBox16);
            allboxs.Add(17, this.textBox17);
            allboxs.Add(18, this.textBox18);
            allboxs.Add(19, this.textBox19);
            allboxs.Add(20, this.textBox20);

            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("隶书", 42F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(786, 70);
            this.label1.TabIndex = 0;
            this.label1.Text = "socket server console";
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
            // textBox11
            // 
            this.textBox11.Location = new System.Drawing.Point(518, 82);
            this.textBox11.Name = "textBox11";
            this.textBox11.Size = new System.Drawing.Size(100, 25);
            this.textBox11.TabIndex = 22;
            this.textBox11.Text = "textBox11";
            // 
            // textBox10
            // 
            this.textBox10.Location = new System.Drawing.Point(164, 361);
            this.textBox10.Name = "textBox10";
            this.textBox10.Size = new System.Drawing.Size(100, 25);
            this.textBox10.TabIndex = 21;
            this.textBox10.Text = "textBox10";
            // 
            // textBox9
            // 
            this.textBox9.Location = new System.Drawing.Point(164, 330);
            this.textBox9.Name = "textBox9";
            this.textBox9.Size = new System.Drawing.Size(100, 25);
            this.textBox9.TabIndex = 20;
            this.textBox9.Text = "textBox9";
            // 
            // textBox8
            // 
            this.textBox8.Location = new System.Drawing.Point(164, 299);
            this.textBox8.Name = "textBox8";
            this.textBox8.Size = new System.Drawing.Size(100, 25);
            this.textBox8.TabIndex = 19;
            this.textBox8.Text = "textBox8";
            // 
            // textBox7
            // 
            this.textBox7.Location = new System.Drawing.Point(164, 268);
            this.textBox7.Name = "textBox7";
            this.textBox7.Size = new System.Drawing.Size(100, 25);
            this.textBox7.TabIndex = 18;
            this.textBox7.Text = "textBox7";
            // 
            // textBox6
            // 
            this.textBox6.Location = new System.Drawing.Point(164, 237);
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new System.Drawing.Size(100, 25);
            this.textBox6.TabIndex = 17;
            this.textBox6.Text = "textBox6";
            // 
            // textBox5
            // 
            this.textBox5.Location = new System.Drawing.Point(164, 206);
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(100, 25);
            this.textBox5.TabIndex = 16;
            this.textBox5.Text = "textBox5";
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(164, 175);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(100, 25);
            this.textBox4.TabIndex = 15;
            this.textBox4.Text = "textBox4";
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(164, 144);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(100, 25);
            this.textBox3.TabIndex = 14;
            this.textBox3.Text = "textBox3";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(164, 79);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 25);
            this.textBox1.TabIndex = 13;
            this.textBox1.Text = "textBox1";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(164, 110);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(100, 25);
            this.textBox2.TabIndex = 32;
            this.textBox2.Text = "textBox2";
            // 
            // textBox12
            // 
            this.textBox12.Location = new System.Drawing.Point(518, 113);
            this.textBox12.Name = "textBox12";
            this.textBox12.Size = new System.Drawing.Size(100, 25);
            this.textBox12.TabIndex = 31;
            this.textBox12.Text = "textBox12";
            // 
            // textBox13
            // 
            this.textBox13.Location = new System.Drawing.Point(518, 144);
            this.textBox13.Name = "textBox13";
            this.textBox13.Size = new System.Drawing.Size(100, 25);
            this.textBox13.TabIndex = 30;
            this.textBox13.Text = "textBox13";
            // 
            // textBox14
            // 
            this.textBox14.Location = new System.Drawing.Point(518, 175);
            this.textBox14.Name = "textBox14";
            this.textBox14.Size = new System.Drawing.Size(100, 25);
            this.textBox14.TabIndex = 29;
            this.textBox14.Text = "textBox14";
            // 
            // textBox15
            // 
            this.textBox15.Location = new System.Drawing.Point(518, 206);
            this.textBox15.Name = "textBox15";
            this.textBox15.Size = new System.Drawing.Size(100, 25);
            this.textBox15.TabIndex = 28;
            this.textBox15.Text = "textBox15";
            // 
            // textBox16
            // 
            this.textBox16.Location = new System.Drawing.Point(518, 237);
            this.textBox16.Name = "textBox16";
            this.textBox16.Size = new System.Drawing.Size(100, 25);
            this.textBox16.TabIndex = 27;
            this.textBox16.Text = "textBox16";
            // 
            // textBox17
            // 
            this.textBox17.Location = new System.Drawing.Point(518, 268);
            this.textBox17.Name = "textBox17";
            this.textBox17.Size = new System.Drawing.Size(100, 25);
            this.textBox17.TabIndex = 26;
            this.textBox17.Text = "textBox17";
            // 
            // textBox18
            // 
            this.textBox18.Location = new System.Drawing.Point(518, 299);
            this.textBox18.Name = "textBox18";
            this.textBox18.Size = new System.Drawing.Size(100, 25);
            this.textBox18.TabIndex = 25;
            this.textBox18.Text = "textBox18";
            // 
            // textBox19
            // 
            this.textBox19.Location = new System.Drawing.Point(518, 330);
            this.textBox19.Name = "textBox19";
            this.textBox19.Size = new System.Drawing.Size(100, 25);
            this.textBox19.TabIndex = 24;
            this.textBox19.Text = "textBox19";
            // 
            // textBox20
            // 
            this.textBox20.Location = new System.Drawing.Point(518, 361);
            this.textBox20.Name = "textBox20";
            this.textBox20.Size = new System.Drawing.Size(100, 25);
            this.textBox20.TabIndex = 23;
            this.textBox20.Text = "textBox20";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox12);
            this.Controls.Add(this.textBox13);
            this.Controls.Add(this.textBox14);
            this.Controls.Add(this.textBox15);
            this.Controls.Add(this.textBox16);
            this.Controls.Add(this.textBox17);
            this.Controls.Add(this.textBox18);
            this.Controls.Add(this.textBox19);
            this.Controls.Add(this.textBox20);
            this.Controls.Add(this.textBox11);
            this.Controls.Add(this.textBox10);
            this.Controls.Add(this.textBox9);
            this.Controls.Add(this.textBox8);
            this.Controls.Add(this.textBox7);
            this.Controls.Add(this.textBox6);
            this.Controls.Add(this.textBox5);
            this.Controls.Add(this.textBox4);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "大狗熊";

            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(Form1Closed);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.TextBox textBox11;
        private System.Windows.Forms.TextBox textBox10;
        private System.Windows.Forms.TextBox textBox9;
        private System.Windows.Forms.TextBox textBox8;
        private System.Windows.Forms.TextBox textBox7;
        private System.Windows.Forms.TextBox textBox6;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox12;
        private System.Windows.Forms.TextBox textBox13;
        private System.Windows.Forms.TextBox textBox14;
        private System.Windows.Forms.TextBox textBox15;
        private System.Windows.Forms.TextBox textBox16;
        private System.Windows.Forms.TextBox textBox17;
        private System.Windows.Forms.TextBox textBox18;
        private System.Windows.Forms.TextBox textBox19;
        private System.Windows.Forms.TextBox textBox20;
    }
}

