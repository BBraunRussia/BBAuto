using System.Collections.Generic;
using System.Linq;
using AutoMapper;
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

    public IList<DictionaryModel> GetItems()
    {
      var items = _dbContext.Mark.GetMarks();
      return Mapper.Map<IList<DictionaryModel>>(items);
    }

    public DictionaryModel GetItemById(int id)
    {
      var item = _dbContext.Mark.GetMarkById(id);
      return Mapper.Map<DictionaryModel>(item);
    }
    
    public void Delete(int id)
    {
      _dbContext.Mark.DeleteMark(id);
    }

    public void Save(DictionaryModel model)
    {
      var dbModel = Mapper.Map<DbDictionary>(model);

      _dbContext.Mark.UpsertMark(dbModel);
    }
  }
}
