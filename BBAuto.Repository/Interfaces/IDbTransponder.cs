using System.Collections.Generic;
using BBAuto.Repository.Models;
using Insight.Database;

namespace BBAuto.Repository.Interfaces
{
  [Sql(Schema = "dbo")]
  public interface IDbTransponder
  {
    void DeleteTransponder(int id);

    DbTransponder GetTransponderById(int id);

    IList<DbReportTransponder> GetReportTransponderList();

    DbTransponder UpsertTransponder(DbTransponder transponder);
  }
}
