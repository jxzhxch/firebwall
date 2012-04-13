using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PassThru.Tabs
{
    public partial class DownloadCenter : Form
    {
        static DownloadCenter dc = null;
        static object padlock = new object();
        public static DownloadCenter Instance
        {
            get
            {
                lock (padlock)
                {
                    if (dc == null)
                        dc = new DownloadCenter();
                    return dc;
                }
            }
        }

        public void ShowFirebwallUpdate()
        {
            if (this.InvokeRequired)
            {
                System.Threading.ThreadStart ts = new System.Threading.ThreadStart(ShowFirebwallUpdate);
                this.Invoke(ts);
            }
            else
            {
                this.Visible = true;
            }
        }

        private DownloadCenter()
        {
            InitializeComponent();
        }

        private void DownloadCenter_Load(object sender, EventArgs e)
        {
        }

        private void DownloadCenter_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                this.Visible = false;
                e.Cancel = true;
            }
        }
    }
}
