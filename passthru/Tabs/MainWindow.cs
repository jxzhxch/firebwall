﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using FM;

namespace PassThru
{
		public partial class MainWindow: Form 
        {
			public MainWindow() 
            {
				InitializeComponent();                
			}

			public void Exit() {
				Application.Exit();
			}
			
            private void MainWindow_FormClosing(object sender, FormClosingEventArgs e) 
            {
				if (e.CloseReason == CloseReason.UserClosing)
				{
					this.Visible = false;
					e.Cancel = true;
                    ac.Kill();
				}
			}

            //RuleEditor re;
            Tabs.LogDisplay log;
            AdapterControl ac;
            OptionsDisplay od;
            Help help;

			private void MainWindow_Load(object sender, EventArgs e) 
            {
                //optionsTab.ItemSize = new Size((this.Width / 4) - 6, optionsTab.ItemSize.Height);
                System.Reflection.Assembly target = System.Reflection.Assembly.GetExecutingAssembly();
                this.Icon = new System.Drawing.Icon(target.GetManifestResourceStream("PassThru.Resources.newIcon.ico"));
                LogCenter.ti = new TrayIcon();
                // call the log purger
                LogCenter.cleanLogs();

                log = new Tabs.LogDisplay();
                log.Dock = DockStyle.Fill;
                LogCenter.PushLogEvent += new LogCenter.NewLogEvent(log.Instance_PushLogEvent);
                splitContainer1.Panel2.Controls.Add(log);

                // load up the adapter control handler
				ac = new AdapterControl();
				ac.Dock = DockStyle.Fill;
				//tabPage3.Controls.Add(ac);

                // load up the options tab handler
                od = new OptionsDisplay();
                od.Dock = DockStyle.Fill;
                //tabPage2.Controls.Add(od);

                // load up the options tab handler
                help = new Help();
                help.Dock = DockStyle.Fill;
                //tabPage4.Controls.Add(help);

                switch (LanguageConfig.GetCurrentLanguage())
                {
                    case LanguageConfig.Language.NONE:
                    case LanguageConfig.Language.ENGLISH:
                        tabPage1.Text = "Log";
                        tabPage2.Text = "Options";
                        tabPage3.Text = "Adapters";
                        break;
                    case LanguageConfig.Language.CHINESE:
                        tabPage1.Text = "登录";
                        tabPage2.Text = "选项";
                        tabPage3.Text = "适配器";
                        break;
                    case LanguageConfig.Language.GERMAN:
                        tabPage1.Text = "Log";
                        tabPage2.Text = "Optionen";
                        tabPage3.Text = "Adapter";
                        break;
                    case LanguageConfig.Language.RUSSIAN:
                        tabPage1.Text = "журнал";
                        tabPage2.Text = "опции";
                        tabPage3.Text = "Адаптеры";
                        break;
                    case LanguageConfig.Language.SPANISH:
                        tabPage1.Text = "log";
                        tabPage2.Text = "opciones";
                        tabPage3.Text = "adaptadores";
                        break;
                    case LanguageConfig.Language.PORTUGUESE:
                        tabPage1.Text = "Entrar";
                        tabPage2.Text = "opções";
                        tabPage3.Text = "adaptadores";
                        break;
                }
                MainWindow_Resize(null, null);
			}

            private void MainWindow_Resize(object sender, EventArgs e)
            {
                tabPage1.Location = new Point(pictureBox1.Width / 20, (pictureBox1.Height / 2) - (tabPage1.Height / 2) - 4);
                tabPage2.Location = new Point(12 * pictureBox1.Width / 20, (pictureBox1.Height / 2) - (tabPage2.Height / 2) - 4);
                tabPage3.Location = new Point(5 * pictureBox1.Width / 20, (pictureBox1.Height / 2) - (tabPage3.Height / 2) - 4);
                tabPage4.Location = new Point(16 * pictureBox1.Width / 20, (pictureBox1.Height / 2) - (tabPage4.Height / 2) - 4);                
            }

            private void tabPage1_Click(object sender, EventArgs e)
            {
                splitContainer1.Panel2.Controls.Clear();
                splitContainer1.Panel2.Controls.Add(log);
            }

            private void tabPage2_Click(object sender, EventArgs e)
            {
                splitContainer1.Panel2.Controls.Clear();
                splitContainer1.Panel2.Controls.Add(od);
            }

            private void tabPage2_Click_1(object sender, EventArgs e)
            {
                splitContainer1.Panel2.Controls.Clear();
                splitContainer1.Panel2.Controls.Add(ac);
            }

            private void tabPage4_Click(object sender, EventArgs e)
            {
                splitContainer1.Panel2.Controls.Clear();
                splitContainer1.Panel2.Controls.Add(help);
            }
		}
}