namespace PassThru
{
		partial class MainWindow: System.Windows.Forms.Form {
			/// <summary>
			/// Required designer variable.
			/// </summary>
			private System.ComponentModel.IContainer components = null;
			private System.Windows.Forms.TabControl optionsTab;
            private System.Windows.Forms.TabPage tabPage1;
			private System.Windows.Forms.TabPage tabPage3;
			private System.Windows.Forms.TextBox textBox1;

			/// <summary>
			/// Clean up any resources being used.
			/// </summary>
			/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
			protected override void Dispose(bool disposing) {
				if (disposing && (components != null))
				{
						components.Dispose();
				}
				base.Dispose(disposing);
			}

			/// <summary>
			/// Required method for Designer support - do not modify
			/// the contents of this method with the code editor.
			/// </summary>
			private void InitializeComponent() 
            {
                this.textBox1 = new System.Windows.Forms.TextBox();
                this.optionsTab = new System.Windows.Forms.TabControl();
                this.tabPage1 = new System.Windows.Forms.TabPage();
                this.tabPage3 = new System.Windows.Forms.TabPage();
                this.tabPage2 = new System.Windows.Forms.TabPage();
                this.tabPage4 = new System.Windows.Forms.TabPage();
                this.splitContainer1 = new System.Windows.Forms.SplitContainer();
                this.linkLabel5 = new System.Windows.Forms.LinkLabel();
                this.linkLabel4 = new System.Windows.Forms.LinkLabel();
                this.linkLabel3 = new System.Windows.Forms.LinkLabel();
                this.linkLabel2 = new System.Windows.Forms.LinkLabel();
                this.linkLabel1 = new System.Windows.Forms.LinkLabel();
                this.optionsTab.SuspendLayout();
                this.tabPage1.SuspendLayout();
                this.splitContainer1.Panel1.SuspendLayout();
                this.splitContainer1.Panel2.SuspendLayout();
                this.splitContainer1.SuspendLayout();
                this.SuspendLayout();
                // 
                // textBox1
                // 
                this.textBox1.BackColor = System.Drawing.SystemColors.ControlLight;
                this.textBox1.Dock = System.Windows.Forms.DockStyle.Fill;
                this.textBox1.ForeColor = System.Drawing.SystemColors.ControlText;
                this.textBox1.Location = new System.Drawing.Point(3, 3);
                this.textBox1.Multiline = true;
                this.textBox1.Name = "textBox1";
                this.textBox1.ReadOnly = true;
                this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Both;
                this.textBox1.Size = new System.Drawing.Size(770, 501);
                this.textBox1.TabIndex = 0;
                this.textBox1.WordWrap = false;
                // 
                // optionsTab
                // 
                this.optionsTab.Controls.Add(this.tabPage1);
                this.optionsTab.Controls.Add(this.tabPage3);
                this.optionsTab.Controls.Add(this.tabPage2);
                this.optionsTab.Controls.Add(this.tabPage4);
                this.optionsTab.Dock = System.Windows.Forms.DockStyle.Fill;
                this.optionsTab.Location = new System.Drawing.Point(0, 0);
                this.optionsTab.Name = "optionsTab";
                this.optionsTab.SelectedIndex = 0;
                this.optionsTab.Size = new System.Drawing.Size(784, 533);
                this.optionsTab.TabIndex = 1;
                this.optionsTab.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.optionsTab_DrawItem);
                // 
                // tabPage1
                // 
                this.tabPage1.BackColor = System.Drawing.SystemColors.Control;
                this.tabPage1.Controls.Add(this.textBox1);
                this.tabPage1.ForeColor = System.Drawing.Color.Black;
                this.tabPage1.Location = new System.Drawing.Point(4, 22);
                this.tabPage1.Name = "tabPage1";
                this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
                this.tabPage1.Size = new System.Drawing.Size(776, 507);
                this.tabPage1.TabIndex = 0;
                this.tabPage1.Text = "Log";
                // 
                // tabPage3
                // 
                this.tabPage3.Location = new System.Drawing.Point(4, 22);
                this.tabPage3.Name = "tabPage3";
                this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
                this.tabPage3.Size = new System.Drawing.Size(776, 507);
                this.tabPage3.TabIndex = 2;
                this.tabPage3.Text = "Adapters";
                this.tabPage3.UseVisualStyleBackColor = true;
                // 
                // tabPage2
                // 
                this.tabPage2.Location = new System.Drawing.Point(4, 22);
                this.tabPage2.Name = "tabPage2";
                this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
                this.tabPage2.Size = new System.Drawing.Size(776, 507);
                this.tabPage2.TabIndex = 3;
                this.tabPage2.Text = "Help";
                this.tabPage2.UseVisualStyleBackColor = true;
                // 
                // tabPage4
                // 
                this.tabPage4.Location = new System.Drawing.Point(4, 22);
                this.tabPage4.Name = "tabPage4";
                this.tabPage4.Size = new System.Drawing.Size(776, 507);
                this.tabPage4.TabIndex = 4;
                this.tabPage4.Text = "Help";
                this.tabPage4.UseVisualStyleBackColor = true;
                // 
                // splitContainer1
                // 
                this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
                this.splitContainer1.Location = new System.Drawing.Point(0, 0);
                this.splitContainer1.Name = "splitContainer1";
                this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
                // 
                // splitContainer1.Panel1
                // 
                this.splitContainer1.Panel1.Controls.Add(this.optionsTab);
                // 
                // splitContainer1.Panel2
                // 
                this.splitContainer1.Panel2.Controls.Add(this.linkLabel5);
                this.splitContainer1.Panel2.Controls.Add(this.linkLabel4);
                this.splitContainer1.Panel2.Controls.Add(this.linkLabel3);
                this.splitContainer1.Panel2.Controls.Add(this.linkLabel2);
                this.splitContainer1.Panel2.Controls.Add(this.linkLabel1);
                this.splitContainer1.Size = new System.Drawing.Size(784, 562);
                this.splitContainer1.SplitterDistance = 533;
                this.splitContainer1.TabIndex = 2;
                // 
                // linkLabel5
                // 
                this.linkLabel5.AutoSize = true;
                this.linkLabel5.Location = new System.Drawing.Point(398, 7);
                this.linkLabel5.Name = "linkLabel5";
                this.linkLabel5.Size = new System.Drawing.Size(96, 13);
                this.linkLabel5.TabIndex = 4;
                this.linkLabel5.TabStop = true;
                this.linkLabel5.Text = "fireBwall on Twitter";
                this.linkLabel5.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel5_LinkClicked);
                // 
                // linkLabel4
                // 
                this.linkLabel4.AutoSize = true;
                this.linkLabel4.Location = new System.Drawing.Point(303, 7);
                this.linkLabel4.Name = "linkLabel4";
                this.linkLabel4.Size = new System.Drawing.Size(89, 13);
                this.linkLabel4.TabIndex = 3;
                this.linkLabel4.TabStop = true;
                this.linkLabel4.Text = "fireBwall Modules";
                this.linkLabel4.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel4_LinkClicked);
                // 
                // linkLabel3
                // 
                this.linkLabel3.AutoSize = true;
                this.linkLabel3.Location = new System.Drawing.Point(202, 7);
                this.linkLabel3.Name = "linkLabel3";
                this.linkLabel3.Size = new System.Drawing.Size(95, 13);
                this.linkLabel3.TabIndex = 2;
                this.linkLabel3.TabStop = true;
                this.linkLabel3.Text = "fireBwall on Reddit";
                this.linkLabel3.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel3_LinkClicked);
                // 
                // linkLabel2
                // 
                this.linkLabel2.AutoSize = true;
                this.linkLabel2.Location = new System.Drawing.Point(84, 7);
                this.linkLabel2.Name = "linkLabel2";
                this.linkLabel2.Size = new System.Drawing.Size(112, 13);
                this.linkLabel2.TabIndex = 1;
                this.linkLabel2.TabStop = true;
                this.linkLabel2.Text = "fireBwall on Facebook";
                this.linkLabel2.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel2_LinkClicked);
                // 
                // linkLabel1
                // 
                this.linkLabel1.AutoSize = true;
                this.linkLabel1.Location = new System.Drawing.Point(9, 7);
                this.linkLabel1.Name = "linkLabel1";
                this.linkLabel1.Size = new System.Drawing.Size(69, 13);
                this.linkLabel1.TabIndex = 0;
                this.linkLabel1.TabStop = true;
                this.linkLabel1.Text = "fireBwall.com";
                this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
                // 
                // MainWindow
                // 
                this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
                this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
                this.BackColor = System.Drawing.SystemColors.Control;
                this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
                this.ClientSize = new System.Drawing.Size(784, 562);
                this.Controls.Add(this.splitContainer1);
                this.ForeColor = System.Drawing.Color.Black;
                this.Name = "MainWindow";
                this.Text = "fireBwall v0.3.9.0";
                this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainWindow_FormClosing);
                this.Load += new System.EventHandler(this.MainWindow_Load);
                this.optionsTab.ResumeLayout(false);
                this.tabPage1.ResumeLayout(false);
                this.tabPage1.PerformLayout();
                this.splitContainer1.Panel1.ResumeLayout(false);
                this.splitContainer1.Panel2.ResumeLayout(false);
                this.splitContainer1.Panel2.PerformLayout();
                this.splitContainer1.ResumeLayout(false);
                this.ResumeLayout(false);

            }
            
            private System.Windows.Forms.TabPage tabPage2;
            private System.Windows.Forms.TabPage tabPage4;
            private System.Windows.Forms.SplitContainer splitContainer1;
            private System.Windows.Forms.LinkLabel linkLabel3;
            private System.Windows.Forms.LinkLabel linkLabel2;
            private System.Windows.Forms.LinkLabel linkLabel1;
            private System.Windows.Forms.LinkLabel linkLabel4;
            private System.Windows.Forms.LinkLabel linkLabel5;
		}
}
