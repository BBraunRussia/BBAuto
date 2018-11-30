using BBAuto.Domain.Abstract;
using BBAuto.Domain.Common;
using BBAuto.Domain.Dictionary;
using BBAuto.Domain.Entities;
using BBAuto.Domain.Lists;
using System;
using BBAuto.Domain.Services.Mail;

namespace BBAuto.Domain.ForCar
{
  public class Violation : MainDictionary
  {
    private int _sum;
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
        if (_datePay.HasValue && value == null)
          Agreed = false;

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

    public int ViolationTypeId { get; set; }

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
      int.TryParse(row[0].ToString(), out int id);
      ID = id;

      int.TryParse(row[1].ToString(), out int carId);
      Car = CarList.GetInstance().getItem(carId);

      DateTime.TryParse(row[2].ToString(), out DateTime date);
      Date = date;

      Number = row[3].ToString();
      File = row[4].ToString();
      _fileBegin = File;

      if (DateTime.TryParse(row[5].ToString(), out DateTime datePay))
        DatePay = datePay;

      FilePay = row[6].ToString();
      _fileBeginPay = FilePay;

      int.TryParse(row[7].ToString(), out int violationTypeId);
      ViolationTypeId = violationTypeId;

      int.TryParse(row[8].ToString(), out _sum);
      int.TryParse(row[9].ToString(), out _sent);
      int.TryParse(row[10].ToString(), out _noDeduction);

      bool.TryParse(row[11].ToString(), out bool agreed);
      Agreed = agreed;

      DateTime.TryParse(row[12].ToString(), out DateTime dateCreate);
      DateCreate = new DateTime(dateCreate.Year, dateCreate.Month, dateCreate.Day);
    }

    public override void Save()
    {
      DeleteFile(File);
      DeleteFilePay();

      if (_fileBegin != File)
        File = WorkWithFiles.fileCopyByID(File, "cars", Car.ID, "Violation", Number);
      if (_fileBeginPay != FilePay)
        FilePay = WorkWithFiles.fileCopyByID(FilePay, "cars", Car.ID, "ViolationPay", Number);

      var datePaySql = string.Empty;
      if (DatePay.HasValue)
      {
        datePaySql = string.Concat(DatePay.Value.Year.ToString(), "-", DatePay.Value.Month.ToString(), "-",
          DatePay.Value.Day.ToString());
      }

      var dateSql = string.Concat(Date.Year.ToString(), "-", Date.Month.ToString(), "-", Date.Day.ToString());

      int.TryParse(_provider.Insert("Violation", ID, Car.ID, dateSql, Number, File, datePaySql,
        FilePay ?? string.Empty, ViolationTypeId, _sum, _sent, _noDeduction, Agreed.ToString()), out int id);
      ID = id;
    }

    internal override void Delete()
    {
      DeleteFile(File);
      DeleteFilePay();

      _provider.Delete("Violation", ID);
    }

    internal override object[] getRow()
    {
      var driver = GetDriver();

      var violationType = ViolationTypes.getInstance();

      var invoice = InvoiceList.getInstance().getItem(Car, Date);
      var regions = Regions.getInstance();
      var regionName = invoice == null
        ? regions.getItem(Car.RegionUsingId)
        : regions.getItem(Convert.ToInt32(invoice.RegionToId));

      return new object[]
      {
        ID, Car.ID, Car.BBNumber, Car.Grz, regionName, Date, driver.FullName, Number, DatePay,
        violationType.getItem(ViolationTypeId), _sum
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
        GetDriver().FullName,
        ViolationTypes.getInstance().getItem(ViolationTypeId),
        _sum,
        btnName
      };
    }

    public override string ToString()
    {
      return Car == null ? "нет данных" : string.Concat("№", Number, " от ", Date.ToShortDateString());
    }

    public Driver GetDriver()
    {
      return DriverCarList.GetInstance().GetDriver(Car, Date) ?? new Driver();
    }

    protected void DeleteFilePay()
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
