namespace BBAuto.FormsForCar.AddEdit
{
    partial class Invoice_AddEdit
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
      this.cbDriverFrom = new System.Windows.Forms.ComboBox();
      this.label3 = new System.Windows.Forms.Label();
      this.cbDriverTo = new System.Windows.Forms.ComboBox();
      this.label4 = new System.Windows.Forms.Label();
      this.label5 = new System.Windows.Forms.Label();
      this.btnSave = new System.Windows.Forms.Button();
      this.btnClose = new System.Windows.Forms.Button();
      this.dtpDate = new System.Windows.Forms.DateTimePicker();
      this.label2 = new System.Windows.Forms.Label();
      this.mtbDateMove = new System.Windows.Forms.MaskedTextBox();
      this.cbRegionTo = new System.Windows.Forms.ComboBox();
      this.label6 = new System.Windows.Forms.Label();
      this.cbRegionFrom = new System.Windows.Forms.ComboBox();
      this.label7 = new System.Windows.Forms.Label();
      this.label8 = new System.Windows.Forms.Label();
      this.lbMoveCar = new System.Windows.Forms.Label();
      this.ucFile = new BBAuto.Domain.FileOpenTextBox();
      this.lbNumber = new System.Windows.Forms.Label();
      this.chbMain = new System.Windows.Forms.CheckBox();
      this.SuspendLayout();
      // 
      // cbDriverFrom
      // 
      this.cbDriverFrom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cbDriverFrom.Enabled = false;
      this.cbDriverFrom.FormattingEnabled = true;
      this.cbDriverFrom.Location = new System.Drawing.Point(15, 77);
      this.cbDriverFrom.Name = "cbDriverFrom";
      this.cbDriverFrom.Size = new System.Drawing.Size(177, 21);
      this.cbDriverFrom.TabIndex = 8;
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(12, 61);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(35, 13);
      this.label3.TabIndex = 4;
      this.label3.Text = "Сдал:";
      // 
      // cbDriverTo
      // 
      this.cbDriverTo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cbDriverTo.FormattingEnabled = true;
      this.cbDriverTo.Location = new System.Drawing.Point(206, 77);
      this.cbDriverTo.Name = "cbDriverTo";
      this.cbDriverTo.Size = new System.Drawing.Size(177, 21);
      this.cbDriverTo.TabIndex = 1;
      this.cbDriverTo.SelectedIndexChanged += new System.EventHandler(this.cbDriverTo_SelectedIndexChanged);
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Location = new System.Drawing.Point(203, 61);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(48, 13);
      this.label4.TabIndex = 6;
      this.label4.Text = "Принял:";
      // 
      // label5
      // 
      this.label5.AutoSize = true;
      this.label5.Location = new System.Drawing.Point(12, 141);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(93, 13);
      this.label5.TabIndex = 8;
      this.label5.Text = "Дата накладной:";
      // 
      // btnSave
      // 
      this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnSave.Location = new System.Drawing.Point(355, 226);
      this.btnSave.Name = "btnSave";
      this.btnSave.Size = new System.Drawing.Size(95, 23);
      this.btnSave.TabIndex = 4;
      this.btnSave.Text = "Сохранить";
      this.btnSave.UseVisualStyleBackColor = true;
      this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
      // 
      // btnClose
      // 
      this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.btnClose.Location = new System.Drawing.Point(456, 226);
      this.btnClose.Name = "btnClose";
      this.btnClose.Size = new System.Drawing.Size(75, 23);
      this.btnClose.TabIndex = 5;
      this.btnClose.Text = "Закрыть";
      this.btnClose.UseVisualStyleBackColor = true;
      // 
      // dtpDate
      // 
      this.dtpDate.Location = new System.Drawing.Point(15, 157);
      this.dtpDate.Name = "dtpDate";
      this.dtpDate.Size = new System.Drawing.Size(176, 20);
      this.dtpDate.TabIndex = 2;
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(203, 141);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(86, 13);
      this.label2.TabIndex = 43;
      this.label2.Text = "Дата передачи:";
      // 
      // mtbDateMove
      // 
      this.mtbDateMove.Location = new System.Drawing.Point(206, 157);
      this.mtbDateMove.Mask = "00/00/0000";
      this.mtbDateMove.Name = "mtbDateMove";
      this.mtbDateMove.Size = new System.Drawing.Size(176, 20);
      this.mtbDateMove.TabIndex = 3;
      this.mtbDateMove.ValidatingType = typeof(System.DateTime);
      // 
      // cbRegionTo
      // 
      this.cbRegionTo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cbRegionTo.FormattingEnabled = true;
      this.cbRegionTo.Location = new System.Drawing.Point(206, 117);
      this.cbRegionTo.Name = "cbRegionTo";
      this.cbRegionTo.Size = new System.Drawing.Size(177, 21);
      this.cbRegionTo.TabIndex = 0;
      this.cbRegionTo.SelectedIndexChanged += new System.EventHandler(this.cbRegionTo_SelectedIndexChanged);
      // 
      // label6
      // 
      this.label6.AutoSize = true;
      this.label6.Location = new System.Drawing.Point(203, 101);
      this.label6.Name = "label6";
      this.label6.Size = new System.Drawing.Size(127, 13);
      this.label6.TabIndex = 48;
      this.label6.Text = "Регион принимающего:";
      // 
      // cbRegionFrom
      // 
      this.cbRegionFrom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cbRegionFrom.Enabled = false;
      this.cbRegionFrom.FormattingEnabled = true;
      this.cbRegionFrom.Location = new System.Drawing.Point(15, 117);
      this.cbRegionFrom.Name = "cbRegionFrom";
      this.cbRegionFrom.Size = new System.Drawing.Size(177, 21);
      this.cbRegionFrom.TabIndex = 7;
      // 
      // label7
      // 
      this.label7.AutoSize = true;
      this.label7.Location = new System.Drawing.Point(12, 101);
      this.label7.Name = "label7";
      this.label7.Size = new System.Drawing.Size(101, 13);
      this.label7.TabIndex = 46;
      this.label7.Text = "Регион сдающего:";
      // 
      // label8
      // 
      this.label8.AutoSize = true;
      this.label8.Location = new System.Drawing.Point(12, 193);
      this.label8.Name = "label8";
      this.label8.Size = new System.Drawing.Size(92, 13);
      this.label8.TabIndex = 50;
      this.label8.Text = "Скан документа:";
      // 
      // lbMoveCar
      // 
      this.lbMoveCar.AutoSize = true;
      this.lbMoveCar.Location = new System.Drawing.Point(12, 39);
      this.lbMoveCar.Name = "lbMoveCar";
      this.lbMoveCar.Size = new System.Drawing.Size(144, 13);
      this.lbMoveCar.TabIndex = 51;
      this.lbMoveCar.Text = "Перемещение автомобиля";
      // 
      // ucFile
      // 
      this.ucFile.Location = new System.Drawing.Point(110, 191);
      this.ucFile.Name = "ucFile";
      this.ucFile.Size = new System.Drawing.Size(236, 23);
      this.ucFile.TabIndex = 49;
      // 
      // lbNumber
      // 
      this.lbNumber.AutoSize = true;
      this.lbNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.lbNumber.Location = new System.Drawing.Point(119, 9);
      this.lbNumber.Name = "lbNumber";
      this.lbNumber.Size = new System.Drawing.Size(95, 20);
      this.lbNumber.TabIndex = 52;
      this.lbNumber.Text = "Накладная";
      // 
      // chbMain
      // 
      this.chbMain.AutoSize = true;
      this.chbMain.Location = new System.Drawing.Point(390, 80);
      this.chbMain.Name = "chbMain";
      this.chbMain.Size = new System.Drawing.Size(150, 17);
      this.chbMain.TabIndex = 53;
      this.chbMain.Text = "Основной пользователь";
      this.chbMain.UseVisualStyleBackColor = true;
      // 
      // Invoice_AddEdit
      // 
      this.AcceptButton = this.btnSave;
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.CancelButton = this.btnClose;
      this.ClientSize = new System.Drawing.Size(543, 261);
      this.Controls.Add(this.chbMain);
      this.Controls.Add(this.lbNumber);
      this.Controls.Add(this.lbMoveCar);
      this.Controls.Add(this.label8);
      this.Controls.Add(this.ucFile);
      this.Controls.Add(this.cbRegionTo);
      this.Controls.Add(this.label6);
      this.Controls.Add(this.cbRegionFrom);
      this.Controls.Add(this.label7);
      this.Controls.Add(this.mtbDateMove);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.dtpDate);
      this.Controls.Add(this.btnClose);
      this.Controls.Add(this.btnSave);
      this.Controls.Add(this.label5);
      this.Controls.Add(this.cbDriverTo);
      this.Controls.Add(this.label4);
      this.Controls.Add(this.cbDriverFrom);
      this.Controls.Add(this.label3);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "Invoice_AddEdit";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "--";
      this.Load += new System.EventHandler(this.Invoice_AddEdit_Load);
      this.ResumeLayout(false);
      this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbDriverFrom;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbDriverTo;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.DateTimePicker dtpDate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.MaskedTextBox mtbDateMove;
        private System.Windows.Forms.ComboBox cbRegionTo;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cbRegionFrom;
        private System.Windows.Forms.Label label7;
        private BBAuto.Domain.FileOpenTextBox ucFile;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lbMoveCar;
        private System.Windows.Forms.Label lbNumber;
    private System.Windows.Forms.CheckBox chbMain;
  }
}
