using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using BBAuto.Domain.Abstract;
using BBAuto.Domain.ForDriver;

namespace BBAuto.Domain.Lists
{
  public class FuelCardDriverList : MainList<FuelCardDriver>
  {
    private static FuelCardDriverList _uniqueInstance;
    
    public static FuelCardDriverList getInstance()
    {
      return _uniqueInstance ?? (_uniqueInstance = new FuelCardDriverList());
    }

    protected override void LoadFromSql()
    {
      var dt = Provider.Select("FuelCardDriver");
      
      foreach (DataRow row in dt.Rows)
      {
        Add(new FuelCardDriver(row));
      }
    }

    public override void Add(FuelCardDriver fuelCardDriver)
    {
      if (_list.Contains(fuelCardDriver) || fuelCardDriver.FuelCard == null)
        return;

      _list.Add(fuelCardDriver);
    }

    public FuelCardDriver getItem(int id)
    {
      return _list.FirstOrDefault(item => item.ID == id);
    }

    public FuelCardDriver getItem(FuelCard fuelCard)
    {
      return _list.OrderByDescending(item => item.DateBegin).FirstOrDefault(item => item.FuelCard == fuelCard);
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

      _list.Remove(fuelCardDriver);

      fuelCardDriver.Delete();
    }

    public DataTable ToDataTable()
    {
      return CreateTable(
        _list.OrderBy(item => item.FuelCard.Number).OrderBy(item => item.FuelCard.IsLost).ToList()
      );
    }

    public DataTable ToDataTable(FuelCard fuelCard)
    {
      return CreateTable(
        _list.Where(item => item.FuelCard.ID == fuelCard.ID).OrderByDescending(item => item.DateBegin)
      );
    }

    private static DataTable CreateTable(IEnumerable<FuelCardDriver> list)
    {
      DataTable dt = new DataTable();
      dt.Columns.Add("idFuelCardDriver");
      dt.Columns.Add("idFuelCard");
      dt.Columns.Add("Номер");
      dt.Columns.Add("Водитель");
      dt.Columns.Add("Регион");
      dt.Columns.Add("Срок действия", typeof(DateTime));
      dt.Columns.Add("Фирма");
      dt.Columns.Add("Начало использования", typeof(DateTime));
      dt.Columns.Add("Окончание использования", typeof(DateTime));

      foreach (FuelCardDriver fuelCarsDriver in list)
        dt.Rows.Add(fuelCarsDriver.getRow());

      return dt;
    }

    public FuelCardDriver GetItemFirst(int driverId)
    {
      var list = GetList(driverId);

      return list.FirstOrDefault();
    }

    public FuelCardDriver GetItemSecond(int driverId)
    {
      var list = GetList(driverId);

      return list.Count > 1 ? list[1] : null;
    }

    internal List<FuelCardDriver> GetList(int driverId)
    {
      return _list.Where(item => item.Driver.ID == driverId && !item.DateEnd.HasValue && !item.FuelCard.IsLost).OrderByDescending(item => item.DateBegin)
        .ToList();
    }

    public DataTable ToDataTable(int driverId)
    {
      var myList = _list.Where(item => item.Driver.ID == driverId).OrderByDescending(item => item.DateBegin).ToList();

      return CreateTable(myList);
    }
  }
}
