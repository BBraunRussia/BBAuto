using System.Security.Principal;
using BBAuto.Logic.Entities;
using BBAuto.Logic.Lists;

namespace BBAuto.Logic.Static
{
  public static class User
  {
    private static Driver _driver;

    public static bool Login()
    {
      var login = WindowsIdentity.GetCurrent().Name.Replace("\\", "-");
      var s2 = login.Split('-');
      login = s2[1];
      login = "Next";
      login = "shelmaru";
      //login = "boganaru";
      //login = "stolekru";            
      //login = "mikhmrru";
      var driverList = DriverList.getInstance();
      _driver = driverList.getItem(login);

      return _driver != null && GetRole() != RolesList.Employee;
    }

    public static Driver GetDriver()
    {
      return _driver;
    }

    public static bool IsFullAccess()
    {
      return _driver.UserRole == RolesList.Adminstrator
             || _driver.UserRole == RolesList.Boss
             || _driver.UserRole == RolesList.Editor
             || _driver.UserRole == RolesList.proxyBoss;
    }

    public static RolesList GetRole()
    {
      return _driver.UserRole;
    }
  }
}
