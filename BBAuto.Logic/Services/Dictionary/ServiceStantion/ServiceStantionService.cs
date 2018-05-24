using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using BBAuto.Repositories;
using BBAuto.Repositories.Entities;

namespace BBAuto.Logic.Services.Dictionary.ServiceStantion
{
  public class ServiceStantionService : IServiceStantionService
  {
    private readonly IDbContext _dbContext;

    public ServiceStantionService(IDbContext dbContext)
    {
      _dbContext = dbContext;
    }

    public IList<DictionaryModel> GetItems()
    {
      var items = _dbContext.ServiceStantion.GetServiceStantions();
      return Mapper.Map<IList<DictionaryModel>>(items);
    }

    public DictionaryModel GetItemById(int id)
    {
      var item = _dbContext.ServiceStantion.GetServiceStantionById(id);
      return Mapper.Map<DictionaryModel>(item);
    }

    public void Delete(int id)
    {
      _dbContext.ServiceStantion.DeleteServiceStantion(id);
    }

    public void Save(DictionaryModel model)
    {
      var dbModel = Mapper.Map<DbDictionary>(model);

      _dbContext.ServiceStantion.UpsertServiceStantion(dbModel);
    }
  }
}
