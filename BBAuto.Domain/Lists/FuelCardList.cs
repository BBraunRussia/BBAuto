using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using BBAuto.Domain.Abstract;
using BBAuto.Domain.ForDriver;

namespace BBAuto.Domain.Lists
{
  public class FuelCardList : MainList<FuelCard>
  {
    private static FuelCardList _uniqueInstance;
    
    public static FuelCardList getInstance()
    {
      return _uniqueInstance ?? (_uniqueInstance = new FuelCardList());
    }

    protected override void LoadFromSql()
    {
      DataTable dt = Provider.Select("FuelCard");

      foreach (DataRow row in dt.Rows)
      {
        FuelCard fuelCard = new FuelCard(row);
        Add(fuelCard);
      }
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
