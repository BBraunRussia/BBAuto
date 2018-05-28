using System;
using System.Windows.Forms;
using BBAuto.App.Events;
using BBAuto.App.FormsForDriver.AddEdit;
using BBAuto.Logic.Common;
using BBAuto.Logic.Dictionary;
using BBAuto.Logic.Services.Car;
using BBAuto.Logic.Services.Driver.DriverCar;
using BBAuto.Logic.Services.Violation;
using BBAuto.Logic.Static;
using Common.Resources;

namespace BBAuto.App.FormsForCar.AddEdit
{
  public partial class ViolationForm : Form, IViolationForm
  {
    private ICarForm _carForm;
    private readonly IViolationService _violationService;
    private readonly ICarService _carService;
    private readonly IDriverCarService _driverCarService;

    private ViolationModel _violation;

    private WorkWithForm _workWithForm;
    
    public ViolationForm(
      IViolationService violationService,
      ICarService carService,
      IDriverCarService driverCarService)
    {
      InitializeComponent();

      _violationService = violationService;
      _carService = carService;
      _driverCarService = driverCarService;
    }

    public DialogResult ShowDialog(int violationId, int carId, ICarForm carForm)
    {
      _violation = _violationService.GetById(violationId) ?? new ViolationModel(carId);
      _carForm = carForm;

      return ShowDialog();
    }

    private void Violation_AddEdit_Load(object sender, EventArgs e)
    {
      FillFields();

      ChangeVisible();

      _workWithForm = new WorkWithForm(Controls, btnSave, btnClose);
      _workWithForm.SetEditMode(_violation.Id == 0);
    }

    private void FillFields()
    {
      dtpDate.Value = _violation.Date ?? DateTime.Today;
      tbNumber.Text = _violation.Number;
      chbPaid.Checked = _violation.DatePay != null;

      var tbFile = ucFile.Controls["tbFile"] as TextBox;
      tbFile.Text = _violation.File;

      cbViolationType.SelectedValue = _violation.ViolationTypeId;
      tbSum.Text = _violation.Sum.ToString();

      var violationType = ViolationTypes.getInstance();
      cbViolationType.DataSource = violationType.ToDataTable();
      cbViolationType.ValueMember = "id";
      cbViolationType.DisplayMember = "Название";

      cbViolationType.SelectedValue = _violation.ViolationTypeId;
      tbSum.Text = _violation.Sum.ToString();

      var tbFilePay = ucFilePay.Controls["tbFile"] as TextBox;
      tbFilePay.Text = _violation.FilePay;

      chbNoDeduction.Checked = _violation.NoDeduction;

      var driver = _driverCarService.GetDriver(_violation.CarId, _violation.Date);
      var car = _carService.GetCarById(_violation.CarId);

      llDriver.Text = driver?.GetName(NameType.Full);
      llCar.Text = car?.ToString();
    }

    private void btnSave_Click(object sender, EventArgs e)
    {
      if (_workWithForm.IsEditMode())
      {
        TrySave();
        DialogResult = DialogResult.OK;
      }
      else
        _workWithForm.SetEditMode(true);
    }

    private void TrySave()
    {
      try
      {
        Save();
      }
      catch (NullReferenceException)
      {
        MessageBox.Show("Для сохранения выберите тип нарушения", "Не возможно сохранить", MessageBoxButtons.OK,
          MessageBoxIcon.Warning);
      }
    }

    private void Save()
    {
      _violation.Date = dtpDate.Value.Date;
      _violation.Number = tbNumber.Text;
      if (chbPaid.Checked)
        _violation.DatePay = dtpDatePaid.Value.Date;
      else
        _violation.DatePay = null;

      var tbFile = ucFile.Controls["tbFile"] as TextBox;
      _violation.File = tbFile.Text;

      int.TryParse(cbViolationType.SelectedValue.ToString(), out var violationTypeId);
      _violation.ViolationTypeId = violationTypeId;
      int.TryParse(tbSum.Text, out var sum);
      _violation.Sum = sum;

      var tbFilePay = ucFilePay.Controls["tbFile"] as TextBox;
      _violation.FilePay = tbFilePay.Text;

      _violation.NoDeduction = chbNoDeduction.Checked;

      _violationService.Save(_violation);
    }

    private void chbPaid_CheckedChanged(object sender, EventArgs e)
    {
      ChangeVisible();
    }

    private void ChangeVisible()
    {
      labelDatePaid.Visible = chbPaid.Checked;
      dtpDatePaid.Visible = chbPaid.Checked;

      labelFilePay.Visible = chbPaid.Checked;
      ucFilePay.Visible = chbPaid.Checked;
    }


    private void btnSend_Click(object sender, EventArgs e)
    {
      TrySave();

      if (TrySend())
      {
        _violation.Sent = true;
        TrySave();

        DialogResult = DialogResult.OK;
        Close();
      }
    }

    private bool TrySend()
    {
      try
      {
        Send();
        return true;
      }
      catch (TimeoutException)
      {
        MessageBox.Show(Messages.NoAnswerFromMailServer, Captions.TimeoutExpired, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        return false;
      }
    }

    private void Send()
    {
      var car = _carService.GetCarById(_violation.CarId);
      var driver = _driverCarService.GetDriver(_violation.CarId, _violation.Date);

      var mail = new EMail();
      mail.SendMailViolation(_violation, car, driver);
    }

    private void llDriver_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
      var driver = _driverCarService.GetDriver(_violation.CarId, _violation.Date);
      var driverAe = new DriverForm(driver);
      driverAe.ShowDialog();
    }

    private void llCar_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    { 
      _carForm.ShowDialog(_violation.CarId);
    }
  }
}
