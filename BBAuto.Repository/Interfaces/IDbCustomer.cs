using System.Collections.Generic;
using BBAuto.Repository.Models;
using Insight.Database;

namespace BBAuto.Repository.Interfaces
{
  [Sql(Schema = "dbo")]
  public interface IDbCustomer
  {
    void DeleteCustomer(int id);

    IList<DbCustomer> GetCustomerList();

    DbCustomer GetCustomerById(int id);

    DbCustomer UpsertCustomer(DbCustomer customer);
  }
}
