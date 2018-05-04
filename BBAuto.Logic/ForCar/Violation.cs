using System;
using BBAuto.Logic.Abstract;
using BBAuto.Logic.Common;
using BBAuto.Logic.Dictionary;
using BBAuto.Logic.Entities;
using BBAuto.Logic.Lists;
using BBAuto.Logic.Static;

namespace BBAuto.Logic.ForCar
{
  public class Violation : MainDictionary
  {
    private readonly DateTime default_date = new DateTime(1, 1, 1);

    private int _sum;
    private int _idViolationType;
    private int _sent;
    private string _fileBeginPay;
    private DateTime? _datePay;
    private int _noDeduction;

    public string Number { get; set; }
    public DateTime Date { get; set; }
    public string FilePay { get; set; }
    public string File { get; set; }
    public bool Agreed { get; private set; }
    public DateTime DateCreate { get; private set; }

    public DateTime? DatePay
    {
      get { return _datePay; }
      set
      {
        _datePay = value;

        if (_datePay != null)
        {
          Agreed = true;
        }
      }
    }

    public string Sum
    {
      get { return _sum == 0 ? string.Empty : _sum.ToString(); }
      set { int.TryParse(value, out _sum); }
    }

    public string IDViolationType
    {
      get { return _idViolationType.ToString(); }
      set { int.TryParse(value, out _idViolationType); }
    }

    public bool Sent
    {
      get { return Convert.ToBoolean(_sent); }
      set { _sent = Convert.ToInt32(value); }
    }

    public bool NoDeduction
    {
      get { return Convert.ToBoolean(_noDeduction); }
      set { _noDeduction = Convert.ToInt32(value); }
    }

    public Car Car { get; private set; }

    public Violation()
    {
    }

    public Violation(Car car)
    {
      Car = car;
      Date = DateTime.Today;
      File = string.Empty;
      FilePay = string.Empty;
    }

    public Violation(object[] row)
    {
      FillFields(row);
    }

    private void FillFields(object[] row)
    {
      int id;
      int.TryParse(row[0].ToString(), out id);
      Id = id;

      int idCar;
      int.TryParse(row[1].ToString(), out idCar);
      Car = CarList.getInstance().getItem(idCar);

      DateTime date;
      DateTime.TryParse(row[2].ToString(), out date);
      Date = date;

      Number = row[3].ToString();
      File = row[4].ToString();
      FileBegin = File;

      DateTime datePay;
      DateTime.TryParse(row[5].ToString(), out datePay);
      if (datePay != default_date)
        DatePay = datePay;

      FilePay = row[6].ToString();
      _fileBeginPay = FilePay;

      int.TryParse(row[7].ToString(), out _idViolationType);
      int.TryParse(row[8].ToString(), out _sum);
      int.TryParse(row[9].ToString(), out _sent);
      int.TryParse(row[10].ToString(), out _noDeduction);

      bool agreed;
      bool.TryParse(row[11].ToString(), out agreed);
      Agreed = agreed;

      DateTime dateCreate;
      DateTime.TryParse(row[12].ToString(), out dateCreate);
      DateCreate = new DateTime(dateCreate.Year, dateCreate.Month, dateCreate.Day);
    }

    public override void Save()
    {
      DeleteFile(File);
      deleteFilePay();

      File = WorkWithFiles.FileCopyById(File, "cars", Car.Id, "Violation", Number);
      FilePay = WorkWithFiles.FileCopyById(FilePay, "cars", Car.Id, "ViolationPay", Number);

      string datePay = string.Empty;
      if (DatePay != null)
      {
        datePay = string.Concat(DatePay.Value.Year.ToString(), "-", DatePay.Value.Month.ToString(), "-",
          DatePay.Value.Day.ToString());
      }

      int id;
      int.TryParse(Provider.Insert("Violation", Id, Car.Id, Date, Number, File, datePay,
        FilePay, _idViolationType, _sum, _sent, _noDeduction, Agreed.ToString()), out id);
      Id = id;
    }

    internal override void Delete()
    {
      DeleteFile(File);
      deleteFilePay();

      Provider.Delete("Violation", Id);
    }

    internal override object[] ToRow()
    {
      Driver driver = getDriver();

      ViolationTypes violationType = ViolationTypes.getInstance();

      InvoiceList invoiceList = InvoiceList.getInstance();
      Invoice invoice = invoiceList.getItem(Car);
      Regions regions = Regions.getInstance();
      string regionName = (invoice == null)
        ? regions.getItem(Convert.ToInt32(Car.regionUsingID))
        : regions.getItem(Convert.ToInt32(invoice.RegionToID));

      return new object[]
      {
        Id, Car.Id, Car.BBNumber, Car.Grz, regionName, Date, driver.GetName(NameType.Full), Number, DatePay,
        violationType.getItem(_idViolationType), _sum
      };
    }

    internal object[] GetRowAccount()
    {
      string btnName = (Agreed) ? string.Empty : "Согласовать";

      return new object[]
      {
        Id,
        Car.Id,
        Car.BBNumber,
        Car.Grz,
        Number,
        Date,
        getDriver().GetName(NameType.Full),
        ViolationTypes.getInstance().getItem(_idViolationType),
        _sum,
        btnName
      };
    }

    public override string ToString()
    {
      return (Car == null) ? "нет данных" : string.Concat("№", Number, " от ", Date.ToShortDateString());
    }

    public Driver getDriver()
    {
      DriverCarList driverCarList = DriverCarList.getInstance();
      Driver driver = driverCarList.GetDriver(Car, Date);

      return driver ?? new Driver();
    }

    protected void deleteFilePay()
    {
      if ((_fileBeginPay != string.Empty) && (_fileBeginPay != FilePay))
        WorkWithFiles.Delete(_fileBeginPay);
    }

    public void Agree()
    {
      EMail email = new EMail();
      email.SendMailAccountViolation(this);

      Agreed = true;

      Save();
    }
  }
}
