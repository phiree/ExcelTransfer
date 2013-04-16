namespace ETWin
{
    partial class Main
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
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.fdBaojia = new System.Windows.Forms.OpenFileDialog();
            this.btnBaojiaSelect = new System.Windows.Forms.Button();
            this.tbxBaojia = new System.Windows.Forms.TextBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.fdSupplier = new System.Windows.Forms.OpenFileDialog();
            this.label1 = new System.Windows.Forms.Label();
            this.btnCreate = new System.Windows.Forms.Button();
            this.btnCreateTest = new System.Windows.Forms.Button();
            this.lblBaojiandanMsg = new System.Windows.Forms.Label();
            this.btnSelectOK = new System.Windows.Forms.Button();
            this.lbViewCreaedFiles = new System.Windows.Forms.LinkLabel();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // fdBaojia
            // 
            this.fdBaojia.FileName = "openFileDialog1";
            // 
            // btnBaojiaSelect
            // 
            this.btnBaojiaSelect.Location = new System.Drawing.Point(12, 73);
            this.btnBaojiaSelect.Name = "btnBaojiaSelect";
            this.btnBaojiaSelect.Size = new System.Drawing.Size(88, 23);
            this.btnBaojiaSelect.TabIndex = 0;
            this.btnBaojiaSelect.Text = "选择报价单";
            this.btnBaojiaSelect.UseVisualStyleBackColor = true;
            this.btnBaojiaSelect.Click += new System.EventHandler(this.btnBaojiaSelect_Click);
            // 
            // tbxBaojia
            // 
            this.tbxBaojia.Location = new System.Drawing.Point(106, 73);
            this.tbxBaojia.Name = "tbxBaojia";
            this.tbxBaojia.Size = new System.Drawing.Size(288, 21);
            this.tbxBaojia.TabIndex = 2;
            this.tbxBaojia.TextChanged += new System.EventHandler(this.tbxBaojia_TextChanged);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(511, 25);
            this.toolStrip1.TabIndex = 3;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(57, 22);
            this.toolStripButton1.Text = "基础信息";
            // 
            // fdSupplier
            // 
            this.fdSupplier.FileName = "openFileDialog1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.DarkGreen;
            this.label1.Location = new System.Drawing.Point(12, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(382, 21);
            this.label1.TabIndex = 4;
            this.label1.Text = "选择报价单,或者将报价单文件拖放到本窗口";
            this.label1.Visible = false;
            // 
            // btnCreate
            // 
            this.btnCreate.Location = new System.Drawing.Point(126, 141);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(88, 23);
            this.btnCreate.TabIndex = 0;
            this.btnCreate.Text = "正式生成";
            this.btnCreate.UseVisualStyleBackColor = true;
            this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);
            // 
            // btnCreateTest
            // 
            this.btnCreateTest.Location = new System.Drawing.Point(12, 141);
            this.btnCreateTest.Name = "btnCreateTest";
            this.btnCreateTest.Size = new System.Drawing.Size(88, 23);
            this.btnCreateTest.TabIndex = 0;
            this.btnCreateTest.Text = "测试生成";
            this.btnCreateTest.UseVisualStyleBackColor = true;
            this.btnCreateTest.Click += new System.EventHandler(this.btnCreateTest_Click);
            // 
            // lblBaojiandanMsg
            // 
            this.lblBaojiandanMsg.AutoSize = true;
            this.lblBaojiandanMsg.Location = new System.Drawing.Point(104, 107);
            this.lblBaojiandanMsg.Name = "lblBaojiandanMsg";
            this.lblBaojiandanMsg.Size = new System.Drawing.Size(0, 12);
            this.lblBaojiandanMsg.TabIndex = 5;
            // 
            // btnSelectOK
            // 
            this.btnSelectOK.Location = new System.Drawing.Point(400, 71);
            this.btnSelectOK.Name = "btnSelectOK";
            this.btnSelectOK.Size = new System.Drawing.Size(88, 23);
            this.btnSelectOK.TabIndex = 6;
            this.btnSelectOK.Text = "确定";
            this.btnSelectOK.UseVisualStyleBackColor = true;
            // 
            // lbViewCreaedFiles
            // 
            this.lbViewCreaedFiles.AutoSize = true;
            this.lbViewCreaedFiles.Location = new System.Drawing.Point(13, 190);
            this.lbViewCreaedFiles.Name = "lbViewCreaedFiles";
            this.lbViewCreaedFiles.Size = new System.Drawing.Size(101, 12);
            this.lbViewCreaedFiles.TabIndex = 7;
            this.lbViewCreaedFiles.TabStop = true;
            this.lbViewCreaedFiles.Text = "查看已生成的文件";
            this.lbViewCreaedFiles.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lbViewCreaedFiles_LinkClicked);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(511, 285);
            this.Controls.Add(this.lbViewCreaedFiles);
            this.Controls.Add(this.btnSelectOK);
            this.Controls.Add(this.lblBaojiandanMsg);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.tbxBaojia);
            this.Controls.Add(this.btnCreateTest);
            this.Controls.Add(this.btnCreate);
            this.Controls.Add(this.btnBaojiaSelect);
            this.Name = "Main";
            this.Text = "Form1";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog fdBaojia;
        private System.Windows.Forms.Button btnBaojiaSelect;
        private System.Windows.Forms.TextBox tbxBaojia;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.OpenFileDialog fdSupplier;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnCreate;
        private System.Windows.Forms.Button btnCreateTest;
        private System.Windows.Forms.Label lblBaojiandanMsg;
        private System.Windows.Forms.Button btnSelectOK;
        private System.Windows.Forms.LinkLabel lbViewCreaedFiles;
    }
}

