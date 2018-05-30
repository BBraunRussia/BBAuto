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

    public IDbAccount Account => CreateRepository<IDbAccount>();
    public IDbCar Car => CreateRepository<IDbCar>();
    public IDbCarDoc CarDoc => CreateRepository<IDbCarDoc>();
    public IDbSaleCar SaleCar => CreateRepository<IDbSaleCar>();
    public IDbColor Color => CreateRepository<IDbColor>();
    public IDbComp Comp => CreateRepository<IDbComp>();
    public IDbCulprit Culprit => CreateRepository<IDbCulprit>();
    public IDbCurrentStatusAfterDtp CurrentStatusAfterDtp => CreateRepository<IDbCurrentStatusAfterDtp>();
    public IDbEngineType EngineType => CreateRepository<IDbEngineType>();
    public IDbDiagCard DiagCard => CreateRepository<IDbDiagCard>();
    public IDbDriver Driver => CreateRepository<IDbDriver>();
    public IDbDriverCar DriverCar => CreateRepository<IDbDriverCar>();
    public IDbDealer Dealer => CreateRepository<IDbDealer>();
    public IDbEmployeeName EmployeeName => CreateRepository<IDbEmployeeName>();
    public IDbFuelCardType FuelCardType => CreateRepository<IDbFuelCardType>();
    public IDbGrade Grade => CreateRepository<IDbGrade>();
    public IDbLicense License => CreateRepository<IDbLicense>();
    public IDbMark Mark => CreateRepository<IDbMark>();
    public IDbMedicalCert MedicalCert => CreateRepository<IDbMedicalCert>();
    public IDbMileage Mileage => CreateRepository<IDbMileage>();
    public IDbModel Model => CreateRepository<IDbModel>();
    public IDbInvoice Invoice => CreateRepository<IDbInvoice>();
    public IDbOwner Owner => CreateRepository<IDbOwner>();
    public IDbPolicy Policy => CreateRepository<IDbPolicy>();
    public IDbProxyType ProxyType => CreateRepository<IDbProxyType>();
    public IDbRegion Region => CreateRepository<IDbRegion>();
    public IDbRepairType RepairType => CreateRepository<IDbRepairType>();
    public IDbServiceStantion ServiceStantion => CreateRepository<IDbServiceStantion>();
    public IDbStatusAfterDtp StatusAfterDtp => CreateRepository<IDbStatusAfterDtp>();
    public IDbTempMove TempMove => CreateRepository<IDbTempMove>();
    public IDbViolation Violation => CreateRepository<IDbViolation>();
    public IDbViolationType ViolationType => CreateRepository<IDbViolationType>();
  }
}
