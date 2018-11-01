using System;
using System.Collections.Generic;
using System.Windows.Forms;
using BBAuto.Domain.Common;
using BBAuto.Domain.Entities;
using BBAuto.Domain.ForCar;
using BBAuto.Domain.Static;

namespace BBAuto.Domain.Services.OfficeDocument
{
  public interface IExcelDocumentService
  {
    IDocument CreateExcelFromDGV(DataGridView dgv);
    IDocument CreateExcelFromAllDGV(DataGridView dgv);
    IDocument CreateInvoice(Car car, Invoice invoice);
    IDocument CreateNotice(Car car, DTP dtp);
    IExcelDoc CreateWaybill(Car car, DateTime date, Driver driver = null);
    void AddRouteInWayBill(IExcelDoc excelDoc, Car car, DateTime date, Fields fields);
    IDocument CreateAttacheToOrder(Car car, Invoice invoice);
    void CreateHeader(ExcelDoc excelDoc, string text);

    IDocument CreateReportPolicy();
    IDocument CreateReportInstractionDocument();
    IDocument CreateReportMileage(IList<Car> carList, DateTime dateBegin, DateTime dateEnd);

    IDocument CreateReportLoadMileage(IList<MileageReport> mileageReportList);
  }
}
