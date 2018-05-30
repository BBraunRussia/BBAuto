using System.Collections.Generic;
using AutoMapper;
using BBAuto.Repositories;

namespace BBAuto.Logic.Services.Model
{
  public class ModelService : IModelService
  {
    private readonly IDbContext _dbContext;

    public ModelService(IDbContext dbContext)
    {
      _dbContext = dbContext;
    }

    public IList<ModelModel> GetModels()
    {
      var dbModels = _dbContext.Model.GetModels();

      return Mapper.Map<IList<ModelModel>>(dbModels);
    }
  }
}
