namespace BBAuto.App.CommonForms
{
    partial class TemplateListForm
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
            this._dgvTemplate = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this._dgvTemplate)).BeginInit();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(368, 349);
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
            // _dgvTemplate
            // 
            this._dgvTemplate.AllowUserToAddRows = false;
            this._dgvTemplate.AllowUserToDeleteRows = false;
            this._dgvTemplate.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._dgvTemplate.BackgroundColor = System.Drawing.SystemColors.Window;
            this._dgvTemplate.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this._dgvTemplate.Location = new System.Drawing.Point(12, 41);
            this._dgvTemplate.Name = "_dgvTemplate";
            this._dgvTemplate.ReadOnly = true;
            this._dgvTemplate.RowHeadersVisible = false;
            this._dgvTemplate.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this._dgvTemplate.Size = new System.Drawing.Size(431, 302);
            this._dgvTemplate.TabIndex = 20;
            this._dgvTemplate.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this._dgvTemplate_CellDoubleClick);
            this._dgvTemplate.Resize += new System.EventHandler(this._dgvTemplate_Resize);
            // 
            // formTemplateList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(455, 384);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnDel);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this._dgvTemplate);
            this.Name = "formTemplateList";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Шаблоны документов";
            this.Load += new System.EventHandler(this.TemplateList_Load);
            ((System.ComponentModel.ISupportInitialize)(this._dgvTemplate)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnDel;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.DataGridView _dgvTemplate;
    }
}
