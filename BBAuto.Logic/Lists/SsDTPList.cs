using System.Collections.Generic;
using System.Data;
using System.Linq;
using BBAuto.Logic.Abstract;
using BBAuto.Logic.ForCar;
using BBAuto.Logic.Tables;

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

      clearList();

      foreach (DataRow row in dt.Rows)
      {
        SsDTP ssDTP = new SsDTP(row);
        Add(ssDTP);
      }
    }

    public void Add(SsDTP ssDTP)
    {
      if (list.Exists(item => item == ssDTP))
        return;

      list.Add(ssDTP);
    }

    private void clearList()
    {
      if (list.Count > 0)
        list.Clear();
    }

    public SsDTP getItem(Mark mark)
    {
      return list.FirstOrDefault(item => item.Mark.Id == mark.Id);
    }

    public DataTable ToDataTable()
    {
      DataTable dt = new DataTable();
      dt.Columns.Add("id");
      dt.Columns.Add("Марка");
      dt.Columns.Add("СТО");

      foreach (SsDTP ssDTP in list)
      {
        dt.Rows.Add(ssDTP.ToRow());
      }

      return dt;
    }

    public void Delete(Mark mark)
    {
      SsDTP ssDTP = getItem(mark);

      list.Remove(ssDTP);

      ssDTP.Delete();
    }
  }
}
