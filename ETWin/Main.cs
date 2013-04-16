using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ETBiz;
using System.IO;
using System.Data;
using System.Diagnostics;
namespace ETWin
{
    public partial class Main : Form
    {
        ETBiz.TransferInDatatable bizTransfer = new TransferInDatatable();
        public Main()
        {
            InitializeComponent();
        }
     
       

     
       

        private void btnCreate_Click(object sender, EventArgs e)
        {
            bizTransfer.Transfer(tbxBaojia.Text, true);
            MessageBox.Show("生成成功"); 
        }

        private void btnCreateTest_Click(object sender, EventArgs e)
        {
            bizTransfer.Transfer(tbxBaojia.Text, false);

            MessageBox.Show("测试成功");
        }

        private void tbxBaojia_TextChanged(object sender, EventArgs e)
        {
            string filePath = tbxBaojia.Text.Trim();
            if (!File.Exists(filePath))
            {
                lblBaojiandanMsg.Text = "文件不存在,请重新选择.";
                return;
            }
            DataTable dt = bizTransfer.CreateFromXsl(filePath);
            lblBaojiandanMsg.Text = "报价单条目总数: " + dt.Rows.Count;
        }

        private void btnBaojiaSelect_Click(object sender, EventArgs e)
        {
            if (fdBaojia.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                tbxBaojia.Text = fdBaojia.FileName;
            }

        }

        private void lbViewCreaedFiles_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(Environment.CurrentDirectory);
        }
    }
}
