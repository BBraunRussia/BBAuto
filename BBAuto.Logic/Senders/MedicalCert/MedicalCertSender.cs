using System;
using System.Collections.Generic;
using System.Linq;
using BBAuto.Logic.Common;
using BBAuto.Logic.Entities;
using BBAuto.Logic.Lists;
using BBAuto.Logic.Services.Driver;
using BBAuto.Logic.Services.MedicalCert;
using BBAuto.Logic.Static;

namespace BBAuto.Logic.Senders.MedicalCert
{
  public class MedicalCertSender : IMedicalCertSender
  {
    private readonly IMedicalCertService _medicalCertService;
    private readonly IDriverService _driverService;

    public MedicalCertSender(
      IMedicalCertService medicalCertService,
      IDriverService driverService)
    {
      _medicalCertService = medicalCertService;
      _driverService = driverService;
    }

    public void SendNotificationOverdue()
    {
      var drivers = _driverService.GetDrivers();

      if (DateTime.Today.Day % 7 != 0)
        return;

      var list = GetListOverdue(drivers);

      var mailTextList = MailTextList.getInstance();
      var mailText = mailTextList.getItemByType(MailTextType.MedicalCert);

      foreach (var medicalCert in list)
      {
        medicalCert.NotificationSent = true;
      }
    }

    private IList<MedicalCertModel> GetListOverdue(IList<DriverModel> drivers)
    {
      var list = _medicalCertService.GetMedicalCertForNotification();

      return list.Where(m => drivers.FirstOrDefault(d => d.Id == m.DriverId && !d.IsStopNotification()) != null).ToList();
    }

    private string CreateMessageNotification(DriverModel driver, DateTime dateEnd, MailText mailText)
    {
      if (dateEnd < DateTime.Today)
      {
        return "Добрый день, " + driver.GetName(NameType.Full)
               + "!\r\n\r\nНапоминаем, что Вы своевременно не оформили водительскую медицинскую справку.\r\nПросим оформить данную справку.\r\nОригинал необходимо прислать в отдел кадров, а скан копию в транспортный отдел.\r\n\r\nС уважением,\r\nТранспортный отдел.";
      }

      return mailText?.Text.Replace("UserName", driver.GetName(NameType.Full))
               .Replace("DateEnd", dateEnd.ToShortDateString()) ?? "Шаблон текста письма не найден";
    }
  }
}
