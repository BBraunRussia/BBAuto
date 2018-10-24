using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using BBAuto.Domain.Common;
using BBAuto.Domain.Abstract;
using BBAuto.Domain.Entities;

namespace BBAuto.Domain.Lists
{
    public class WayBillDayList : MainList
    {
        private static WayBillDayList uniqueInstance;
        private List<WayBillDay> list;

        private WayBillDayList()
        {
            list = new List<WayBillDay>();

            loadFromSql();
        }

        public static WayBillDayList getInstance()
        {
            if (uniqueInstance == null)
                uniqueInstance = new WayBillDayList();

            return uniqueInstance;
        }

        protected override void loadFromSql()
        {
            DataTable dt = _provider.Select("WayBillDay");

            foreach (DataRow row in dt.Rows)
            {
                WayBillDay wayBillDay = new WayBillDay(row);
                Add(wayBillDay);
            }
        }

        public void Add(WayBillDay wayBillDay)
        {
            if (list.Exists(item => item == wayBillDay))
                return;

            list.Add(wayBillDay);
        }

        public WayBillDay getItem(int id)
        {
            return list.FirstOrDefault(item => item.ID == id);
        }

        public IEnumerable<WayBillDay> getList(Car car, DateTime date)
        {
            return list.Where(item => item.Car.ID == car.ID && item.Date.Year == date.Year && item.Date.Month == date.Month).OrderBy(item => item.Date);
        }
    }
}
