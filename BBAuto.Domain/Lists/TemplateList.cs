using System.Linq;
using System.Data;
using BBAuto.Domain.Common;
using BBAuto.Domain.Abstract;

namespace BBAuto.Domain.Lists
{
  public class TemplateList : MainList<Template>
  {
    private static TemplateList _uniqueInstance;

    public static TemplateList getInstance()
    {
      return _uniqueInstance ?? (_uniqueInstance = new TemplateList());
    }

    protected override void LoadFromSql()
    {
      var dt = Provider.Select("Template");
      
      foreach (DataRow row in dt.Rows)
      {
        var template = new Template(row);
        Add(template);
      }
    }
    
    public void Delete(int idTemplate)
    {
      var template = getItem(idTemplate);

      _list.Remove(template);

      template.Delete();
    }

    public Template getItem(int id)
    {
      return _list.FirstOrDefault(t => t.ID == id);
    }

    public Template getItem(string name)
    {
      return _list.FirstOrDefault(t => t.Name == name);
    }

    public DataTable ToDataTable()
    {
      LoadFromSql();

      var dt = new DataTable();
      dt.Columns.Add("id");
      dt.Columns.Add("Название");
      dt.Columns.Add("Файл");

      _list.ForEach(item => dt.Rows.Add(item.getRow()));
      
      return dt;
    }
  }
}
