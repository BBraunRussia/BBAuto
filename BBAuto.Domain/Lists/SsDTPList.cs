using System.Collections.Generic;
using System.Linq;
using System.Data;
using BBAuto.Domain.ForCar;
using BBAuto.Domain.Abstract;

namespace BBAuto.Domain.Lists
{
  public class SsDTPList : MainList
  {
    private static SsDTPList _uniqueInstance;
    private readonly List<SsDTP> _list;

    private SsDTPList()
    {
      _list = new List<SsDTP>();

      loadFromSql();
    }

    public static SsDTPList getInstance()
    {
      return _uniqueInstance ?? (_uniqueInstance = new SsDTPList());
    }

    protected override void loadFromSql()
    {
      DataTable dt = _provider.Select("ssDTP");

      clearList();

      foreach (DataRow row in dt.Rows)
      {
        SsDTP ssDTP = new SsDTP(row);
        Add(ssDTP);
      }
    }

    public void Add(SsDTP ssDTP)
    {
      if (_list.Exists(item => item == ssDTP))
        return;

      _list.Add(ssDTP);
    }

    private void clearList()
    {
      if (_list.Count > 0)
        _list.Clear();
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
