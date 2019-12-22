namespace KJtest
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
            this.list = new System.Windows.Forms.ListBox();
            this.certern = new System.Windows.Forms.Button();
            this.tcount = new System.Windows.Forms.TextBox();
            this.del = new System.Windows.Forms.Button();
            this.read = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // list
            // 
            this.list.FormattingEnabled = true;
            this.list.ItemHeight = 15;
            this.list.Location = new System.Drawing.Point(199, 45);
            this.list.Name = "list";
            this.list.Size = new System.Drawing.Size(161, 319);
            this.list.TabIndex = 0;
            this.list.Tag = "cscsc";
            this.list.SelectedIndexChanged += new System.EventHandler(this.List_SelectedIndexChanged);
            // 
            // certern
            // 
            this.certern.Location = new System.Drawing.Point(570, 149);
            this.certern.Name = "certern";
            this.certern.Size = new System.Drawing.Size(137, 65);
            this.certern.TabIndex = 1;
            this.certern.Text = "cretern";
            this.certern.UseVisualStyleBackColor = true;
            this.certern.Click += new System.EventHandler(this.Certern_Click);
            // 
            // tcount
            // 
            this.tcount.Location = new System.Drawing.Point(514, 107);
            this.tcount.Name = "tcount";
            this.tcount.Size = new System.Drawing.Size(193, 25);
            this.tcount.TabIndex = 2;
            this.tcount.Text = "0";
            // 
            // del
            // 
            this.del.Location = new System.Drawing.Point(629, 344);
            this.del.Name = "del";
            this.del.Size = new System.Drawing.Size(137, 65);
            this.del.TabIndex = 3;
            this.del.Text = "del";
            this.del.UseVisualStyleBackColor = true;
            this.del.Click += new System.EventHandler(this.Del_Click);
            // 
            // read
            // 
            this.read.Location = new System.Drawing.Point(386, 344);
            this.read.Name = "read";
            this.read.Size = new System.Drawing.Size(137, 65);
            this.read.TabIndex = 4;
            this.read.Text = "read";
            this.read.UseVisualStyleBackColor = true;
            this.read.Click += new System.EventHandler(this.Read_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.read);
            this.Controls.Add(this.del);
            this.Controls.Add(this.tcount);
            this.Controls.Add(this.certern);
            this.Controls.Add(this.list);
            this.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::KJtest.Properties.Settings.Default, "zhang", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.HelpButton = true;
            this.Name = "Form1";
            this.Tag = "123";
            this.Text = global::KJtest.Properties.Settings.Default.zhang;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox list;
        private System.Windows.Forms.Button certern;
        private System.Windows.Forms.TextBox tcount;
        private System.Windows.Forms.Button del;
        private System.Windows.Forms.Button read;
    }
}

