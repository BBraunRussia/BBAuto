using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using BBAuto.Logic.Abstract;
using BBAuto.Logic.ForDriver;

namespace BBAuto.Logic.Lists
{
  public class FuelCardList : MainList
  {
    private static FuelCardList _uniqueInstance;
    private List<FuelCard> _list;

    private FuelCardList()
    {
      _list = new List<FuelCard>();

      loadFromSql();
    }

    public static FuelCardList getInstance()
    {
      if (_uniqueInstance == null)
        _uniqueInstance = new FuelCardList();

      return _uniqueInstance;
    }

    protected override void loadFromSql()
    {
      DataTable dt = _provider.Select("FuelCard");

      _list.Clear();

      foreach (DataRow row in dt.Rows)
      {
        FuelCard fuelCard = new FuelCard(row);
        Add(fuelCard);
      }
    }

    internal void Add(FuelCard fuelCard)
    {
      if (_list.Exists(item => item.ID == fuelCard.ID))
        return;

      _list.Add(fuelCard);
    }

    public FuelCard getItem(int id)
    {
      return _list.FirstOrDefault(item => item.ID == id);
    }

    public FuelCard getItem(string number)
    {
      return _list.FirstOrDefault(item => item.Number == number);
    }

    public void Delete(int idFuelCard)
    {
      FuelCard fuelCard = getItem(idFuelCard);

      _list.Remove(fuelCard);

      fuelCard.Delete();
    }

    public DataTable ToDataTable()
    {
      return CreateTable(_list.OrderBy(item => item.IsLost).ThenBy(item => item.DateEnd));
    }

    private DataTable CreateTable(IEnumerable<FuelCard> list)
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

      foreach (FuelCard fuelCard in list)
        dt.Rows.Add(fuelCard.getRow());

      return dt;
    }
  }
}
