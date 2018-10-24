using System.Collections.Generic;
using System.Linq;
using System.Data;
using BBAuto.Domain.Common;
using BBAuto.Domain.Abstract;

namespace BBAuto.Domain.Lists
{
  public class TemplateList : MainList
  {
    private static TemplateList uniqueInstance;

    private List<Template> list;

    private TemplateList()
    {
      list = new List<Template>();

      loadFromSql();
    }

    public static TemplateList getInstance()
    {
      return uniqueInstance ?? (uniqueInstance = new TemplateList());
    }

    protected override void loadFromSql()
    {
      list.Clear();

      var dt = _provider.Select("Template");
      
      foreach (DataRow row in dt.Rows)
      {
        var template = new Template(row);
        Add(template);
      }
    }

    public void Add(Template template)
    {
      if (list.Exists(item => item.ID == template.ID))
        return;

      list.Add(template);
    }

    public void Delete(int idTemplate)
    {
      var template = getItem(idTemplate);

      list.Remove(template);

      template.Delete();
    }

    public Template getItem(int id)
    {
      return list.FirstOrDefault(t => t.ID == id);
    }

    public Template getItem(string name)
    {
      return list.FirstOrDefault(t => t.Name == name);
    }

    public DataTable ToDataTable()
    {
      loadFromSql();

      var dt = new DataTable();
      dt.Columns.Add("id");
      dt.Columns.Add("Название");
      dt.Columns.Add("Файл");

      list.ForEach(item => dt.Rows.Add(item.getRow()));
      
      return dt;
    }
  }
}
