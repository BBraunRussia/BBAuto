using BBAuto.Domain.Entities;
using BBAuto.Domain.ForCar;
using BBAuto.Domain.Lists;
using BBAuto.Domain.Static;
using System.Collections.Generic;
using System.Linq;
using BBAuto.Domain.Services.Mail;

namespace BBAuto.Domain.Senders
{
  public class AccountSender
  {
    public void SendNotification()
    {
      var accountList = AccountList.getInstance();
      IList<Account> list = accountList.GetAccountForAgree().ToList();

      if (!list.Any())
        return;

      var driversTo = GetDriverForSending(RolesList.Boss);

      var mailText = CreateMailToBoss(list);

      IMailService mailService = new MailService();

      mailService.SendNotification(driversTo, mailText);
    }

    private static Driver GetDriverForSending(RolesList role = RolesList.Editor)
    {
      return DriverList.getInstance().GetDriverListByRole(role).First();
    }

    private static string CreateMailToBoss(IEnumerable<Account> list)
    {
      return "Добрый день!\n\n" +
             $"В программе BBAuto появились новые счета по страховым полисам для согласования. Количество счетов: {list.Count()}";
    }
  }
}
