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
      /* ������ ������������ */
      //IExcelImporter importer = new BusinessTripFromExcelFile { FilePath = @"\\bbmru08\depts\Accounting\������������\������_" + DateTime.Today.Year + ".xls" };
      BusinessTripFromExcelFile businessTripFromExcelFile = new BusinessTripFromExcelFile { FilePath = @"\\bbmru08\1cv77\Autoexchange\Lotus\BBAuto" };
      if (businessTripFromExcelFile.StartImport())
        LogManager.Logger.Information("BusinessTrip loading done");

      ///* ������� �������� ������� */
      ////importer = new MileageMonthFromExcelFile { FilePath = @"J:\Hospital Care\Kasyanova Tatyana\������\������������ � BBAuto\�������� �������� ����������� ��� ���������� �� �� ���.xlsx" };
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
