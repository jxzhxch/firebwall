using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace PassThru
{
    public partial class BasicFirewallControl : UserControl
    {
        BasicFirewall basicfirewall;

        public BasicFirewallControl(BasicFirewall basicfirewall)
        {
            this.basicfirewall = basicfirewall;            
            InitializeComponent();
        }

        private void buttonMoveUp_Click(object sender, EventArgs e)
        {
            try
            {
                int index = listBox1.SelectedIndex;
                if (index != 0)
                {
                    BasicFirewall.Rule rule = (BasicFirewall.Rule)listBox1.Items[index];
                    listBox1.Items.RemoveAt(index);
                    index--;
                    listBox1.Items.Insert(index, rule);
                    listBox1.SelectedIndex = index;
                    List<BasicFirewall.Rule> r = new List<BasicFirewall.Rule>();
                    foreach (object ru in listBox1.Items)
                    {
                        r.Add((BasicFirewall.Rule)ru);
                    }

                    basicfirewall.InstanceGetRuleUpdates(r);
                }
            }
            catch { }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                AddEditRule aer = new AddEditRule();
                if (aer.ShowDialog() == DialogResult.OK)
                {
                    listBox1.Items.Add(aer.NewRule);
                    List<BasicFirewall.Rule> r = new List<BasicFirewall.Rule>();
                    foreach (object rule in listBox1.Items)
                    {
                        r.Add((BasicFirewall.Rule)rule);
                    }

                    basicfirewall.InstanceGetRuleUpdates(r);
                }
            }
            catch (Exception exception)
            {
                LogCenter.WriteErrorLog(exception);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (listBox1.SelectedItem == null) return;
                listBox1.Items.RemoveAt(listBox1.SelectedIndex);

                List<BasicFirewall.Rule> r = new List<BasicFirewall.Rule>();
                foreach (object rule in listBox1.Items)
                {
                    r.Add((BasicFirewall.Rule)rule);
                }

                basicfirewall.InstanceGetRuleUpdates(r);
            }
            catch { }
        }

        private void BasicFirewallControl_Load(object sender, EventArgs e)
        {
            listBox1.DisplayMember = "String";
            List<BasicFirewall.Rule> r = new List<BasicFirewall.Rule>(basicfirewall.rules);
            foreach (BasicFirewall.Rule rule in r)
            {
                listBox1.Items.Add(rule);
            }
            switch (FM.LanguageConfig.GetCurrentLanguage())
            {
                case FM.LanguageConfig.Language.NONE:
                case FM.LanguageConfig.Language.ENGLISH:
                    button1.Text = "Add Rule";
                    button2.Text = "Remove Rule";
                    buttonMoveDown.Text = "Move Down";
                    buttonMoveUp.Text = "Move Up";
                    break;
                case FM.LanguageConfig.Language.PORTUGUESE:
                    button1.Text = "Adicionar regra";
                    button2.Text = "remover Regra";
                    buttonMoveDown.Text = "mover para Baixo";
                    buttonMoveUp.Text = "mover para cima";
                    break;
                case FM.LanguageConfig.Language.RUSSIAN:
                    button1.Text = "Добавить правило";
                    button2.Text = "Удалить правило";
                    buttonMoveDown.Text = "спускать";
                    buttonMoveUp.Text = "вверх";
                    break;
                case FM.LanguageConfig.Language.SPANISH:
                    button1.Text = "Añadir regla";
                    button2.Text = "Eliminar la regla";
                    buttonMoveDown.Text = "Bajar";
                    buttonMoveUp.Text = "Subir";
                    break;
                case FM.LanguageConfig.Language.CHINESE:
                    button1.Text = "新增规则";
                    button2.Text = "删除规则";
                    buttonMoveDown.Text = "下移";
                    buttonMoveUp.Text = "动起来";
                    break;
                case FM.LanguageConfig.Language.GERMAN:
                    button1.Text = "Regel hinzufügen";
                    button2.Text = "Regel entfernen";
                    buttonMoveDown.Text = "Nach unten";
                    buttonMoveUp.Text = "Nach oben";
                    break;
            }
        }

        private void buttonMoveDown_Click(object sender, EventArgs e)
        {
            try
            {
                int index = listBox1.SelectedIndex;
                if (index != listBox1.Items.Count - 1)
                {
                    BasicFirewall.Rule rule = (BasicFirewall.Rule)listBox1.Items[index];
                    listBox1.Items.RemoveAt(index);
                    index++;
                    listBox1.Items.Insert(index, rule);
                    listBox1.SelectedIndex = index;
                    List<BasicFirewall.Rule> r = new List<BasicFirewall.Rule>();
                    foreach (object ru in listBox1.Items)
                    {
                        r.Add((BasicFirewall.Rule)ru);
                    }

                    basicfirewall.InstanceGetRuleUpdates(r);
                }
            }
            catch { }
        }
    }
}
