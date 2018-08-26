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
    private readonly List<Instruction> _list;

    private InstractionList()
    {
      _list = new List<Instruction>();

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
        Instruction instraction = new Instruction(row);
        Add(instraction);
      }
    }

    public void Add(Instruction instraction)
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
      return CreateTable(_list.Where(i => i.DriverId == driver.ID));
    }

    private DataTable CreateTable(IEnumerable<Instruction> instractions)
    {
      DataTable dt = new DataTable();
      dt.Columns.Add("id");
      dt.Columns.Add("Название");
      dt.Columns.Add("Дата инструктажа");

      foreach (Instruction instraction in instractions)
        dt.Rows.Add(instraction.getRow());

      return dt;
    }

    public Instruction getItem(int id)
    {
      return _list.FirstOrDefault(i => i.ID == id);
    }

    public Instruction getItem(Driver driver)
    {
      return _list.FirstOrDefault(i => i.DriverId == driver.ID);
    }

    public void Delete(int idInstraction)
    {
      Instruction instraction = getItem(idInstraction);

      _list.Remove(instraction);

      instraction.Delete();
    }
  }
}
