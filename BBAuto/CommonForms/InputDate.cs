using System;
using System.Windows.Forms;
using BBAuto.Domain.Common;
using BBAuto.Domain.Entities;
using BBAuto.Domain.ForCar;
using BBAuto.Domain.Lists;
using BBAuto.Domain.Services.Document;
using BBAuto.Domain.Static;

namespace BBAuto.CommonForms
{
  public partial class InputDate : Form
  {
    private MainDGV _dgvMain;
    private Actions _action;
    private WayBillType _type;

    public InputDate(MainDGV dgvMain, Actions action, WayBillType type)
    {
      InitializeComponent();

      _dgvMain = dgvMain;
      _action = action;
      _type = type;
    }

    private void btnOK_Click(object sender, EventArgs e)
    {
      MainStatus _mainStatus = MainStatus.getInstance();
      Status status = _mainStatus.Get();

      foreach (DataGridViewCell cell in _dgvMain.SelectedCells)
      {
        Car car = _dgvMain.GetCar(cell);

        DateTime date = new DateTime(dateTimePicker1.Value.Year, dateTimePicker1.Value.Month, 1);

        IDocument excelWayBill;

        try
        {
          excelWayBill = status == Status.Invoice
            ? CreateWayBill(car, date, _dgvMain.GetID(cell.RowIndex))
            : CreateWayBill(car, date);
        }
        catch (NullReferenceException)
        {
          continue;
        }

        if (_action == Actions.Print)
          excelWayBill.Print();
        else
          excelWayBill.Show();
      }

      if (_action == Actions.Print)
      {
        MyPrinter printer = new MyPrinter();
        MessageBox.Show("Документы отправлены на печать на принтер " + printer.GetDefaultPrinterName(), "Информация",
          MessageBoxButtons.OK, MessageBoxIcon.Information);
      }
    }

    private IDocument CreateWayBill(Car car, DateTime date, int idInvoice = 0)
    {
      IExcelDocumentService excelDocumentService = new ExcelDocumentService();

      Driver driver = null;
      if (idInvoice != 0)
      {
        InvoiceList invoiceList = InvoiceList.getInstance();
        Invoice invoice = invoiceList.getItem(idInvoice);
        DriverList driverList = DriverList.getInstance();
        driver = driverList.getItem(Convert.ToInt32(invoice.DriverToID));
      }

      var waybill = excelDocumentService.CreateWaybill(car, date, driver);

      try
      {
        if (_type == WayBillType.Day)
          excelDocumentService.AddRouteInWayBill(waybill, car, date, Fields.All);
      }
      catch (NullReferenceException ex)
      {
        MessageBox.Show(ex.Message, "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Information);
        waybill.Close();
      }

      return waybill;
    }
  }
}
