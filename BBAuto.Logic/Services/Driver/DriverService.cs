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
  }
}
