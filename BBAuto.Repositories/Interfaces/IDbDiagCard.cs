using System.Collections.Generic;
using BBAuto.Repositories.Entities;
using Insight.Database;

namespace BBAuto.Repositories.Interfaces
{
  [Sql(Schema = "dbo")]
  public interface IDbDiagCard
  {
    void DeleteDiagCard(int id);
    IList<DbDiagCard> GetDiagCards();
    DbDiagCard GetDiagCardById(int id);
    DbDiagCard UpsertDiagCard(DbDiagCard diagCard);
  }
}
