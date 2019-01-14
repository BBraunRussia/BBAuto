using BBAuto.Domain.Abstract;
using BBAuto.Domain.Common;
using BBAuto.Domain.Dictionary;
using BBAuto.Domain.Entities;
using BBAuto.Domain.Lists;
using System;
using System.Data;

namespace BBAuto.Domain.ForCar
{
  public class Invoice : MainDictionary
  {
    private const int DefaultDriverMediator = 2;

    private int _idDriverFrom;
    private int _idDriverTo;
    private int _idRegionFrom;
    private int _idRegionTo;
    
    public string Number { get; set; }
    public string File { get; set; }

    public string DriverFromId
    {
      get => _idDriverFrom.ToString();
      set => _idDriverFrom = Convert.ToInt32(value);
    }

    public string DriverToId
    {
      get => _idDriverTo.ToString();
      set => _idDriverTo = Convert.ToInt32(value);
    }

    public string RegionFromId
    {
      get => _idRegionFrom.ToString();
      set => _idRegionFrom = Convert.ToInt32(value);
    }

    public string RegionToId
    {
      get => _idRegionTo.ToString();
      set => _idRegionTo = Convert.ToInt32(value);
    }

    public DateTime? DateMove { get; set; }
    public bool IsMain { get; set; }
    
    public DateTime Date { get; set; }
    public Car Car { get; private set; }

    internal Invoice(Car car)
    {
      Car = car;
      ID = 0;
      Number = getNextNumber();
      Date = DateTime.Today;

      fillNewInvoice();
    }

    public Invoice(DataRow row)
    {
      fillFields(row);
    }

    private void fillFields(DataRow row)
    {
      ID = Convert.ToInt32(row.ItemArray[0]);

      int.TryParse(row.ItemArray[1].ToString(), out int carId);
      Car = CarList.GetInstance().getItem(carId);

      Number = row.ItemArray[2].ToString();
      int.TryParse(row.ItemArray[3].ToString(), out _idDriverFrom);
      int.TryParse(row.ItemArray[4].ToString(), out _idDriverTo);

      DateTime date;
      DateTime.TryParse(row.ItemArray[5].ToString(), out date);
      Date = date;

      if (DateTime.TryParse(row.ItemArray[6].ToString(), out DateTime datetMove))
        DateMove = datetMove;

      int.TryParse(row.ItemArray[7].ToString(), out _idRegionFrom);
      int.TryParse(row.ItemArray[8].ToString(), out _idRegionTo);
      File = row.ItemArray[9].ToString();
      _fileBegin = File;

      bool.TryParse(row.ItemArray[10].ToString(), out bool isMain);
      IsMain = isMain;
    }

    private void fillNewInvoice()
    {
      InvoiceList invoiceList = InvoiceList.getInstance();
      Invoice invoice = invoiceList.getItem(Car);

      if (invoice == null)
      {
        _idRegionFrom = Car.RegionUsingId;
        _idDriverFrom = DefaultDriverMediator;
        _idRegionTo = Car.RegionUsingId;
        int.TryParse(Car.driverID.ToString(), out _idDriverTo);
      }
      else
      {
        _idRegionFrom = invoice._idRegionTo;
        _idDriverFrom = invoice._idDriverTo;
        _idRegionTo = 0;
        _idDriverTo = 0;
      }
    }

    private string getNextNumber()
    {
      InvoiceList invoiceList = InvoiceList.getInstance();
      int number = invoiceList.GetNextNumber();
      return number.ToString();
    }

    public override void Save()
    {
      DeleteFile(File);

      File = WorkWithFiles.fileCopyByID(File, "cars", Car.ID, "Invoices", Number);

      var dateSql = string.Concat(Date.Year.ToString(), "-", Date.Month.ToString(), "-",
        Date.Day.ToString());

      var dateMoveSql = string.Empty;
      if (DateMove.HasValue)
        dateMoveSql = string.Concat(DateMove.Value.Year.ToString(), "-", DateMove.Value.Month.ToString(), "-",
          DateMove.Value.Day.ToString());

      ID = Convert.ToInt32(_provider.Insert("Invoice", ID, Car.ID, Number, DriverFromId, DriverToId, dateSql,
        dateMoveSql, RegionFromId, RegionToId, File, IsMain));
    }

    internal override object[] getRow()
    {
      var regions = Regions.getInstance();
      var driverList = DriverList.getInstance();

      var driverFrom = driverList.getItem(_idDriverFrom);
      var driverTo = driverList.getItem(_idDriverTo);

      return new object[]
      {
        ID, Car.ID, Car.BBNumber, Car.Grz, Number, regions.getItem(_idRegionFrom), driverFrom.FullName,
        regions.getItem(_idRegionTo), driverTo.FullName, IsMain ? "Основной" : "Временный", Date, DateMove
      };
    }

    internal override void Delete()
    {
      DeleteFile(File);

      _provider.Delete("Invoice", ID);
    }
  }
}