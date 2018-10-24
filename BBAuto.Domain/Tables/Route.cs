using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using BBAuto.Domain.Abstract;
using BBAuto.Domain.Lists;

namespace BBAuto.Domain.Tables
{
    public class Route : MainDictionary
    {
        public MyPoint MyPoint1 { get; private set; }
        public MyPoint MyPoint2 { get; set; }
        public int Distance { get; set; }

        public Route(MyPoint myPoint1)
        {
            MyPoint1 = myPoint1;
            Distance = 0;
        }

        public Route(MyPoint myPoint1, MyPoint myPoint2, string distance)
        {
            MyPoint1 = myPoint1;
            MyPoint2 = myPoint2;

            int distanceInt;
            int.TryParse(distance, out distanceInt);
            Distance = distanceInt;
        }

        public Route(DataRow row)
        {
            int id;
            int.TryParse(row[0].ToString(), out id);
            ID = id;

            MyPointList myPointList = MyPointList.getInstance();
            int idMyPoint1;
            int.TryParse(row[1].ToString(), out idMyPoint1);
            MyPoint1 = myPointList.getItem(idMyPoint1);

            int idMyPoint2;
            int.TryParse(row[2].ToString(), out idMyPoint2);
            MyPoint2 = myPointList.getItem(idMyPoint2);

            int distance;
            int.TryParse(row[3].ToString(), out distance);
            Distance = distance;
        }

        public override void Save()
        {
            if (ID == 0)
                return;

            int id;
            int.TryParse(_provider.Insert("Route", ID, MyPoint1.ID, MyPoint2.ID, Distance), out id);
            ID = id;

            RouteList.getInstance().Add(this);
        }

        internal override void Delete()
        {
            _provider.Delete("Route", ID);
        }

        internal override object[] getRow()
        {
            return new object[] { ID, MyPoint2.Name, Distance };
        }

        internal object[] getRow(MyPoint myPoint1)
        {
            MyPoint myPoint = (MyPoint1.ID == myPoint1.ID) ? MyPoint2 : MyPoint1;

            return new object[] { ID, myPoint.Name, Distance };
        }
    }
}
