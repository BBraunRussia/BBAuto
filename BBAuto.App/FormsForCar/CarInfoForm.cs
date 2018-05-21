using System;
using System.Data;
using System.Windows.Forms;
using BBAuto.Logic.Services.Car;
using BBAuto.Logic.Services.Grade;

namespace BBAuto.App.FormsForCar
{
  public partial class CarInfoForm : Form, ICarInfoForm
  {
    private readonly IGradeService _gradeService;

    public CarInfoForm(IGradeService gradeService)
    {
      _gradeService = gradeService;

      InitializeComponent();
    }

    public DialogResult ShowDialog(CarModel car)
    {
      var dt = car.ToDataTableInfo();

      var grade = _gradeService.GetById(car.GradeId ?? 0);

      if (grade != null)
      {
        var dt2 = grade.ToDataTable();

        foreach (DataRow row in dt2.Rows)
          dt.Rows.Add(row.ItemArray);
      }

      _dgvCarInfo.DataSource = dt;

      ResizeDgv();

      return ShowDialog();
    }

    private void FormCarInfo_Resize(object sender, EventArgs e)
    {
      ResizeDgv();
    }

    private void ResizeDgv()
    {
      _dgvCarInfo.Columns[0].Width = _dgvCarInfo.Width / 2;
      _dgvCarInfo.Columns[1].Width = _dgvCarInfo.Width / 2;
    }
  }
}
