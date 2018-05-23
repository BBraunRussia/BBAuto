using System.Collections.Generic;
using BBAuto.Repositories.Entities;
using Insight.Database;

namespace BBAuto.Repositories.Interfaces
{
  [Sql(Schema = "dbo")]
  public interface IDbMark
  {
    void DeleteMark(int id);
    IList<DbDictionary> GetMarkByGradeId(int gradeId);
    IList<DbDictionary> GetMarks();
    DbDictionary GetMarkById(int id);
    DbDictionary UpsertMark(DbDictionary model);
  }
}
