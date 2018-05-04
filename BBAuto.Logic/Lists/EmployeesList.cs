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

    public void Delete(Region region, int idEmployeesName)
    {
      Employees employees = getItem(region, idEmployeesName);

      list.Remove(employees);

      employees.Delete();
    }

    public Employees getItem(Region region, string EmployeesName, bool allowNull = false)
    {
      int idEmployeesName = 0;
      EmployeesNames employeesNames = EmployeesNames.getInstance();
      idEmployeesName = employeesNames.getItem(EmployeesName);

      return getItem(region, idEmployeesName, allowNull);
    }

    private Employees getItem(Region region, int idEmployeesName, bool allowNull = false)
    {
      List<Employees> EmployeesList = getList(region, idEmployeesName);
      Employees employees;

      if (EmployeesList.Count() > 0)
        employees = EmployeesList.First() as Employees;
      else if (allowNull)
        return null;
      else
      {
        RegionList regionList = RegionList.getInstance();
        region = regionList.getItem(1);

        EmployeesList = getList(region, idEmployeesName);

        if (EmployeesList.Count() > 0)
          employees = EmployeesList.First() as Employees;
        else
          employees = new Employees();
      }

      return employees;
    }

    private List<Employees> getList(Region region, int idEmployeesName)
    {
      var EmployeesList = from employees in list
        where employees.Region == region && employees.IdEmployeesName == idEmployeesName.ToString()
        select employees;

      return EmployeesList.ToList();
    }

    public DataTable ToDataTable()
    {
      DataTable dt = new DataTable();
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
