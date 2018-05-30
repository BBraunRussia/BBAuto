using System.Collections.Generic;
using BBAuto.Repositories.Entities;
using Insight.Database;

namespace BBAuto.Repositories.Interfaces
{
  [Sql(Schema = "dbo")]
  public interface IDbDriver
  {
    IList<DbDriver> GetDrivers();
    DbDriver UpsertDriver(DbDriver driver);
    DbDriver GetDriverById(int id);
    DbDriver GetDriverByLogin(string login);
    IList<DbDriver> GetDriversByRoleId(int roleId);
  }
}
