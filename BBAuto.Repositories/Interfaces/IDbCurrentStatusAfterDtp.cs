using System.Collections.Generic;
using BBAuto.Repositories.Entities;
using Insight.Database;

namespace BBAuto.Repositories.Interfaces
{
  [Sql(Schema = "dbo")]
  public interface IDbCurrentStatusAfterDtp
  {
    void DeleteCurrentStatusAfterDtp(int id);
    DbDictionary GetCurrentStatusAfterDtpById(int id);
    IList<DbDictionary> GetCurrentStatusAfterDtps();
    DbDictionary UpsertCurrentStatusAfterDtp(DbDictionary model);
  }
}
