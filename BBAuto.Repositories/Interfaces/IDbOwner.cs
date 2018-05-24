using System.Collections.Generic;
using BBAuto.Repositories.Entities;
using Insight.Database;

namespace BBAuto.Repositories.Interfaces
{
  [Sql(Schema = "dbo")]
  public interface IDbOwner
  {
    void DeleteOwner(int id);
    DbDictionary GetOwnerById(int id);
    IList<DbDictionary> GetOwners();
    DbDictionary UpsertOwner(DbDictionary model);
  }
}
