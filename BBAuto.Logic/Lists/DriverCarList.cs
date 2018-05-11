using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using BBAuto.Logic.Abstract;
using BBAuto.Logic.Entities;

namespace BBAuto.Logic.Lists
{
  public class DriverCarList : MainList
  {
    private static DriverCarList uniqueInstance;
    private List<DriverCar> list;

    private DriverCarList()
    {
      list = new List<DriverCar>();

      LoadFromSql();
    }

    public static DriverCarList getInstance()
    {
      if (uniqueInstance == null)
        uniqueInstance = new DriverCarList();

      return uniqueInstance;
    }

    protected override void LoadFromSql()
    {
      DataTable dt = Provider.Select("DriverCar");

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
    
    public Driver GetDriver(int carId)
    {
      var driverCars = from driverCar in list
        where driverCar.Car.Id == carId
        orderby driverCar.dateEnd descending, driverCar.Number descending
        select driverCar;

      var carList = CarList.getInstance();
      var car = carList.getItem(carId);

      if (driverCars.ToList().Count != 0 || car.IsGet)
        return getDriver(driverCars.ToList());

      var driverList = DriverList.getInstance();
      return driverList.getItem(Convert.ToInt32(car.driverID));
    }

    public Driver GetDriver(int carId, DateTime date)
    {
      var driverCars = from driverCar in list
        where driverCar.IsDriverCar(carId, date)
        orderby driverCar.dateEnd descending, driverCar.Number descending
        select driverCar;

      TempMoveList tempMoveList = TempMoveList.getInstance();

      Driver driver = tempMoveList.GetDriver(carId, date);
      return driver ?? getDriver(driverCars.ToList());
    }

    private Driver getDriver(List<DriverCar> driverCars)
    {
      if (!driverCars.Any())
        return null;

      var driverCar = driverCars.First();
      var driverList = DriverList.getInstance();
      return driverList.getItem(driverCar.Driver.Id);
    }

    public Car GetCar(Driver driver)
    {
      DateTime date = DateTime.Today;

      var driverCars = list.Where(item => item.Driver.Id == driver.Id && item.dateEnd == date)
        .OrderByDescending(item => item.dateEnd);

      if (driverCars.Count() > 0)
      {
        CarList carList = CarList.getInstance();

        foreach (var driverCar in driverCars)
        {
          if (list.Where(item => !(item.Driver.Id == driver.Id) && item.dateEnd == date &&
                                 item.Car.Id == driverCar.Car.Id && item.Number > driverCar.Number).Count() == 0)
            return carList.getItem(driverCar.Car.Id);
        }

        return null;
      }
      else
        return null;
    }

    public Car GetCar(Driver driver, DateTime date)
    {
      var driverCars = from driverCar in list
        where driverCar.Driver.Id == driver.Id
        orderby driverCar.dateEnd descending, driverCar.Number descending
        select driverCar;

      return (driverCars.Count() > 0) ? CarList.getInstance().getItem(driverCars.First().Car.Id) : null;
    }

    public bool IsDriverHaveCar(Driver driver)
    {
      return GetCar(driver) != null;
    }

    public DataTable ToDataTableCar(Driver driver)
    {
      var driverCars = list.Where(item => item.Driver.Id == driver.Id).OrderByDescending(item => item.dateEnd);

      CarList carList = CarList.getInstance();
      List<Car> cars = new List<Car>();

      foreach (DriverCar driverCar in driverCars)
      {
        Car car = carList.getItem(driverCar.Car.Id);
        cars.Add(car);
      }

      return carList.createTable(cars);
    }
  }
}
