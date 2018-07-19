using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using BBAuto.Domain.Abstract;
using BBAuto.Domain.Entities;

namespace BBAuto.Domain.Lists
{
  public class DriverCarList : MainList
  {
    private static DriverCarList _uniqueInstance;
    private readonly List<DriverCar> _list;

    private DriverCarList()
    {
      _list = new List<DriverCar>();

      loadFromSql();
    }

    public static DriverCarList getInstance()
    {
      return _uniqueInstance ?? (_uniqueInstance = new DriverCarList());
    }

    protected override void loadFromSql()
    {
      DataTable dt = _provider.Select("DriverCar");

      foreach (DataRow row in dt.Rows)
      {
        DriverCar drCar = new DriverCar(row);
        Add(drCar);
      }
    }

    private void Add(DriverCar drCar)
    {
      if ((drCar.Driver == null) || (drCar.Car == null))
        return;

      if (_list.Exists(item => item == drCar))
        return;

      _list.Add(drCar);
    }

    public Driver GetDriver(Car car)
    {
      var driverCars = from driverCar in _list
        where driverCar.Car.ID == car.ID
        orderby driverCar.dateEnd descending, driverCar.Number descending
        select driverCar;

      if (driverCars.ToList().Count == 0 && !car.IsGet)
      {
        DriverList driverList = DriverList.getInstance();
        return driverList.getItem(Convert.ToInt32(car.driverID));
      }

      return getDriver(driverCars.ToList());
    }

    public Driver GetDriver(Car car, DateTime date)
    {
      var driverCars = from driverCar in _list
        where driverCar.isDriverCar(car, date)
        orderby driverCar.dateEnd descending, driverCar.Number descending
        select driverCar;

      TempMoveList tempMoveList = TempMoveList.getInstance();

      Driver driver = tempMoveList.getDriver(car, date);
      return driver ?? getDriver(driverCars.ToList());
    }

    private Driver getDriver(List<DriverCar> driverCars)
    {
      if (driverCars.Any())
      {
        DriverCar driverCar = driverCars.First();
        DriverList driverList = DriverList.getInstance();
        return driverList.getItem(driverCar.Driver.ID);
      }

      return null;
    }

    public Car GetCar(Driver driver)
    {
      DateTime date = DateTime.Today;

      var driverCars = _list.Where(item => item.Driver.ID == driver.ID && item.dateEnd == date)
        .OrderByDescending(item => item.dateEnd);

      if (driverCars.Any())
      {
        CarList carList = CarList.GetInstance();

        foreach (var driverCar in driverCars)
        {
          if (_list.Where(item =>
                !(item.Driver.ID == driver.ID) && item.dateEnd == date && item.Car.ID == driverCar.Car.ID &&
                item.Number > driverCar.Number).Count() == 0)
            return carList.getItem(driverCar.Car.ID);
        }

        return null;
      }

      return null;
    }

    public Car GetCar(Driver driver, DateTime date)
    {
      var driverCars = from driverCar in _list
        where driverCar.Driver.ID == driver.ID
        orderby driverCar.dateEnd descending, driverCar.Number descending
        select driverCar;

      return driverCars.Any() ? CarList.GetInstance().getItem(driverCars.First().Car.ID) : null;
    }

    public bool IsDriverHaveCar(Driver driver)
    {
      return GetCar(driver) != null;
    }

    public DataTable ToDataTableCar(Driver driver)
    {
      var driverCars = _list.Where(item => item.Driver.ID == driver.ID).OrderByDescending(item => item.dateEnd);

      CarList carList = CarList.GetInstance();
      List<Car> cars = new List<Car>();

      foreach (DriverCar driverCar in driverCars)
      {
        Car car = carList.getItem(driverCar.Car.ID);
        cars.Add(car);
      }

      return carList.createTable(cars);
    }
  }
}
