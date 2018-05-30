using BBAuto.Repositories.Interfaces;

namespace BBAuto.Repositories
{
  public interface IDbContext
  {
    IDbAccount Account { get; }

    IDbCar Car { get; }
    IDbCarDoc CarDoc { get; }
    IDbSaleCar SaleCar { get; }

    IDbColor Color { get; }
    IDbComp Comp { get; }
    IDbCulprit Culprit { get; }
    IDbCurrentStatusAfterDtp CurrentStatusAfterDtp { get; }
    IDbEngineType EngineType { get; }
    IDbDiagCard DiagCard { get; }
    IDbDriver Driver { get; }
    IDbDriverCar DriverCar { get; }
    IDbDealer Dealer { get; }
    IDbEmployeeName EmployeeName { get; }
    IDbFuelCardType FuelCardType { get; }
    IDbGrade Grade { get; }
    IDbLicense License { get; }
    IDbMark Mark { get; }
    IDbMedicalCert MedicalCert { get; }
    IDbMileage Mileage { get; }
    IDbModel Model { get; }
    IDbInvoice Invoice { get; }
    IDbOwner Owner { get; }
    IDbPolicy Policy { get; }
    IDbProxyType ProxyType { get; }
    IDbRegion Region { get; }
    IDbRepairType RepairType { get; }
    IDbServiceStantion ServiceStantion { get; }
    IDbStatusAfterDtp StatusAfterDtp { get; }
    IDbTempMove TempMove { get; }
    IDbViolation Violation { get; }
    IDbViolationType ViolationType { get; }
  }
}
