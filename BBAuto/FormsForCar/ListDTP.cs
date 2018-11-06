using BBAuto.Domain.Entities;
using BBAuto.Domain.ForCar;
using BBAuto.Domain.Lists;
using System;
using System.Windows.Forms;

namespace BBAuto
{
  public partial class ListDTP : Form
  {
    private readonly Car _car;

    private readonly DTPList _dtpList;

    public ListDTP(Car car)
    {
      InitializeComponent();

      _car = car;

      _dtpList = DTPList.getInstance();

      LoadDtp();
    }

    private void Add_Click(object sender, EventArgs e)
    {
      var aedtp = new DTP_AddEdit(new DTP(_car));
      aedtp.ShowDialog();
    }

    private void delete_Click(object sender, EventArgs e)
    {
      if (int.TryParse(_dgvDTP.Rows[_dgvDTP.SelectedCells[0].RowIndex].Cells[0].Value?.ToString(), out int dtpId))
        _dtpList.Delete(dtpId);

      LoadDtp();
    }

    private void LoadDtp()
    {
      _dgvDTP.DataSource = _dtpList.ToDataTable(_car);
    }

    private void _dgvDTP_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
    {
      if (!isCellNoHeader(e.RowIndex))
        return;

      if (!int.TryParse(_dgvDTP.Rows[e.RowIndex].Cells[0].Value?.ToString(), out int dtpId))
        return;

      var dtpForm = new DTP_AddEdit(_dtpList.getItem(dtpId));
      dtpForm.ShowDialog();
    }

    private bool isCellNoHeader(int rowIndex)
    {
      return rowIndex >= 0;
    }
  }
}
