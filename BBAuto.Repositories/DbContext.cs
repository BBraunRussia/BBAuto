using System;
using System.Configuration;
using System.Data;
using System.Data.Common;
using BBAuto.Repositories.Interfaces;
using Common;
using Insight.Database;

namespace BBAuto.Repositories
{
  public class DbContext : DisposableObject, IDbContext
  {
    public Guid Id { get; set; }
    public IDbConnection Connection { get; }

    public DbContext(ConnectionStringSettings connectionStringSettings)
    {
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

    public IDbCar Car => CreateRepository<IDbCar>();
    public IDbCarDoc CarDoc => CreateRepository<IDbCarDoc>();
    public IDbSaleCar SaleCar => CreateRepository<IDbSaleCar>();
    public IDbEngineType EngineType => CreateRepository<IDbEngineType>();
    public IDbDiagCard DiagCard => CreateRepository<IDbDiagCard>();
    public IDbDriver Driver => CreateRepository<IDbDriver>();
    public IDbDealer Dealer => CreateRepository<IDbDealer>();
    public IDbEmployeeName EmployeeName => CreateRepository<IDbEmployeeName>();
    public IDbGrade Grade => CreateRepository<IDbGrade>();
    public IDbMark Mark => CreateRepository<IDbMark>();
    public IDbMileage Mileage => CreateRepository<IDbMileage>();
    public IDbInvoice Invoice => CreateRepository<IDbInvoice>();
    public IDbRegion Region => CreateRepository<IDbRegion>();
    public IDbViolation Violation => CreateRepository<IDbViolation>();
  }
}
