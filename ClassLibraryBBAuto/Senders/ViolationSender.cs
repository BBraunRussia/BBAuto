using BBAuto.Domain.Entities;
using BBAuto.Domain.ForCar;
using BBAuto.Domain.Lists;
using BBAuto.Domain.Static;
using System.Collections.Generic;
using System.Linq;
using BBAuto.Domain.Services.Mail;

namespace BBAuto.Domain.Senders
{
  public class ViolationSender
  {
    public void SendNotification()
    {
      var list = ViolationList.getInstance().GetViolationForAccount();

      if (!list.Any())
        return;

      var driversTo = GetDriverForSending();

      var mailText = CreateMail(list);

      var email = new MailService();

      email.SendNotification(driversTo, mailText, false);
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
