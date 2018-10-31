using System.Linq;
using System.Data;
using BBAuto.Domain.ForCar;
using BBAuto.Domain.Abstract;

namespace BBAuto.Domain.Lists
{
  public class SsDTPList : MainList<SsDTP>
  {
    private static SsDTPList _uniqueInstance;

    public static SsDTPList getInstance()
    {
      return _uniqueInstance ?? (_uniqueInstance = new SsDTPList());
    }

    protected override void LoadFromSql()
    {
      DataTable dt = Provider.Select("ssDTP");

      foreach (DataRow row in dt.Rows)
      {
        SsDTP ssDTP = new SsDTP(row);
        Add(ssDTP);
      }
    }
   
    public SsDTP getItem(int markId)
    {
      return _list.FirstOrDefault(item => item.MarkId == markId);
    }

    public DataTable ToDataTable()
    {
      DataTable dt = new DataTable();
      dt.Columns.Add("id");
      dt.Columns.Add("Марка");
      dt.Columns.Add("СТО");

      foreach (SsDTP ssDTP in _list)
      {
        dt.Rows.Add(ssDTP.getRow());
      }

      return dt;
    }

    public void Delete(int markId)
    {
      var ssDTP = getItem(markId);

      _list.Remove(ssDTP);

      ssDTP.Delete();
    }
  }
}
