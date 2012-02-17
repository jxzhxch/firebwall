using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Text.RegularExpressions;
using FM;

namespace PassThru
{
    /// <summary>
    /// DDoS display module
    /// </summary>
    public partial class DDoSDisplay : UserControl
    {
        private DDoSModule dosmod;
        private List<BlockedIP> blockcache = new List<BlockedIP>();

        // constructor sets the local DDoSModule object
        public DDoSDisplay(DDoSModule dosmod)
        {
            this.dosmod = dosmod;
            InitializeComponent();
        }

        // update the modules' blocked IP cache
        private void UpdateBlockedCache()
        {
            this.dosmod.data.BlockCache = blockcache;
        }

        /// <summary>
        /// Loads data grid settings back up into the table
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void DDoSDisplay_Load(object sender, EventArgs e)
        {
            blockcache = new List<BlockedIP>(this.dosmod.data.BlockCache);
            switch (LanguageConfig.GetCurrentLanguage())
            {
                case LanguageConfig.Language.ENGLISH:
                    removeButton.Text = "Remove IP";
                    addButton.Text = "Add IP";
                    blockipLabel.Text = "Blocked IPs";
                    dosBlockTable.Columns[0].HeaderText = "IP Address";
                    dosBlockTable.Columns[1].HeaderText = "Reason";
                    dosBlockTable.Columns[2].HeaderText = "Date Blocked";
                    break;
                case LanguageConfig.Language.PORTUGUESE:
                    removeButton.Text = "remover IP";
                    addButton.Text = "Adicionar IP";
                    blockipLabel.Text = "IPs bloqueados";
                    dosBlockTable.Columns[0].HeaderText = "Endereço IP";
                    dosBlockTable.Columns[1].HeaderText = "razão";
                    dosBlockTable.Columns[2].HeaderText = "data bloqueados";
                    break;
                case LanguageConfig.Language.CHINESE:
                    removeButton.Text = "删除IP";
                    addButton.Text = "添加IP";
                    blockipLabel.Text = "封鎖的IP地址";
                    dosBlockTable.Columns[0].HeaderText = "IP地址";
                    dosBlockTable.Columns[1].HeaderText = "原因";
                    dosBlockTable.Columns[2].HeaderText = "日期已封鎖";
                    break;
                case LanguageConfig.Language.GERMAN:
                    removeButton.Text = "entfernen IP";
                    addButton.Text = "Add IP";
                    blockipLabel.Text = "Blockierte IPs";
                    dosBlockTable.Columns[0].HeaderText = "IP-Adresse";
                    dosBlockTable.Columns[1].HeaderText = "Grund";
                    dosBlockTable.Columns[2].HeaderText = "Datum Blocked";
                    break;
                case LanguageConfig.Language.RUSSIAN:
                    removeButton.Text = "Удалите IP";
                    addButton.Text = "Добавить IP-";
                    blockipLabel.Text = "Заблокированные IP-адреса";
                    dosBlockTable.Columns[0].HeaderText = "IP-адрес";
                    dosBlockTable.Columns[1].HeaderText = "причина";
                    dosBlockTable.Columns[2].HeaderText = "Дата Заблокированные";
                    break;
                case LanguageConfig.Language.SPANISH:
                    removeButton.Text = "quitar IP";
                    addButton.Text = "Añadir IP";
                    blockipLabel.Text = "IPs bloqueadas";
                    dosBlockTable.Columns[0].HeaderText = "Dirección IP";
                    dosBlockTable.Columns[1].HeaderText = "Razón";
                    dosBlockTable.Columns[2].HeaderText = "Fecha Bloqueados";
                    break;
            }
            RebuildTable();
        }
        
        /// <summary>
        /// Handles the IP add button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addButton_Click(object sender, EventArgs e)
        {
            // if the given string is a valid IPv4 addr.
            // IPAddress.TryParse is broken.
            if (regIP.IsMatch(addField.Text))
            {
                IPAddress t = IPAddress.Parse(addField.Text);
                blockcache.Add(new BlockedIP(t, DateTime.UtcNow, "User added"));
                
                // update the module blockcache and update the table
                UpdateBlockedCache();
                RebuildTable();

                // consume input
                addField.Text = "";
            }
        }

        /// <summary>
        /// Rebuilds the table from what's in blockcache
        /// </summary>
        private void RebuildTable()
        {
            dosBlockTable.Rows.Clear();
            foreach (BlockedIP ip in blockcache)
            {
                object[] t = { ip.Blockedip, ip.Reason, ip.DateBlocked };
                dosBlockTable.Rows.Add(t);
            }
        }
       

        /// <summary>
        /// Handles the remove button action
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void removeButton_Click(object sender, EventArgs e)
        {
            // if nothing's been selected, get out
            if (dosBlockTable.SelectedRows.Count <= 0)
                return;                

            // grab the rowidx/type from the table
            int rowIdx = dosBlockTable.SelectedCells[0].RowIndex;
            string remIP = dosBlockTable["blockedip", rowIdx].Value.ToString();
            BlockedIP remove = new BlockedIP();

            // find the IP in the blockcache
            foreach ( BlockedIP ip in blockcache )
            {
                // if the two match
                if (((ip.Blockedip).ToString()).Equals(remIP))
                {
                    remove = ip;
                    break;
                }
            }

            // remove from the cache, update the module blockcache, and rebuild grid
            blockcache.Remove(remove);
            this.dosmod.data.BlockCache = this.blockcache;
            RebuildTable();
        }

        // regex pattern for matching a valid IP address
        // http://www.codekeep.net/snippets/9bd4694c-1d33-415e-b97f-db2f7f07015e.aspx
        private static Regex regIP = new Regex(
            @"(?<First>2[0-4]\d|25[0-5]|[01]?\d\d?)\.(?<Second>2[0-4]\d|25"
            + @"[0-5]|[01]?\d\d?)\.(?<Third>2[0-4]\d|25[0-5]|[01]?\d\d?)\.(?"
            + @"<Fourth>2[0-4]\d|25[0-5]|[01]?\d\d?)",
            RegexOptions.IgnoreCase
            | RegexOptions.CultureInvariant
            | RegexOptions.IgnorePatternWhitespace
            | RegexOptions.Compiled
            );
    }
}
