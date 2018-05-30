using BBAuto.Domain.Abstract;
using BBAuto.Domain.Dictionary;
using BBAuto.Domain.Entities;
using BBAuto.Domain.Lists;
using BBAuto.Domain.Static;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BBAuto.Domain.ForCar
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
      ID = 0;
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
      ID = id;

      int idCar;
      int.TryParse(row.ItemArray[1].ToString(), out idCar);
      Car = CarList.GetInstance().getItem(idCar);

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
      int.TryParse(_provider.Insert("DTP", ID, Car.ID, Date, _idRegion, _dateCallInsure, IDCulprit, IDStatusAfterDTP,
        NumberLoss,
        _sum, Damage, Facts, Comm, IDcurrentStatusAfterDTP), out id);
      ID = id;

      DTPList dtpList = DTPList.getInstance();
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
        ID, Car.ID, Car.BBNumber, Car.Grz, Number, Date, regions.getItem(_idRegion), driver.GetName(NameType.Full),
        _dateCallInsure, GetCurrentStatusAfterDTP(), culpritList.getItem(_idCulprit), _sum, Comm, Facts, Damage,
        statusAfterDTP.getItem(_idStatusAfterDTP), NumberLoss
      };
    }

    internal bool isEqualDriverID(Driver driver)
    {
      Driver driver2 = GetDriver();

      return driver?.ID == driver2?.ID;
    }

    public override string ToString()
    {
      return (Car == null) ? "нет данных" : string.Concat("№", Number, " дата ", Date.ToShortDateString());
    }

    public DTPFile createFile()
    {
      return new DTPFile(this);
    }

    internal object[] getCulpit()
    {
      DriverCarList driverCarList = DriverCarList.getInstance();
      Driver driver = driverCarList.GetDriver(Car, Date);

      return new object[] {4, driver.GetName(NameType.Full)};
    }

    public Driver GetDriver()
    {
      DriverCarList driverCarList = DriverCarList.getInstance();
      return driverCarList.GetDriver(Car, Date);
    }

    public string GetCurrentStatusAfterDTP()
    {
      CurrentStatusAfterDTPs currentStatusAfterDTPs = CurrentStatusAfterDTPs.getInstance();
      return currentStatusAfterDTPs.getItem(_idCurrentStatusAfterDTP);
    }
  }
}
