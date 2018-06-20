using System.Collections.Generic;
using AutoMapper;
using BBAuto.Repository;
using BBAuto.Repository.Models;

namespace BBAuto.Domain.Services.Comp
{
  public class CompService : ICompService
  {
    private readonly IDbContext _dbContext;

    public CompService()
    {
      _dbContext = new DbContext();
    }

    public Comp GetCompById(int id)
    {
      var dbModel = _dbContext.Comp.GetCompById(id);

      return Mapper.Map<Comp>(dbModel);
    }

    public IList<Comp> GetCompList()
    {
      var dbList = _dbContext.Comp.GetCompList();

      return Mapper.Map<IList<Comp>>(dbList);
    }

    public Comp SaveComp(Comp comp)
    {
      var dbModel = Mapper.Map<DbComp>(comp);

      var result = _dbContext.Comp.UpsertComp(dbModel);

      return Mapper.Map<Comp>(result);
    }

    public void DeleteComp(int id)
    {
      _dbContext.Comp.DeleteComp(id);
    }
  }
}
