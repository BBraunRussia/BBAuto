using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using BBAuto.Repositories;

namespace BBAuto.Logic.Services.Dictionary.Mark
{
  public class MarkService : IMarkService
  {
    private readonly IDbContext _dbContext;

    public MarkService(IDbContext dbContext)
    {
      _dbContext = dbContext;
    }

    public IList<MarkModel> GetItems()
    {
      var dbMarks = _dbContext.Mark.GetMarks();

      return Mapper.Map<IList<MarkModel>>(dbMarks);
    }

    KeyValuePair<int, string> IBasicDictionaryService.GetItemById(int id)
    {
      var dbMark = _dbContext.Mark.GetMarkById(id);
      var mark = Mapper.Map<MarkModel>(dbMark);
      return new KeyValuePair<int, string>(mark.Id, mark.Name);
    }

    Dictionary<int, string> IBasicDictionaryService.GetItems()
    {
      var dbMarks = _dbContext.Mark.GetMarks();

      return Mapper.Map<IList<MarkModel>>(dbMarks).ToDictionary(mark => mark.Id, mark => mark.Name);
    }

    public MarkModel GetItemById(int id)
    {
      var dbMark = _dbContext.Mark.GetMarkById(id);

      return Mapper.Map<MarkModel>(dbMark);
    }
    
    public void Delete(int id)
    {
      _dbContext.Mark.DeleteMark(id);
    }

    public void Save(int id, string name)
    {
      throw new System.NotImplementedException();
    }
  }
}
