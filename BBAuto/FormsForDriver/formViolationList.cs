using System;
using System.Windows.Forms;
using BBAuto.Domain.Entities;
using BBAuto.Domain.Lists;

namespace BBAuto
{
  public partial class formViolationList : Form
  {
    Driver driver;

    public formViolationList(Driver driver)
    {
      InitializeComponent();

      this.driver = driver;
    }

    private void ViolationList_Load(object sender, EventArgs e)
    {
      var violationList = ViolationList.getInstance();
      dgvViolation.DataSource = violationList.ToDataTable(driver);

      if (dgvViolation.DataSource != null)
        formatDGV();
    }

    private void formatDGV()
    {
      dgvViolation.Columns[0].Visible = false;
      dgvViolation.Columns[1].Visible = false;
      dgvViolation.Columns[5].Visible = false;
    }

    private void dgvViolation_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
    {
      if (e.ColumnIndex == 2)
      {
        var carId = Convert.ToInt32(dgvViolation.Rows[dgvViolation.SelectedCells[0].RowIndex].Cells[1].Value);
        var car = CarList.GetInstance().getItem(carId);

        var carForm = new Car_AddEdit(car);
        carForm.ShowDialog();
      }
    }
  }
}
