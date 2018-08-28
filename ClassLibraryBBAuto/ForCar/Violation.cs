using BBAuto.Domain.Abstract;
using BBAuto.Domain.Common;
using BBAuto.Domain.Dictionary;
using BBAuto.Domain.Entities;
using BBAuto.Domain.Lists;
using BBAuto.Domain.Static;
using System;
using BBAuto.Domain.Services.Mail;

namespace BBAuto.Domain.ForCar
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
      get => _datePay;
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
      get => _sum == 0 ? string.Empty : _sum.ToString();
      set => int.TryParse(value, out _sum);
    }

    public string IDViolationType
    {
      get => _idViolationType.ToString();
      set => int.TryParse(value, out _idViolationType);
    }

    public bool Sent
    {
      get => Convert.ToBoolean(_sent);
      set => _sent = Convert.ToInt32(value);
    }

    public bool NoDeduction
    {
      get => Convert.ToBoolean(_noDeduction);
      set => _noDeduction = Convert.ToInt32(value);
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
      ID = id;

      int idCar;
      int.TryParse(row[1].ToString(), out idCar);
      Car = CarList.GetInstance().getItem(idCar);

      DateTime date;
      DateTime.TryParse(row[2].ToString(), out date);
      Date = date;

      Number = row[3].ToString();
      File = row[4].ToString();
      _fileBegin = File;

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

      if (_fileBegin != File)
        File = WorkWithFiles.fileCopyByID(File, "cars", Car.ID, "Violation", Number);
      if (_fileBeginPay != FilePay)
        FilePay = WorkWithFiles.fileCopyByID(FilePay, "cars", Car.ID, "ViolationPay", Number);

      string datePay = string.Empty;
      if (DatePay != null)
      {
        datePay = string.Concat(DatePay.Value.Year.ToString(), "-", DatePay.Value.Month.ToString(), "-",
          DatePay.Value.Day.ToString());
      }

      int id;
      int.TryParse(_provider.Insert("Violation", ID, Car.ID, Date, Number, File, datePay,
        FilePay, _idViolationType, _sum, _sent, _noDeduction, Agreed.ToString()), out id);
      ID = id;
    }

    internal override void Delete()
    {
      DeleteFile(File);
      deleteFilePay();

      _provider.Delete("Violation", ID);
    }

    internal override object[] getRow()
    {
      Driver driver = getDriver();

      ViolationTypes violationType = ViolationTypes.getInstance();

      InvoiceList invoiceList = InvoiceList.getInstance();
      Invoice invoice = invoiceList.getItem(Car, Date);
      Regions regions = Regions.getInstance();
      string regionName = invoice == null
        ? regions.getItem(Car.RegionUsingId)
        : regions.getItem(Convert.ToInt32(invoice.RegionToID));

      return new object[]
      {
        ID, Car.ID, Car.BBNumber, Car.Grz, regionName, Date, driver.GetName(NameType.Full), Number, DatePay,
        violationType.getItem(_idViolationType), _sum
      };
    }

    internal object[] GetRowAccount()
    {
      var btnName = Agreed ? string.Empty : "Согласовать";

      return new object[]
      {
        ID,
        Car.ID,
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
      if (!string.IsNullOrEmpty(_fileBeginPay) && _fileBeginPay != FilePay)
        WorkWithFiles.Delete(_fileBeginPay);
    }

    public void Agree()
    {
      IMailService mailService = new MailService();
      mailService.SendMailAccountViolation(this);

      Agreed = true;

      Save();
    }
  }
}
