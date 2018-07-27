using System.Collections.Generic;
using BBAuto.Repository.Models;
using Insight.Database;

namespace BBAuto.Repository.Interfaces
{
  [Sql(Schema = "dbo")]
  public interface IDbDriverTransponder
  {
    IList<DbDriverTransponder> GetDriverTranspondersByTransponderId(int transponderId);

    DbDriverTransponder UpsertDriverTransponder(DbDriverTransponder transponder);

    DbDriverTransponder GetDriverTransponderById(int id);

    void DeleteDriverTransponder(int id);
  }
}
