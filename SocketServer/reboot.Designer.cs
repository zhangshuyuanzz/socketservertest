namespace SocketServer
{
    partial class reboot
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(reboot));
            this.rebootbtn1 = new System.Windows.Forms.Button();
            this.warning = new System.Windows.Forms.Label();
            this.warinig2 = new System.Windows.Forms.Label();
            this.rebootip = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // rebootbtn1
            // 
            this.rebootbtn1.BackColor = System.Drawing.Color.Red;
            this.rebootbtn1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.rebootbtn1.FlatAppearance.BorderColor = System.Drawing.Color.Red;
            this.rebootbtn1.Font = new System.Drawing.Font("微软雅黑", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.rebootbtn1.Location = new System.Drawing.Point(198, 156);
            this.rebootbtn1.Name = "rebootbtn1";
            this.rebootbtn1.Size = new System.Drawing.Size(251, 64);
            this.rebootbtn1.TabIndex = 0;
            this.rebootbtn1.Text = "确认";
            this.rebootbtn1.UseVisualStyleBackColor = false;
            this.rebootbtn1.Click += new System.EventHandler(this.RebootConfirmHandler);
            // 
            // warning
            // 
            this.warning.AutoSize = true;
            this.warning.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.warning.ForeColor = System.Drawing.Color.Blue;
            this.warning.Location = new System.Drawing.Point(122, 18);
            this.warning.Name = "warning";
            this.warning.Size = new System.Drawing.Size(448, 30);
            this.warning.TabIndex = 1;
            this.warning.Text = "请在下方输入欲重启的设备IP！";
            // 
            // warinig2
            // 
            this.warinig2.AutoSize = true;
            this.warinig2.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.warinig2.ForeColor = System.Drawing.Color.Blue;
            this.warinig2.Location = new System.Drawing.Point(174, 113);
            this.warinig2.Name = "warinig2";
            this.warinig2.Size = new System.Drawing.Size(323, 30);
            this.warinig2.TabIndex = 2;
            this.warinig2.Text = "并点击【确认】按钮！";
            // 
            // rebootip
            // 
            this.rebootip.Location = new System.Drawing.Point(179, 60);
            this.rebootip.Name = "rebootip";
            this.rebootip.Size = new System.Drawing.Size(297, 25);
            this.rebootip.TabIndex = 3;
            this.rebootip.TextChanged += new System.EventHandler(RebootEventHandler);
            // 
            // reboot
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(701, 222);
            this.Controls.Add(this.rebootip);
            this.Controls.Add(this.warinig2);
            this.Controls.Add(this.warning);
            this.Controls.Add(this.rebootbtn1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "reboot";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "reboot";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.RebootFormClosingEventHandler);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button rebootbtn1;
        private System.Windows.Forms.Label warning;
        private System.Windows.Forms.Label warinig2;
        private System.Windows.Forms.TextBox rebootip;
    }
}