using System.Linq;
using System.Data;
using BBAuto.Domain.Abstract;
using BBAuto.Domain.ForCar;

namespace BBAuto.Domain.Lists
{
  public class DealerList : MainList<Diler>
  {
    private static DealerList _uniqueInstance;
    
    public static DealerList getInstance()
    {
      return _uniqueInstance ?? (_uniqueInstance = new DealerList());
    }

    protected override void LoadFromSql()
    {
      DataTable dt = Provider.Select("Diller");

      foreach (DataRow row in dt.Rows)
      {
        Diler diller = new Diler(row);
        Add(diller);
      }
    }
    
    public void Delete(int idDiller)
    {
      Diler diller = getItem(idDiller);

      _list.Remove(diller);

      diller.Delete();
    }

    public Diler getItem(int id)
    {
      return _list.FirstOrDefault(d => d.ID == id);
    }

    private DataTable createTable()
    {
      DataTable dt = new DataTable();
      dt.Columns.Add("id");
      dt.Columns.Add("Название");
      dt.Columns.Add("Контакты");

      return dt;
    }

    public DataTable ToDataTable()
    {
      DataTable dt = createTable();

      foreach (Diler diller in _list)
        dt.Rows.Add(diller.getRow());

      return dt;
    }
  }
}
