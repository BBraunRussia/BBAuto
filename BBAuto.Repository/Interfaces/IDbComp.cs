using System.Collections.Generic;
using BBAuto.Repository.Models;
using Insight.Database;

namespace BBAuto.Repository.Interfaces
{
  [Sql(Schema = "dbo")]
  public interface IDbComp
  {
    void DeleteComp(int id);

    DbComp GetCompById(int id);

    IList<DbComp> GetCompList();

    DbComp UpsertComp(DbComp comp);
  }
}
