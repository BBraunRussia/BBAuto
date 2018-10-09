using System.Data;
using BBAuto.Domain.Abstract;
using BBAuto.Domain.Lists;
using BBAuto.Domain.Dictionary;
using BBAuto.Domain.Static;
using BBAuto.Domain.Entities;

namespace BBAuto.Domain.ForDriver
{
  public class UserAccess : MainDictionary
  {
    public int RoleID { get; set; }

    public Driver Driver
    {
      get { return DriverList.getInstance().getItem(ID); }
      set { ID = value.ID; }
    }

    public string Role
    {
      get
      {
        if (RoleID == 0)
          return "Нет доступа";

        Roles roles = Roles.getInstance();
        return roles.getItem(RoleID);
      }
    }

    public UserAccess()
    {
      ID = 0;
      RoleID = 0;
    }

    internal UserAccess(DataRow row)
    {
      fillFields(row);
    }

    private void fillFields(DataRow row)
    {
      int id;
      int.TryParse(row.ItemArray[0].ToString(), out id);
      ID = id;

      int idRole;
      int.TryParse(row.ItemArray[1].ToString(), out idRole);
      RoleID = idRole;
    }

    internal override object[] getRow()
    {
      return new object[] {ID, Driver.Login, Driver.FullName, Role};
    }

    internal override void Delete()
    {
      _provider.DoOther("exec UserAccess_Delete @p1, @p2", ID, RoleID);
    }

    public override void Save()
    {
      _provider.Insert("UserAccess", ID, RoleID);
    }
  }
}
