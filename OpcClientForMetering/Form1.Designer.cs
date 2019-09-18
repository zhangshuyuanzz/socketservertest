
using System.Windows.Forms;

namespace OpcClientForMetering
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
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem("");
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.label1 = new System.Windows.Forms.Label();
            this.listView1 = new System.Windows.Forms.ListView();
            this.tagName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tagValue = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tagTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.itemlist = new System.Windows.Forms.Label();
            this.inputitem = new System.Windows.Forms.TextBox();
            this.itemname = new System.Windows.Forms.Label();
            this.AddItem = new System.Windows.Forms.Button();
            this.DelitemBtn = new System.Windows.Forms.Button();
            this.DelItem = new System.Windows.Forms.Label();
            this.DelInputItem = new System.Windows.Forms.TextBox();
            this.index = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.label1.Location = new System.Drawing.Point(257, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(247, 30);
            this.label1.TabIndex = 0;
            this.label1.Text = "Opc数据配置界面";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // listView1
            // 
            this.listView1.Alignment = System.Windows.Forms.ListViewAlignment.Default;
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.index,
            this.tagName,
            this.tagValue,
            this.tagTime});
            this.listView1.FullRowSelect = true;
            this.listView1.GridLines = true;
            this.listView1.HideSelection = false;
            this.listView1.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1});
            this.listView1.Location = new System.Drawing.Point(57, 86);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(650, 354);
            this.listView1.TabIndex = 1;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            //this.listView1.SelectedIndexChanged += new System.EventHandler(this.ListView1_SelectedIndexChanged);
            this.listView1.MouseDoubleClick += ListView1_MouseDoubleClick;
            // 
            // tagName
            // 
            this.tagName.DisplayIndex = 0;
            this.tagName.Text = "tag名字";
            this.tagName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tagName.Width = 239;
            // 
            // tagValue
            // 
            this.tagValue.DisplayIndex = 1;
            this.tagValue.Text = "tag值";
            this.tagValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tagValue.Width = 185;
            // 
            // tagTime
            // 
            this.tagTime.DisplayIndex = 2;
            this.tagTime.Text = "tag时间";
            this.tagTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tagTime.Width = 222;
            // 
            // itemlist
            // 
            this.itemlist.AutoSize = true;
            this.itemlist.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.itemlist.Location = new System.Drawing.Point(340, 59);
            this.itemlist.Name = "itemlist";
            this.itemlist.Size = new System.Drawing.Size(67, 15);
            this.itemlist.TabIndex = 2;
            this.itemlist.Text = "条目列表";
            // 
            // inputitem
            // 
            this.inputitem.Location = new System.Drawing.Point(307, 500);
            this.inputitem.Name = "inputitem";
            this.inputitem.Size = new System.Drawing.Size(100, 25);
            this.inputitem.TabIndex = 3;
            this.inputitem.TextChanged += new System.EventHandler(this.Inputitem_TextChanged);
            // 
            // itemname
            // 
            this.itemname.AutoSize = true;
            this.itemname.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.itemname.Location = new System.Drawing.Point(220, 503);
            this.itemname.Name = "itemname";
            this.itemname.Size = new System.Drawing.Size(67, 15);
            this.itemname.TabIndex = 4;
            this.itemname.Text = "增加条目";
            // 
            // AddItem
            // 
            this.AddItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.AddItem.ForeColor = System.Drawing.Color.Black;
            this.AddItem.Location = new System.Drawing.Point(429, 502);
            this.AddItem.Name = "AddItem";
            this.AddItem.Size = new System.Drawing.Size(75, 23);
            this.AddItem.TabIndex = 5;
            this.AddItem.Text = "Add";
            this.AddItem.UseVisualStyleBackColor = false;
            this.AddItem.Click += new System.EventHandler(this.AddItem_Click);
            // 
            // DelitemBtn
            // 
            this.DelitemBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.DelitemBtn.ForeColor = System.Drawing.Color.Black;
            this.DelitemBtn.Location = new System.Drawing.Point(429, 560);
            this.DelitemBtn.Name = "DelitemBtn";
            this.DelitemBtn.Size = new System.Drawing.Size(75, 23);
            this.DelitemBtn.TabIndex = 8;
            this.DelitemBtn.Text = "Del";
            this.DelitemBtn.UseVisualStyleBackColor = false;
            this.DelitemBtn.Click += new System.EventHandler(this.DelitemBtn_Click);
            // 
            // DelItem
            // 
            this.DelItem.AutoSize = true;
            this.DelItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.DelItem.Location = new System.Drawing.Point(220, 561);
            this.DelItem.Name = "DelItem";
            this.DelItem.Size = new System.Drawing.Size(67, 15);
            this.DelItem.TabIndex = 7;
            this.DelItem.Text = "删除条目";
            // 
            // DelInputItem
            // 
            this.DelInputItem.Location = new System.Drawing.Point(307, 558);
            this.DelInputItem.Name = "DelInputItem";
            this.DelInputItem.Size = new System.Drawing.Size(100, 25);
            this.DelInputItem.TabIndex = 6;
            // 
            // index
            // 
            this.index.DisplayIndex = 3;
            this.index.Text = "ID";
            this.index.Width = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 639);
            this.Controls.Add(this.DelitemBtn);
            this.Controls.Add(this.DelItem);
            this.Controls.Add(this.DelInputItem);
            this.Controls.Add(this.AddItem);
            this.Controls.Add(this.itemname);
            this.Controls.Add(this.inputitem);
            this.Controls.Add(this.itemlist);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "计量用Opc客户端";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.Label itemlist;
        private System.Windows.Forms.TextBox inputitem;
        private System.Windows.Forms.Label itemname;
        private System.Windows.Forms.Button AddItem;
        private System.Windows.Forms.Button DelitemBtn;
        private System.Windows.Forms.Label DelItem;
        private System.Windows.Forms.TextBox DelInputItem;
        private ColumnHeader tagName;
        private ColumnHeader tagValue;
        private ColumnHeader tagTime;
        private ColumnHeader index;
    }
}

