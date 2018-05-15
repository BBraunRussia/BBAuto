using System;
using System.Windows.Forms;
using BBAuto.App.GUI;
using BBAuto.App.Utils.DGV;
using BBAuto.Logic.Common;
using BBAuto.Logic.Entities;
using BBAuto.Logic.Lists;
using BBAuto.Logic.Services.Documents;
using BBAuto.Logic.Static;
using Common.Resources;

namespace BBAuto.App.CommonForms
{
  public partial class InputDate : Form
  {
    private readonly IMainDgv _dgvMain;
    private readonly Logic.Static.Actions _action;
    private readonly WayBillType _type;

    private readonly IDocumentsService _documentsService;

    public InputDate(
      IMainDgv dgvMain,
      Logic.Static.Actions action,
      WayBillType type,
      IDocumentsService documentsService)
    {
      InitializeComponent();

      _dgvMain = dgvMain;
      _action = action;
      _type = type;
      _documentsService = documentsService;
    }

    private void btnOK_Click(object sender, EventArgs e)
    {
      var mainStatus = MainStatus.getInstance();
      var status = mainStatus.Get();

      foreach (DataGridViewCell cell in _dgvMain.SelectedCells)
      {
        Car car = _dgvMain.GetCar(cell);

        DateTime date = new DateTime(dateTimePicker1.Value.Year, dateTimePicker1.Value.Month, 1);

        IDocument excelWayBill;

        try
        {
          excelWayBill = status == Status.Invoice
            ? CreateWayBill(car, date, _dgvMain.GetId(cell.RowIndex))
            : CreateWayBill(car, date);
        }
        catch (NullReferenceException)
        {
          continue;
        }

        if (_action == Logic.Static.Actions.Print)
          excelWayBill.Print();
        else
          excelWayBill.Show();
      }

      if (_action == Logic.Static.Actions.Print)
      {
        MyPrinter printer = new MyPrinter();
        MessageBox.Show("Документы отправлены на печать на принтер " + printer.GetDefaultPrinterName(), Captions.Information,
          MessageBoxButtons.OK, MessageBoxIcon.Information);
      }
    }

    private IDocument CreateWayBill(Car car, DateTime date, int idInvoice = 0)
    {
      Driver driver = null;
      if (idInvoice != 0)
      {
        var invoiceList = InvoiceList.getInstance();
        var invoice = invoiceList.GetItem(idInvoice);
        var driverList = DriverList.getInstance();
        driver = driverList.getItem(Convert.ToInt32(invoice.DriverToID));
      }

      var document = _documentsService.CreateWaybill(car.Id, date, driver);

      try
      {
        if (_type == WayBillType.Day)
          _documentsService.AddRouteInWayBill(document, car.Id, date, Fields.All);
      }
      catch (NullReferenceException ex)
      {
        MessageBox.Show(ex.Message, Captions.Warning, MessageBoxButtons.OK, MessageBoxIcon.Information);
        document.Close();
        throw;
      }

      return document;
    }
  }
}
