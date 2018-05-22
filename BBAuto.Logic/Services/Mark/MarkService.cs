using System.Collections.Generic;
using AutoMapper;
using BBAuto.Repositories;

namespace BBAuto.Logic.Services.Mark
{
  public class MarkService : IMarkService
  {
    private readonly IDbContext _dbContext;

    public MarkService(IDbContext dbContext)
    {
      _dbContext = dbContext;
    }

    public IList<MarkModel> GetMarks()
    {
      var dbMarks = _dbContext.Mark.GetMarks();

      return Mapper.Map<IList<MarkModel>>(dbMarks);
    }

    public MarkModel GetMarkById(int id)
    {
      var dbMark = _dbContext.Mark.GetMarkById(id);

      return Mapper.Map<MarkModel>(dbMark);
    }
  }
}
