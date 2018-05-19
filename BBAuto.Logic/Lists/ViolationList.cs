using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using BBAuto.Logic.Abstract;
using BBAuto.Logic.Entities;
using BBAuto.Logic.ForCar;

namespace BBAuto.Logic.Lists
{
  public class ViolationList : MainList
  {
    private static ViolationList uniqueInstance;
    private List<Violation> list;

    private ViolationList()
    {
      list = new List<Violation>();

      LoadFromSql();
    }

    public static ViolationList getInstance()
    {
      if (uniqueInstance == null)
        uniqueInstance = new ViolationList();

      return uniqueInstance;
    }

    protected override void LoadFromSql()
    {
      if (list.Count > 0)
        return;

      DataTable dt = Provider.Select("Violation");

      foreach (DataRow row in dt.Rows)
      {
        Violation violation = new Violation(row.ItemArray);
        Add(violation);
      }
    }

    public void Add(Violation violation)
    {
      if (list.Exists(item => item.Id == violation.Id))
        return;

      list.Add(violation);
    }

    public Violation getItem(int id)
    {
      var violations = list.Where(item => item.Id == id);

      return (violations.Count() > 0) ? violations.First() : null;
    }

    public Violation getItem(Driver driver)
    {
      return list.FirstOrDefault(item => item.getDriver().Id == driver.Id);
    }

    public DataTable ToDataTable()
    {
      var violations = list.OrderByDescending(item => item.Date);

      return createTable(violations);
    }

    public DataTable ToDataTableAccount()
    {
      var violations = list
        .Where(v => v.DateCreate < DateTime.Today.AddDays(-5) && v.DatePay == null)
        .OrderByDescending(item => item.Date);

      return CreateTableAccount(violations);
    }

    public DataTable ToDataTable(int carId)
    {
      var violations = from violation in list
        where violation.CarId == carId
        orderby violation.Date descending
        select violation;

      return createTable(violations.ToList());
    }

    public DataTable ToDataTable(Driver driver)
    {
      var violations = from violation in list
        where violation.getDriver().Id == driver.Id
        orderby violation.Date descending
        select violation;

      return createTable(violations.ToList());
    }

    private DataTable createTable(IEnumerable<Violation> violations)
    {
      DataTable dt = new DataTable();
      dt.Columns.Add("id");
      dt.Columns.Add("idCar");
      dt.Columns.Add("Бортовой номер");
      dt.Columns.Add("Регистрационный знак");
      dt.Columns.Add("Регион");
      dt.Columns.Add("Дата", Type.GetType("System.DateTime"));
      dt.Columns.Add("Водитель");
      dt.Columns.Add("№ постановления");
      dt.Columns.Add("Дата оплаты", Type.GetType("System.DateTime"));
      dt.Columns.Add("Тип нарушения");
      dt.Columns.Add("Сумма штрафа", Type.GetType("System.Int32"));

   //   foreach (Violation violation in violations)
  //      dt.Rows.Add(violation.ToRow());

      return dt;
    }

    private DataTable CreateTableAccount(IEnumerable<Violation> violations)
    {
      DataTable dt = new DataTable();
      dt.Columns.Add("id");
      dt.Columns.Add("idCar");
      dt.Columns.Add("Бортовой номер");
      dt.Columns.Add("Регистрационный знак");
      dt.Columns.Add("№ постановления");
      dt.Columns.Add("Дата", Type.GetType("System.DateTime"));
      dt.Columns.Add("Водитель");
      dt.Columns.Add("Тип нарушения");
      dt.Columns.Add("Сумма штрафа", Type.GetType("System.Int32"));
      dt.Columns.Add("Согласование");

    //  foreach (Violation violation in violations)
    //    dt.Rows.Add(violation.GetRowAccount());

      return dt;
    }

    public void Delete(int idViolation)
    {
      Violation violation = getItem(idViolation);

      list.Remove(violation);

      //violation.Delete();
    }

    private IEnumerable<Violation> GetListViolationAccount()
    {
      return list.Where(v => v.DateCreate <= DateTime.Today.AddDays(-5) && v.DatePay == null);
    }

    internal IEnumerable<Violation> GetViolationForAccount()
    {
      return GetListViolationAccount().Where(v => v.DateCreate == DateTime.Today.AddDays(-5) && !v.Agreed);
    }
  }
}
