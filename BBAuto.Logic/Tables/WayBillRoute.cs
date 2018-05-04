using System;
using System.Data;
using BBAuto.Logic.Abstract;
using BBAuto.Logic.Common;
using BBAuto.Logic.Lists;

namespace BBAuto.Logic.Tables
{
  public class WayBillRoute : MainDictionary
  {
    public WayBillDay WayBillDay { get; private set; }
    public Route Route { get; set; }

    public WayBillRoute(DataRow row)
    {
      int id;
      int.TryParse(row[0].ToString(), out id);
      Id = id;

      int idWayBillDay;
      int.TryParse(row[1].ToString(), out idWayBillDay);
      WayBillDay = WayBillDayList.getInstance().getItem(idWayBillDay);

      MyPointList myPointList = MyPointList.getInstance();
      int idMyPoint1;
      int.TryParse(row[2].ToString(), out idMyPoint1);
      MyPoint myPoint1 = myPointList.getItem(idMyPoint1);

      int idMyPoint2;
      int.TryParse(row[3].ToString(), out idMyPoint2);
      MyPoint myPoint2 = myPointList.getItem(idMyPoint2);

      string distance = row[4].ToString();

      Route = new Route(myPoint1, myPoint2, distance);
    }

    public WayBillRoute(WayBillDay wayBillDay)
    {
      WayBillDay = wayBillDay;
    }

    public override void Save()
    {
      Provider.Insert("WayBillRoute", Id, WayBillDay.Id, Route.MyPoint1.Id, Route.MyPoint2.Id, Route.Distance);

      WayBillRouteList wayBillRouteList = WayBillRouteList.getInstance();
      wayBillRouteList.Add(this);
    }

    internal override object[] ToRow()
    {
      throw new NotImplementedException();
    }
  }
}
