namespace BBAuto.AddEdit
{
  partial class CompForm
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
      this.tbName = new System.Windows.Forms.TextBox();
      this.label1 = new System.Windows.Forms.Label();
      this.groupBox1 = new System.Windows.Forms.GroupBox();
      this.rb1 = new System.Windows.Forms.RadioButton();
      this.rb2 = new System.Windows.Forms.RadioButton();
      this.groupBox1.SuspendLayout();
      this.SuspendLayout();
      // 
      // btnClose
      // 
      this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.btnClose.Location = new System.Drawing.Point(186, 129);
      this.btnClose.Name = "btnClose";
      this.btnClose.Size = new System.Drawing.Size(75, 23);
      this.btnClose.TabIndex = 12;
      this.btnClose.Text = "Закрыть";
      this.btnClose.UseVisualStyleBackColor = true;
      // 
      // btnSave
      // 
      this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnSave.Location = new System.Drawing.Point(85, 129);
      this.btnSave.Name = "btnSave";
      this.btnSave.Size = new System.Drawing.Size(95, 23);
      this.btnSave.TabIndex = 11;
      this.btnSave.Text = "Сохранить";
      this.btnSave.UseVisualStyleBackColor = true;
      this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
      // 
      // tbName
      // 
      this.tbName.Location = new System.Drawing.Point(15, 25);
      this.tbName.MaxLength = 50;
      this.tbName.Name = "tbName";
      this.tbName.Size = new System.Drawing.Size(128, 20);
      this.tbName.TabIndex = 28;
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(12, 9);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(60, 13);
      this.label1.TabIndex = 29;
      this.label1.Text = "Название:";
      // 
      // groupBox1
      // 
      this.groupBox1.Controls.Add(this.rb2);
      this.groupBox1.Controls.Add(this.rb1);
      this.groupBox1.Location = new System.Drawing.Point(12, 51);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new System.Drawing.Size(176, 55);
      this.groupBox1.TabIndex = 30;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "Количество взносов КАСКО";
      // 
      // rb1
      // 
      this.rb1.AutoSize = true;
      this.rb1.Checked = true;
      this.rb1.Location = new System.Drawing.Point(7, 20);
      this.rb1.Name = "rb1";
      this.rb1.Size = new System.Drawing.Size(31, 17);
      this.rb1.TabIndex = 0;
      this.rb1.TabStop = true;
      this.rb1.Text = "1";
      this.rb1.UseVisualStyleBackColor = true;
      // 
      // rb2
      // 
      this.rb2.AutoSize = true;
      this.rb2.Location = new System.Drawing.Point(100, 20);
      this.rb2.Name = "rb2";
      this.rb2.Size = new System.Drawing.Size(31, 17);
      this.rb2.TabIndex = 1;
      this.rb2.Text = "2";
      this.rb2.UseVisualStyleBackColor = true;
      // 
      // CompForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(273, 164);
      this.Controls.Add(this.groupBox1);
      this.Controls.Add(this.tbName);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.btnClose);
      this.Controls.Add(this.btnSave);
      this.Name = "CompForm";
      this.Text = "Карточка \"Страховая компания\"";
      this.Load += new System.EventHandler(this.CompForm_Load);
      this.groupBox1.ResumeLayout(false);
      this.groupBox1.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button btnClose;
    private System.Windows.Forms.Button btnSave;
    private System.Windows.Forms.TextBox tbName;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.RadioButton rb2;
    private System.Windows.Forms.RadioButton rb1;
  }
}
