using System;
using System.Data;
using BBAuto.Logic.Abstract;
using BBAuto.Logic.Common;
using BBAuto.Logic.Dictionary;
using BBAuto.Logic.Entities;
using BBAuto.Logic.Lists;
using BBAuto.Logic.Static;

namespace BBAuto.Logic.ForCar
{
  public class Invoice : MainDictionary
  {
    private const int DEFAULT_DRIVER_MEDIATOR = 2;

    private int _idDriverFrom;
    private int _idDriverTo;
    private int _idRegionFrom;
    private int _idRegionTo;
    private DateTime _dateMove;

    public string Number { get; set; }
    public string File { get; set; }

    public string DriverFromID
    {
      get { return _idDriverFrom.ToString(); }
      set { _idDriverFrom = Convert.ToInt32(value); }
    }

    public string DriverToID
    {
      get { return _idDriverTo.ToString(); }
      set { _idDriverTo = Convert.ToInt32(value); }
    }

    public string RegionFromID
    {
      get { return _idRegionFrom.ToString(); }
      set { _idRegionFrom = Convert.ToInt32(value); }
    }

    public string RegionToID
    {
      get { return _idRegionTo.ToString(); }
      set { _idRegionTo = Convert.ToInt32(value); }
    }

    public string DateMove
    {
      get { return (_dateMove.Year == 1) ? string.Empty : _dateMove.ToShortDateString(); }
      set { DateTime.TryParse(value, out _dateMove); }
    }

    public string DateMoveForSQL
    {
      get
      {
        return (_dateMove.Year == 1)
          ? string.Empty
          : string.Concat(_dateMove.Year.ToString(), "-", _dateMove.Month.ToString(), "-", _dateMove.Day.ToString());
      }
    }

    public DateTime Date { get; set; }
    public Car Car { get; private set; }

    internal Invoice(Car car)
    {
      Car = car;
      Id = 0;
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
      Id = Convert.ToInt32(row.ItemArray[0]);

      int idCar;
      int.TryParse(row.ItemArray[1].ToString(), out idCar);
      Car = CarList.getInstance().getItem(idCar);

      Number = row.ItemArray[2].ToString();
      int.TryParse(row.ItemArray[3].ToString(), out _idDriverFrom);
      int.TryParse(row.ItemArray[4].ToString(), out _idDriverTo);

      DateTime date;
      DateTime.TryParse(row.ItemArray[5].ToString(), out date);
      Date = date;

      DateMove = row.ItemArray[6].ToString();
      int.TryParse(row.ItemArray[7].ToString(), out _idRegionFrom);
      int.TryParse(row.ItemArray[8].ToString(), out _idRegionTo);
      File = row.ItemArray[9].ToString();
      FileBegin = File;
    }

    private void fillNewInvoice()
    {
      InvoiceList invoiceList = InvoiceList.getInstance();
      Invoice invoice = invoiceList.GetItem(Car.Id);

      if (invoice == null)
      {
        int.TryParse(Car.regionUsingID.ToString(), out _idRegionFrom);
        _idDriverFrom = DEFAULT_DRIVER_MEDIATOR;
        int.TryParse(Car.regionUsingID.ToString(), out _idRegionTo);
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

      File = WorkWithFiles.FileCopyById(File, "cars", Car.Id, "Invoices", Number);

      Id = Convert.ToInt32(Provider.Insert("Invoice", Id, Car.Id, Number, DriverFromID, DriverToID, Date,
        DateMoveForSQL, RegionFromID, RegionToID, File));
    }

    internal override object[] ToRow()
    {
      Regions regions = Regions.getInstance();

      DriverList driverList = DriverList.getInstance();

      Driver driverFrom = driverList.getItem(_idDriverFrom);
      Driver driverTo = driverList.getItem(_idDriverTo);

      return new object[]
      {
        Id, Car.Id, Car.BBNumber, Car.Grz, Number, regions.getItem(_idRegionFrom), driverFrom.GetName(NameType.Full),
        regions.getItem(_idRegionTo), driverTo.GetName(NameType.Full), Date, _dateMove
      };
    }

    internal override void Delete()
    {
      DeleteFile(File);

      Provider.Delete("Invoice", Id);
    }
  }
}
