using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BBAuto.Logic.Lists;
using BBAuto.Logic.Services.Car;
using BBAuto.Logic.Services.DiagCard;
using BBAuto.Logic.Services.Driver;
using BBAuto.Logic.Services.MailService;
using BBAuto.Logic.Static;

namespace BBAuto.Logic.Senders.DiagCard
{
  public class DiagCardSender : IDiagCardSender
  {
    private const int SendDay = 5;
    private const int MailsCount = 100;

    private readonly IDiagCardService _diagCardService;
    private readonly ICarService _carService;
    private readonly IMailService _mailService;
    private readonly IDriverService _driverService;

    public DiagCardSender(
      IDiagCardService diagCardService,
      ICarService carService,
      IMailService mailService,
      IDriverService driverService)
    {
      _diagCardService = diagCardService;
      _carService = carService;
      _mailService = mailService;
      _driverService = driverService;
    }

    public void SendNotification()
    {
      if (DateTime.Today.Day != SendDay)
        return;

      var diagCards = _diagCardService.GetDiagCardsForSend();

      var end = 0;

      if (diagCards.Any())
      {
        var stsList = STSList.getInstance();

        while (end < diagCards.Count)
        {
          var begin = end;
          end += end + MailsCount < diagCards.Count ? MailsCount : diagCards.Count - end;

          var listCut = new List<DiagCardModel>();

          for (var i = begin; i < end; i++)
            listCut.Add(diagCards[i]);

          var carIds = diagCards.Select(diagCard => diagCard.CarId).Distinct();
          var files = (from carId in carIds select stsList.getItem(carId) into sts where sts.File != string.Empty select sts.File).ToList();

          var mailText = CreateMail(listCut);

          var employeeAutoDept = _driverService.GetDriversByRole(RolesList.Editor).FirstOrDefault();
          _mailService.SendNotification(employeeAutoDept, mailText, true, files);
        }
      }
    }
    
    private string CreateMail(IList<DiagCardModel> diagCards)
    {
      var mailTextList = MailTextList.getInstance();
      var mailText = mailTextList.getItemByType(MailTextType.DiagCard);
      if (mailText == null)
        return "Шаблон текста письма не найден";

      var sb = new StringBuilder();

      diagCards.ToList().ForEach(diagCard => sb.AppendLine(diagCard.ToMail(_carService)));
      
      return mailText.Text.Replace("List", sb.ToString());
    }
  }
}
