using System;
using System.Windows.Forms;
using BBAuto.Domain.Entities;
using BBAuto.Domain.Lists;

namespace BBAuto
{
  public partial class formDriverCar : Form
  {
    private readonly Driver _driver;

    public formDriverCar(Driver driver)
    {
      InitializeComponent();

      _driver = driver;
    }

    private void DriverCar_Load(object sender, EventArgs e)
    {
      DriverCarList driverCarList = DriverCarList.GetInstance();

      dgvDriverCar.DataSource = driverCarList.ToDataTableCar(_driver);
      dgvDriverCar.Columns[0].Visible = false;
      dgvDriverCar.Columns[1].Visible = false;
      dgvDriverCar.Columns[8].Visible = false;
      dgvDriverCar.Columns[9].Visible = false;
    }

    private void dgvDriverCar_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
    {
      if (e.ColumnIndex == 2)
      {
        var carId = Convert.ToInt32(dgvDriverCar.Rows[dgvDriverCar.SelectedCells[0].RowIndex].Cells[1].Value);
        var car = CarList.GetInstance().getItem(carId);

        var carForm = new CarForm(car);
        carForm.ShowDialog();
      }
    }
  }
}
