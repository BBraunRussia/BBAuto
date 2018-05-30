namespace BBAuto
{
    partial class formDTPList
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
      this.dgvDTP = new System.Windows.Forms.DataGridView();
      ((System.ComponentModel.ISupportInitialize)(this.dgvDTP)).BeginInit();
      this.SuspendLayout();
      // 
      // dgvDTP
      // 
      this.dgvDTP.AllowUserToAddRows = false;
      this.dgvDTP.AllowUserToDeleteRows = false;
      this.dgvDTP.AllowUserToResizeRows = false;
      this.dgvDTP.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.dgvDTP.BackgroundColor = System.Drawing.SystemColors.Window;
      this.dgvDTP.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.dgvDTP.Location = new System.Drawing.Point(12, 12);
      this.dgvDTP.Name = "dgvDTP";
      this.dgvDTP.ReadOnly = true;
      this.dgvDTP.RowHeadersVisible = false;
      this.dgvDTP.Size = new System.Drawing.Size(608, 377);
      this.dgvDTP.TabIndex = 13;
      this.dgvDTP.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDTP_CellDoubleClick);
      // 
      // formDTPList
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(632, 401);
      this.Controls.Add(this.dgvDTP);
      this.Name = "formDTPList";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "formDTPList";
      this.Load += new System.EventHandler(this.formDTPList_Load);
      ((System.ComponentModel.ISupportInitialize)(this.dgvDTP)).EndInit();
      this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvDTP;
    }
}