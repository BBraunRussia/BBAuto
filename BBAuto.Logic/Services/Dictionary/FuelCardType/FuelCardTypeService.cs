using System.Collections.Generic;
using System.Linq;
using AutoMapper;
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

    public IList<DictionaryModel> GetItems()
    {
      var items = _dbContext.FuelCardType.GetFuelCardTypes();
      return Mapper.Map<IList<DictionaryModel>>(items);
    }

    public DictionaryModel GetItemById(int id)
    {
      var item = _dbContext.FuelCardType.GetFuelCardTypeById(id);
      return Mapper.Map<DictionaryModel>(item);
    }

    public void Delete(int id)
    {
      _dbContext.FuelCardType.DeleteFuelCardType(id);
    }

    public void Save(DictionaryModel model)
    {
      var dbModel = Mapper.Map<DbDictionary>(model);

      _dbContext.FuelCardType.UpsertFuelCardType(dbModel);
    }
  }
}
