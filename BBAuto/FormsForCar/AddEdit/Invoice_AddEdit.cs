using System;
using System.Windows.Forms;
using BBAuto.Domain.Entities;
using BBAuto.Domain.ForCar;
using BBAuto.Domain.Lists;
using BBAuto.Domain.Static;
using BBAuto.Domain.Tables;
using Common;

namespace BBAuto.FormsForCar.AddEdit
{
  public partial class Invoice_AddEdit : Form
  {
    private readonly Invoice _invoice;
    private bool _load;

    private WorkWithForm _workWithForm;

    private readonly CheckBox _check;

    public Invoice_AddEdit(Invoice invoice)
    {
      InitializeComponent();

      _invoice = invoice;

      _check = new CheckBox();

      if (_invoice.ID == 0)
      {
        _check.Location = new System.Drawing.Point(12, 225);
        _check.Width = 250;
        _check.Text = "удалить сдающего из списка Водителей";
        Controls.Add(_check);
      }

      lbMoveCar.Text = "Перемещение автомобиля " + _invoice.Car;
    }

    private void Invoice_AddEdit_Load(object sender, EventArgs e)
    {
      LoadData();

      Text = "Перемещение №" + _invoice.Number;

      _workWithForm = new WorkWithForm(Controls, btnSave, btnClose);
      _workWithForm.EditModeChanged += EditModeChanged;
      _workWithForm.SetEditMode(_invoice.ID == 0);
    }

    private void EditModeChanged(Object sender, EditModeEventArgs e)
    {
      if (_invoice.ID != 0)
      {
        cbDriverFrom.Enabled = false;
        cbRegionFrom.Enabled = false;
        dtpDate.Enabled = false;
      }
    }

    private void LoadData()
    {
      LoadDictionary();

      lbNumber.Text = "Накладная №" + _invoice.Number;

      cbRegionFrom.SelectedValue = _invoice.RegionFromID;
      cbRegionTo.SelectedValue = _invoice.RegionToID;
      cbDriverFrom.SelectedValue = _invoice.DriverFromID;
      cbDriverTo.SelectedValue = _invoice.DriverToID;

      chbMain.Checked = _invoice.IsMain;
      
      dtpDate.Value = _invoice.Date;
      mtbDateMove.Text = _invoice.DateMove?.ToShortDateString();

      TextBox tbFile = ucFile.Controls["tbFile"] as TextBox;
      tbFile.Text = _invoice.File;
    }

    private void LoadDictionary()
    {
      _load = false;

      SetDataSourceRegion(cbRegionFrom);
      SetDataSourceRegion(cbRegionTo);

      SetDataSourceDriver(cbDriverFrom);
      SetDataSourceDriver(cbDriverTo);

      _load = true;
    }

    private void SetDataSourceDriver(ComboBox combo)
    {
      var driverList = DriverList.getInstance();

      combo.DataSource = driverList.ToDataTable(_invoice.ID != 0);
      combo.DisplayMember = "ФИО";
      combo.ValueMember = "id";
    }

    private static void SetDataSourceRegion(ComboBox combo)
    {
      combo.DataSource = OneStringDictionary.getDataTable("Region");
      combo.DisplayMember = "Название";
      combo.ValueMember = "region_id";
    }

    private void btnSave_Click(object sender, EventArgs e)
    {
      if (_workWithForm.IsEditMode())
      {
        _invoice.DriverFromID = cbDriverFrom.SelectedValue.ToString();
        _invoice.DriverToID = cbDriverTo.SelectedValue.ToString();
        _invoice.RegionFromID = cbRegionFrom.SelectedValue.ToString();
        _invoice.RegionToID = cbRegionTo.SelectedValue.ToString();

        _invoice.IsMain = chbMain.Checked;

        _invoice.Date = dtpDate.Value;

        if (DateTime.TryParse(mtbDateMove.Text, out DateTime dateMove))
          _invoice.DateMove = dateMove;
        else
          _invoice.DateMove = null;

        var tbFile = ucFile.Controls["tbFile"] as TextBox;
        _invoice.File = tbFile.Text;

        _invoice.Save();

        if (_invoice.IsMain)
        {
          DriverCarList.GetInstance().ReLoad();
        }

        if (_check.Checked)
        {
          DriverList driverList = DriverList.getInstance();
          Driver driver = driverList.getItem(Convert.ToInt32(cbDriverFrom.SelectedValue.ToString()));
          driver.IsDriver = false;
          driver.Save();
        }

        DialogResult = DialogResult.OK;
      }
      else
      {
        _workWithForm.SetEditMode(true);

        if (int.TryParse(_invoice.DriverToID, out int driverIdTo))
          DisableChbIsMainForReservDriver(driverIdTo);
      }
    }

    private void cbRegionTo_SelectedIndexChanged(object sender, EventArgs e)
    {
      //changeDataSourceDriverTo();
    }

    private void changeDataSourceDriverTo()
    {
      if (isRegionToNotNull())
      {
        Region region = GetRegion();

        DriverList driverList = DriverList.getInstance();
        cbDriverTo.DataSource = driverList.ToDataTableByRegion(region, _invoice.ID != 0);
        cbDriverTo.DisplayMember = "ФИО";
        cbDriverTo.ValueMember = "id";
      }
    }

    private Region GetRegion()
    {
      int.TryParse(cbRegionTo.SelectedValue.ToString(), out int idRegion);
      return RegionList.getInstance().getItem(idRegion);
    }

    private bool isRegionToNotNull()
    {
      return _load && (cbRegionTo.SelectedValue != null);
    }

    private void cbDriverTo_SelectedIndexChanged(object sender, EventArgs e)
    {
      var driverList = DriverList.getInstance();

      if (cbDriverTo.SelectedValue == null)
        return;

      if (int.TryParse(cbDriverTo.SelectedValue.ToString(), out int driverId))
      {
        DisableChbIsMainForReservDriver(driverId);

        var driver = driverList.getItem(driverId);
        cbRegionTo.SelectedValue = driver.Region.ID;
      }
    }

    private void DisableChbIsMainForReservDriver(int driverId)
    {
      chbMain.Enabled = driverId != Consts.ReserveDriverId;
      if (driverId == 0)
        chbMain.Checked = false;
    }
  }
}
