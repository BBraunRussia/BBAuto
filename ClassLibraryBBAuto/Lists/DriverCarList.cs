using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using BBAuto.Domain.Abstract;
using BBAuto.Domain.Entities;

namespace BBAuto.Domain.Lists
{
  public class DriverCarList : MainList
  {
    private static DriverCarList uniqueInstance;
    private List<DriverCar> list;

    private DriverCarList()
    {
      list = new List<DriverCar>();

      loadFromSql();
    }

    public static DriverCarList getInstance()
    {
      if (uniqueInstance == null)
        uniqueInstance = new DriverCarList();

      return uniqueInstance;
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

      if (list.Exists(item => item == drCar))
        return;

      list.Add(drCar);
    }

    public Driver GetDriver(Car car)
    {
      var driverCars = from driverCar in list
        where driverCar.Car.ID == car.ID
        orderby driverCar.dateEnd descending, driverCar.Number descending
        select driverCar;

      if ((driverCars.ToList().Count == 0) && (!car.IsGet))
      {
        DriverList driverList = DriverList.getInstance();
        return driverList.getItem(Convert.ToInt32(car.driverID));
      }
      else
      {
        return getDriver(driverCars.ToList());
      }
    }

    public Driver GetDriver(Car car, DateTime date)
    {
      var driverCars = from driverCar in list
        where driverCar.isDriverCar(car, date)
        orderby driverCar.dateEnd descending, driverCar.Number descending
        select driverCar;

      TempMoveList tempMoveList = TempMoveList.getInstance();

      Driver driver = tempMoveList.getDriver(car, date);
      return (driver == null) ? getDriver(driverCars.ToList()) : driver;
    }

    private Driver getDriver(List<DriverCar> driverCars)
    {
      if (driverCars.Count() > 0)
      {
        DriverCar driverCar = driverCars.First() as DriverCar;
        DriverList driverList = DriverList.getInstance();
        return driverList.getItem(driverCar.Driver.ID);
      }
      else
      {
        return null;
      }
    }

    public Car GetCar(Driver driver)
    {
      DateTime date = DateTime.Today;

      var driverCars = list.Where(item => item.Driver.ID == driver.ID && item.dateEnd == date)
        .OrderByDescending(item => item.dateEnd);

      if (driverCars.Count() > 0)
      {
        CarList carList = CarList.GetInstance();

        foreach (var driverCar in driverCars)
        {
          if (list.Where(item =>
                !(item.Driver.ID == driver.ID) && item.dateEnd == date && item.Car.ID == driverCar.Car.ID &&
                item.Number > driverCar.Number).Count() == 0)
            return carList.getItem(driverCar.Car.ID);
        }

        return null;
      }
      else
        return null;
    }

    public Car GetCar(Driver driver, DateTime date)
    {
      var driverCars = from driverCar in list
        where driverCar.Driver.ID == driver.ID
        orderby driverCar.dateEnd descending, driverCar.Number descending
        select driverCar;

      return (driverCars.Count() > 0) ? CarList.GetInstance().getItem(driverCars.First().Car.ID) : null;
    }

    public bool IsDriverHaveCar(Driver driver)
    {
      return GetCar(driver) != null;
    }

    public DataTable ToDataTableCar(Driver driver)
    {
      var driverCars = list.Where(item => item.Driver.ID == driver.ID).OrderByDescending(item => item.dateEnd);

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