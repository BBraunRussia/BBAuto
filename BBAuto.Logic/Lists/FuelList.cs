using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using BBAuto.Logic.Abstract;
using BBAuto.Logic.Entities;
using BBAuto.Logic.ForDriver;
using BBAuto.Logic.Tables;

namespace BBAuto.Logic.Lists
{
  public class FuelList : MainList
  {
    private static FuelList uniqueInstance;
    private List<Fuel> list;

    private FuelList()
    {
      list = new List<Fuel>();

      LoadFromSql();
    }

    public static FuelList getInstance()
    {
      if (uniqueInstance == null)
        uniqueInstance = new FuelList();

      return uniqueInstance;
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

    public void Add(Fuel fuel)
    {
      if (list.Exists(item => item.Id == fuel.Id))
        return;

      list.Add(fuel);
    }

    public Fuel getItem(int id)
    {
      return list.FirstOrDefault(f => f.Id == id);
    }

    public Fuel getItem(FuelCard fuelCard, DateTime date, EngineType engineType)
    {
      var fuels = list.Where(item => item.FuelCard.Id == fuelCard.Id && item.Date == date &&
                                     item.EngineType.Id == engineType.Id);

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
      var dt = Provider.DoOther("exec FuelByCarAndDate_Select @p1, @p2", car.Id, date);

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
        dt.Rows.Add(item.ToRow());
      }

      return dt;
    }
  }
}
