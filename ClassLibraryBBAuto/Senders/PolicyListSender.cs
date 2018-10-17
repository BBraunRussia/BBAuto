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

    public bool SendNotification()
    {
      if (DateTime.Today.Day != SendDay)
      {
        Logger.LogManager.Logger.Debug("Сегодня рассылка по страховым полисам не производится");
        return false;
      }

      try
      {
        var policyList = PolicyList.getInstance().GetPolicyEnds().ToList();

        if (!policyList.Any())
        {
          Logger.LogManager.Logger.Information("Страховые полисы для отправки не найдены");
          return false;
        }

        var driversTo = DriverList.getInstance().GetDriverListByRole(RolesList.Editor).FirstOrDefault();

        var mailText = CreateMail(policyList);

        IMailService mailService = new MailService();

        mailService.SendNotification(driversTo, mailText);

        return true;
      }
      catch(Exception ex)
      {
        Logger.LogManager.Logger.Error(ex, ex.Message);
        return false;
      }
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
