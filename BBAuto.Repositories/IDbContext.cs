using BBAuto.Repositories.Interfaces;

namespace BBAuto.Repositories
{
  public interface IDbContext
  {
    IDbCar Car { get; }
    IDbCarDoc CarDoc { get; }
    IDbSaleCar SaleCar { get; }
    IDbDiagCard DiagCard { get; }
    IDbDriver Driver { get; }
    IDbDealer Dealer { get; }
    IDbMileage Mileage { get; }
    IDbInvoice Invoice { get; }
  }
}
