using System;
using BBAuto.Logic.Services.Car;

namespace BBAuto.Logic.Services.Driver.DriverCar
{
  public interface IDriverCarService
  {
    DriverModel GetDriver(int carId, DateTime? date);
    DriverModel GetDriver(int carId);
    CarModel GetCar(int driverId);
  }
}
