using System;
using System.Windows.Forms;
using BBAuto.Domain.Entities;
using BBAuto.Domain.Lists;

namespace BBAuto
{
  public partial class formFuelCardDriver : Form
  {
    private Driver _driver;

    public formFuelCardDriver(Driver driver)
    {
      InitializeComponent();

      _driver = driver;
    }

    private void formFuelCardDriver_Load(object sender, EventArgs e)
    {
      var fuelCardDriverList = FuelCardDriverList.getInstance();

      dgvDriverFuelCard.DataSource = fuelCardDriverList.ToDataTable(_driver);
      dgvDriverFuelCard.Columns[0].Visible = false;
      dgvDriverFuelCard.Columns[1].Visible = false;
      dgvDriverFuelCard.Columns[3].Visible = false;
    }

    private void dgvDriverFuelCard_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
    {
      if (e.ColumnIndex == 2)
      {
        var fuelCardId = Convert.ToInt32(dgvDriverFuelCard.Rows[dgvDriverFuelCard.SelectedCells[0].RowIndex].Cells[1].Value);
        var fuelCard = FuelCardList.getInstance().getItem(fuelCardId);

        var fuelCardForm = new FuelCard_AddEdit(fuelCard);
        fuelCardForm.ShowDialog();
      }
    }
  }
}
