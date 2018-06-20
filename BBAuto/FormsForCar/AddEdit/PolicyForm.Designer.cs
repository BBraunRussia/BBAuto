namespace BBAuto.FormsForCar.AddEdit
{
    partial class PolicyForm
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
      this.cbPolicyType = new System.Windows.Forms.ComboBox();
      this.label2 = new System.Windows.Forms.Label();
      this.cbOwner = new System.Windows.Forms.ComboBox();
      this.cbComp = new System.Windows.Forms.ComboBox();
      this.label3 = new System.Windows.Forms.Label();
      this.label4 = new System.Windows.Forms.Label();
      this.tbNumber = new System.Windows.Forms.TextBox();
      this.label5 = new System.Windows.Forms.Label();
      this.dtpDateBegin = new System.Windows.Forms.DateTimePicker();
      this.dtpDateEnd = new System.Windows.Forms.DateTimePicker();
      this.label6 = new System.Windows.Forms.Label();
      this.lbPay = new System.Windows.Forms.Label();
      this.tbPay = new System.Windows.Forms.TextBox();
      this.tbPay2 = new System.Windows.Forms.TextBox();
      this.lbPay2 = new System.Windows.Forms.Label();
      this.dtpDatePay2 = new System.Windows.Forms.DateTimePicker();
      this.lbPay2Date = new System.Windows.Forms.Label();
      this.tbLimitCost = new System.Windows.Forms.TextBox();
      this.lbLimitCost = new System.Windows.Forms.Label();
      this.btnSave = new System.Windows.Forms.Button();
      this.btnClose = new System.Windows.Forms.Button();
      this.label7 = new System.Windows.Forms.Label();
      this.label8 = new System.Windows.Forms.Label();
      this.tbComment = new System.Windows.Forms.TextBox();
      this.ucFile = new BBAuto.Domain.FileOpenTextBox();
      this.SuspendLayout();
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(13, 13);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(68, 13);
      this.label1.TabIndex = 13;
      this.label1.Text = "Тип полиса:";
      // 
      // cbPolicyType
      // 
      this.cbPolicyType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cbPolicyType.FormattingEnabled = true;
      this.cbPolicyType.Location = new System.Drawing.Point(134, 10);
      this.cbPolicyType.Name = "cbPolicyType";
      this.cbPolicyType.Size = new System.Drawing.Size(132, 21);
      this.cbPolicyType.TabIndex = 0;
      this.cbPolicyType.SelectedIndexChanged += new System.EventHandler(this.cbPolicyType_SelectedIndexChanged);
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(13, 40);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(80, 13);
      this.label2.TabIndex = 14;
      this.label2.Text = "Страхователь:";
      // 
      // cbOwner
      // 
      this.cbOwner.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cbOwner.FormattingEnabled = true;
      this.cbOwner.Location = new System.Drawing.Point(134, 37);
      this.cbOwner.Name = "cbOwner";
      this.cbOwner.Size = new System.Drawing.Size(132, 21);
      this.cbOwner.TabIndex = 2;
      // 
      // cbComp
      // 
      this.cbComp.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cbComp.FormattingEnabled = true;
      this.cbComp.Location = new System.Drawing.Point(422, 37);
      this.cbComp.Name = "cbComp";
      this.cbComp.Size = new System.Drawing.Size(132, 21);
      this.cbComp.TabIndex = 3;
      this.cbComp.SelectedIndexChanged += new System.EventHandler(this.cbComp_SelectedIndexChanged);
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(280, 40);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(72, 13);
      this.label3.TabIndex = 15;
      this.label3.Text = "Страховщик:";
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Location = new System.Drawing.Point(280, 14);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(44, 13);
      this.label4.TabIndex = 16;
      this.label4.Text = "Номер:";
      // 
      // tbNumber
      // 
      this.tbNumber.Location = new System.Drawing.Point(422, 11);
      this.tbNumber.Name = "tbNumber";
      this.tbNumber.Size = new System.Drawing.Size(132, 20);
      this.tbNumber.TabIndex = 1;
      // 
      // label5
      // 
      this.label5.AutoSize = true;
      this.label5.Location = new System.Drawing.Point(13, 67);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(97, 13);
      this.label5.TabIndex = 17;
      this.label5.Text = "Начало действия:";
      // 
      // dtpDateBegin
      // 
      this.dtpDateBegin.Location = new System.Drawing.Point(134, 64);
      this.dtpDateBegin.Name = "dtpDateBegin";
      this.dtpDateBegin.Size = new System.Drawing.Size(132, 20);
      this.dtpDateBegin.TabIndex = 4;
      this.dtpDateBegin.ValueChanged += new System.EventHandler(this.dtpDateBegin_ValueChanged);
      // 
      // dtpDateEnd
      // 
      this.dtpDateEnd.Location = new System.Drawing.Point(134, 90);
      this.dtpDateEnd.Name = "dtpDateEnd";
      this.dtpDateEnd.Size = new System.Drawing.Size(132, 20);
      this.dtpDateEnd.TabIndex = 5;
      // 
      // label6
      // 
      this.label6.AutoSize = true;
      this.label6.Location = new System.Drawing.Point(13, 93);
      this.label6.Name = "label6";
      this.label6.Size = new System.Drawing.Size(115, 13);
      this.label6.TabIndex = 18;
      this.label6.Text = "Окончание действия:";
      // 
      // lbPay
      // 
      this.lbPay.AutoSize = true;
      this.lbPay.Location = new System.Drawing.Point(13, 119);
      this.lbPay.Name = "lbPay";
      this.lbPay.Size = new System.Drawing.Size(72, 13);
      this.lbPay.TabIndex = 19;
      this.lbPay.Text = "Платёж, руб:";
      // 
      // tbPay
      // 
      this.tbPay.Location = new System.Drawing.Point(134, 116);
      this.tbPay.Name = "tbPay";
      this.tbPay.Size = new System.Drawing.Size(132, 20);
      this.tbPay.TabIndex = 6;
      this.tbPay.Enter += new System.EventHandler(this.tbPay_Enter);
      this.tbPay.Leave += new System.EventHandler(this.tbPay_Leave);
      // 
      // tbPay2
      // 
      this.tbPay2.Location = new System.Drawing.Point(422, 90);
      this.tbPay2.Name = "tbPay2";
      this.tbPay2.Size = new System.Drawing.Size(132, 20);
      this.tbPay2.TabIndex = 8;
      this.tbPay2.Enter += new System.EventHandler(this.tbPay2_Enter);
      this.tbPay2.Leave += new System.EventHandler(this.tbPay2_Leave);
      // 
      // lbPay2
      // 
      this.lbPay2.AutoSize = true;
      this.lbPay2.Location = new System.Drawing.Point(280, 93);
      this.lbPay2.Name = "lbPay2";
      this.lbPay2.Size = new System.Drawing.Size(92, 13);
      this.lbPay2.TabIndex = 21;
      this.lbPay2.Text = "Платёж №2, руб:";
      // 
      // dtpDatePay2
      // 
      this.dtpDatePay2.Location = new System.Drawing.Point(422, 116);
      this.dtpDatePay2.Name = "dtpDatePay2";
      this.dtpDatePay2.Size = new System.Drawing.Size(132, 20);
      this.dtpDatePay2.TabIndex = 9;
      // 
      // lbPay2Date
      // 
      this.lbPay2Date.AutoSize = true;
      this.lbPay2Date.Location = new System.Drawing.Point(280, 119);
      this.lbPay2Date.Name = "lbPay2Date";
      this.lbPay2Date.Size = new System.Drawing.Size(102, 13);
      this.lbPay2Date.TabIndex = 22;
      this.lbPay2Date.Text = "Дата платежа №2:";
      // 
      // tbLimitCost
      // 
      this.tbLimitCost.Location = new System.Drawing.Point(422, 64);
      this.tbLimitCost.Name = "tbLimitCost";
      this.tbLimitCost.Size = new System.Drawing.Size(132, 20);
      this.tbLimitCost.TabIndex = 7;
      this.tbLimitCost.Enter += new System.EventHandler(this.tbLimitCost_Enter);
      this.tbLimitCost.Leave += new System.EventHandler(this.tbLimitCost_Leave);
      // 
      // lbLimitCost
      // 
      this.lbLimitCost.AutoSize = true;
      this.lbLimitCost.Location = new System.Drawing.Point(280, 67);
      this.lbLimitCost.Name = "lbLimitCost";
      this.lbLimitCost.Size = new System.Drawing.Size(43, 13);
      this.lbLimitCost.TabIndex = 20;
      this.lbLimitCost.Text = "Лимит:";
      // 
      // btnSave
      // 
      this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnSave.Location = new System.Drawing.Point(386, 208);
      this.btnSave.Name = "btnSave";
      this.btnSave.Size = new System.Drawing.Size(95, 23);
      this.btnSave.TabIndex = 12;
      this.btnSave.Text = "Сохранить";
      this.btnSave.UseVisualStyleBackColor = true;
      this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
      // 
      // btnClose
      // 
      this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.btnClose.Location = new System.Drawing.Point(487, 208);
      this.btnClose.Name = "btnClose";
      this.btnClose.Size = new System.Drawing.Size(75, 23);
      this.btnClose.TabIndex = 13;
      this.btnClose.Text = "Закрыть";
      this.btnClose.UseVisualStyleBackColor = true;
      // 
      // label7
      // 
      this.label7.AutoSize = true;
      this.label7.Location = new System.Drawing.Point(13, 145);
      this.label7.Name = "label7";
      this.label7.Size = new System.Drawing.Size(70, 13);
      this.label7.TabIndex = 24;
      this.label7.Text = "Скан файла:";
      // 
      // label8
      // 
      this.label8.AutoSize = true;
      this.label8.Location = new System.Drawing.Point(13, 177);
      this.label8.Name = "label8";
      this.label8.Size = new System.Drawing.Size(73, 13);
      this.label8.TabIndex = 26;
      this.label8.Text = "Примечание:";
      // 
      // tbComment
      // 
      this.tbComment.Location = new System.Drawing.Point(134, 174);
      this.tbComment.MaxLength = 100;
      this.tbComment.Name = "tbComment";
      this.tbComment.Size = new System.Drawing.Size(399, 20);
      this.tbComment.TabIndex = 27;
      // 
      // ucFile
      // 
      this.ucFile.Location = new System.Drawing.Point(134, 144);
      this.ucFile.Name = "ucFile";
      this.ucFile.Size = new System.Drawing.Size(238, 23);
      this.ucFile.TabIndex = 25;
      // 
      // PolicyForm
      // 
      this.AcceptButton = this.btnSave;
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.CancelButton = this.btnClose;
      this.ClientSize = new System.Drawing.Size(574, 243);
      this.Controls.Add(this.tbComment);
      this.Controls.Add(this.label8);
      this.Controls.Add(this.ucFile);
      this.Controls.Add(this.label7);
      this.Controls.Add(this.btnClose);
      this.Controls.Add(this.btnSave);
      this.Controls.Add(this.tbLimitCost);
      this.Controls.Add(this.lbLimitCost);
      this.Controls.Add(this.dtpDatePay2);
      this.Controls.Add(this.lbPay2Date);
      this.Controls.Add(this.tbPay2);
      this.Controls.Add(this.lbPay2);
      this.Controls.Add(this.tbPay);
      this.Controls.Add(this.lbPay);
      this.Controls.Add(this.dtpDateEnd);
      this.Controls.Add(this.label6);
      this.Controls.Add(this.dtpDateBegin);
      this.Controls.Add(this.label5);
      this.Controls.Add(this.tbNumber);
      this.Controls.Add(this.label4);
      this.Controls.Add(this.cbComp);
      this.Controls.Add(this.label3);
      this.Controls.Add(this.cbOwner);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.cbPolicyType);
      this.Controls.Add(this.label1);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "PolicyForm";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "Полис";
      this.Load += new System.EventHandler(this.Policy_AddEdit_Load);
      this.ResumeLayout(false);
      this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbPolicyType;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbOwner;
        private System.Windows.Forms.ComboBox cbComp;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbNumber;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker dtpDateBegin;
        private System.Windows.Forms.DateTimePicker dtpDateEnd;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lbPay;
        private System.Windows.Forms.TextBox tbPay;
        private System.Windows.Forms.TextBox tbPay2;
        private System.Windows.Forms.Label lbPay2;
        private System.Windows.Forms.DateTimePicker dtpDatePay2;
        private System.Windows.Forms.Label lbPay2Date;
        private System.Windows.Forms.TextBox tbLimitCost;
        private System.Windows.Forms.Label lbLimitCost;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label label7;
        private BBAuto.Domain.FileOpenTextBox ucFile;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox tbComment;
    }
}
