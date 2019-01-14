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
  public class DiagCardSender
  {
    private const int SendDay = 5;
    private const int MailsCount = 100;

    public bool SendNotification()
    {
      if (DateTime.Today.Day != SendDay)
      {
        Logger.LogManager.Logger.Debug("Сегодня рассылка по диагностическим картам не производится");
        return false;
      }

      try
      {
        var diagCardList = DiagCardList.getInstance();
        var list = diagCardList.GetDiagCardEnds().ToList();

        var end = 0;

        if (!list.Any())
        {
          Logger.LogManager.Logger.Information("Диагностические карты для отправки не найдены");
          return false;
        }

        var stsList = STSList.getInstance();

        IMailService mailService = new MailService();

        while (end < list.Count)
        {
          var begin = end;
          end += end + MailsCount < list.Count ? MailsCount : list.Count - end;

          var listCut = new List<DiagCard>();

          for (var i = begin; i < end; i++)
            listCut.Add(list[i]);

          var carList = diagCardList.GetCarListFromDiagCardList(listCut).ToList();
          var files = new List<string>();

          foreach (var car in carList)
          {
            var sts = stsList.getItem(car.ID);
            if (!string.IsNullOrEmpty(sts.File))
              files.Add(sts.File);
          }

          var mailText = CreateMail(listCut);

          var employeeAutoDept = DriverList.getInstance().GetDriverListByRole(RolesList.Editor).FirstOrDefault();
          mailService.SendNotification(employeeAutoDept, mailText, true, files);
        }

        return true;
      }
      catch(Exception ex)
      {
        Logger.LogManager.Logger.Error(ex, ex.Message);
        return false;
      }
    }
    
    private static string CreateMail(IEnumerable<DiagCard> diagCards)
    {
      var sb = new StringBuilder();

      foreach (var diagCard in diagCards)
      {
        sb.AppendLine(diagCard.ToMail());
      }

      var mailTextList = MailTextList.getInstance();
      var mailText = mailTextList.getItemByType(MailTextType.DiagCard);

      return mailText?.Text.Replace("List", sb.ToString()) ?? "Шаблон текста письма не найден";
    }
  }
}