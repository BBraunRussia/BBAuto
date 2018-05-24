using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using BBAuto.Repositories;
using BBAuto.Repositories.Entities;

namespace BBAuto.Logic.Services.Dictionary.ProxyType
{
  public class ProxyTypeService : IProxyTypeService
  {
    private readonly IDbContext _dbContext;

    public ProxyTypeService(IDbContext dbContext)
    {
      _dbContext = dbContext;
    }

    public IList<DictionaryModel> GetItems()
    {
      var items = _dbContext.ProxyType.GetProxyTypes();
      return Mapper.Map<IList<DictionaryModel>>(items);
    }

    public DictionaryModel GetItemById(int id)
    {
      var item = _dbContext.ProxyType.GetProxyTypeById(id);
      return Mapper.Map<DictionaryModel>(item);
    }

    public void Delete(int id)
    {
      _dbContext.ProxyType.DeleteProxyType(id);
    }

    public void Save(DictionaryModel model)
    {
      var dbModel = Mapper.Map<DbDictionary>(model);

      _dbContext.ProxyType.UpsertProxyType(dbModel);
    }
  }
}
