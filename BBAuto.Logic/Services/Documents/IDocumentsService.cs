using System;
using System.Windows.Forms;
using BBAuto.Logic.Entities;
using BBAuto.Logic.ForCar;
using BBAuto.Logic.Static;

namespace BBAuto.Logic.Services.Documents
{
  public interface IDocumentsService
  {
    void ShowActFuelCard(int invoiceId);
    IDocument CreateDocumentInvoice(int carId, int invoiceId);
    IDocument CreateExcelFromAllDgv(DataGridView mainDgvDgv);
    void CreateHeader(string text);
    IDocument CreatePolicyTable();
    IDocument CreateNotice(int carId, DTP dtp);
    IDocument CreateExcelFromDgv(DataGridView mainDgvDgv);
    IDocument CreateAttacheToOrder(int carId, int invoiceId);
    void ShowProxyOnSto(int carId, int invoiceId);
    void PrintProxyOnSto(int carId, int invoiceId);
    IDocument CreateWaybill(int carId, DateTime date, Driver driver = null);
    void AddRouteInWayBill(IDocument document, int carId, DateTime dtpDateValue, Fields fields);
  }
}
