namespace BBAuto.App.AddEdit
{
    partial class GradeForm
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
            this.label2 = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.tbName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbEVol = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tbMaxLoad = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tbNoLoad = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cbEngineType = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.tbEPower = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(136, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "Название комплектации:";
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(169, 275);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 9;
            this.btnClose.Text = "Закрыть";
            this.btnClose.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Location = new System.Drawing.Point(68, 275);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(95, 23);
            this.btnSave.TabIndex = 8;
            this.btnSave.Text = "Сохранить";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.BtnOK_Click);
            // 
            // tbName
            // 
            this.tbName.Location = new System.Drawing.Point(12, 25);
            this.tbName.Name = "tbName";
            this.tbName.Size = new System.Drawing.Size(207, 20);
            this.tbName.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 102);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(123, 13);
            this.label3.TabIndex = 14;
            this.label3.Text = "Объём двигателя см3:";
            // 
            // tbEVol
            // 
            this.tbEVol.Location = new System.Drawing.Point(12, 118);
            this.tbEVol.Name = "tbEVol";
            this.tbEVol.Size = new System.Drawing.Size(207, 20);
            this.tbEVol.TabIndex = 4;
            this.tbEVol.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TbEVol_KeyPress);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 185);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(210, 13);
            this.label4.TabIndex = 16;
            this.label4.Text = "Разрешенная максимальная масса, кг:";
            // 
            // tbMaxLoad
            // 
            this.tbMaxLoad.Location = new System.Drawing.Point(12, 201);
            this.tbMaxLoad.Name = "tbMaxLoad";
            this.tbMaxLoad.Size = new System.Drawing.Size(207, 20);
            this.tbMaxLoad.TabIndex = 6;
            this.tbMaxLoad.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TbMaxLoad_KeyPress);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 224);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(130, 13);
            this.label5.TabIndex = 17;
            this.label5.Text = "Масса без нагрузки, кг:";
            // 
            // tbNoLoad
            // 
            this.tbNoLoad.Location = new System.Drawing.Point(12, 240);
            this.tbNoLoad.Name = "tbNoLoad";
            this.tbNoLoad.Size = new System.Drawing.Size(207, 20);
            this.tbNoLoad.TabIndex = 7;
            this.tbNoLoad.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TbNoLoad_KeyPress);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(9, 52);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(84, 13);
            this.label6.TabIndex = 13;
            this.label6.Text = "Тип двигателя:";
            // 
            // cbEngineType
            // 
            this.cbEngineType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbEngineType.FormattingEnabled = true;
            this.cbEngineType.Location = new System.Drawing.Point(12, 68);
            this.cbEngineType.Name = "cbEngineType";
            this.cbEngineType.Size = new System.Drawing.Size(207, 21);
            this.cbEngineType.TabIndex = 28;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(13, 143);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(139, 13);
            this.label7.TabIndex = 15;
            this.label7.Text = "Мощность двигателя л.с.:";
            // 
            // tbEPower
            // 
            this.tbEPower.Location = new System.Drawing.Point(12, 159);
            this.tbEPower.Name = "tbEPower";
            this.tbEPower.Size = new System.Drawing.Size(207, 20);
            this.tbEPower.TabIndex = 5;
            this.tbEPower.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TbEPower_KeyPress);
            // 
            // Grade_AddEdit
            // 
            this.AcceptButton = this.btnSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(256, 310);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.tbEPower);
            this.Controls.Add(this.cbEngineType);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.tbNoLoad);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.tbMaxLoad);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tbEVol);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.tbName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Grade_AddEdit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Комплектация";
            this.Load += new System.EventHandler(this.aeGrade_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.TextBox tbName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbEVol;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbMaxLoad;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbNoLoad;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cbEngineType;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tbEPower;
    }
}
