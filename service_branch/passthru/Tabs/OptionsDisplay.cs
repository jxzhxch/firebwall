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
            languageBox.SelectedIndex = 1;
            switch (LanguageConfig.GetCurrentLanguage())
            {
                case LanguageConfig.Language.NONE:
                case LanguageConfig.Language.ENGLISH:
                    displayTrayLogs.Text = "Display Icon Popups";
                    languageLabel.Text = "Language: ";
                    linkLabel1.Text = "Report Issue";
                    break;
                case LanguageConfig.Language.CHINESE:
                    displayTrayLogs.Text = "显示图标弹出窗口";
                    languageLabel.Text = "語言標籤:";
                    linkLabel1.Text = "报告问题";
                    break;
                case LanguageConfig.Language.GERMAN:
                    displayTrayLogs.Text = "Anzeige Icon Popups";
                    languageLabel.Text = "Sprachensiegel:";
                    linkLabel1.Text = "Report Issue";
                    break;
                case LanguageConfig.Language.RUSSIAN:
                    displayTrayLogs.Text = "Показать Иконка всплывающие окна";
                    languageLabel.Text = "Язык этикетки:";
                    linkLabel1.Text = "Сообщить о проблеме";
                    break;
                case LanguageConfig.Language.SPANISH:
                    displayTrayLogs.Text = "Mostrar ventanas emergentes Icono";
                    languageLabel.Text = "Lenguaje de etiquetas:";
                    linkLabel1.Text = "informe del problema";
                    break;
            }
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
    }
}
