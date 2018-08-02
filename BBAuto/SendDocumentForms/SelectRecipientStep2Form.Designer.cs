namespace BBAuto.SendDocumentForms
{
  partial class SelectRecipientStep2Form
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
      this.btnNext = new System.Windows.Forms.Button();
      this.btnCancel = new System.Windows.Forms.Button();
      this.SuspendLayout();
      // 
      // chbList
      // 
      this.chbList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.chbList.CheckOnClick = true;
      this.chbList.FormattingEnabled = true;
      this.chbList.Location = new System.Drawing.Point(12, 12);
      this.chbList.MultiColumn = true;
      this.chbList.Name = "chbList";
      this.chbList.Size = new System.Drawing.Size(576, 424);
      this.chbList.TabIndex = 0;
      // 
      // btnNext
      // 
      this.btnNext.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnNext.Location = new System.Drawing.Point(513, 458);
      this.btnNext.Name = "btnNext";
      this.btnNext.Size = new System.Drawing.Size(75, 23);
      this.btnNext.TabIndex = 6;
      this.btnNext.Text = "Далее";
      this.btnNext.UseVisualStyleBackColor = true;
      this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
      // 
      // btnCancel
      // 
      this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.btnCancel.Location = new System.Drawing.Point(12, 458);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new System.Drawing.Size(75, 23);
      this.btnCancel.TabIndex = 5;
      this.btnCancel.Text = "Отмена";
      this.btnCancel.UseVisualStyleBackColor = true;
      // 
      // SelectRecipientStep2Form
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(600, 493);
      this.Controls.Add(this.btnNext);
      this.Controls.Add(this.btnCancel);
      this.Controls.Add(this.chbList);
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "SelectRecipientStep2Form";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "SelectRecipientStep2Form";
      this.Load += new System.EventHandler(this.SelectRecipientStep2Form_Load);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.CheckedListBox chbList;
    private System.Windows.Forms.Button btnNext;
    private System.Windows.Forms.Button btnCancel;
  }
}
