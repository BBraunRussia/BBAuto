using System.Collections.Generic;
using System.Data;
using System.Linq;
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

    public DataTable GetReportTransponderList()
    {
      var dbTransponders = _dbContext.Transponder.GetReportTransponderList();

      var transponders = Mapper.Map<IList<ReportTransponder>>(dbTransponders).ToList();

      var dt = new DataTable();
      dt.Columns.Add("Id");
      dt.Columns.Add("DriverId");
      dt.Columns.Add("Номер транспондера");
      dt.Columns.Add("Регион");
      dt.Columns.Add("Водитель");
      dt.Columns.Add("Lost");

      transponders.ForEach(item => dt.Rows.Add(item.Id, item.DriverId, item.Number, item.RegionName, item.DriverFio, item.Lost));

      return dt;
    }

    public Transponder Save(Transponder transponder)
    {
      var dbTransponder = Mapper.Map<DbTransponder>(transponder);

      var result = _dbContext.Transponder.UpsertTransponder(dbTransponder);

      return Mapper.Map<Transponder>(result);
    }

    public Transponder GetTransponder(int id)
    {
      var dbTransponder = _dbContext.Transponder.GetTransponderById(id);

      return Mapper.Map<Transponder>(dbTransponder);
    }
  }
}
