using System.Data;
using BBAuto.Logic.Abstract;
using BBAuto.Logic.Dictionary;
using BBAuto.Logic.Lists;
using BBAuto.Logic.Tables;

namespace BBAuto.Logic.Common
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
      Id = 0;
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
      Provider.Delete("SuppyAddress", Id);
    }

    internal override object[] ToRow()
    {
      return new object[] {Point.Id, Region, Point.Name};
    }

    public override void Save()
    {
      Provider.Insert("SuppyAddress", Point.Id);

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
