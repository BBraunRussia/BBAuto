using System;
using System.Windows.Forms;
using BBAuto.App.GUI;
using BBAuto.App.Utils.DGV;
using BBAuto.Logic.Common;
using BBAuto.Logic.Lists;
using BBAuto.Logic.Services.Documents;
using BBAuto.Logic.Services.Driver;
using BBAuto.Logic.Static;
using Common.Resources;

namespace BBAuto.App.CommonForms
{
  public partial class InputDateForm : Form, IInputDateForm
  {
    private Logic.Static.Actions _action;
    private WayBillType _type;

    private readonly IMainDgv _dgvMain;
    private readonly IDocumentsService _documentsService;
    private readonly IDriverService _driverService;

    public InputDateForm(
      IMainDgv dgvMain,
      IDocumentsService documentsService,
      IDriverService driverService)
    {
      InitializeComponent();

      _dgvMain = dgvMain;
      _documentsService = documentsService;
      _driverService = driverService;
    }

    public DialogResult ShowDialog(Logic.Static.Actions action, WayBillType type)
    {
      _action = action;
      _type = type;

      return ShowDialog();
    }

    private void btnOK_Click(object sender, EventArgs e)
    {
      var mainStatus = MainStatus.getInstance();
      var status = mainStatus.Get();

      foreach (DataGridViewCell cell in _dgvMain.SelectedCells)
      {
        var carId = _dgvMain.GetCarId(cell);

        DateTime date = new DateTime(dateTimePicker1.Value.Year, dateTimePicker1.Value.Month, 1);

        ExcelDocument excelWayBill;

        try
        {
          excelWayBill = status == Status.Invoice
            ? CreateWayBill(carId, date, _dgvMain.GetId(cell.RowIndex))
            : CreateWayBill(carId, date);
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

    private ExcelDocument CreateWayBill(int carId, DateTime date, int idInvoice = 0)
    {
      DriverModel driver = null;
      if (idInvoice != 0)
      {
        var invoiceList = InvoiceList.getInstance();
        var invoice = invoiceList.GetItem(idInvoice);
        
        driver = _driverService.GetDriverById(Convert.ToInt32(invoice.DriverToId));
      }

      var document = _documentsService.CreateWaybill(carId, date, driver);

      try
      {
        if (_type == WayBillType.Day)
          _documentsService.AddRouteInWayBill(document, carId, date, Fields.All);
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
