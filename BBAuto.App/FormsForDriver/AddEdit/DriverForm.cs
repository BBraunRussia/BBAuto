using System;
using System.Windows.Forms;
using BBAuto.App.Events;
using BBAuto.Logic.Common;
using BBAuto.Logic.ForCar;
using BBAuto.Logic.ForDriver;
using BBAuto.Logic.Lists;
using BBAuto.Logic.Services.Dept;
using BBAuto.Logic.Services.Driver;
using BBAuto.Logic.Services.Driver.DriverCar;
using BBAuto.Logic.Services.Position;
using BBAuto.Logic.Services.Violation;
using BBAuto.Logic.Static;
using BBAuto.Logic.Tables;

namespace BBAuto.App.FormsForDriver.AddEdit
{
  public partial class DriverForm : Form, IDriverForm
  {
    private DriverModel _driver;
    private readonly Ldap _ldap = new Ldap();

    private WorkWithForm _workWithForm;

    private readonly IDeptService _deptService;
    private readonly IPositionService _positionService;
    private readonly IDriverCarService _driverCarService;
    private readonly IViolationService _violationService;
    private readonly IDriverService _driverService;

    public DriverForm(
      IDeptService deptService,
      IPositionService positionService,
      IDriverCarService driverCarService,
      IViolationService violationService,
      IDriverService driverService)
    {
      InitializeComponent();

      _deptService = deptService;
      _positionService = positionService;
      _driverCarService = driverCarService;
      _violationService = violationService;
      _driverService = driverService;
    }

    public DialogResult ShowDialog(DriverModel driver)
    {
      _driver = driver;

      return ShowDialog();
    }

    private void Driver_AddEdit_Load(object sender, EventArgs e)
    {
      LoadData();

      tbNumber.Visible = lbNumber.Visible = (_driver.OwnerId < 3);

      _workWithForm = new WorkWithForm(this.Controls, btnSave, btnClose);
      _workWithForm.EditModeChanged += SetEnable;
      _workWithForm.SetEditMode(_driver.Id == 0);
    }

    private void LoadData()
    {
      loadDictionary(cbRegion, "Region");
      
      FillCommonFields();
      fillExtraFields();
    }

    private void SetEnable(Object sender, EditModeEventArgs e)
    {
      /*TODO: для Столяровй сделать видимой инфу про водителя*/
      //if (User.GetRole() == RolesList.AccountantWayBill)
      //{
      //    this.Size = new System.Drawing.Size(410, 486);
      //    _workWithForm.SetEnableValue(btnSave, true);
      //}

      if (_workWithForm.IsEditMode())
      {
        if (_driver.From1C ?? false)
        {
          tbCompany.ReadOnly = true;

          rbMan.Enabled = false;
          rbWoman.Enabled = false;

          tbFio.ReadOnly = true;
          cbRegion.Enabled = false;
          tbDept.ReadOnly = true;
          tbPosition.ReadOnly = true;
          chbDecret.Enabled = false;
          chbFired.Enabled = false;

          mtbDateBirth.ReadOnly = true;
          tbLogin.ReadOnly = true;
        }

        tbNumber.ReadOnly = _driver.From1C ?? false;
      }
    }

    private void loadDictionary(ComboBox combo, string name)
    {
      combo.DataSource = OneStringDictionary.getDataTable(name);
      combo.ValueMember = name + "_id";
      combo.DisplayMember = "Название";
    }

    private void FillCommonFields()
    {
      tbFio.Text = _driver.GetName(NameType.Full);
      mtbMobile.Text = _driver.Mobile;
      tbEmail.Text = _driver.Email;
      mtbDateBirth.Text = _driver.DateBirth?.ToShortDateString();
      if (_driver.RegionId != 0)
        cbRegion.SelectedValue = _driver.RegionId;
      tbCompany.Text = _driver.CompanyName;
      chbFired.Checked = _driver.Fired ?? false;
      tbPosition.Text = _driver.Position;
      tbExpSince.Text = _driver.ExpSince;
      tbDept.Text = _driver.Dept;
      tbLogin.Text = _driver.Login;
      tbSuppyAddress.Text = _driver.suppyAddress;

      rbMan.Checked = _driver.SexString == "мужской";
      rbWoman.Checked = _driver.SexString == "женский";

      chbDecret.Checked = _driver.Decret ?? false;
      chbNotificationStop.Checked = _driver.DateStopNotification.HasValue;
      if (_driver.DateStopNotification.HasValue)
        dtpStopNotificationDate.Value = _driver.DateStopNotification.Value;
      tbNumber.Text = _driver.Number;
    }

    private void fillExtraFields()
    {
      FillCar();
      FillInstraction();
      FillMedicalCert();
      FillDriverLicense();
      FillPassport();
      FillDTP();
      FillViolation();
      FillFuelCardDriver();
    }

    private void FillCar()
    {
      var car = _driverCarService.GetCar(_driver.Id);

      if (car != null)
        carInfo.Text = car.ToString();
    }

    private void FillInstraction()
    {
      InstractionList instractionList = InstractionList.getInstance();
      Instraction instraction = instractionList.getItemByDriverId(_driver.Id);

      if (instraction != null)
        instractionInfo.Text = instraction.ToString();
    }

    private void FillMedicalCert()
    {
      MedicalCertList medicalCertList = MedicalCertList.getInstance();
      MedicalCert medicalCert = medicalCertList.getItemByDriverId(_driver.Id);

      if (medicalCert != null)
        medicalCertInfo.Text = medicalCert.ToString();
    }

    private void FillDriverLicense()
    {
      LicenseList licencesList = LicenseList.getInstance();
      DriverLicense driverLicense = licencesList.getItemByDriverId(_driver.Id);

      if (driverLicense != null)
        licenceInfo.Text = driverLicense.ToString();
    }

    private void FillPassport()
    {
      PassportList passportList = PassportList.getInstance();
      Passport passport = passportList.getLastPassport(_driver);

      if (passport != null)
        labelPassport.Text = passport.ToString();
    }

    private void FillDTP()
    {
      DTPList dtpList = DTPList.getInstance();
      DTP dtp = dtpList.getLastByDriver(_driver);

      if (dtp != null)
        dtpInfo.Text = dtp.ToString();
    }

    private void FillViolation()
    {
      ViolationList violationList = ViolationList.getInstance();
      Violation violation = violationList.getItem(_driver);

      if (violation != null)
        ViolationInfo.Text = violation.ToString();
    }

    private void FillFuelCardDriver()
    {
      FuelCardDriverList fuelCardDriverList = FuelCardDriverList.getInstance();

      FuelCardDriver fuelCardDriver = fuelCardDriverList.getItemFirst(_driver);

      if (fuelCardDriver != null)
        lbFuelCard1.Text = fuelCardDriver.ToString();

      fuelCardDriver = fuelCardDriverList.getItemSecond(_driver);

      if (fuelCardDriver != null)
        lbFuelCard2.Text = fuelCardDriver.ToString();
    }

    private void save_Click(object sender, EventArgs e)
    {
      if (User.GetRole() == RolesList.AccountantWayBill)
      {
        if (btnSave.Text == "Редактировать")
        {
          _workWithForm.SetEnableValue(tbSuppyAddress, true);
          btnSave.Text = "Сохранить";
        }
        else
        {
          if (trySave())
            DialogResult = System.Windows.Forms.DialogResult.OK;
        }
      }
      else
      {
        if (_workWithForm.IsEditMode())
        {
          if (trySave())
            DialogResult = System.Windows.Forms.DialogResult.OK;
        }
        else
        {
          _workWithForm.SetEditMode(true);
        }
      }
    }

    private void instractionLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
      if (trySave())
      {
        formInstractionList instList = new formInstractionList(_driver);
        if (instList.ShowDialog() == System.Windows.Forms.DialogResult.OK)
          FillInstraction();
      }
    }

    private void medicalCertLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
      if (trySave())
      {
        formMedicalCertList mcList = new formMedicalCertList(_driver);
        if (mcList.ShowDialog() == System.Windows.Forms.DialogResult.OK)
          FillMedicalCert();
      }
    }

    private void licenceLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
      if (trySave())
      {
        formLicenseList licList = new formLicenseList(_driver);
        if (licList.ShowDialog() == System.Windows.Forms.DialogResult.OK)
          FillDriverLicense();
      }
    }

    private void linkPassport_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
      if (trySave())
      {
        formPassportList passList = new formPassportList(_driver);
        if (passList.ShowDialog() == System.Windows.Forms.DialogResult.OK)
          FillPassport();
      }
    }

    private bool trySave()
    {
      try
      {
        Save();
        return true;
      }
      catch (NullReferenceException)
      {
        MessageBox.Show("Для продолжения заполните поля формы \"Водитель\"", "Информация", MessageBoxButtons.OK,
          MessageBoxIcon.Information);
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }

      return false;
    }

    private void Save()
    {
      if (string.IsNullOrEmpty(_driver.Number) && (!_driver.From1C) && (_driver.OwnerId < 3) &&
          (!string.IsNullOrEmpty(tbNumber.Text)))
      {
        DriverList driverList = DriverList.getInstance();
        if (!driverList.IsUniqueNumber(tbNumber.Text))
        {
          MessageBox.Show("Сохранение невозможно. Сотрудник с таким номером уже есть.", "Сохранение",
            MessageBoxButtons.OK, MessageBoxIcon.Error);
          return;
        }
      }

      CopyFields();
      _driverService.Save(_driver);
    }

    private void CopyFields()
    {
      if (!_driver.From1C)
      {
        _driver.Fio = tbFio.Text;

        int.TryParse(cbRegion.SelectedValue.ToString(), out int regionId);
        _driver.RegionId = regionId;

        _driver.CompanyName = tbCompany.Text;
        _driver.Position = tbPosition.Text;
        _driver.Dept = tbDept.Text;
        _driver.Sex = rbMan.Checked ? 0 : 1;
        _driver.Fired = chbFired.Checked;
        _driver.Decret = chbDecret.Checked;
        DateTime.TryParse(mtbDateBirth.Text, out var dateBirth);
        _driver.DateBirth = dateBirth;
        _driver.Login = tbLogin.Text;
        _driver.Number = tbNumber.Text;
      }

      _driver.Email = tbEmail.Text;
      int.TryParse(tbExpSince.Text, out var expSince);
      _driver.ExpSince = expSince;
      _driver.Mobile = mtbMobile.Text;
      _driver.SuppyAddress = tbSuppyAddress.Text;
      _driver.DateStopNotification = (chbNotificationStop.Checked)
        ? dtpStopNotificationDate.Value
        : new DateTime(1, 1, 1);

      _driver.IsDriver = true;
    }

    private void carLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
      DriverCarForm formDriverCar = new DriverCarForm(_driver);
      formDriverCar.ShowDialog();
    }

    private void dtpLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
      formDTPList dtpList = new formDTPList(_driver);
      dtpList.ShowDialog();
    }

    private void violationLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
      formViolationList vList = new formViolationList(_driver);
      vList.ShowDialog();
    }

    private void linkFuelCard_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
      formFuelCardDriver fFuelCardDriver = new formFuelCardDriver(_driver);
      fFuelCardDriver.ShowDialog();
    }

    private void tbLogin_TextChanged(object sender, EventArgs e)
    {
      if (tbEmail.Text == string.Empty)
      {
        tbEmail.Text = _ldap.GetEmail(tbLogin.Text);
        Save();
      }
    }

    private void chbNotificationStop_CheckedChanged(object sender, EventArgs e)
    {
      dtpStopNotificationDate.Visible = chbNotificationStop.Checked;
    }
  }
}
