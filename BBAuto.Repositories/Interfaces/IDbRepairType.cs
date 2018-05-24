using System.Collections.Generic;
using BBAuto.Repositories.Entities;
using Insight.Database;

namespace BBAuto.Repositories.Interfaces
{
  [Sql(Schema = "dbo")]
  public interface IDbRepairType
  {
    void DeleteRepairType(int id);
    DbDictionary GetRepairTypeById(int id);
    IList<DbDictionary> GetRepairTypes();
    DbDictionary UpsertRepairType(DbDictionary model);
  }
}
