namespace BBAuto.AddEdit
{
  partial class TransponderForm
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
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
      this.tbComment = new System.Windows.Forms.TextBox();
      this.label6 = new System.Windows.Forms.Label();
      this.chbLost = new System.Windows.Forms.CheckBox();
      this.btnDel = new System.Windows.Forms.Button();
      this.btnAdd = new System.Windows.Forms.Button();
      this._dgv = new System.Windows.Forms.DataGridView();
      this.btnClose = new System.Windows.Forms.Button();
      this.btnSave = new System.Windows.Forms.Button();
      this.cbRegion = new System.Windows.Forms.ComboBox();
      this.label4 = new System.Windows.Forms.Label();
      this.tbNumber = new System.Windows.Forms.TextBox();
      this.label1 = new System.Windows.Forms.Label();
      ((System.ComponentModel.ISupportInitialize)(this._dgv)).BeginInit();
      this.SuspendLayout();
      // 
      // tbComment
      // 
      this.tbComment.Location = new System.Drawing.Point(12, 112);
      this.tbComment.Multiline = true;
      this.tbComment.Name = "tbComment";
      this.tbComment.Size = new System.Drawing.Size(203, 73);
      this.tbComment.TabIndex = 63;
      // 
      // label6
      // 
      this.label6.AutoSize = true;
      this.label6.Location = new System.Drawing.Point(12, 96);
      this.label6.Name = "label6";
      this.label6.Size = new System.Drawing.Size(73, 13);
      this.label6.TabIndex = 62;
      this.label6.Text = "Примечание:";
      // 
      // chbLost
      // 
      this.chbLost.AutoSize = true;
      this.chbLost.Location = new System.Drawing.Point(12, 191);
      this.chbLost.Name = "chbLost";
      this.chbLost.Size = new System.Drawing.Size(63, 17);
      this.chbLost.TabIndex = 61;
      this.chbLost.Text = "Утерян";
      this.chbLost.UseVisualStyleBackColor = true;
      // 
      // btnDel
      // 
      this.btnDel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.btnDel.Location = new System.Drawing.Point(453, 12);
      this.btnDel.Name = "btnDel";
      this.btnDel.Size = new System.Drawing.Size(132, 23);
      this.btnDel.TabIndex = 60;
      this.btnDel.Text = "Удалить водителя";
      this.btnDel.UseVisualStyleBackColor = true;
      this.btnDel.Click += new System.EventHandler(this.btnDel_Click);
      // 
      // btnAdd
      // 
      this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.btnAdd.Location = new System.Drawing.Point(322, 13);
      this.btnAdd.Name = "btnAdd";
      this.btnAdd.Size = new System.Drawing.Size(125, 23);
      this.btnAdd.TabIndex = 59;
      this.btnAdd.Text = "Добавить водителя";
      this.btnAdd.UseVisualStyleBackColor = true;
      this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
      // 
      // _dgv
      // 
      this._dgv.AllowUserToAddRows = false;
      this._dgv.AllowUserToDeleteRows = false;
      this._dgv.AllowUserToResizeRows = false;
      this._dgv.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this._dgv.BackgroundColor = System.Drawing.SystemColors.Window;
      dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
      dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
      dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
      dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
      dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
      dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
      this._dgv.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
      this._dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
      dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
      dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
      dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
      dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
      dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
      this._dgv.DefaultCellStyle = dataGridViewCellStyle5;
      this._dgv.Location = new System.Drawing.Point(221, 42);
      this._dgv.Name = "_dgv";
      this._dgv.ReadOnly = true;
      dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
      dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
      dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
      dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
      dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
      dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
      this._dgv.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
      this._dgv.RowHeadersVisible = false;
      this._dgv.Size = new System.Drawing.Size(364, 259);
      this._dgv.TabIndex = 58;
      this._dgv.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this._dgv_CellDoubleClick);
      // 
      // btnClose
      // 
      this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.btnClose.Location = new System.Drawing.Point(510, 307);
      this.btnClose.Name = "btnClose";
      this.btnClose.Size = new System.Drawing.Size(75, 23);
      this.btnClose.TabIndex = 57;
      this.btnClose.Text = "Закрыть";
      this.btnClose.UseVisualStyleBackColor = true;
      // 
      // btnSave
      // 
      this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnSave.Location = new System.Drawing.Point(409, 307);
      this.btnSave.Name = "btnSave";
      this.btnSave.Size = new System.Drawing.Size(95, 23);
      this.btnSave.TabIndex = 56;
      this.btnSave.Text = "Сохранить";
      this.btnSave.UseVisualStyleBackColor = true;
      this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
      // 
      // cbRegion
      // 
      this.cbRegion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cbRegion.FormattingEnabled = true;
      this.cbRegion.Location = new System.Drawing.Point(12, 67);
      this.cbRegion.Name = "cbRegion";
      this.cbRegion.Size = new System.Drawing.Size(203, 21);
      this.cbRegion.TabIndex = 52;
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Location = new System.Drawing.Point(12, 51);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(46, 13);
      this.label4.TabIndex = 51;
      this.label4.Text = "Регион:";
      // 
      // tbNumber
      // 
      this.tbNumber.Location = new System.Drawing.Point(12, 25);
      this.tbNumber.Name = "tbNumber";
      this.tbNumber.Size = new System.Drawing.Size(203, 20);
      this.tbNumber.TabIndex = 50;
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(12, 9);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(44, 13);
      this.label1.TabIndex = 45;
      this.label1.Text = "Номер:";
      // 
      // TransponderForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(597, 342);
      this.Controls.Add(this.tbComment);
      this.Controls.Add(this.label6);
      this.Controls.Add(this.chbLost);
      this.Controls.Add(this.btnDel);
      this.Controls.Add(this.btnAdd);
      this.Controls.Add(this._dgv);
      this.Controls.Add(this.btnClose);
      this.Controls.Add(this.btnSave);
      this.Controls.Add(this.cbRegion);
      this.Controls.Add(this.label4);
      this.Controls.Add(this.tbNumber);
      this.Controls.Add(this.label1);
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "TransponderForm";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "Карточка \"Транспондер\"";
      this.Load += new System.EventHandler(this.TransponderForm_Load);
      ((System.ComponentModel.ISupportInitialize)(this._dgv)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.TextBox tbComment;
    private System.Windows.Forms.Label label6;
    private System.Windows.Forms.CheckBox chbLost;
    private System.Windows.Forms.Button btnDel;
    private System.Windows.Forms.Button btnAdd;
    private System.Windows.Forms.DataGridView _dgv;
    private System.Windows.Forms.Button btnClose;
    private System.Windows.Forms.Button btnSave;
    private System.Windows.Forms.ComboBox cbRegion;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.TextBox tbNumber;
    private System.Windows.Forms.Label label1;
  }
}
