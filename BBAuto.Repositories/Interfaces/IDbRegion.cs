using System.Collections.Generic;
using BBAuto.Repositories.Entities;
using Insight.Database;

namespace BBAuto.Repositories.Interfaces
{
  [Sql(Schema = "dbo")]
  public interface IDbRegion
  {
    void DeleteRegion(int id);
    DbDictionary GetRegionById(int id);
    IList<DbDictionary> GetRegions();
    DbDictionary UpsertRegion(DbDictionary model);
  }
}
