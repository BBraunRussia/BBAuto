using System.Collections.Generic;
using System.Linq;
using AutoMapper;
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

    public IList<DictionaryModel> GetItems()
    {
      var items = _dbContext.ViolationType.GetViolationTypes();
      return Mapper.Map<IList<DictionaryModel>>(items);
    }

    public DictionaryModel GetItemById(int id)
    {
      var item = _dbContext.ViolationType.GetViolationTypeById(id);
      return Mapper.Map<DictionaryModel>(item);
    }

    public void Delete(int id)
    {
      _dbContext.ViolationType.DeleteViolationType(id);
    }

    public void Save(DictionaryModel model)
    {
      var dbModel = Mapper.Map<DbDictionary>(model);

      _dbContext.ViolationType.UpsertViolationType(dbModel);
    }
  }
}
