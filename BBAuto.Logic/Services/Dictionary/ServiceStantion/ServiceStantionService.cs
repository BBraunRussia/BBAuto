using System.Collections.Generic;
using System.Linq;
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

    public Dictionary<int, string> GetItems()
    {
      var items = _dbContext.ServiceStantion.GetServiceStantions();
      return items.ToDictionary(item => item.Id, item => item.Name);
    }

    public KeyValuePair<int, string> GetItemById(int id)
    {
      var item = _dbContext.ServiceStantion.GetServiceStantionById(id);
      return new KeyValuePair<int, string>(item.Id, item.Name);
    }

    public void Delete(int id)
    {
      _dbContext.ServiceStantion.DeleteServiceStantion(id);
    }

    public void Save(int id, string name)
    {
      _dbContext.ServiceStantion.UpsertServiceStantion(new DbDictionary(id, name));
    }
  }
}
