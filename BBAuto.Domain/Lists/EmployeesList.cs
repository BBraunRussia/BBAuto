using System.Collections.Generic;
using System.Linq;
using System.Data;
using BBAuto.Domain.Tables;
using BBAuto.Domain.Common;
using BBAuto.Domain.Abstract;
using BBAuto.Domain.Dictionary;

namespace BBAuto.Domain.Lists
{
  public class EmployeesList : MainList<Employees>
  {
    private static EmployeesList _uniqueInstance;
    
    public static EmployeesList getInstance()
    {
      return _uniqueInstance ?? (_uniqueInstance = new EmployeesList());
    }

    protected override void LoadFromSql()
    {
      DataTable dt = Provider.Select("Employees");

      foreach (DataRow row in dt.Rows)
      {
        Employees employees = new Employees(row);
        Add(employees);
      }
    }
    
    public void Delete(Region region, int idEmployeesName)
    {
      Employees employees = getItem(region, idEmployeesName);

      _list.Remove(employees);

      employees.Delete();
    }

    public Employees getItem(Region region, string EmployeesName, bool allowNull = false)
    {
      var employeesNameId = EmployeesNames.getInstance().getItem(EmployeesName);
      
      return getItem(region, employeesNameId, allowNull);
    }

    private Employees getItem(Region region, int idEmployeesName, bool allowNull = false)
    {
      List<Employees> EmployeesList = GetList(region, idEmployeesName);
      Employees employees;

      if (EmployeesList.Any())
        employees = EmployeesList.First();
      else if (allowNull)
        return null;
      else
      {
        RegionList regionList = RegionList.getInstance();
        region = regionList.getItem(1);

        EmployeesList = GetList(region, idEmployeesName);

        employees = EmployeesList.Any()
          ? EmployeesList.First()
          : new Employees();
      }

      return employees;
    }

    private List<Employees> GetList(Region region, int employeesNameId)
    {
      return _list.Where(item => item.Region == region && item.EmployeesNameId == employeesNameId).ToList();
    }

    public DataTable ToDataTable()
    {
      var dt = new DataTable();
      dt.Columns.Add("idRegion");
      dt.Columns.Add("idEmployeesName");
      dt.Columns.Add("Регион");
      dt.Columns.Add("Должность");
      dt.Columns.Add("Фамилия");

      /** Не работает ОШИБКА - должен быть реализован IComparable интерфейс* /
      var empList = from employee in list
                    orderby employee.Region//, employee.EmployeeName
                    select employee;
      */
      foreach (var employees in _list.ToList()) //empList.ToList())
        dt.Rows.Add(employees.getRow());

      return dt;
    }
  }
}
