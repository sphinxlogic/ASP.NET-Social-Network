namespace FisharooOutlookImporter
{
    partial class OutlookImporter
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
            this.clbAddresses = new System.Windows.Forms.CheckedListBox();
            this.btnImport = new System.Windows.Forms.Button();
            this.btnGetAddresses = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // clbAddresses
            // 
            this.clbAddresses.BackColor = System.Drawing.SystemColors.Control;
            this.clbAddresses.FormattingEnabled = true;
            this.clbAddresses.Location = new System.Drawing.Point(4, 16);
            this.clbAddresses.Name = "clbAddresses";
            this.clbAddresses.Size = new System.Drawing.Size(220, 124);
            this.clbAddresses.TabIndex = 0;
            // 
            // btnImport
            // 
            this.btnImport.Location = new System.Drawing.Point(150, 146);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(75, 23);
            this.btnImport.TabIndex = 1;
            this.btnImport.Text = "Import";
            this.btnImport.UseVisualStyleBackColor = true;
            // 
            // btnGetAddresses
            // 
            this.btnGetAddresses.Location = new System.Drawing.Point(4, 146);
            this.btnGetAddresses.Name = "btnGetAddresses";
            this.btnGetAddresses.Size = new System.Drawing.Size(75, 23);
            this.btnGetAddresses.TabIndex = 2;
            this.btnGetAddresses.Text = "GetAddresses";
            this.btnGetAddresses.UseVisualStyleBackColor = true;
            this.btnGetAddresses.Click += new System.EventHandler(this.btnGetAddresses_Click);
            // 
            // OutlookImporter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnGetAddresses);
            this.Controls.Add(this.btnImport);
            this.Controls.Add(this.clbAddresses);
            this.Name = "OutlookImporter";
            this.Size = new System.Drawing.Size(230, 175);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckedListBox clbAddresses;
        private System.Windows.Forms.Button btnImport;
        private System.Windows.Forms.Button btnGetAddresses;
    }
}