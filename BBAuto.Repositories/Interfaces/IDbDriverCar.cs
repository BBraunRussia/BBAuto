using System;
using System.Collections.Generic;
using BBAuto.Repositories.Entities;

namespace BBAuto.Repositories.Interfaces
{
  public interface IDbDriverCar
  {
    IList<DbDriverCar> GetDriverCars();
    IList<DbDriverCar> GetDriverCarsByCarIdAndDate(int carId, DateTime date);
    IList<DbDriverCar> GetDriverCarsByCarId(int carId);
    IList<DbDriverCar> GetDriverCarsByDriverId(int driverId, DateTime date);
  }
}
