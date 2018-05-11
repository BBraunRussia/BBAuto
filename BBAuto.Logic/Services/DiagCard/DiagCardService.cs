using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using BBAuto.Repositories;
using BBAuto.Repositories.Entities;

namespace BBAuto.Logic.Services.DiagCard
{
  public class DiagCardService : IDiagCardService
  {
    private readonly IDbContext _dbContext;

    public DiagCardService(IDbContext dbContext)
    {
      _dbContext = dbContext;
    }

    public DiagCardModel GetByCarId(int id)
    {
      var dbModel = _dbContext.DiagCard.GetDiagCardById(id);

      return Mapper.Map<IList<DiagCardModel>>(dbModel).OrderByDescending(m => m.Date).FirstOrDefault();
    }

    public void Save(DiagCardModel diagCard)
    {
      var dbModel = Mapper.Map<DbDiagCard>(diagCard);

      _dbContext.DiagCard.UpsertDiagCard(dbModel);
    }

    public void Delete(int id)
    {
      _dbContext.DiagCard.DeleteDiagCard(id);
    }
  }
}
