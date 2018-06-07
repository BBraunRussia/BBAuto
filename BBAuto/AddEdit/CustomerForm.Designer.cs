namespace BBAuto.AddEdit
{
  partial class CustomerForm
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
      this.mtbGiveDate = new System.Windows.Forms.MaskedTextBox();
      this.btnClose = new System.Windows.Forms.Button();
      this.btnSave = new System.Windows.Forms.Button();
      this.tbAddress = new System.Windows.Forms.TextBox();
      this.label7 = new System.Windows.Forms.Label();
      this.label6 = new System.Windows.Forms.Label();
      this.tbGiveOrg = new System.Windows.Forms.TextBox();
      this.label5 = new System.Windows.Forms.Label();
      this.mtbNumber = new System.Windows.Forms.MaskedTextBox();
      this.label4 = new System.Windows.Forms.Label();
      this.tbSecondName = new System.Windows.Forms.TextBox();
      this.label3 = new System.Windows.Forms.Label();
      this.tbFirstName = new System.Windows.Forms.TextBox();
      this.label2 = new System.Windows.Forms.Label();
      this.tbLastName = new System.Windows.Forms.TextBox();
      this.label1 = new System.Windows.Forms.Label();
      this.tbInn = new System.Windows.Forms.TextBox();
      this.label8 = new System.Windows.Forms.Label();
      this.SuspendLayout();
      // 
      // mtbGiveDate
      // 
      this.mtbGiveDate.Location = new System.Drawing.Point(212, 66);
      this.mtbGiveDate.Mask = "00/00/0000";
      this.mtbGiveDate.Name = "mtbGiveDate";
      this.mtbGiveDate.Size = new System.Drawing.Size(197, 20);
      this.mtbGiveDate.TabIndex = 5;
      // 
      // btnClose
      // 
      this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.btnClose.Location = new System.Drawing.Point(342, 224);
      this.btnClose.Name = "btnClose";
      this.btnClose.Size = new System.Drawing.Size(75, 23);
      this.btnClose.TabIndex = 10;
      this.btnClose.Text = "Закрыть";
      this.btnClose.UseVisualStyleBackColor = true;
      // 
      // btnSave
      // 
      this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnSave.Location = new System.Drawing.Point(241, 224);
      this.btnSave.Name = "btnSave";
      this.btnSave.Size = new System.Drawing.Size(95, 23);
      this.btnSave.TabIndex = 9;
      this.btnSave.Text = "Сохранить";
      this.btnSave.UseVisualStyleBackColor = true;
      this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
      // 
      // tbAddress
      // 
      this.tbAddress.Location = new System.Drawing.Point(212, 107);
      this.tbAddress.MaxLength = 200;
      this.tbAddress.Multiline = true;
      this.tbAddress.Name = "tbAddress";
      this.tbAddress.Size = new System.Drawing.Size(197, 69);
      this.tbAddress.TabIndex = 7;
      // 
      // label7
      // 
      this.label7.AutoSize = true;
      this.label7.Location = new System.Drawing.Point(209, 91);
      this.label7.Name = "label7";
      this.label7.Size = new System.Drawing.Size(108, 13);
      this.label7.TabIndex = 33;
      this.label7.Text = "Адрес регистрации:";
      // 
      // label6
      // 
      this.label6.AutoSize = true;
      this.label6.Location = new System.Drawing.Point(209, 50);
      this.label6.Name = "label6";
      this.label6.Size = new System.Drawing.Size(76, 13);
      this.label6.TabIndex = 31;
      this.label6.Text = "Дата выдачи:";
      // 
      // tbGiveOrg
      // 
      this.tbGiveOrg.Location = new System.Drawing.Point(13, 107);
      this.tbGiveOrg.MaxLength = 200;
      this.tbGiveOrg.Multiline = true;
      this.tbGiveOrg.Name = "tbGiveOrg";
      this.tbGiveOrg.Size = new System.Drawing.Size(193, 69);
      this.tbGiveOrg.TabIndex = 6;
      // 
      // label5
      // 
      this.label5.AutoSize = true;
      this.label5.Location = new System.Drawing.Point(10, 91);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(66, 13);
      this.label5.TabIndex = 32;
      this.label5.Text = "Кем выдан:";
      // 
      // mtbNumber
      // 
      this.mtbNumber.Location = new System.Drawing.Point(13, 68);
      this.mtbNumber.Mask = "AA AA 000000";
      this.mtbNumber.Name = "mtbNumber";
      this.mtbNumber.Size = new System.Drawing.Size(193, 20);
      this.mtbNumber.TabIndex = 4;
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Location = new System.Drawing.Point(10, 52);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(85, 13);
      this.label4.TabIndex = 30;
      this.label4.Text = "Серия и номер:";
      // 
      // tbSecondName
      // 
      this.tbSecondName.Location = new System.Drawing.Point(281, 30);
      this.tbSecondName.MaxLength = 50;
      this.tbSecondName.Name = "tbSecondName";
      this.tbSecondName.Size = new System.Drawing.Size(128, 20);
      this.tbSecondName.TabIndex = 3;
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(278, 14);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(57, 13);
      this.label3.TabIndex = 29;
      this.label3.Text = "Отчество:";
      // 
      // tbFirstName
      // 
      this.tbFirstName.Location = new System.Drawing.Point(147, 30);
      this.tbFirstName.MaxLength = 50;
      this.tbFirstName.Name = "tbFirstName";
      this.tbFirstName.Size = new System.Drawing.Size(128, 20);
      this.tbFirstName.TabIndex = 2;
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(144, 14);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(32, 13);
      this.label2.TabIndex = 28;
      this.label2.Text = "Имя:";
      // 
      // tbLastName
      // 
      this.tbLastName.Location = new System.Drawing.Point(13, 30);
      this.tbLastName.MaxLength = 50;
      this.tbLastName.Name = "tbLastName";
      this.tbLastName.Size = new System.Drawing.Size(128, 20);
      this.tbLastName.TabIndex = 1;
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(10, 14);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(59, 13);
      this.label1.TabIndex = 27;
      this.label1.Text = "Фамилия:";
      // 
      // tbInn
      // 
      this.tbInn.Location = new System.Drawing.Point(13, 195);
      this.tbInn.MaxLength = 12;
      this.tbInn.Name = "tbInn";
      this.tbInn.Size = new System.Drawing.Size(128, 20);
      this.tbInn.TabIndex = 8;
      // 
      // label8
      // 
      this.label8.AutoSize = true;
      this.label8.Location = new System.Drawing.Point(10, 179);
      this.label8.Name = "label8";
      this.label8.Size = new System.Drawing.Size(34, 13);
      this.label8.TabIndex = 35;
      this.label8.Text = "ИНН:";
      // 
      // CustomerForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.CancelButton = this.btnClose;
      this.ClientSize = new System.Drawing.Size(429, 259);
      this.Controls.Add(this.tbInn);
      this.Controls.Add(this.label8);
      this.Controls.Add(this.mtbGiveDate);
      this.Controls.Add(this.btnClose);
      this.Controls.Add(this.btnSave);
      this.Controls.Add(this.tbAddress);
      this.Controls.Add(this.label7);
      this.Controls.Add(this.label6);
      this.Controls.Add(this.tbGiveOrg);
      this.Controls.Add(this.label5);
      this.Controls.Add(this.mtbNumber);
      this.Controls.Add(this.label4);
      this.Controls.Add(this.tbSecondName);
      this.Controls.Add(this.label3);
      this.Controls.Add(this.tbFirstName);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.tbLastName);
      this.Controls.Add(this.label1);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "CustomerForm";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "Карточка \"Покупатель\"";
      this.Load += new System.EventHandler(this.CustomerForm_Load);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.MaskedTextBox mtbGiveDate;
    private System.Windows.Forms.Button btnClose;
    private System.Windows.Forms.Button btnSave;
    private System.Windows.Forms.TextBox tbAddress;
    private System.Windows.Forms.Label label7;
    private System.Windows.Forms.Label label6;
    private System.Windows.Forms.TextBox tbGiveOrg;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.MaskedTextBox mtbNumber;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.TextBox tbSecondName;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.TextBox tbFirstName;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.TextBox tbLastName;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.TextBox tbInn;
    private System.Windows.Forms.Label label8;
  }
}
