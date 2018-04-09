using System.Collections.Generic;
using System.Data;
using System.Linq;
using BBAuto.Logic.Abstract;
using BBAuto.Logic.Common;
using BBAuto.Logic.Tables;

namespace BBAuto.Logic.Lists
{
  public class WayBillRouteList : MainList
  {
    private static WayBillRouteList uniqueInstance;
    private List<WayBillRoute> list;

    private WayBillRouteList()
    {
      list = new List<WayBillRoute>();

      loadFromSql();
    }

    public static WayBillRouteList getInstance()
    {
      if (uniqueInstance == null)
        uniqueInstance = new WayBillRouteList();

      return uniqueInstance;
    }

    protected override void loadFromSql()
    {
      DataTable dt = _provider.Select("WayBillRoute");

      foreach (DataRow row in dt.Rows)
      {
        WayBillRoute wayBillRoute = new WayBillRoute(row);
        Add(wayBillRoute);
      }
    }

    public void Add(WayBillRoute wayBillRoute)
    {
      if (list.Exists(item => item == wayBillRoute))
        return;

      list.Add(wayBillRoute);
    }

    public MainDictionary GetItem(int id)
    {
      return list.FirstOrDefault(i => i.ID == id);
    }

    public IEnumerable<Route> GetList(WayBillDay wayBillDay)
    {
      IEnumerable<Route> routeList = list.Where(item => item.WayBillDay == wayBillDay).Select(item => item.Route);

      return routeList;
    }
  }
}
