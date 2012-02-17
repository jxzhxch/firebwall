using System;
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

            /*
             * Object handles logging of a log event to the window
             * @param le is the log event object to be logged
             */
			void AddLogEvent(object le) {
                // if the logger is busy, invoke it
				if (textBox1.InvokeRequired)
				{
						System.Threading.ParameterizedThreadStart d = new System.Threading.ParameterizedThreadStart(AddLogEvent);
						textBox1.Invoke(d, new object[] { le });
				}
                // else log the message
				else
				{
						LogEvent e = (LogEvent)le;
						textBox1.Text = e.time.ToString() + " " + e.Module + ": " + e.Message + "\r\n" + textBox1.Text;
				}
			}

            // receives a log event and pushes it to AddLogEvent
			void Instance_PushLogEvent(LogEvent e) {
				AddLogEvent(e);
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
            AdapterControl ac;
            OptionsDisplay od;
            Help help;

			private void MainWindow_Load(object sender, EventArgs e) 
            {
                System.Reflection.Assembly target = System.Reflection.Assembly.GetExecutingAssembly();
                this.Icon = new System.Drawing.Icon(target.GetManifestResourceStream("PassThru.Resources.newIcon.ico"));
				LogCenter.PushLogEvent += new LogCenter.NewLogEvent(Instance_PushLogEvent);
                
                // call the log purger
                LogCenter.cleanLogs();
				//RuleEditor re = new RuleEditor();
				//re.Dock = DockStyle.Fill;
				//tabPage2.Controls.Add(re);

                //re = new RuleEditor(null);
                //re.Dock = DockStyle.Fill;
                //tabPage2.Controls.Add(re);

                // load up the adapter control handler
				ac = new AdapterControl();
				ac.Dock = DockStyle.Fill;
				tabPage3.Controls.Add(ac);

                // load up the options tab handler
                od = new OptionsDisplay();
                od.Dock = DockStyle.Fill;
                tabPage2.Controls.Add(od);

                // load up the options tab handler
                help = new Help();
                help.Dock = DockStyle.Fill;
                tabPage4.Controls.Add(help);

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
                }
			}

            private void optionsTab_DrawItem(object sender, DrawItemEventArgs e)
            {
                Font TabFont;
                Brush BackBrush = new SolidBrush(Color.Black); //Set background color
                Brush ForeBrush = new SolidBrush(Color.DarkRed);//Set foreground color
                if (e.Index == this.optionsTab.SelectedIndex)
                {
                    TabFont = new Font(e.Font, FontStyle.Italic | FontStyle.Bold);
                }
                else
                {
                    TabFont = e.Font;
                }
                string TabName = this.optionsTab.TabPages[e.Index].Text;
                StringFormat sf = new StringFormat();
                sf.Alignment = StringAlignment.Center;
                e.Graphics.FillRectangle(BackBrush, e.Bounds);
                Rectangle r = e.Bounds;
                r = new Rectangle(r.X, r.Y + 3, r.Width, r.Height - 3);
                e.Graphics.DrawString(TabName, TabFont, ForeBrush, r, sf);
                //Dispose objects
                sf.Dispose();
                if (e.Index == this.optionsTab.SelectedIndex)
                {
                    TabFont.Dispose();
                    BackBrush.Dispose();
                }
                else
                {
                    BackBrush.Dispose();
                    ForeBrush.Dispose();
                }
            }
		}
}
