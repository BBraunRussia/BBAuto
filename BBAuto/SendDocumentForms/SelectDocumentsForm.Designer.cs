namespace BBAuto.SendDocumentForms
{
  partial class SelectDocumentsForm
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
      this.chbList = new System.Windows.Forms.CheckedListBox();
      this.btnSend = new System.Windows.Forms.Button();
      this.btnCancel = new System.Windows.Forms.Button();
      this.listBoxDrivers = new System.Windows.Forms.ListBox();
      this.label1 = new System.Windows.Forms.Label();
      this.label2 = new System.Windows.Forms.Label();
      this.SuspendLayout();
      // 
      // chbList
      // 
      this.chbList.CheckOnClick = true;
      this.chbList.FormattingEnabled = true;
      this.chbList.Location = new System.Drawing.Point(180, 32);
      this.chbList.Name = "chbList";
      this.chbList.Size = new System.Drawing.Size(266, 289);
      this.chbList.TabIndex = 0;
      // 
      // btnSend
      // 
      this.btnSend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnSend.Location = new System.Drawing.Point(371, 340);
      this.btnSend.Name = "btnSend";
      this.btnSend.Size = new System.Drawing.Size(75, 23);
      this.btnSend.TabIndex = 8;
      this.btnSend.Text = "Отправить";
      this.btnSend.UseVisualStyleBackColor = true;
      this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
      // 
      // btnCancel
      // 
      this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.btnCancel.Location = new System.Drawing.Point(12, 340);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new System.Drawing.Size(75, 23);
      this.btnCancel.TabIndex = 7;
      this.btnCancel.Text = "Отмена";
      this.btnCancel.UseVisualStyleBackColor = true;
      // 
      // listBoxDrivers
      // 
      this.listBoxDrivers.FormattingEnabled = true;
      this.listBoxDrivers.Location = new System.Drawing.Point(12, 32);
      this.listBoxDrivers.Name = "listBoxDrivers";
      this.listBoxDrivers.Size = new System.Drawing.Size(162, 290);
      this.listBoxDrivers.TabIndex = 9;
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(13, 13);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(69, 13);
      this.label1.TabIndex = 10;
      this.label1.Text = "Получатели:";
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(177, 13);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(140, 13);
      this.label2.TabIndex = 11;
      this.label2.Text = "Документы для отправки:";
      // 
      // SelectDocumentsForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(458, 375);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.listBoxDrivers);
      this.Controls.Add(this.btnSend);
      this.Controls.Add(this.btnCancel);
      this.Controls.Add(this.chbList);
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "SelectDocumentsForm";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "Документы для отправки";
      this.Load += new System.EventHandler(this.SelectDocumentsForm_Load);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.CheckedListBox chbList;
    private System.Windows.Forms.Button btnSend;
    private System.Windows.Forms.Button btnCancel;
    private System.Windows.Forms.ListBox listBoxDrivers;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label label2;
  }
}
