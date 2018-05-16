using System.Collections.Generic;
using BBAuto.Repositories.Entities;
using Insight.Database;

namespace BBAuto.Repositories.Interfaces
{
  [Sql(Schema = "dbo")]
  public interface IDbCarDoc
  {
    void DeleteCarDoc(int id);
    DbCarDoc GetCarDocById(int id);
    IList<DbCarDoc> GetCarDocByCarId(int carId);
    DbCarDoc UpsertCarDoc(DbCarDoc carDoc);
  }
}
