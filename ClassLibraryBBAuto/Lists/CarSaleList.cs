using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using BBAuto.Domain.Abstract;
using BBAuto.Domain.ForCar;

namespace BBAuto.Domain.Lists
{
  public class CarSaleList : MainList
  {
    private static CarSaleList uniqueInstance;
    private List<CarSale> list;

    private CarSaleList()
    {
      list = new List<CarSale>();

      loadFromSql();
    }

    public static CarSaleList getInstance()
    {
      if (uniqueInstance == null)
        uniqueInstance = new CarSaleList();

      return uniqueInstance;
    }

    protected override void loadFromSql()
    {
      DataTable dt = _provider.Select("CarSale");

      foreach (DataRow row in dt.Rows)
      {
        CarSale carSale = new CarSale(row);
        Add(carSale);
      }
    }

    public void Add(CarSale carSale)
    {
      if (list.Exists(item => item == carSale))
        return;

      list.Add(carSale);
    }
  }
}
