﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace PassThru
{
    public partial class AdapterDisplay : UserControl
    {
        AdapterInfo ai;

        public AdapterDisplay(AdapterInfo ai)
        {
            this.ai = ai;
            InitializeComponent();
            textBoxDetails.Text = ai.Summary;
        }

        private void buttonConfig_Click(object sender, EventArgs e)
        {
            Form f = new Form();
            f.Text = ai.NIName;
            f.Width = 640;
            f.Height = 480;
            TabControl tc = new TabControl();
            tc.Dock = DockStyle.Fill;            
            for (int i = 0; i < ai.na.modules.Count; i++)
            {
                FirewallModule fm = ai.na.modules.GetModule(i);
                if (fm.GetControl() != null)
                {
                    TabPage tp = new TabPage(fm.moduleName);
                    tp.Location = new System.Drawing.Point(4, 22);
                    tp.Name = fm.moduleName;
                    tp.Controls.Add(fm.GetControl());
                    tp.Controls[0].BringToFront();
                    tc.TabPages.Add(tp);
                }
            }
            f.Controls.Add(tc);
            System.Reflection.Assembly target = System.Reflection.Assembly.GetExecutingAssembly();
            f.Icon = new System.Drawing.Icon(target.GetManifestResourceStream("PassThru.Resources.HoneyPorts.ico"));
            f.Show();
        }
    }
}
