namespace BBAuto.CommonForms
{
  partial class DocumentsListForm
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
      this._dgv = new System.Windows.Forms.DataGridView();
      this.btnSend = new System.Windows.Forms.Button();
      ((System.ComponentModel.ISupportInitialize)(this._dgv)).BeginInit();
      this.SuspendLayout();
      // 
      // btnClose
      // 
      this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.btnClose.Location = new System.Drawing.Point(368, 349);
      this.btnClose.Name = "btnClose";
      this.btnClose.Size = new System.Drawing.Size(75, 23);
      this.btnClose.TabIndex = 27;
      this.btnClose.Text = "Закрыть";
      this.btnClose.UseVisualStyleBackColor = true;
      // 
      // btnDel
      // 
      this.btnDel.Location = new System.Drawing.Point(93, 12);
      this.btnDel.Name = "btnDel";
      this.btnDel.Size = new System.Drawing.Size(75, 23);
      this.btnDel.TabIndex = 26;
      this.btnDel.Text = "Удалить";
      this.btnDel.UseVisualStyleBackColor = true;
      this.btnDel.Click += new System.EventHandler(this.btnDel_Click);
      // 
      // btnAdd
      // 
      this.btnAdd.Location = new System.Drawing.Point(12, 12);
      this.btnAdd.Name = "btnAdd";
      this.btnAdd.Size = new System.Drawing.Size(75, 23);
      this.btnAdd.TabIndex = 25;
      this.btnAdd.Text = "Добавить";
      this.btnAdd.UseVisualStyleBackColor = true;
      this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
      // 
      // _dgv
      // 
      this._dgv.AllowUserToAddRows = false;
      this._dgv.AllowUserToDeleteRows = false;
      this._dgv.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this._dgv.BackgroundColor = System.Drawing.SystemColors.Window;
      this._dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this._dgv.Location = new System.Drawing.Point(12, 41);
      this._dgv.Name = "_dgv";
      this._dgv.ReadOnly = true;
      this._dgv.RowHeadersVisible = false;
      this._dgv.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
      this._dgv.Size = new System.Drawing.Size(431, 302);
      this._dgv.TabIndex = 24;
      this._dgv.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this._dgv_CellDoubleClick);
      // 
      // btnSend
      // 
      this.btnSend.Location = new System.Drawing.Point(174, 12);
      this.btnSend.Name = "btnSend";
      this.btnSend.Size = new System.Drawing.Size(75, 23);
      this.btnSend.TabIndex = 28;
      this.btnSend.Text = "Рассылка";
      this.btnSend.UseVisualStyleBackColor = true;
      // 
      // DocumentsListForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(455, 384);
      this.Controls.Add(this.btnSend);
      this.Controls.Add(this.btnClose);
      this.Controls.Add(this.btnDel);
      this.Controls.Add(this.btnAdd);
      this.Controls.Add(this._dgv);
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "DocumentsListForm";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "Документы для рассылки";
      this.Load += new System.EventHandler(this.DocumentsForSendListForm_Load);
      ((System.ComponentModel.ISupportInitialize)(this._dgv)).EndInit();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Button btnClose;
    private System.Windows.Forms.Button btnDel;
    private System.Windows.Forms.Button btnAdd;
    private System.Windows.Forms.DataGridView _dgv;
    private System.Windows.Forms.Button btnSend;
  }
}
