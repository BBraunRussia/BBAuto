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
  public class ViolationSender
  {
    public bool SendNotification()
    {
      try
      {
        var list = ViolationList.getInstance().GetViolationForAccount();

        if (!list.Any())
        {
          Logger.LogManager.Logger.Debug("Нарушения ПДД для отправки не найдены");
          return false;
        }

        var driversTo = GetDriverForSending();

        var mailText = CreateMail(list);

        var email = new MailService();

        email.SendNotification(driversTo, mailText, false);

        return true;
      }
      catch(Exception ex)
      {
        Logger.LogManager.Logger.Error(ex, ex.Message);
        return false;
      }
    }

    private static Driver GetDriverForSending()
    {
      var driverList = DriverList.getInstance();

      return driverList.GetDriverListByRole(RolesList.Boss).First();
    }

    private static string CreateMail(IEnumerable<Violation> violations)
    {
      return "Добрый день!\n\n" +
             $"В программе BBAuto появились новые нарушения ПДД на согласование. Количество нарушений: {violations.Count()}";
    }
  }
}
