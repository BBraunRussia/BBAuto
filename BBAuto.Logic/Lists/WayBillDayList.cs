using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using BBAuto.Logic.Abstract;
using BBAuto.Logic.Common;
using BBAuto.Logic.Entities;

namespace BBAuto.Logic.Lists
{
  public class WayBillDayList : MainList
  {
    private static WayBillDayList uniqueInstance;
    private List<WayBillDay> list;

    private WayBillDayList()
    {
      list = new List<WayBillDay>();

      LoadFromSql();
    }

    public static WayBillDayList getInstance()
    {
      if (uniqueInstance == null)
        uniqueInstance = new WayBillDayList();

      return uniqueInstance;
    }

    protected override void LoadFromSql()
    {
      DataTable dt = Provider.Select("WayBillDay");

      foreach (DataRow row in dt.Rows)
      {
        WayBillDay wayBillDay = new WayBillDay(row);
        Add(wayBillDay);
      }
    }

    public void Add(WayBillDay wayBillDay)
    {
      if (list.Exists(item => item == wayBillDay))
        return;

      list.Add(wayBillDay);
    }

    public WayBillDay getItem(int id)
    {
      return list.FirstOrDefault(item => item.Id == id);
    }

    public IEnumerable<WayBillDay> getList(int carId, DateTime date)
    {
      return list.Where(item => item.Car.Id == carId && item.Date.Year == date.Year && item.Date.Month == date.Month)
        .OrderBy(item => item.Date);
    }
  }
}
