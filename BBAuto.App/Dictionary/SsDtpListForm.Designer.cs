namespace BBAuto.App.Dictionary
{
    partial class SsDtpListForm
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
            this.btnClose = new System.Windows.Forms.Button();
            this.btnDel = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this._dgvSsDTP = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this._dgvSsDTP)).BeginInit();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(340, 244);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 23;
            this.btnClose.Text = "Закрыть";
            this.btnClose.UseVisualStyleBackColor = true;
            // 
            // btnDel
            // 
            this.btnDel.Location = new System.Drawing.Point(93, 12);
            this.btnDel.Name = "btnDel";
            this.btnDel.Size = new System.Drawing.Size(75, 23);
            this.btnDel.TabIndex = 22;
            this.btnDel.Text = "Удалить";
            this.btnDel.UseVisualStyleBackColor = true;
            this.btnDel.Click += new System.EventHandler(this.btnDel_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(12, 12);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 21;
            this.btnAdd.Text = "Добавить";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // _dgvSsDTP
            // 
            this._dgvSsDTP.AllowUserToAddRows = false;
            this._dgvSsDTP.AllowUserToDeleteRows = false;
            this._dgvSsDTP.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._dgvSsDTP.BackgroundColor = System.Drawing.SystemColors.Window;
            this._dgvSsDTP.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this._dgvSsDTP.Location = new System.Drawing.Point(12, 41);
            this._dgvSsDTP.Name = "_dgvSsDTP";
            this._dgvSsDTP.ReadOnly = true;
            this._dgvSsDTP.RowHeadersVisible = false;
            this._dgvSsDTP.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this._dgvSsDTP.Size = new System.Drawing.Size(403, 197);
            this._dgvSsDTP.TabIndex = 20;
            this._dgvSsDTP.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this._dgvSsDTP_CellMouseDoubleClick);
            // 
            // formSsDTPList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(427, 279);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnDel);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this._dgvSsDTP);
            this.Name = "formSsDTPList";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "СТО для страховых по маркам";
            this.Load += new System.EventHandler(this.FormSsDTPList_Load);
            ((System.ComponentModel.ISupportInitialize)(this._dgvSsDTP)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnDel;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.DataGridView _dgvSsDTP;
    }
}
