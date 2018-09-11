using BBAuto.Domain.Abstract;
using BBAuto.Domain.Dictionary;
using BBAuto.Domain.Entities;
using BBAuto.Domain.Lists;
using BBAuto.Domain.Static;
using System;
using System.Data;

namespace BBAuto.Domain.ForCar
{
  public class DTP : MainDictionary
  {
    private int _statusAfterDtpId;
    private int _idRegion;
    private int _idCulprit;
    private double _sum;
    private DateTime _dateCallInsure;
    private int _currentStatusAfterDtpId;

    public string Facts { get; set; }
    public string Damage { get; set; }
    public string Comm { get; set; }
    public string NumberLoss { get; set; }

    public Car Car { get; }

    public string IDStatusAfterDTP
    {
      get => _statusAfterDtpId.ToString();
      set => int.TryParse(value, out _statusAfterDtpId);
    }

    public object CurrentStatusAfterDtpId
    {
      get => _currentStatusAfterDtpId.ToString();
      set
      {
        if (value != null)
          int.TryParse(value.ToString(), out _currentStatusAfterDtpId);
      }
    }

    public string RegionId
    {
      get => _idRegion.ToString();
      set => int.TryParse(value, out _idRegion);
    }

    public string CulpritId
    {
      get => _idCulprit.ToString();
      set => int.TryParse(value, out _idCulprit);
    }

    public int Number { get; private set; }

    public string Sum
    {
      get => _sum.ToString();
      set => double.TryParse(value.Replace(" ", "").Replace(".", ","), out _sum);
    }

    public string DateCallInsure
    {
      get => _dateCallInsure.Year == 1 ? string.Empty : _dateCallInsure.ToShortDateString();
      set => DateTime.TryParse(value, out _dateCallInsure);
    }

    public DateTime Date { get; set; }

    public DTP(Car car)
    {
      ID = 0;
      Car = car;
      _statusAfterDtpId = 0;
      _idRegion = 0;
      Date = DateTime.Now;
      _dateCallInsure = DateTime.Now;
    }

    public DTP(DataRow row)
    {
      int.TryParse(row.ItemArray[0].ToString(), out int id);
      ID = id;

      int.TryParse(row.ItemArray[1].ToString(), out int idCar);
      Car = CarList.GetInstance().getItem(idCar);

      int.TryParse(row.ItemArray[2].ToString(), out int number);
      Number = number;

      DateTime.TryParse(row.ItemArray[3].ToString(), out DateTime date);
      Date = date;

      int.TryParse(row.ItemArray[4].ToString(), out _idRegion);
      DateTime.TryParse(row.ItemArray[5].ToString(), out _dateCallInsure);
      int.TryParse(row.ItemArray[6].ToString(), out _idCulprit);
      IDStatusAfterDTP = row.ItemArray[7].ToString();
      NumberLoss = row.ItemArray[8].ToString();
      double.TryParse(row.ItemArray[9].ToString(), out _sum);
      Damage = row.ItemArray[10].ToString();
      Facts = row.ItemArray[11].ToString();
      Comm = row.ItemArray[12].ToString();
      CurrentStatusAfterDtpId = row.ItemArray[13].ToString();
    }

    public override void Save()
    {
      int.TryParse(_provider.Insert("DTP", ID, Car.ID, Date, _idRegion, _dateCallInsure, CulpritId, IDStatusAfterDTP,
        NumberLoss, _sum, Damage, Facts, Comm, CurrentStatusAfterDtpId), out int id);
      ID = id;

      var dtpList = DTPList.getInstance();
      dtpList.Add(this);

      if (Number == 0)
        Number = dtpList.GetMaxNumber() + 1;
    }

    private DataTable getCulpritDataTable()
    {
      return _provider.DoOther("exec Culprit_SelectWithUser @p1, @p2", Car.ID, Date);
    }

    internal override void Delete()
    {
      _provider.Delete("DTP", ID);
    }

    internal override object[] getRow()
    {
      var regions = Regions.getInstance();

      Culprits culpritList = Culprits.GetInstance();
      StatusAfterDTPs statusAfterDTP = StatusAfterDTPs.getInstance();

      Driver driver = GetDriver() ?? new Driver();

      return new object[]
      {
        ID, Car.ID, Car.BBNumber, Car.Grz, Number, Date, regions.getItem(_idRegion), driver.GetName(NameType.Full),
        _dateCallInsure, GetCurrentStatusAfterDtp(), culpritList.getItem(_idCulprit), _sum, Comm, Facts, Damage,
        statusAfterDTP.getItem(_statusAfterDtpId), NumberLoss
      };
    }

    internal bool IsEqualDriverId(Driver driver)
    {
      Driver driver2 = GetDriver();

      return driver?.ID == driver2?.ID;
    }

    public override string ToString()
    {
      return Car == null ? "нет данных" : string.Concat("№", Number, " дата ", Date.ToShortDateString());
    }

    public DTPFile CreateFile()
    {
      return new DTPFile(this);
    }

    internal object[] GetCulpit()
    {
      var driverCarList = DriverCarList.GetInstance();
      var driver = driverCarList.GetDriver(Car, Date);

      return new object[] {4, driver.GetName(NameType.Full)};
    }

    public Driver GetDriver()
    {
      var driverCarList = DriverCarList.GetInstance();
      return driverCarList.GetDriver(Car, Date);
    }

    public string GetCurrentStatusAfterDtp()
    {
      var currentStatusAfterDTPs = CurrentStatusAfterDTPs.getInstance();
      return currentStatusAfterDTPs.getItem(_currentStatusAfterDtpId);
    }
  }
}
