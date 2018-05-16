using System.Collections.Generic;
using BBAuto.Repositories.Entities;
using Insight.Database;

namespace BBAuto.Repositories.Interfaces
{
  [Sql(Schema = "dbo")]
  public interface IDbSaleCar
  {
    IList<DbSaleCar> GetSaleCars();
    void DeleteSaleCar(int carId);
    void UpsertSaleCar(DbSaleCar model);
  }
}
