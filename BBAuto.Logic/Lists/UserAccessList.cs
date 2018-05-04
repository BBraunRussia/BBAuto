using System.Collections.Generic;
using System.Data;
using System.Linq;
using BBAuto.Logic.Abstract;
using BBAuto.Logic.ForDriver;
using BBAuto.Logic.Static;

namespace BBAuto.Logic.Lists
{
  public class UserAccessList : MainList
  {
    private static UserAccessList uniqueInstance;
    private List<UserAccess> list;

    private UserAccessList()
    {
      list = new List<UserAccess>();

      LoadFromSql();
    }

    public static UserAccessList getInstance()
    {
      if (uniqueInstance == null)
        uniqueInstance = new UserAccessList();

      return uniqueInstance;
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

    public void Add(UserAccess userAccess)
    {
      if (list.Exists(item => item == userAccess))
        return;

      list.Add(userAccess);
    }

    public void Delete(int idDriver)
    {
      UserAccess userAccess = getItem(idDriver);

      list.Remove(userAccess);

      userAccess.Delete();
    }

    public UserAccess getItem(int id)
    {
      return list.FirstOrDefault(item => item.Driver.Id == id);
    }

    public UserAccess getItem(RolesList role)
    {
      List<UserAccess> userAccesses = ToList(role);

      return (userAccesses != null) ? userAccesses.First() : new UserAccess();
    }

    public DataTable ToDataTable()
    {
      DataTable dt = createTable();

      List<UserAccess> userAccesses = list.OrderBy(item => item.Driver.GetName(NameType.Full)).ToList();

      foreach (UserAccess userAccess in userAccesses)
        dt.Rows.Add(userAccess.ToRow());

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

      List<UserAccess> userAccesses = list.Where(item => item.RoleId == idRole).ToList();

      return (userAccesses.Count() > 0) ? userAccesses : null;
    }
  }
}
