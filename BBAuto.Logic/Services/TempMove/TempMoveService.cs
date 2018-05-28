using System;
using System.Linq;
using BBAuto.Logic.Services.Driver;
using BBAuto.Repositories;

namespace BBAuto.Logic.Services.TempMove
{
  public class TempMoveService : ITempMoveService
  {
    private readonly IDbContext _dbContext;
    private readonly IDriverService _driverService;

    public TempMoveService(
      IDbContext dbContext,
      IDriverService driverService)
    {
      _dbContext = dbContext;
      _driverService = driverService;
    }

    public DriverModel GetDriver(int carId, DateTime date)
    {
      var tempMoves = _dbContext.TempMove.GetTempMoveByCarId(carId, date);

      var tempMove = tempMoves.FirstOrDefault();

      return tempMove == null
        ? null
        : _driverService.GetDriverById(tempMove.DriverId);
    }
  }
}
