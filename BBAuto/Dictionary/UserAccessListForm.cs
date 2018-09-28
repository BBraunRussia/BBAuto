using System;
using System.Windows.Forms;
using BBAuto.Domain.Lists;
using BBAuto.Domain.ForDriver;

namespace BBAuto
{
  public partial class UserAccessListForm : Form
  {
    public UserAccessListForm()
    {
      InitializeComponent();
    }

    private void formUsersAccess_Load(object sender, EventArgs e)
    {
      LoadData();
    }

    private void LoadData()
    {
      _dgvUserAccess.DataSource = UserAccessList.getInstance().ToDataTable();
      _dgvUserAccess.Columns[0].Visible = false;
      ResizeDgv();
    }

    private void ResizeDgv()
    {
      var colSize = _dgvUserAccess.Width / 3;

      _dgvUserAccess.Columns[1].Width = colSize;
      _dgvUserAccess.Columns[2].Width = colSize;
      _dgvUserAccess.Columns[3].Width = colSize;
    }

    private void btnAdd_Click(object sender, EventArgs e)
    {
      var userAccess = new UserAccess();
      var userAccessForm = new UserAccessForm(userAccess);
      if (userAccessForm.ShowDialog() == DialogResult.OK)
      {
        UserAccessList.getInstance().Add(userAccess);
        LoadData();
      }
    }

    private void _dgvUserAccess_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
    {
      if (int.TryParse(_dgvUserAccess.Rows[_dgvUserAccess.SelectedCells[0].RowIndex].Cells[0].Value?.ToString(),
        out int dillerId))
      {
        var userAccess = UserAccessList.getInstance().getItem(dillerId);

        var userAccessForm = new UserAccessForm(userAccess);
        if (userAccessForm.ShowDialog() == DialogResult.OK)
          LoadData();
      }
    }

    private void btnDel_Click(object sender, EventArgs e)
    {
      if (MessageBox.Show("Удалить пользователя из списка?", "Удаление", MessageBoxButtons.YesNo,
            MessageBoxIcon.Question) == DialogResult.Yes)
      {
        var userAccessId = Convert.ToInt32(_dgvUserAccess.Rows[_dgvUserAccess.SelectedCells[0].RowIndex].Cells[0].Value);
        UserAccessList.getInstance().Delete(userAccessId);
        LoadData();
      }
    }
  }
}
