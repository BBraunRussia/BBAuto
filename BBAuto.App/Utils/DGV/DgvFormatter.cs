using System;
using System.Drawing;
using System.Windows.Forms;
using BBAuto.App.GUI;
using BBAuto.Logic.Lists;
using BBAuto.Logic.Services.DiagCard;
using BBAuto.Logic.Services.Violation;
using BBAuto.Logic.Static;

namespace BBAuto.App.Utils.DGV
{
  internal class DgvFormatter : IDgvFormatter
  {
    private DataGridView _dgv;

    private readonly IDiagCardService _diagCardService;
    private readonly IViolationService _violationService;

    public DgvFormatter(
      IDiagCardService diagCardService,
      IViolationService violationService)
    {
      _diagCardService = diagCardService;
      _violationService = violationService;
    }

    public void SetDgv(DataGridView dgv)
    {
      _dgv = dgv;
    }
    
    public void FormatByOwner()
    {
      var carList = CarList.getInstance();

      foreach (DataGridViewRow row in _dgv.Rows)
      {
        int.TryParse(row.Cells[1].Value.ToString(), out int id);
        var car = carList.getItem(id);

        if (car == null)
          return;

        if (car.info.Owner == "ООО \"Б.Браун Медикал\"")
        {
          row.DefaultCellStyle.BackColor = BBColors.bbGreen3;
        }
        else if (car.info.Owner == "ООО \"ГЕМАТЕК\"")
        {
          row.DefaultCellStyle.BackColor = BBColors.bbGray5;
        }
      }
    }

    public void HideTwoFirstColumns()
    {
      HideColumn(0);
      HideColumn(1);
    }

    public void FormatDgv(Status status)
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
          SetFormatDtp();
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
      var invoiceList = InvoiceList.getInstance();

      foreach (DataGridViewRow row in _dgv.Rows)
      {
        int.TryParse(row.Cells[0].Value.ToString(), out int id);

        var invoice = invoiceList.GetItem(id);

        if (invoice.DateMove != string.Empty)
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
        int.TryParse(row.Cells[0].Value.ToString(), out int id);

        var policy = policyList.getItem(id);

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

    private void SetFormatDtp()
    {
      SetCellFormat("Сумма возмещения", "N2");

      var dtpList = DTPList.getInstance();

      foreach (DataGridViewRow row in _dgv.Rows)
      {
        int.TryParse(row.Cells[0].Value.ToString(), out int id);

        var dtp = dtpList.getItem(id);

        if (dtp.GetCurrentStatusAfterDtp() == "Отремонтирован")
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

      foreach (DataGridViewRow row in _dgv.Rows)
      {
        int.TryParse(row.Cells[0].Value.ToString(), out int id);

        var violation = _violationService.GetById(id);

        if (violation.Sent)
          row.Cells["№ постановления"].Style.BackColor = Color.MediumPurple;

        if (violation.FilePay != string.Empty)
          row.Cells["Дата оплаты"].Style.BackColor = Color.MediumPurple;
      }
    }

    private void SetFormatDiagCard()
    {
      foreach (DataGridViewRow row in _dgv.Rows)
      {
        int.TryParse(row.Cells[0].Value.ToString(), out int id);
        var diagCard = _diagCardService.GetByCarId(id);

        if (diagCard.File != string.Empty)
          row.Cells["№ ДК"].Style.BackColor = Color.MediumPurple;
      }
    }

    private void SetFormatAccount()
    {
      SetRightAligment("Сумма");
      SetCellFormat("Сумма", "N2");

      var accountList = AccountList.GetInstance();

      foreach (DataGridViewRow row in _dgv.Rows)
      {
        int.TryParse(row.Cells[0].Value.ToString(), out int id);

        var account = accountList.getItem(id);

        if (account.Agreed)
          row.Cells["Согласование"].Style.BackColor = Color.MediumPurple;
      }
    }

    private void SetFormatFuelCard()
    {
      HideColumn("Начало использования");
      HideColumn("Окончание использования");

      var fuelCardList = FuelCardList.getInstance();

      foreach (DataGridViewRow row in _dgv.Rows)
      {
        int.TryParse(row.Cells[1].Value.ToString(), out int id);

        var fuelCard = fuelCardList.getItem(id);

        if (fuelCard.IsVoid)
          row.DefaultCellStyle.BackColor = BBColors.bbGray4;
        else if (row.Cells["Водитель"].Value.ToString() == "(Резерв)")
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
        if (row.Cells["Скан водительского удостоверения"].Value.ToString() == "нет"
            || row.Cells["Скан медицинской справки"].Value.ToString() == "нет")
        {
          row.DefaultCellStyle.BackColor = Color.LightYellow;
        }

        int.TryParse(row.Cells[0].Value.ToString(), out int idDriver);
        var driver = driverList.getItem(idDriver);

        if (driver.Fired)
          row.DefaultCellStyle.ForeColor = Color.Red;

        if (driver.OwnerID < 3 && string.IsNullOrEmpty(driver.Number) || driver.Decret)
          row.DefaultCellStyle.ForeColor = Color.Blue;

        if (driver.OwnerID > 2)
          row.DefaultCellStyle.ForeColor = BBColors.bbGreen1;
      }
    }

    public void SetFormatMileage()
    {
      SetCellFormat("Пробег", "N0");
      SetRightAligment("Пробег");
    }

    public void SetFormatRepair()
    {
      SetCellFormat("Стоимость", "N2");
      SetRightAligment("Стоимость");
    }

    public void HideColumn(int index)
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
