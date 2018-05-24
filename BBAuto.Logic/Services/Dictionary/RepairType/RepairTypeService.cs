using System.Collections.Generic;
using System.Linq;
using BBAuto.Repositories;
using BBAuto.Repositories.Entities;

namespace BBAuto.Logic.Services.Dictionary.RepairType
{
  public class RepairTypeService : IRepairTypeService
  {
    private readonly IDbContext _dbContext;

    public RepairTypeService(IDbContext dbContext)
    {
      _dbContext = dbContext;
    }

    public Dictionary<int, string> GetItems()
    {
      var items = _dbContext.RepairType.GetRepairTypes();
      return items.ToDictionary(item => item.Id, item => item.Name);
    }

    public KeyValuePair<int, string> GetItemById(int id)
    {
      var item = _dbContext.RepairType.GetRepairTypeById(id);
      return new KeyValuePair<int, string>(item.Id, item.Name);
    }

    public void Delete(int id)
    {
      _dbContext.RepairType.DeleteRepairType(id);
    }

    public void Save(int id, string name)
    {
      _dbContext.RepairType.UpsertRepairType(new DbDictionary(id, name));
    }
  }
}
