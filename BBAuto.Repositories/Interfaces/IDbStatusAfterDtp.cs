using System.Collections.Generic;
using BBAuto.Repositories.Entities;
using Insight.Database;

namespace BBAuto.Repositories.Interfaces
{
  [Sql(Schema = "dbo")]
  public interface IDbStatusAfterDtp
  {
    void DeleteStatusAfterDtp(int id);
    DbDictionary GetStatusAfterDtpById(int id);
    IList<DbDictionary> GetStatusAfterDtps();
    DbDictionary UpsertStatusAfterDtp(DbDictionary model);
  }
}
