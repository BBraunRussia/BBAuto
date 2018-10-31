using System.Collections.Generic;
using System.Linq;
using System.Data;
using BBAuto.Domain.Tables;
using BBAuto.Domain.Abstract;

namespace BBAuto.Domain.Lists
{
  public class RegionList : MainList<Region>
  {
    private static RegionList _uniqueInstance;
    
    public static RegionList getInstance()
    {
      return _uniqueInstance ?? (_uniqueInstance = new RegionList());
    }

    protected override void LoadFromSql()
    {
      var dt = Provider.Select("Region");

      foreach (DataRow row in dt.Rows)
      {
        var region = new Region(row);
        Add(region);
      }
    }

    public Region getItem(int id)
    {
      return _list.FirstOrDefault(item => item.ID == id);
    }

    public Region getItem(string name)
    {
      return _list.FirstOrDefault(item => item.Name == name);
    }

    public IList<Region> GetList()
    {
      return _list;
    }
  }
}
