using System.Collections.Generic;
using System.Linq;
using System.Data;
using BBAuto.Domain.Tables;
using BBAuto.Domain.Abstract;

namespace BBAuto.Domain.Lists
{
  public class RegionList : MainList
  {
    private List<Region> list;
    private static RegionList uniqueInstance;

    private RegionList()
    {
      list = new List<Region>();

      loadFromSql();
    }

    public static RegionList getInstance()
    {
      if (uniqueInstance == null)
        uniqueInstance = new RegionList();

      return uniqueInstance;
    }

    protected override void loadFromSql()
    {
      var dt = _provider.Select("Region");

      foreach (DataRow row in dt.Rows)
      {
        Region region = new Region(row);
        Add(region);
      }
    }

    public void Add(Region region)
    {
      if (list.Exists(item => item == region))
        return;

      list.Add(region);
    }

    public Region getItem(int id)
    {
      var regions = list.Where(item => item.ID == id);

      return (regions.Any()) ? regions.First() : null;
    }

    public Region getItem(string name)
    {
      var regions = list.Where(item => item.Name == name);

      return regions.Any() ? regions.First() : null;
    }
  }
}
