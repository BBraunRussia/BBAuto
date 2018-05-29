namespace BBAuto
{
    partial class formViolationList
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
      this.dgvViolation = new System.Windows.Forms.DataGridView();
      ((System.ComponentModel.ISupportInitialize)(this.dgvViolation)).BeginInit();
      this.SuspendLayout();
      // 
      // dgvViolation
      // 
      this.dgvViolation.AllowUserToAddRows = false;
      this.dgvViolation.AllowUserToDeleteRows = false;
      this.dgvViolation.AllowUserToResizeRows = false;
      this.dgvViolation.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.dgvViolation.BackgroundColor = System.Drawing.SystemColors.Window;
      this.dgvViolation.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.dgvViolation.Location = new System.Drawing.Point(12, 12);
      this.dgvViolation.Name = "dgvViolation";
      this.dgvViolation.ReadOnly = true;
      this.dgvViolation.RowHeadersVisible = false;
      this.dgvViolation.Size = new System.Drawing.Size(707, 232);
      this.dgvViolation.TabIndex = 14;
      this.dgvViolation.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvViolation_CellDoubleClick);
      // 
      // formViolationList
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(731, 256);
      this.Controls.Add(this.dgvViolation);
      this.Name = "formViolationList";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "Нарушения ПДД";
      this.Load += new System.EventHandler(this.ViolationList_Load);
      ((System.ComponentModel.ISupportInitialize)(this.dgvViolation)).EndInit();
      this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvViolation;
    }
}
