using System.Collections.Generic;

namespace BBAuto.Domain.Services.DriverTransponder
{
  public interface IDriverTransponderService
  {
    IList<DriverTransponder> GetDriversByTransponderId(int transponderId);
    DriverTransponder Save(DriverTransponder driverTransponder);
    DriverTransponder GetDriverTransponderById(int driverTransponderId);
    void Delete(int driverTransponderId);
  }
}
