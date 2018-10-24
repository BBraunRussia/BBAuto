using System.Collections.Generic;
using System.Linq;
using System.Data;
using BBAuto.Domain.Tables;
using BBAuto.Domain.Abstract;

namespace BBAuto.Domain.Lists
{
  public class RegionList : MainList
  {
    private readonly List<Region> _list;
    private static RegionList _uniqueInstance;

    private RegionList()
    {
      _list = new List<Region>();

      loadFromSql();
    }

    public static RegionList getInstance()
    {
      return _uniqueInstance ?? (_uniqueInstance = new RegionList());
    }

    protected override void loadFromSql()
    {
      var dt = _provider.Select("Region");

      foreach (DataRow row in dt.Rows)
      {
        var region = new Region(row);
        Add(region);
      }
    }

    public void Add(Region region)
    {
      if (_list.Exists(item => item == region))
        return;

      _list.Add(region);
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
