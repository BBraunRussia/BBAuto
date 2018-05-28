using System.Collections.Generic;

namespace BBAuto.Logic.Services.Driver
{
  public interface IDriverService
  {
    DriverModel GetDriverById(int id);

    IList<DriverModel> GetDriversByRegionId(int regionId);
  }
}
