using System;
using System.Windows.Forms;
using BBAuto.Domain.Entities;
using BBAuto.Domain.Lists;

namespace BBAuto
{
  public partial class formDTPList : Form
  {
    private readonly Driver _driver;

    public formDTPList(Driver driver)
    {
      InitializeComponent();
      _driver = driver;
    }

    private void formDTPList_Load(object sender, EventArgs e)
    {
      DTPList dtpList = DTPList.getInstance();
      dgvDTP.DataSource = dtpList.ToDataTable(_driver);

      if (dgvDTP.DataSource != null)
        formatDGV();
    }

    private void formatDGV()
    {
      dgvDTP.Columns[0].Visible = false;
      dgvDTP.Columns[1].Visible = false;
    }

    private void dgvDTP_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
    {
      if (e.ColumnIndex == 2)
      {
        var carId = Convert.ToInt32(dgvDTP.Rows[dgvDTP.SelectedCells[0].RowIndex].Cells[1].Value);
        var car = CarList.GetInstance().getItem(carId);

        var carForm = new CarForm(car);
        carForm.ShowDialog();
      }
    }
  }
}
