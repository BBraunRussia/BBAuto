using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using BBAuto.Domain.Tables;
using BBAuto.Domain.Abstract;
using BBAuto.Domain.Dictionary;
using BBAuto.Domain.Lists;

namespace BBAuto.Domain.Common
{
    public class SuppyAddress : MainDictionary
    {
        public MyPoint Point { get; set; }

        public string Region
        {
            get
            {
                Regions regions = Regions.getInstance();
                return regions.getItem(Point.RegionID);
            }
        }
        
        public SuppyAddress()
        {
            ID = 0;
        }

        public SuppyAddress(DataRow row)
        {
            int idPoint;
            MyPointList myPointList = MyPointList.getInstance();
            int.TryParse(row.ItemArray[0].ToString(), out idPoint);
            Point = myPointList.getItem(idPoint);
        }

        internal override void Delete()
        {
            _provider.Delete("SuppyAddress", ID);
        }

        internal override object[] getRow()
        {
            return new object[] { Point.ID, Region, Point.Name };
        }

        public override void Save()
        {
            _provider.Insert("SuppyAddress", Point.ID);

            SuppyAddressList suppyAddressList = SuppyAddressList.getInstance();
            suppyAddressList.Add(this);
        }

        public override string ToString()
        {
            return string.Concat("г. ", Region, " ", Point.Name);
        }

        public bool IsEqualsRegionID(int idRegion)
        {
            return ((Point != null) && (Point.RegionID == idRegion));
        }
    }
}
