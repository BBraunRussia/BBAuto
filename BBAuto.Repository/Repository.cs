using System.Configuration;
using System.Data;
using Insight.Database;

namespace BBAuto.Repository
{
  public class Repository<T> : IRepository<T> where T : class, IDbConnection, IDbTransaction
  {
    private readonly ConnectionStringSettings _settings;

    public T DbRepository => _settings.As<T>();

    static Repository()
    {
      SqlInsightDbProvider.RegisterProvider();
    }

    public Repository(ConnectionStringSettings connectionStringSettings)
    {
      _settings = connectionStringSettings;
    }
  }
}
