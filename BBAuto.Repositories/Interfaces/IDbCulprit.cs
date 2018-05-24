using System.Collections.Generic;
using BBAuto.Repositories.Entities;
using Insight.Database;

namespace BBAuto.Repositories.Interfaces
{
  [Sql(Schema = "dbo")]
  public interface IDbCulprit
  {
    void DeleteCulprit(int id);
    DbDictionary GetCulpritById(int id);
    IList<DbDictionary> GetCulprits();
    DbDictionary UpsertCulprit(DbDictionary model);
  }
}
