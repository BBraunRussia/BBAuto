using System.Collections.Generic;
using System.Linq;
using BBAuto.Repositories;
using BBAuto.Repositories.Entities;

namespace BBAuto.Logic.Services.Dictionary.Region
{
  public class RegionService : IRegionService
  {
    private readonly IDbContext _dbContext;

    public RegionService(IDbContext dbContext)
    {
      _dbContext = dbContext;
    }

    public Dictionary<int, string> GetItems()
    {
      var items = _dbContext.Region.GetRegions();
      return items.ToDictionary(item => item.Id, mark => mark.Name);
    }

    public KeyValuePair<int, string> GetItemById(int id)
    {
      var item = _dbContext.Region.GetRegionById(id);
      return new KeyValuePair<int, string>(item.Id, item.Name);
    }

    public void Delete(int id)
    {
      _dbContext.Region.DeleteRegion(id);
    }

    public void Save(int id, string name)
    {
      _dbContext.Region.UpsertRegion(new DbDictionary(id, name));
    }
  }
}
