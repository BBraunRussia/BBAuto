using System;
using BBAuto.Logic.Services.Driver;

namespace BBAuto.Logic.Services.TempMove
{
  public interface ITempMoveService
  {
    DriverModel GetDriver(int carId, DateTime date);
  }
}
