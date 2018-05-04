using System.Collections.Generic;
using System.Data;
using System.Linq;
using BBAuto.Logic.Abstract;
using BBAuto.Logic.Common;

namespace BBAuto.Logic.Lists
{
  public class TemplateList : MainList
  {
    private static TemplateList uniqueInstance;

    private List<Template> list;

    private TemplateList()
    {
      list = new List<Template>();

      LoadFromSql();
    }

    public static TemplateList getInstance()
    {
      if (uniqueInstance == null)
        uniqueInstance = new TemplateList();

      return uniqueInstance;
    }

    protected override void LoadFromSql()
    {
      DataTable dt = Provider.Select("Template");

      foreach (DataRow row in dt.Rows)
      {
        Template template = new Template(row);
        Add(template);
      }
    }

    public void Add(Template template)
    {
      if (list.Exists(item => item.Id == template.Id))
        return;

      list.Add(template);
    }

    public void Delete(int idTemplate)
    {
      Template template = getItem(idTemplate);

      list.Remove(template);

      template.Delete();
    }

    public Template getItem(int id)
    {
      return list.FirstOrDefault(t => t.Id == id);
    }

    public Template getItem(string name)
    {
      return list.FirstOrDefault(t => t.Name == name);
    }

    public DataTable ToDataTable()
    {
      DataTable dt = new DataTable();
      dt.Columns.Add("id");
      dt.Columns.Add("Название");
      dt.Columns.Add("Файл");

      foreach (var item in list)
      {
        dt.Rows.Add(item.ToRow());
      }

      return dt;
    }
  }
}
