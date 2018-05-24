using System.Collections.Generic;
using BBAuto.Repositories.Entities;
using Insight.Database;

namespace BBAuto.Repositories.Interfaces
{
  [Sql(Schema = "dbo")]
  public interface IDbViolationType
  {
    void DeleteViolationType(int id);
    DbDictionary GetViolationTypeById(int id);
    IList<DbDictionary> GetViolationTypes();
    DbDictionary UpsertViolationType(DbDictionary model);
  }
}
