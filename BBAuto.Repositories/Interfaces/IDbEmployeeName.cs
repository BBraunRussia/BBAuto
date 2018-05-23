using System.Collections.Generic;
using BBAuto.Repositories.Entities;
using Insight.Database;

namespace BBAuto.Repositories.Interfaces
{
  [Sql(Schema = "dbo")]
  public interface IDbEmployeeName
  {
    void DeleteEmployeeName(int id);
    DbDictionary GetEmployeeNameById(int id);
    IList<DbDictionary> GetEmployeeNames();
    DbDictionary UpsertEmployeeName(DbDictionary model);
  }
}
