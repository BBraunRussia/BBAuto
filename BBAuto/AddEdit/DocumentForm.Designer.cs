namespace BBAuto.AddEdit
{
  partial class DocumentForm
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
      this.btnSave = new System.Windows.Forms.Button();
      this.btnBrowse = new System.Windows.Forms.Button();
      this.tbPath = new System.Windows.Forms.TextBox();
      this.label2 = new System.Windows.Forms.Label();
      this.tbName = new System.Windows.Forms.TextBox();
      this.label1 = new System.Windows.Forms.Label();
      this.chbInstraction = new System.Windows.Forms.CheckBox();
      this.SuspendLayout();
      // 
      // btnClose
      // 
      this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.btnClose.Location = new System.Drawing.Point(280, 142);
      this.btnClose.Name = "btnClose";
      this.btnClose.Size = new System.Drawing.Size(75, 23);
      this.btnClose.TabIndex = 13;
      this.btnClose.Text = "Отмена";
      this.btnClose.UseVisualStyleBackColor = true;
      // 
      // btnSave
      // 
      this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnSave.DialogResult = System.Windows.Forms.DialogResult.OK;
      this.btnSave.Location = new System.Drawing.Point(199, 142);
      this.btnSave.Name = "btnSave";
      this.btnSave.Size = new System.Drawing.Size(75, 23);
      this.btnSave.TabIndex = 12;
      this.btnSave.Text = "Сохранить";
      this.btnSave.UseVisualStyleBackColor = true;
      this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
      // 
      // btnBrowse
      // 
      this.btnBrowse.Location = new System.Drawing.Point(244, 74);
      this.btnBrowse.Name = "btnBrowse";
      this.btnBrowse.Size = new System.Drawing.Size(75, 23);
      this.btnBrowse.TabIndex = 11;
      this.btnBrowse.Text = "Обзор...";
      this.btnBrowse.UseVisualStyleBackColor = true;
      this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
      // 
      // tbPath
      // 
      this.tbPath.Location = new System.Drawing.Point(12, 76);
      this.tbPath.Name = "tbPath";
      this.tbPath.Size = new System.Drawing.Size(223, 20);
      this.tbPath.TabIndex = 10;
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(12, 60);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(77, 13);
      this.label2.TabIndex = 9;
      this.label2.Text = "Путь к файлу:";
      // 
      // tbName
      // 
      this.tbName.Location = new System.Drawing.Point(12, 25);
      this.tbName.Name = "tbName";
      this.tbName.Size = new System.Drawing.Size(223, 20);
      this.tbName.TabIndex = 8;
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(12, 9);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(60, 13);
      this.label1.TabIndex = 7;
      this.label1.Text = "Название:";
      // 
      // chbInstraction
      // 
      this.chbInstraction.AutoSize = true;
      this.chbInstraction.Location = new System.Drawing.Point(12, 114);
      this.chbInstraction.Name = "chbInstraction";
      this.chbInstraction.Size = new System.Drawing.Size(87, 17);
      this.chbInstraction.TabIndex = 14;
      this.chbInstraction.Text = "Инструктаж";
      this.chbInstraction.UseVisualStyleBackColor = true;
      // 
      // DocumentForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(367, 177);
      this.Controls.Add(this.chbInstraction);
      this.Controls.Add(this.btnClose);
      this.Controls.Add(this.btnSave);
      this.Controls.Add(this.btnBrowse);
      this.Controls.Add(this.tbPath);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.tbName);
      this.Controls.Add(this.label1);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "DocumentForm";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "Документ для рассылки";
      this.Load += new System.EventHandler(this.DocumentForm_Load);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button btnClose;
    private System.Windows.Forms.Button btnSave;
    private System.Windows.Forms.Button btnBrowse;
    private System.Windows.Forms.TextBox tbPath;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.TextBox tbName;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.CheckBox chbInstraction;
  }
}
