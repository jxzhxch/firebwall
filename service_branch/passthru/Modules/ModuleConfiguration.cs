using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using FM;

namespace PassThru.Modules
{
    public partial class ModuleConfiguration : UserControl
    {
        List<FirewallModule> modules = new List<FirewallModule>();
        NetworkAdapter na;
        string dragged = null;
        
        public ModuleConfiguration(NetworkAdapter na)
        {
            this.na = na;
            for (int x = 0; x < na.modules.Count; x++)
            {
                modules.Add(na.modules.GetModule(x));
            }
            InitializeComponent();
        }

        void UpdateView()
        {
            if (this.checkedListBoxModules.InvokeRequired)
            {
                System.Threading.ThreadStart ts = new System.Threading.ThreadStart(UpdateView);
                this.checkedListBoxModules.Invoke(ts);
            }
            else
            {
                checkedListBoxModules.Items.Clear();
                for (int x = 0; x < modules.Count; x++)
                {
                    checkedListBoxModules.Items.Add(modules[x].MetaData.Name, modules[x].Enabled);
                }
            }
        }

        private void checkedListBoxModules_MouseDown(object sender, MouseEventArgs e)
        {
            int index = this.checkedListBoxModules.IndexFromPoint(new Point(e.X, e.Y));
            if (index == -1) return;
            dragged = (string)checkedListBoxModules.Items[index];
            checkedListBoxModules.SelectedIndex = index;
        }

        private void checkedListBoxModules_MouseUp(object sender, MouseEventArgs e)
        {
            if (dragged == null) return;
            int index = this.checkedListBoxModules.IndexFromPoint(new Point(e.X, e.Y));
            int oindex = checkedListBoxModules.Items.IndexOf(dragged);
            if (checkedListBoxModules.Items.IndexOf(dragged) == index) return;
            if (index < 0) index = this.checkedListBoxModules.Items.Count - 1;
            this.checkedListBoxModules.Items.Remove(dragged);
            this.checkedListBoxModules.Items.Insert(index, dragged);
            dragged = null;

            na.modules.InsertPIndex(oindex, index);

            modules.Clear();
            for (int x = 0; x < na.modules.Count; x++)
            {
                modules.Add(na.modules.GetModule(x));
            }

            UpdateView();
        }

        private void ModuleConfiguration_Load(object sender, EventArgs e)
        {
            UpdateView();
        }

        private void checkedListBoxModules_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (e.NewValue == CheckState.Checked)
                modules[e.Index].Enabled = true;
            else
                modules[e.Index].Enabled = false;
        }
    }
}
