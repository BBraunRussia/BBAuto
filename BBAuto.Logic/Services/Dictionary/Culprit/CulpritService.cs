using System.Collections.Generic;
using System.Linq;
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

    public Dictionary<int, string> GetItems()
    {
      var items = _dbContext.Culprit.GetCulprits();
      return items.ToDictionary(item => item.Id, item => item.Name);
    }

    public KeyValuePair<int, string> GetItemById(int id)
    {
      var item = _dbContext.Culprit.GetCulpritById(id);
      return new KeyValuePair<int, string>(item.Id, item.Name);
    }

    public void Delete(int id)
    {
      _dbContext.Culprit.DeleteCulprit(id);
    }

    public void Save(int id, string name)
    {
      _dbContext.Culprit.UpsertCulprit(new DbDictionary(id, name));
    }
  }
}
