using System.Collections.Generic;
using AutoMapper;
using BBAuto.Repository;
using BBAuto.Repository.Models;

namespace BBAuto.Domain.Services.DriverInstruction
{
  public class DriverInstructionService : IDriverInstructionService
  {
    private readonly IDbContext _dbContext;

    public DriverInstructionService()
    {
      _dbContext = new DbContext();
    }

    public IList<DriverInstruction> GetDriverInstructions()
    {
      var dbList = _dbContext.DriverInstruction.GetDriverInstructions();

      return Mapper.Map<IList<DriverInstruction>>(dbList);
    }

    public IList<DriverInstruction> GetDriverInstructionsByDriverId(int driverId)
    {
      var dbList = _dbContext.DriverInstruction.GetDriverInstructionsByDriverId(driverId);

      return Mapper.Map<IList<DriverInstruction>>(dbList);
    }

    public void DeleteDriverInstruction(int id)
    {
      _dbContext.DriverInstruction.DeleteDriverInstruction(id);
    }

    public void Save(DriverInstruction driverInstruction)
    {
      var dbDriverInstruction = Mapper.Map<DbDriverInstruction>(driverInstruction);

      _dbContext.DriverInstruction.UpsertDriverInstruction(dbDriverInstruction);
    }
  }
}
