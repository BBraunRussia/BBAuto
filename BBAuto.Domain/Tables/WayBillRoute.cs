using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using BBAuto.Domain.Lists;
using BBAuto.Domain.Common;
using BBAuto.Domain.Abstract;

namespace BBAuto.Domain.Tables
{
    public class WayBillRoute : MainDictionary
    {
        public WayBillDay WayBillDay { get; private set; }
        public Route Route { get; set; }
        
        public WayBillRoute(DataRow row)
        {
            int id;
            int.TryParse(row[0].ToString(), out id);
            ID = id;

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
            _provider.Insert("WayBillRoute", ID, WayBillDay.ID, Route.MyPoint1.ID, Route.MyPoint2.ID, Route.Distance);

            WayBillRouteList wayBillRouteList = WayBillRouteList.getInstance();
            wayBillRouteList.Add(this);
        }

        internal override object[] getRow()
        {
            throw new NotImplementedException();
        }
    }
}
