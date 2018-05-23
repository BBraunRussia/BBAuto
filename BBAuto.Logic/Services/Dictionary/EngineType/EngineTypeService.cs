using System.Collections.Generic;
using System.Linq;
using BBAuto.Repositories;
using BBAuto.Repositories.Entities;

namespace BBAuto.Logic.Services.Dictionary.EngineType
{
  public class EngineTypeService : IEngineTypeService
  {
    private readonly IDbContext _dbContext;

    public EngineTypeService(IDbContext dbContext)
    {
      _dbContext = dbContext;
    }

    public Dictionary<int, string> GetItems()
    {
      var items = _dbContext.EngineType.GetEngineTypes();
      return items.ToDictionary(item => item.Id, item => item.Name);
    }

    public KeyValuePair<int, string> GetItemById(int id)
    {
      var item = _dbContext.EngineType.GetEngineTypeById(id);
      return new KeyValuePair<int, string>(item.Id, item.Name);
    }

    public void Delete(int id)
    {
      _dbContext.EngineType.DeleteEngineType(id);
    }

    public void Save(int id, string name)
    {
      _dbContext.EngineType.UpsertEngineType(new DbDictionary(id, name));
    }
  }
}
