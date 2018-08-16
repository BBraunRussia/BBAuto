using System.Collections.Generic;
using System.Linq;
using System.Data;
using BBAuto.Domain.Abstract;
using BBAuto.Domain.ForDriver;
using BBAuto.Domain.Entities;

namespace BBAuto.Domain.Lists
{
  public class InstractionList : MainList
  {
    private static InstractionList _uniqueInstance;
    private readonly List<Instraction> _list;

    private InstractionList()
    {
      _list = new List<Instraction>();

      loadFromSql();
    }

    public static InstractionList getInstance()
    {
      return _uniqueInstance ?? (_uniqueInstance = new InstractionList());
    }

    protected override void loadFromSql()
    {
      DataTable dt = _provider.Select("Instraction");

      foreach (DataRow row in dt.Rows)
      {
        Instraction instraction = new Instraction(row);
        Add(instraction);
      }
    }

    public void Add(Instraction instraction)
    {
      if (_list.Exists(item => item == instraction))
        return;

      _list.Add(instraction);
    }

    public DataTable ToDataTable()
    {
      return CreateTable(_list);
    }

    public DataTable ToDataTable(Driver driver)
    {
      return CreateTable(_list.Where(i => i.Driver.ID == driver.ID));
    }

    private DataTable CreateTable(IEnumerable<Instraction> instractions)
    {
      DataTable dt = new DataTable();
      dt.Columns.Add("id");
      dt.Columns.Add("Номер");
      dt.Columns.Add("Дата инструктажа");

      foreach (Instraction instraction in instractions)
        dt.Rows.Add(instraction.getRow());

      return dt;
    }

    public Instraction getItem(int id)
    {
      return _list.FirstOrDefault(i => i.ID == id);
    }

    public Instraction getItem(Driver driver)
    {
      return _list.FirstOrDefault(i => i.Driver.ID == driver.ID);
    }

    public void Delete(int idInstraction)
    {
      Instraction instraction = getItem(idInstraction);

      _list.Remove(instraction);

      instraction.Delete();
    }
  }
}
