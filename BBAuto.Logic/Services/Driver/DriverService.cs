using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using BBAuto.Logic.Static;
using BBAuto.Repositories;
using BBAuto.Repositories.Entities;

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

    public IList<DriverModel> GetDriversByRole(RolesList role)
    {
      var dbDrivers = _dbContext.Driver.GetDriversByRoleId((int)role);

      return Mapper.Map<IList<DriverModel>>(dbDrivers);
    }

    public DriverModel GetDriverByLogin(string login)
    {
      var dbDriver = _dbContext.Driver.GetDriverByLogin(login);

      return Mapper.Map<DriverModel>(dbDriver);
    }

    public DriverModel Save(DriverModel driver)
    {
      var dbModel = Mapper.Map<DbDriver>(driver);

      return Mapper.Map<DriverModel>(_dbContext.Driver.UpsertDriver(dbModel));
    }

    public IList<DriverModel> GetDrivers()
    {
      var list = _dbContext.Driver.GetDrivers();

      return Mapper.Map<IList<DriverModel>>(list);
    }
  }
}
