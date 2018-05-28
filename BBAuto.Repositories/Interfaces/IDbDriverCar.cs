using System.Collections.Generic;
using BBAuto.Repositories.Entities;

namespace BBAuto.Repositories.Interfaces
{
  public interface IDbDriverCar
  {
    IList<DbDriverCar> GetDriverCars();
    IList<DbDriverCar> GetDriverCarsByCarId(int carId);
    IList<DbDriverCar> GetDriverCarsByDriverId(int driverId);
  }
}
