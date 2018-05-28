using System.Collections.Generic;
using System.Data;
using System.Linq;
using BBAuto.Logic.Abstract;
using BBAuto.Logic.Common;
using BBAuto.Logic.Dictionary;
using BBAuto.Logic.Tables;

namespace BBAuto.Logic.Lists
{
  public class EmployeesList : MainList
  {
    private static EmployeesList uniqueInstance;
    private List<Employees> list;

    private EmployeesList()
    {
      list = new List<Employees>();

      LoadFromSql();
    }

    public static EmployeesList getInstance()
    {
      if (uniqueInstance == null)
        uniqueInstance = new EmployeesList();

      return uniqueInstance;
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

    public void Add(Employees employees)
    {
      if (list.Exists(item => item == employees))
        return;

      list.Add(employees);
    }

    public void Delete(int regionId, int idEmployeesName)
    {
      Employees employees = getItem(regionId, idEmployeesName);

      list.Remove(employees);

      employees.Delete();
    }

    public Employees getItem(int regionId, string EmployeesName, bool allowNull = false)
    {
      var idEmployeesName = 0;
      EmployeesNames employeesNames = EmployeesNames.getInstance();
      idEmployeesName = employeesNames.getItem(EmployeesName);

      return getItem(regionId, idEmployeesName, allowNull);
    }

    private Employees getItem(int regionId, int idEmployeesName, bool allowNull = false)
    {
      var employeesList = getList(regionId, idEmployeesName);
      Employees employees;

      if (employeesList.Any())
        employees = employeesList.First();
      else if (allowNull)
        return null;
      else
      {
        var regionList = RegionList.getInstance();
        regionId = 1;

        employeesList = getList(regionId, idEmployeesName);

        employees = employeesList.FirstOrDefault();
      }

      return employees;
    }

    private List<Employees> getList(int regionId, int idEmployeesName)
    {
      var employeesList = from employees in list
        where employees.Region.Id == regionId && employees.IdEmployeesName == idEmployeesName.ToString()
        select employees;

      return employeesList.ToList();
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
      foreach (Employees employees in list.ToList()) //empList.ToList())
        dt.Rows.Add(employees.ToRow());

      return dt;
    }
  }
}
