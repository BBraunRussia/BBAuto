using BBAuto.Domain.ForDriver;
using BBAuto.Domain.Lists;
using System;
using System.Windows.Forms;

namespace BBAuto
{
  public partial class FuelCardDriver_AddEdit : Form
  {
    private readonly FuelCardDriver _fuelCardDriver;

    private WorkWithForm _workWithForm;

    public FuelCardDriver_AddEdit(FuelCardDriver fuelCardDriver)
    {
      InitializeComponent();

      _fuelCardDriver = fuelCardDriver;
    }

    private void FuelCardDriver_AddEdit_Load(object sender, EventArgs e)
    {
      LoadDictionary();

      LoadData();

      _workWithForm = new WorkWithForm(Controls, btnSave, btnClose);
      _workWithForm.SetEditMode(_fuelCardDriver.ID == 0);
    }

    private void LoadDictionary()
    {
      DriverList driverList = DriverList.getInstance();
      cbDriver.DataSource = driverList.ToDataTable(_fuelCardDriver.Driver.ID != 0);
      cbDriver.DisplayMember = "ФИО";
      cbDriver.ValueMember = "id";
    }

    private void LoadData()
    {
      cbDriver.SelectedValue = _fuelCardDriver.Driver.ID;
      dtpDateBegin.Value = _fuelCardDriver.DateBegin;

      chbNotUse.Checked = _fuelCardDriver.IsNotUse;

      if (chbNotUse.Checked)
      {
        dtpDateEnd.Value = _fuelCardDriver.DateEnd ?? new DateTime(1, 1, 1);
      }
    }

    private void chbNotUse_CheckedChanged(object sender, EventArgs e)
    {
      label3.Visible = chbNotUse.Checked;
      dtpDateEnd.Visible = chbNotUse.Checked;
    }

    private void btnSave_Click(object sender, EventArgs e)
    {
      if (_workWithForm.IsEditMode())
      {
        int idDriver;
        int.TryParse(cbDriver.SelectedValue.ToString(), out idDriver);
        _fuelCardDriver.Driver = DriverList.getInstance().getItem(idDriver);
        _fuelCardDriver.DateBegin = dtpDateBegin.Value;

        if (chbNotUse.Checked)
        {
          if ((!_fuelCardDriver.IsNotUse) && (chbNotUse.Checked))
          {
            FuelCard fuelCard = _fuelCardDriver.FuelCard;
            FuelCardDriver fuelCardDriver = fuelCard.CreateFuelCardDriver();
            fuelCardDriver.Save();
          }

          _fuelCardDriver.DateEnd = dtpDateEnd.Value.Date;
        }
        else
          _fuelCardDriver.IsNotUse = false;

        _fuelCardDriver.Save();

        DialogResult = DialogResult.OK;
      }
      else
        _workWithForm.SetEditMode(true);
    }
  }
}
