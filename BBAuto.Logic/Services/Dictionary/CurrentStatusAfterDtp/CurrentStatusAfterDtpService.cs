using System.Collections.Generic;
using System.Linq;
using BBAuto.Repositories;
using BBAuto.Repositories.Entities;

namespace BBAuto.Logic.Services.Dictionary.CurrentStatusAfterDtp
{
  public class CurrentStatusAfterDtpService : ICurrentStatusAfterDtpService
  {
    private readonly IDbContext _dbContext;

    public CurrentStatusAfterDtpService(IDbContext dbContext)
    {
      _dbContext = dbContext;
    }

    public Dictionary<int, string> GetItems()
    {
      var items = _dbContext.CurrentStatusAfterDtp.GetCurrentStatusAfterDtps();
      return items.ToDictionary(item => item.Id, item => item.Name);
    }

    public KeyValuePair<int, string> GetItemById(int id)
    {
      var item = _dbContext.CurrentStatusAfterDtp.GetCurrentStatusAfterDtpById(id);
      return new KeyValuePair<int, string>(item.Id, item.Name);
    }

    public void Delete(int id)
    {
      _dbContext.CurrentStatusAfterDtp.DeleteCurrentStatusAfterDtp(id);
    }

    public void Save(int id, string name)
    {
      _dbContext.CurrentStatusAfterDtp.UpsertCurrentStatusAfterDtp(new DbDictionary(id, name));
    }
  }
}
