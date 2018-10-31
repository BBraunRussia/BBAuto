using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using BBAuto.Domain.Common;
using BBAuto.Domain.Abstract;
using BBAuto.Domain.Entities;

namespace BBAuto.Domain.Lists
{
  public class TabelList : MainList<Tabel>
  {
    private static TabelList _uniqueInstance;

    public static TabelList GetInstance()
    {
      return _uniqueInstance ?? (_uniqueInstance = new TabelList());
    }

    protected override void LoadFromSql()
    {
      DataTable dt = Provider.Select("Tabel");

      foreach (DataRow row in dt.Rows)
      {
        Tabel tabel = new Tabel(row);
        Add(tabel);
      }
    }
    
    internal List<int> GetDays(Driver driver, DateTime date)
    {
      var listNew = from item in _list
        where item.Driver == driver && item.Date.Year == date.Year && item.Date.Month == date.Month
        orderby item.Date.Day
        select item.Date.Day;

      return listNew.ToList();
    }

    public Tabel getItem(string comm, Driver driver, DateTime date)
    {
      List<Tabel> tabels = _list.Where(t => t.Driver == driver && t.Comment == comm && t.Date.Year == date.Year &&
                                            t.Date.Month == date.Month && t.Date.Day == date.Day).ToList();

      return tabels.FirstOrDefault();
    }

    public List<Tabel> getItemWithoutDay(string comm, Driver driver, DateTime date)
    {
      List<Tabel> tabels = _list.Where(t => t.Driver == driver && t.Comment == comm && t.Date.Year == date.Year &&
                                            t.Date.Month == date.Month).ToList();

      return tabels;
    }

  }
}
