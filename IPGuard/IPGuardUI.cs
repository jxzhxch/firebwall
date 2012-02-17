using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

/// This is the frontend to IPGuard.
namespace PassThru
{
    public partial class IPGuardUI : UserControl
    {
        private IPGuard g;

        public IPGuardUI(IPGuard g)
        {
            this.g = g;
            InitializeComponent();
        }

        /// <summary>
        /// Called from IPGuard to update the GUI with all the available lists
        /// </summary>
        public void available()
        {
            foreach (string s in g.Available_Block_Lists)
            {
                availableBox.Items.Add(s);
            }
        }

        /// <summary>
        /// Handles the add list button action.  First we add the item to the loaded
        /// box, then dump it off the available list, then go and build the ranges of that
        /// item.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addList_Click(object sender, EventArgs e)
        {
            try
            {
                String item = (String)availableBox.SelectedItem;

                // add the item to the loaded box
                loadedBox.Items.Add(item);
                // remove from availableBox
                availableBox.Items.Remove(item);
                // go and build stuff
                g.buildRanges(item);
            }
            catch { }
        }

        /// <summary>
        /// Handles the remove list button.  We first add it to the available box,
        /// then dump it off the loaded box, then go and rebuild the blocked ranges.  More
        /// info over at rebuild() on that.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void removeButton_Click(object sender, EventArgs e)
        {
            try
            {
                String item = (String)loadedBox.SelectedItem;

                // add the item to the availableBox
                availableBox.Items.Add(item);
                // remove from loaded box
                loadedBox.Items.Remove(item);
                // go and rebuild all the ranges
                g.rebuild();
            }
            catch { }
        } 

        /// <summary>
        /// Return whether we should be logging all blocked connections
        /// </summary>
        /// <returns></returns>
        public bool isLog()
        {
            return logBox.Checked;
        }

        /// <summary>
        /// Return whether we should block incoming packets at these IPs too
        /// </summary>
        /// <returns></returns>
        public bool isIncoming()
        {
            return incomingSelection.Checked;
        }
        
        /// <summary>
        /// Checks if a list is already loaded
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public bool isLoaded(String s)
        {
            return loadedBox.Items.Contains(s);
        }

        /// <summary>
        /// we don't keep any data structures hanging around for the currently loaded
        /// lists because they're kinda already there.  This method retrieves it.
        /// </summary>
        /// <returns></returns>
        public List<Object> getLoadedLists()
        {
            List<Object> tmp = new List<Object>();
            foreach (Object o in loadedBox.Items)
            {
                tmp.Add(o);
            }
            return tmp;
        }
    }
}
