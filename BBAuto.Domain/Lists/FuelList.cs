using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using BBAuto.Domain.Abstract;
using BBAuto.Domain.Tables;
using BBAuto.Domain.ForDriver;
using BBAuto.Domain.Entities;

namespace BBAuto.Domain.Lists
{
  public class FuelList : MainList<Fuel>
  {
    private static FuelList _uniqueInstance;

    public static FuelList getInstance()
    {
      return _uniqueInstance ?? (_uniqueInstance = new FuelList());
    }

    protected override void LoadFromSql()
    {
      DataTable dt = Provider.Select("Fuel");

      foreach (DataRow row in dt.Rows)
      {
        Fuel fuel = new Fuel(row);
        Add(fuel);
      }
    }

    public Fuel getItem(int id)
    {
      return _list.FirstOrDefault(f => f.ID == id);
    }

    public Fuel getItem(FuelCard fuelCard, DateTime date, EngineType engineType)
    {
      var fuels = _list.Where(item => item.FuelCard.ID == fuelCard.ID && item.Date == date &&
                                      item.EngineType.ID == engineType.ID);

      if (fuels.Count() > 0)
        return fuels.First();

      Fuel fuel = new Fuel(fuelCard, date, engineType);
      Add(fuel);

      return fuel;
    }

    public DataTable ToDataTable(Car car, DateTime date)
    {
      var listFiltred = GetListFiltred(car, date);

      return CreateTable(listFiltred);
    }

    public IEnumerable<Fuel> GetListFiltred(Car car, DateTime date)
    {
      var dt = Provider.DoOther("exec FuelByCarAndDate_Select @p1, @p2", car.ID, date);

      var listFiltred = new List<Fuel>();

      foreach (DataRow row in dt.Rows)
      {
        listFiltred.Add(getItem(Convert.ToInt32(row.ItemArray[0])));
      }

      return listFiltred;
    }

    private DataTable CreateTable(IEnumerable<Fuel> listNew)
    {
      DataTable dt = new DataTable();
      dt.Columns.Add("id");
      dt.Columns.Add("Дата", Type.GetType("System.DateTime"));
      dt.Columns.Add("Объём");

      foreach (var item in listNew)
      {
        dt.Rows.Add(item.getRow());
      }

      return dt;
    }
  }
}
