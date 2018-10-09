using System;
using System.Windows.Forms;
using BBAuto.Domain.Lists;
using BBAuto.Domain.Services.DriverTransponder;

namespace BBAuto.AddEdit
{
  public partial class DriverTransponderForm : Form
  {
    private readonly DriverTransponder _driverTransponder;

    private readonly IDriverTransponderService _driverTransponderService;

    private WorkWithForm _workWithForm;

    public DriverTransponderForm(
      DriverTransponder driverTransponder,
      IDriverTransponderService driverTransponderService)
    {
      InitializeComponent();

      _driverTransponder = driverTransponder;
      _driverTransponderService = driverTransponderService;
    }

    private void DriverTransponderForm_Load(object sender, EventArgs e)
    {
      LoadDictionary();

      LoadData();

      _workWithForm = new WorkWithForm(Controls, btnSave, btnClose);
      _workWithForm.SetEditMode(_driverTransponder.Id == 0);
    }

    private void LoadDictionary()
    {
      DriverList driverList = DriverList.getInstance();
      cbDriver.DataSource = driverList.ToDataTable();
      cbDriver.DisplayMember = "ФИО";
      cbDriver.ValueMember = "id";
    }

    private void LoadData()
    {
      cbDriver.SelectedValue = _driverTransponder.DriverId;
      dtpDateBegin.Value = _driverTransponder.DateBegin ?? DateTime.Today;

      chbNotUse.Checked = _driverTransponder.DateEnd.HasValue;

      if (_driverTransponder.DateEnd.HasValue)
      {
        dtpDateEnd.Value = _driverTransponder.DateEnd ?? DateTime.Today;
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
        int.TryParse(cbDriver.SelectedValue.ToString(), out int driverId);
        _driverTransponder.DriverId = driverId;
        _driverTransponder.DateBegin = dtpDateBegin.Value;

        if (chbNotUse.Checked)
        {
          if (!_driverTransponder.DateEnd.HasValue)
          {
            _driverTransponderService.Save(new DriverTransponder
            {
              TransponderId = _driverTransponder.TransponderId,
              DateBegin = dtpDateEnd.Value.Date
            });
          }

          _driverTransponder.DateEnd = dtpDateEnd.Value.Date;
        }
        else
          _driverTransponder.DateEnd = null;

        _driverTransponderService.Save(_driverTransponder);

        DialogResult = DialogResult.OK;
      }
      else
        _workWithForm.SetEditMode(true);
    }
  }
}
