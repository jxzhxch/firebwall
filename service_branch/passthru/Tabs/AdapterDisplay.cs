using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using FM;

namespace PassThru
{
    public partial class AdapterDisplay : UserControl
    {
        public AdapterInfo ai;

        public void Update()
        {
            if (textBoxDetails.InvokeRequired)
            {
                System.Threading.ThreadStart ts = new System.Threading.ThreadStart(Update);
                textBoxDetails.Invoke(ts);
            }
            else
            {
                textBoxDetails.Text = ai.Summary;
            }
        }

        public AdapterDisplay(AdapterInfo ai)
        {
            if (null != ai)
            {
                this.ai = ai;
                InitializeComponent();
                textBoxDetails.Text = ai.Summary;
                checkBox1.Checked = ai.NetAdapter.Enabled;
            }
        }

        private void buttonConfig_Click(object sender, EventArgs e)
        {
            Form f = new Form();
            f.Text = ai.NIName;
            f.Width = 640;
            f.Height = 480;
            TabControl tc = new TabControl();
            tc.Dock = DockStyle.Fill;
            TabPage tpage = new TabPage("Module Configurations");
            tpage.Name = "Module Configurations";
            tpage.Controls.Add(new Modules.ModuleConfiguration(ai.NetAdapter) { Dock = DockStyle.Fill });
            tpage.Controls[0].BringToFront();
            tc.TabPages.Add(tpage);
            for (int i = 0; i < ai.NetAdapter.modules.Count; i++)
            {
                FirewallModule fm = ai.NetAdapter.modules.GetModule(i);
                if (fm.GetControl() != null)
                {
                    TabPage tp = new TabPage(fm.MetaData.Name);
                    tp.Location = new System.Drawing.Point(4, 22);
                    tp.Name = fm.MetaData.Name;
                    tp.Controls.Add(fm.GetControl());
                    tp.Controls[0].BringToFront();
                    tc.TabPages.Add(tp);
                }
            }
            f.Controls.Add(tc);
            System.Reflection.Assembly target = System.Reflection.Assembly.GetExecutingAssembly();
            f.Icon = new System.Drawing.Icon(target.GetManifestResourceStream("PassThru.Resources.newIcon.ico"));
            f.Show();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            ai.NetAdapter.Enabled = checkBox1.Checked;
            buttonConfig.Enabled = checkBox1.Checked;
        }

        private void AdapterDisplay_Load(object sender, EventArgs e)
        {
            switch (LanguageConfig.GetCurrentLanguage())
            {
                case LanguageConfig.Language.NONE:
                case LanguageConfig.Language.ENGLISH:
                    checkBox1.Text = "Enable";
                    buttonConfig.Text = "Configure Device";
                    break;
                case LanguageConfig.Language.CHINESE:
                    checkBox1.Text = "启用";
                    buttonConfig.Text = "配置设备";
                    break;
                case LanguageConfig.Language.GERMAN:
                    checkBox1.Text = "ermöglichen";
                    buttonConfig.Text = "Gerät konfigurieren";
                    break;
                case LanguageConfig.Language.RUSSIAN:
                    checkBox1.Text = "Включить";
                    buttonConfig.Text = "Настройка устройства";
                    break;
                case LanguageConfig.Language.SPANISH:
                    checkBox1.Text = "permitir";
                    buttonConfig.Text = "configurar dispositivo";
                    break;
            }
        }
    }
}
