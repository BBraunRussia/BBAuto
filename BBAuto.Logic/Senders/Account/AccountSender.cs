using System.Collections.Generic;
using System.Linq;
using BBAuto.Logic.Services.Account;
using BBAuto.Logic.Services.Driver;
using BBAuto.Logic.Services.MailService;
using BBAuto.Logic.Static;

namespace BBAuto.Logic.Senders.Account
{
  public class AccountSender : IAccountSender
  {
    private readonly IAccountService _accountService;
    private readonly IDriverService _driverService;
    private readonly IMailService _mailService;

    public AccountSender(
      IAccountService accountService,
      IDriverService driverService,
      IMailService mailService)
    {
      _accountService = accountService;
      _driverService = driverService;
      _mailService = mailService;
    }

    public void SendNotification()
    {
      var list = _accountService.GetAccountForAgree();

      if (!list.Any())
        return;

      var driversTo = _driverService.GetDriversByRole(RolesList.Boss).FirstOrDefault();

      var mailText = CreateMailToBoss(list);

      _mailService.SendNotification(driversTo, mailText);
    }
    
    private string CreateMailToBoss(IEnumerable<AccountModel> list)
    {
      return "Добрый день!\n\n" +
             $"В программе BBAuto появились новые счета по страховым полисам для согласования. Количество счетов: {list.Count()}";
    }
  }
}
