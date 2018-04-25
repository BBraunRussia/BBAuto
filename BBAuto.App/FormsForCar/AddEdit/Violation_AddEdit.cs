using System;
using System.Configuration;
using System.Windows.Forms;
using BBAuto.App.config;
using BBAuto.App.Events;
using BBAuto.App.FormsForDriver.AddEdit;
using BBAuto.Logic.Common;
using BBAuto.Logic.Dictionary;
using BBAuto.Logic.ForCar;
using BBAuto.Logic.Services.Dealer;
using BBAuto.Logic.Static;
using BBAuto.Repositories;
using Castle.Windsor;

namespace BBAuto.App.FormsForCar.AddEdit
{
  public partial class Violation_AddEdit : Form
  {
    private readonly Violation _violation;

    private WorkWithForm _workWithForm;

    public Violation_AddEdit(Violation violation)
    {
      InitializeComponent();

      _violation = violation;
    }

    private void Violation_AddEdit_Load(object sender, EventArgs e)
    {
      fillFields();

      changeVisible();

      _workWithForm = new WorkWithForm(this.Controls, btnSave, btnClose);
      _workWithForm.SetEditMode(_violation.Id == 0);
    }

    private void fillFields()
    {
      dtpDate.Value = _violation.Date;
      tbNumber.Text = _violation.Number;
      chbPaid.Checked = (_violation.DatePay != null);

      TextBox tbFile = ucFile.Controls["tbFile"] as TextBox;
      tbFile.Text = _violation.File;

      cbViolationType.SelectedValue = _violation.IDViolationType;
      tbSum.Text = _violation.Sum;

      ViolationTypes violationType = ViolationTypes.getInstance();
      cbViolationType.DataSource = violationType.ToDataTable();
      cbViolationType.ValueMember = "id";
      cbViolationType.DisplayMember = "Название";

      cbViolationType.SelectedValue = _violation.IDViolationType;
      tbSum.Text = _violation.Sum;

      TextBox tbFilePay = ucFilePay.Controls["tbFile"] as TextBox;
      tbFilePay.Text = _violation.FilePay;

      chbNoDeduction.Checked = _violation.NoDeduction;

      llDriver.Text = _violation.getDriver().GetName(NameType.Full);
      llCar.Text = _violation.Car.ToString();
    }

    private void btnSave_Click(object sender, EventArgs e)
    {
      if (_workWithForm.IsEditMode())
      {
        TrySave();
        this.DialogResult = System.Windows.Forms.DialogResult.OK;
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

      TextBox tbFile = ucFile.Controls["tbFile"] as TextBox;
      _violation.File = tbFile.Text;

      _violation.IDViolationType = cbViolationType.SelectedValue.ToString();
      _violation.Sum = tbSum.Text;

      TextBox tbFilePay = ucFilePay.Controls["tbFile"] as TextBox;
      _violation.FilePay = tbFilePay.Text;

      _violation.NoDeduction = chbNoDeduction.Checked;

      _violation.Save();
    }

    private void chbPaid_CheckedChanged(object sender, EventArgs e)
    {
      changeVisible();
    }

    private void changeVisible()
    {
      labelDatePaid.Visible = chbPaid.Checked;
      dtpDatePaid.Visible = chbPaid.Checked;

      labelFilePay.Visible = chbPaid.Checked;
      ucFilePay.Visible = chbPaid.Checked;
    }


    private void btnSend_Click(object sender, EventArgs e)
    {
      TrySave();

      if (trySend())
      {
        _violation.Sent = true;
        TrySave();

        this.DialogResult = System.Windows.Forms.DialogResult.OK;
        Close();
      }
    }

    private bool trySend()
    {
      try
      {
        Send();
        return true;
      }
      catch (TimeoutException)
      {
        MessageBox.Show(
          "Не удалось отправить письмо. Нет ответа от сервера. Попытайтесь отправить через некоторое время",
          "Время ожидания истекло", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        return false;
      }
    }

    private void Send()
    {
      var mail = new EMail();
      mail.SendMailViolation(_violation);
    }

    private void llDriver_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
      DriverForm driverAE = new DriverForm(_violation.getDriver());
      driverAE.ShowDialog();
    }

    private void llCar_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
      var container = WindsorConfiguration.Container;

      var carForm = new CarForm(container.Resolve<IDealerService>());
      carForm.ShowDialog(_violation.Car);
    }
  }
}
