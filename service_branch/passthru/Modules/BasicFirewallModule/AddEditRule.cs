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
    public partial class AddEditRule : Form
    {
        public AddEditRule()
        {
            InitializeComponent();
        }

        private BasicFirewall.Rule newRule;
        public BasicFirewall.Rule NewRule
        {
            get { return newRule; }
            set { newRule = value; }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox1.Text)
            {
                case "All UDP Rule":
                case "所有的UDP規則":
                case "Alle UDP Regel":
                case "Все правила UDP":
                case "Todos los Regla UDP":
                    // most of these aren't going to translate very differently 
                    // into their foreign tongue, so i'm leaving it as is
                    labelArgs.Text = "(Space Separated) No args";
                    break;
                case "UDP Port Rule":
                case "UDP端口規則":
                case "UDP-порта Правило":
                case "El puerto UDP Regla":
                    labelArgs.Text = "(Space Separated) Port";
                    break;
                case "TCP IP and Port Rule":
                case "TCP IP和端口規則":
                case "TCP IP und Port Regel":
                case "TCP-IP и порт Правило":
                case "TCP IP y puerto de Regla":
                    labelArgs.Text = "(Space Separated) IP Port";
                    break;
                case "TCP Port Rule":
                case "TCP端口規則":
                case "TCP-порт Правило":
                case "Puerto TCP Regla":
                    labelArgs.Text = "(Space Separated) Port";
                    break;
                case "All TCP Rule":
                case "所有的TCP規則":
                case "Alle TCP Rule":
                case "Все правила TCP":
                case "Todos los Regla TCP":
                    labelArgs.Text = "(Space Separated) No args";
                    break;
                case "All Rule":
                case "所有規則":
                case "Alle Rule":
                case "Все правила":
                case "todos los Regla":
                    labelArgs.Text = "(Space Separated) No args";
                    break;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (checkBoxIn.Checked || checkBoxOut.Checked)
            {
                try
                {
                    BasicFirewall.RuleType rt = BasicFirewall.RuleType.ALL;
                    switch (comboBox1.Text)
                    {
                        case "All UDP Rule":
                        case "所有的UDP規則":
                        case "Alle UDP Regel":
                        case "Все правила UDP":
                        case "Todos los Regla UDP":
                            rt = BasicFirewall.RuleType.UDPALL;
                            break;
                        case "UDP Port Rule":
                        case "UDP端口規則":
                        case "UDP-порта Правило":
                        case "El puerto UDP Regla":
                            rt = BasicFirewall.RuleType.UDPPORT;
                            break;
                        case "TCP IP and Port Rule":
                        case "TCP IP和端口規則":
                        case "TCP IP und Port Regel":
                        case "TCP-IP и порт Правило":
                        case "TCP IP y puerto de Regla":
                            rt = BasicFirewall.RuleType.TCPIPPORT;
                            break;
                        case "TCP Port Rule":
                        case "TCP端口規則":
                        case "TCP-порт Правило":
                        case "Puerto TCP Regla":
                            rt = BasicFirewall.RuleType.TCPPORT;
                            break;
                        case "All TCP Rule":
                        case "所有的TCP規則":
                        case "Alle TCP Rule":
                        case "Все правила TCP":
                        case "Todos los Regla TCP":
                            rt = BasicFirewall.RuleType.TCPALL;
                            break;
                        case "All Rule":
                        case "所有規則":
                        case "Alle Rule":
                        case "Все правила":
                        case "todos los Regla":
                            rt = BasicFirewall.RuleType.ALL;
                            break;
                    }
                    BasicFirewall.Direction dir;
                    if (checkBoxIn.Checked && checkBoxOut.Checked)
                    {
                        dir = BasicFirewall.Direction.IN | BasicFirewall.Direction.OUT;
                    }
                    else if (checkBoxOut.Checked)
                    {
                        dir = BasicFirewall.Direction.OUT;
                    }
                    else
                    {
                        dir = BasicFirewall.Direction.IN;
                    }
                    BasicFirewall.PacketStatus ps;
                    if (comboBoxAction.Text == "Block")
                        ps = BasicFirewall.PacketStatus.BLOCKED;
                    else
                        ps = BasicFirewall.PacketStatus.ALLOWED;
                    this.DialogResult = System.Windows.Forms.DialogResult.OK;
                    newRule = BasicFirewall.RuleFactory.MakeRule(rt, ps, dir, textBoxArguments.Text, checkBoxLog.Checked);
                    this.Close();
                }
                catch (Exception exception)
                {
                    MessageBox.Show("Error in creating rule.");
                    LogCenter.WriteErrorLog(exception);
                }
            }
            else
            {
                MessageBox.Show("You need to select in or out first");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void AddEditRule_Load(object sender, EventArgs e)
        {
            switch (LanguageConfig.GetCurrentLanguage())
            {
                case LanguageConfig.Language.ENGLISH:
                    comboBox1.Text = "All Rule";
                    comboBox1.Items.Add("All TCP Rule");
                    comboBox1.Items.Add("TCP IP and Port Rule");
                    comboBox1.Items.Add("TCP Port Rule");
                    comboBox1.Items.Add("All UDP Rule");
                    comboBox1.Items.Add("UDP Port Rule");
                    comboBox1.Items.Add("All Rule");
                    break;
                case LanguageConfig.Language.CHINESE:
                    comboBox1.Text = "所有規則";
                    comboBox1.Items.Add("所有的TCP規則");
                    comboBox1.Items.Add("TCP IP和端口規則");
                    comboBox1.Items.Add("TCP端口規則");
                    comboBox1.Items.Add("所有的UDP規則");
                    comboBox1.Items.Add("UDP端口規則");
                    comboBox1.Items.Add("所有規則");
                    break;
                case LanguageConfig.Language.GERMAN:
                    comboBox1.Text = "Alle Rule";
                    comboBox1.Items.Add("Alle TCP Rule");
                    comboBox1.Items.Add("TCP IP und Port Regel");
                    comboBox1.Items.Add("TCP Port Rule");
                    comboBox1.Items.Add("Alle UDP Regel");
                    comboBox1.Items.Add("UDP Port Rule");
                    comboBox1.Items.Add("Alle Rule");
                    break;
                case LanguageConfig.Language.RUSSIAN:
                    comboBox1.Text = "Все правила";
                    comboBox1.Items.Add("Все правила TCP");
                    comboBox1.Items.Add("TCP-IP и порт Правило");
                    comboBox1.Items.Add("TCP-порт Правило");
                    comboBox1.Items.Add("Все правила UDP");
                    comboBox1.Items.Add("UDP-порта Правило");
                    comboBox1.Items.Add("Все правила");
                    break;
                case LanguageConfig.Language.SPANISH:
                    comboBox1.Text = "todos los Regla";
                    comboBox1.Items.Add("Todos los Regla TCP");
                    comboBox1.Items.Add("TCP IP y puerto de Regla");
                    comboBox1.Items.Add("Puerto TCP Regla");
                    comboBox1.Items.Add("Todos los Regla UDP");
                    comboBox1.Items.Add("El puerto UDP Regla");
                    comboBox1.Items.Add("todos los Regla");
                    break;
            }
        }

        private void textBoxArguments_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
