using System.Collections.Generic;
using System.Linq;
using BBAuto.Repositories;
using BBAuto.Repositories.Entities;

namespace BBAuto.Logic.Services.Dictionary.FuelCardType
{
  public class FuelCardTypeService : IFuelCardTypeService
  {
    private readonly IDbContext _dbContext;

    public FuelCardTypeService(IDbContext dbContext)
    {
      _dbContext = dbContext;
    }

    public Dictionary<int, string> GetItems()
    {
      var items = _dbContext.FuelCardType.GetFuelCardTypes();
      return items.ToDictionary(item => item.Id, item => item.Name);
    }

    public KeyValuePair<int, string> GetItemById(int id)
    {
      var item = _dbContext.FuelCardType.GetFuelCardTypeById(id);
      return new KeyValuePair<int, string>(item.Id, item.Name);
    }

    public void Delete(int id)
    {
      _dbContext.FuelCardType.DeleteFuelCardType(id);
    }

    public void Save(int id, string name)
    {
      _dbContext.FuelCardType.UpsertFuelCardType(new DbDictionary(id, name));
    }
  }
}
