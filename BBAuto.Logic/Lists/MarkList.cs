using System.Collections.Generic;
using System.Data;
using System.Linq;
using BBAuto.Logic.Abstract;
using BBAuto.Logic.Tables;

namespace BBAuto.Logic.Lists
{
  public class MarkList : MainList
  {
    private static MarkList uniqueInstance;
    private List<Mark> list;

    private MarkList()
    {
      list = new List<Mark>();

      LoadFromSql();
    }

    public static MarkList getInstance()
    {
      if (uniqueInstance == null)
        uniqueInstance = new MarkList();

      return uniqueInstance;
    }

    protected override void LoadFromSql()
    {
      DataTable dt = Provider.Select("Mark");

      foreach (DataRow row in dt.Rows)
      {
        Mark mark = new Mark(row);
        Add(mark);
      }
    }

    public void Add(Mark mark)
    {
      if (list.Exists(item => item.Id == mark.Id))
        return;

      list.Add(mark);
    }

    public Mark getItem(int? id)
    {
      return list.FirstOrDefault(m => id.HasValue && m.Id == id);
    }
  }
}
