using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using BBAuto.Logic.Abstract;
using BBAuto.Logic.Entities;
using BBAuto.Logic.Static;

namespace BBAuto.Logic.Lists
{
  public class CarList : MainList
  {
    private static CarList _uniqueInstance;
    private List<Car> list;

    private CarList()
    {
      list = new List<Car>();

      LoadFromSql();
    }

    public static CarList getInstance()
    {
      if (_uniqueInstance == null)
        _uniqueInstance = new CarList();

      return _uniqueInstance;
    }

    protected override void LoadFromSql()
    {
      DataTable dt = Provider.Select("Car");

      foreach (DataRow row in dt.Rows)
      {
        Car car = new Car(row);
        Add(car);
      }
    }

    public void Add(Car car)
    {
      if (list.Exists(item => item == car))
        return;

      list.Add(car);
    }

    private DataTable ToDataTable()
    {
      return createTable(list);
    }

    private DataTable ToDataTableActual()
    {
      List<Car> cars;

      if (User.GetRole() == RolesList.Employee)
      {
        DriverCarList driverCarList = DriverCarList.getInstance();
        Car myCar = driverCarList.GetCar(User.GetDriver());

        cars = list.Where(car => car == myCar).ToList();
      }
      else
      {
        cars = list.Where(car => car.IsGet && !car.info.IsSale).ToList();
      }

      return createTable(cars);
    }

    private DataTable ToDataTableBuy()
    {
      var cars = list.Where(car => !car.IsGet);

      return createTable(cars.ToList());
    }

    internal DataTable createTable(List<Car> cars)
    {
      DataTable dt = new DataTable();
      dt.Columns.Add("id");
      dt.Columns.Add("idCar");
      dt.Columns.Add("Бортовой номер");
      dt.Columns.Add("Регистрационный знак");
      dt.Columns.Add("Марка");
      dt.Columns.Add("Модель");
      dt.Columns.Add("VIN");
      dt.Columns.Add("Регион");
      dt.Columns.Add("Водитель");
      dt.Columns.Add("№ ПТС");
      dt.Columns.Add("№ СТС");
      dt.Columns.Add("Год выпуска");
      dt.Columns.Add("Пробег", System.Type.GetType("System.Int32"));
      dt.Columns.Add("Дата последней записи о пробеге", Type.GetType("System.DateTime"));
      dt.Columns.Add("Собственник");
      dt.Columns.Add("Дата окончания гарантии", Type.GetType("System.DateTime"));
      dt.Columns.Add("Статус");

      foreach (Car car in cars)
        dt.Rows.Add(car.ToRow());

      return dt;
    }

    public Car getItem(int id)
    {
      return list.FirstOrDefault(car => car.Id == id);
    }

    public Car getItem(string grz)
    {
      var cars = list.Where(item => ((item.Grz.Replace(" ", "") != string.Empty) &&
                                     (item.Grz.Replace(" ", "") == grz.Replace(" ", ""))));

      if (cars.Count() > 0)
        return cars.First();

      if (grz.Replace(" ", "").Length >= 6)
      {
        cars = list.Where(item => ((item.Grz.Replace(" ", "") != string.Empty) &&
                                   (item.Grz.Replace(" ", "").Substring(0, 6) ==
                                    grz.Replace(" ", "").Substring(0, 6))));

        if (cars.Count() == 1)
          return cars.First();
      }

      return null;
    }

    public DataTable ToDataTable(Status status)
    {
      switch (status)
      {
        case Status.Buy:
          return ToDataTableBuy();
        case Status.Actual:
          return ToDataTableActual();
        case Status.Repair:
          return RepairList.getInstance().ToDataTable();
        case Status.Sale:
          return CarSaleList.getInstance().ToDataTable();
        case Status.Invoice:
          return InvoiceList.getInstance().ToDataTable();
        case Status.Policy:
          return PolicyList.getInstance().ToDataTable();
        case Status.DTP:
          return DTPList.getInstance().ToDataTable();
        case Status.Violation:
          return ViolationList.getInstance().ToDataTable();
        case Status.DiagCard:
          return DiagCardList.getInstance().ToDataTable();
        case Status.TempMove:
          return TempMoveList.getInstance().ToDataTable();
        case Status.ShipPart:
          return ShipPartList.getInstance().ToDataTable();
        case Status.Account:
          return AccountList.GetInstance().ToDataTable();
        case Status.AccountViolation:
          return ViolationList.getInstance().ToDataTableAccount();
        case Status.FuelCard:
          return FuelCardList.getInstance().ToDataTable();
        case Status.Driver:
          return DriverList.getInstance().ToDataTable();
        default:
          return ToDataTable();
      }
    }

    internal int getNextBBNumber()
    {
      if (list.Count > 0)
      {
        int maxNumber = list.Max(item => item.BBNumberInt);

        return maxNumber + 1;
      }

      return 1;
    }

    public void Delete(int idCar)
    {
      Car car = getItem(idCar);

      list.Remove(car);

      if (car != null)
        car.Delete();
    }
  }
}
