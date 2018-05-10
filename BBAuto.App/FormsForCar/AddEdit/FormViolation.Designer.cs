using BBAuto.Logic;

namespace BBAuto.App.FormsForCar.AddEdit
{
    partial class FormViolation
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
            this.label1 = new System.Windows.Forms.Label();
            this.dtpDate = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.tbNumber = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.chbPaid = new System.Windows.Forms.CheckBox();
            this.dtpDatePaid = new System.Windows.Forms.DateTimePicker();
            this.labelDatePaid = new System.Windows.Forms.Label();
            this.labelFile = new System.Windows.Forms.Label();
            this.labelFilePay = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cbViolationType = new System.Windows.Forms.ComboBox();
            this.tbSum = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnSend = new System.Windows.Forms.Button();
            this.chbNoDeduction = new System.Windows.Forms.CheckBox();
            this.ucFilePay = new FileOpenTextBox();
            this.ucFile = new FileOpenTextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.llDriver = new System.Windows.Forms.LinkLabel();
            this.llCar = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 61);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Дата нарушения:";
            // 
            // dtpDate
            // 
            this.dtpDate.Location = new System.Drawing.Point(142, 55);
            this.dtpDate.Name = "dtpDate";
            this.dtpDate.Size = new System.Drawing.Size(200, 20);
            this.dtpDate.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 84);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(124, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Номер постановления:";
            // 
            // tbNumber
            // 
            this.tbNumber.Location = new System.Drawing.Point(142, 81);
            this.tbNumber.Name = "tbNumber";
            this.tbNumber.Size = new System.Drawing.Size(200, 20);
            this.tbNumber.TabIndex = 1;
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Location = new System.Drawing.Point(227, 290);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(95, 23);
            this.btnSave.TabIndex = 8;
            this.btnSave.Text = "Сохранить";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(328, 290);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 9;
            this.btnClose.Text = "Закрыть";
            this.btnClose.UseVisualStyleBackColor = true;
            // 
            // chbPaid
            // 
            this.chbPaid.AutoSize = true;
            this.chbPaid.Location = new System.Drawing.Point(15, 187);
            this.chbPaid.Name = "chbPaid";
            this.chbPaid.Size = new System.Drawing.Size(75, 17);
            this.chbPaid.TabIndex = 4;
            this.chbPaid.Text = "Оплачено";
            this.chbPaid.UseVisualStyleBackColor = true;
            this.chbPaid.CheckedChanged += new System.EventHandler(this.chbPaid_CheckedChanged);
            // 
            // dtpDatePaid
            // 
            this.dtpDatePaid.Location = new System.Drawing.Point(142, 201);
            this.dtpDatePaid.Name = "dtpDatePaid";
            this.dtpDatePaid.Size = new System.Drawing.Size(200, 20);
            this.dtpDatePaid.TabIndex = 5;
            this.dtpDatePaid.Visible = false;
            // 
            // labelDatePaid
            // 
            this.labelDatePaid.AutoSize = true;
            this.labelDatePaid.Location = new System.Drawing.Point(12, 207);
            this.labelDatePaid.Name = "labelDatePaid";
            this.labelDatePaid.Size = new System.Drawing.Size(76, 13);
            this.labelDatePaid.TabIndex = 8;
            this.labelDatePaid.Text = "Дата оплаты:";
            this.labelDatePaid.Visible = false;
            // 
            // labelFile
            // 
            this.labelFile.AutoSize = true;
            this.labelFile.Location = new System.Drawing.Point(12, 110);
            this.labelFile.Name = "labelFile";
            this.labelFile.Size = new System.Drawing.Size(115, 13);
            this.labelFile.TabIndex = 9;
            this.labelFile.Text = "Скан постановления:";
            // 
            // labelFilePay
            // 
            this.labelFilePay.AutoSize = true;
            this.labelFilePay.Location = new System.Drawing.Point(12, 230);
            this.labelFilePay.Name = "labelFilePay";
            this.labelFilePay.Size = new System.Drawing.Size(92, 13);
            this.labelFilePay.TabIndex = 12;
            this.labelFilePay.Text = "Скан документа:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 136);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(87, 13);
            this.label3.TabIndex = 13;
            this.label3.Text = "Тип нарушения:";
            // 
            // cbViolationType
            // 
            this.cbViolationType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbViolationType.FormattingEnabled = true;
            this.cbViolationType.Location = new System.Drawing.Point(142, 133);
            this.cbViolationType.Name = "cbViolationType";
            this.cbViolationType.Size = new System.Drawing.Size(200, 21);
            this.cbViolationType.TabIndex = 14;
            // 
            // tbSum
            // 
            this.tbSum.Location = new System.Drawing.Point(142, 160);
            this.tbSum.Name = "tbSum";
            this.tbSum.Size = new System.Drawing.Size(200, 20);
            this.tbSum.TabIndex = 15;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 163);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(86, 13);
            this.label4.TabIndex = 16;
            this.label4.Text = "Сумма штрафа:";
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(11, 267);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(147, 23);
            this.btnSend.TabIndex = 17;
            this.btnSend.Text = "Отправить уведомление";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // chbNoDeduction
            // 
            this.chbNoDeduction.AutoSize = true;
            this.chbNoDeduction.Location = new System.Drawing.Point(11, 296);
            this.chbNoDeduction.Name = "chbNoDeduction";
            this.chbNoDeduction.Size = new System.Drawing.Size(103, 17);
            this.chbNoDeduction.TabIndex = 20;
            this.chbNoDeduction.Text = "Без удержания";
            this.chbNoDeduction.UseVisualStyleBackColor = true;
            // 
            // ucFilePay
            // 
            this.ucFilePay.Location = new System.Drawing.Point(142, 227);
            this.ucFilePay.Name = "ucFilePay";
            this.ucFilePay.Size = new System.Drawing.Size(241, 23);
            this.ucFilePay.TabIndex = 19;
            // 
            // ucFile
            // 
            this.ucFile.Location = new System.Drawing.Point(142, 107);
            this.ucFile.Name = "ucFile";
            this.ucFile.Size = new System.Drawing.Size(376, 23);
            this.ucFile.TabIndex = 18;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 9);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(58, 13);
            this.label5.TabIndex = 21;
            this.label5.Text = "Водитель:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 28);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(72, 13);
            this.label6.TabIndex = 22;
            this.label6.Text = "Автомобиль:";
            // 
            // llDriver
            // 
            this.llDriver.AutoSize = true;
            this.llDriver.Location = new System.Drawing.Point(142, 8);
            this.llDriver.Name = "llDriver";
            this.llDriver.Size = new System.Drawing.Size(55, 13);
            this.llDriver.TabIndex = 23;
            this.llDriver.TabStop = true;
            this.llDriver.Text = "linkLabel1";
            this.llDriver.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llDriver_LinkClicked);
            // 
            // llCar
            // 
            this.llCar.AutoSize = true;
            this.llCar.Location = new System.Drawing.Point(142, 28);
            this.llCar.Name = "llCar";
            this.llCar.Size = new System.Drawing.Size(55, 13);
            this.llCar.TabIndex = 24;
            this.llCar.TabStop = true;
            this.llCar.Text = "linkLabel2";
            this.llCar.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llCar_LinkClicked);
            // 
            // Violation_AddEdit
            // 
            this.AcceptButton = this.btnSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(415, 325);
            this.Controls.Add(this.llCar);
            this.Controls.Add(this.llDriver);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.chbNoDeduction);
            this.Controls.Add(this.ucFilePay);
            this.Controls.Add(this.ucFile);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.tbSum);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cbViolationType);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.labelFilePay);
            this.Controls.Add(this.labelFile);
            this.Controls.Add(this.labelDatePaid);
            this.Controls.Add(this.dtpDatePaid);
            this.Controls.Add(this.chbPaid);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.tbNumber);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dtpDate);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Violation_AddEdit";
            this.StartPosition
                = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Нарушение ПДД";
            this.Load += new System.EventHandler(this.Violation_AddEdit_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpDate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbNumber;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.CheckBox chbPaid;
        private System.Windows.Forms.DateTimePicker dtpDatePaid;
        private System.Windows.Forms.Label labelDatePaid;
        private System.Windows.Forms.Label labelFile;
        private System.Windows.Forms.Label labelFilePay;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbViolationType;
        private System.Windows.Forms.TextBox tbSum;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnSend;
        private FileOpenTextBox ucFile;
        private FileOpenTextBox ucFilePay;
        private System.Windows.Forms.CheckBox chbNoDeduction;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.LinkLabel llDriver;
        private System.Windows.Forms.LinkLabel llCar;
    }
}
