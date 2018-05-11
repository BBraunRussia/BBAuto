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
    IList<DbDiagCard> GetDiagCardById(int idCar);
    DbDiagCard UpsertDiagCard(DbDiagCard diagCard);
  }
}