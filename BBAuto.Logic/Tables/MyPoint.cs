using System.Data;
using BBAuto.Logic.Abstract;
using BBAuto.Logic.Dictionary;
using BBAuto.Logic.Lists;

namespace BBAuto.Logic.Tables
{
  public class MyPoint : MainDictionary
  {
    public int RegionID { get; set; }
    public string Name { get; set; }

    public MyPoint(int idRegion)
    {
      Id = 0;
      RegionID = idRegion;
      Name = string.Empty;
    }

    public MyPoint(DataRow row)
    {
      int id;
      int.TryParse(row[0].ToString(), out id);
      Id = id;

      int idRegion;
      int.TryParse(row[1].ToString(), out idRegion);
      RegionID = idRegion;

      Name = row[2].ToString();
    }

    public override void Save()
    {
      int id;
      int.TryParse(Provider.Insert("MyPoint", Id, RegionID, Name), out id);
      Id = id;

      MyPointList pointList = MyPointList.getInstance();
      pointList.Add(this);
    }

    internal override void Delete()
    {
      Provider.Delete("MyPoint", Id);
    }

    internal override object[] ToRow()
    {
      Regions regions = Regions.getInstance();

      return new object[] {Id, Name};
    }
  }
}
