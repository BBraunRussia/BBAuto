namespace BBAuto.AddEdit
{
  partial class DriverTransponderForm
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
      this.btnSave = new System.Windows.Forms.Button();
      this.btnClose = new System.Windows.Forms.Button();
      this.chbNotUse = new System.Windows.Forms.CheckBox();
      this.dtpDateEnd = new System.Windows.Forms.DateTimePicker();
      this.label3 = new System.Windows.Forms.Label();
      this.dtpDateBegin = new System.Windows.Forms.DateTimePicker();
      this.label2 = new System.Windows.Forms.Label();
      this.cbDriver = new System.Windows.Forms.ComboBox();
      this.label1 = new System.Windows.Forms.Label();
      this.SuspendLayout();
      // 
      // btnSave
      // 
      this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnSave.Location = new System.Drawing.Point(123, 129);
      this.btnSave.Name = "btnSave";
      this.btnSave.Size = new System.Drawing.Size(95, 23);
      this.btnSave.TabIndex = 17;
      this.btnSave.Text = "Сохранить";
      this.btnSave.UseVisualStyleBackColor = true;
      this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
      // 
      // btnClose
      // 
      this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.btnClose.Location = new System.Drawing.Point(224, 129);
      this.btnClose.Name = "btnClose";
      this.btnClose.Size = new System.Drawing.Size(75, 23);
      this.btnClose.TabIndex = 16;
      this.btnClose.Text = "Закрыть";
      this.btnClose.UseVisualStyleBackColor = true;
      // 
      // chbNotUse
      // 
      this.chbNotUse.AutoSize = true;
      this.chbNotUse.Location = new System.Drawing.Point(12, 100);
      this.chbNotUse.Name = "chbNotUse";
      this.chbNotUse.Size = new System.Drawing.Size(153, 17);
      this.chbNotUse.TabIndex = 15;
      this.chbNotUse.Text = "Больше не используется";
      this.chbNotUse.UseVisualStyleBackColor = true;
      this.chbNotUse.CheckedChanged += new System.EventHandler(this.chbNotUse_CheckedChanged);
      // 
      // dtpDateEnd
      // 
      this.dtpDateEnd.CustomFormat = "dd-MM-yyyy";
      this.dtpDateEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
      this.dtpDateEnd.Location = new System.Drawing.Point(146, 74);
      this.dtpDateEnd.Name = "dtpDateEnd";
      this.dtpDateEnd.Size = new System.Drawing.Size(125, 20);
      this.dtpDateEnd.TabIndex = 14;
      this.dtpDateEnd.Visible = false;
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(143, 58);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(146, 13);
      this.label3.TabIndex = 13;
      this.label3.Text = "Окончание использования:";
      this.label3.Visible = false;
      // 
      // dtpDateBegin
      // 
      this.dtpDateBegin.CustomFormat = "dd-MM-yyyy";
      this.dtpDateBegin.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
      this.dtpDateBegin.Location = new System.Drawing.Point(12, 74);
      this.dtpDateBegin.Name = "dtpDateBegin";
      this.dtpDateBegin.Size = new System.Drawing.Size(125, 20);
      this.dtpDateBegin.TabIndex = 12;
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(9, 58);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(128, 13);
      this.label2.TabIndex = 11;
      this.label2.Text = "Начало использования:";
      // 
      // cbDriver
      // 
      this.cbDriver.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cbDriver.FormattingEnabled = true;
      this.cbDriver.Location = new System.Drawing.Point(12, 29);
      this.cbDriver.Name = "cbDriver";
      this.cbDriver.Size = new System.Drawing.Size(273, 21);
      this.cbDriver.TabIndex = 10;
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(9, 13);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(58, 13);
      this.label1.TabIndex = 9;
      this.label1.Text = "Водитель:";
      // 
      // DriverTransponderForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(311, 164);
      this.Controls.Add(this.btnSave);
      this.Controls.Add(this.btnClose);
      this.Controls.Add(this.chbNotUse);
      this.Controls.Add(this.dtpDateEnd);
      this.Controls.Add(this.label3);
      this.Controls.Add(this.dtpDateBegin);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.cbDriver);
      this.Controls.Add(this.label1);
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "DriverTransponderForm";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "DriverTransponderForm";
      this.Load += new System.EventHandler(this.DriverTransponderForm_Load);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button btnSave;
    private System.Windows.Forms.Button btnClose;
    private System.Windows.Forms.CheckBox chbNotUse;
    private System.Windows.Forms.DateTimePicker dtpDateEnd;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.DateTimePicker dtpDateBegin;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.ComboBox cbDriver;
    private System.Windows.Forms.Label label1;
  }
}
