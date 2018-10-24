using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using BBAuto.Domain.Tables;
using BBAuto.Domain.Abstract;

namespace BBAuto.Domain.Lists
{
    public class RouteList : MainList
    {
        private static RouteList uniqueInstance;
        private List<Route> list;
        
        private RouteList()
        {
            list = new List<Route>();

            loadFromSql();
        }

        public static RouteList getInstance()
        {
            if (uniqueInstance == null)
                uniqueInstance = new RouteList();

            return uniqueInstance;
        }

        protected override void loadFromSql()
        {
            DataTable dt = _provider.Select("Route");

            foreach (DataRow row in dt.Rows)
            {
                Route route = new Route(row);
                Add(route);
            }
        }

        public void Add(Route route)
        {
            if (list.Exists(item => item == route))
                return;

            list.Add(route);
        }

        public void Delete(int idRoute)
        {
            Route route = getItem(idRoute);

            if (route == null)
                return;

            list.Remove(route);

            route.Delete();
        }

        public Route getItem(int id)
        {
            return list.FirstOrDefault(item => item.ID == id);
        }

        public DataTable ToDataTable(MyPoint myPoint1)
        {
            var listNew = list.Where(item => (item.MyPoint1 == myPoint1) || (item.MyPoint2 == myPoint1)).ToList();

            return CreateTable(listNew, myPoint1);
        }

        private DataTable CreateTable(List<Route> routes, MyPoint myPoint1)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("id");
            dt.Columns.Add("Пункт назначения");
            dt.Columns.Add("Дистанция");

            foreach (Route route in routes)
                dt.Rows.Add(route.getRow(myPoint1));

            return dt;
        }

        public bool Exists(MyPoint myPoint1, MyPoint myPoint2)
        {
            return list.Exists(item => ((item.MyPoint1 == myPoint1 && myPoint2 == item.MyPoint2)) || ((item.MyPoint2 == myPoint1 && myPoint2 == item.MyPoint1)));
        }

        public bool Exists(MyPoint myPoint)
        {
            return list.Exists(item => myPoint == item.MyPoint1 || myPoint == item.MyPoint2);
        }

        internal Route GetRandomItem(Random random, MyPoint mainPoint)
        {
            var listNew = (from item in list
                           where mainPoint == item.MyPoint1 || mainPoint == item.MyPoint2
                           select item).ToList();

            if (listNew.Count == 0)
                throw new NullReferenceException("Отсутствуют маршруты для данного города");

            int rand = random.Next(0, listNew.Count - 1);

            return (mainPoint == listNew[rand].MyPoint1) ? new Route(listNew[rand].MyPoint1, listNew[rand].MyPoint2, listNew[rand].Distance.ToString()) : new Route(listNew[rand].MyPoint2, listNew[rand].MyPoint1, listNew[rand].Distance.ToString());
        }

        internal Route GetRandomItem(Random random, MyPoint mainPoint, MyPoint toPoint)
        {
            var listNew = (from item in list
                           where (toPoint == item.MyPoint1 || toPoint == item.MyPoint2) && mainPoint != item.MyPoint1 && mainPoint != item.MyPoint2
                           select item).ToList();

            int rand = random.Next(0, listNew.Count - 1);

            return (toPoint == listNew[rand].MyPoint1) ? new Route(listNew[rand].MyPoint1, listNew[rand].MyPoint2, listNew[rand].Distance.ToString()) : new Route(listNew[rand].MyPoint2, listNew[rand].MyPoint1, listNew[rand].Distance.ToString());
        }

        internal Route GetItem(MyPoint point1, MyPoint point2)
        {
            var listNew = (from item in list
                           where (point1 == item.MyPoint1 || point1 == item.MyPoint2) && (point2 == item.MyPoint1 || point2 == item.MyPoint2)
                           select item).ToList();

            return (point1 == listNew.First().MyPoint1) ? listNew.First() : new Route(listNew.First().MyPoint2, listNew.First().MyPoint1, listNew.First().Distance.ToString());
        }
    }
}
