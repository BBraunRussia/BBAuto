namespace BBAuto.Print
{
  partial class ProxyOnStoForm
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
      PresentationControls.CheckBoxProperties checkBoxProperties1 = new PresentationControls.CheckBoxProperties();
      PresentationControls.CheckBoxProperties checkBoxProperties2 = new PresentationControls.CheckBoxProperties();
      this.cbRegion = new PresentationControls.CheckBoxComboBox();
      this.cbDriver = new PresentationControls.CheckBoxComboBox();
      this.btnCancel = new System.Windows.Forms.Button();
      this.btnOk = new System.Windows.Forms.Button();
      this.dtpEndDate = new System.Windows.Forms.DateTimePicker();
      this.lbEndDate = new System.Windows.Forms.Label();
      this.dtpBeginDate = new System.Windows.Forms.DateTimePicker();
      this.lbBeginDate = new System.Windows.Forms.Label();
      this.rbRegion = new System.Windows.Forms.RadioButton();
      this.rbDriver = new System.Windows.Forms.RadioButton();
      this.SuspendLayout();
      // 
      // cbRegion
      // 
      checkBoxProperties1.ForeColor = System.Drawing.SystemColors.ControlText;
      this.cbRegion.CheckBoxProperties = checkBoxProperties1;
      this.cbRegion.DisplayMemberSingleItem = "";
      this.cbRegion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cbRegion.FormattingEnabled = true;
      this.cbRegion.Location = new System.Drawing.Point(148, 39);
      this.cbRegion.Name = "cbRegion";
      this.cbRegion.Size = new System.Drawing.Size(200, 21);
      this.cbRegion.TabIndex = 44;
      // 
      // cbDriver
      // 
      checkBoxProperties2.ForeColor = System.Drawing.SystemColors.ControlText;
      this.cbDriver.CheckBoxProperties = checkBoxProperties2;
      this.cbDriver.DisplayMemberSingleItem = "";
      this.cbDriver.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cbDriver.FormattingEnabled = true;
      this.cbDriver.Location = new System.Drawing.Point(148, 12);
      this.cbDriver.Name = "cbDriver";
      this.cbDriver.Size = new System.Drawing.Size(200, 21);
      this.cbDriver.TabIndex = 43;
      // 
      // btnCancel
      // 
      this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.btnCancel.Location = new System.Drawing.Point(279, 142);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new System.Drawing.Size(75, 23);
      this.btnCancel.TabIndex = 41;
      this.btnCancel.Text = "Отменить";
      this.btnCancel.UseVisualStyleBackColor = true;
      // 
      // btnOk
      // 
      this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnOk.Location = new System.Drawing.Point(192, 142);
      this.btnOk.Name = "btnOk";
      this.btnOk.Size = new System.Drawing.Size(81, 23);
      this.btnOk.TabIndex = 40;
      this.btnOk.Text = "Печать";
      this.btnOk.UseVisualStyleBackColor = true;
      this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
      // 
      // dtpEndDate
      // 
      this.dtpEndDate.Location = new System.Drawing.Point(148, 95);
      this.dtpEndDate.Name = "dtpEndDate";
      this.dtpEndDate.Size = new System.Drawing.Size(200, 20);
      this.dtpEndDate.TabIndex = 39;
      // 
      // lbEndDate
      // 
      this.lbEndDate.AutoSize = true;
      this.lbEndDate.Location = new System.Drawing.Point(9, 101);
      this.lbEndDate.Name = "lbEndDate";
      this.lbEndDate.Size = new System.Drawing.Size(112, 13);
      this.lbEndDate.TabIndex = 38;
      this.lbEndDate.Text = "Окончание действия";
      // 
      // dtpBeginDate
      // 
      this.dtpBeginDate.Location = new System.Drawing.Point(148, 66);
      this.dtpBeginDate.Name = "dtpBeginDate";
      this.dtpBeginDate.Size = new System.Drawing.Size(200, 20);
      this.dtpBeginDate.TabIndex = 37;
      // 
      // lbBeginDate
      // 
      this.lbBeginDate.AutoSize = true;
      this.lbBeginDate.Location = new System.Drawing.Point(9, 72);
      this.lbBeginDate.Name = "lbBeginDate";
      this.lbBeginDate.Size = new System.Drawing.Size(94, 13);
      this.lbBeginDate.TabIndex = 36;
      this.lbBeginDate.Text = "Начало действия";
      // 
      // rbRegion
      // 
      this.rbRegion.AutoSize = true;
      this.rbRegion.Location = new System.Drawing.Point(12, 40);
      this.rbRegion.Name = "rbRegion";
      this.rbRegion.Size = new System.Drawing.Size(61, 17);
      this.rbRegion.TabIndex = 32;
      this.rbRegion.TabStop = true;
      this.rbRegion.Text = "Регион";
      this.rbRegion.UseVisualStyleBackColor = true;
      // 
      // rbDriver
      // 
      this.rbDriver.AutoSize = true;
      this.rbDriver.Location = new System.Drawing.Point(12, 13);
      this.rbDriver.Name = "rbDriver";
      this.rbDriver.Size = new System.Drawing.Size(113, 17);
      this.rbDriver.TabIndex = 31;
      this.rbDriver.TabStop = true;
      this.rbDriver.Text = "ФИО сотрудника";
      this.rbDriver.UseVisualStyleBackColor = true;
      // 
      // ProxyOnStoForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(366, 177);
      this.Controls.Add(this.cbRegion);
      this.Controls.Add(this.cbDriver);
      this.Controls.Add(this.btnCancel);
      this.Controls.Add(this.btnOk);
      this.Controls.Add(this.dtpEndDate);
      this.Controls.Add(this.lbEndDate);
      this.Controls.Add(this.dtpBeginDate);
      this.Controls.Add(this.lbBeginDate);
      this.Controls.Add(this.rbRegion);
      this.Controls.Add(this.rbDriver);
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "ProxyOnStoForm";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "Формирование доверенностей";
      this.Load += new System.EventHandler(this.ProxyOnStoForm_Load);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private PresentationControls.CheckBoxComboBox cbRegion;
    private PresentationControls.CheckBoxComboBox cbDriver;
    private System.Windows.Forms.Button btnCancel;
    private System.Windows.Forms.Button btnOk;
    private System.Windows.Forms.DateTimePicker dtpEndDate;
    private System.Windows.Forms.Label lbEndDate;
    private System.Windows.Forms.DateTimePicker dtpBeginDate;
    private System.Windows.Forms.Label lbBeginDate;
    private System.Windows.Forms.RadioButton rbRegion;
    private System.Windows.Forms.RadioButton rbDriver;
  }
}
