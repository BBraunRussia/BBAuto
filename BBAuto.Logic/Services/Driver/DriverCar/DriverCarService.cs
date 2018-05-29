using System;
using System.Linq;
using BBAuto.Logic.Services.Car;
using BBAuto.Logic.Services.TempMove;
using BBAuto.Repositories;

namespace BBAuto.Logic.Services.Driver.DriverCar
{
  public class DriverCarService : IDriverCarService
  {
    private readonly IDbContext _dbContext;
    private readonly IDriverService _driverService;
    private readonly ITempMoveService _tempMoveService;
    private readonly ICarService _carService;

    public DriverCarService(
      IDbContext dbContext,
      IDriverService driverService,
      ITempMoveService tempMoveService,
      ICarService carService)
    {
      _dbContext = dbContext;
      _driverService = driverService;
      _tempMoveService = tempMoveService;
      _carService = carService;
    }

    public DriverModel GetDriver(int carId, DateTime? date)
    {
      if (!date.HasValue)
        date = DateTime.Today;
      
      var driver = _tempMoveService.GetDriver(carId, date.Value);
      if (driver != null)
        return driver;

      var driverCar = _dbContext.DriverCar.GetDriverCarsByCarIdAndDate(carId, date.Value).FirstOrDefault();

      driver = _driverService.GetDriverById(driverCar?.DriverId ?? 0);

      return driver;
    }

    public DriverModel GetDriver(int carId)
    {
      var car = _carService.GetCarById(carId);
      if (car == null)
        return null;

      var driverCars = _dbContext.DriverCar.GetDriverCarsByCarId(carId);

      if (driverCars.Any() || car.IsGet)
        return _driverService.GetDriverById(driverCars.First().DriverId);

      return _driverService.GetDriverById(car.DriverId ?? 0);
    }

    public CarModel GetCar(int driverId)
    {
      var driverCars = _dbContext.DriverCar.GetDriverCarsByDriverId(driverId, DateTime.Today);

      return driverCars.Any()
        ? _carService.GetCarById(driverCars.First().CarId)
        : null;
    }
  }
}
