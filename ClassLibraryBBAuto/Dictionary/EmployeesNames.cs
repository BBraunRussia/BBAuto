using System.Data;
using BBAuto.Domain.Common;

namespace BBAuto.Domain.Dictionary
{
  public class EmployeesNames : MyDictionary
  {
    private static EmployeesNames uniqueInstance;

    public static EmployeesNames getInstance()
    {
      if (uniqueInstance == null)
        uniqueInstance = new EmployeesNames();

      return uniqueInstance;
    }

    protected override void loadFromSql()
    {
      DataTable dt = provider.Select("EmployeesName");

      FillList(dt);
    }
  }
}
