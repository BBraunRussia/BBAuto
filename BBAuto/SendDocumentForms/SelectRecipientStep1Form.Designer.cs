namespace BBAuto.SendDocumentForms
{
  partial class SelectRecipientStep1Form
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
      this.rbAll = new System.Windows.Forms.RadioButton();
      this.rbCity = new System.Windows.Forms.RadioButton();
      this.rbDriver = new System.Windows.Forms.RadioButton();
      this.btnCancel = new System.Windows.Forms.Button();
      this.btnNext = new System.Windows.Forms.Button();
      this.SuspendLayout();
      // 
      // rbAll
      // 
      this.rbAll.AutoSize = true;
      this.rbAll.Checked = true;
      this.rbAll.Location = new System.Drawing.Point(24, 23);
      this.rbAll.Name = "rbAll";
      this.rbAll.Size = new System.Drawing.Size(110, 17);
      this.rbAll.TabIndex = 0;
      this.rbAll.TabStop = true;
      this.rbAll.Text = "Всем водителям";
      this.rbAll.UseVisualStyleBackColor = true;
      // 
      // rbCity
      // 
      this.rbCity.AutoSize = true;
      this.rbCity.Location = new System.Drawing.Point(24, 57);
      this.rbCity.Name = "rbCity";
      this.rbCity.Size = new System.Drawing.Size(102, 17);
      this.rbCity.TabIndex = 1;
      this.rbCity.Text = "Выбор городов";
      this.rbCity.UseVisualStyleBackColor = true;
      // 
      // rbDriver
      // 
      this.rbDriver.AutoSize = true;
      this.rbDriver.Location = new System.Drawing.Point(24, 89);
      this.rbDriver.Name = "rbDriver";
      this.rbDriver.Size = new System.Drawing.Size(171, 17);
      this.rbDriver.TabIndex = 2;
      this.rbDriver.Text = "Выбор отдельных водителей";
      this.rbDriver.UseVisualStyleBackColor = true;
      // 
      // btnCancel
      // 
      this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.btnCancel.Location = new System.Drawing.Point(12, 130);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new System.Drawing.Size(75, 23);
      this.btnCancel.TabIndex = 3;
      this.btnCancel.Text = "Отмена";
      this.btnCancel.UseVisualStyleBackColor = true;
      // 
      // btnNext
      // 
      this.btnNext.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnNext.Location = new System.Drawing.Point(126, 130);
      this.btnNext.Name = "btnNext";
      this.btnNext.Size = new System.Drawing.Size(75, 23);
      this.btnNext.TabIndex = 4;
      this.btnNext.Text = "Далее";
      this.btnNext.UseVisualStyleBackColor = true;
      this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
      // 
      // SelectRecipientStep1Form
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.CancelButton = this.btnCancel;
      this.ClientSize = new System.Drawing.Size(213, 165);
      this.Controls.Add(this.btnNext);
      this.Controls.Add(this.btnCancel);
      this.Controls.Add(this.rbDriver);
      this.Controls.Add(this.rbCity);
      this.Controls.Add(this.rbAll);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "SelectRecipientStep1Form";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "Способ выбора получателей";
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.RadioButton rbAll;
    private System.Windows.Forms.RadioButton rbCity;
    private System.Windows.Forms.RadioButton rbDriver;
    private System.Windows.Forms.Button btnCancel;
    private System.Windows.Forms.Button btnNext;
  }
}
