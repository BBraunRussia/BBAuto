using System.Collections.Generic;
using BBAuto.Repositories.Entities;
using Insight.Database;

namespace BBAuto.Repositories.Interfaces
{
  [Sql(Schema = "dbo")]
  public interface IDbAccount
  {
    void UpsertAccount(DbAccount model);
    IList<DbAccount> GetAccounts();
    IList<DbAccount> GetAccountListForAgree();
  }
}
