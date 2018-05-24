using System.Collections.Generic;
using System.Linq;
using BBAuto.Repositories;
using BBAuto.Repositories.Entities;

namespace BBAuto.Logic.Services.Dictionary.Comp
{
  public class CompService : ICompService
  {
    private readonly IDbContext _dbContext;

    public CompService(IDbContext dbContext)
    {
      _dbContext = dbContext;
    }

    public Dictionary<int, string> GetItems()
    {
      var items = _dbContext.Comp.GetComps();
      return items.ToDictionary(item => item.Id, item => item.Name);
    }

    public KeyValuePair<int, string> GetItemById(int id)
    {
      var item = _dbContext.Comp.GetCompById(id);
      return new KeyValuePair<int, string>(item.Id, item.Name);
    }

    public void Delete(int id)
    {
      _dbContext.Comp.DeleteComp(id);
    }

    public void Save(int id, string name)
    {
      _dbContext.Comp.UpsertComp(new DbDictionary(id, name));
    }
  }
}
