using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BBAuto.Domain;
using BBAuto.Domain.Lists;
using BBAuto.Domain.ForCar;
using BBAuto.Domain.Tables;

namespace BBAuto
{
  public partial class formSsDTPList : Form
  {
    public formSsDTPList()
    {
      InitializeComponent();
    }

    private void formSsDTPList_Load(object sender, EventArgs e)
    {
      loadData();
    }

    private void loadData()
    {
      SsDTPList ssDTPs = SsDTPList.getInstance();

      _dgvSsDTP.DataSource = ssDTPs.ToDataTable();
      _dgvSsDTP.Columns[0].Visible = false;
    }

    private void btnAdd_Click(object sender, EventArgs e)
    {
      SsDTPList ssDTPs = SsDTPList.getInstance();
      SsDTP ssDTP = new SsDTP();

      SsDTP_AddEdit aessDTP = new SsDTP_AddEdit(ssDTP);

      if (aessDTP.ShowDialog() == DialogResult.OK)
      {
        ssDTPs.Add(ssDTP);
        loadData();
      }
    }

    private void btnDel_Click(object sender, EventArgs e)
    {
      Mark mark = GetMark();

      SsDTPList.getInstance().Delete(mark.ID);

      loadData();
    }

    private void _dgvSsDTP_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
    {
      Mark mark = GetMark();

      SsDTP ssDTP = SsDTPList.getInstance().getItem(mark.ID);

      SsDTP_AddEdit aessDTP = new SsDTP_AddEdit(ssDTP);
      if (aessDTP.ShowDialog() == DialogResult.OK)
        loadData();
    }

    private Mark GetMark()
    {
      int idMark;
      int.TryParse(_dgvSsDTP.Rows[_dgvSsDTP.SelectedCells[0].RowIndex].Cells[0].Value.ToString(), out idMark);
      return MarkList.getInstance().getItem(idMark);
    }
  }
}
