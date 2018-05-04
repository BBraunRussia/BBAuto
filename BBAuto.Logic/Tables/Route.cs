using System.Data;
using BBAuto.Logic.Abstract;
using BBAuto.Logic.Lists;

namespace BBAuto.Logic.Tables
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
      Id = id;

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
      if (Id == 0)
        return;

      int id;
      int.TryParse(Provider.Insert("Route", Id, MyPoint1.Id, MyPoint2.Id, Distance), out id);
      Id = id;

      RouteList.getInstance().Add(this);
    }

    internal override void Delete()
    {
      Provider.Delete("Route", Id);
    }

    internal override object[] ToRow()
    {
      return new object[] {Id, MyPoint2.Name, Distance};
    }

    internal object[] getRow(MyPoint myPoint1)
    {
      MyPoint myPoint = (MyPoint1.Id == myPoint1.Id) ? MyPoint2 : MyPoint1;

      return new object[] {Id, myPoint.Name, Distance};
    }
  }
}
