using System;
using System.Data;
using BBAuto.Logic.Common;
using BBAuto.Logic.Dictionary;
using BBAuto.Logic.Entities;
using BBAuto.Logic.Lists;
using BBAuto.Logic.Services.Car;
using BBAuto.Logic.Static;

namespace BBAuto.Logic.ForCar
{
  public class Invoice
  {
    private const int DefaultDriverMediator = 2;

    private int _idDriverFrom;
    private int _idDriverTo;
    private int _idRegionFrom;
    private int _idRegionTo;
    private DateTime _dateMove;

    public int Id { get; private set; }

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

    public string DateMove
    {
      get => (_dateMove.Year == 1) ? string.Empty : _dateMove.ToShortDateString();
      set => DateTime.TryParse(value, out _dateMove);
    }

    public string DateMoveForSql => _dateMove.Year == 1
      ? string.Empty
      : string.Concat(_dateMove.Year.ToString(), "-", _dateMove.Month.ToString(), "-", _dateMove.Day.ToString());

    public DateTime Date { get; set; }
    public int CarId { get; private set; }

    public Invoice(int carId)
    {
      CarId = carId;
      Id = 0;
      Number = getNextNumber();
      Date = DateTime.Today;

      fillNewInvoice();
    }

    public Invoice(DataRow row)
    {
      FillFields(row);
    }

    private void FillFields(DataRow row)
    {
      Id = Convert.ToInt32(row.ItemArray[0]);

      int.TryParse(row.ItemArray[1].ToString(), out int idCar);
      CarId = idCar;

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
    }

    private void fillNewInvoice()
    {
      InvoiceList invoiceList = InvoiceList.getInstance();
      Invoice invoice = invoiceList.GetItem(CarId);

      if (invoice == null)
      {
        //int.TryParse(Car.regionUsingID.ToString(), out _idRegionFrom);
        _idDriverFrom = DefaultDriverMediator;
        //int.TryParse(Car.regionUsingID.ToString(), out _idRegionTo);
        //int.TryParse(Car.driverID.ToString(), out _idDriverTo);
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

    public void Save()
    {
      //DeleteFile(File);

      File = WorkWithFiles.FileCopyById(File, "cars", CarId, "Invoices", Number);

      //Id = Convert.ToInt32(Provider.Insert("Invoice", Id, CarId, Number, DriverFromId, DriverToId, Date,
        //DateMoveForSql, RegionFromId, RegionToId, File));
    }

    internal object[] ToRow()
    {
      var regions = Regions.getInstance();

      var driverList = DriverList.getInstance();

      var driverFrom = driverList.getItem(_idDriverFrom);
      var driverTo = driverList.getItem(_idDriverTo);

      return new object[]
      {
        Id, CarId, "car.BbNumber", "car.Grz", Number, regions.getItem(_idRegionFrom), driverFrom.GetName(NameType.Full),
        regions.getItem(_idRegionTo), driverTo.GetName(NameType.Full), Date, _dateMove
      };
    }

    internal void Delete()
    {
      //DeleteFile(File);

      //Provider.Delete("Invoice", Id);
    }
  }
}
