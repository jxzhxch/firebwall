namespace PassThru
{
    partial class OptionsDisplay
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
            this.displayTrayLogs = new System.Windows.Forms.CheckBox();
            this.languageBox = new System.Windows.Forms.ComboBox();
            this.languageLabel = new System.Windows.Forms.Label();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.checkBoxStartMinimized = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.themeBox = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.optionsControl = new System.Windows.Forms.TabControl();
            this.options = new System.Windows.Forms.TabPage();
            this.maxLogsBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.maxPcapBox = new System.Windows.Forms.TextBox();
            this.themeTab = new System.Windows.Forms.TabPage();
            this.updatingTab = new System.Windows.Forms.TabPage();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.optionsControl.SuspendLayout();
            this.options.SuspendLayout();
            this.themeTab.SuspendLayout();
            this.updatingTab.SuspendLayout();
            this.SuspendLayout();
            // 
            // displayTrayLogs
            // 
            this.displayTrayLogs.AutoSize = true;
            this.displayTrayLogs.Checked = true;
            this.displayTrayLogs.CheckState = System.Windows.Forms.CheckState.Checked;
            this.displayTrayLogs.Location = new System.Drawing.Point(6, 6);
            this.displayTrayLogs.Name = "displayTrayLogs";
            this.displayTrayLogs.Size = new System.Drawing.Size(123, 17);
            this.displayTrayLogs.TabIndex = 0;
            this.displayTrayLogs.Text = "Display Icon Popups";
            this.displayTrayLogs.UseVisualStyleBackColor = true;
            this.displayTrayLogs.CheckedChanged += new System.EventHandler(this.displayTrayLogs_CheckedChanged);
            // 
            // languageBox
            // 
            this.languageBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.languageBox.FormattingEnabled = true;
            this.languageBox.Items.AddRange(new object[] {
            "--",
            "English",
            "Español",
            "Deutsch",
            "中國",
            "русский",
            "Português"});
            this.languageBox.Location = new System.Drawing.Point(89, 114);
            this.languageBox.Name = "languageBox";
            this.languageBox.Size = new System.Drawing.Size(121, 21);
            this.languageBox.TabIndex = 1;
            this.languageBox.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // languageLabel
            // 
            this.languageLabel.AutoSize = true;
            this.languageLabel.Location = new System.Drawing.Point(25, 117);
            this.languageLabel.Name = "languageLabel";
            this.languageLabel.Size = new System.Drawing.Size(58, 13);
            this.languageLabel.TabIndex = 2;
            this.languageLabel.Text = "Language:";
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(7, 282);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(225, 13);
            this.linkLabel1.TabIndex = 3;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "http://code.google.com/p/firebwall/issues/list";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Controls.Add(this.checkBox2);
            this.groupBox1.Controls.Add(this.checkBox1);
            this.groupBox1.Location = new System.Drawing.Point(6, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(267, 119);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Update Options";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 89);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(212, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "The Interval must be greater than 9 minutes";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 67);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(96, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Interval in Minutes:";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(134, 64);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(127, 20);
            this.textBox1.TabIndex = 2;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(7, 43);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(112, 17);
            this.checkBox2.TabIndex = 1;
            this.checkBox2.Text = "Intervaled Checks";
            this.checkBox2.UseVisualStyleBackColor = true;
            this.checkBox2.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(7, 20);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(114, 17);
            this.checkBox1.TabIndex = 0;
            this.checkBox1.Text = "Check on Start Up";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // checkBoxStartMinimized
            // 
            this.checkBoxStartMinimized.AutoSize = true;
            this.checkBoxStartMinimized.Checked = true;
            this.checkBoxStartMinimized.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxStartMinimized.Location = new System.Drawing.Point(135, 6);
            this.checkBoxStartMinimized.Name = "checkBoxStartMinimized";
            this.checkBoxStartMinimized.Size = new System.Drawing.Size(97, 17);
            this.checkBoxStartMinimized.TabIndex = 9;
            this.checkBoxStartMinimized.Text = "Start Minimized";
            this.checkBoxStartMinimized.UseVisualStyleBackColor = true;
            this.checkBoxStartMinimized.CheckedChanged += new System.EventHandler(this.checkBoxStartMinimized_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Theme:";
            // 
            // themeBox
            // 
            this.themeBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.themeBox.FormattingEnabled = true;
            this.themeBox.Location = new System.Drawing.Point(72, 16);
            this.themeBox.Name = "themeBox";
            this.themeBox.Size = new System.Drawing.Size(121, 21);
            this.themeBox.TabIndex = 11;
            this.themeBox.SelectedIndexChanged += new System.EventHandler(this.themeBox_SelectedIndexChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(200, 16);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(78, 23);
            this.button1.TabIndex = 12;
            this.button1.Text = "Edit Current Theme";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.optionsControl, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(555, 330);
            this.tableLayoutPanel1.TabIndex = 13;
            // 
            // optionsControl
            // 
            this.optionsControl.Controls.Add(this.options);
            this.optionsControl.Controls.Add(this.themeTab);
            this.optionsControl.Controls.Add(this.updatingTab);
            this.optionsControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.optionsControl.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            this.optionsControl.Location = new System.Drawing.Point(3, 3);
            this.optionsControl.Name = "optionsControl";
            this.optionsControl.SelectedIndex = 0;
            this.optionsControl.Size = new System.Drawing.Size(549, 324);
            this.optionsControl.TabIndex = 0;
            this.optionsControl.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.Updating_DrawItem);
            // 
            // options
            // 
            this.options.Controls.Add(this.maxLogsBox);
            this.options.Controls.Add(this.label5);
            this.options.Controls.Add(this.label2);
            this.options.Controls.Add(this.maxPcapBox);
            this.options.Controls.Add(this.displayTrayLogs);
            this.options.Controls.Add(this.checkBoxStartMinimized);
            this.options.Controls.Add(this.linkLabel1);
            this.options.Controls.Add(this.languageBox);
            this.options.Controls.Add(this.languageLabel);
            this.options.Location = new System.Drawing.Point(4, 22);
            this.options.Name = "options";
            this.options.Padding = new System.Windows.Forms.Padding(3);
            this.options.Size = new System.Drawing.Size(541, 298);
            this.options.TabIndex = 0;
            this.options.Text = "General";
            this.options.UseVisualStyleBackColor = true;
            // 
            // maxLogsBox
            // 
            this.maxLogsBox.Location = new System.Drawing.Point(89, 61);
            this.maxLogsBox.Name = "maxLogsBox";
            this.maxLogsBox.Size = new System.Drawing.Size(40, 20);
            this.maxLogsBox.TabIndex = 13;
            this.maxLogsBox.TextChanged += new System.EventHandler(this.maxLogsBox_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(27, 61);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(56, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "Max Logs:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 90);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "Max Pcap logs:";
            // 
            // maxPcapBox
            // 
            this.maxPcapBox.Location = new System.Drawing.Point(89, 87);
            this.maxPcapBox.Name = "maxPcapBox";
            this.maxPcapBox.Size = new System.Drawing.Size(40, 20);
            this.maxPcapBox.TabIndex = 10;
            this.maxPcapBox.TextChanged += new System.EventHandler(this.maxPcapBox_TextChanged);
            // 
            // themeTab
            // 
            this.themeTab.Controls.Add(this.button1);
            this.themeTab.Controls.Add(this.label1);
            this.themeTab.Controls.Add(this.themeBox);
            this.themeTab.Location = new System.Drawing.Point(4, 22);
            this.themeTab.Name = "themeTab";
            this.themeTab.Padding = new System.Windows.Forms.Padding(3);
            this.themeTab.Size = new System.Drawing.Size(541, 298);
            this.themeTab.TabIndex = 1;
            this.themeTab.Text = "Theme";
            this.themeTab.UseVisualStyleBackColor = true;
            // 
            // updatingTab
            // 
            this.updatingTab.Controls.Add(this.groupBox1);
            this.updatingTab.Location = new System.Drawing.Point(4, 22);
            this.updatingTab.Name = "updatingTab";
            this.updatingTab.Padding = new System.Windows.Forms.Padding(3);
            this.updatingTab.Size = new System.Drawing.Size(541, 298);
            this.updatingTab.TabIndex = 2;
            this.updatingTab.Text = "Updates";
            this.updatingTab.UseVisualStyleBackColor = true;
            // 
            // OptionsDisplay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Silver;
            this.Controls.Add(this.tableLayoutPanel1);
            this.ForeColor = System.Drawing.Color.White;
            this.Name = "OptionsDisplay";
            this.Size = new System.Drawing.Size(555, 330);
            this.Load += new System.EventHandler(this.OptionsDisplay_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.optionsControl.ResumeLayout(false);
            this.options.ResumeLayout(false);
            this.options.PerformLayout();
            this.themeTab.ResumeLayout(false);
            this.themeTab.PerformLayout();
            this.updatingTab.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.CheckBox displayTrayLogs;
        private System.Windows.Forms.Label languageLabel;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.ComboBox languageBox;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Label label4;
        public System.Windows.Forms.CheckBox checkBoxStartMinimized;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox themeBox;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TabControl optionsControl;
        private System.Windows.Forms.TabPage options;
        private System.Windows.Forms.TabPage themeTab;
        private System.Windows.Forms.TabPage updatingTab;
        private System.Windows.Forms.TextBox maxLogsBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox maxPcapBox;



    }
}
