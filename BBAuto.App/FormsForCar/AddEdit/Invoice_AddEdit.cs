using System;
using System.Windows.Forms;
using BBAuto.App.Events;
using BBAuto.Logic.Entities;
using BBAuto.Logic.ForCar;
using BBAuto.Logic.Lists;
using BBAuto.Logic.Static;
using BBAuto.Logic.Tables;

namespace BBAuto.App.FormsForCar.AddEdit
{
  public partial class Invoice_AddEdit : Form
  {
    private readonly Invoice _invoice;
    private bool _load;

    private WorkWithForm _workWithForm;

    private CheckBox _check;

    public Invoice_AddEdit(Invoice invoice)
    {
      InitializeComponent();

      _invoice = invoice;

      _check = new CheckBox();

      if (_invoice.Id == 0)
      {
        _check.Location = new System.Drawing.Point(12, 225);
        _check.Width = 250;
        _check.Text = "удалить сдающего из списка Водителей";
        Controls.Add(_check);
      }

      lbMoveCar.Text = "Перемещение автомобиля " + _invoice.CarId;
    }

    private void Invoice_AddEdit_Load(object sender, EventArgs e)
    {
      loadData();

      Text = "Перемещение №" + _invoice.Number;

      _workWithForm = new WorkWithForm(Controls, btnSave, btnClose);
      _workWithForm.EditModeChanged += EditModeChanged;
      _workWithForm.SetEditMode(_invoice.Id == 0);
    }

    private void EditModeChanged(Object sender, EditModeEventArgs e)
    {
      if (_invoice.Id != 0)
      {
        cbDriverFrom.Enabled = false;
        cbRegionFrom.Enabled = false;
        dtpDate.Enabled = false;
      }
    }

    private void loadData()
    {
      LoadDictionary();

      lbNumber.Text = "Накладная №" + _invoice.Number;

      cbRegionFrom.SelectedValue = _invoice.RegionFromId;
      cbRegionTo.SelectedValue = _invoice.RegionToId;
      cbDriverFrom.SelectedValue = _invoice.DriverFromId;
      cbDriverTo.SelectedValue = _invoice.DriverToId;

      dtpDate.Value = _invoice.Date;
      mtbDateMove.Text = _invoice.DateMove;

      TextBox tbFile = ucFile.Controls["tbFile"] as TextBox;
      tbFile.Text = _invoice.File;
    }

    private void LoadDictionary()
    {
      _load = false;

      setDataSourceRegion(cbRegionFrom);
      setDataSourceRegion(cbRegionTo);

      SetDataSourceDriver(cbDriverFrom);
      SetDataSourceDriver(cbDriverTo);

      _load = true;
    }

    private void SetDataSourceDriver(ComboBox combo)
    {
      var driverList = DriverList.getInstance();

      combo.DataSource = driverList.ToDataTable(_invoice.Id != 0);
      combo.DisplayMember = "ФИО";
      combo.ValueMember = "id";
    }

    private void setDataSourceRegion(ComboBox combo)
    {
      combo.DataSource = OneStringDictionary.getDataTable("Region");
      combo.DisplayMember = "Название";
      combo.ValueMember = "region_id";
    }

    private void btnSave_Click(object sender, EventArgs e)
    {
      if (_workWithForm.IsEditMode())
      {
        _invoice.DriverFromId = cbDriverFrom.SelectedValue.ToString();
        _invoice.DriverToId = cbDriverTo.SelectedValue.ToString();
        _invoice.RegionFromId = cbRegionFrom.SelectedValue.ToString();
        _invoice.RegionToId = cbRegionTo.SelectedValue.ToString();
        _invoice.Date = dtpDate.Value;
        _invoice.DateMove = mtbDateMove.Text;

        TextBox tbFile = ucFile.Controls["tbFile"] as TextBox;
        _invoice.File = tbFile.Text;

        _invoice.Save();

        if (_check.Checked)
        {
          var driverList = DriverList.getInstance();
          var driver = driverList.getItem(Convert.ToInt32(cbDriverFrom.SelectedValue.ToString()));
          driver.IsDriver = false;
          driver.Save();
        }

        DialogResult = System.Windows.Forms.DialogResult.OK;
      }
      else
        _workWithForm.SetEditMode(true);
    }

    private void cbRegionTo_SelectedIndexChanged(object sender, EventArgs e)
    {
      //changeDataSourceDriverTo();
    }

    private void changeDataSourceDriverTo()
    {
      if (IsRegionToNotNull())
      {
        Region region = GetRegion();

        DriverList driverList = DriverList.getInstance();
        cbDriverTo.DataSource = driverList.ToDataTableByRegion(region, _invoice.Id != 0);
        cbDriverTo.DisplayMember = "ФИО";
        cbDriverTo.ValueMember = "id";
      }
    }

    private Region GetRegion()
    {
      int.TryParse(cbRegionTo.SelectedValue.ToString(), out int idRegion);
      var regionList = RegionList.getInstance();
      return regionList.getItem(idRegion);
    }

    private bool IsRegionToNotNull()
    {
      return _load && cbRegionTo.SelectedValue != null;
    }

    private void cbDriverTo_SelectedIndexChanged(object sender, EventArgs e)
    {
      var driverList = DriverList.getInstance();

      if (cbDriverTo.SelectedValue == null)
        return;

      int idDriver;
      if (int.TryParse(cbDriverTo.SelectedValue.ToString(), out idDriver))
      {
        Driver driver = driverList.getItem(idDriver);
        cbRegionTo.SelectedValue = driver.Region.Id;
      }
    }
  }
}
