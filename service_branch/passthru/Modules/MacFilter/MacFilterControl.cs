using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace PassThru.Modules.MacFilter
{
    public partial class MacFilterControl : UserControl
    {
        MacFilterModule mf;
        public MacFilterControl(MacFilterModule mf)
        {
            this.mf = mf;
            InitializeComponent();
        }

        MacFilterModule.MacRule dragged = null;

        private void listBox1_MouseDown(object sender, MouseEventArgs e)
        {
            int index = this.listBox1.IndexFromPoint(new Point(e.X, e.Y));
            if (index == -1) return;
            dragged = (MacFilterModule.MacRule)listBox1.Items[index];
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

            List<MacFilterModule.MacRule> r = new List<MacFilterModule.MacRule>();
            foreach (object rule in listBox1.Items)
            {
                r.Add((MacFilterModule.MacRule)rule);
            }

            mf.InstanceGetRuleUpdates(r);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                AddEditMacRule aer = new AddEditMacRule();
                if (aer.ShowDialog() == DialogResult.OK)
                {
                    listBox1.Items.Add(aer.newRule);
                    List<MacFilterModule.MacRule> r = new List<MacFilterModule.MacRule>();
                    foreach (object rule in listBox1.Items)
                    {
                        r.Add((MacFilterModule.MacRule)rule);
                    }

                    mf.InstanceGetRuleUpdates(r);
                }
                aer.Dispose();
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

                List<MacFilterModule.MacRule> r = new List<MacFilterModule.MacRule>();
                foreach (object rule in listBox1.Items)
                {
                    r.Add((MacFilterModule.MacRule)rule);
                }

                mf.InstanceGetRuleUpdates(r);
            }
            catch { }
        }

        private void MacFilterControl_Load(object sender, EventArgs e)
        {
            listBox1.DisplayMember = "String";
            List<MacFilterModule.MacRule> r = new List<MacFilterModule.MacRule>(mf.rules);
            foreach (MacFilterModule.MacRule rule in r)
            {
                listBox1.Items.Add(rule);
            }
        }
    }
}
