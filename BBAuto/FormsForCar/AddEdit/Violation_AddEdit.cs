using System;
using System.Windows.Forms;
using BBAuto.Domain.ForCar;
using BBAuto.Domain.Dictionary;
using BBAuto.Domain.Static;
using BBAuto.Domain.Services.Mail;

namespace BBAuto
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

      ChangeVisible();

      _workWithForm = new WorkWithForm(this.Controls, btnSave, btnClose);
      _workWithForm.SetEditMode(_violation.ID == 0);
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
      {
        _violation.DatePay = dtpDatePaid.Value.Date;
        var tbFilePay = ucFilePay.Controls["tbFile"] as TextBox;
        _violation.FilePay = tbFilePay.Text;
      }
      else
      {
        _violation.DatePay = null;
        _violation.FilePay = null;
      }

      TextBox tbFile = ucFile.Controls["tbFile"] as TextBox;
      _violation.File = tbFile.Text;

      _violation.IDViolationType = cbViolationType.SelectedValue.ToString();
      _violation.Sum = tbSum.Text;
      
      _violation.NoDeduction = chbNoDeduction.Checked;

      _violation.Save();
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
        MessageBox.Show(
          "Не удалось отправить письмо. Нет ответа от сервера. Попытайтесь отправить через некоторое время",
          "Время ожидания истекло", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        return false;
      }
    }

    private void Send()
    {
      IMailService mailService = new MailService();
      mailService.SendMailViolation(_violation);
    }

    private void llDriver_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
      var driverAE = new Driver_AddEdit(_violation.getDriver());
      driverAE.ShowDialog();
    }

    private void llCar_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
      var carAE = new CarForm(_violation.Car);
      carAE.ShowDialog();
    }
  }
}
