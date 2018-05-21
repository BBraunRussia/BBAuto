using System.Collections.Generic;
using System.Data;

namespace BBAuto.Logic.Services.Grade
{
  public interface IGradeService
  {
    IList<GradeModel> GetGrades(int idModel);
    GradeModel GetById(int id);
    DataTable GetDataTable(int modelId);
    GradeModel Save(GradeModel grade);
    void Delete(int id);
  }
}
