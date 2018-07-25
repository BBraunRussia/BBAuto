using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using BBAuto.Domain.Abstract;
using BBAuto.Domain.ForCar;
using BBAuto.Domain.Entities;

namespace BBAuto.Domain.Lists
{
  public class InvoiceList : MainList
  {
    private static InvoiceList _uniqueInstance;
    private readonly List<Invoice> _list;

    private InvoiceList()
    {
      _list = new List<Invoice>();

      loadFromSql();
    }

    public static InvoiceList getInstance()
    {
      if (_uniqueInstance == null)
        _uniqueInstance = new InvoiceList();

      return _uniqueInstance;
    }

    protected override void loadFromSql()
    {
      var dt = _provider.Select("Invoice");

      foreach (DataRow row in dt.Rows)
      {
        Invoice invoice = new Invoice(row);
        Add(invoice);
      }
    }

    public void Add(Invoice invoice)
    {
      if (_list.Exists(item => item == invoice))
        return;

      _list.Add(invoice);
    }

    public Invoice getItem(int id)
    {
      return _list.FirstOrDefault(i => i.ID == id);
    }

    public Invoice getItem(Car car)
    {
      var invoices = from invoice in _list
        where invoice.Car.ID == car.ID && invoice.DateMove != string.Empty
        orderby invoice.Date descending, Convert.ToInt32(invoice.Number) descending
        select invoice;

      return invoices.FirstOrDefault();
    }

    public Invoice getItem(Car car, DateTime date)
    {
      return (from invoice in _list
          where invoice.Car.ID == car.ID && invoice.DateMove != string.Empty && Convert.ToDateTime(invoice.DateMove) <= date
          orderby invoice.Date descending, Convert.ToInt32(invoice.Number) descending
          select invoice)
        .FirstOrDefault();
    }

    public DataTable ToDataTable()
    {
      var invoices = from invoice in _list
        orderby invoice.Date descending, Convert.ToInt32(invoice.Number) descending
        select invoice;

      return CreateTable(invoices.ToList());
    }

    public DataTable ToDataTable(Car car)
    {
      var invoices = from invoice in _list
        where invoice.Car.ID == car.ID
        orderby invoice.Date descending, Convert.ToInt32(invoice.Number) descending
        select invoice;

      return CreateTable(invoices.ToList());
    }

    private static DataTable CreateTable(IEnumerable<Invoice> invoices)
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
        dt.Rows.Add(invoice.getRow());

      return dt;
    }

    public void Delete(int idInvoice)
    {
      var invoice = getItem(idInvoice);

      _list.Remove(invoice);
      invoice.Delete();
    }

    public int GetNextNumber()
    {
      var invoice = _list.Where(item => item.Date.Year == DateTime.Today.Year)
        .OrderByDescending(item => Convert.ToInt32(item.Number)).FirstOrDefault();

      return invoice == null ? 1 : Convert.ToInt32(invoice.Number) + 1;
    }
  }
}
