using System.Data;
using BBAuto.Logic.Abstract;
using BBAuto.Logic.Dictionary;
using BBAuto.Logic.Entities;
using BBAuto.Logic.Lists;
using BBAuto.Logic.Static;

namespace BBAuto.Logic.ForDriver
{
  public class UserAccess : MainDictionary
  {
    public int RoleId { get; set; }

    public Driver Driver
    {
      get => DriverList.getInstance().getItem(Id);
      set => Id = value.Id;
    }

    public string Role
    {
      get
      {
        if (RoleId == 0)
          return "Нет доступа";

        var roles = Roles.getInstance();
        return roles.getItem(RoleId);
      }
    }

    public UserAccess()
    {
      Id = 0;
      RoleId = 0;
    }

    internal UserAccess(DataRow row)
    {
      fillFields(row);
    }

    private void fillFields(DataRow row)
    {
      int id;
      int.TryParse(row.ItemArray[0].ToString(), out id);
      Id = id;

      int idRole;
      int.TryParse(row.ItemArray[1].ToString(), out idRole);
      RoleId = idRole;
    }

    internal override object[] ToRow()
    {
      return new object[] {Id, Driver.Login, Driver.GetName(NameType.Full), Role};
    }

    internal override void Delete()
    {
      Provider.DoOther("exec UserAccess_Delete @p1, @p2", Id, RoleId);
    }

    public override void Save()
    {
      Provider.Insert("UserAccess", Id, RoleId);
    }
  }
}
