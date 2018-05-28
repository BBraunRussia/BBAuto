using BBAuto.Repositories.Interfaces;

namespace BBAuto.Repositories
{
  public interface IDbContext
  {
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
    IDbMark Mark { get; }
    IDbMileage Mileage { get; }
    IDbInvoice Invoice { get; }
    IDbOwner Owner { get; }
    IDbProxyType ProxyType { get; }
    IDbRegion Region { get; }
    IDbRepairType RepairType { get; }
    IDbServiceStantion ServiceStantion { get; }
    IDbStatusAfterDtp StatusAfterDtp { get; }
    IDbViolation Violation { get; }
    IDbViolationType ViolationType { get; }
  }
}
