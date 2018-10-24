using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using BBAuto.Domain.Tables;
using BBAuto.Domain.Abstract;

namespace BBAuto.Domain.Lists
{
    public class MyPointList : MainList
    {
        private List<MyPoint> list;
        private static MyPointList uniqueInstance;

        private MyPointList()
        {
            list = new List<MyPoint>();

            loadFromSql();
        }

        public static MyPointList getInstance()
        {
            if (uniqueInstance == null)
                uniqueInstance = new MyPointList();

            return uniqueInstance;
        }

        protected override void loadFromSql()
        {
            DataTable dt = _provider.Select("MyPoint");

            foreach (DataRow row in dt.Rows)
            {
                MyPoint myPoint = new MyPoint(row);
                Add(myPoint);
            }
        }

        public void Add(MyPoint myPoint)
        {
            if (list.Exists(item => item == myPoint))
                return;

            list.Add(myPoint);
        }

        public void Delete(int idMyPoint)
        {
            MyPoint myPoint = getItem(idMyPoint);

            RouteList routeList = RouteList.getInstance();

            if (routeList.Exists(myPoint))
                throw new NotSupportedException("Невозможно удалить пункт, так как существует маршрут");

            list.Remove(myPoint);

            myPoint.Delete();
        }

        public MyPoint getItem(int id)
        {
            return list.FirstOrDefault(p => p.ID == id);
        }
        
        public DataTable ToDataTable(int idRegion)
        {
            var listNew = list.Where(item => item.RegionID == idRegion).ToList();
            listNew.Sort(Compare);

            return CreateTable(listNew);
        }

        public DataTable ToDataTableWithoutExists(int idRegion, MyPoint myPoint1)
        {
            RouteList routeList = RouteList.getInstance();

            var listNew = list.Where(item => item.RegionID == idRegion && !routeList.Exists(myPoint1, item) && item != myPoint1).ToList();
            
            listNew.Sort(Compare);

            return CreateTable(listNew);
        }

        private DataTable CreateTable(List<MyPoint> myPoints)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("id");
            dt.Columns.Add("Название");

            foreach (MyPoint myPoint in myPoints)
                dt.Rows.Add(myPoint.getRow());

            return dt;
        }

        private static int Compare(MyPoint point1, MyPoint point2)
        {
            return string.Compare(point1.Name, point2.Name);
        }
    }
}
