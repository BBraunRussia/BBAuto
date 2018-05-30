using System.Collections.Generic;
using BBAuto.Logic.Static;

namespace BBAuto.Logic.Services.Driver
{
  public interface IDriverService
  {
    DriverModel GetDriverById(int id);

    IList<DriverModel> GetDriversByRegionId(int regionId);
    IList<DriverModel> GetDriversByRole(RolesList role);
    DriverModel GetDriverByLogin(string login);
    DriverModel Save(DriverModel driver);
    IList<DriverModel> GetDrivers();
  }
}
