using System.Collections.Generic;

namespace BBAuto.Domain.Services.DriverInstruction
{
  public interface IDriverInstructionService
  {
    IList<DriverInstruction> GetDriverInstructions();
    void DeleteDriverInstruction(int id);
    void Save(DriverInstruction driverInstruction);
    IList<DriverInstruction> GetDriverInstructionsByDriverId(int driverId);
  }
}
