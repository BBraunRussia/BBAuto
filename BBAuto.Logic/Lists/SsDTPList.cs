using System.Collections.Generic;
using System.Data;
using System.Linq;
using BBAuto.Logic.Abstract;
using BBAuto.Logic.ForCar;

namespace BBAuto.Logic.Lists
{
  public class SsDTPList : MainList
  {
    private static SsDTPList uniqueInstance;
    private List<SsDTP> list;

    private SsDTPList()
    {
      list = new List<SsDTP>();

      LoadFromSql();
    }

    public static SsDTPList getInstance()
    {
      if (uniqueInstance == null)
        uniqueInstance = new SsDTPList();

      return uniqueInstance;
    }

    protected override void LoadFromSql()
    {
      DataTable dt = Provider.Select("ssDTP");

      ClearList();

      foreach (DataRow row in dt.Rows)
      {
        var ssDtp = new SsDTP(row);
        Add(ssDtp);
      }
    }

    public void Add(SsDTP ssDtp)
    {
      if (list.Exists(item => item == ssDtp))
        return;

      list.Add(ssDtp);
    }

    private void ClearList()
    {
      if (list.Count > 0)
        list.Clear();
    }

    public SsDTP GetItem(int markId)
    {
      return list.FirstOrDefault(item => item.MarkId == markId);
    }

    public DataTable ToDataTable()
    {
      var dt = new DataTable();
      dt.Columns.Add("id");
      dt.Columns.Add("Марка");
      dt.Columns.Add("СТО");

      foreach (var ssDtp in list)
      {
        dt.Rows.Add(ssDtp.ToRow());
      }

      return dt;
    }

    public void Delete(int markId)
    {
      var ssDtp = GetItem(markId);

      list.Remove(ssDtp);

      ssDtp.Delete();
    }
  }
}
