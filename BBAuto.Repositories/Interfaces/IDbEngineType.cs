using System.Collections.Generic;
using BBAuto.Repositories.Entities;
using Insight.Database;

namespace BBAuto.Repositories.Interfaces
{
  [Sql(Schema = "dbo")]
  public interface IDbEngineType
  {
    void DeleteEngineType(int id);
    IList<DbEngineType> GetEngineTypes();
    DbEngineType GetEngineTypeById(int id);
    DbDictionary UpsertEngineType(DbDictionary model);
  }
}
