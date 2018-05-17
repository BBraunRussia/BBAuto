using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using BBAuto.Logic.Abstract;
using BBAuto.Logic.ForCar;

namespace BBAuto.Logic.Lists
{
  public class InvoiceList : MainList
  {
    private static InvoiceList uniqueInstance;
    private List<Invoice> list;

    private InvoiceList()
    {
      list = new List<Invoice>();

      LoadFromSql();
    }

    public static InvoiceList getInstance()
    {
      return uniqueInstance ?? (uniqueInstance = new InvoiceList());
    }

    protected override void LoadFromSql()
    {
      DataTable dt = Provider.Select("Invoice");

      foreach (DataRow row in dt.Rows)
      {
        Invoice invoice = new Invoice(row);
        Add(invoice);
      }
    }

    public void Add(Invoice invoice)
    {
      if (list.Exists(item => item == invoice))
        return;

      list.Add(invoice);
    }

    public Invoice GetItem(int id)
    {
      return list.FirstOrDefault(i => i.Id == id);
    }

    public Invoice GetItemByCarId(int carId)
    {
      var invoices = from invoice in list
        where invoice.CarId == carId && invoice.DateMove != string.Empty
        orderby invoice.Date descending, Convert.ToInt32(invoice.Number) descending
        select invoice;

      return invoices.FirstOrDefault();
    }

    public DataTable ToDataTable()
    {
      var invoices = from invoice in list
        orderby invoice.Date descending, Convert.ToInt32(invoice.Number) descending
        select invoice;

      return createTable(invoices.ToList());
    }

    public DataTable ToDataTable(int carId)
    {
      var invoices = from invoice in list
        where invoice.CarId == carId
        orderby invoice.Date descending, Convert.ToInt32(invoice.Number) descending
        select invoice;

      return createTable(invoices.ToList());
    }

    private DataTable createTable(List<Invoice> invoices)
    {
      var dt = new DataTable();
      dt.Columns.Add("id");
      dt.Columns.Add("idCar");
      dt.Columns.Add("Бортовой номер");
      dt.Columns.Add("Регистрационный знак");
      dt.Columns.Add("№ накладной", typeof(int));
      dt.Columns.Add("Откуда");
      dt.Columns.Add("Сдал");
      dt.Columns.Add("Куда");
      dt.Columns.Add("Принял");
      dt.Columns.Add("Дата накладной", typeof(DateTime));
      dt.Columns.Add("Дата передачи", typeof(DateTime));

      foreach (var invoice in invoices)
        dt.Rows.Add(invoice.ToRow());

      return dt;
    }

    public void Delete(int idInvoice)
    {
      Invoice invoice = GetItem(idInvoice);

      list.Remove(invoice);
      invoice.Delete();
    }

    internal int GetNextNumber()
    {
      var invoices = list.Where(item => item.Date.Year == DateTime.Today.Year)
        .OrderByDescending(item => Convert.ToInt32(item.Number));

      return !invoices.Any() ? 1 : Convert.ToInt32(invoices.First().Number) + 1;
    }
  }
}
