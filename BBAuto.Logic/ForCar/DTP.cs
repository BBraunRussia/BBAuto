using System;
using System.Data;
using BBAuto.Logic.Abstract;
using BBAuto.Logic.Dictionary;
using BBAuto.Logic.Entities;
using BBAuto.Logic.Lists;
using BBAuto.Logic.Static;

namespace BBAuto.Logic.ForCar
{
  public class DTP : MainDictionary
  {
    private int _idStatusAfterDTP;
    private int _idRegion;
    private int _idCulprit;
    private double _sum;
    private DateTime _dateCallInsure;
    private int _idCurrentStatusAfterDTP;

    public string Facts { get; set; }
    public string Damage { get; set; }
    public string Comm { get; set; }
    public string NumberLoss { get; set; }

    public Car Car { get; private set; }

    public string IDStatusAfterDTP
    {
      get { return _idStatusAfterDTP.ToString(); }
      set { int.TryParse(value, out _idStatusAfterDTP); }
    }

    public object IDcurrentStatusAfterDTP
    {
      get { return _idCurrentStatusAfterDTP.ToString(); }
      set
      {
        if (value != null)
          int.TryParse(value.ToString(), out _idCurrentStatusAfterDTP);
      }
    }

    public string IDRegion
    {
      get { return _idRegion.ToString(); }
      set { int.TryParse(value, out _idRegion); }
    }

    public string IDCulprit
    {
      get { return _idCulprit.ToString(); }
      set { int.TryParse(value, out _idCulprit); }
    }

    public int Number { get; private set; }

    public string Sum
    {
      get { return _sum.ToString(); }
      set { double.TryParse(value.Replace(" ", "").Replace(".", ","), out _sum); }
    }

    public string DateCallInsure
    {
      get { return (_dateCallInsure.Year == 1) ? string.Empty : _dateCallInsure.ToShortDateString(); }
      set { DateTime.TryParse(value, out _dateCallInsure); }
    }

    public DateTime Date { get; set; }

    public DTP(Car car)
    {
      Id = 0;
      Car = car;
      _idStatusAfterDTP = 0;
      _idRegion = 0;
      Date = DateTime.Now;
      _dateCallInsure = DateTime.Now;
    }

    public DTP(DataRow row)
    {
      int id;
      int.TryParse(row.ItemArray[0].ToString(), out id);
      Id = id;

      int.TryParse(row.ItemArray[1].ToString(), out int idCar);
      Car = CarList.getInstance().getItem(idCar);

      int number;
      int.TryParse(row.ItemArray[2].ToString(), out number);
      Number = number;

      DateTime date;
      DateTime.TryParse(row.ItemArray[3].ToString(), out date);
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
      IDcurrentStatusAfterDTP = row.ItemArray[13].ToString();
    }

    public override void Save()
    {
      int id;
      int.TryParse(Provider.Insert("DTP", Id, Car.Id, Date, _idRegion, _dateCallInsure, IDCulprit, IDStatusAfterDTP,
        NumberLoss,
        _sum, Damage, Facts, Comm, IDcurrentStatusAfterDTP), out id);
      Id = id;

      DTPList dtpList = DTPList.getInstance();
      dtpList.Add(this);

      if (Number == 0)
        Number = dtpList.GetMaxNumber() + 1;
    }

    private DataTable getCulpritDataTable()
    {
      return Provider.DoOther("exec Culprit_SelectWithUser @p1, @p2", Car.Id, Date);
    }

    internal override void Delete()
    {
      Provider.Delete("DTP", Id);
    }

    internal override object[] ToRow()
    {
      Regions regions = Regions.getInstance();

      Culprits culpritList = Culprits.getInstance();
      StatusAfterDTPs statusAfterDTP = StatusAfterDTPs.getInstance();

      Driver driver = GetDriver();
      if (driver == null)
      {
        driver = new Driver();
      }
      
      return new object[]
      {
        Id, Car.Id, Car.BBNumber, Car.Grz, Number, Date, regions.getItem(_idRegion), driver.GetName(NameType.Full),
        _dateCallInsure, GetCurrentStatusAfterDtp(), culpritList.getItem(_idCulprit), _sum, Comm, Facts, Damage,
        statusAfterDTP.getItem(_idStatusAfterDTP), NumberLoss
      };
    }

    internal bool isEqualDriverID(Driver driver)
    {
      Driver driver2 = GetDriver();

      return driver.Equals(driver2);
    }

    public override string ToString()
    {
      return Car == null ? "нет данных" : string.Concat("№", Number, " дата ", Date.ToShortDateString());
    }

    public DTPFile createFile()
    {
      return new DTPFile(this);
    }

    internal object[] getCulpit()
    {
      DriverCarList driverCarList = DriverCarList.getInstance();
      Driver driver = driverCarList.GetDriver(Car.Id, Date);

      return new object[] {4, driver.GetName(NameType.Full)};
    }

    public Driver GetDriver()
    {
      DriverCarList driverCarList = DriverCarList.getInstance();
      return driverCarList.GetDriver(Car.Id, Date);
    }

    public string GetCurrentStatusAfterDtp()
    {
      var currentStatusAfterDtPs = CurrentStatusAfterDTPs.getInstance();
      return currentStatusAfterDtPs.getItem(_idCurrentStatusAfterDTP);
    }
  }
}
