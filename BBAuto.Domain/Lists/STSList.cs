using System.Linq;
using System.Data;
using BBAuto.Domain.ForCar;
using BBAuto.Domain.Abstract;

namespace BBAuto.Domain.Lists
{
  public class STSList : MainList<STS>
  {
    private static STSList _uniqueInstance;

    public static STSList getInstance()
    {
      return _uniqueInstance ?? (_uniqueInstance = new STSList());
    }

    protected override void LoadFromSql()
    {
      var dt = Provider.Select("STS");

      foreach (DataRow row in dt.Rows)
      {
        Add(new STS(row));
      }
    }

    public void Delete(int carId)
    {
      var sts = _list.FirstOrDefault(s => s.CarId == carId);

      _list.Remove(sts);

      sts?.Delete();
    }

    public STS getItem(int carId)
    {
      return _list.FirstOrDefault(s => s.CarId == carId) ?? new STS(carId);
    }
  }
}
