using System;
using System.Collections.Generic;
using AutoMapper;
using BBAuto.Repository;
using BBAuto.Repository.Models;
using Common;

namespace BBAuto.Domain.Services.DriverTransponder
{
  public class DriverTransponderService : IDriverTransponderService
  {
    private readonly IDbContext _dbContext;

    public DriverTransponderService()
    {
      _dbContext = new DbContext();
    }

    public IList<DriverTransponder> GetDriversByTransponderId(int transponderId)
    {
      var result = _dbContext.DriverTransponder.GetDriverTranspondersByTransponderId(transponderId);

      return Mapper.Map<IList<DriverTransponder>>(result);
    }

    public DriverTransponder Save(DriverTransponder driverTransponder)
    {
      if (!driverTransponder.DateBegin.HasValue)
        driverTransponder.DateBegin = DateTime.Today;
      if (driverTransponder.DriverId == 0)
        driverTransponder.DriverId = Consts.ReserveDriverId;

      var dbDriverTransponder = Mapper.Map<DbDriverTransponder>(driverTransponder);

      var result = _dbContext.DriverTransponder.UpsertDriverTransponder(dbDriverTransponder);

      return Mapper.Map<DriverTransponder>(result);
    }

    public DriverTransponder GetDriverTransponderById(int driverTransponderId)
    {
      var result = _dbContext.DriverTransponder.GetDriverTransponderById(driverTransponderId);

      return Mapper.Map<DriverTransponder>(result);
    }

    public void Delete(int driverTransponderId)
    {
      _dbContext.DriverTransponder.DeleteDriverTransponder(driverTransponderId);
    }
  }
}
