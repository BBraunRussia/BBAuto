using System;
using System.Windows.Forms;
using BBAuto.Logic.Entities;
using BBAuto.Logic.ForCar;
using BBAuto.Logic.Static;

namespace BBAuto.Logic.Services.Documents
{
  public interface IDocumentsService
  {
    WordDocument CreateActFuelCard(int invoiceId);
    WordDocument CreateProxyOnSto(int carId, int invoiceId);
    void PrintProxyOnSto(int carId, int invoiceId);

    ExcelDocument CreateDocumentInvoice(int carId, int invoiceId);
    ExcelDocument CreateExcelFromAllDgv(DataGridView mainDgvDgv);
    ExcelDocument CreatePolicyTable();
    ExcelDocument CreateNotice(int carId, DTP dtp);
    ExcelDocument CreateExcelFromDgv(DataGridView mainDgvDgv);
    ExcelDocument CreateAttacheToOrder(int carId, int invoiceId);
    ExcelDocument CreateWaybill(int carId, DateTime date, Driver driver = null);
    void AddRouteInWayBill(ExcelDocument document, int carId, DateTime dtpDateValue, Fields fields);
  }
}
