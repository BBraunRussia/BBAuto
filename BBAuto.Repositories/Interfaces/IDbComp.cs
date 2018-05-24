using System.Collections.Generic;
using BBAuto.Repositories.Entities;
using Insight.Database;

namespace BBAuto.Repositories.Interfaces
{
  [Sql(Schema = "dbo")]
  public interface IDbComp
  {
    void DeleteComp(int id);
    DbDictionary GetCompById(int id);
    IList<DbDictionary> GetComps();
    DbDictionary UpsertComp(DbDictionary model);
  }
}
