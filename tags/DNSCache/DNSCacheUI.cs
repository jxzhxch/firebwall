﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace DNSCache
{
    public partial class DNSCacheUI : UserControl
    {
        DNSCache cache;
        public DNSCacheUI(DNSCache cache)
        {
            InitializeComponent();
            this.cache = cache;
        }

        private void DNSCacheUI_Load(object sender, EventArgs e)
        {
            UpdateList();
            cache.CacheUpdate += new System.Threading.ThreadStart(cache_CacheUpdate);
        }

        void cache_CacheUpdate()
        {
            UpdateList();
        }

        void UpdateList()
        {
            if (listBox1.InvokeRequired)
            {
                listBox1.Invoke(new System.Threading.ThreadStart(UpdateList));
            }
            else
            {
                listBox1.Items.Clear();
                foreach(KeyValuePair<string, System.Net.IPAddress> pair in cache.GetCache())
                {
                    string i = pair.Key + " -> ";
                    i = i + pair.Value.ToString();
                    listBox1.Items.Add(i);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            cache.ClearCache();
            UpdateList();
        }
    }
}