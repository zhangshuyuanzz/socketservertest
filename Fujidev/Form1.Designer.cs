namespace Fujidev
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
            this.components = new System.ComponentModel.Container();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.addrt = new System.Windows.Forms.Label();
            this.addrin = new System.Windows.Forms.TextBox();
            this.framein = new System.Windows.Forms.TextBox();
            this.framet = new System.Windows.Forms.Label();
            this.currentin = new System.Windows.Forms.TextBox();
            this.currentt = new System.Windows.Forms.Label();
            this.totalin = new System.Windows.Forms.TextBox();
            this.totalt = new System.Windows.Forms.Label();
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.serialin = new System.Windows.Forms.TextBox();
            this.serialt = new System.Windows.Forms.Label();
            this.but = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.serialcert = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 22.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(305, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(115, 38);
            this.label1.TabIndex = 0;
            this.label1.Text = " 测试";
            // 
            // addrt
            // 
            this.addrt.AutoSize = true;
            this.addrt.Font = new System.Drawing.Font("隶书", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.addrt.ForeColor = System.Drawing.Color.Green;
            this.addrt.Location = new System.Drawing.Point(98, 91);
            this.addrt.Name = "addrt";
            this.addrt.Size = new System.Drawing.Size(85, 24);
            this.addrt.TabIndex = 1;
            this.addrt.Text = "地址：";
            // 
            // addrin
            // 
            this.addrin.Location = new System.Drawing.Point(182, 91);
            this.addrin.Name = "addrin";
            this.addrin.Size = new System.Drawing.Size(100, 25);
            this.addrin.TabIndex = 2;
            this.addrin.Text = "0";
            this.addrin.TextChanged += new System.EventHandler(this.Dev_addr_in);
            // 
            // framein
            // 
            this.framein.Location = new System.Drawing.Point(182, 154);
            this.framein.Name = "framein";
            this.framein.Size = new System.Drawing.Size(555, 25);
            this.framein.TabIndex = 4;
            this.framein.Text = "0";
            // 
            // framet
            // 
            this.framet.AutoSize = true;
            this.framet.Font = new System.Drawing.Font("隶书", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.framet.ForeColor = System.Drawing.Color.Green;
            this.framet.Location = new System.Drawing.Point(74, 154);
            this.framet.Name = "framet";
            this.framet.Size = new System.Drawing.Size(110, 24);
            this.framet.TabIndex = 3;
            this.framet.Text = "发送帧：";
            // 
            // currentin
            // 
            this.currentin.Location = new System.Drawing.Point(182, 323);
            this.currentin.Name = "currentin";
            this.currentin.Size = new System.Drawing.Size(555, 25);
            this.currentin.TabIndex = 6;
            this.currentin.Text = "0";
            // 
            // currentt
            // 
            this.currentt.AutoSize = true;
            this.currentt.Font = new System.Drawing.Font("隶书", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.currentt.ForeColor = System.Drawing.Color.Blue;
            this.currentt.Location = new System.Drawing.Point(74, 323);
            this.currentt.Name = "currentt";
            this.currentt.Size = new System.Drawing.Size(110, 24);
            this.currentt.TabIndex = 5;
            this.currentt.Text = "瞬时量：";
            // 
            // totalin
            // 
            this.totalin.Location = new System.Drawing.Point(182, 391);
            this.totalin.Name = "totalin";
            this.totalin.Size = new System.Drawing.Size(555, 25);
            this.totalin.TabIndex = 8;
            this.totalin.Text = "0";
            // 
            // totalt
            // 
            this.totalt.AutoSize = true;
            this.totalt.Font = new System.Drawing.Font("隶书", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.totalt.ForeColor = System.Drawing.Color.Blue;
            this.totalt.Location = new System.Drawing.Point(74, 391);
            this.totalt.Name = "totalt";
            this.totalt.Size = new System.Drawing.Size(110, 24);
            this.totalt.TabIndex = 7;
            this.totalt.Text = "发送帧：";
            // 
            // serialin
            // 
            this.serialin.Location = new System.Drawing.Point(419, 91);
            this.serialin.Name = "serialin";
            this.serialin.Size = new System.Drawing.Size(100, 25);
            this.serialin.TabIndex = 10;
            this.serialin.Text = "0";
            this.serialin.TextChanged += new System.EventHandler(Dev_serial_in);
            // 
            // serialt
            // 
            this.serialt.AutoSize = true;
            this.serialt.Font = new System.Drawing.Font("隶书", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.serialt.ForeColor = System.Drawing.Color.Green;
            this.serialt.Location = new System.Drawing.Point(335, 91);
            this.serialt.Name = "serialt";
            this.serialt.Size = new System.Drawing.Size(85, 24);
            this.serialt.TabIndex = 9;
            this.serialt.Text = "串口：";
            // 
            // but
            // 
            this.but.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.but.AutoSize = true;
            this.but.Font = new System.Drawing.Font("幼圆", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.but.Location = new System.Drawing.Point(561, 197);
            this.but.Name = "but";
            this.but.Size = new System.Drawing.Size(206, 64);
            this.but.TabIndex = 11;
            this.but.Text = "发送确认";
            this.but.UseVisualStyleBackColor = true;
            this.but.Click += new System.EventHandler(this.But_Click);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label2.Location = new System.Drawing.Point(-11, 293);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(799, 2);
            this.label2.TabIndex = 12;
            this.label2.Text = "label2";
            // 
            // serialcert
            // 
            this.serialcert.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.serialcert.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.serialcert.ForeColor = System.Drawing.Color.DarkMagenta;
            this.serialcert.Location = new System.Drawing.Point(537, 82);
            this.serialcert.Name = "serialcert";
            this.serialcert.Size = new System.Drawing.Size(176, 46);
            this.serialcert.TabIndex = 13;
            this.serialcert.Text = "串口确认";
            this.serialcert.UseVisualStyleBackColor = false;
            this.serialcert.Click += new System.EventHandler(this.Button1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.serialcert);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.but);
            this.Controls.Add(this.serialin);
            this.Controls.Add(this.serialt);
            this.Controls.Add(this.totalin);
            this.Controls.Add(this.totalt);
            this.Controls.Add(this.currentin);
            this.Controls.Add(this.currentt);
            this.Controls.Add(this.framein);
            this.Controls.Add(this.framet);
            this.Controls.Add(this.addrin);
            this.Controls.Add(this.addrt);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "fuji test";

            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(Form1Closed);

            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label addrt;
        private System.Windows.Forms.TextBox addrin;
        private System.Windows.Forms.TextBox framein;
        private System.Windows.Forms.Label framet;
        private System.Windows.Forms.TextBox currentin;
        private System.Windows.Forms.Label currentt;
        private System.Windows.Forms.TextBox totalin;
        private System.Windows.Forms.Label totalt;
        private System.IO.Ports.SerialPort serialPort1;
        private System.Windows.Forms.TextBox serialin;
        private System.Windows.Forms.Label serialt;
        private System.Windows.Forms.Button but;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button serialcert;
    }
}

