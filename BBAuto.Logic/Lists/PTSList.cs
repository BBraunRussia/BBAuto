using System.Collections.Generic;
using System.Data;
using System.Linq;
using BBAuto.Logic.Abstract;
using BBAuto.Logic.Entities;
using BBAuto.Logic.ForCar;

namespace BBAuto.Logic.Lists
{
  public class PTSList : MainList
  {
    private List<PTS> list;
    private static PTSList uniqueInstance;

    private PTSList()
    {
      list = new List<PTS>();

      LoadFromSql();
    }

    public static PTSList getInstance()
    {
      if (uniqueInstance == null)
        uniqueInstance = new PTSList();

      return uniqueInstance;
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
      return list.FirstOrDefault(item => item.CarId == car.Id) ?? car.createPTS();
    }

    public PTS getItem(int carId)
    {
      return list.FirstOrDefault(item => item.CarId == carId) ?? new PTS(carId);
    }
  }
}
