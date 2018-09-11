using System;
using System.Windows.Forms;
using BBAuto.Domain.Lists;
using BBAuto.Domain.ForDriver;

namespace BBAuto
{
  public partial class formUsersAccess : Form
  {
    public formUsersAccess()
    {
      InitializeComponent();
    }

    private void formUsersAccess_Load(object sender, EventArgs e)
    {
      loadData();
    }

    private void loadData()
    {
      _dgvUserAccess.DataSource = UserAccessList.getInstance().ToDataTable();
      _dgvUserAccess.Columns[0].Visible = false;
      resizeDGV();
    }

    private void resizeDGV()
    {
      int colSize = _dgvUserAccess.Width / 3;

      _dgvUserAccess.Columns[1].Width = colSize;
      _dgvUserAccess.Columns[2].Width = colSize;
      _dgvUserAccess.Columns[3].Width = colSize;
    }

    private void btnAdd_Click(object sender, EventArgs e)
    {
      UserAccess userAccess = new UserAccess();
      UserAccess_AddEdit dAE = new UserAccess_AddEdit(userAccess);
      if (dAE.ShowDialog() == DialogResult.OK)
      {
        UserAccessList.getInstance().Add(userAccess);
        loadData();
      }
    }

    private void _dgvUserAccess_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
    {
      int idDiller = Convert.ToInt32(_dgvUserAccess.Rows[_dgvUserAccess.SelectedCells[0].RowIndex].Cells[0].Value);

      UserAccess userAccess = UserAccessList.getInstance().getItem(idDiller);

      UserAccess_AddEdit dAE = new UserAccess_AddEdit(userAccess);
      if (dAE.ShowDialog() == DialogResult.OK)
        loadData();
    }

    private void btnDel_Click(object sender, EventArgs e)
    {
      int idUserAccess = Convert.ToInt32(_dgvUserAccess.Rows[_dgvUserAccess.SelectedCells[0].RowIndex].Cells[0].Value);
      UserAccessList.getInstance().Delete(idUserAccess);
    }
  }
}
