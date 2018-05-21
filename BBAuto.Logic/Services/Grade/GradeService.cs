using System.Collections.Generic;
using System.Data;
using System.Linq;
using AutoMapper;
using BBAuto.Repositories;
using BBAuto.Repositories.Entities;
using Common.Resources;

namespace BBAuto.Logic.Services.Grade
{
  public class GradeService : IGradeService
  {
    private readonly IDbContext _dbContext;

    public GradeService(IDbContext dbContext)
    {
      _dbContext = dbContext;
    }

    public IList<GradeModel> GetGrades(int modelId)
    {
      var dbModels = _dbContext.Grade.GetGradesByModelId(modelId);

      return Mapper.Map<IList<GradeModel>>(dbModels);
    }

    public GradeModel GetById(int id)
    {
      var dbModel = _dbContext.Grade.GetGradeById(id);

      return Mapper.Map<GradeModel>(dbModel);
    }

    public DataTable GetDataTable(int modelId)
    {
      var grades = GetGrades(modelId).ToList();

      var dt = new DataTable();
      dt.Columns.Add(Columns.Id);
      dt.Columns.Add(Columns.Name);

      grades.ForEach(grade => dt.Rows.Add(grade.ToRow()));

      return dt;
    }

    public GradeModel Save(GradeModel grade)
    {
      var dbModel = Mapper.Map<DbGrade>(grade);

      var result = _dbContext.Grade.UpsertGrade(dbModel);

      return Mapper.Map<GradeModel>(result);
    }

    public void Delete(int id)
    {
      _dbContext.Grade.DeleteGrade(id);
    }
  }
}
