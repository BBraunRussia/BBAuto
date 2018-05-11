namespace BBAuto.App.CommonForms
{
    partial class RouteListForm
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
            this.btnDel = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.dgv = new System.Windows.Forms.DataGridView();
            this.cbRegion = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cbMyPoint1 = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.SuspendLayout();
            // 
            // btnDel
            // 
            this.btnDel.Location = new System.Drawing.Point(93, 83);
            this.btnDel.Name = "btnDel";
            this.btnDel.Size = new System.Drawing.Size(75, 23);
            this.btnDel.TabIndex = 47;
            this.btnDel.Text = "Удалить";
            this.btnDel.UseVisualStyleBackColor = true;
            this.btnDel.Click += new System.EventHandler(this.btnDel_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(12, 83);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 46;
            this.btnAdd.Text = "Добавить";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(408, 330);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 45;
            this.btnCancel.Text = "Закрыть";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // dgv
            // 
            this.dgv.AllowUserToAddRows = false;
            this.dgv.AllowUserToDeleteRows = false;
            this.dgv.AllowUserToResizeRows = false;
            this.dgv.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgv.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv.Location = new System.Drawing.Point(12, 125);
            this.dgv.Name = "dgv";
            this.dgv.ReadOnly = true;
            this.dgv.RowHeadersVisible = false;
            this.dgv.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgv.Size = new System.Drawing.Size(471, 199);
            this.dgv.TabIndex = 44;
            this.dgv.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgv_CellMouseDoubleClick);
            this.dgv.Resize += new System.EventHandler(this.dgv_Resize);
            // 
            // cbRegion
            // 
            this.cbRegion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbRegion.FormattingEnabled = true;
            this.cbRegion.Location = new System.Drawing.Point(64, 6);
            this.cbRegion.Name = "cbRegion";
            this.cbRegion.Size = new System.Drawing.Size(208, 21);
            this.cbRegion.TabIndex = 49;
            this.cbRegion.SelectedIndexChanged += new System.EventHandler(this.cbRegion_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 13);
            this.label1.TabIndex = 48;
            this.label1.Text = "Регион:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 109);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(110, 13);
            this.label3.TabIndex = 52;
            this.label3.Text = "Пункты назначения:";
            // 
            // cbMyPoint1
            // 
            this.cbMyPoint1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbMyPoint1.FormattingEnabled = true;
            this.cbMyPoint1.Location = new System.Drawing.Point(12, 46);
            this.cbMyPoint1.Name = "cbMyPoint1";
            this.cbMyPoint1.Size = new System.Drawing.Size(436, 21);
            this.cbMyPoint1.TabIndex = 51;
            this.cbMyPoint1.SelectedIndexChanged += new System.EventHandler(this.cbMyPoint1_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(108, 13);
            this.label2.TabIndex = 50;
            this.label2.Text = "Пункт отправления:";
            // 
            // formRouteList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(495, 365);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cbMyPoint1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cbRegion);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnDel);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.dgv);
            this.Name = "formRouteList";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Справочник \"Маршруты\"";
            this.Load += new System.EventHandler(this.formRouteList_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnDel;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.DataGridView dgv;
        private System.Windows.Forms.ComboBox cbRegion;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbMyPoint1;
        private System.Windows.Forms.Label label2;
    }
}
