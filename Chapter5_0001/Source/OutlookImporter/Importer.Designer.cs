namespace Fisharoo.OutlookTools
{
    partial class Importer
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnGetAddresses = new System.Windows.Forms.Button();
            this.btnImport = new System.Windows.Forms.Button();
            this.clbAddresses = new System.Windows.Forms.CheckedListBox();
            this.SuspendLayout();
            // 
            // btnGetAddresses
            // 
            this.btnGetAddresses.Location = new System.Drawing.Point(3, 132);
            this.btnGetAddresses.Name = "btnGetAddresses";
            this.btnGetAddresses.Size = new System.Drawing.Size(75, 23);
            this.btnGetAddresses.TabIndex = 5;
            this.btnGetAddresses.Text = "GetAddresses";
            this.btnGetAddresses.UseVisualStyleBackColor = true;
            // 
            // btnImport
            // 
            this.btnImport.Location = new System.Drawing.Point(149, 132);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(75, 23);
            this.btnImport.TabIndex = 4;
            this.btnImport.Text = "Import";
            this.btnImport.UseVisualStyleBackColor = true;
            // 
            // clbAddresses
            // 
            this.clbAddresses.BackColor = System.Drawing.SystemColors.Control;
            this.clbAddresses.FormattingEnabled = true;
            this.clbAddresses.Location = new System.Drawing.Point(3, 2);
            this.clbAddresses.Name = "clbAddresses";
            this.clbAddresses.Size = new System.Drawing.Size(220, 124);
            this.clbAddresses.TabIndex = 3;
            // 
            // Importer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(229, 159);
            this.Controls.Add(this.btnGetAddresses);
            this.Controls.Add(this.btnImport);
            this.Controls.Add(this.clbAddresses);
            this.Name = "Importer";
            this.Text = "Importer";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnGetAddresses;
        private System.Windows.Forms.Button btnImport;
        private System.Windows.Forms.CheckedListBox clbAddresses;
    }
}