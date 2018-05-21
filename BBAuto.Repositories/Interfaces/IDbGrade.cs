using System.Collections.Generic;
using BBAuto.Repositories.Entities;
using Insight.Database;

namespace BBAuto.Repositories.Interfaces
{
  [Sql(Schema = "dbo")]
  public interface IDbGrade
  {
    void DeleteGrade(int id);
    IList<DbGrade> GetGradesByModelId(int modelId);
    DbGrade UpsertGrade(DbGrade grade);
    DbGrade GetGradeById(int id);
  }
}
