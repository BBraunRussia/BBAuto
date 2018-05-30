using System;
using System.Collections.Generic;
using BBAuto.Logic.Services.Account;

namespace BBAuto.Logic.Services.Policy
{
  public interface IPolicyService
  {
    IList<PolicyModel> GetPolicyListByAccountId(int accountId);
    decimal GetSumByAccountId(AccountModel account);

    IList<PolicyModel> GetPolicyList(DateTime date);
    IList<PolicyModel> GetPolicyEnds();

    string GetPolicyToMail(PolicyModel policy);

    PolicyModel Save(PolicyModel policy);
  }
}
