namespace BBAuto.App.FormsForDriver
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
            this.dgvDriverCar = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDriverCar)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvDriverCar
            // 
            this.dgvDriverCar.AllowUserToAddRows = false;
            this.dgvDriverCar.AllowUserToDeleteRows = false;
            this.dgvDriverCar.AllowUserToResizeRows = false;
            this.dgvDriverCar.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvDriverCar.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgvDriverCar.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDriverCar.Location = new System.Drawing.Point(12, 12);
            this.dgvDriverCar.Name = "dgvDriverCar";
            this.dgvDriverCar.ReadOnly = true;
            this.dgvDriverCar.RowHeadersVisible = false;
            this.dgvDriverCar.Size = new System.Drawing.Size(819, 232);
            this.dgvDriverCar.TabIndex = 13;
            // 
            // formFuelCardDriver
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(843, 256);
            this.Controls.Add(this.dgvDriverCar);
            this.Name = "formFuelCardDriver";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Топливные карты";
            this.Load += new System.EventHandler(this.formFuelCardDriver_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDriverCar)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvDriverCar;
    }
}
