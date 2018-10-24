using BBAuto.Domain.Common;
using BBAuto.Domain.ForCar;
using System.Data;

namespace BBAuto.Domain.Dictionary
{
  public class Culprits : MyDictionary
  {
    private static Culprits _uniqueInstance;

    public static Culprits GetInstance()
    {
      return _uniqueInstance ?? (_uniqueInstance = new Culprits());
    }

    protected override void loadFromSql()
    {
      var dt = provider.Select("Culprit");

      fillList(dt);
    }

    public DataTable ToDataTable(DTP dtp)
    {
      var dt = new DataTable();
      dt.Columns.Add("id");
      dt.Columns.Add("Название");

      foreach (var item in dictionary)
        dt.Rows.Add(item.Key, item.Value);

      dt.Rows.Add(dtp.GetCulpit());

      return dt;
    }
  }
}
