using System.Collections.Generic;
using System.Linq;
using BBAuto.Logic.Services.Driver;
using BBAuto.Logic.Services.MailService;
using BBAuto.Logic.Services.Violation;
using BBAuto.Logic.Static;

namespace BBAuto.Logic.Senders.Violation
{
  public class ViolationSender : IViolationSender
  {
    private readonly IViolationService _violationService;
    private readonly IDriverService _driverService;
    private readonly IMailService _mailService;

    public ViolationSender(
      IViolationService violationService,
      IDriverService driverService,
      IMailService mailService)
    {
      _violationService = violationService;
      _driverService = driverService;
      _mailService = mailService;
    }

    public void SendNotification()
    {
      var list = _violationService.GetViolationForAccount();

      if (!list.Any())
        return;

      var driversTo = _driverService.GetDriversByRole(RolesList.Boss).FirstOrDefault();

      string mailText = CreateMail(list);

      _mailService.SendNotification(driversTo, mailText, false);
    }
    
    private string CreateMail(IList<ViolationModel> violations)
    {
      return "Добрый день!\n\n" +
             $"В программе BBAuto появились новые нарушения ПДД на согласование. Количество нарушений: {violations.Count}";
    }
  }
}
