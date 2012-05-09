using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using FM;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Drawing.Drawing2D;

namespace PassThru
{
    /// <summary>
    /// Object used for options interfacing
    /// </summary>
    public partial class OptionsDisplay : UserControl
    {
        public OptionsDisplay()
        {
            InitializeComponent();
            LoadGeneralConfig();
        }

        /// <summary>
        /// Event for when displayTrayLogs changes state
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void displayTrayLogs_CheckedChanged(object sender, EventArgs e)
        {
            TrayIcon.displayTrayLogs = displayTrayLogs.Checked;
        }

        /// <summary>
        /// Language load for option settings
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OptionsDisplay_Load(object sender, EventArgs e)
        {
            foreach (string theme in ColorScheme.GetThemes())
                themeBox.Items.Add(theme);
            themeBox.SelectedItem = ColorScheme.GetCurrentTheme();

            switch (LanguageConfig.GetCurrentLanguage())
            {
                case LanguageConfig.Language.NONE:
                case LanguageConfig.Language.ENGLISH:
                    displayTrayLogs.Text = "Display Icon Popups";
                    languageLabel.Text = "Language: ";
                    linkLabel1.Text = "Report Issue";
                    languageBox.SelectedIndex = 1;
                    break;
                case LanguageConfig.Language.PORTUGUESE:
                    displayTrayLogs.Text = "Mostrar Popups Icon";
                    languageLabel.Text = "Linguagem: ";
                    linkLabel1.Text = "Reportagem Edição";
                    languageBox.SelectedIndex = 6;
                    break;
                case LanguageConfig.Language.CHINESE:
                    displayTrayLogs.Text = "显示图标弹出窗口";
                    languageLabel.Text = "語言標籤:";
                    linkLabel1.Text = "报告问题";
                    languageBox.SelectedIndex = 4;
                    break;
                case LanguageConfig.Language.GERMAN:
                    displayTrayLogs.Text = "Anzeige Icon Popups";
                    languageLabel.Text = "Sprachensiegel:";
                    linkLabel1.Text = "Report Issue";
                    languageBox.SelectedIndex = 3;
                    break;
                case LanguageConfig.Language.RUSSIAN:
                    displayTrayLogs.Text = "Показать Иконка всплывающие окна";
                    languageLabel.Text = "Язык этикетки:";
                    linkLabel1.Text = "Сообщить о проблеме";
                    languageBox.SelectedIndex = 5;
                    break;
                case LanguageConfig.Language.SPANISH:
                    displayTrayLogs.Text = "Mostrar ventanas emergentes Icono";
                    languageLabel.Text = "Lenguaje de etiquetas:";
                    linkLabel1.Text = "informe del problema";
                    languageBox.SelectedIndex = 2;
                    break;
            }
            checkBox1.Checked = Program.uc.Config.StartUpCheck;
            checkBox2.Checked = Program.uc.Config.Enabled;
            checkBoxStartMinimized.Checked = TrayIcon.StartMinimized;
            textBox1.Text = Program.uc.Config.MinuteInterval.ToString();
            displayTrayLogs.Checked = TrayIcon.displayTrayLogs;
            ColorScheme.ThemeChanged += new System.Threading.ThreadStart(ColorScheme_ThemeChanged);

            maxLogsBox.Text = Convert.ToString(gSettings.max_logs);
            maxPcapBox.Text = Convert.ToString(gSettings.max_pcap_logs);
        }

        void ColorScheme_ThemeChanged()
        {
            themeBox.Items.Clear();
            foreach (string theme in ColorScheme.GetThemes())
                themeBox.Items.Add(theme);
            themeBox.SelectedItem = ColorScheme.GetCurrentTheme();
            ColorScheme.SetColorScheme(this);
        }        

        /// <summary>
        /// Let the user select their language!
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (languageBox.SelectedIndex)
            {
                case 0:
                    LanguageConfig.SetLanguage(LanguageConfig.Language.NONE);
                    break;
                case 1:
                    LanguageConfig.SetLanguage(LanguageConfig.Language.ENGLISH);
                    break;
                case 2:
                    LanguageConfig.SetLanguage(LanguageConfig.Language.SPANISH);
                    break;
                case 3:
                    LanguageConfig.SetLanguage(LanguageConfig.Language.GERMAN);
                    break;
                case 4:
                    LanguageConfig.SetLanguage(LanguageConfig.Language.CHINESE);
                    break;
                case 5:
                    LanguageConfig.SetLanguage(LanguageConfig.Language.RUSSIAN);
                    break;
                case 6:
                    LanguageConfig.SetLanguage(LanguageConfig.Language.PORTUGUESE);
                    break;
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://code.google.com/p/firebwall/issues/list");
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.facebook.com/pages/FireBwall/261822493882169");
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.reddit.com/r/firebwall/");
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            Program.uc.Config.Enabled = checkBox2.Checked;
            Program.uc.SaveConfig();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            Program.uc.Config.StartUpCheck = checkBox1.Checked;
            Program.uc.SaveConfig();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            uint v = 0;
            if (uint.TryParse(textBox1.Text, out v) && v >= 10)
            {
                Program.uc.Config.MinuteInterval = v;
                Program.uc.SaveConfig();
            }
        }

        private void checkBoxStartMinimized_CheckedChanged(object sender, EventArgs e)
        {
            TrayIcon.StartMinimized = checkBoxStartMinimized.Checked;
        }

        private void themeBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ColorScheme.GetThemes().Contains((string)themeBox.SelectedItem) && ColorScheme.GetCurrentTheme() != (string)themeBox.SelectedItem)
                ColorScheme.ChangeTheme((string)themeBox.SelectedItem);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ColorSchemeEditor cse = new ColorSchemeEditor(ColorScheme.currentTheme);
            cse.Width = 640;
            cse.Height = 480;
            System.Reflection.Assembly target = System.Reflection.Assembly.GetExecutingAssembly();
            cse.Icon = new System.Drawing.Icon(target.GetManifestResourceStream("PassThru.Resources.newIcon.ico"));
            cse.Show();
        }

        // object houses general settings
        [Serializable]
        public class GeneralSettings
        {
            public int max_logs = 5;
            public int max_pcap_logs = 25;
        }

        public static GeneralSettings gSettings;

        /// <summary>
        /// Save the general configuration 
        /// </summary>
        public void SaveGeneralConfig()
        {
            try
            {
                string folder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                folder = folder + Path.DirectorySeparatorChar + "firebwall";
                if (!Directory.Exists(folder))
                    Directory.CreateDirectory(folder);
                string file = folder + Path.DirectorySeparatorChar + "generalconfig.cfg";
                FileStream stream = File.Open(file, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite);
                BinaryFormatter bFormatter = new BinaryFormatter();
                bFormatter.AssemblyFormat = System.Runtime.Serialization.Formatters.FormatterAssemblyStyle.Simple;
                bFormatter.Serialize(stream, gSettings);
                stream.Close();
            }
            catch (Exception e) 
            {
                LogCenter.WriteErrorLog(e);
            }
        }

        /// <summary>
        /// load the general configuration up
        /// </summary>
        public static void LoadGeneralConfig()
        {
            try
            {
                string folder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                folder = folder + Path.DirectorySeparatorChar + "firebwall";
                if (!Directory.Exists(folder))
                    Directory.CreateDirectory(folder);
                string file = folder + Path.DirectorySeparatorChar + "generalconfig.cfg";
                if (File.Exists(file))
                {
                    FileStream stream = File.Open(file, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                    BinaryFormatter bFormatter = new BinaryFormatter();
                    bFormatter.AssemblyFormat = System.Runtime.Serialization.Formatters.FormatterAssemblyStyle.Simple;
                    bFormatter.Binder = new PassThru.UpdateChecker.VersionConfigToNamespaceAssemblyObjectBinder();
                    gSettings = (GeneralSettings)bFormatter.Deserialize(stream);
                    stream.Close();
                }
                else
                {
                    gSettings = new GeneralSettings();
                }
            }
            catch
            {
                gSettings = new GeneralSettings();
            }
        }

        /// <summary>
        /// Stores the new max log
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void maxLogsBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                int tmp = Convert.ToInt32(maxLogsBox.Text);
                if (tmp < Int32.MaxValue)
                {
                    gSettings.max_logs = tmp;
                    SaveGeneralConfig();
                }
            }
            catch
            { }
        }

        /// <summary>
        /// Stores the new max pcap log
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void maxPcapBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                int tmp = Convert.ToInt32(maxPcapBox.Text);
                if (tmp < Int32.MaxValue)
                {
                    gSettings.max_pcap_logs = tmp;
                    SaveGeneralConfig();
                }
            }
            catch { }
        }

        /// <summary>
        /// .NET doesn't give us much control over the TabControl, so we have to draw it ourselves if we want
        /// themes to work correctly.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Updating_DrawItem(object sender, DrawItemEventArgs e)
        {
            Rectangle tabstripEndRect = optionsControl.GetTabRect(optionsControl.TabPages.Count - 1);
            RectangleF tabstripEndRectF = new RectangleF(tabstripEndRect.X + tabstripEndRect.Width, tabstripEndRect.Y - 5,
                                          optionsControl.Width -(tabstripEndRect.X + tabstripEndRect.Width), tabstripEndRect.Height + 5);

            if (optionsControl.Parent.BackgroundImage != null)
            {
                RectangleF src = new RectangleF(tabstripEndRectF.X + optionsControl.Left,
                                                tabstripEndRectF.Y + optionsControl.Top, tabstripEndRectF.Width,
                                                tabstripEndRectF.Height);
                e.Graphics.DrawImage(optionsControl.Parent.BackgroundImage, tabstripEndRectF, src, GraphicsUnit.Pixel);
            }
            else
            {
                using (Brush back = new SolidBrush(optionsControl.Parent.BackColor))
                {
                    e.Graphics.FillRectangle(back, tabstripEndRectF);
                }
            }

            TabPage page = optionsControl.TabPages[e.Index];
            Brush BackBrush = new SolidBrush(page.BackColor);
            Brush ForeBrush = new SolidBrush(page.ForeColor);
            string TabName = page.Text;

            int iconOffset = 0;
            Rectangle tabBackgroundRect = e.Bounds;
            e.Graphics.FillRectangle(BackBrush, tabBackgroundRect);

            Rectangle labelRect = new Rectangle(tabBackgroundRect.X + iconOffset,
                tabBackgroundRect.Y + 3,
                tabBackgroundRect.Width - iconOffset, tabBackgroundRect.Height - 3);
            
            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Center;
            e.Graphics.DrawString(TabName, e.Font, ForeBrush, labelRect, sf);

            sf.Dispose();
            BackBrush.Dispose();
            ForeBrush.Dispose();
        }
    }
}
