using System.Collections.Generic;
using BBAuto.Repositories.Entities;
using Insight.Database;

namespace BBAuto.Repositories.Interfaces
{
  [Sql(Schema = "dbo")]
  public interface IDbTempMove
  {
    IList<DbTempMove> GetTempMoves();
    DbTempMove GetTempMoveById(int id);
    DbTempMove UpsertTempMove(DbTempMove tempMove);
  }
}
