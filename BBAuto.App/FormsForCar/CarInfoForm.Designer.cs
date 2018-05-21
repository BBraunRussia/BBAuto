namespace BBAuto.App.FormsForCar
{
    partial class CarInfoForm
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
            this._dgvCarInfo = new System.Windows.Forms.DataGridView();
            this.btnClose = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this._dgvCarInfo)).BeginInit();
            this.SuspendLayout();
            // 
            // _dgvCarInfo
            // 
            this._dgvCarInfo.AllowUserToAddRows = false;
            this._dgvCarInfo.AllowUserToDeleteRows = false;
            this._dgvCarInfo.AllowUserToResizeRows = false;
            this._dgvCarInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._dgvCarInfo.BackgroundColor = System.Drawing.SystemColors.Window;
            this._dgvCarInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this._dgvCarInfo.Location = new System.Drawing.Point(12, 12);
            this._dgvCarInfo.Name = "_dgvCarInfo";
            this._dgvCarInfo.ReadOnly = true;
            this._dgvCarInfo.RowHeadersVisible = false;
            this._dgvCarInfo.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this._dgvCarInfo.Size = new System.Drawing.Size(379, 336);
            this._dgvCarInfo.TabIndex = 1;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(316, 354);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "Закрыть";
            this.btnClose.UseVisualStyleBackColor = true;
            // 
            // formCarInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(403, 389);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this._dgvCarInfo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "formCarInfo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Техническая информация";
            this.Resize += new System.EventHandler(this.FormCarInfo_Resize);
            ((System.ComponentModel.ISupportInitialize)(this._dgvCarInfo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView _dgvCarInfo;
        private System.Windows.Forms.Button btnClose;

    }
}
