namespace PassThru.Modules
{
    partial class ModuleConfiguration
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
            this.checkedListBoxModules = new System.Windows.Forms.CheckedListBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.buttonOpenConfiguration = new System.Windows.Forms.Button();
            this.buttonEnable = new System.Windows.Forms.Button();
            this.buttonMoveUp = new System.Windows.Forms.Button();
            this.buttonMoveDown = new System.Windows.Forms.Button();
            this.buttonHelp = new System.Windows.Forms.Button();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // checkedListBoxModules
            // 
            this.checkedListBoxModules.Dock = System.Windows.Forms.DockStyle.Fill;
            this.checkedListBoxModules.FormattingEnabled = true;
            this.checkedListBoxModules.Location = new System.Drawing.Point(3, 3);
            this.checkedListBoxModules.Name = "checkedListBoxModules";
            this.checkedListBoxModules.Size = new System.Drawing.Size(305, 338);
            this.checkedListBoxModules.TabIndex = 0;
            this.checkedListBoxModules.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.checkedListBoxModules_ItemCheck_1);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.buttonOpenConfiguration, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.buttonEnable, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.buttonMoveUp, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.buttonMoveDown, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.buttonHelp, 0, 4);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(314, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(114, 338);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // buttonOpenConfiguration
            // 
            this.buttonOpenConfiguration.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonOpenConfiguration.Location = new System.Drawing.Point(3, 70);
            this.buttonOpenConfiguration.Name = "buttonOpenConfiguration";
            this.buttonOpenConfiguration.Size = new System.Drawing.Size(108, 61);
            this.buttonOpenConfiguration.TabIndex = 1;
            this.buttonOpenConfiguration.Text = "Open Configuration";
            this.buttonOpenConfiguration.UseVisualStyleBackColor = true;
            this.buttonOpenConfiguration.Click += new System.EventHandler(this.buttonOpenConfiguration_Click);
            // 
            // buttonEnable
            // 
            this.buttonEnable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonEnable.Location = new System.Drawing.Point(3, 3);
            this.buttonEnable.Name = "buttonEnable";
            this.buttonEnable.Size = new System.Drawing.Size(108, 61);
            this.buttonEnable.TabIndex = 0;
            this.buttonEnable.Text = "Enable/Disable";
            this.buttonEnable.UseVisualStyleBackColor = true;
            this.buttonEnable.Click += new System.EventHandler(this.buttonEnable_Click);
            // 
            // buttonMoveUp
            // 
            this.buttonMoveUp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonMoveUp.Location = new System.Drawing.Point(3, 137);
            this.buttonMoveUp.Name = "buttonMoveUp";
            this.buttonMoveUp.Size = new System.Drawing.Size(108, 61);
            this.buttonMoveUp.TabIndex = 2;
            this.buttonMoveUp.Text = "Move Up";
            this.buttonMoveUp.UseVisualStyleBackColor = true;
            this.buttonMoveUp.Click += new System.EventHandler(this.buttonMoveUp_Click);
            // 
            // buttonMoveDown
            // 
            this.buttonMoveDown.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonMoveDown.Location = new System.Drawing.Point(3, 204);
            this.buttonMoveDown.Name = "buttonMoveDown";
            this.buttonMoveDown.Size = new System.Drawing.Size(108, 61);
            this.buttonMoveDown.TabIndex = 3;
            this.buttonMoveDown.Text = "Move Down";
            this.buttonMoveDown.UseVisualStyleBackColor = true;
            this.buttonMoveDown.Click += new System.EventHandler(this.buttonMoveDown_Click);
            // 
            // buttonHelp
            // 
            this.buttonHelp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonHelp.Location = new System.Drawing.Point(3, 271);
            this.buttonHelp.Name = "buttonHelp";
            this.buttonHelp.Size = new System.Drawing.Size(108, 64);
            this.buttonHelp.TabIndex = 4;
            this.buttonHelp.Text = "Help";
            this.buttonHelp.UseVisualStyleBackColor = true;
            this.buttonHelp.Click += new System.EventHandler(this.buttonHelp_Click);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel1, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.checkedListBoxModules, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(431, 344);
            this.tableLayoutPanel2.TabIndex = 2;
            // 
            // ModuleConfiguration
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel2);
            this.Name = "ModuleConfiguration";
            this.Size = new System.Drawing.Size(431, 344);
            this.Load += new System.EventHandler(this.ModuleConfiguration_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckedListBox checkedListBoxModules;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button buttonOpenConfiguration;
        private System.Windows.Forms.Button buttonEnable;
        private System.Windows.Forms.Button buttonMoveUp;
        private System.Windows.Forms.Button buttonMoveDown;
        private System.Windows.Forms.Button buttonHelp;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
    }
}
