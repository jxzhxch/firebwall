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
    /*
     *  Class implements the Help tab 
     */
    public partial class Help : UserControl
    {
        // list of modules associated with the first adapter.
        // They all have the same, anyway.
        private ModuleList list;

        public Help()
        {
            InitializeComponent();            
        }

        // set the labels when the user flips through them
        private void modBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (modBox.SelectedIndex >= 0)
            {
                FirewallModule temp = list.GetModule(modBox.SelectedIndex);

                modData.Text = temp.MetaData.Name;
                modAuthorField.Text = temp.MetaData.Author;
                modContactField.Text = temp.MetaData.Contact;
                modVersionField.Text = temp.MetaData.Version;
                modDescriptionField.Text = temp.MetaData.Description;
                modHelpBox.Text = temp.MetaData.HelpString;
            }
        }

        private void Help_Load(object sender, EventArgs e)
        {
            // grab the first adapter
            NetworkAdapter first_adapter = NetworkAdapter.GetAllAdapters()[0];
            // get it's module list
            list = first_adapter.modules;

            // add it to the box
            for (int i = 0; i < list.Count; ++i)
            {
                modBox.Items.Insert(i, list.GetModule(i).MetaData.Name);
            }
        }
    }
}
