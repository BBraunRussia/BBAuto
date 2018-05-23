using BBAuto.Repositories.Interfaces;

namespace BBAuto.Repositories
{
  public interface IDbContext
  {
    IDbCar Car { get; }
    IDbCarDoc CarDoc { get; }
    IDbSaleCar SaleCar { get; }

    IDbEngineType EngineType { get; }
    IDbDiagCard DiagCard { get; }
    IDbDriver Driver { get; }
    IDbDealer Dealer { get; }
    IDbEmployeeName EmployeeName { get; }
    IDbGrade Grade { get; }
    IDbMark Mark { get; }
    IDbMileage Mileage { get; }
    IDbInvoice Invoice { get; }
    IDbRegion Region { get; }
    IDbViolation Violation { get; }
  }
}
