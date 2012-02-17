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

        BasicFirewall.Rule dragged = null;

        private void listBox1_MouseDown(object sender, MouseEventArgs e)
        {
            int index = this.listBox1.IndexFromPoint(new Point(e.X, e.Y));
            if (index == -1) return;
            dragged = (BasicFirewall.Rule)listBox1.Items[index];
            listBox1.SelectedIndex = index;
        }

        private void listBox1_MouseUp(object sender, MouseEventArgs e)
        {
            if (dragged == null) return;
            int index = this.listBox1.IndexFromPoint(new Point(e.X, e.Y));
            if (listBox1.Items.IndexOf(dragged) == index) return;
            if (index < 0) index = this.listBox1.Items.Count - 1;
            this.listBox1.Items.Remove(dragged);
            this.listBox1.Items.Insert(index, dragged);
            dragged = null;

            List<BasicFirewall.Rule> r = new List<BasicFirewall.Rule>();
            foreach (object rule in listBox1.Items)
            {
                r.Add((BasicFirewall.Rule)rule);
            }

            basicfirewall.InstanceGetRuleUpdates(r);
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
        }
    }
}
