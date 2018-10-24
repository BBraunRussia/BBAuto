using BBAuto.Domain.Entities;
using BBAuto.Domain.ForCar;
using BBAuto.Domain.Lists;
using BBAuto.Domain.Static;
using System.Collections.Generic;
using System.Linq;
using BBAuto.Domain.Services.Mail;
using System;

namespace BBAuto.Domain.Senders
{
  public class AccountSender
  {
    public bool SendNotification()
    {
      try
      {
        var accountList = AccountList.getInstance();
        IList<Account> list = accountList.GetAccountForAgree().ToList();

        if (!list.Any())
        {
          Logger.LogManager.Logger.Debug("Счета для отправки не найдены");
          return false;
        }

        var driversTo = GetDriverForSending(RolesList.Boss);

        var mailText = CreateMailToBoss(list);

        IMailService mailService = new MailService();

        mailService.SendNotification(driversTo, mailText);

        return true;
      }
      catch (Exception ex)
      {
        Logger.LogManager.Logger.Error(ex, ex.Message);
        return false;
      }
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
