using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Data.SQLite;
using System.IO;
using FM;

namespace BandwidthAnalyzer
{
    public partial class ConnectionGraphControl : UserControl
    {
        public ConnectionGraphControl(BandwidthAnalyzer module)
        {
            InitializeComponent();
            formatCombobox.SelectedIndex = 0;
            updateMaxValuesTextBox();
        }

        private void entriesTickBar_ValueChanged(object sender, EventArgs e)
        {
            updateMaxValuesTextBox();
        }

        private void updateMaxValuesTextBox()
        {
            maxValuesTextBox.Text = entriesTickBar.Value + "";
        }

        private void maxValuesTextBox_TextChanged(object sender, EventArgs e)
        {

            if (!String.IsNullOrEmpty(maxValuesTextBox.Text))
            {
                int maxNum = 0;
                try
                {
                    maxNum = Int32.Parse(maxValuesTextBox.Text);
                }
                catch (Exception)
                {
                    string cleaned = (new Regex(@"[^\d]")).Replace(maxValuesTextBox.Text, "");

                    if (!String.IsNullOrEmpty(cleaned))
                    {
                        maxNum = Int32.Parse(cleaned);
                    }
                    maxValuesTextBox.Text = cleaned;
                }
                entriesTickBar.Value = maxNum > entriesTickBar.Maximum ? entriesTickBar.Maximum : maxNum;
            }

        }

        private string matchCheckbox(CheckBox checkbox)
        {

            string name;

            if (checkbox == tcpCheckbox) {
                name = BandwidthAnalyzer.TCP;
            } else if (checkbox == udpCheckbox) {
                name = BandwidthAnalyzer.UDP;
            } else if (checkbox == eethCheckbox) {
                name = BandwidthAnalyzer.EETH;
            } else if (checkbox == ethernetCheckbox) {
                name = BandwidthAnalyzer.ETHERNET;
            } else if (checkbox == ipCheckbox) {
                name = BandwidthAnalyzer.IP;
            } else if (checkbox == icmpv6Checkbox) {
                name = BandwidthAnalyzer.ICMPV6;
            } else if (checkbox == arpCheckbox) {
                name = BandwidthAnalyzer.ARP;
            } else if (checkbox == icmpCheckbox) {
                name = BandwidthAnalyzer.ICMP;
            } else if (checkbox == dnsCheckbox) {
                name = BandwidthAnalyzer.DNS;
            } else if (checkbox == dhcpCheckbox) {
                name = BandwidthAnalyzer.DHCP;
            } else if (checkbox == snmpCheckbox) {
                name = BandwidthAnalyzer.SNMP;
            } else {
                name = null;
            }

            return name;
        }

        private void generateReportButton_Click(object sender, EventArgs e)
        {
            // get the filepath
            string filepath = BandwidthAnalyzer.GetDBPath() + Path.DirectorySeparatorChar +  BandwidthAnalyzer.DB_NAME;

            // process the file line by line based on the selected filters
            try
            {
                int packetsOut = 0;
                int packetsIn = 0;
                BandwidthCounter bandwidthOut = new BandwidthCounter();
                BandwidthCounter bandwidthIn = new BandwidthCounter();

                if (File.Exists(filepath))
                {

                    // get the filter parameters
                    long startTime = startDatePicker.Value.Ticks;
                    long endTime = endDatePicker.Value.Ticks;

                    DateTime accessTime = File.GetLastAccessTime(filepath);

                    try
                    {
                        SQLiteConnection db = new SQLiteConnection();
                        db.ConnectionString = new SQLiteConnectionStringBuilder()
                        {
                            {"Data Source", BandwidthAnalyzer.GetDBPath() + Path.DirectorySeparatorChar + "connection_db.db"},
                            {"Version", "3"},
                            {"FailIfMissing", "False"}
                        }.ConnectionString;
                        db.Open();

                        CheckBox[] checkboxes = new CheckBox[] { 
                            tcpCheckbox, udpCheckbox, eethCheckbox, ethernetCheckbox, ipCheckbox,
                            icmpv6Checkbox, arpCheckbox, icmpCheckbox, dnsCheckbox, dhcpCheckbox,
                            snmpCheckbox
                        };

                        string protocolList = "";
                        foreach (CheckBox checkbox in checkboxes) {
                            if (checkbox.Checked)
                                protocolList += ",'" + matchCheckbox(checkbox) + "'";
                        }
                        protocolList = protocolList.Remove(0, 1);

                        string timeFilter = "";
                        if (timestampCheckbox.Checked)
                        {
                            timeFilter = "timestamp between " + startTime + " and " + endTime;
                        }

                        string query = "select * from connection_log where protocol in (" + protocolList + ")" + timeFilter;

                        SQLiteCommand command = new SQLiteCommand(query, db);

                        using (SQLiteDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                bool outgoing = reader.GetBoolean(0);
                                long timestamp = reader.GetInt64(1);
                                string ip = reader.GetString(2);
                                uint size = (uint) reader.GetInt32(3);
                                string protocol = reader.GetString(4);

                                if (outgoing)
                                {
                                    ++packetsOut;
                                    bandwidthOut.AddBits(size);
                                }
                                else
                                {
                                    ++packetsIn;
                                    bandwidthIn.AddBits(size);
                                }
                            }
                        }
                        db.Close();

                        MessageBox.Show("Analysis of connections logged as of " + accessTime +
                            "\nTotal Packets Sent: " + packetsOut +
                            "\nTotal Packets Received: " + packetsIn +
                            "\nTotal Outgoing Bandwidth: " + bandwidthOut.ToString() +
                            "\nTotal Incoming Bandwidth: " + bandwidthIn.ToString());
                    }
                    catch (Exception ex)
                    {
                        PassThru.LogCenter.WriteErrorLog(ex);
                    }

                    



                }
                else
                {
                    MessageBox.Show("No data available.  This module needs to be enabled in " +
                        "order for it to collect data to analyze.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void clearButton_Click(object sender, EventArgs e)
        {
            BandwidthAnalyzer.DO_CLEAR_LOG = true;
            MessageBox.Show("Log will be cleared on next write or system exit.");
        }
    }
}
