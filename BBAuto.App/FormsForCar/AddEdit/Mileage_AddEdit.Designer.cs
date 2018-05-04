namespace BBAuto.App.FormsForCar.AddEdit
{
    partial class Mileage_AddEdit
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
      this.dtpDate = new System.Windows.Forms.DateTimePicker();
      this.label2 = new System.Windows.Forms.Label();
      this.tbCount = new System.Windows.Forms.TextBox();
      this.label1 = new System.Windows.Forms.Label();
      this.label3 = new System.Windows.Forms.Label();
      this.lbPrevMileage = new System.Windows.Forms.Label();
      this.groupBox1 = new System.Windows.Forms.GroupBox();
      this.groupBox1.SuspendLayout();
      this.SuspendLayout();
      // 
      // btnSave
      // 
      this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnSave.Location = new System.Drawing.Point(97, 128);
      this.btnSave.Name = "btnSave";
      this.btnSave.Size = new System.Drawing.Size(95, 23);
      this.btnSave.TabIndex = 17;
      this.btnSave.Text = "ОК";
      this.btnSave.UseVisualStyleBackColor = true;
      this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
      // 
      // btnClose
      // 
      this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.btnClose.Location = new System.Drawing.Point(198, 128);
      this.btnClose.Name = "btnClose";
      this.btnClose.Size = new System.Drawing.Size(75, 23);
      this.btnClose.TabIndex = 16;
      this.btnClose.Text = "Закрыть";
      this.btnClose.UseVisualStyleBackColor = true;
      // 
      // dtpDate
      // 
      this.dtpDate.Location = new System.Drawing.Point(64, 23);
      this.dtpDate.Name = "dtpDate";
      this.dtpDate.Size = new System.Drawing.Size(150, 20);
      this.dtpDate.TabIndex = 12;
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(6, 25);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(36, 13);
      this.label2.TabIndex = 11;
      this.label2.Text = "Дата:";
      // 
      // tbCount
      // 
      this.tbCount.Location = new System.Drawing.Point(64, 49);
      this.tbCount.Name = "tbCount";
      this.tbCount.Size = new System.Drawing.Size(150, 20);
      this.tbCount.TabIndex = 10;
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(6, 52);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(47, 13);
      this.label1.TabIndex = 9;
      this.label1.Text = "Пробег:";
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(12, 9);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(114, 13);
      this.label3.TabIndex = 18;
      this.label3.Text = "Предыдущий пробег:";
      // 
      // lbPrevMileage
      // 
      this.lbPrevMileage.AutoSize = true;
      this.lbPrevMileage.Location = new System.Drawing.Point(121, 9);
      this.lbPrevMileage.Name = "lbPrevMileage";
      this.lbPrevMileage.Size = new System.Drawing.Size(54, 13);
      this.lbPrevMileage.TabIndex = 19;
      this.lbPrevMileage.Text = "значение";
      // 
      // groupBox1
      // 
      this.groupBox1.Controls.Add(this.label2);
      this.groupBox1.Controls.Add(this.label1);
      this.groupBox1.Controls.Add(this.tbCount);
      this.groupBox1.Controls.Add(this.dtpDate);
      this.groupBox1.Location = new System.Drawing.Point(12, 32);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new System.Drawing.Size(230, 80);
      this.groupBox1.TabIndex = 20;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "Новое значение пробега";
      // 
      // Mileage_AddEdit
      // 
      this.AcceptButton = this.btnSave;
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.CancelButton = this.btnClose;
      this.ClientSize = new System.Drawing.Size(285, 163);
      this.Controls.Add(this.groupBox1);
      this.Controls.Add(this.lbPrevMileage);
      this.Controls.Add(this.label3);
      this.Controls.Add(this.btnSave);
      this.Controls.Add(this.btnClose);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "Mileage_AddEdit";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "Пробег";
      this.Load += new System.EventHandler(this.Mileage_AddEdit_Load);
      this.groupBox1.ResumeLayout(false);
      this.groupBox1.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.DateTimePicker dtpDate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbCount;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lbPrevMileage;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}
