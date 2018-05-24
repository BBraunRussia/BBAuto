using System.Collections.Generic;
using BBAuto.Repositories.Entities;
using Insight.Database;

namespace BBAuto.Repositories.Interfaces
{
  [Sql(Schema = "dbo")]
  public interface IDbProxyType
  {
    void DeleteProxyType(int id);
    DbDictionary GetProxyTypeById(int id);
    IList<DbDictionary> GetProxyTypes();
    DbDictionary UpsertProxyType(DbDictionary model);
  }
}
