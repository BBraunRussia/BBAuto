using System.Collections.Generic;
using System.Linq;
using BBAuto.Repositories;
using BBAuto.Repositories.Entities;

namespace BBAuto.Logic.Services.Dictionary.Color
{
  public class ColorService : IColorService
  {
    private readonly IDbContext _dbContext;

    public ColorService(IDbContext dbContext)
    {
      _dbContext = dbContext;
    }

    public Dictionary<int, string> GetItems()
    {
      var items = _dbContext.Color.GetColors();
      return items.ToDictionary(item => item.Id, item => item.Name);
    }

    public KeyValuePair<int, string> GetItemById(int id)
    {
      var item = _dbContext.Color.GetColorById(id);
      return new KeyValuePair<int, string>(item.Id, item.Name);
    }

    public void Delete(int id)
    {
      _dbContext.Color.DeleteColor(id);
    }

    public void Save(int id, string name)
    {
      _dbContext.Color.UpsertColor(new DbDictionary(id, name));
    }
  }
}
