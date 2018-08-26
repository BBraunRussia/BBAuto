using System;
using System.Linq;
using System.Windows.Forms;
using BBAuto.Domain.Common;
using BBAuto.Domain.Entities;
using BBAuto.Domain.ForCar;
using BBAuto.Domain.ForDriver;
using BBAuto.Domain.Lists;
using BBAuto.Domain.Services.Documents;
using BBAuto.Domain.Services.DriverInstruction;
using BBAuto.Domain.Static;
using BBAuto.Domain.Tables;

namespace BBAuto
{
  public partial class Driver_AddEdit : Form
  {
    private Driver _driver;
    private Ldap ldap = new Ldap();

    private WorkWithForm _workWithForm;

    public Driver_AddEdit(Driver driver)
    {
      InitializeComponent();

      _driver = driver;
    }

    private void Driver_AddEdit_Load(object sender, EventArgs e)
    {
      loadData();

      tbNumber.Visible = lbNumber.Visible = (_driver.OwnerID < 3);

      _workWithForm = new WorkWithForm(this.Controls, btnSave, btnClose);
      _workWithForm.EditModeChanged += SetEnable;
      _workWithForm.SetEditMode(_driver.ID == 0);
    }

    private void loadData()
    {
      loadDictionary(cbRegion, "Region");

      fillCommonFields();
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
        if (_driver.From1C)
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

        tbNumber.ReadOnly = _driver.From1C;
      }
    }

    private void loadDictionary(ComboBox combo, string name)
    {
      combo.DataSource = OneStringDictionary.getDataTable(name);
      combo.ValueMember = name + "_id";
      combo.DisplayMember = "Название";
    }

    private void fillCommonFields()
    {
      tbFio.Text = _driver.GetName(NameType.Full);
      mtbMobile.Text = _driver.Mobile;
      tbEmail.Text = _driver.Email;
      mtbDateBirth.Text = _driver.DateBirth;
      if (_driver.Region != null)
        cbRegion.SelectedValue = _driver.Region.ID;
      tbCompany.Text = _driver.CompanyName;
      chbFired.Checked = _driver.Fired;
      tbPosition.Text = _driver.Position;
      tbExpSince.Text = _driver.ExpSince;
      tbDept.Text = _driver.Dept;
      tbLogin.Text = _driver.Login;
      tbSuppyAddress.Text = _driver.SuppyAddress;

      rbMan.Checked = (_driver.SexIndex == 0);
      rbWoman.Checked = (_driver.SexIndex == 1);

      chbDecret.Checked = _driver.Decret;
      chbNotificationStop.Checked = _driver.NotificationStop;
      if (_driver.NotificationStop)
        dtpStopNotificationDate.Value = _driver.DateStopNotification;
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
      DriverCarList driverCarList = DriverCarList.getInstance();
      Car car = driverCarList.GetCar(_driver);

      if (car != null)
        carInfo.Text = car.ToString();
    }

    private void FillInstraction()
    {
      IDriverInstructionService driverInstructionService = new DriverInstructionService();
      var driverInstructions = driverInstructionService.GetDriverInstructionsByDriverId(_driver.ID);

      if (driverInstructions.Any())
      {
        var driverInstruction = driverInstructions.First();

        IDocumentsService documentsService = new DocumentsService();
        var document = documentsService.GetDocumentById(driverInstruction.DocumentId);
      
        instractionInfo.Text = $"{document.Name} дата {driverInstruction.Date.ToShortDateString()}";
      }
      else
      {
        instractionInfo.Text = "нет данных";
      }
    }

    private void FillMedicalCert()
    {
      MedicalCertList medicalCertList = MedicalCertList.getInstance();
      MedicalCert medicalCert = medicalCertList.getItem(_driver);

      if (medicalCert != null)
        medicalCertInfo.Text = medicalCert.ToString();
    }

    private void FillDriverLicense()
    {
      LicenseList licencesList = LicenseList.getInstance();
      DriverLicense driverLicense = licencesList.getItem(_driver);

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
      DTP dtp = dtpList.GetLastByDriver(_driver);

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
        var instList = new formInstractionList(_driver);
        if (instList.ShowDialog() == DialogResult.OK)
          FillInstraction();
      }
    }

    private void medicalCertLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
      if (trySave())
      {
        formMedicalCertList mcList = new formMedicalCertList(_driver);
        if (mcList.ShowDialog() == DialogResult.OK)
          FillMedicalCert();
      }
    }

    private void licenceLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
      if (trySave())
      {
        formLicenseList licList = new formLicenseList(_driver);
        if (licList.ShowDialog() == DialogResult.OK)
          FillDriverLicense();
      }
    }

    private void linkPassport_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
      if (trySave())
      {
        formPassportList passList = new formPassportList(_driver);
        if (passList.ShowDialog() == DialogResult.OK)
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
      if (string.IsNullOrEmpty(_driver.Number) && (!_driver.From1C) && (_driver.OwnerID < 3) &&
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
      _driver.Save();
    }

    private void CopyFields()
    {
      if (!_driver.From1C)
      {
        _driver.Fio = tbFio.Text;

        int.TryParse(cbRegion.SelectedValue.ToString(), out var idRegion);
        RegionList regionList = RegionList.getInstance();
        Region region = regionList.getItem(idRegion);

        _driver.Region = region;
        _driver.CompanyName = tbCompany.Text;
        _driver.Position = tbPosition.Text;
        _driver.Dept = tbDept.Text;
        _driver.SexIndex = (rbMan.Checked) ? 0 : 1;
        _driver.Fired = chbFired.Checked;
        _driver.Decret = chbDecret.Checked;
        _driver.DateBirth = mtbDateBirth.Text;
        _driver.Login = tbLogin.Text;
        _driver.Number = tbNumber.Text;
      }

      _driver.Email = tbEmail.Text;
      _driver.ExpSince = tbExpSince.Text;
      _driver.Mobile = mtbMobile.Text;
      _driver.SuppyAddress = tbSuppyAddress.Text;
      _driver.DateStopNotification =
        (chbNotificationStop.Checked) ? dtpStopNotificationDate.Value : new DateTime(1, 1, 1);

      _driver.IsDriver = true;
    }

    private void carLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
      formDriverCar formDriverCar = new formDriverCar(_driver);
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
        tbEmail.Text = ldap.GetEmail(tbLogin.Text);
        Save();
      }
    }

    private void chbNotificationStop_CheckedChanged(object sender, EventArgs e)
    {
      dtpStopNotificationDate.Visible = chbNotificationStop.Checked;
    }
  }
}
