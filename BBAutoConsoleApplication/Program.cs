using BBAuto.Domain.Abstract;
using BBAuto.Domain.Import;
using BBAuto.Domain.DataBase;
using BBAuto.Domain.Lists;
using BBAuto.Domain.Senders;
using BBAuto.Domain.Logger;

namespace BBAutoConsoleApplication
{
  class Program
  {
    static void Main(string[] args)
    {
      DataBase.InitDataBase();
      Provider.InitSqlProvider();

      LogManager.Logger.Debug("Program started");
      /* старые командировки */
      //IExcelImporter importer = new BusinessTripFromExcelFile { FilePath = @"\\bbmru08\depts\Accounting\Командировки\Реестр_" + DateTime.Today.Year + ".xls" };
      //BusinessTripFromExcelFile importer1 = new BusinessTripFromExcelFile { FilePath = @"\\bbmru08\1cv77\Autoexchange\Lotus\BBAuto" };
      //importer1.StartImport();
      //LogManager.Logger.Debug("BusinessTrip loading done");

      ///* Сделать загрузку вручную */
      ////importer = new MileageMonthFromExcelFile { FilePath = @"J:\Hospital Care\Kasyanova Tatyana\Отчёты\Командировки в BBAuto\Загрузка Перечень сотрудников для заполнения ПЛ на мес.xlsx" };
      ////importer.StartImport();
      ////LogManager.Logger.Debug("Mileage Month loading done");

      IExcelImporter importer = new EmployeesFrom1C {FilePath = @"\\bbmru08\1cv77\Autoexchange\Lotus\BBAuto"};
      importer.StartImport();
      LogManager.Logger.Debug("EmployeesFrom1C loading done");



      //importer = new TabelFrom1C { FilePath = @"\\bbmru08\1cv77\Autoexchange\Lotus\BBAuto\Time" };
      //importer.StartImport();
      //LogManager.Logger.Debug("TabelFrom1C loading done");

      var medicalCertList = MedicalCertList.getInstance();
      var medicalCertSender = new NotificationSender(medicalCertList);
      //medicalCertSender.SendNotification();
      //medicalCertSender.ClearStopIfNeed();
      medicalCertSender.SendNotificationOverdue();
      //medicalCertSender.SendNotificationNotExist();
      //LogManager.Logger.Debug("MedicalCerts sent");

      var licenseList = LicenseList.getInstance();
      var licenceSender = new NotificationSender(licenseList);
      //licenceSender.SendNotification();
      licenceSender.SendNotificationOverdue();
      licenceSender.SendNotificationNotExist();
      LogManager.Logger.Debug("Licenses sent");

      //var policySender = new PolicyListSender();
      //policySender.SendNotification();
      //LogManager.Logger.Debug("Policies sent");

      //var diagCardSender = new DiagCardSender();
      //diagCardSender.SendNotification();
      //LogManager.Logger.Debug("DiagCards sent");

      //var violationSender = new ViolationSender();
      //violationSender.SendNotification();
      //LogManager.Logger.Debug("Violations sent");

      var accountSender = new AccountSender();
      accountSender.SendNotification();
      LogManager.Logger.Debug("Accounts sent");

      LogManager.Logger.Debug("Program finished");
    }
  }
}
