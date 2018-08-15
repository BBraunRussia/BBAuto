using BBAuto.Domain.ForCar;
using BBAuto.Domain.Lists;
using BBAuto.Domain.Static;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BBAuto.Domain.Services.Mail;

namespace BBAuto.Domain.Senders
{
  public class PolicyListSender
  {
    private const int SendDay = 5;

    public void SendNotification()
    {
      if (DateTime.Today.Day != SendDay)
        return;

      var policyList = PolicyList.getInstance().GetPolicyEnds().ToList();

      if (!policyList.Any())
        return;

      var driversTo = DriverList.getInstance().GetDriverListByRole(RolesList.Editor).FirstOrDefault();

      var mailText = CreateMail(policyList);

      IMailService mailService = new MailService();

      mailService.SendNotification(driversTo, mailText);
    }
    
    private static string CreateMail(List<Policy> policies)
    {
      var sb = new StringBuilder();

      policies.ForEach(policy => sb.AppendLine(policy.ToMail()));
      
      var mailText = MailTextList.getInstance().getItemByType(MailTextType.Policy);

      return mailText?.Text.Replace("List", sb.ToString()) ?? "Шаблон текста письма не найден";
    }
  }
}
