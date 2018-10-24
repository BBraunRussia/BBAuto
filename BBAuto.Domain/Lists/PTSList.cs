using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using BBAuto.Domain.ForCar;
using BBAuto.Domain.Abstract;
using BBAuto.Domain.Entities;

namespace BBAuto.Domain.Lists
{
    public class PTSList : MainList
    {
        private List<PTS> list;
        private static PTSList uniqueInstance;

        private PTSList()
        {
            list = new List<PTS>();

            loadFromSql();
        }

        public static PTSList getInstance()
        {
            if (uniqueInstance == null)
                uniqueInstance = new PTSList();

            return uniqueInstance;
        }

        protected override void loadFromSql()
        {
            DataTable dt = _provider.Select("PTS");

            foreach (DataRow row in dt.Rows)
            {
                PTS pts = new PTS(row);
                Add(pts);
            }
        }

        public void Add(PTS pts)
        {
            if (list.Exists(x => x == pts))
                return;

            list.Add(pts);
        }

        public void Delete(Car car)
        {
            PTS pts = getItem(car);

            list.Remove(pts);

            pts.Delete();
        }

        public PTS getItem(Car car)
        {
            var PTSs = list.Where(item => item.Car.ID == car.ID);

            return (PTSs.Count() > 0) ? PTSs.First() : car.createPTS();
        }
    }
}
