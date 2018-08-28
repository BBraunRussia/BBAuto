using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using BBAuto.Domain.Dictionary;
using BBAuto.Domain.Entities;
using BBAuto.Domain.Lists;
using BBAuto.Domain.Services.OfficeDocument;
using Common;

namespace BBAuto.Report
{
  public partial class ReportMileageForm : Form
  {
    public ReportMileageForm()
    {
      InitializeComponent();
    }

    private void ReportMileageForm_Load(object sender, EventArgs e)
    {
      var dateMinus2 = DateTime.Today.AddMonths(-2);
      dtpBeginDate.Value = new DateTime(dateMinus2.Year, dateMinus2.Month,
        DateTime.DaysInMonth(dateMinus2.Year, dateMinus2.Month));

      var dateMinus1 = DateTime.Today.AddMonths(-1);
      dtpEndDate.Value = new DateTime(dateMinus1.Year, dateMinus1.Month,
        DateTime.DaysInMonth(dateMinus1.Year, dateMinus1.Month));

      LoadDictionaries();
    }

    private void LoadDictionaries()
    {
      var carList = CarList.GetInstance().GetActualCars().OrderBy(car => car.Grz).Select(car => car.Grz).ToArray();
      cbGrz.Items.Add("(все)");
      cbGrz.Items.AddRange(carList);
      //cbGrz.ValueMember = nameof(Car.ID);
      //cbGrz.DisplayMember = nameof(Car.Grz);

      var markList = Marks.getInstance().ToDataTable();
      cbMark.DataSource = markList;
      cbMark.ValueMember = "id";
      cbMark.DisplayMember = "Название";

      var driverList = DriverList.getInstance().GetList().Where(dr => dr.ID != Consts.ReserveDriverId)
        .OrderBy(dr => dr.Name).Select(d => d.Name).ToArray();
      //cbDriver.ValueMember = nameof(Driver.ID);
      //cbDriver.DisplayMember = nameof(Driver.Name);
      cbDriver.Items.Add("(все)");
      cbDriver.Items.AddRange(driverList);


      var regionList = RegionList.getInstance().GetList().Select(r => r.Name).ToArray();
      cbRegion.Items.Add("(все)");
      cbRegion.Items.AddRange(regionList);
      //cbRegion.ValueMember = nameof(Domain.Tables.Region.ID);
      //cbRegion.DisplayMember = nameof(Domain.Tables.Region.Name);
    }

    private void rb_CheckedChanged(object sender, EventArgs e)
    {
      cbGrz.Enabled = false;
      cbMark.Enabled = false;
      chbModel.Enabled = false;
      cbModel.Enabled = false;
      cbDriver.Enabled = false;
      cbRegion.Enabled = false;
      tbAll.Enabled = false;

      var rb = sender as ButtonBase;

      switch (rb?.Name)
      {
        case nameof(rbGrz):
        {
          cbGrz.Enabled = true;
          break;
        }
        case nameof(rbMark):
        case nameof(chbModel):
        {
          cbMark.Enabled = true;
          chbModel.Enabled = true;
          cbModel.Enabled = chbModel.Checked;
          break;
        }
        case nameof(rbDriver):
        {
          cbDriver.Enabled = true;
          break;
        }
        case nameof(rbRegion):
        {
          cbRegion.Enabled = true;
          break;
        }
        case nameof(rbAll):
        {
          tbAll.Enabled = true;
          break;
        }
      }
    }

    private void tbAll_KeyPress(object sender, KeyPressEventArgs e)
    {
      if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
      {
        e.Handled = true;
      }
    }

    private void cbMark_SelectedValueChanged(object sender, EventArgs e)
    {
      if (int.TryParse(cbMark.SelectedValue?.ToString(), out int markId))
      {
        var modelList = ModelList.getInstance().ToDataTable(markId);
        cbModel.DataSource = modelList;
        cbModel.ValueMember = "id";
        cbModel.DisplayMember = "Название";
      }
    }

    private void btnOk_Click(object sender, EventArgs e)
    {
      IExcelDocumentService service = new ExcelDocumentService();

      var cars = GetCars();

      if (!cars.Any())
      {
        MessageBox.Show("Не найдены автомобили для построения отчёта", "Информация", MessageBoxButtons.OK,
          MessageBoxIcon.Information);
        return;
      }

      var report = service.CreateReportMileage(cars, dtpBeginDate.Value, dtpEndDate.Value);
      report.Show();
    }

    private IList<Car> GetCars()
    {
      var carList = CarList.GetInstance();
      var actualCars = carList.GetActualCars();

      if (rbGrz.Checked)
      {
        return cbGrz.CheckBoxItems.Where(item => item.Checked).Select(item => carList.getItem(item.Text)).ToList();
      }

      if (rbMark.Checked)
      {
        if (chbModel.Checked)
        {
          return carList.GetActualCars().Where(car => car.ModelID == cbModel.SelectedValue.ToString()).ToList();
        }

        int.TryParse(cbMark.SelectedValue.ToString(), out int markId);
        return carList.GetActualCars().Where(car => car.Mark.ID == markId).ToList();
      }

      if (rbDriver.Checked)
      {
        var cars = new List<Car>();
        foreach (var item in cbDriver.CheckBoxItems)
        {
          cars.AddRange(actualCars.Where(car => car.info.Driver.Name == item.Text));
        }

        return cars;
      }

      if (rbRegion.Checked)
      {
        var cars = new List<Car>();
        foreach (var item in cbDriver.CheckBoxItems)
        {
          cars.AddRange(actualCars.Where(car => car.info.Region == item.Text));
        }

        return cars;
      }

      var mileageList = MileageList.getInstance();

      if (int.TryParse(tbAll.Text, out int mileage))
        return actualCars.Where(car =>
        {
          var carMileage = 0;
          try
          {
            mileage = mileageList.GetBeginDistance(car, dtpBeginDate.Value);
          }
          catch
          {
            // ignored
          }
          return carMileage >= mileage;
        }).ToList();

      return new List<Car>();
    }
  }
}
