using System;
using System.Windows.Forms;
using BBAuto.App.FormsForCar.AddEdit;
using BBAuto.Logic.Entities;
using BBAuto.Logic.ForCar;
using BBAuto.Logic.Lists;

namespace BBAuto.App.FormsForCar
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
      DTP_AddEdit aedtp = new DTP_AddEdit(_car.createDTP());
      aedtp.ShowDialog();
    }

    private void delete_Click(object sender, EventArgs e)
    {
      int idDTP = Convert.ToInt32(_dgvDTP.Rows[_dgvDTP.SelectedCells[0].RowIndex].Cells[0].Value);

      _dtpList.Delete(idDTP);

      LoadDtp();
    }

    private void LoadDtp()
    {
      _dgvDTP.DataSource = _dtpList.ToDataTable(_car.Id);
    }

    private void _dgvDTP_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
    {
      if (IsCellNoHeader(e.RowIndex))
      {
        int idDtp = Convert.ToInt32(_dgvDTP.Rows[e.RowIndex].Cells[0].Value);

        var dtp = _dtpList.getItem(idDtp);

        var aedtp = new DTP_AddEdit(dtp);
        aedtp.ShowDialog();
      }
    }

    private static bool IsCellNoHeader(int rowIndex)
    {
      return rowIndex >= 0;
    }
  }
}
