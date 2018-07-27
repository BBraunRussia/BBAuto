using System.Collections.Generic;
using AutoMapper;
using BBAuto.Repository;
using BBAuto.Repository.Models;

namespace BBAuto.Domain.Services.Transponder
{
  public class TransponderService : ITransponderService
  {
    private readonly IDbContext _dbContext;

    public TransponderService()
    {
      _dbContext = new DbContext();
    }

    public IList<ReportTransponder> GetReportTransponderList()
    {
      var dbTransponders = _dbContext.Transponder.GetReportTransponderList();

      return Mapper.Map<IList<ReportTransponder>>(dbTransponders);
    }

    public Transponder Save(Transponder transponder)
    {
      var dbTransponder = Mapper.Map<DbTransponder>(transponder);

      var result = _dbContext.Transponder.UpsertTransponder(dbTransponder);

      return Mapper.Map<Transponder>(result);
    }
  }
}
