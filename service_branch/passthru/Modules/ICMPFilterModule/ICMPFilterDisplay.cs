using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace PassThru
{
    /// <summary>
    /// ICMP filter display class
    /// </summary>
    public partial class ICMPFilterDisplay : UserControl
    {
        // global filter obj
        private ICMPFilterModule filter;

        // table of user rule mappings
        // key: type
        // value: list<string> of codes
        private SerializableDictionary<string, List<string>> ruletable;

        // table of ICMP mappings
        // key: type
        // value: dictionary of codes and descriptions
        private Dictionary<string, Dictionary<string, string>> icmpList = new Dictionary<string, Dictionary<string, string>>();

        // constructor initializes the local ICMPFilter object and the UI
        public ICMPFilterDisplay(ICMPFilterModule filter)
        {
            this.filter = filter;
            ruletable = new SerializableDictionary<string, List<string>>(filter.data.RuleTable);
            buildICMPList();
            InitializeComponent();
        }

       /// <summary>
       /// Parse input and add to the blocked box
       /// </summary>
       /// <param name="sender"></param>
       /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            List<string> temp = new List<String>();
            string[] codes;
            string type;

            // remove spaces and split on comma
            codes = codeField.Text.Replace(" ", string.Empty).Split(',');
            type = typeField.Text;

            // if the type exists
            if (icmpList.ContainsKey(type))
            {
                // iterate through codes and add them to the table and hashtable
                foreach (string s in codes)
                {
                    // if the code exists
                    if (icmpList[type].ContainsKey(s))
                    {
                        // if that type already exists
                        if (ruletable.ContainsKey(type))
                        {
                            // get the list of the key
                            ruletable.TryGetValue(type, out temp);
                            // don't allow duplicate entries
                            if (temp.Contains(s))
                                continue;
                            // add the code to the list
                            temp.Add(s);
                            // assignment of updated kv pair
                            ruletable[type] = temp;
                        }

                        // if the type doesn't exist yet, create it
                        else
                        {
                            temp.Add(s);
                            ruletable.Add(type, temp);
                        }

                        object[] row = { type, s, icmpList[type][s] };
                        tableDisplay.Rows.Add(row);
                    }
                }
            }
            
            // consume input
            codeField.Text = "";
            typeField.Text = "";

            // update our rule table
            UpdateRuleTable();
        }

        /*
         * Method handles the delete button
         * 
         * @param sender
         * @param e
         */
        private void button1_Click_1(object sender, EventArgs e)
        {
            // if nothing's been selected, get out
            if (tableDisplay.SelectedRows.Count <= 0)
                return;

            // grab the type/code/rowIdx from the table
            int rowIdx = tableDisplay.SelectedCells[0].RowIndex;
            string type = tableDisplay["Type", rowIdx].Value.ToString();
            string code = tableDisplay["Code", rowIdx].Value.ToString();

            // if ruletable contains the key (it should)
            if (ruletable.ContainsKey(type))
            {
                List<string> temp;
                ruletable.TryGetValue(type, out temp);
                // check if the code list of the key contains the code
                if (temp.Contains(code))
                {
                    // remove code from temp list
                    temp.Remove(code);
                    // assignment of updated kv pair
                    ruletable[type] = temp;
                    // remove from display table
                    tableDisplay.Rows.Remove(tableDisplay.Rows[rowIdx]);
                }
            }

            // update our rule table
            UpdateRuleTable();
        }

        /*
         * Loads settings back up into the table
         * 
         * @param sender
         * @param e
         */
        private void ICMPFilterDisplay_Load(object sender, EventArgs e)
        {
            // generate the ruletable
            ruletable = new SerializableDictionary<string, List<string>>(filter.data.RuleTable);

            // rebuild the datagrid
            rebuildTable();

            // load up the deny all state
            if (filter.data.DenyAll)
            {
                allBox.CheckState = CheckState.Checked;
            }
        }

        // pushes update to the ruletable object
        private void UpdateRuleTable()
        {
            this.filter.data.RuleTable = ruletable;
        }

        /*
         * Method handles the All ICMP check box; 
         * Disables the type/code fields if selected, enables if not.
         * Updates the denyAll bool in the module.
         * Disables the add/delete button.
        */
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            typeField.Enabled = !(allBox.Checked);
            codeField.Enabled = !(allBox.Checked);
            addButton.Enabled = !(allBox.Checked);
            deleteButton.Enabled = !(allBox.Checked);
            viewICMP.Enabled = !(allBox.Checked);
            this.filter.data.DenyAll = allBox.Checked;
        }

        /*
         * Method handles the View ICMP button;
         * Displays all available ICMP types/codes
         */
        private void button1_Click_2(object sender, EventArgs e)
        {
            // if they're viewing the ruletable
            if (typeField.Enabled)
            {
                tableDisplay.Rows.Clear();

                // get all the keys
                ICollection keys = icmpList.Keys;
                foreach (string s in keys)
                {
                    // get all the codes
                    Dictionary<string, string> temp = icmpList[s];
                    ICollection codes = temp.Keys;
                    foreach (string c in codes)
                    {
                        // add each to the table
                        object[] t = { s, c, icmpList[s][c] };
                        tableDisplay.Rows.Add(t);
                    }
                }
                // set button text
                viewICMP.Text = "Hide ICMP";
            }

            // if they're viewing the ICMP stuff
            else
            {
                // clear it and rebuild from the ruletable
                tableDisplay.Rows.Clear();
                rebuildTable();

                // update button text
                viewICMP.Text = "View ICMP";
            }

            // swap the fields and buttons based on !(state)
            typeField.Enabled = !(typeField.Enabled);
            codeField.Enabled = !(codeField.Enabled);
            addButton.Enabled = !(addButton.Enabled);
            deleteButton.Enabled = !(deleteButton.Enabled);
            allBox.Enabled = !(allBox.Enabled);
        }

        /// <summary>
        /// Rebuilds the datagridview
        /// </summary>
        private void rebuildTable()
        {
            ICollection keys = ruletable.Keys;
            List<string> lVal;

            // iterate through each type
            foreach (string type in keys)
            {
                // get the list of codes from the key
                ruletable.TryGetValue(type, out lVal);

                // iterate through the codes, adding them to the table
                // with the appropriate description
                foreach (string code in lVal)
                {
                    object[] row = { type, code, icmpList[type][code] };
                    tableDisplay.Rows.Add(row);
                }
            }
        }

        /*
         * Builds the ICMP type/code/description object for desc retrieval
         * http://www.iana.org/assignments/icmp-parameters/icmp-parameters.xml
         * 
         * iterate through all 41 supported ICMP types, add the codes, 
         * then add the type with the temp dict.
         */
        private void buildICMPList()
        {
            for (int i = 0; i <= 41; ++i)
            {
                Dictionary<string, string> temp = new Dictionary<string, string>();
                switch (i)
                {
                    // echo
                    case 0:
                        temp.Add("0", "Echo Reply");
                        icmpList.Add("0", temp);
                        break;

                    // reserve
                    case 1: 
                        temp.Add("0", "Reserved");
                        temp.Add("1", "Reserved");
                        icmpList.Add("1", temp);
                        break;

                    // reserve
                    case 2:
                        temp.Add("0", "Reserved");
                        temp.Add("1", "Reserved");
                        icmpList.Add("2", temp);
                        break;

                    // destination unreachable
                    case 3:
                        temp.Add("0", "Destination Network Unreachable");
                        temp.Add("1", "Destination Host Unreachable");
                        temp.Add("2", "Destination Protocol Unreachable");
                        temp.Add("3", "Destination Port Unreachable");
                        temp.Add("4", "Fragmentation Required, and DF Flag Set");
                        temp.Add("5", "Source Route Failed");
                        temp.Add("6", "Destination Network Unknown");
                        temp.Add("7", "Destination Host Unknown");
                        temp.Add("8", "Source Host Isolated");
                        temp.Add("9", "Network Administratively Prohibited");
                        temp.Add("10", "Host Administratively Prohibited");
                        temp.Add("11", "Network Unreachable for TOS");
                        temp.Add("12", "Host Unreachable for TOS");
                        temp.Add("13", "Communication Administratively Prohibited");
                        icmpList.Add("3", temp);
                        break;

                    // source quench
                    case 4:
                        temp.Add("0", "Source Quenched(Congestion Control)");
                        icmpList.Add("4", temp);
                        break;

                    // redirect message
                    case 5:
                        temp.Add("0", "Redirect Datagram For the Network");
                        temp.Add("1", "Redirect Datagram For the Host");
                        temp.Add("2", "Redirect Datagram For the TOS & Network");
                        temp.Add("3", "Redirect Datagram For the TOS & Host");
                        icmpList.Add("5", temp);
                        break;

                    // alt host addr
                    case 6:
                        temp.Add("0", "Alternate Host Address");
                        icmpList.Add("6", temp);
                        break;

                    // alt host addr
                    case 7:
                        temp.Add("0", "Reserved");
                        icmpList.Add("7", temp);
                        break;

                    // echo request
                    case 8:
                        temp.Add("0", "Echo Request (Used to Ping)");
                        icmpList.Add("8", temp);
                        break;

                    // router advertisement
                    case 9:
                        temp.Add("0", "Router Advertisement");
                        icmpList.Add("9", temp);
                        break;

                    // router solicitation
                    case 10:
                        temp.Add("0", "Router Discovery/Selection/Solicitation");
                        icmpList.Add("10", temp);
                        break;

                    // time exceeded
                    case 11:
                        temp.Add("0", "TTL Expired in Transit");
                        temp.Add("1", "Fragment Reassembly Time Exceeded");
                        icmpList.Add("11", temp);
                        break;

                     // bad IP header
                    case 12:
                        temp.Add("0", "Pointer Indicates the Error");
                        temp.Add("1", "Missing A Required Option");
                        temp.Add("2", "Bad Length");
                        icmpList.Add("12", temp);
                        break;

                     // Timestamp
                    case 13:
                        temp.Add("0", "Timestamp");
                        icmpList.Add("13", temp);
                        break;
                    
                     // Timestamp reply
                    case 14:
                        temp.Add("0", "Timestamp Reply");
                        icmpList.Add("14", temp);
                        break;

                     // information request
                    case 15:
                        temp.Add("0", "Information Request");
                        icmpList.Add("15", temp);
                        break;

                     // information reply
                    case 16:
                        temp.Add("0", "Information Reply");
                        icmpList.Add("16", temp);
                        break;

                     // address mask request
                    case 17:
                        temp.Add("0", "Address Mask Request");
                        icmpList.Add("17", temp);
                        break;

                     // Address mask reply
                    case 18:
                        temp.Add("0", "Address Mask Reply");
                        icmpList.Add("18", temp);
                        break;

                     // Reserved
                    case 19:
                        temp.Add("0", "Reserved For Security");
                        icmpList.Add("19", temp);
                        break;

                    // traceroute
                    case 30:
                        temp.Add("0", "Information Request");
                        icmpList.Add("30", temp );
                        break;
                    
                    // datagram 
                    case 31:
                        temp.Add("0", "Datagram Conversion Error");
                        icmpList.Add("31", temp );
                        break;

                    // mobile host redirect
                    case 32:
                        temp.Add("0", "Mobile Host Redirect");
                        icmpList.Add("32", temp );
                        break;

                    // where-are-you
                    case 33:
                        temp.Add("0", "Where-Are-You (Originally meant for IPv6)");
                        icmpList.Add("33", temp );
                        break;

                    // here-i-am
                    case 34:
                        temp.Add("0", "Here-I-Am (Originally meant for IPv6)");
                        icmpList.Add("34", temp );
                        break;

                    // mobile reg
                    case 35:
                        temp.Add("0", "Mobile Registration Request");
                        icmpList.Add("35", temp );
                        break;

                    // mobile reg
                    case 36:
                        temp.Add("0", "Mobile Registration Reply");
                        icmpList.Add("36", temp );
                        break;

                    // domain name request
                    case 37:
                        temp.Add("0", "Domain Name Request");
                        icmpList.Add("37", temp );
                        break;

                    // domain name reply
                    case 38:
                        temp.Add("0", "Domain Name Reply");
                        icmpList.Add("38", temp );
                        break;
                
                    // SKIP
                    case 39:
                        temp.Add("0", "SKIP Algorithm Discovery Protocol");
                        icmpList.Add("39", temp );
                        break;

                    // Photuris
                    case 40:
                        temp.Add("0", "Photuris, Security Failures");
                        icmpList.Add("40", temp );
                        break;

                    // experimental ICMP
                    case 41:
                        temp.Add("0", "ICMP For Experimental Mobility Protocols");
                        icmpList.Add("41", temp );
                        break;
                }
            }
        }
    }
}
