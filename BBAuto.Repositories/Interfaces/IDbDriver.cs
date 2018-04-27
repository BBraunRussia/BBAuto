using BBAuto.Repositories.Entities;
using Insight.Database;

namespace BBAuto.Repositories.Interfaces
{
  [Sql(Schema = "dbo")]
  public interface IDbDriver
  {
    DbDriver GetDriversByCarId(int carId);
  }
}
