using System.Collections.Generic;
using System.Linq;
using System.Data;
using BBAuto.Domain.Abstract;
using BBAuto.Domain.Tables;
using BBAuto.Domain.Common;

namespace BBAuto.Domain.Lists
{
  public class WayBillRouteList : MainList<WayBillRoute>
  {
    private static WayBillRouteList _uniqueInstance;
    
    private WayBillRouteList()
    {
      _list = new List<WayBillRoute>();

      LoadFromSql();
    }

    public static WayBillRouteList getInstance()
    {
      return _uniqueInstance ?? (_uniqueInstance = new WayBillRouteList());
    }

    protected override void LoadFromSql()
    {
      var dt = Provider.Select("WayBillRoute");

      foreach (DataRow row in dt.Rows)
      {
        WayBillRoute wayBillRoute = new WayBillRoute(row);
        Add(wayBillRoute);
      }
    }
    
    public MainDictionary GetItem(int id)
    {
      return _list.FirstOrDefault(i => i.ID == id);
    }

    public IEnumerable<Route> GetList(WayBillDay wayBillDay)
    {
      var routeList = _list.Where(item => item.WayBillDay == wayBillDay).Select(item => item.Route);

      return routeList;
    }
  }
}
