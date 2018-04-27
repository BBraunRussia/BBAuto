using System.Collections.Generic;
using BBAuto.Repositories.Entities;
using Insight.Database;

namespace BBAuto.Repositories.Interfaces
{
  [Sql(Schema = "dbo")]
  public interface IDbCar
  {
    IList<DbCar> GetCars();
    DbCar GetCarById(int id);

    void DeleteCar(DbCar car);
    DbCar UpsertCar(DbCar car);
  }
}
