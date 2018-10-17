using BBAuto.Domain.Abstract;
using BBAuto.Domain.Import;
using BBAuto.Domain.DataBase;
using BBAuto.Domain.Lists;
using BBAuto.Domain.Senders;
using BBAuto.Domain.Logger;
using BBAutoConsoleApplication.config;

namespace BBAutoConsoleApplication
{
  class Program
  {
    static void Main(string[] args)
    {
      DataBase.InitDataBase();
      Provider.InitSqlProvider();
      AutoMapperConfiguration.Initialize();

      LogManager.Logger.Information("Program started");
      /* старые командировки */
      //IExcelImporter importer = new BusinessTripFromExcelFile { FilePath = @"\\bbmru08\depts\Accounting\Командировки\Реестр_" + DateTime.Today.Year + ".xls" };
      BusinessTripFromExcelFile businessTripFromExcelFile = new BusinessTripFromExcelFile { FilePath = @"\\bbmru08\1cv77\Autoexchange\Lotus\BBAuto" };
      if (businessTripFromExcelFile.StartImport())
        LogManager.Logger.Information("BusinessTrip loading done");

      ///* Сделать загрузку вручную */
      ////importer = new MileageMonthFromExcelFile { FilePath = @"J:\Hospital Care\Kasyanova Tatyana\Отчёты\Командировки в BBAuto\Загрузка Перечень сотрудников для заполнения ПЛ на мес.xlsx" };
      ////importer.StartImport();
      ////LogManager.Logger.Debug("Mileage Month loading done");

      IExcelImporter employeesImporter = new EmployeesFrom1C {FilePath = @"\\bbmru08\1cv77\Autoexchange\Lotus\BBAuto"};
      if (employeesImporter.StartImport())
        LogManager.Logger.Information("EmployeesFrom1C loading done");
      
      IExcelImporter tabelImporter = new TabelFrom1C { FilePath = @"\\bbmru08\1cv77\Autoexchange\Lotus\BBAuto\Time" };
      if (tabelImporter.StartImport())
        LogManager.Logger.Information("TabelFrom1C loading done");

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

      LogManager.Logger.Information("Program finished");
    }
  }
}
