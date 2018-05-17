using System;
using System.Data;
using System.Windows.Forms;
using BBAuto.Logic.Entities;
using BBAuto.Logic.Lists;
using BBAuto.Logic.Services.Car;

namespace BBAuto.App.FormsForCar
{
  public partial class formCarInfo : Form
  {
    public formCarInfo(CarModel car)
    {
      InitializeComponent();

      DataTable dt = car.ToDataTableInfo();
      var grade = GradeList.getInstance().getItem(car.GradeId.Value);

      DataTable dt2 = grade.ToDataTable();

      foreach (DataRow row in dt2.Rows)
        dt.Rows.Add(row.ItemArray);

      _dgvCarInfo.DataSource = dt;

      ResizeDGV();
    }

    private void formCarInfo_Resize(object sender, EventArgs e)
    {
      ResizeDGV();
    }

    private void ResizeDGV()
    {
      _dgvCarInfo.Columns[0].Width = _dgvCarInfo.Width / 2;
      _dgvCarInfo.Columns[1].Width = _dgvCarInfo.Width / 2;
    }
  }
}
