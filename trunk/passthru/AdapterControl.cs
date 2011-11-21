﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Net.NetworkInformation;
using System.Threading;

namespace PassThru
{
		public partial class AdapterControl: UserControl 
        {
            Thread t;

            bool timing = true;

            void Timing()
            {
                try
                {
                    while (timing)
                    {
                        Thread.Sleep(5000);
                        UpdateAdapterList();
                    }
                }
                catch { }
            }

            public void Kill()
            {
                timing = false;
                t.Abort();
            }

			public AdapterControl() 
            {
				InitializeComponent();
				UpdateAdapterList();
                t = new Thread(new ThreadStart(Timing));
                t.Start();
                flowLayoutPanel1.SizeChanged += new EventHandler(flowLayoutPanel1_SizeChanged);
			}

            void flowLayoutPanel1_SizeChanged(object sender, EventArgs e)
            {
                foreach (Control c in flowLayoutPanel1.Controls)
                    c.Width = flowLayoutPanel1.Width - 5;
            }

			public void UpdateAdapterList() 
            {
                if (flowLayoutPanel1.InvokeRequired)
                {
                    ThreadStart t = new ThreadStart(UpdateAdapterList);
                    flowLayoutPanel1.Invoke(t);
                }
                else
                {
                    flowLayoutPanel1.Controls.Clear();
                    foreach (NetworkAdapter na in NetworkAdapter.GetAllAdapters())
                    {
                        AdapterDisplay ad = new AdapterDisplay(new AdapterInfo(na.Pointer, na.Name, na.InterfaceInformation, na.InBandwidth, na.OutBandwidth, na));
                        ad.Width = flowLayoutPanel1.Width - 5;
                        flowLayoutPanel1.Controls.Add(ad);
                    }
                }
			}
		}

        public class AdapterInfo
        {
            public AdapterInfo(string P, string N, NetworkInterface NI, BandwidthCounter In, BandwidthCounter Out, NetworkAdapter na)
            {
                pointer = P;
                deviceName = N;
                ni = NI;
                this.In = In;
                this.Out = Out;
                this.na = na;
            }

            public NetworkAdapter na;
            BandwidthCounter In;
            BandwidthCounter Out;

            string deviceName = "";
            NetworkInterface ni = null;

            public string IPv4
            {
                get
                {
                    if (ni == null)
                        return "";
                    else
                    {
                        foreach (UnicastIPAddressInformation ip in ni.GetIPProperties().UnicastAddresses)
                        {
                            if (ip.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                            {
                                return ip.Address.ToString();
                            }
                        }
                        return "";
                    }
                }
            }

            public string IPv6
            {
                get
                {
                    if (ni == null)
                        return "";
                    else
                    {
                        foreach (UnicastIPAddressInformation ip in ni.GetIPProperties().UnicastAddresses)
                        {
                            if (ip.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetworkV6)
                            {
                                return ip.Address.ToString();
                            }
                        }
                        return "";
                    }
                }
            }

            public string NIDescription
            {
                get
                {
                    if (ni == null)
                        return "";
                    else
                        return ni.Description;
                }
            }

            public string NIName
            {
                get
                {
                    if (ni == null)
                        return "";
                    else
                        return ni.Name;
                }
            }

            public string DataOut
            {
                get
                {
                    return Out.ToString();
                }
            }

            public string DataOutPerSecond
            {
                get
                {
                    return Out.GetPerSecond();
                }
            }

            public string DataIn
            {
                get
                {
                    return In.ToString();
                }
            }

            public string DataInPerSecond
            {
                get
                {
                    return In.GetPerSecond();
                }
            }

            public string Summary
            {
                get
                {
                    return NIName + ":\t" + IPv4 + "\t" + IPv6 + "\r\nIn(" + DataIn + " | " + DataInPerSecond + ")\tOut(" + DataOut + " | " + DataOutPerSecond + ")";
                }
            }

            string pointer = null;
        }
}
