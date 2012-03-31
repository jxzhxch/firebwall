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
                this.splitContainer1 = new System.Windows.Forms.SplitContainer();
                this.pictureBox1 = new System.Windows.Forms.PictureBox();
                this.tabPage1 = new System.Windows.Forms.Button();
                this.tabPage2 = new System.Windows.Forms.Button();
                this.tabPage3 = new System.Windows.Forms.Button();
                this.tabPage4 = new System.Windows.Forms.Button();
                this.splitContainer1.Panel1.SuspendLayout();
                this.splitContainer1.SuspendLayout();
                ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
                this.SuspendLayout();
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
                this.splitContainer1.Panel1.Controls.Add(this.tabPage4);
                this.splitContainer1.Panel1.Controls.Add(this.tabPage3);
                this.splitContainer1.Panel1.Controls.Add(this.tabPage2);
                this.splitContainer1.Panel1.Controls.Add(this.tabPage1);
                this.splitContainer1.Panel1.Controls.Add(this.pictureBox1);
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
                // tabPage1
                // 
                this.tabPage1.BackColor = System.Drawing.Color.Black;
                this.tabPage1.FlatAppearance.BorderColor = System.Drawing.Color.Black;
                this.tabPage1.FlatAppearance.BorderSize = 0;
                this.tabPage1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
                this.tabPage1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.tabPage1.ForeColor = System.Drawing.Color.White;
                this.tabPage1.Location = new System.Drawing.Point(86, 26);
                this.tabPage1.Name = "tabPage1";
                this.tabPage1.Size = new System.Drawing.Size(85, 33);
                this.tabPage1.TabIndex = 1;
                this.tabPage1.Text = "button1";
                this.tabPage1.UseVisualStyleBackColor = false;
                this.tabPage1.Click += new System.EventHandler(this.tabPage1_Click);
                // 
                // tabPage2
                // 
                this.tabPage2.BackColor = System.Drawing.Color.Black;
                this.tabPage2.FlatAppearance.BorderColor = System.Drawing.Color.Black;
                this.tabPage2.FlatAppearance.BorderSize = 0;
                this.tabPage2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
                this.tabPage2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.tabPage2.ForeColor = System.Drawing.Color.White;
                this.tabPage2.Location = new System.Drawing.Point(513, 26);
                this.tabPage2.Name = "tabPage2";
                this.tabPage2.Size = new System.Drawing.Size(75, 33);
                this.tabPage2.TabIndex = 2;
                this.tabPage2.Text = "button1";
                this.tabPage2.UseVisualStyleBackColor = false;
                this.tabPage2.Click += new System.EventHandler(this.tabPage2_Click);
                // 
                // tabPage3
                // 
                this.tabPage3.BackColor = System.Drawing.Color.Black;
                this.tabPage3.FlatAppearance.BorderColor = System.Drawing.Color.Black;
                this.tabPage3.FlatAppearance.BorderSize = 0;
                this.tabPage3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
                this.tabPage3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.tabPage3.ForeColor = System.Drawing.Color.White;
                this.tabPage3.Location = new System.Drawing.Point(213, 26);
                this.tabPage3.Name = "tabPage3";
                this.tabPage3.Size = new System.Drawing.Size(95, 33);
                this.tabPage3.TabIndex = 3;
                this.tabPage3.Text = "button1";
                this.tabPage3.UseVisualStyleBackColor = false;
                this.tabPage3.Click += new System.EventHandler(this.tabPage2_Click_1);
                // 
                // tabPage4
                // 
                this.tabPage4.BackColor = System.Drawing.Color.Black;
                this.tabPage4.FlatAppearance.BorderColor = System.Drawing.Color.Black;
                this.tabPage4.FlatAppearance.BorderSize = 0;
                this.tabPage4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
                this.tabPage4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.tabPage4.ForeColor = System.Drawing.Color.White;
                this.tabPage4.Location = new System.Drawing.Point(687, 26);
                this.tabPage4.Name = "tabPage4";
                this.tabPage4.Size = new System.Drawing.Size(95, 33);
                this.tabPage4.TabIndex = 4;
                this.tabPage4.Text = "Help";
                this.tabPage4.UseVisualStyleBackColor = false;
                this.tabPage4.Click += new System.EventHandler(this.tabPage4_Click);
                // 
                // MainWindow
                // 
                this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
                this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
                this.BackColor = System.Drawing.Color.Black;
                this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
                this.ClientSize = new System.Drawing.Size(794, 572);
                this.Controls.Add(this.splitContainer1);
                this.DoubleBuffered = true;
                this.ForeColor = System.Drawing.Color.White;
                this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
                this.Name = "MainWindow";
                this.Text = "fireBwall v0.3.11.0";
                this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainWindow_FormClosing);
                this.Load += new System.EventHandler(this.MainWindow_Load);
                this.Resize += new System.EventHandler(this.MainWindow_Resize);
                this.splitContainer1.Panel1.ResumeLayout(false);
                this.splitContainer1.ResumeLayout(false);
                ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
                this.ResumeLayout(false);

            }
            private System.Windows.Forms.SplitContainer splitContainer1;
            private System.Windows.Forms.PictureBox pictureBox1;
            private System.Windows.Forms.Button tabPage1;
            private System.Windows.Forms.Button tabPage4;
            private System.Windows.Forms.Button tabPage3;
            private System.Windows.Forms.Button tabPage2;
		}
}
