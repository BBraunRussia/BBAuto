using System.Linq;
using System.Data;
using BBAuto.Domain.Abstract;
using BBAuto.Domain.Tables;

namespace BBAuto.Domain.Lists
{
  public class MarkList : MainList<Mark>
  {
    private static MarkList _uniqueInstance;

    public static MarkList getInstance()
    {
      return _uniqueInstance ?? (_uniqueInstance = new MarkList());
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
    
    public Mark getItem(int id)
    {
      return _list.FirstOrDefault(m => m.ID == id);
    }
  }
}
