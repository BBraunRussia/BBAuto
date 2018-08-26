using System.Collections.Generic;
using BBAuto.Repository.Models;
using Insight.Database;

namespace BBAuto.Repository.Interfaces
{
  [Sql(Schema = "dbo")]
  public interface IDbDriverInstruction
  {
    IList<DbDriverInstruction> GetDriverInstructions();
    IList<DbDriverInstruction> GetDriverInstructionsByDriverId(int driverId);
    void DeleteDriverInstruction(int id);
    int UpsertDriverInstruction(DbDriverInstruction driverInstruction);
  }
}
