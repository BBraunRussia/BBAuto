using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BBAuto.Logic.Lists;
using BBAuto.Logic.Services.Driver;
using BBAuto.Logic.Services.MailService;
using BBAuto.Logic.Services.Policy;
using BBAuto.Logic.Static;

namespace BBAuto.Logic.Senders.PolicyList
{
  public class PolicyListSender : IPolicyListSender
  {
    private const int SendDay = 5;

    private readonly IPolicyService _policyService;
    private readonly IMailService _mailService;
    private readonly IDriverService _driverService;

    public PolicyListSender(
      IPolicyService policyService,
      IMailService mailService,
      IDriverService driverService)
    {
      _policyService = policyService;
      _mailService = mailService;
      _driverService = driverService;
    }

    public void SendNotification()
    {
      if (DateTime.Today.Day != SendDay)
        return;

      var list = _policyService.GetPolicyEnds();

      if (!list.Any())
        return;

      var driversTo = _driverService.GetDriversByRole(RolesList.Editor).FirstOrDefault();

      var mailText = CreateMail(list);

      _mailService.SendNotification(driversTo, mailText);
    }
    
    private string CreateMail(IList<PolicyModel> policies)
    {
      var sb = new StringBuilder();

      policies.ToList().ForEach(policy => sb.AppendLine(_policyService.GetPolicyToMail(policy)));
      
      var mailTextList = MailTextList.getInstance();
      var mailText = mailTextList.getItemByType(MailTextType.Policy);

      return mailText?.Text.Replace("List", sb.ToString()) ?? "Шаблон текста письма не найден";
    }
  }
}
