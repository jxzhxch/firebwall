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
        private BasicFirewall.Rule newRule;
        public BasicFirewall.Rule NewRule
        {
            get { return newRule; }
            set { newRule = value; }
        }
        
        public AddEditRule()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Constructor for initializing the GUi when editing a rule
        /// </summary>
        /// <param name="tmp"></param>
        public AddEditRule(BasicFirewall.Rule tmp)
        {
            InitializeComponent();

            // force the combo box to load the rules
            AddEditRule_Load(this, null);

            // disallow changing the base type of the rule
            comboBox1.Enabled = false;

            // set the rule type and GUI options
            if (tmp is BasicFirewall.AllRule)
            {
                BasicFirewall.AllRule t = (BasicFirewall.AllRule)tmp;
                checkBoxLog.Checked = t.log;

                // set the rule type
                comboBox1.SelectedIndex = 5;

                // set the direction
                checkBoxIn.Checked = ((t.direction & BasicFirewall.Direction.IN) != 0) ? true : false;
                checkBoxOut.Checked = ((t.direction & BasicFirewall.Direction.OUT) != 0) ? true : false;

                // set the action box
                comboBoxAction.SelectedIndex = ((t.ps & BasicFirewall.PacketStatus.ALLOWED) != 0) ? 1 : 0;
            }
            else if (tmp is BasicFirewall.IPRule)
            {
                BasicFirewall.IPRule t = (BasicFirewall.IPRule)tmp;
                checkBoxLog.Checked = t.log;

                //idx
                comboBox1.SelectedIndex = 6;

                //direction
                checkBoxIn.Checked = ((t.direction & BasicFirewall.Direction.IN) != 0) ? true : false;
                checkBoxOut.Checked = ((t.direction & BasicFirewall.Direction.OUT) != 0) ? true : false;

                //action
                comboBoxAction.SelectedIndex = ((t.ps & BasicFirewall.PacketStatus.ALLOWED) != 0) ? 1 : 0;

                //args
                textBoxArguments.Text = t.ip.ToString();
            }
            else if (tmp is BasicFirewall.TCPAllRule)
            {
                BasicFirewall.TCPAllRule t = (BasicFirewall.TCPAllRule)tmp;
                checkBoxLog.Checked = t.log;

                //idx
                comboBox1.SelectedIndex = 0;

                //dir
                checkBoxIn.Checked = ((t.direction & BasicFirewall.Direction.IN) != 0) ? true : false;
                checkBoxOut.Checked = ((t.direction & BasicFirewall.Direction.OUT) != 0) ? true : false;

                //action
                comboBoxAction.SelectedIndex = ((t.ps & BasicFirewall.PacketStatus.ALLOWED) != 0) ? 1 : 0;
            }
            else if (tmp is BasicFirewall.TCPIPPortRule)
            {
                BasicFirewall.TCPIPPortRule t = (BasicFirewall.TCPIPPortRule)tmp;
                checkBoxLog.Checked = t.log;

                //idx
                comboBox1.SelectedIndex = 1;

                //dir
                checkBoxIn.Checked = ((t.direction & BasicFirewall.Direction.IN) != 0) ? true : false;
                checkBoxOut.Checked = ((t.direction & BasicFirewall.Direction.OUT) != 0) ? true : false;

                //action
                comboBoxAction.SelectedIndex = ((t.ps & BasicFirewall.PacketStatus.ALLOWED) != 0) ? 1 : 0;

                //args
                textBoxArguments.Text = String.Format("{0} {1}", t.ip, t.port);
            }
            else if (tmp is BasicFirewall.TCPPortRule)
            {
                BasicFirewall.TCPPortRule t = (BasicFirewall.TCPPortRule)tmp;
                checkBoxLog.Checked = t.log;

                //idx
                comboBox1.SelectedIndex = 2;

                //dir
                checkBoxIn.Checked = ((t.direction & BasicFirewall.Direction.IN) != 0) ? true : false;
                checkBoxOut.Checked = ((t.direction & BasicFirewall.Direction.OUT) != 0) ? true : false;

                //action
                comboBoxAction.SelectedIndex = ((t.ps & BasicFirewall.PacketStatus.ALLOWED) != 0) ? 1 : 0;

                //args; convert the list of ints to a list of strings, return it as an array, and String.join all of them together with a space
                textBoxArguments.Text = String.Join(" ", t.port.ConvertAll<string>(delegate(int i) { return i.ToString(); }).ToArray());
            }
            else if (tmp is BasicFirewall.UDPAllRule)
            {
                BasicFirewall.UDPAllRule t = (BasicFirewall.UDPAllRule)tmp;
                checkBoxLog.Checked = t.log;

                //idx
                comboBox1.SelectedIndex = 3;

                //dir
                checkBoxIn.Checked = ((t.direction & BasicFirewall.Direction.IN) != 0) ? true : false;
                checkBoxOut.Checked = ((t.direction & BasicFirewall.Direction.OUT) != 0) ? true : false;

                //action
                comboBoxAction.SelectedIndex = ((t.ps & BasicFirewall.PacketStatus.ALLOWED) != 0) ? 1 : 0;
            }
            else if (tmp is BasicFirewall.UDPPortRule)
            {
                BasicFirewall.UDPPortRule t = (BasicFirewall.UDPPortRule)tmp;
                checkBoxLog.Checked = t.log;

                //idx
                comboBox1.SelectedIndex = 4;

                //dir
                checkBoxIn.Checked = ((t.direction & BasicFirewall.Direction.IN) != 0) ? true : false;
                checkBoxOut.Checked = ((t.direction & BasicFirewall.Direction.OUT) != 0) ? true : false;

                //action
                comboBoxAction.SelectedIndex = ((t.ps & BasicFirewall.PacketStatus.ALLOWED) != 0) ? 1 : 0;

                //args
                textBoxArguments.Text = String.Join(" ", t.port.ConvertAll<string>(delegate(int i) { return i.ToString(); }).ToArray());
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string tmp = "";
            bool enableArgs = true;

            switch (comboBox1.SelectedIndex)
            {
                case 0:
                case 3:
                case 5:
                    // most of these aren't going to translate very differently 
                    // into their foreign tongue, so i'm leaving it as is
                    tmp = "No args";
                    enableArgs = false;
                    break;
                case 2:
                case 4:
                    tmp = "(Space Separated) Port";
                    break;
                case 1:
                    tmp = "(Space Separated) IP Port";
                    break;
                case 6:
                    tmp = "IP";
                    break;
            }

            labelArgs.Text = tmp;
            textBoxArguments.Enabled = enableArgs;
        }

        /// <summary>
        /// Event handles the OK button to generate a new rule
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            if (checkBoxIn.Checked || checkBoxOut.Checked)
            {
                try
                {
                    BasicFirewall.RuleType rt = BasicFirewall.RuleType.ALL;
                    switch (comboBox1.SelectedIndex)
                    {
                        case 3:
                            rt = BasicFirewall.RuleType.UDPALL;
                            break;
                        case 4:
                            rt = BasicFirewall.RuleType.UDPPORT;
                            break;
                        case 1:
                            rt = BasicFirewall.RuleType.TCPIPPORT;
                            break;
                        case 2:
                            rt = BasicFirewall.RuleType.TCPPORT;
                            break;
                        case 0:
                            rt = BasicFirewall.RuleType.TCPALL;
                            break;
                        case 5:
                            rt = BasicFirewall.RuleType.ALL;
                            break;
                        case 6:
                            rt = BasicFirewall.RuleType.IP;
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

                    // multiple ports and the ip:port rule are parsed later on, so send all args as a string

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

        /// <summary>
        /// Event handles the cancel button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void AddEditRule_Load(object sender, EventArgs e)
        {
            switch (LanguageConfig.GetCurrentLanguage())
            {
                case LanguageConfig.Language.ENGLISH:
                    comboBox1.Items.Add("All TCP Rule");//0
                    comboBox1.Items.Add("TCP IP and Port Rule");//1
                    comboBox1.Items.Add("TCP Port Rule");//2
                    comboBox1.Items.Add("All UDP Rule");//3
                    comboBox1.Items.Add("UDP Port Rule");//4
                    comboBox1.Items.Add("All Rule");//5
                    comboBox1.Items.Add("IP Rule");//6
                    break;
                case LanguageConfig.Language.PORTUGUESE:
                    comboBox1.Items.Add("Todos Regra TCP");
                    comboBox1.Items.Add("TCP IP e regra de porta");
                    comboBox1.Items.Add("Porta TCP Regra");
                    comboBox1.Items.Add("Todos Regra UDP");
                    comboBox1.Items.Add("Porta UDP Regra");
                    comboBox1.Items.Add("Todos Regra");
                    comboBox1.Items.Add("Regra IP");
                    break;
                case LanguageConfig.Language.CHINESE:
                    comboBox1.Items.Add("所有的TCP規則");
                    comboBox1.Items.Add("TCP IP和端口規則");
                    comboBox1.Items.Add("TCP端口規則");
                    comboBox1.Items.Add("所有的UDP規則");
                    comboBox1.Items.Add("UDP端口規則");
                    comboBox1.Items.Add("所有規則");
                    comboBox1.Items.Add("IP规则");
                    break;
                case LanguageConfig.Language.GERMAN:
                    comboBox1.Items.Add("Alle TCP Rule");
                    comboBox1.Items.Add("TCP IP und Port Regel");
                    comboBox1.Items.Add("TCP Port Rule");
                    comboBox1.Items.Add("Alle UDP Regel");
                    comboBox1.Items.Add("UDP Port Rule");
                    comboBox1.Items.Add("Alle Rule");
                    comboBox1.Items.Add("IP Rule");
                    break;
                case LanguageConfig.Language.RUSSIAN:
                    comboBox1.Items.Add("Все правила TCP");
                    comboBox1.Items.Add("TCP-IP и порт Правило");
                    comboBox1.Items.Add("TCP-порт Правило");
                    comboBox1.Items.Add("Все правила UDP");
                    comboBox1.Items.Add("UDP-порта Правило");
                    comboBox1.Items.Add("Все правила");
                    comboBox1.Items.Add("IP правила");
                    break;
                case LanguageConfig.Language.SPANISH:
                    comboBox1.Items.Add("Todos los Regla TCP");
                    comboBox1.Items.Add("TCP IP y puerto de Regla");
                    comboBox1.Items.Add("Puerto TCP Regla");
                    comboBox1.Items.Add("Todos los Regla UDP");
                    comboBox1.Items.Add("El puerto UDP Regla");
                    comboBox1.Items.Add("todos los Regla");
                    comboBox1.Items.Add("Regla IP");
                    break;
            }
        }
    }
}
