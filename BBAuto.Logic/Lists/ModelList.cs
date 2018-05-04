using System.Collections.Generic;
using System.Data;
using System.Linq;
using BBAuto.Logic.Abstract;
using BBAuto.Logic.ForCar;

namespace BBAuto.Logic.Lists
{
  public class ModelList : MainList
  {
    private static ModelList uniqueInstance;
    private List<Model> list;

    private ModelList()
    {
      list = new List<Model>();

      LoadFromSql();
    }

    public static ModelList getInstance()
    {
      if (uniqueInstance == null)
        uniqueInstance = new ModelList();

      return uniqueInstance;
    }

    protected override void LoadFromSql()
    {
      DataTable dt = Provider.Select("Model");

      clearList();

      foreach (DataRow row in dt.Rows)
      {
        Model model = new Model(row);
        Add(model);
      }
    }

    public void Add(Model model)
    {
      if (list.Exists(item => item.Id == model.Id))
        return;

      list.Add(model);
    }

    private void clearList()
    {
      if (list.Count > 0)
        list.Clear();
    }

    public Model getItem(int id)
    {
      return list.FirstOrDefault(m => m.Id == id);
    }

    public void Delete(int idModel)
    {
      Model model = getItem(idModel);

      list.Remove(model);

      model.Delete();
    }

    public DataTable ToDataTable(int idMark)
    {
      DataTable dt = new DataTable();
      dt.Columns.Add("id");
      dt.Columns.Add("Название");

      foreach (Model model in list.Where(m => m.MarkId == idMark))
      {
        dt.Rows.Add(model.ToRow());
      }

      return dt;
    }
  }
}
