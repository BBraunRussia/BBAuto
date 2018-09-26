using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using BBAuto.Domain.Abstract;
using BBAuto.Domain.Entities;
using BBAuto.Domain.Static;

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

    public static DriverCarList GetInstance()
    {
      return _uniqueInstance ?? (_uniqueInstance = new DriverCarList());
    }

    protected override void loadFromSql()
    {
      _list.Clear();

      var dt = _provider.Select("DriverCar");

      foreach (DataRow row in dt.Rows)
      {
        Add(new DriverCar(row));
      }
    }

    private void Add(DriverCar drCar)
    {
      if (drCar.Driver == null || drCar.Car == null)
        return;

      if (_list.Exists(item => item == drCar))
        return;

      _list.Add(drCar);
    }

    public Driver GetDriver(Car car)
    {
      var driverCars = from driverCar in _list
        where driverCar.Car.ID == car.ID
        orderby driverCar.DateEnd descending, driverCar.Number descending
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
        where driverCar.IsDriverCar(car, date)
        orderby driverCar.DateEnd descending, driverCar.Number descending
        select driverCar;

      var tempMoveList = TempMoveList.getInstance();

      var driver = tempMoveList.getDriver(car, date);
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

      var driverCars = _list.Where(item => item.Driver.ID == driver.ID && item.DateEnd == date)
        .OrderByDescending(item => item.DateEnd);

      if (driverCars.Any())
      {
        CarList carList = CarList.GetInstance();

        foreach (var driverCar in driverCars)
        {
          if (_list.Where(item =>
                !(item.Driver.ID == driver.ID) && item.DateEnd == date && item.Car.ID == driverCar.Car.ID &&
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
        orderby driverCar.DateEnd descending, driverCar.Number descending
        select driverCar;

      return driverCars.Any() ? CarList.GetInstance().getItem(driverCars.First().Car.ID) : null;
    }

    public bool IsDriverHaveCar(Driver driver)
    {
      return GetCar(driver) != null;
    }

    public DataTable ToDataTableCar(Driver driver)
    {
      var driverCars = _list.Where(item => item.Driver.ID == driver.ID).OrderByDescending(item => item.DateEnd);

      var carList = CarList.GetInstance();
      var cars = driverCars.Select(driverCar => carList.getItem(driverCar.Car.ID)).ToList();

      return carList.createTable(cars);
    }

    public DataTable ToDataTable(Car car)
    {
      var driverCars = _list.Where(item => item.Car.ID == car.ID && item.IsMain).OrderByDescending(item => item.DateEnd);
      
      var dt = new DataTable();
      dt.Columns.Add("������");
      dt.Columns.Add("��� ����������");
      dt.Columns.Add("������ �����������");
      dt.Columns.Add("��������� �����������");

      var regions = RegionList.getInstance();

      foreach (var driverCar in driverCars)
      {
        dt.Rows.Add(regions.getItem(driverCar.RegionId).Name, driverCar.Driver.GetName(NameType.Full),
          driverCar.DateBegin.ToShortDateString(), driverCar.DateEnd.ToShortDateString());
      }

      return dt;
    }
  }
}
