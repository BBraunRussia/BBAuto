using System;

namespace BBAuto.Logic.Services.Driver.DriverCar
{
  public interface IDriverCarService
  {
    DriverModel GetDriver(int carId, DateTime date);
  }
}
