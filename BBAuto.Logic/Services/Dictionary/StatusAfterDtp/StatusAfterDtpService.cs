using System.Collections.Generic;
using System.Linq;
using BBAuto.Repositories;
using BBAuto.Repositories.Entities;

namespace BBAuto.Logic.Services.Dictionary.StatusAfterDtp
{
  public class StatusAfterDtpService : IStatusAfterDtpService
  {
    private readonly IDbContext _dbContext;

    public StatusAfterDtpService(IDbContext dbContext)
    {
      _dbContext = dbContext;
    }

    public Dictionary<int, string> GetItems()
    {
      var items = _dbContext.StatusAfterDtp.GetStatusAfterDtps();
      return items.ToDictionary(item => item.Id, item => item.Name);
    }

    public KeyValuePair<int, string> GetItemById(int id)
    {
      var item = _dbContext.StatusAfterDtp.GetStatusAfterDtpById(id);
      return new KeyValuePair<int, string>(item.Id, item.Name);
    }

    public void Delete(int id)
    {
      _dbContext.StatusAfterDtp.DeleteStatusAfterDtp(id);
    }

    public void Save(int id, string name)
    {
      _dbContext.StatusAfterDtp.UpsertStatusAfterDtp(new DbDictionary(id, name));
    }
  }
}
