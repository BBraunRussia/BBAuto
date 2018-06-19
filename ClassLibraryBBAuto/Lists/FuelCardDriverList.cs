using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using BBAuto.Domain.Abstract;
using BBAuto.Domain.ForDriver;
using BBAuto.Domain.Entities;

namespace BBAuto.Domain.Lists
{
  public class FuelCardDriverList : MainList
  {
    private static FuelCardDriverList uniqueInstance;
    private List<FuelCardDriver> list;

    private FuelCardDriverList()
    {
      list = new List<FuelCardDriver>();

      loadFromSql();
    }

    public static FuelCardDriverList getInstance()
    {
      if (uniqueInstance == null)
        uniqueInstance = new FuelCardDriverList();

      return uniqueInstance;
    }

    protected override void loadFromSql()
    {
      DataTable dt = _provider.Select("FuelCardDriver");

      list.Clear();

      foreach (DataRow row in dt.Rows)
      {
        FuelCardDriver fuelCardDriver = new FuelCardDriver(row);
        Add(fuelCardDriver);
      }
    }

    public void Add(FuelCardDriver fuelCardDriver)
    {
      if ((list.Exists(item => item == fuelCardDriver)) || (fuelCardDriver.FuelCard == null))
        return;

      list.Add(fuelCardDriver);
    }

    public FuelCardDriver getItem(int id)
    {
      return list.FirstOrDefault(item => item.ID == id);
    }

    public FuelCardDriver getItem(FuelCard fuelCard)
    {
      return list.Where(item => item.FuelCard == fuelCard).OrderByDescending(item => item.DateBegin).FirstOrDefault();
    }

    /*
    public FuelCardDriver getItem(Car car, DateTime date)
    {
        DriverCarList driverCarList = DriverCarList.getInstance();

        var table = _provider.DoOther("Select_FuelCardDriver_ByDate", date, car.ID);

        //TODO сделать другую выборку. зависает...
        var list = this.list.Where(item => date > item.DateBegin && date <= item.DateEnd.Value && driverCarList.GetCar(item.Driver, date) == car).ToList();

        return (list.Count == 0) ? null : list.First();
    }
    */
    public void Delete(int idFuelCardDriver)
    {
      FuelCardDriver fuelCardDriver = getItem(idFuelCardDriver);

      list.Remove(fuelCardDriver);

      fuelCardDriver.Delete();
    }

    public DataTable ToDataTable()
    {
      return createTable(
        list.OrderBy(item => item.FuelCard.Number).OrderBy(item => item.FuelCard.IsLost).ToList()
      );
    }

    public DataTable ToDataTable(FuelCard fuelCard)
    {
      return createTable(
        list.Where(item => item.FuelCard.ID == fuelCard.ID).OrderByDescending(item => item.DateBegin)
      );
    }

    private DataTable createTable(IEnumerable<FuelCardDriver> list)
    {
      DataTable dt = new DataTable();
      dt.Columns.Add("idFuelCardDriver");
      dt.Columns.Add("idFuelCard");
      dt.Columns.Add("Номер");
      dt.Columns.Add("Водитель");
      dt.Columns.Add("Регион");
      dt.Columns.Add("Срок действия", Type.GetType("System.DateTime"));
      dt.Columns.Add("Фирма");
      dt.Columns.Add("Начало использования", Type.GetType("System.DateTime"));
      dt.Columns.Add("Окончание использования", Type.GetType("System.DateTime"));

      foreach (FuelCardDriver fuelCarsDriver in list)
        dt.Rows.Add(fuelCarsDriver.getRow());

      return dt;
    }

    public FuelCardDriver getItemFirst(Driver driver)
    {
      List<FuelCardDriver> list = ToList(driver);

      return list.FirstOrDefault();
    }

    public FuelCardDriver getItemSecond(Driver driver)
    {
      List<FuelCardDriver> list = ToList(driver);

      return (list.Count > 1) ? list[1] : null;
    }

    internal List<FuelCardDriver> ToList(Driver driver)
    {
      return list.Where(item => item.Driver == driver && item.DateEnd == null).OrderByDescending(item => item.DateBegin)
        .ToList();
    }

    public DataTable ToDataTable(Driver driver)
    {
      var myList = list.Where(item => item.Driver == driver).OrderByDescending(item => item.DateBegin).ToList();

      return createTable(myList);
    }
  }
}
