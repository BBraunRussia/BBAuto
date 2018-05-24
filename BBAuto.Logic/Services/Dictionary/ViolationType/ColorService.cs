using System.Collections.Generic;
using System.Linq;
using BBAuto.Repositories;
using BBAuto.Repositories.Entities;

namespace BBAuto.Logic.Services.Dictionary.ViolationType
{
  public class ViolationTypeService : IViolationTypeService
  {
    private readonly IDbContext _dbContext;

    public ViolationTypeService(IDbContext dbContext)
    {
      _dbContext = dbContext;
    }

    public Dictionary<int, string> GetItems()
    {
      var items = _dbContext.ViolationType.GetViolationTypes();
      return items.ToDictionary(item => item.Id, item => item.Name);
    }

    public KeyValuePair<int, string> GetItemById(int id)
    {
      var item = _dbContext.ViolationType.GetViolationTypeById(id);
      return new KeyValuePair<int, string>(item.Id, item.Name);
    }

    public void Delete(int id)
    {
      _dbContext.ViolationType.DeleteViolationType(id);
    }

    public void Save(int id, string name)
    {
      _dbContext.ViolationType.UpsertViolationType(new DbDictionary(id, name));
    }
  }
}
