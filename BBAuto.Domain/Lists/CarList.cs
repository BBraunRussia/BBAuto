using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using BBAuto.Domain.Static;
using BBAuto.Domain.Abstract;
using BBAuto.Domain.Entities;
using BBAuto.Domain.Services.CarSale;

namespace BBAuto.Domain.Lists
{
  public class CarList : MainList<Car>
  {
    private static CarList _uniqueInstance;
    
    public static CarList GetInstance()
    {
      return _uniqueInstance ?? (_uniqueInstance = new CarList());
    }

    protected override void LoadFromSql()
    {
      var dt = Provider.Select("Car");

      foreach (DataRow row in dt.Rows)
      {
        Add(new Car(row));
      }
    }

    public DataTable ToDataTable()
    {
      return createTable(_list);
    }

    public DataTable ToDataTableActual()
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

    public DataTable ToDataTableBuy()
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
      dt.Columns.Add("Пробег", typeof(int));
      dt.Columns.Add("Дата последней записи о пробеге", typeof(DateTime));
      dt.Columns.Add("Собственник");
      dt.Columns.Add("Дата окончания гарантии", typeof(DateTime));
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
        ((item.Grz.Replace(" ", "") != string.Empty) && item.Grz.Replace(" ", "") == grz.Replace(" ", "")));

      if (cars.Any())
        return cars.First();

      if (grz.Replace(" ", "").Length >= 6)
      {
        cars = _list.Where(item =>
          item.Grz.Replace(" ", "") != string.Empty &&
          item.Grz.Replace(" ", "").Substring(0, 6) == grz.Replace(" ", "").Substring(0, 6));

        if (cars.Count() == 1)
          return cars.First();
      }

      return null;
    }
    
    internal int GetNextBbNumber()
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
