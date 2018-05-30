using System.Collections.Generic;
using BBAuto.Repositories.Entities;

namespace BBAuto.Repositories.Interfaces
{
  public interface IDbPolicy
  {
    void DeletePolicy(int id);
    void DeletePolicyByAccountId(int accountId);
    DbPolicy GetPolicyById(int id);
    IList<DbPolicy> GetPolicys();
    IList<DbPolicy> GetPolicyListByAccountId(int accountId);
    void UpdatePolicyByAccountId(int id, int accountId, int numberId);
    DbPolicy UpsertPolicy(DbPolicy policy);
  }
}
