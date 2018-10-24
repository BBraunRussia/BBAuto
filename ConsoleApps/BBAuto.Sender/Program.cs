using BBAuto.Domain.DataBase;
using BBAuto.Domain.Lists;
using BBAuto.Domain.Logger;
using BBAuto.Domain.Senders;
using Common;

namespace BBAuto.Sender
{
  class Program
  {
    static void Main(string[] args)
    {
      DataBase.InitDataBase();
      Provider.InitSqlProvider();
      AutoMapperConfiguration.Initialize();

      LogManager.Logger.Information("Sender started");

      var medicalCertSender = new NotificationSender(MedicalCertList.getInstance());
      if (medicalCertSender.SendNotification())
        LogManager.Logger.Information("MedicalCerts first notification sent");

      medicalCertSender.ClearStopIfNeed();

      if (medicalCertSender.SendNotificationOverdue())
        LogManager.Logger.Information("MedicalCerts notification sent");

      if (medicalCertSender.SendNotificationNotExist())
        LogManager.Logger.Information("MedicalCerts not exists notification sent");

      var licenceSender = new NotificationSender(LicenseList.getInstance());
      if (licenceSender.SendNotification())
        LogManager.Logger.Information("DriverLicense first notification sent");

      if (licenceSender.SendNotificationOverdue())
        LogManager.Logger.Information("DriverLicense notification sent");

      if (licenceSender.SendNotificationNotExist())
        LogManager.Logger.Information("DriverLicense not exists notification sent");

      var policySender = new PolicyListSender();
      if (policySender.SendNotification())
        LogManager.Logger.Information("Policies sent");

      var diagCardSender = new DiagCardSender();
      if (diagCardSender.SendNotification())
        LogManager.Logger.Information("DiagCards sent");

      var violationSender = new ViolationSender();
      if (violationSender.SendNotification())
        LogManager.Logger.Information("Violations sent");

      var accountSender = new AccountSender();
      if (accountSender.SendNotification())
        LogManager.Logger.Information("Accounts sent");

      LogManager.Logger.Information("Sender finished");
    }
  }
}
