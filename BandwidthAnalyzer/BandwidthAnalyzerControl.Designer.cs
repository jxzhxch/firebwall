namespace BandwidthAnalyzer
{
    partial class ConnectionGraphControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.moduleOptionsTab = new System.Windows.Forms.TabPage();
            this.storageOptionsGroupBox = new System.Windows.Forms.GroupBox();
            this.maxValuesTextBox = new System.Windows.Forms.TextBox();
            this.clearButton = new System.Windows.Forms.Button();
            this.unlimitedCheckbox = new System.Windows.Forms.CheckBox();
            this.entriesTickBar = new System.Windows.Forms.TrackBar();
            this.label3 = new System.Windows.Forms.Label();
            this.analyzerTab = new System.Windows.Forms.TabPage();
            this.viewOptionsGroupBox = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.formatCombobox = new System.Windows.Forms.ComboBox();
            this.filteringGroupBox = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.protocolGroupBox = new System.Windows.Forms.GroupBox();
            this.snmpCheckbox = new System.Windows.Forms.CheckBox();
            this.dhcpCheckbox = new System.Windows.Forms.CheckBox();
            this.dnsCheckbox = new System.Windows.Forms.CheckBox();
            this.icmpCheckbox = new System.Windows.Forms.CheckBox();
            this.arpCheckbox = new System.Windows.Forms.CheckBox();
            this.icmpv6Checkbox = new System.Windows.Forms.CheckBox();
            this.ipCheckbox = new System.Windows.Forms.CheckBox();
            this.ethernetCheckbox = new System.Windows.Forms.CheckBox();
            this.eethCheckbox = new System.Windows.Forms.CheckBox();
            this.udpCheckbox = new System.Windows.Forms.CheckBox();
            this.tcpCheckbox = new System.Windows.Forms.CheckBox();
            this.endDatePicker = new System.Windows.Forms.DateTimePicker();
            this.timestampCheckbox = new System.Windows.Forms.CheckBox();
            this.startDatePicker = new System.Windows.Forms.DateTimePicker();
            this.generateReportButton = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.moduleOptionsTab.SuspendLayout();
            this.storageOptionsGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.entriesTickBar)).BeginInit();
            this.analyzerTab.SuspendLayout();
            this.viewOptionsGroupBox.SuspendLayout();
            this.filteringGroupBox.SuspendLayout();
            this.protocolGroupBox.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // moduleOptionsTab
            // 
            this.moduleOptionsTab.Controls.Add(this.storageOptionsGroupBox);
            this.moduleOptionsTab.Location = new System.Drawing.Point(4, 22);
            this.moduleOptionsTab.Name = "moduleOptionsTab";
            this.moduleOptionsTab.Padding = new System.Windows.Forms.Padding(3);
            this.moduleOptionsTab.Size = new System.Drawing.Size(608, 237);
            this.moduleOptionsTab.TabIndex = 1;
            this.moduleOptionsTab.Text = "Module Options";
            this.moduleOptionsTab.UseVisualStyleBackColor = true;
            // 
            // storageOptionsGroupBox
            // 
            this.storageOptionsGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.storageOptionsGroupBox.Controls.Add(this.label5);
            this.storageOptionsGroupBox.Controls.Add(this.label4);
            this.storageOptionsGroupBox.Controls.Add(this.maxValuesTextBox);
            this.storageOptionsGroupBox.Controls.Add(this.clearButton);
            this.storageOptionsGroupBox.Controls.Add(this.unlimitedCheckbox);
            this.storageOptionsGroupBox.Controls.Add(this.entriesTickBar);
            this.storageOptionsGroupBox.Controls.Add(this.label3);
            this.storageOptionsGroupBox.Location = new System.Drawing.Point(7, 6);
            this.storageOptionsGroupBox.Name = "storageOptionsGroupBox";
            this.storageOptionsGroupBox.Size = new System.Drawing.Size(595, 224);
            this.storageOptionsGroupBox.TabIndex = 0;
            this.storageOptionsGroupBox.TabStop = false;
            this.storageOptionsGroupBox.Text = "Storage Options";
            // 
            // maxValuesTextBox
            // 
            this.maxValuesTextBox.Enabled = false;
            this.maxValuesTextBox.Location = new System.Drawing.Point(432, 145);
            this.maxValuesTextBox.Name = "maxValuesTextBox";
            this.maxValuesTextBox.Size = new System.Drawing.Size(81, 20);
            this.maxValuesTextBox.TabIndex = 4;
            this.maxValuesTextBox.Visible = false;
            this.maxValuesTextBox.TextChanged += new System.EventHandler(this.maxValuesTextBox_TextChanged);
            // 
            // clearButton
            // 
            this.clearButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.clearButton.Location = new System.Drawing.Point(10, 195);
            this.clearButton.Name = "clearButton";
            this.clearButton.Size = new System.Drawing.Size(579, 23);
            this.clearButton.TabIndex = 3;
            this.clearButton.Text = "Clear Stored Entries";
            this.clearButton.UseVisualStyleBackColor = true;
            this.clearButton.Click += new System.EventHandler(this.clearButton_Click);
            // 
            // unlimitedCheckbox
            // 
            this.unlimitedCheckbox.AutoSize = true;
            this.unlimitedCheckbox.Enabled = false;
            this.unlimitedCheckbox.Location = new System.Drawing.Point(519, 145);
            this.unlimitedCheckbox.Name = "unlimitedCheckbox";
            this.unlimitedCheckbox.Size = new System.Drawing.Size(69, 17);
            this.unlimitedCheckbox.TabIndex = 2;
            this.unlimitedCheckbox.Text = "Unlimited";
            this.unlimitedCheckbox.UseVisualStyleBackColor = true;
            this.unlimitedCheckbox.Visible = false;
            // 
            // entriesTickBar
            // 
            this.entriesTickBar.Enabled = false;
            this.entriesTickBar.LargeChange = 100000;
            this.entriesTickBar.Location = new System.Drawing.Point(188, 144);
            this.entriesTickBar.Maximum = 1000000;
            this.entriesTickBar.Name = "entriesTickBar";
            this.entriesTickBar.Size = new System.Drawing.Size(237, 45);
            this.entriesTickBar.TabIndex = 1;
            this.entriesTickBar.TickFrequency = 100000;
            this.entriesTickBar.Value = 5000;
            this.entriesTickBar.Visible = false;
            this.entriesTickBar.ValueChanged += new System.EventHandler(this.entriesTickBar_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Enabled = false;
            this.label3.Location = new System.Drawing.Point(7, 152);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(176, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Maximum number of entries to store:";
            this.label3.Visible = false;
            // 
            // analyzerTab
            // 
            this.analyzerTab.Controls.Add(this.viewOptionsGroupBox);
            this.analyzerTab.Controls.Add(this.filteringGroupBox);
            this.analyzerTab.Controls.Add(this.generateReportButton);
            this.analyzerTab.Location = new System.Drawing.Point(4, 22);
            this.analyzerTab.Name = "analyzerTab";
            this.analyzerTab.Padding = new System.Windows.Forms.Padding(3);
            this.analyzerTab.Size = new System.Drawing.Size(608, 237);
            this.analyzerTab.TabIndex = 0;
            this.analyzerTab.Text = "Bandwidth Analyzer";
            this.analyzerTab.UseVisualStyleBackColor = true;
            // 
            // viewOptionsGroupBox
            // 
            this.viewOptionsGroupBox.Controls.Add(this.label1);
            this.viewOptionsGroupBox.Controls.Add(this.formatCombobox);
            this.viewOptionsGroupBox.Location = new System.Drawing.Point(10, 6);
            this.viewOptionsGroupBox.Name = "viewOptionsGroupBox";
            this.viewOptionsGroupBox.Size = new System.Drawing.Size(591, 63);
            this.viewOptionsGroupBox.TabIndex = 12;
            this.viewOptionsGroupBox.TabStop = false;
            this.viewOptionsGroupBox.Text = "View Options";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "View data as";
            // 
            // formatCombobox
            // 
            this.formatCombobox.FormattingEnabled = true;
            this.formatCombobox.Items.AddRange(new object[] {
            "Windows Form",
            "Web Page",
            "PDF"});
            this.formatCombobox.Location = new System.Drawing.Point(80, 13);
            this.formatCombobox.Name = "formatCombobox";
            this.formatCombobox.Size = new System.Drawing.Size(121, 21);
            this.formatCombobox.TabIndex = 1;
            // 
            // filteringGroupBox
            // 
            this.filteringGroupBox.Controls.Add(this.label2);
            this.filteringGroupBox.Controls.Add(this.protocolGroupBox);
            this.filteringGroupBox.Controls.Add(this.endDatePicker);
            this.filteringGroupBox.Controls.Add(this.timestampCheckbox);
            this.filteringGroupBox.Controls.Add(this.startDatePicker);
            this.filteringGroupBox.Location = new System.Drawing.Point(10, 75);
            this.filteringGroupBox.Name = "filteringGroupBox";
            this.filteringGroupBox.Size = new System.Drawing.Size(591, 127);
            this.filteringGroupBox.TabIndex = 11;
            this.filteringGroupBox.TabStop = false;
            this.filteringGroupBox.Text = "Filtering";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(346, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(25, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "and";
            // 
            // protocolGroupBox
            // 
            this.protocolGroupBox.Controls.Add(this.snmpCheckbox);
            this.protocolGroupBox.Controls.Add(this.dhcpCheckbox);
            this.protocolGroupBox.Controls.Add(this.dnsCheckbox);
            this.protocolGroupBox.Controls.Add(this.icmpCheckbox);
            this.protocolGroupBox.Controls.Add(this.arpCheckbox);
            this.protocolGroupBox.Controls.Add(this.icmpv6Checkbox);
            this.protocolGroupBox.Controls.Add(this.ipCheckbox);
            this.protocolGroupBox.Controls.Add(this.ethernetCheckbox);
            this.protocolGroupBox.Controls.Add(this.eethCheckbox);
            this.protocolGroupBox.Controls.Add(this.udpCheckbox);
            this.protocolGroupBox.Controls.Add(this.tcpCheckbox);
            this.protocolGroupBox.Location = new System.Drawing.Point(7, 42);
            this.protocolGroupBox.Name = "protocolGroupBox";
            this.protocolGroupBox.Size = new System.Drawing.Size(570, 79);
            this.protocolGroupBox.TabIndex = 12;
            this.protocolGroupBox.TabStop = false;
            this.protocolGroupBox.Text = "Protocol";
            // 
            // snmpCheckbox
            // 
            this.snmpCheckbox.AutoSize = true;
            this.snmpCheckbox.Checked = true;
            this.snmpCheckbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.snmpCheckbox.Location = new System.Drawing.Point(310, 19);
            this.snmpCheckbox.Name = "snmpCheckbox";
            this.snmpCheckbox.Size = new System.Drawing.Size(57, 17);
            this.snmpCheckbox.TabIndex = 19;
            this.snmpCheckbox.Text = "SNMP";
            this.snmpCheckbox.UseVisualStyleBackColor = true;
            // 
            // dhcpCheckbox
            // 
            this.dhcpCheckbox.AutoSize = true;
            this.dhcpCheckbox.Checked = true;
            this.dhcpCheckbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.dhcpCheckbox.Location = new System.Drawing.Point(254, 42);
            this.dhcpCheckbox.Name = "dhcpCheckbox";
            this.dhcpCheckbox.Size = new System.Drawing.Size(56, 17);
            this.dhcpCheckbox.TabIndex = 18;
            this.dhcpCheckbox.Text = "DHCP";
            this.dhcpCheckbox.UseVisualStyleBackColor = true;
            // 
            // dnsCheckbox
            // 
            this.dnsCheckbox.AutoSize = true;
            this.dnsCheckbox.Checked = true;
            this.dnsCheckbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.dnsCheckbox.Location = new System.Drawing.Point(254, 19);
            this.dnsCheckbox.Name = "dnsCheckbox";
            this.dnsCheckbox.Size = new System.Drawing.Size(49, 17);
            this.dnsCheckbox.TabIndex = 17;
            this.dnsCheckbox.Text = "DNS";
            this.dnsCheckbox.UseVisualStyleBackColor = true;
            // 
            // icmpCheckbox
            // 
            this.icmpCheckbox.AutoSize = true;
            this.icmpCheckbox.Checked = true;
            this.icmpCheckbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.icmpCheckbox.Location = new System.Drawing.Point(199, 43);
            this.icmpCheckbox.Name = "icmpCheckbox";
            this.icmpCheckbox.Size = new System.Drawing.Size(52, 17);
            this.icmpCheckbox.TabIndex = 16;
            this.icmpCheckbox.Text = "ICMP";
            this.icmpCheckbox.UseVisualStyleBackColor = true;
            // 
            // arpCheckbox
            // 
            this.arpCheckbox.AutoSize = true;
            this.arpCheckbox.Checked = true;
            this.arpCheckbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.arpCheckbox.Location = new System.Drawing.Point(199, 19);
            this.arpCheckbox.Name = "arpCheckbox";
            this.arpCheckbox.Size = new System.Drawing.Size(48, 17);
            this.arpCheckbox.TabIndex = 15;
            this.arpCheckbox.Text = "ARP";
            this.arpCheckbox.UseVisualStyleBackColor = true;
            // 
            // icmpv6Checkbox
            // 
            this.icmpv6Checkbox.AutoSize = true;
            this.icmpv6Checkbox.Checked = true;
            this.icmpv6Checkbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.icmpv6Checkbox.Location = new System.Drawing.Point(132, 42);
            this.icmpv6Checkbox.Name = "icmpv6Checkbox";
            this.icmpv6Checkbox.Size = new System.Drawing.Size(64, 17);
            this.icmpv6Checkbox.TabIndex = 14;
            this.icmpv6Checkbox.Text = "ICMPv6";
            this.icmpv6Checkbox.UseVisualStyleBackColor = true;
            // 
            // ipCheckbox
            // 
            this.ipCheckbox.AutoSize = true;
            this.ipCheckbox.Checked = true;
            this.ipCheckbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ipCheckbox.Location = new System.Drawing.Point(132, 19);
            this.ipCheckbox.Name = "ipCheckbox";
            this.ipCheckbox.Size = new System.Drawing.Size(36, 17);
            this.ipCheckbox.TabIndex = 13;
            this.ipCheckbox.Text = "IP";
            this.ipCheckbox.UseVisualStyleBackColor = true;
            // 
            // ethernetCheckbox
            // 
            this.ethernetCheckbox.AutoSize = true;
            this.ethernetCheckbox.Checked = true;
            this.ethernetCheckbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ethernetCheckbox.Location = new System.Drawing.Point(59, 42);
            this.ethernetCheckbox.Name = "ethernetCheckbox";
            this.ethernetCheckbox.Size = new System.Drawing.Size(66, 17);
            this.ethernetCheckbox.TabIndex = 12;
            this.ethernetCheckbox.Text = "Ethernet";
            this.ethernetCheckbox.UseVisualStyleBackColor = true;
            // 
            // eethCheckbox
            // 
            this.eethCheckbox.AutoSize = true;
            this.eethCheckbox.Checked = true;
            this.eethCheckbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.eethCheckbox.Location = new System.Drawing.Point(59, 19);
            this.eethCheckbox.Name = "eethCheckbox";
            this.eethCheckbox.Size = new System.Drawing.Size(49, 17);
            this.eethCheckbox.TabIndex = 11;
            this.eethCheckbox.Text = "EEth";
            this.eethCheckbox.UseVisualStyleBackColor = true;
            // 
            // udpCheckbox
            // 
            this.udpCheckbox.AutoSize = true;
            this.udpCheckbox.Checked = true;
            this.udpCheckbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.udpCheckbox.Location = new System.Drawing.Point(6, 42);
            this.udpCheckbox.Name = "udpCheckbox";
            this.udpCheckbox.Size = new System.Drawing.Size(49, 17);
            this.udpCheckbox.TabIndex = 10;
            this.udpCheckbox.Text = "UDP";
            this.udpCheckbox.UseVisualStyleBackColor = true;
            // 
            // tcpCheckbox
            // 
            this.tcpCheckbox.AutoSize = true;
            this.tcpCheckbox.Checked = true;
            this.tcpCheckbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.tcpCheckbox.Location = new System.Drawing.Point(6, 19);
            this.tcpCheckbox.Name = "tcpCheckbox";
            this.tcpCheckbox.Size = new System.Drawing.Size(47, 17);
            this.tcpCheckbox.TabIndex = 9;
            this.tcpCheckbox.Text = "TCP";
            this.tcpCheckbox.UseVisualStyleBackColor = true;
            // 
            // endDatePicker
            // 
            this.endDatePicker.Location = new System.Drawing.Point(377, 16);
            this.endDatePicker.Name = "endDatePicker";
            this.endDatePicker.Size = new System.Drawing.Size(200, 20);
            this.endDatePicker.TabIndex = 8;
            // 
            // timestampCheckbox
            // 
            this.timestampCheckbox.AutoSize = true;
            this.timestampCheckbox.Location = new System.Drawing.Point(9, 19);
            this.timestampCheckbox.Name = "timestampCheckbox";
            this.timestampCheckbox.Size = new System.Drawing.Size(124, 17);
            this.timestampCheckbox.TabIndex = 6;
            this.timestampCheckbox.Text = "Timestamp between:";
            this.timestampCheckbox.UseVisualStyleBackColor = true;
            // 
            // startDatePicker
            // 
            this.startDatePicker.Location = new System.Drawing.Point(139, 16);
            this.startDatePicker.Name = "startDatePicker";
            this.startDatePicker.Size = new System.Drawing.Size(200, 20);
            this.startDatePicker.TabIndex = 7;
            // 
            // generateReportButton
            // 
            this.generateReportButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.generateReportButton.Location = new System.Drawing.Point(10, 208);
            this.generateReportButton.Name = "generateReportButton";
            this.generateReportButton.Size = new System.Drawing.Size(595, 23);
            this.generateReportButton.TabIndex = 2;
            this.generateReportButton.Text = "Generate Bandwidth Report";
            this.generateReportButton.UseVisualStyleBackColor = true;
            this.generateReportButton.Click += new System.EventHandler(this.generateReportButton_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.tabControl1.Controls.Add(this.analyzerTab);
            this.tabControl1.Controls.Add(this.moduleOptionsTab);
            this.tabControl1.Location = new System.Drawing.Point(0, 2);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(616, 263);
            this.tabControl1.TabIndex = 0;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 20);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(535, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "If you wish to permanently delete all entries from the connection log, you can do" +
    " so below.   Make sure you have";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(10, 33);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(327, 13);
            this.label5.TabIndex = 6;
            this.label5.Text = "saved any reports generated from this data which you want to keep.";
            // 
            // ConnectionGraphControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControl1);
            this.Name = "ConnectionGraphControl";
            this.Size = new System.Drawing.Size(619, 265);
            this.moduleOptionsTab.ResumeLayout(false);
            this.storageOptionsGroupBox.ResumeLayout(false);
            this.storageOptionsGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.entriesTickBar)).EndInit();
            this.analyzerTab.ResumeLayout(false);
            this.viewOptionsGroupBox.ResumeLayout(false);
            this.viewOptionsGroupBox.PerformLayout();
            this.filteringGroupBox.ResumeLayout(false);
            this.filteringGroupBox.PerformLayout();
            this.protocolGroupBox.ResumeLayout(false);
            this.protocolGroupBox.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabPage moduleOptionsTab;
        private System.Windows.Forms.GroupBox storageOptionsGroupBox;
        private System.Windows.Forms.TrackBar entriesTickBar;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TabPage analyzerTab;
        private System.Windows.Forms.GroupBox viewOptionsGroupBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox formatCombobox;
        private System.Windows.Forms.GroupBox filteringGroupBox;
        private System.Windows.Forms.CheckBox udpCheckbox;
        private System.Windows.Forms.CheckBox tcpCheckbox;
        private System.Windows.Forms.DateTimePicker endDatePicker;
        private System.Windows.Forms.CheckBox timestampCheckbox;
        private System.Windows.Forms.DateTimePicker startDatePicker;
        private System.Windows.Forms.Button generateReportButton;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.Button clearButton;
        private System.Windows.Forms.CheckBox unlimitedCheckbox;
        private System.Windows.Forms.TextBox maxValuesTextBox;
        private System.Windows.Forms.CheckBox eethCheckbox;
        private System.Windows.Forms.GroupBox protocolGroupBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox ethernetCheckbox;
        private System.Windows.Forms.CheckBox ipCheckbox;
        private System.Windows.Forms.CheckBox icmpv6Checkbox;
        private System.Windows.Forms.CheckBox arpCheckbox;
        private System.Windows.Forms.CheckBox icmpCheckbox;
        private System.Windows.Forms.CheckBox dnsCheckbox;
        private System.Windows.Forms.CheckBox dhcpCheckbox;
        private System.Windows.Forms.CheckBox snmpCheckbox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;

    }
}
