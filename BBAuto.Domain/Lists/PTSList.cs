using System.Linq;
using System.Data;
using BBAuto.Domain.ForCar;
using BBAuto.Domain.Abstract;

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
      var dt = Provider.Select("PTS");

      foreach (DataRow row in dt.Rows)
      {
        Add(new PTS(row));
      }
    }

    public void Delete(int carId)
    {
      var pts = _list.FirstOrDefault(item => item.CarId == carId);

      _list.Remove(pts);

      pts?.Delete();
    }

    public PTS getItem(int carId)
    {
      return _list.FirstOrDefault(item => item.CarId == carId) ?? new PTS(carId);
    }
  }
}
