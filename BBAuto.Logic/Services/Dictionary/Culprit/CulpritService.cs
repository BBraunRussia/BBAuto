using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using BBAuto.Repositories;
using BBAuto.Repositories.Entities;

namespace BBAuto.Logic.Services.Dictionary.Culprit
{
  public class CulpritService : ICulpritService
  {
    private readonly IDbContext _dbContext;

    public CulpritService(IDbContext dbContext)
    {
      _dbContext = dbContext;
    }

    public IList<DictionaryModel> GetItems()
    {
      var items = _dbContext.Culprit.GetCulprits();
      return Mapper.Map<IList<DictionaryModel>>(items);
    }

    public DictionaryModel GetItemById(int id)
    {
      var item = _dbContext.Culprit.GetCulpritById(id);
      return Mapper.Map<DictionaryModel>(item);
    }

    public void Delete(int id)
    {
      _dbContext.Culprit.DeleteCulprit(id);
    }

    public void Save(DictionaryModel model)
    {
      var dbModel = Mapper.Map<DbDictionary>(model);

      _dbContext.Culprit.UpsertCulprit(dbModel);
    }
  }
}
