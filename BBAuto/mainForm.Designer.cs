namespace BBAuto
{
    partial class mainForm
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
      this.statusStrip1 = new System.Windows.Forms.StatusStrip();
      this._dgvCar = new System.Windows.Forms.DataGridView();
      this.btnBack = new System.Windows.Forms.Button();
      this.btnClearFilter = new System.Windows.Forms.Button();
      this.btnApply = new System.Windows.Forms.Button();
      this.tbSearch = new System.Windows.Forms.TextBox();
      this.label1 = new System.Windows.Forms.Label();
      this.btnUpdateData = new System.Windows.Forms.Button();
      ((System.ComponentModel.ISupportInitialize)(this._dgvCar)).BeginInit();
      this.SuspendLayout();
      // 
      // statusStrip1
      // 
      this.statusStrip1.Location = new System.Drawing.Point(0, 435);
      this.statusStrip1.Name = "statusStrip1";
      this.statusStrip1.Size = new System.Drawing.Size(975, 22);
      this.statusStrip1.TabIndex = 0;
      this.statusStrip1.Text = "statusStrip1";
      // 
      // _dgvCar
      // 
      this._dgvCar.AllowUserToAddRows = false;
      this._dgvCar.AllowUserToDeleteRows = false;
      this._dgvCar.AllowUserToResizeRows = false;
      this._dgvCar.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this._dgvCar.BackgroundColor = System.Drawing.SystemColors.Window;
      this._dgvCar.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this._dgvCar.Location = new System.Drawing.Point(12, 94);
      this._dgvCar.Name = "_dgvCar";
      this._dgvCar.ReadOnly = true;
      this._dgvCar.RowHeadersVisible = false;
      this._dgvCar.Size = new System.Drawing.Size(951, 338);
      this._dgvCar.TabIndex = 0;
      this._dgvCar.CellContextMenuStripNeeded += new System.Windows.Forms.DataGridViewCellContextMenuStripNeededEventHandler(this._dgvCar_CellContextMenuStripNeeded);
      this._dgvCar.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this._dgvCar_CellDoubleClick);
      this._dgvCar.CellMouseEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this._dgvCar_CellMouseEnter);
      this._dgvCar.ColumnWidthChanged += new System.Windows.Forms.DataGridViewColumnEventHandler(this._dgvCar_ColumnWidthChanged);
      this._dgvCar.SelectionChanged += new System.EventHandler(this._dgvCar_SelectionChanged);
      this._dgvCar.Sorted += new System.EventHandler(this._dgvCar_Sorted);
      // 
      // btnBack
      // 
      this.btnBack.Location = new System.Drawing.Point(216, 27);
      this.btnBack.Name = "btnBack";
      this.btnBack.Size = new System.Drawing.Size(75, 23);
      this.btnBack.TabIndex = 9;
      this.btnBack.Text = "Назад";
      this.btnBack.UseVisualStyleBackColor = true;
      this.btnBack.Visible = false;
      this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
      // 
      // btnClearFilter
      // 
      this.btnClearFilter.Location = new System.Drawing.Point(93, 27);
      this.btnClearFilter.Name = "btnClearFilter";
      this.btnClearFilter.Size = new System.Drawing.Size(117, 23);
      this.btnClearFilter.TabIndex = 10;
      this.btnClearFilter.Text = "Очистить фильтры";
      this.btnClearFilter.UseVisualStyleBackColor = true;
      this.btnClearFilter.Click += new System.EventHandler(this.btnClearFilter_Click);
      // 
      // btnApply
      // 
      this.btnApply.Location = new System.Drawing.Point(12, 27);
      this.btnApply.Name = "btnApply";
      this.btnApply.Size = new System.Drawing.Size(75, 23);
      this.btnApply.TabIndex = 11;
      this.btnApply.Text = "Применить";
      this.btnApply.UseVisualStyleBackColor = true;
      this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
      // 
      // tbSearch
      // 
      this.tbSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.tbSearch.Location = new System.Drawing.Point(706, 27);
      this.tbSearch.Name = "tbSearch";
      this.tbSearch.Size = new System.Drawing.Size(176, 20);
      this.tbSearch.TabIndex = 12;
      this.tbSearch.TextChanged += new System.EventHandler(this.tbSearch_TextChanged);
      this.tbSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbSearch_KeyDown);
      // 
      // label1
      // 
      this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(658, 30);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(42, 13);
      this.label1.TabIndex = 13;
      this.label1.Text = "Поиск:";
      // 
      // btnUpdateData
      // 
      this.btnUpdateData.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.btnUpdateData.Location = new System.Drawing.Point(888, 26);
      this.btnUpdateData.Name = "btnUpdateData";
      this.btnUpdateData.Size = new System.Drawing.Size(75, 23);
      this.btnUpdateData.TabIndex = 14;
      this.btnUpdateData.Text = "Обновить";
      this.btnUpdateData.UseVisualStyleBackColor = true;
      this.btnUpdateData.Click += new System.EventHandler(this.btnUpdateData_Click);
      // 
      // mainForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(975, 457);
      this.Controls.Add(this.btnUpdateData);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.tbSearch);
      this.Controls.Add(this.btnApply);
      this.Controls.Add(this.btnClearFilter);
      this.Controls.Add(this.btnBack);
      this.Controls.Add(this._dgvCar);
      this.Controls.Add(this.statusStrip1);
      this.DoubleBuffered = true;
      this.Name = "mainForm";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "BBAuto";
      this.Load += new System.EventHandler(this.mainForm_Load);
      ((System.ComponentModel.ISupportInitialize)(this._dgvCar)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.DataGridView _dgvCar;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Button btnClearFilter;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.TextBox tbSearch;
        private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Button btnUpdateData;
  }
}

