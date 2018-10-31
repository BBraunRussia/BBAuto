using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using BBAuto.Domain.Common;
using BBAuto.Domain.Abstract;
using BBAuto.Domain.Entities;

namespace BBAuto.Domain.Lists
{
  public class WayBillDayList : MainList<WayBillDay>
  {
    private static WayBillDayList _uniqueInstance;
    
    public static WayBillDayList getInstance()
    {
      return _uniqueInstance ?? (_uniqueInstance = new WayBillDayList());
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

    public WayBillDay getItem(int id)
    {
      return _list.FirstOrDefault(item => item.ID == id);
    }

    public IEnumerable<WayBillDay> getList(Car car, DateTime date)
    {
      return _list.Where(item => item.Car.ID == car.ID && item.Date.Year == date.Year && item.Date.Month == date.Month)
        .OrderBy(item => item.Date);
    }
  }
}
