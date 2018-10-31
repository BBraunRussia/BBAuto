using System.Linq;
using System.Data;
using BBAuto.Domain.ForCar;
using BBAuto.Domain.Abstract;
using BBAuto.Domain.Entities;

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
      DataTable dt = Provider.Select("STS");

      foreach (DataRow row in dt.Rows)
      {
        STS sts = new STS(row);
        Add(sts);
      }
    }

    public void Delete(Car car)
    {
      STS sts = getItem(car);

      _list.Remove(sts);

      sts.Delete();
    }

    public STS getItem(Car car)
    {
      var STSs = _list.Where(s => s.Car.ID == car.ID);

      return (STSs.Count() > 0) ? STSs.First() : car.createSTS();
    }
  }
}
