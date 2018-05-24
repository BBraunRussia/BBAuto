using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using BBAuto.Repositories;
using BBAuto.Repositories.Entities;

namespace BBAuto.Logic.Services.Dictionary.EmployeesName
{
  public class EmployeesNameService : IEmployeesNameService
  {
    private readonly IDbContext _dbContext;

    public EmployeesNameService(IDbContext dbContext)
    {
      _dbContext = dbContext;
    }

    public IList<DictionaryModel> GetItems()
    {
      var items = _dbContext.EmployeeName.GetEmployeeNames();
      return Mapper.Map<IList<DictionaryModel>>(items);
    }

    public DictionaryModel GetItemById(int id)
    {
      var item = _dbContext.EmployeeName.GetEmployeeNameById(id);
      return Mapper.Map<DictionaryModel>(item);
    }

    public void Delete(int id)
    {
      _dbContext.EmployeeName.DeleteEmployeeName(id);
    }

    public void Save(DictionaryModel model)
    {
      var dbModel = Mapper.Map<DbDictionary>(model);

      _dbContext.EmployeeName.UpsertEmployeeName(dbModel);
    }
  }
}
