using System.Linq;
using AutoMapper;
using BBAuto.Repositories;

namespace BBAuto.Logic.Services.License
{
  public class LicenseService : ILicenseService
  {
    private readonly IDbContext _dbContext;

    public LicenseService(IDbContext dbContext)
    {
      _dbContext = dbContext;
    }

    public LicenseModel GetLicenseByDriverId(int driverId)
    {
      var dbModel = _dbContext.License.GetLicensesByDriverId(driverId).FirstOrDefault();

      return Mapper.Map<LicenseModel>(dbModel);
    }
  }
}
