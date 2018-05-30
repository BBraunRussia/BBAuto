using System.Collections.Generic;
using AutoMapper;
using BBAuto.Logic.Services.MailService;
using BBAuto.Repositories;
using BBAuto.Repositories.Entities;

namespace BBAuto.Logic.Services.Account
{
  public class AccountService : IAccountService
  {
    private readonly IDbContext _dbContext;
    private readonly IMailService _mailService;
    
    public AccountService(
      IDbContext dbContext,
      IMailService mailService)
    {
      _dbContext = dbContext;
      _mailService = mailService;
    }

    public void Agree(AccountModel account)
    {
      if (account.Agreed)
        return;

      _mailService.SendMailAccount(account);
      account.Agreed = true;

      var dbAccount = Mapper.Map<DbAccount>(account);

      _dbContext.Account.UpsertAccount(dbAccount);
    }

    public IList<AccountModel> GetAccountForAgree()
    {
      var dbAccountList = _dbContext.Account.GetAccountListForAgree();

      return Mapper.Map<IList<AccountModel>>(dbAccountList);
    }
  }
}
