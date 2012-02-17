namespace PassThru
{
    partial class ICMPFilterDisplay
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
            this.label1 = new System.Windows.Forms.Label();
            this.typeField = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.codeField = new System.Windows.Forms.TextBox();
            this.addButton = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.tableDisplay = new System.Windows.Forms.DataGridView();
            this.Type = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label4 = new System.Windows.Forms.Label();
            this.deleteButton = new System.Windows.Forms.Button();
            this.allBox = new System.Windows.Forms.CheckBox();
            this.viewICMP = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.tableDisplay)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(123, 316);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "Filter Type";
            this.label1.UseCompatibleTextRendering = true;
            // 
            // typeField
            // 
            this.typeField.Location = new System.Drawing.Point(182, 313);
            this.typeField.Name = "typeField";
            this.typeField.Size = new System.Drawing.Size(61, 20);
            this.typeField.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(271, 316);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Code";
            // 
            // codeField
            // 
            this.codeField.Location = new System.Drawing.Point(309, 313);
            this.codeField.Name = "codeField";
            this.codeField.Size = new System.Drawing.Size(188, 20);
            this.codeField.TabIndex = 4;
            // 
            // addButton
            // 
            this.addButton.Location = new System.Drawing.Point(513, 311);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(97, 23);
            this.addButton.TabIndex = 5;
            this.addButton.Text = "Add";
            this.addButton.UseVisualStyleBackColor = true;
            this.addButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(306, 336);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(164, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Codes may be comma separated.";
            // 
            // tableDisplay
            // 
            this.tableDisplay.AllowUserToAddRows = false;
            this.tableDisplay.AllowUserToDeleteRows = false;
            this.tableDisplay.AllowUserToResizeRows = false;
            this.tableDisplay.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tableDisplay.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Type,
            this.Code,
            this.Description});
            this.tableDisplay.Location = new System.Drawing.Point(16, 34);
            this.tableDisplay.MultiSelect = false;
            this.tableDisplay.Name = "tableDisplay";
            this.tableDisplay.ReadOnly = true;
            this.tableDisplay.RowHeadersVisible = false;
            this.tableDisplay.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tableDisplay.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.tableDisplay.Size = new System.Drawing.Size(604, 263);
            this.tableDisplay.TabIndex = 7;
            // 
            // Type
            // 
            this.Type.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.Type.HeaderText = "Type";
            this.Type.Name = "Type";
            this.Type.ReadOnly = true;
            this.Type.Width = 56;
            // 
            // Code
            // 
            this.Code.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.Code.HeaderText = "Code";
            this.Code.Name = "Code";
            this.Code.ReadOnly = true;
            this.Code.Width = 57;
            // 
            // Description
            // 
            this.Description.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Description.HeaderText = "Description";
            this.Description.Name = "Description";
            this.Description.ReadOnly = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(16, 15);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(83, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Filtered ICMP";
            // 
            // deleteButton
            // 
            this.deleteButton.Location = new System.Drawing.Point(513, 341);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(97, 23);
            this.deleteButton.TabIndex = 9;
            this.deleteButton.Text = "Delete Selected";
            this.deleteButton.UseVisualStyleBackColor = true;
            this.deleteButton.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // allBox
            // 
            this.allBox.AutoSize = true;
            this.allBox.Location = new System.Drawing.Point(19, 315);
            this.allBox.Name = "allBox";
            this.allBox.Size = new System.Drawing.Size(96, 17);
            this.allBox.TabIndex = 10;
            this.allBox.Text = "Block All ICMP";
            this.allBox.UseVisualStyleBackColor = true;
            this.allBox.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // viewICMP
            // 
            this.viewICMP.Location = new System.Drawing.Point(513, 371);
            this.viewICMP.Name = "viewICMP";
            this.viewICMP.Size = new System.Drawing.Size(97, 23);
            this.viewICMP.TabIndex = 11;
            this.viewICMP.Text = "View ICMP";
            this.viewICMP.UseVisualStyleBackColor = true;
            this.viewICMP.Click += new System.EventHandler(this.button1_Click_2);
            // 
            // ICMPFilterDisplay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.viewICMP);
            this.Controls.Add(this.allBox);
            this.Controls.Add(this.deleteButton);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.tableDisplay);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.addButton);
            this.Controls.Add(this.codeField);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.typeField);
            this.Controls.Add(this.label1);
            this.Name = "ICMPFilterDisplay";
            this.Size = new System.Drawing.Size(679, 451);
            this.Load += new System.EventHandler(this.ICMPFilterDisplay_Load);
            ((System.ComponentModel.ISupportInitialize)(this.tableDisplay)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox typeField;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox codeField;
        private System.Windows.Forms.Button addButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView tableDisplay;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button deleteButton;
        private System.Windows.Forms.CheckBox allBox;
        private System.Windows.Forms.Button viewICMP;
        private System.Windows.Forms.DataGridViewTextBoxColumn Type;
        private System.Windows.Forms.DataGridViewTextBoxColumn Code;
        private System.Windows.Forms.DataGridViewTextBoxColumn Description;

    }
}
