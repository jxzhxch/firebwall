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
                this.optionsTab.SuspendLayout();
                this.tabPage1.SuspendLayout();
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
                this.textBox1.Size = new System.Drawing.Size(770, 530);
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
                this.optionsTab.Size = new System.Drawing.Size(784, 562);
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
                this.tabPage1.Size = new System.Drawing.Size(776, 536);
                this.tabPage1.TabIndex = 0;
                this.tabPage1.Text = "Log";
                // 
                // tabPage3
                // 
                this.tabPage3.Location = new System.Drawing.Point(4, 22);
                this.tabPage3.Name = "tabPage3";
                this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
                this.tabPage3.Size = new System.Drawing.Size(776, 536);
                this.tabPage3.TabIndex = 2;
                this.tabPage3.Text = "Adapters";
                this.tabPage3.UseVisualStyleBackColor = true;
                // 
                // tabPage2
                // 
                this.tabPage2.Location = new System.Drawing.Point(4, 22);
                this.tabPage2.Name = "tabPage2";
                this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
                this.tabPage2.Size = new System.Drawing.Size(776, 536);
                this.tabPage2.TabIndex = 3;
                this.tabPage2.Text = "Help";
                this.tabPage2.UseVisualStyleBackColor = true;
                // 
                // tabPage4
                // 
                this.tabPage4.Location = new System.Drawing.Point(4, 22);
                this.tabPage4.Name = "tabPage4";
                this.tabPage4.Size = new System.Drawing.Size(776, 536);
                this.tabPage4.TabIndex = 4;
                this.tabPage4.Text = "Help";
                this.tabPage4.UseVisualStyleBackColor = true;
                // 
                // MainWindow
                // 
                this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
                this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
                this.BackColor = System.Drawing.SystemColors.Control;
                this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
                this.ClientSize = new System.Drawing.Size(784, 562);
                this.Controls.Add(this.optionsTab);
                this.ForeColor = System.Drawing.Color.Black;
                this.Name = "MainWindow";
                this.Text = "fireBwall v0.3.6.0";
                this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainWindow_FormClosing);
                this.Load += new System.EventHandler(this.MainWindow_Load);
                this.optionsTab.ResumeLayout(false);
                this.tabPage1.ResumeLayout(false);
                this.tabPage1.PerformLayout();
                this.ResumeLayout(false);

            }
            
            private System.Windows.Forms.TabPage tabPage2;
            private System.Windows.Forms.TabPage tabPage4;
		}
}
