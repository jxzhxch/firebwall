namespace PassThru
{
		partial class MainWindow: System.Windows.Forms.Form {
			/// <summary>
			/// Required designer variable.
			/// </summary>
            private System.ComponentModel.IContainer components = null;

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
                this.optionsTab = new System.Windows.Forms.TabControl();
                this.tabPage1 = new System.Windows.Forms.TabPage();
                this.textBox1 = new System.Windows.Forms.TextBox();
                this.tabPage3 = new System.Windows.Forms.TabPage();
                this.tabPage2 = new System.Windows.Forms.TabPage();
                this.tabPage4 = new System.Windows.Forms.TabPage();
                this.splitContainer1 = new System.Windows.Forms.SplitContainer();
                this.pictureBox1 = new System.Windows.Forms.PictureBox();
                this.optionsTab.SuspendLayout();
                this.tabPage1.SuspendLayout();
                this.splitContainer1.Panel1.SuspendLayout();
                this.splitContainer1.Panel2.SuspendLayout();
                this.splitContainer1.SuspendLayout();
                ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
                this.SuspendLayout();
                // 
                // optionsTab
                // 
                this.optionsTab.Appearance = System.Windows.Forms.TabAppearance.Buttons;
                this.optionsTab.Controls.Add(this.tabPage1);
                this.optionsTab.Controls.Add(this.tabPage3);
                this.optionsTab.Controls.Add(this.tabPage2);
                this.optionsTab.Controls.Add(this.tabPage4);
                this.optionsTab.Dock = System.Windows.Forms.DockStyle.Fill;
                this.optionsTab.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.optionsTab.HotTrack = true;
                this.optionsTab.ItemSize = new System.Drawing.Size(96, 35);
                this.optionsTab.Location = new System.Drawing.Point(0, 0);
                this.optionsTab.Margin = new System.Windows.Forms.Padding(0);
                this.optionsTab.Multiline = true;
                this.optionsTab.Name = "optionsTab";
                this.optionsTab.RightToLeft = System.Windows.Forms.RightToLeft.No;
                this.optionsTab.SelectedIndex = 0;
                this.optionsTab.Size = new System.Drawing.Size(794, 475);
                this.optionsTab.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
                this.optionsTab.TabIndex = 3;
                // 
                // tabPage1
                // 
                this.tabPage1.BackColor = System.Drawing.SystemColors.Control;
                this.tabPage1.Controls.Add(this.textBox1);
                this.tabPage1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.tabPage1.ForeColor = System.Drawing.Color.Black;
                this.tabPage1.Location = new System.Drawing.Point(4, 39);
                this.tabPage1.Margin = new System.Windows.Forms.Padding(0);
                this.tabPage1.Name = "tabPage1";
                this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
                this.tabPage1.Size = new System.Drawing.Size(786, 432);
                this.tabPage1.TabIndex = 0;
                this.tabPage1.Text = "Log";
                // 
                // textBox1
                // 
                this.textBox1.BackColor = System.Drawing.SystemColors.ControlLight;
                this.textBox1.Dock = System.Windows.Forms.DockStyle.Fill;
                this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.textBox1.ForeColor = System.Drawing.SystemColors.ControlText;
                this.textBox1.Location = new System.Drawing.Point(3, 3);
                this.textBox1.Multiline = true;
                this.textBox1.Name = "textBox1";
                this.textBox1.ReadOnly = true;
                this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
                this.textBox1.Size = new System.Drawing.Size(780, 426);
                this.textBox1.TabIndex = 0;
                this.textBox1.WordWrap = false;
                // 
                // tabPage3
                // 
                this.tabPage3.Location = new System.Drawing.Point(4, 39);
                this.tabPage3.Name = "tabPage3";
                this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
                this.tabPage3.Size = new System.Drawing.Size(786, 432);
                this.tabPage3.TabIndex = 2;
                this.tabPage3.Text = "Adapters";
                this.tabPage3.UseVisualStyleBackColor = true;
                // 
                // tabPage2
                // 
                this.tabPage2.Location = new System.Drawing.Point(4, 39);
                this.tabPage2.Name = "tabPage2";
                this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
                this.tabPage2.Size = new System.Drawing.Size(786, 432);
                this.tabPage2.TabIndex = 3;
                this.tabPage2.Text = "Help";
                this.tabPage2.UseVisualStyleBackColor = true;
                // 
                // tabPage4
                // 
                this.tabPage4.Location = new System.Drawing.Point(4, 39);
                this.tabPage4.Name = "tabPage4";
                this.tabPage4.Size = new System.Drawing.Size(786, 432);
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
                this.splitContainer1.Panel1.Controls.Add(this.pictureBox1);
                // 
                // splitContainer1.Panel2
                // 
                this.splitContainer1.Panel2.Controls.Add(this.optionsTab);
                this.splitContainer1.Size = new System.Drawing.Size(794, 572);
                this.splitContainer1.SplitterDistance = 93;
                this.splitContainer1.TabIndex = 4;
                // 
                // pictureBox1
                // 
                this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
                this.pictureBox1.Image = global::PassThru.Properties.Resources.banner;
                this.pictureBox1.Location = new System.Drawing.Point(0, 0);
                this.pictureBox1.Name = "pictureBox1";
                this.pictureBox1.Size = new System.Drawing.Size(794, 93);
                this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                this.pictureBox1.TabIndex = 0;
                this.pictureBox1.TabStop = false;
                // 
                // MainWindow
                // 
                this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
                this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
                this.BackColor = System.Drawing.SystemColors.Control;
                this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
                this.ClientSize = new System.Drawing.Size(794, 572);
                this.Controls.Add(this.splitContainer1);
                this.DoubleBuffered = true;
                this.ForeColor = System.Drawing.Color.Black;
                this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
                this.Name = "MainWindow";
                this.Text = "fireBwall v0.3.10.0";
                this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainWindow_FormClosing);
                this.Load += new System.EventHandler(this.MainWindow_Load);
                this.Resize += new System.EventHandler(this.MainWindow_Resize);
                this.optionsTab.ResumeLayout(false);
                this.tabPage1.ResumeLayout(false);
                this.tabPage1.PerformLayout();
                this.splitContainer1.Panel1.ResumeLayout(false);
                this.splitContainer1.Panel2.ResumeLayout(false);
                this.splitContainer1.ResumeLayout(false);
                ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
                this.ResumeLayout(false);

            }

            private System.Windows.Forms.TabControl optionsTab;
            private System.Windows.Forms.TabPage tabPage1;
            private System.Windows.Forms.TextBox textBox1;
            private System.Windows.Forms.TabPage tabPage3;
            private System.Windows.Forms.TabPage tabPage2;
            private System.Windows.Forms.TabPage tabPage4;
            private System.Windows.Forms.SplitContainer splitContainer1;
            private System.Windows.Forms.PictureBox pictureBox1;
		}
}
