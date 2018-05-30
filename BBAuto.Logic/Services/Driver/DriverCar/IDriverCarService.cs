using System;
using System.Data;
using BBAuto.Logic.Services.Car;

namespace BBAuto.Logic.Services.Driver.DriverCar
{
  public interface IDriverCarService
  {
    DriverModel GetDriver(int carId, DateTime? date);
    DriverModel GetDriver(int carId);
    DriverModel GetDriverByAccountId(int accountId);
    CarModel GetCar(int driverId);
    DataTable GetDataTableCarsByDriverId(int driverId);
  }
}
