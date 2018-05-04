using System.Collections.Generic;
using System.Data;
using System.Linq;
using BBAuto.Logic.Abstract;
using BBAuto.Logic.Entities;
using BBAuto.Logic.ForDriver;

namespace BBAuto.Logic.Lists
{
  public class InstractionList : MainList
  {
    private static InstractionList uniqueInstance;
    private List<Instraction> list;

    private InstractionList()
    {
      list = new List<Instraction>();

      LoadFromSql();
    }

    public static InstractionList getInstance()
    {
      if (uniqueInstance == null)
        uniqueInstance = new InstractionList();

      return uniqueInstance;
    }

    protected override void LoadFromSql()
    {
      DataTable dt = Provider.Select("Instraction");

      foreach (DataRow row in dt.Rows)
      {
        Instraction instraction = new Instraction(row);
        Add(instraction);
      }
    }

    public void Add(Instraction instraction)
    {
      if (list.Exists(item => item == instraction))
        return;

      list.Add(instraction);
    }

    public DataTable ToDataTable()
    {
      return CreateTable(list);
    }

    public DataTable ToDataTable(Driver driver)
    {
      return CreateTable(list.Where(i => i.Driver.Id == driver.Id));
    }

    private DataTable CreateTable(IEnumerable<Instraction> instractions)
    {
      DataTable dt = new DataTable();
      dt.Columns.Add("id");
      dt.Columns.Add("Номер");
      dt.Columns.Add("Дата инструктажа");

      foreach (Instraction instraction in instractions)
        dt.Rows.Add(instraction.ToRow());

      return dt;
    }

    public Instraction getItem(int id)
    {
      return list.FirstOrDefault(i => i.Id == id);
    }

    public Instraction getItem(Driver driver)
    {
      return list.FirstOrDefault(i => i.Driver.Id == driver.Id);
    }

    public void Delete(int idInstraction)
    {
      Instraction instraction = getItem(idInstraction);

      list.Remove(instraction);

      instraction.Delete();
    }
  }
}
