using System.Collections.Generic;
using System.Linq;
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

    public Dictionary<int, string> GetItems()
    {
      var items = _dbContext.EmployeeName.GetEmployeeNames();
      return items.ToDictionary(item => item.Id, item => item.Name);
    }

    public KeyValuePair<int, string> GetItemById(int id)
    {
      var item = _dbContext.EmployeeName.GetEmployeeNameById(id);
      return new KeyValuePair<int, string>(item.Id, item.Name);
    }

    public void Delete(int id)
    {
      _dbContext.EmployeeName.DeleteEmployeeName(id);
    }

    public void Save(int id, string name)
    {
      _dbContext.EmployeeName.UpsertEmployeeName(new DbDictionary(id, name));
    }
  }
}
