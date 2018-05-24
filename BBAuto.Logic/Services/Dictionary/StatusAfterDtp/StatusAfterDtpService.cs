using System.Collections.Generic;
using System.Linq;
using AutoMapper;
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

    public IList<DictionaryModel> GetItems()
    {
      var items = _dbContext.StatusAfterDtp.GetStatusAfterDtps();
      return Mapper.Map<IList<DictionaryModel>>(items);
    }

    public DictionaryModel GetItemById(int id)
    {
      var item = _dbContext.StatusAfterDtp.GetStatusAfterDtpById(id);
      return Mapper.Map<DictionaryModel>(item);
    }

    public void Delete(int id)
    {
      _dbContext.StatusAfterDtp.DeleteStatusAfterDtp(id);
    }

    public void Save(DictionaryModel model)
    {
      var dbModel = Mapper.Map<DbDictionary>(model);

      _dbContext.StatusAfterDtp.UpsertStatusAfterDtp(dbModel);
    }
  }
}
