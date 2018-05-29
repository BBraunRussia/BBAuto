using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using BBAuto.Domain;
using BBAuto.Domain.Static;
using BBAuto.Domain.Lists;
using BBAuto.Domain.Entities;

namespace BBAuto.Domain.Static
{
  public static class User
  {
    private static Driver _driver;

    public static bool Login()
    {
      string login = WindowsIdentity.GetCurrent().Name.Replace("\\", "-");
      String[] s2 = login.Split('-');
      login = s2[1];
      login = "shelmaru";
      //login = "boganaru";
      //login = "stolekru";            
      //login = "mikhmrru";
      DriverList driverList = DriverList.getInstance();
      _driver = driverList.getItem(login);

      return ((_driver != null) && (User.GetRole() != RolesList.Employee));
    }

    public static Driver getDriver()
    {
      return _driver;
    }

    public static bool IsFullAccess()
    {
      return ((_driver.UserRole == RolesList.Adminstrator)
              || (_driver.UserRole == RolesList.Boss)
              || (_driver.UserRole == RolesList.Editor)
              || (_driver.UserRole == RolesList.proxyBoss));
    }

    public static RolesList GetRole()
    {
      return _driver.UserRole;
    }
  }
}
