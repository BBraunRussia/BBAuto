using System.Collections.Generic;

namespace BBAuto.Logic.Services.Account
{
  public interface IAccountService
  {
    void Agree(AccountModel account);
    IList<AccountModel> GetAccountForAgree();
  }
}
