using System.Collections.Generic;
using System.Linq;
using BBAuto.Repositories;
using BBAuto.Repositories.Entities;

namespace BBAuto.Logic.Services.Dictionary.Owner
{
  public class OwnerService : IOwnerService
  {
    private readonly IDbContext _dbContext;

    public OwnerService(IDbContext dbContext)
    {
      _dbContext = dbContext;
    }

    public Dictionary<int, string> GetItems()
    {
      var items = _dbContext.Owner.GetOwners();
      return items.ToDictionary(item => item.Id, item => item.Name);
    }

    public KeyValuePair<int, string> GetItemById(int id)
    {
      var item = _dbContext.Owner.GetOwnerById(id);
      return new KeyValuePair<int, string>(item.Id, item.Name);
    }

    public void Delete(int id)
    {
      _dbContext.Owner.DeleteOwner(id);
    }

    public void Save(int id, string name)
    {
      _dbContext.Owner.UpsertOwner(new DbDictionary(id, name));
    }
  }
}
