namespace BBAuto
{
    partial class formFuelCardDriver
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
      this.dgvDriverFuelCard = new System.Windows.Forms.DataGridView();
      ((System.ComponentModel.ISupportInitialize)(this.dgvDriverFuelCard)).BeginInit();
      this.SuspendLayout();
      // 
      // dgvDriverFuelCard
      // 
      this.dgvDriverFuelCard.AllowUserToAddRows = false;
      this.dgvDriverFuelCard.AllowUserToDeleteRows = false;
      this.dgvDriverFuelCard.AllowUserToResizeRows = false;
      this.dgvDriverFuelCard.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.dgvDriverFuelCard.BackgroundColor = System.Drawing.SystemColors.Window;
      this.dgvDriverFuelCard.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.dgvDriverFuelCard.Location = new System.Drawing.Point(12, 12);
      this.dgvDriverFuelCard.Name = "dgvDriverFuelCard";
      this.dgvDriverFuelCard.ReadOnly = true;
      this.dgvDriverFuelCard.RowHeadersVisible = false;
      this.dgvDriverFuelCard.Size = new System.Drawing.Size(819, 232);
      this.dgvDriverFuelCard.TabIndex = 13;
      this.dgvDriverFuelCard.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDriverFuelCard_CellDoubleClick);
      // 
      // formFuelCardDriver
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(843, 256);
      this.Controls.Add(this.dgvDriverFuelCard);
      this.Name = "formFuelCardDriver";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "Топливные карты";
      this.Load += new System.EventHandler(this.formFuelCardDriver_Load);
      ((System.ComponentModel.ISupportInitialize)(this.dgvDriverFuelCard)).EndInit();
      this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvDriverFuelCard;
    }
}