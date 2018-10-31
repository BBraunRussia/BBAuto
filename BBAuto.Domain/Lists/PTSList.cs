using System.Linq;
using System.Data;
using BBAuto.Domain.ForCar;
using BBAuto.Domain.Abstract;
using BBAuto.Domain.Entities;

namespace BBAuto.Domain.Lists
{
  public class PTSList : MainList<PTS>
  {
    private static PTSList _uniqueInstance;

    public static PTSList getInstance()
    {
      return _uniqueInstance ?? (_uniqueInstance = new PTSList());
    }

    protected override void LoadFromSql()
    {
      DataTable dt = Provider.Select("PTS");

      foreach (DataRow row in dt.Rows)
      {
        PTS pts = new PTS(row);
        Add(pts);
      }
    }

    public void Delete(Car car)
    {
      PTS pts = getItem(car);

      _list.Remove(pts);

      pts.Delete();
    }

    public PTS getItem(Car car)
    {
      var PTSs = _list.Where(item => item.Car.ID == car.ID);

      return (PTSs.Count() > 0) ? PTSs.First() : car.createPTS();
    }
  }
}
