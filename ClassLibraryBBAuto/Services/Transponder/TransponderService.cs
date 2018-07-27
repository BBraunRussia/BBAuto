using System.Collections.Generic;
using AutoMapper;
using BBAuto.Repository;

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
  }
}
