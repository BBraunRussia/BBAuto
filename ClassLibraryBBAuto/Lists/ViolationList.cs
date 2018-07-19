using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using BBAuto.Domain.ForCar;
using BBAuto.Domain.Abstract;
using BBAuto.Domain.Entities;

namespace BBAuto.Domain.Lists
{
  public class ViolationList : MainList
  {
    private static ViolationList _uniqueInstance;
    private readonly List<Violation> _list;

    private ViolationList()
    {
      _list = new List<Violation>();

      loadFromSql();
    }

    public static ViolationList getInstance()
    {
      return _uniqueInstance ?? (_uniqueInstance = new ViolationList());
    }

    protected override void loadFromSql()
    {
      if (_list.Any())
        return;

      DataTable dt = _provider.Select("Violation");

      foreach (DataRow row in dt.Rows)
      {
        Violation violation = new Violation(row.ItemArray);
        Add(violation);
      }
    }

    public void Add(Violation violation)
    {
      if (_list.Exists(item => item.ID == violation.ID))
        return;

      _list.Add(violation);
    }

    public Violation getItem(int id)
    {
      return _list.FirstOrDefault(item => item.ID == id);
    }

    public Violation getItem(Driver driver)
    {
      return _list.FirstOrDefault(item => item.getDriver().ID == driver.ID);
    }

    public DataTable ToDataTable()
    {
      var violations = _list.OrderByDescending(item => item.Date);

      return createTable(violations);
    }

    public DataTable ToDataTableAccount()
    {
      var violations = _list
        .Where(v => v.DateCreate < DateTime.Today.AddDays(-5) && v.DatePay == null)
        .OrderByDescending(item => item.Date);

      return CreateTableAccount(violations);
    }

    public DataTable ToDataTable(Car car)
    {
      var violations = from violation in _list
        where violation.Car.ID == car.ID
        orderby violation.Date descending
        select violation;

      return createTable(violations.ToList());
    }

    public DataTable ToDataTable(Driver driver)
    {
      var violations = from violation in _list
        where violation.getDriver().ID == driver.ID
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
      dt.Columns.Add("Дата", typeof(DateTime));
      dt.Columns.Add("Водитель");
      dt.Columns.Add("№ постановления");
      dt.Columns.Add("Дата оплаты", typeof(DateTime));
      dt.Columns.Add("Тип нарушения");
      dt.Columns.Add("Сумма штрафа", typeof(int));

      foreach (Violation violation in violations)
        dt.Rows.Add(violation.getRow());

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
      dt.Columns.Add("Дата", typeof(DateTime));
      dt.Columns.Add("Водитель");
      dt.Columns.Add("Тип нарушения");
      dt.Columns.Add("Сумма штрафа", typeof(int));
      dt.Columns.Add("Согласование");

      foreach (var violation in violations)
        dt.Rows.Add(violation.GetRowAccount());

      return dt;
    }

    public void Delete(int idViolation)
    {
      Violation violation = getItem(idViolation);

      _list.Remove(violation);

      violation.Delete();
    }

    private IEnumerable<Violation> GetListViolationAccount()
    {
      return _list.Where(v => v.DateCreate <= DateTime.Today.AddDays(-5) && v.DatePay == null);
    }

    internal IEnumerable<Violation> GetViolationForAccount()
    {
      return GetListViolationAccount().Where(v => v.DateCreate == DateTime.Today.AddDays(-5) && !v.Agreed);
    }
  }
}
