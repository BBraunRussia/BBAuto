using System.Collections.Generic;
using System.Linq;
using System.Data;
using BBAuto.Domain.ForDriver;
using BBAuto.Domain.Abstract;
using BBAuto.Domain.Static;

namespace BBAuto.Domain.Lists
{
  public class UserAccessList : MainList<UserAccess>
  {
    private static UserAccessList _uniqueInstance;

    public static UserAccessList getInstance()
    {
      return _uniqueInstance ?? (_uniqueInstance = new UserAccessList());
    }

    protected override void LoadFromSql()
    {
      DataTable dt = Provider.Select("UserAccess");

      foreach (DataRow row in dt.Rows)
      {
        UserAccess userAccess = new UserAccess(row);
        Add(userAccess);
      }
    }

    public void Delete(int idDriver)
    {
      UserAccess userAccess = getItem(idDriver);

      _list.Remove(userAccess);

      userAccess.Delete();
    }

    public UserAccess getItem(int id)
    {
      return _list.FirstOrDefault(item => item.Driver.ID == id);
    }

    public UserAccess getItem(RolesList role)
    {
      List<UserAccess> userAccesses = ToList(role);

      return (userAccesses != null) ? userAccesses.First() : new UserAccess();
    }

    public DataTable ToDataTable()
    {
      var dt = createTable();

      var drivers = _list.OrderBy(item => item.Driver.OwnerID).GroupBy(item => item.Driver.Login).Select(group => group.First());

      var userAccesses = _list.Where(driver => drivers.Contains(driver)).OrderBy(item => item.Driver.FullName).ToList();
      
      userAccesses.ForEach(userAccess => dt.Rows.Add(userAccess.getRow()));
      
      return dt;
    }
    
    private DataTable createTable()
    {
      DataTable dt = new DataTable();
      dt.Columns.Add("idDriver");
      dt.Columns.Add("login");
      dt.Columns.Add("ФИО");
      dt.Columns.Add("Роль");

      return dt;
    }

    public List<UserAccess> ToList(RolesList role)
    {
      int idRole = (int) role;

      List<UserAccess> userAccesses = _list.Where(item => item.RoleID == idRole).ToList();

      return (userAccesses.Count() > 0) ? userAccesses : null;
    }
  }
}
