using System.Collections.Generic;
using System.Linq;
using BBAuto.Repositories;
using BBAuto.Repositories.Entities;

namespace BBAuto.Logic.Services.Dictionary.Mark
{
  public class MarkService : IMarkService
  {
    private readonly IDbContext _dbContext;

    public MarkService(IDbContext dbContext)
    {
      _dbContext = dbContext;
    }

    public Dictionary<int, string> GetItems()
    {
      var items = _dbContext.Mark.GetMarks();
      return items.ToDictionary(item => item.Id, mark => mark.Name);
    }

    public KeyValuePair<int, string> GetItemById(int id)
    {
      var item = _dbContext.Mark.GetMarkById(id);
      return new KeyValuePair<int, string>(item.Id, item.Name);
    }
    
    public void Delete(int id)
    {
      _dbContext.Mark.DeleteMark(id);
    }

    public void Save(int id, string name)
    {
      _dbContext.Mark.UpsertMark(new DbDictionary(id, name));
    }
  }
}
