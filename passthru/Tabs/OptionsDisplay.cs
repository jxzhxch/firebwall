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
    /// <summary>
    /// Object used for options interfacing
    /// </summary>
    public partial class OptionsDisplay : UserControl
    {
        public OptionsDisplay()
        {
            InitializeComponent();
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
            textBox1.Text = Program.uc.Config.MinuteInterval.ToString();
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
    }
}
