using System.Collections.Generic;
using System.Linq;
using System.Data;
using BBAuto.Domain.Abstract;
using BBAuto.Domain.ForCar;

namespace BBAuto.Domain.Lists
{
  public class ModelList : MainList<Model>
  {
    private static ModelList _uniqueInstance;

    public static ModelList getInstance()
    {
      return _uniqueInstance ?? (_uniqueInstance = new ModelList());
    }

    protected override void LoadFromSql()
    {
      DataTable dt = Provider.Select("Model");
      
      foreach (DataRow row in dt.Rows)
      {
        Model model = new Model(row);
        Add(model);
      }
    }

    public Model getItem(int id)
    {
      return _list.FirstOrDefault(m => m.ID == id);
    }

    public void Delete(int idModel)
    {
      Model model = getItem(idModel);

      _list.Remove(model);

      model.Delete();
    }

    public DataTable ToDataTable(int idMark)
    {
      DataTable dt = new DataTable();
      dt.Columns.Add("id");
      dt.Columns.Add("Название");

      foreach (Model model in _list.Where(m => m.MarkID == idMark))
      {
        dt.Rows.Add(model.getRow());
      }

      return dt;
    }
  }
}
