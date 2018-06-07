namespace BBAuto.AddEdit
{
    partial class Car_Sale
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
      this.dtpDate = new System.Windows.Forms.DateTimePicker();
      this.btnSave = new System.Windows.Forms.Button();
      this.btnCancel = new System.Windows.Forms.Button();
      this.label2 = new System.Windows.Forms.Label();
      this.tbComm = new System.Windows.Forms.TextBox();
      this.chbSale = new System.Windows.Forms.CheckBox();
      this.label1 = new System.Windows.Forms.Label();
      this.cbCustomer = new System.Windows.Forms.ComboBox();
      this.SuspendLayout();
      // 
      // dtpDate
      // 
      this.dtpDate.Location = new System.Drawing.Point(16, 116);
      this.dtpDate.Name = "dtpDate";
      this.dtpDate.Size = new System.Drawing.Size(166, 20);
      this.dtpDate.TabIndex = 1;
      // 
      // btnSave
      // 
      this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnSave.DialogResult = System.Windows.Forms.DialogResult.OK;
      this.btnSave.Location = new System.Drawing.Point(184, 165);
      this.btnSave.Name = "btnSave";
      this.btnSave.Size = new System.Drawing.Size(75, 23);
      this.btnSave.TabIndex = 2;
      this.btnSave.Text = "Сохранить";
      this.btnSave.UseVisualStyleBackColor = true;
      this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
      // 
      // btnCancel
      // 
      this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.btnCancel.Location = new System.Drawing.Point(265, 165);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new System.Drawing.Size(75, 23);
      this.btnCancel.TabIndex = 3;
      this.btnCancel.Text = "Отмена";
      this.btnCancel.UseVisualStyleBackColor = true;
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(12, 49);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(129, 13);
      this.label2.TabIndex = 4;
      this.label2.Text = "Примечание к продаже:";
      // 
      // tbComm
      // 
      this.tbComm.Location = new System.Drawing.Point(15, 65);
      this.tbComm.Name = "tbComm";
      this.tbComm.Size = new System.Drawing.Size(253, 20);
      this.tbComm.TabIndex = 5;
      // 
      // chbSale
      // 
      this.chbSale.AutoSize = true;
      this.chbSale.Location = new System.Drawing.Point(16, 93);
      this.chbSale.Name = "chbSale";
      this.chbSale.Size = new System.Drawing.Size(127, 17);
      this.chbSale.TabIndex = 6;
      this.chbSale.Text = "Автомобиль продан";
      this.chbSale.UseVisualStyleBackColor = true;
      this.chbSale.CheckedChanged += new System.EventHandler(this.chbSale_CheckedChanged);
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(12, 9);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(67, 13);
      this.label1.TabIndex = 7;
      this.label1.Text = "Покупатель";
      // 
      // cbCustomer
      // 
      this.cbCustomer.FormattingEnabled = true;
      this.cbCustomer.Location = new System.Drawing.Point(16, 25);
      this.cbCustomer.Name = "cbCustomer";
      this.cbCustomer.Size = new System.Drawing.Size(252, 21);
      this.cbCustomer.TabIndex = 8;
      // 
      // Car_Sale
      // 
      this.AcceptButton = this.btnSave;
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.CancelButton = this.btnCancel;
      this.ClientSize = new System.Drawing.Size(352, 200);
      this.Controls.Add(this.cbCustomer);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.chbSale);
      this.Controls.Add(this.tbComm);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.btnCancel);
      this.Controls.Add(this.btnSave);
      this.Controls.Add(this.dtpDate);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "Car_Sale";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "Продажа автомобиля";
      this.Load += new System.EventHandler(this.Car_Sale_Load);
      this.ResumeLayout(false);
      this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dtpDate;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbComm;
        private System.Windows.Forms.CheckBox chbSale;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.ComboBox cbCustomer;
  }
}
