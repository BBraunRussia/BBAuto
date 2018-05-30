using System.Collections.Generic;
using BBAuto.Repositories.Entities;
using Insight.Database;

namespace BBAuto.Repositories.Interfaces
{
  [Sql(Schema = "dbo")]
  public interface IDbModel
  {
    void DeleteModel(int id);
    IList<DbModel> GetModelByGradeId(int gradeId);
    DbModel GetModelById(int id);
    IList<DbModel> GetModels();
    DbModel UpsertModel(DbModel model);
  }
}
