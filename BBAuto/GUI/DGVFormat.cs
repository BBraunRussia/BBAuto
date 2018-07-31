using BBAuto.Domain.Entities;
using BBAuto.Domain.ForCar;
using BBAuto.Domain.ForDriver;
using BBAuto.Domain.Lists;
using BBAuto.Domain.Static;
using System;
using System.Drawing;
using System.Windows.Forms;
using Common;

namespace BBAuto
{
  internal class DGVFormat
  {
    private DataGridView _dgv;

    internal DGVFormat(DataGridView dgv)
    {
      _dgv = dgv;
    }

    internal void FormatByOwner()
    {
      CarList carList = CarList.GetInstance();

      foreach (DataGridViewRow row in _dgv.Rows)
      {
        int.TryParse(row.Cells[1].Value.ToString(), out int id);
        Car car = carList.getItem(id);

        if (car == null)
          return;

        if (car.info.Owner == "ООО \"ГЕМАТЕК\"")
          row.DefaultCellStyle.BackColor = BBColors.bbGray5;
      }
    }

    internal void HideTwoFirstColumns()
    {
      HideColumn(0);
      HideColumn(1);
    }

    internal void Format(Status status)
    {
      switch (status)
      {
        case Status.Actual:
          SetFormatActual();
          break;
        case Status.Buy:
          SetFormatBuy();
          break;
        case Status.Invoice:
          SetFormatInvoice();
          break;
        case Status.Policy:
          SetFormatPolicy();
          break;
        case Status.DTP:
          SetFormatDTP();
          break;
        case Status.Violation:
          SetFormatViolation();
          break;
        case Status.DiagCard:
          SetFormatDiagCard();
          break;
        case Status.Account:
          SetFormatAccount();
          break;
        case Status.FuelCard:
          SetFormatFuelCard();
          break;
        case Status.Driver:
          SetFormatDriver();
          break;
        case Status.Transponder:
          SetFormatTransponder();
          break;
      }
    }

    private void SetFormatActual()
    {
      SetRightAligment("Пробег");
      SetCellFormat("Пробег", "N0");
      SetCellFormat("Дата последней записи о пробеге", "dd.MM.yyyy");
    }

    private void SetFormatBuy()
    {
      HideColumn("№ ПТС");
      HideColumn("№ СТС");
    }

    private void SetFormatInvoice()
    {
      InvoiceList invoiceList = InvoiceList.getInstance();

      foreach (DataGridViewRow row in _dgv.Rows)
      {
        int.TryParse(row.Cells[0].Value.ToString(), out int id);

        var invoice = invoiceList.getItem(id);

        if (invoice.DateMove != null)
          row.Cells["Дата передачи"].Style.BackColor = Color.MediumPurple;


        if (invoice.File != string.Empty)
          row.Cells["№ накладной"].Style.BackColor = Color.MediumPurple;
      }
    }

    private void SetFormatPolicy()
    {
      SetColumnHeaderText("Pay", "Оплата, руб");
      SetColumnHeaderText("LimitCost", "Страховая стоимость/ Лимит ответственности, руб");
      SetColumnHeaderText("Pay2", "Оплата (2 платеж), руб");

      SetCellFormat("Pay", "N2");
      SetCellFormat("LimitCost", "N0");
      SetCellFormat("Pay2", "N2");

      SetRightAligment("Pay");
      SetRightAligment("LimitCost");
      SetRightAligment("Pay2");

      PolicyList policyList = PolicyList.getInstance();

      foreach (DataGridViewRow row in _dgv.Rows)
      {
        int id;
        int.TryParse(row.Cells[0].Value.ToString(), out id);

        Policy policy = policyList.getItem(id);

        if (policy.File != string.Empty)
          row.Cells["Номер полиса"].Style.BackColor = Color.MediumPurple;

        if ((policy.Type == PolicyType.КАСКО) && (policy.IsAgreed(2)))
          row.Cells["Pay2"].Style.BackColor = Color.MediumPurple;

        if (policy.IsAgreed(1))
          row.Cells["Pay"].Style.BackColor = Color.MediumPurple;

        if (policy.DateEnd < DateTime.Today)
          row.DefaultCellStyle.BackColor = BBColors.bbGray5;

        if (policy.File != string.Empty)
          row.Cells["Тип полиса"].Style.BackColor = BBColors.bbGreen3;
      }
    }

    private void SetFormatDTP()
    {
      SetCellFormat("Сумма возмещения", "N2");

      DTPList dtpList = DTPList.getInstance();

      foreach (DataGridViewRow row in _dgv.Rows)
      {
        int id;
        int.TryParse(row.Cells[0].Value.ToString(), out id);

        DTP dtp = dtpList.getItem(id);

        if (dtp.GetCurrentStatusAfterDTP() == "Отремонтирован")
        {
          row.Cells["Текущее состояние"].Style.BackColor = BBColors.bbGreen3;

          if (row.Cells["Сумма возмещения"].Value.ToString() == "0")
            row.Cells["Сумма возмещения"].Style.BackColor = Color.MediumPurple;
        }
        else
          row.Cells["Текущее состояние"].Style.BackColor = Color.White;
      }
    }

    private void SetFormatViolation()
    {
      SetCellFormat("Сумма штрафа", "N0");
      SetRightAligment("Сумма штрафа");

      ViolationList violationList = ViolationList.getInstance();

      foreach (DataGridViewRow row in _dgv.Rows)
      {
        int id;
        int.TryParse(row.Cells[0].Value.ToString(), out id);

        Violation violation = violationList.getItem(id);

        if (violation.Sent)
          row.Cells["№ постановления"].Style.BackColor = Color.MediumPurple;

        if (violation.FilePay != string.Empty)
          row.Cells["Дата оплаты"].Style.BackColor = Color.MediumPurple;
      }
    }

    private void SetFormatDiagCard()
    {
      DiagCardList diagCardList = DiagCardList.getInstance();

      foreach (DataGridViewRow row in _dgv.Rows)
      {
        int id;
        int.TryParse(row.Cells[0].Value.ToString(), out id);
        DiagCard diagCard = diagCardList.getItem(id);

        if (diagCard.File != string.Empty)
          row.Cells["№ ДК"].Style.BackColor = Color.MediumPurple;
      }
    }

    private void SetFormatAccount()
    {
      SetRightAligment("Сумма");
      SetCellFormat("Сумма", "N2");

      AccountList accountList = AccountList.getInstance();

      foreach (DataGridViewRow row in _dgv.Rows)
      {
        int id;
        int.TryParse(row.Cells[0].Value.ToString(), out id);

        Account account = accountList.getItem(id);

        if (account.Agreed)
          row.Cells["Согласование"].Style.BackColor = Color.MediumPurple;
      }
    }

    private void SetFormatFuelCard()
    {
      HideColumn("Начало использования");
      HideColumn("Окончание использования");

      FuelCardList fuelCardList = FuelCardList.getInstance();

      foreach (DataGridViewRow row in _dgv.Rows)
      {
        int id;
        int.TryParse(row.Cells[1].Value.ToString(), out id);

        FuelCard fuelCard = fuelCardList.getItem(id);

        if (fuelCard.IsVoid)
          row.DefaultCellStyle.BackColor = BBColors.bbGray4;
        else if (row.Cells["Водитель"].Value.ToString() == "(Резерв)")
          row.DefaultCellStyle.BackColor = BBColors.bbGreen3;
        else
          row.DefaultCellStyle.BackColor = Color.White;
      }
    }

    private void SetFormatTransponder()
    {
      HideColumn("Lost");

      foreach (DataGridViewRow row in _dgv.Rows)
      {
        bool.TryParse(row.Cells["Lost"].Value.ToString(), out bool lost);

        int.TryParse(row.Cells["DriverId"].Value.ToString(), out int driverId);

        if (lost)
          row.DefaultCellStyle.BackColor = BBColors.bbGray4;
        else if (driverId == Consts.ReserveDriverId)
          row.DefaultCellStyle.BackColor = BBColors.bbGreen3;
        else
          row.DefaultCellStyle.BackColor = Color.White;
      }
    }

    private void SetFormatDriver()
    {
      var driverList = DriverList.getInstance();

      foreach (DataGridViewRow row in _dgv.Rows)
      {
        if ((row.Cells["Скан водительского удостоверения"].Value.ToString() == "нет")
            || (row.Cells["Скан медицинской справки"].Value.ToString() == "нет"))
        {
          row.DefaultCellStyle.BackColor = Color.LightYellow;
        }

        int idDriver = 0;
        int.TryParse(row.Cells[0].Value.ToString(), out idDriver);
        Driver driver = driverList.getItem(idDriver);

        if (driver.Fired)
          row.DefaultCellStyle.ForeColor = Color.Red;

        if (((driver.OwnerID < 3) && (string.IsNullOrEmpty(driver.Number))) || (driver.Decret))
          row.DefaultCellStyle.ForeColor = Color.Blue;

        if (driver.OwnerID > 2)
          row.DefaultCellStyle.ForeColor = BBColors.bbGreen1;
      }
    }

    internal void SetFormatMileage()
    {
      SetCellFormat("Пробег", "N0");
      SetRightAligment("Пробег");
    }

    internal void SetFormatRepair()
    {
      SetCellFormat("Стоимость", "N2");
      SetRightAligment("Стоимость");
    }

    internal void HideColumn(int index)
    {
      _dgv.Columns[index].Visible = false;
    }

    internal void HideColumn(string columnName)
    {
      _dgv.Columns[columnName].Visible = false;
    }

    private void SetRightAligment(string columnName)
    {
      _dgv.Columns[columnName].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
    }

    private void SetColumnHeaderText(string columnName, string text)
    {
      _dgv.Columns[columnName].HeaderText = text;
    }

    private void SetCellFormat(string columnName, string format)
    {
      _dgv.Columns[columnName].DefaultCellStyle.Format = format;
    }
  }
}
