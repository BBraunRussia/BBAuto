using Insight.Database;

namespace BBAuto.Repositories.Interfaces
{
  [Sql(Schema = "dbo")]
  public interface ICarDoc
  {
    void DeleteCarDoc(int id);
  }
}
