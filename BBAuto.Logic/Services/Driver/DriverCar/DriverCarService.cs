using System;
using System.Linq;
using BBAuto.Logic.Services.TempMove;
using BBAuto.Repositories;

namespace BBAuto.Logic.Services.Driver.DriverCar
{
  public class DriverCarService : IDriverCarService
  {
    private readonly IDbContext _dbContext;
    private readonly IDriverService _driverService;
    private readonly ITempMoveService _tempMoveService;

    public DriverCarService(
      IDbContext dbContext,
      IDriverService driverService,
      ITempMoveService tempMoveService)
    {
      _dbContext = dbContext;
      _driverService = driverService;
      _tempMoveService = tempMoveService;
    }

    public DriverModel GetDriver(int carId, DateTime? date)
    {
      if (!date.HasValue)
        date = DateTime.Today;

      

      

      TempMoveList tempMoveList = TempMoveList.getInstance();
      var driver = tempMoveList.GetDriver(carId, date.Value);
      if (driver != null)
        return driver;

      var driverCar = _dbContext.DriverCar.GetDriverCarsByCarId(carId, date.Value).FirstOrDefault();

      var driver = _driverService.GetDriverById(driverCar?.DriverId);

      return driver;

      return getDriver(driverCars1.ToList());
    }
  }
}
