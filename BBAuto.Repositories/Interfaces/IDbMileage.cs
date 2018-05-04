using System.Collections.Generic;
using BBAuto.Repositories.Entities;
using Insight.Database;

namespace BBAuto.Repositories.Interfaces
{
  [Sql(Schema = "dbo")]
  public interface IDbMileage
  {
    void DeleteMileage(int mileageId);
    IList<DbMileage> GetMileageByCarId(int carId);
    DbMileage GetMileageById(int id);
    DbMileage UpsertMileage(DbMileage mileage);
  }
}
