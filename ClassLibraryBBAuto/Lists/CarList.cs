using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using BBAuto.Domain.Static;
using BBAuto.Domain.Abstract;
using BBAuto.Domain.Entities;
using BBAuto.Domain.Services.CarSale;
using BBAuto.Domain.Services.Transponder;

namespace BBAuto.Domain.Lists
{
  public class CarList : MainList
  {
    private static CarList _uniqueInstance;
    private readonly List<Car> _list;

    private CarList()
    {
      _list = new List<Car>();

      loadFromSql();
    }

    public static CarList GetInstance()
    {
      return _uniqueInstance ?? (_uniqueInstance = new CarList());
    }

    protected override void loadFromSql()
    {
      _list.Clear();

      var dt = _provider.Select("Car");

      foreach (DataRow row in dt.Rows)
      {
        _list.Add(new Car(row));
      }
    }

    private DataTable ToDataTable()
    {
      return createTable(_list);
    }

    private DataTable ToDataTableActual()
    {
      var cars = GetActualCars();

      return createTable(cars);
    }

    public IList<Car> GetActualCars()
    {
      List<Car> cars;

      if (User.GetRole() == RolesList.Employee)
      {
        DriverCarList driverCarList = DriverCarList.GetInstance();
        Car myCar = driverCarList.GetCar(User.GetDriver());

        cars = _list.Where(car => car == myCar).ToList();
      }
      else
      {
        ICarSaleService carSaleService = new CarSaleService();
        var carSaleList = carSaleService.GetCarSaleList();

        cars = _list.Where(car => car.IsGet && carSaleList.All(carSale => carSale.CarId != car.ID)).ToList();
      }

      return cars;
    }

    private DataTable ToDataTableBuy()
    {
      var cars = _list.Where(car => !car.IsGet);

      return createTable(cars.ToList());
    }

    internal DataTable createTable(IList<Car> cars)
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
        dt.Rows.Add(car.getRow());

      return dt;
    }

    public Car getItem(int id)
    {
      return _list.FirstOrDefault(car => car.ID == id);
    }

    public Car getItem(string grz)
    {
      var cars = _list.Where(item =>
        ((item.Grz.Replace(" ", "") != string.Empty) && (item.Grz.Replace(" ", "") == grz.Replace(" ", ""))));

      if (cars.Any())
        return cars.First();

      if (grz.Replace(" ", "").Length >= 6)
      {
        cars = _list.Where(item =>
          ((item.Grz.Replace(" ", "") != string.Empty) &&
           (item.Grz.Replace(" ", "").Substring(0, 6) == grz.Replace(" ", "").Substring(0, 6))));

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
        {
          ICarSaleService carSaleService = new CarSaleService();
          return carSaleService.ToDataTable();
        }
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
          return AccountList.getInstance().ToDataTable();
        case Status.AccountViolation:
          return ViolationList.getInstance().ToDataTableAccount();
        case Status.FuelCard:
          return FuelCardList.getInstance().ToDataTable();
        case Status.Driver:
          return DriverList.getInstance().ToDataTable();
        case Status.Transponder:
        {
          ITransponderService transponderService = new TransponderService();
          return transponderService.GetReportTransponderList();
        }
        default:
          return ToDataTable();
      }
    }

    internal int getNextBBNumber()
    {
      if (_list.Count > 0)
      {
        int maxNumber = _list.Max(item => item.BBNumberInt);

        return maxNumber + 1;
      }

      return 1;
    }

    public void Delete(int idCar)
    {
      var car = getItem(idCar);

      _list.Remove(car);

      car?.Delete();
    }
  }
}
