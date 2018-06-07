using System;
using System.Data;
using System.Data.Common;
using BBAuto.Repository.Interfaces;
using Common;
using Insight.Database;

namespace BBAuto.Repository
{
  public class DbContext : DisposableObject, IDbContext
  {
    public Guid Id { get; set; }
    public IDbConnection Connection { get; }

    public DbContext()
    {
      var connectionStringSettings = SqlDatabase.GetConnectionStringSettings();
      var providerFactory = DbProviderFactories.GetFactory(connectionStringSettings.ProviderName);
      Connection = providerFactory.CreateConnection();
      Connection.ConnectionString = connectionStringSettings.ConnectionString;

      Id = Guid.NewGuid();
    }

    public override string ToString()
    {
      return Id.ToString();
    }
    
    protected override void Dispose(bool disposing)
    {
      if (disposing)
      {
        Connection.Dispose();
      }
    }

    private TRepository CreateRepository<TRepository>() where TRepository : class
    {
      return Connection.As<TRepository>();
    }

    public IDbCustomer Customer => CreateRepository<IDbCustomer>();
  }
}
