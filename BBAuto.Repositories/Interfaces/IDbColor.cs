using System.Collections.Generic;
using BBAuto.Repositories.Entities;
using Insight.Database;

namespace BBAuto.Repositories.Interfaces
{
  [Sql(Schema = "dbo")]
  public interface IDbColor
  {
    void DeleteColor(int id);
    DbDictionary GetColorById(int id);
    IList<DbDictionary> GetColors();
    DbDictionary UpsertColor(DbDictionary model);
  }
}
