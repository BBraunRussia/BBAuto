using System.Collections.Generic;
using BBAuto.Repositories.Entities;
using Insight.Database;

namespace BBAuto.Repositories.Interfaces
{
  [Sql(Schema = "dbo")]
  public interface IDbMark
  {
    void DeleteMark(int id);
    IList<DbMark> GetMarkByGradeId(int gradeId);
    IList<DbMark> GetMarks();
    DbMark GetMarkById(int id);
    DbMark UpsertMark(DbMark mark);
  }
}
