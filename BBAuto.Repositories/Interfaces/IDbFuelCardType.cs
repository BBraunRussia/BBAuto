using System.Collections.Generic;
using BBAuto.Repositories.Entities;
using Insight.Database;

namespace BBAuto.Repositories.Interfaces
{
  [Sql(Schema = "dbo")]
  public interface IDbFuelCardType
  {
    void DeleteFuelCardType(int id);
    DbDictionary GetFuelCardTypeById(int id);
    IList<DbDictionary> GetFuelCardTypes();
    DbDictionary UpsertFuelCardType(DbDictionary model);
  }
}
