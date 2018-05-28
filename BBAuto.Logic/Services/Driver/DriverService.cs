using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using BBAuto.Repositories;

namespace BBAuto.Logic.Services.Driver
{
  public class DriverService : IDriverService
  {
    private readonly IDbContext _dbContext;

    public DriverService(IDbContext dbContext)
    {
      _dbContext = dbContext;
    }

    public DriverModel GetDriverById(int id)
    {
      var dbDriver = _dbContext.Driver.GetDriverById(id);

      return Mapper.Map<DriverModel>(dbDriver);
    }

    public IList<DriverModel> GetDriversByRegionId(int regionId)
    {
      var dbDrivers = _dbContext.Driver.GetDrivers();

      return Mapper.Map<IList<DriverModel>>(dbDrivers)
        .Where(d => d.RegionId == regionId && !d.Fired).ToList();
    }
  }
}
