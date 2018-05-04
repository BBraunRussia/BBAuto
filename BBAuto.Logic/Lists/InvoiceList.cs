using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using BBAuto.Logic.Abstract;
using BBAuto.Logic.Entities;
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
      if (uniqueInstance == null)
        uniqueInstance = new InvoiceList();

      return uniqueInstance;
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

    public Invoice getItem(int id)
    {
      return list.FirstOrDefault(i => i.Id == id);
    }

    public Invoice getItem(Car car)
    {
      var invoices = from invoice in list
        where invoice.Car.Id == car.Id && invoice.DateMove != string.Empty
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

    public DataTable ToDataTable(Car car)
    {
      var invoices = from invoice in list
        where invoice.Car.Id == car.Id
        orderby invoice.Date descending, Convert.ToInt32(invoice.Number) descending
        select invoice;

      return createTable(invoices.ToList());
    }

    private DataTable createTable(List<Invoice> invoices)
    {
      DataTable dt = new DataTable();
      dt.Columns.Add("id");
      dt.Columns.Add("idCar");
      dt.Columns.Add("Бортовой номер");
      dt.Columns.Add("Регистрационный знак");
      dt.Columns.Add("№ накладной", Type.GetType("System.Int32"));
      dt.Columns.Add("Откуда");
      dt.Columns.Add("Сдал");
      dt.Columns.Add("Куда");
      dt.Columns.Add("Принял");
      dt.Columns.Add("Дата накладной", Type.GetType("System.DateTime"));
      dt.Columns.Add("Дата передачи", Type.GetType("System.DateTime"));

      foreach (Invoice invoice in invoices)
        dt.Rows.Add(invoice.ToRow());

      return dt;
    }

    public void Delete(int idInvoice)
    {
      Invoice invoice = getItem(idInvoice);

      list.Remove(invoice);
      invoice.Delete();
    }

    internal int GetNextNumber()
    {
      var invoices = list.Where(item => item.Date.Year == DateTime.Today.Year)
        .OrderByDescending(item => Convert.ToInt32(item.Number));

      return (invoices.Count() == 0) ? 1 : Convert.ToInt32(invoices.First().Number) + 1;
    }
  }
}
