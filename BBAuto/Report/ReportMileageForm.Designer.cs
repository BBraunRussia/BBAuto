namespace BBAuto.Report
{
  partial class ReportMileageForm
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
      PresentationControls.CheckBoxProperties checkBoxProperties3 = new PresentationControls.CheckBoxProperties();
      this.label1 = new System.Windows.Forms.Label();
      this.rbGrz = new System.Windows.Forms.RadioButton();
      this.rbMark = new System.Windows.Forms.RadioButton();
      this.rbDriver = new System.Windows.Forms.RadioButton();
      this.rbRegion = new System.Windows.Forms.RadioButton();
      this.chbModel = new System.Windows.Forms.CheckBox();
      this.rbAll = new System.Windows.Forms.RadioButton();
      this.tbAll = new System.Windows.Forms.TextBox();
      this.label2 = new System.Windows.Forms.Label();
      this.lbBeginDate = new System.Windows.Forms.Label();
      this.dtpBeginDate = new System.Windows.Forms.DateTimePicker();
      this.dtpEndDate = new System.Windows.Forms.DateTimePicker();
      this.lbEndDate = new System.Windows.Forms.Label();
      this.btnOk = new System.Windows.Forms.Button();
      this.btnCancel = new System.Windows.Forms.Button();
      this.cbGrz = new PresentationControls.CheckBoxComboBox();
      this.cbDriver = new PresentationControls.CheckBoxComboBox();
      this.cbRegion = new PresentationControls.CheckBoxComboBox();
      this.cbMark = new System.Windows.Forms.ComboBox();
      this.cbModel = new System.Windows.Forms.ComboBox();
      this.SuspendLayout();
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.label1.Location = new System.Drawing.Point(12, 9);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(266, 22);
      this.label1.TabIndex = 0;
      this.label1.Text = "Параметры отчёта \"Пробеги\"";
      // 
      // rbGrz
      // 
      this.rbGrz.AutoSize = true;
      this.rbGrz.Location = new System.Drawing.Point(12, 50);
      this.rbGrz.Name = "rbGrz";
      this.rbGrz.Size = new System.Drawing.Size(81, 17);
      this.rbGrz.TabIndex = 1;
      this.rbGrz.TabStop = true;
      this.rbGrz.Text = "Гос. номер";
      this.rbGrz.UseVisualStyleBackColor = true;
      this.rbGrz.CheckedChanged += new System.EventHandler(this.rb_CheckedChanged);
      // 
      // rbMark
      // 
      this.rbMark.AutoSize = true;
      this.rbMark.Location = new System.Drawing.Point(12, 79);
      this.rbMark.Name = "rbMark";
      this.rbMark.Size = new System.Drawing.Size(58, 17);
      this.rbMark.TabIndex = 2;
      this.rbMark.TabStop = true;
      this.rbMark.Text = "Марка";
      this.rbMark.UseVisualStyleBackColor = true;
      this.rbMark.CheckedChanged += new System.EventHandler(this.rb_CheckedChanged);
      // 
      // rbDriver
      // 
      this.rbDriver.AutoSize = true;
      this.rbDriver.Location = new System.Drawing.Point(12, 133);
      this.rbDriver.Name = "rbDriver";
      this.rbDriver.Size = new System.Drawing.Size(113, 17);
      this.rbDriver.TabIndex = 4;
      this.rbDriver.TabStop = true;
      this.rbDriver.Text = "ФИО сотрудника";
      this.rbDriver.UseVisualStyleBackColor = true;
      this.rbDriver.CheckedChanged += new System.EventHandler(this.rb_CheckedChanged);
      // 
      // rbRegion
      // 
      this.rbRegion.AutoSize = true;
      this.rbRegion.Location = new System.Drawing.Point(12, 160);
      this.rbRegion.Name = "rbRegion";
      this.rbRegion.Size = new System.Drawing.Size(61, 17);
      this.rbRegion.TabIndex = 5;
      this.rbRegion.TabStop = true;
      this.rbRegion.Text = "Регион";
      this.rbRegion.UseVisualStyleBackColor = true;
      this.rbRegion.CheckedChanged += new System.EventHandler(this.rb_CheckedChanged);
      // 
      // chbModel
      // 
      this.chbModel.AutoSize = true;
      this.chbModel.Location = new System.Drawing.Point(28, 107);
      this.chbModel.Name = "chbModel";
      this.chbModel.Size = new System.Drawing.Size(65, 17);
      this.chbModel.TabIndex = 3;
      this.chbModel.Text = "Модель";
      this.chbModel.UseVisualStyleBackColor = true;
      this.chbModel.CheckedChanged += new System.EventHandler(this.rb_CheckedChanged);
      // 
      // rbAll
      // 
      this.rbAll.AutoSize = true;
      this.rbAll.Location = new System.Drawing.Point(12, 187);
      this.rbAll.Name = "rbAll";
      this.rbAll.Size = new System.Drawing.Size(182, 17);
      this.rbAll.TabIndex = 6;
      this.rbAll.TabStop = true;
      this.rbAll.Text = "По всему парку, пробег более:";
      this.rbAll.UseVisualStyleBackColor = true;
      this.rbAll.CheckedChanged += new System.EventHandler(this.rb_CheckedChanged);
      // 
      // tbAll
      // 
      this.tbAll.Location = new System.Drawing.Point(203, 186);
      this.tbAll.Name = "tbAll";
      this.tbAll.Size = new System.Drawing.Size(145, 20);
      this.tbAll.TabIndex = 12;
      this.tbAll.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbAll_KeyPress);
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(351, 190);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(21, 13);
      this.label2.TabIndex = 13;
      this.label2.Text = "км";
      // 
      // lbBeginDate
      // 
      this.lbBeginDate.AutoSize = true;
      this.lbBeginDate.Location = new System.Drawing.Point(379, 52);
      this.lbBeginDate.Name = "lbBeginDate";
      this.lbBeginDate.Size = new System.Drawing.Size(89, 13);
      this.lbBeginDate.TabIndex = 14;
      this.lbBeginDate.Text = "Начало периода";
      // 
      // dtpBeginDate
      // 
      this.dtpBeginDate.CustomFormat = "MMMM yyyy";
      this.dtpBeginDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
      this.dtpBeginDate.Location = new System.Drawing.Point(474, 50);
      this.dtpBeginDate.Name = "dtpBeginDate";
      this.dtpBeginDate.Size = new System.Drawing.Size(130, 20);
      this.dtpBeginDate.TabIndex = 15;
      // 
      // dtpEndDate
      // 
      this.dtpEndDate.CustomFormat = "MMMM yyyy";
      this.dtpEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
      this.dtpEndDate.Location = new System.Drawing.Point(474, 79);
      this.dtpEndDate.Name = "dtpEndDate";
      this.dtpEndDate.Size = new System.Drawing.Size(130, 20);
      this.dtpEndDate.TabIndex = 17;
      // 
      // lbEndDate
      // 
      this.lbEndDate.AutoSize = true;
      this.lbEndDate.Location = new System.Drawing.Point(379, 81);
      this.lbEndDate.Name = "lbEndDate";
      this.lbEndDate.Size = new System.Drawing.Size(83, 13);
      this.lbEndDate.TabIndex = 16;
      this.lbEndDate.Text = "Конец периода";
      // 
      // btnOk
      // 
      this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnOk.Location = new System.Drawing.Point(448, 231);
      this.btnOk.Name = "btnOk";
      this.btnOk.Size = new System.Drawing.Size(75, 23);
      this.btnOk.TabIndex = 18;
      this.btnOk.Text = "Построить";
      this.btnOk.UseVisualStyleBackColor = true;
      this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
      // 
      // btnCancel
      // 
      this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.btnCancel.Location = new System.Drawing.Point(529, 231);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new System.Drawing.Size(75, 23);
      this.btnCancel.TabIndex = 19;
      this.btnCancel.Text = "Отменить";
      this.btnCancel.UseVisualStyleBackColor = true;
      // 
      // cbGrz
      // 
      checkBoxProperties1.ForeColor = System.Drawing.SystemColors.ControlText;
      this.cbGrz.CheckBoxProperties = checkBoxProperties1;
      this.cbGrz.DisplayMemberSingleItem = "";
      this.cbGrz.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cbGrz.FormattingEnabled = true;
      this.cbGrz.Location = new System.Drawing.Point(203, 49);
      this.cbGrz.Name = "cbGrz";
      this.cbGrz.Size = new System.Drawing.Size(145, 21);
      this.cbGrz.TabIndex = 20;
      // 
      // cbDriver
      // 
      checkBoxProperties2.ForeColor = System.Drawing.SystemColors.ControlText;
      this.cbDriver.CheckBoxProperties = checkBoxProperties2;
      this.cbDriver.DisplayMemberSingleItem = "";
      this.cbDriver.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cbDriver.FormattingEnabled = true;
      this.cbDriver.Location = new System.Drawing.Point(203, 132);
      this.cbDriver.Name = "cbDriver";
      this.cbDriver.Size = new System.Drawing.Size(145, 21);
      this.cbDriver.TabIndex = 23;
      // 
      // cbRegion
      // 
      checkBoxProperties3.ForeColor = System.Drawing.SystemColors.ControlText;
      this.cbRegion.CheckBoxProperties = checkBoxProperties3;
      this.cbRegion.DisplayMemberSingleItem = "";
      this.cbRegion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cbRegion.FormattingEnabled = true;
      this.cbRegion.Location = new System.Drawing.Point(203, 159);
      this.cbRegion.Name = "cbRegion";
      this.cbRegion.Size = new System.Drawing.Size(145, 21);
      this.cbRegion.TabIndex = 24;
      // 
      // cbMark
      // 
      this.cbMark.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cbMark.FormattingEnabled = true;
      this.cbMark.Location = new System.Drawing.Point(203, 78);
      this.cbMark.Name = "cbMark";
      this.cbMark.Size = new System.Drawing.Size(145, 21);
      this.cbMark.TabIndex = 25;
      this.cbMark.SelectedValueChanged += new System.EventHandler(this.cbMark_SelectedValueChanged);
      // 
      // cbModel
      // 
      this.cbModel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cbModel.FormattingEnabled = true;
      this.cbModel.Location = new System.Drawing.Point(203, 105);
      this.cbModel.Name = "cbModel";
      this.cbModel.Size = new System.Drawing.Size(145, 21);
      this.cbModel.TabIndex = 26;
      // 
      // ReportMileageForm
      // 
      this.AcceptButton = this.btnOk;
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.CancelButton = this.btnCancel;
      this.ClientSize = new System.Drawing.Size(616, 266);
      this.Controls.Add(this.cbModel);
      this.Controls.Add(this.cbMark);
      this.Controls.Add(this.cbRegion);
      this.Controls.Add(this.cbDriver);
      this.Controls.Add(this.cbGrz);
      this.Controls.Add(this.btnCancel);
      this.Controls.Add(this.btnOk);
      this.Controls.Add(this.dtpEndDate);
      this.Controls.Add(this.lbEndDate);
      this.Controls.Add(this.dtpBeginDate);
      this.Controls.Add(this.lbBeginDate);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.tbAll);
      this.Controls.Add(this.rbAll);
      this.Controls.Add(this.chbModel);
      this.Controls.Add(this.rbRegion);
      this.Controls.Add(this.rbDriver);
      this.Controls.Add(this.rbMark);
      this.Controls.Add(this.rbGrz);
      this.Controls.Add(this.label1);
      this.Name = "ReportMileageForm";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "Формирование отчёта \"Пробеги\"";
      this.Load += new System.EventHandler(this.ReportMileageForm_Load);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.RadioButton rbGrz;
    private System.Windows.Forms.RadioButton rbMark;
    private System.Windows.Forms.RadioButton rbDriver;
    private System.Windows.Forms.RadioButton rbRegion;
    private System.Windows.Forms.CheckBox chbModel;
    private System.Windows.Forms.RadioButton rbAll;
    private System.Windows.Forms.TextBox tbAll;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Label lbBeginDate;
    private System.Windows.Forms.DateTimePicker dtpBeginDate;
    private System.Windows.Forms.DateTimePicker dtpEndDate;
    private System.Windows.Forms.Label lbEndDate;
    private System.Windows.Forms.Button btnOk;
    private System.Windows.Forms.Button btnCancel;
    private PresentationControls.CheckBoxComboBox cbGrz;
    private PresentationControls.CheckBoxComboBox cbDriver;
    private PresentationControls.CheckBoxComboBox cbRegion;
    private System.Windows.Forms.ComboBox cbMark;
    private System.Windows.Forms.ComboBox cbModel;
  }
}
