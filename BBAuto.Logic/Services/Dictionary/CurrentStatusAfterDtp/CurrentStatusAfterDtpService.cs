using System.Collections.Generic;
using System.Linq;
using AutoMapper;
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

    public IList<DictionaryModel> GetItems()
    {
      var items = _dbContext.CurrentStatusAfterDtp.GetCurrentStatusAfterDtps();
      return Mapper.Map<IList<DictionaryModel>>(items);
    }

    public DictionaryModel GetItemById(int id)
    {
      var item = _dbContext.CurrentStatusAfterDtp.GetCurrentStatusAfterDtpById(id);
      return Mapper.Map<DictionaryModel>(item);
    }

    public void Delete(int id)
    {
      _dbContext.CurrentStatusAfterDtp.DeleteCurrentStatusAfterDtp(id);
    }

    public void Save(DictionaryModel model)
    {
      var dbModel = Mapper.Map<DbDictionary>(model);

      _dbContext.CurrentStatusAfterDtp.UpsertCurrentStatusAfterDtp(dbModel);
    }
  }
}
