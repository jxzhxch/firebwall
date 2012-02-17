namespace PassThru
{
    partial class DDoSDisplay
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
            this.dosBlockTable = new System.Windows.Forms.DataGridView();
            this.blockedip = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Reason = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DateBlocked = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.removeButton = new System.Windows.Forms.Button();
            this.addButton = new System.Windows.Forms.Button();
            this.blockipLabel = new System.Windows.Forms.Label();
            this.addField = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dosBlockTable)).BeginInit();
            this.SuspendLayout();
            // 
            // dosBlockTable
            // 
            this.dosBlockTable.AllowUserToAddRows = false;
            this.dosBlockTable.AllowUserToDeleteRows = false;
            this.dosBlockTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dosBlockTable.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.blockedip,
            this.Reason,
            this.DateBlocked});
            this.dosBlockTable.Location = new System.Drawing.Point(14, 29);
            this.dosBlockTable.Name = "dosBlockTable";
            this.dosBlockTable.ReadOnly = true;
            this.dosBlockTable.RowHeadersVisible = false;
            this.dosBlockTable.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dosBlockTable.Size = new System.Drawing.Size(574, 193);
            this.dosBlockTable.TabIndex = 0;
            // 
            // blockedip
            // 
            this.blockedip.HeaderText = "IP Address";
            this.blockedip.Name = "blockedip";
            this.blockedip.ReadOnly = true;
            this.blockedip.Width = 165;
            // 
            // Reason
            // 
            this.Reason.HeaderText = "Reason";
            this.Reason.Name = "Reason";
            this.Reason.ReadOnly = true;
            this.Reason.Width = 200;
            // 
            // DateBlocked
            // 
            this.DateBlocked.HeaderText = "Date Blocked";
            this.DateBlocked.Name = "DateBlocked";
            this.DateBlocked.ReadOnly = true;
            this.DateBlocked.Width = 200;
            // 
            // removeButton
            // 
            this.removeButton.Location = new System.Drawing.Point(513, 228);
            this.removeButton.Name = "removeButton";
            this.removeButton.Size = new System.Drawing.Size(75, 23);
            this.removeButton.TabIndex = 1;
            this.removeButton.Text = "Remove IP";
            this.removeButton.UseVisualStyleBackColor = true;
            this.removeButton.Click += new System.EventHandler(this.removeButton_Click);
            // 
            // addButton
            // 
            this.addButton.Location = new System.Drawing.Point(513, 257);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(75, 23);
            this.addButton.TabIndex = 2;
            this.addButton.Text = "Add IP";
            this.addButton.UseVisualStyleBackColor = true;
            this.addButton.Click += new System.EventHandler(this.addButton_Click);
            // 
            // blockipLabel
            // 
            this.blockipLabel.AutoSize = true;
            this.blockipLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.blockipLabel.Location = new System.Drawing.Point(11, 13);
            this.blockipLabel.Name = "blockipLabel";
            this.blockipLabel.Size = new System.Drawing.Size(75, 13);
            this.blockipLabel.TabIndex = 3;
            this.blockipLabel.Text = "Blocked IPs";
            // 
            // addField
            // 
            this.addField.Location = new System.Drawing.Point(390, 259);
            this.addField.Name = "addField";
            this.addField.Size = new System.Drawing.Size(117, 20);
            this.addField.TabIndex = 4;
            // 
            // DDoSDisplay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.addField);
            this.Controls.Add(this.blockipLabel);
            this.Controls.Add(this.addButton);
            this.Controls.Add(this.removeButton);
            this.Controls.Add(this.dosBlockTable);
            this.Name = "DDoSDisplay";
            this.Size = new System.Drawing.Size(665, 357);
            this.Load += new System.EventHandler(this.DDoSDisplay_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dosBlockTable)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dosBlockTable;
        private System.Windows.Forms.Button removeButton;
        private System.Windows.Forms.Button addButton;
        private System.Windows.Forms.Label blockipLabel;
        private System.Windows.Forms.TextBox addField;
        private System.Windows.Forms.DataGridViewTextBoxColumn blockedip;
        private System.Windows.Forms.DataGridViewTextBoxColumn Reason;
        private System.Windows.Forms.DataGridViewTextBoxColumn DateBlocked;
    }
}
