using BBAuto.Domain.Abstract;
using BBAuto.Domain.DataBase;
using BBAuto.Domain.Import;
using BBAuto.Domain.Logger;
using Common;

namespace BBAuto.Loader
{
  class Program
  {
    static void Main(string[] args)
    {
      DataBase.InitDataBase();
      Provider.InitSqlProvider();
      AutoMapperConfiguration.Initialize();

      LogManager.Logger.Information("Loader started");
      /* старые командировки */
      //IExcelImporter importer = new BusinessTripFromExcelFile { FilePath = @"\\bbmru08\depts\Accounting\Командировки\Реестр_" + DateTime.Today.Year + ".xls" };
      BusinessTripFromExcelFile businessTripFromExcelFile = new BusinessTripFromExcelFile { FilePath = @"\\bbmru08\1cv77\Autoexchange\Lotus\BBAuto" };
      if (businessTripFromExcelFile.StartImport())
        LogManager.Logger.Information("BusinessTrip loading done");

      ///* Сделать загрузку вручную */
      ////importer = new MileageMonthFromExcelFile { FilePath = @"J:\Hospital Care\Kasyanova Tatyana\Отчёты\Командировки в BBAuto\Загрузка Перечень сотрудников для заполнения ПЛ на мес.xlsx" };
      ////importer.StartImport();
      ////LogManager.Logger.Debug("Mileage Month loading done");

      IExcelImporter employeesImporter = new EmployeesFrom1C { FilePath = @"\\bbmru08\1cv77\Autoexchange\Lotus\BBAuto" };
      if (employeesImporter.StartImport())
        LogManager.Logger.Information("EmployeesFrom1C loading done");

      IExcelImporter tabelImporter = new TabelFrom1C { FilePath = @"\\bbmru08\1cv77\Autoexchange\Lotus\BBAuto\Time" };
      if (tabelImporter.StartImport())
        LogManager.Logger.Information("TabelFrom1C loading done");

      LogManager.Logger.Information("Loader finished");
    }
  }
}
