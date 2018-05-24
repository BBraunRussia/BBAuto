using System.Collections.Generic;
using System.Linq;
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

    public Dictionary<int, string> GetItems()
    {
      var items = _dbContext.ProxyType.GetProxyTypes();
      return items.ToDictionary(item => item.Id, item => item.Name);
    }

    public KeyValuePair<int, string> GetItemById(int id)
    {
      var item = _dbContext.ProxyType.GetProxyTypeById(id);
      return new KeyValuePair<int, string>(item.Id, item.Name);
    }

    public void Delete(int id)
    {
      _dbContext.ProxyType.DeleteProxyType(id);
    }

    public void Save(int id, string name)
    {
      _dbContext.ProxyType.UpsertProxyType(new DbDictionary(id, name));
    }
  }
}
