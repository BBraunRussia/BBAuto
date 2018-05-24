using System.Collections.Generic;
using BBAuto.Repositories.Entities;
using Insight.Database;

namespace BBAuto.Repositories.Interfaces
{
  [Sql(Schema = "dbo")]
  public interface IDbServiceStantion
  {
    void DeleteServiceStantion(int id);
    DbDictionary GetServiceStantionById(int id);
    IList<DbDictionary> GetServiceStantions();
    DbDictionary UpsertServiceStantion(DbDictionary model);
  }
}
