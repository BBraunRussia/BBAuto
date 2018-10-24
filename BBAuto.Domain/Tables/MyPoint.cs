using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using BBAuto.Domain.Abstract;
using BBAuto.Domain.Lists;
using BBAuto.Domain.Dictionary;

namespace BBAuto.Domain.Tables
{
    public class MyPoint : MainDictionary
    {
        public int RegionID { get; set; }
        public string Name { get; set; }

        public MyPoint(int idRegion)
        {
            ID = 0;
            RegionID = idRegion;
            Name = string.Empty;
        }

        public MyPoint(DataRow row)
        {
            int id;
            int.TryParse(row[0].ToString(), out id);
            ID = id;

            int idRegion;
            int.TryParse(row[1].ToString(), out idRegion);
            RegionID = idRegion;

            Name = row[2].ToString();
        }

        public override void Save()
        {
            int id;
            int.TryParse(_provider.Insert("MyPoint", ID, RegionID, Name), out id);
            ID = id;

            MyPointList pointList = MyPointList.getInstance();
            pointList.Add(this);
        }

        internal override void Delete()
        {
            _provider.Delete("MyPoint", ID);
        }

        internal override object[] getRow()
        {
            Regions regions = Regions.getInstance();

            return new object[] { ID, Name };
        }
    }
}
