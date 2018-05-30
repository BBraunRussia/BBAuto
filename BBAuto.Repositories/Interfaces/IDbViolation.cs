using System;
using System.Collections.Generic;
using BBAuto.Repositories.Entities;
using Insight.Database;

namespace BBAuto.Repositories.Interfaces
{
  [Sql(Schema = "dbo")]
  public interface IDbViolation
  {
    void DeleteViolationById(int id);
    DbViolation GetViolationById(int id);
    IList<DbViolation> GetViolations();
    IList<DbViolation> GetViolationsByCarId(int carId);
    DbViolation UpsertViolation(DbViolation violation);
    IList<DbViolation> GetViolationsByDate(DateTime date);
  }
}
