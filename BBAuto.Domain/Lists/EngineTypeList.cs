using System.Linq;
using System.Data;
using BBAuto.Domain.Abstract;
using BBAuto.Domain.Tables;

namespace BBAuto.Domain.Lists
{
  public class EngineTypeList : MainList<EngineType>
  {
    private static EngineTypeList _uniqueInstance;

    public static EngineTypeList getInstance()
    {
      return _uniqueInstance ?? (_uniqueInstance = new EngineTypeList());
    }

    protected override void LoadFromSql()
    {
      _list.Clear();

      DataTable dt = Provider.Select("EngineType");

      foreach (DataRow row in dt.Rows)
      {
        EngineType engineType = new EngineType(row);
        Add(engineType);
      }
    }
    
    public EngineType getItem(int id)
    {
      return _list.FirstOrDefault(et => et.ID == id);
    }
  }
}
