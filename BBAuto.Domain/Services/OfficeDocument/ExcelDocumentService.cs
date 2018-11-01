using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BBAuto.Domain.Common;
using BBAuto.Domain.Dictionary;
using BBAuto.Domain.Entities;
using BBAuto.Domain.ForCar;
using BBAuto.Domain.ForDriver;
using BBAuto.Domain.Lists;
using BBAuto.Domain.Services.Comp;
using BBAuto.Domain.Services.Documents;
using BBAuto.Domain.Services.DriverInstruction;
using BBAuto.Domain.Static;
using BBAuto.Domain.Tables;
using Common;

namespace BBAuto.Domain.Services.OfficeDocument
{
  public class ExcelDocumentService : IExcelDocumentService
  {
    public IDocument CreateExcelFromDGV(DataGridView dgv)
    {
      int minRowIndex = 0;
      int maxRowIndex = 0;
      int minColumnIndex = 0;
      int maxColumnIndex = 0;

      foreach (DataGridViewCell cell in dgv.SelectedCells)
      {
        if (cell.RowIndex > maxRowIndex)
          maxRowIndex = cell.RowIndex;
        if (cell.RowIndex < minRowIndex || minRowIndex == 0)
          minRowIndex = cell.RowIndex;

        if (cell.ColumnIndex > maxColumnIndex)
          maxColumnIndex = cell.ColumnIndex;
        if (cell.ColumnIndex < minColumnIndex || minColumnIndex == 0)
          minColumnIndex = cell.ColumnIndex;
      }

      int rowCount = maxRowIndex + 1;
      int columnCount = maxColumnIndex + 1;

      return CreateExcelFromDGV(dgv, minRowIndex, rowCount, minColumnIndex, columnCount);
    }

    public IDocument CreateExcelFromAllDGV(DataGridView dgv)
    {
      return CreateExcelFromDGV(dgv, 0, dgv.Rows.Count, 0, dgv.Columns.Count);
    }

    private IDocument CreateExcelFromDGV(DataGridView dgv, int minRow, int rowCount, int minColumn, int columnCount)
    {
      IExcelDoc excelDoc = new ExcelDoc();
      WriteHeader(excelDoc, dgv, minColumn, columnCount);

      int diffRow = GetDiffRows(minRow);
      int diffColumn = GetDiffColumns(minColumn);

      int index = 2;

      for (int i = minRow; i < rowCount; i++)
      {
        var IsAdded = false;
        for (var j = minColumn; j < columnCount; j++)
        {
          if (dgv.Rows[i].Cells[j].Visible)
          {
            var value = dgv.Rows[i].Cells[j].Value.ToString();

            if (DateTime.TryParse(value, out DateTime date))
              value = date.ToShortDateString();
            else if (value.All(char.IsDigit))
              value = "'" + value;
            
            excelDoc.setValue(index, dgv.Rows[i].Cells[j].ColumnIndex - diffColumn,
              value);
            IsAdded = true;
          }

        }
        if (IsAdded)
          index++;
      }

      return excelDoc;
    }

    private void WriteHeader(IExcelDoc excelDoc, DataGridView dgv, int minColumn, int columnCount)
    {
      int diffColumn = GetDiffColumns(minColumn);

      int index = 1;

      for (int j = minColumn; j < columnCount; j++)
      {
        if (dgv.Columns[j].Visible)
        {
          excelDoc.setValue(1, index, dgv.Columns[j].HeaderText);
          index++;
        }
      }
    }

    private int GetDiffColumns(int minColumn)
    {
      int diff = 1;
      if (minColumn > 1)
        diff = minColumn - 1;

      return diff;
    }

    private int GetDiffRows(int minRow)
    {
      int diff = 0;
      if (minRow > 0)
        diff = minRow - 1;

      return diff;
    }

    public IDocument CreateInvoice(Car car, Invoice invoice)
    {
      var excelDoc = OpenDocumentExcel("Накладная");

      excelDoc.setValue(7, 2, car.info.Owner);

      excelDoc.setValue(16, 82, invoice.Number);
      excelDoc.setValue(16, 98, invoice.Date.ToShortDateString());

      string fullNameAuto = string.Concat("Автомобиль ", car.Mark.Name, " ", car.info.Model, ", ", car.Grz);

      excelDoc.setValue(22, 10, fullNameAuto);
      excelDoc.setValue(22, 53, car.dateGet.ToShortDateString());

      GradeList grades = GradeList.getInstance();

      Grade grade = grades.getItem(Convert.ToInt32(car.GradeID));

      PTSList ptsList = PTSList.getInstance();
      PTS pts = ptsList.getItem(car);

      string fullDetailAuto = string.Concat("VIN ", car.vin, ", Двигатель ", car.eNumber, ", № кузова ",
        car.bodyNumber, ", Год выпуска ", car.Year, " г., Паспорт ",
        pts.Number, " от ", pts.Date.ToShortDateString(), ", мощность двигателя ", grade.EPower, " л.с.");

      excelDoc.setValue(47, 2, fullDetailAuto);

      var driverList = DriverList.getInstance();

      Driver driver1 = driverList.getItem(Convert.ToInt32(invoice.DriverFromID));
      Driver driver2 = driverList.getItem(Convert.ToInt32(invoice.DriverToID));

      excelDoc.setValue(9, 10, driver1.Dept);
      excelDoc.setValue(56, 11, driver1.Position);
      excelDoc.setValue(56, 63, driver1.FullName);

      excelDoc.setValue(11, 13, driver2.Dept);
      excelDoc.setValue(60, 11, driver2.Position);
      excelDoc.setValue(60, 63, driver2.FullName);

      return excelDoc;
    }

    public IDocument CreateNotice(Car car, DTP dtp)
    {
      var excelDoc = OpenDocumentExcel("Извещение о страховом случае");

      Owners owners = Owners.getInstance();

      excelDoc.setValue(6, 5, owners.getItem(Convert.ToInt32(car.ownerID))); //страхователь
      excelDoc.setValue(7, 6, "а/я 34, 196128"); //почтовый адрес
      excelDoc.setValue(8, 7, "320-40-04"); //телефон

      DriverCarList driverCarList = DriverCarList.GetInstance();
      Driver driver = driverCarList.GetDriver(car, dtp.Date);

      PassportList passportList = PassportList.getInstance();
      Passport passport = passportList.getLastPassport(driver);

      if (passport.Number != string.Empty)
      {
        string number = passport.Number;
        string[] numbers = number.Split(' ');

        excelDoc.setValue(10, 3, numbers[0]); //серия
        excelDoc.setValue(10, 6, numbers[1]); //номер

        excelDoc.setValue(11, 3, passport.GiveOrg); //кем выдан
        excelDoc.setValue(12, 4, passport.GiveDate.ToShortDateString()); //дата выдачи
      }

      PolicyList policyList = PolicyList.getInstance();
      Policy policy = policyList.getItem(car, PolicyType.КАСКО);
      excelDoc.setValue(14, 6, policy.Number); //полис

      excelDoc.setValue(16, 6, string.Concat(car.Mark.Name, " ", car.info.Model)); //марка а/м
      excelDoc.setValue(18, 6, car.Grz); //рег номер а/м
      excelDoc.setValue(20, 6, car.vin); //вин

      excelDoc.setValue(22, 6, dtp.Date.ToShortDateString()); //дата дтп

      excelDoc.setValue(27, 2, driver.FullName); //водитель фио

      Regions regions = Regions.getInstance();

      excelDoc.setValue(29, 3, regions.getItem(Convert.ToInt32(dtp.RegionId))); //город
      excelDoc.setValue(31, 14, dtp.Damage); //повреждения
      excelDoc.setValue(33, 2, dtp.Facts); //обстоятельства происшествия
      
      return excelDoc;
    }

    public IExcelDoc CreateWaybill(Car car, DateTime date, Driver driver = null)
    {
      date = new DateTime(date.Year, date.Month, 1);

      if (driver == null)
      {
        DriverCarList driverCarList = DriverCarList.GetInstance();
        driver = driverCarList.GetDriver(car, date);

        if (driver == null)
        {
          driver = driverCarList.GetDriver(car);
          var invoice = InvoiceList.getInstance().getItem(car);

          if (invoice?.DateMove?.Year == date.Year && invoice.DateMove.Value.Month == date.Month)
            date = new DateTime(date.Year, date.Month, invoice.DateMove.Value.Day);
        }
      }

      var excelDoc = OpenDocumentExcel("Путевой лист");

      excelDoc.setValue(3, 28, car.BBNumber);

      MyDateTime myDate = new MyDateTime(date.ToShortDateString());

      excelDoc.setValue(3, 39, driver.ID + "/01/" + myDate.MonthSlashYear());
      excelDoc.setValue(5, 15, myDate.DaysRange);
      excelDoc.setValue(5, 19, myDate.MonthToStringNominative());
      excelDoc.setValue(5, 32, date.Year.ToString());

      excelDoc.setValue(28, 35, car.info.Grade.EngineType.ShortName);

      MileageMonthList mml = new MileageMonthList(car.ID, date.Year + "-" + date.Month + "-01");
      /* Из файла Татьяны Мироновой пробег за месяц */
      excelDoc.setValue(17, 39, mml.PSN);
      excelDoc.setValue(32, 41, mml.Gas);
      excelDoc.setValue(34, 41, mml.GasBegin);
      excelDoc.setValue(35, 41, mml.GasEnd);
      excelDoc.setValue(36, 41, mml.GasNorm);
      excelDoc.setValue(37, 41, mml.GasNorm);
      excelDoc.setValue(42, 39, mml.PSK);
      excelDoc.setValue(43, 59, mml.Mileage);

      Owners owners = Owners.getInstance();
      string owner = owners.getItem(1);

      excelDoc.setValue(7, 8, owner);

      excelDoc.setValue(9, 11, string.Concat(car.Mark.Name, " ", car.info.Model));
      excelDoc.setValue(10, 17, car.Grz);

      excelDoc.setValue(11, 6, driver.FullName);
      excelDoc.setValue(46, 16, driver.Name);
      excelDoc.setValue(25, 40, driver.Name);

      LicenseList licencesList = LicenseList.getInstance();
      DriverLicense driverLicense = licencesList.getItem(driver);

      excelDoc.setValue(13, 10, driverLicense.Number);

      excelDoc.setValue(19, 9, owner);

      string suppyAddressName;

      if (driver.SuppyAddress != string.Empty)
      {
        suppyAddressName = driver.SuppyAddress;
      }
      else
      {
        SuppyAddressList suppyAddressList = SuppyAddressList.getInstance();
        SuppyAddress suppyAddress = suppyAddressList.getItemByRegion(driver.Region.ID);

        if (suppyAddress != null)
          suppyAddressName = suppyAddress.ToString();
        else
        {
          PassportList passportList = PassportList.getInstance();
          Passport passport = passportList.getLastPassport(driver);
          suppyAddressName = passport.Address;
        }
      }

      string suppyAddressName2 = string.Empty;

      if (suppyAddressName.Length > 40)
      {
        for (int i = 30; i < suppyAddressName.Length; i++)
        {
          if (suppyAddressName[i] == ' ')
          {
            suppyAddressName2 = suppyAddressName.Substring(i, suppyAddressName.Length - i);
            suppyAddressName = suppyAddressName.Substring(0, i);
          }
        }
      }

      excelDoc.setValue(24, 8, suppyAddressName);
      excelDoc.setValue(25, 1, suppyAddressName2);

      string mechanicName;

      EmployeesList employeesList = EmployeesList.getInstance();
      Employees accountant = employeesList.getItem(driver.Region, "Бухгалтер Б.Браун");

      if (driver.IsOne)
      {
        mechanicName = driver.Name;
      }
      else
      {
        Employees mechanic = employeesList.getItem(driver.Region, "Механик", true);
        mechanicName = mechanic == null
          ? driver.Name
          : mechanic.Name;
      }

      Employees dispatcher = employeesList.getItem(driver.Region, "Диспечер-нарядчик");
      string dispatcherName = dispatcher.Name;

      excelDoc.setValue(21, 40, mechanicName);
      excelDoc.setValue(46, 40, mechanicName);

      excelDoc.setValue(30, 18, dispatcherName);
      excelDoc.setValue(36, 18, dispatcherName);

      excelDoc.setValue(46, 72, accountant.Name);

      return excelDoc;
    }

    public void AddRouteInWayBill(IExcelDoc excelDoc, Car car, DateTime date, Fields fields)
    {
      var wayBillDaily = new WayBillDaily(car, date);
      wayBillDaily.Load();

      CopyWayBill(excelDoc, wayBillDaily);

      int k = 0;
      int beginDistance = wayBillDaily.BeginDistance;
      int endDistance = wayBillDaily.EndDistance;

      int curDistance = beginDistance;

      foreach (WayBillDay wayBillDay in wayBillDaily)
      {
        int i = 6 + (47 * k);
        foreach (Route route in wayBillDay)
        {
          MyPoint pointBegin = route.MyPoint1;
          MyPoint pointEnd = route.MyPoint2;

          excelDoc.setValue(i, 59, pointBegin.Name);
          excelDoc.setValue(i, 64, pointEnd.Name);
          excelDoc.setValue(i, 78, route.Distance.ToString());
          i += 2;
        }

        excelDoc.setValue(29 + (47 * k), 20, wayBillDay.Date.ToShortDateString());
        excelDoc.setValue(19 + (47 * k), 39, curDistance.ToString());
        curDistance += wayBillDay.Distance;
        if (fields == Fields.All)
        {
          excelDoc.setValue(43 + (47 * k), 39, curDistance.ToString());
          excelDoc.setValue(41 + (47 * k), 59, wayBillDay.Distance.ToString());
          excelDoc.setValue(33 + (47 * k), 20, wayBillDay.Date.ToShortDateString());
        }
        k++;
      }

      if ((k > 0) && (fields == Fields.All))
        excelDoc.setValue(43 + (47 * (k - 1)), 39, endDistance.ToString());
    }

    private static void CopyWayBill(IExcelDoc excelDoc, WayBillDaily wayBillDaily)
    {
      var i = 0;
      foreach (WayBillDay item in wayBillDaily)
      {
        if (i > 0)
          excelDoc.CopyRange("A1", "CF46", "A" + ((47 * i) + 1));

        excelDoc.setValue(6 + (47 * i), 15, item.Day);

        var fullNumber = GetWaBillFullNumber(excelDoc.getValue("AM4").ToString(), i + 1);

        excelDoc.setValue(4 + (47 * i), 39, fullNumber);

        excelDoc.setValue(12 + (47 * i), 6, item.Driver.FullName);
        excelDoc.setValue(44 + (47 * i), 16, item.Driver.Name);
        excelDoc.setValue(26 + (47 * i), 40, item.Driver.Name);

        i++;
      }
    }

    private static string GetWaBillFullNumber(string value, int currentNumber)
    {
      var wayBillFullNumber = value.Split('/');

      wayBillFullNumber[1] = currentNumber < 10 ? "0" : string.Empty;
      wayBillFullNumber[1] += currentNumber;

      var sb = new StringBuilder();
      
      foreach (var item in wayBillFullNumber)
      {
        if (sb.Length > 0)
          sb.Append("/");

        sb.Append(item);
      }

      return sb.ToString();
    }

    public IDocument CreateAttacheToOrder(Car car, Invoice invoice)
    {
      var excelDoc = OpenDocumentExcel("Приложение к приказу");

      string fullNameAuto = string.Concat(car.Mark.Name, " ", car.info.Model);

      excelDoc.setValue(18, 2, fullNameAuto);
      excelDoc.setValue(18, 3, car.Grz);
      
      Driver driver = DriverList.getInstance().getItem(Convert.ToInt32(invoice.DriverToID));

      excelDoc.setValue(18, 4, driver.FullName);
      excelDoc.setValue(18, 5, driver.Position);

      return excelDoc;
    }
    
    public IDocument CreateReportPolicy()
    {
      var date = DateTime.Today.AddMonths(1);

      var excelDoc = OpenDocumentExcel("Таблица страхования");

      var myDate = new MyDateTime(date.ToShortDateString());

      excelDoc.setValue(2, 1, "Страхуем в " + myDate.MonthToStringPrepositive() + " " + myDate.Year + " г.");

      var policyList = PolicyList.getInstance();
      var list = policyList.GetPolicyList(date);
      var listCar = policyList.GetCarListByPolicyList(list);

      var diagCardList = DiagCardList.getInstance();

      var rowIndex = 6;

      ICompService compService = new CompService();
      var compList = compService.GetCompList();

      var i = 1;
      foreach (var car in listCar)
      {
        var policyOsago = list.FirstOrDefault(policy => policy.Car.ID == car.ID && policy.Type == PolicyType.ОСАГО);
        var policyKasko = list.FirstOrDefault(policy => policy.Car.ID == car.ID && policy.Type == PolicyType.КАСКО);

        excelDoc.setValue(rowIndex, 1, (i++).ToString());
        
        excelDoc.setValue(rowIndex, 2, car.Grz);
        excelDoc.setValue(rowIndex, 3, car.Mark.Name);
        excelDoc.setValue(rowIndex, 4, car.info.Model);
        excelDoc.setValue(rowIndex, 5, car.vin);
        excelDoc.setValue(rowIndex, 6, car.Year);
        excelDoc.setValue(rowIndex, 7, compList.FirstOrDefault(comp => comp.Id == policyOsago?.CompId)?.Name ?? "(нет данных)");
        excelDoc.setValue(rowIndex, 8, policyOsago?.DateBegin.ToShortDateString() ?? "не надо");
        excelDoc.setValue(rowIndex, 9, compList.FirstOrDefault(comp => comp.Id == policyKasko?.CompId)?.Name ?? "(нет данных)");
        excelDoc.setValue(rowIndex, 10, policyKasko?.DateBegin.ToShortDateString() ?? "не надо");
        excelDoc.setValue(rowIndex, 11, car.info.Owner);
        excelDoc.setValue(rowIndex, 12, car.info.Owner);
        excelDoc.setValue(rowIndex, 13, car.info.Owner);
        ////compList.FirstOrDefault(comp => comp.Id == policyOsago?.CompId)?.Name ?? "(нет данных)");

        var diagCard = diagCardList.getItem(car);

        if (diagCard != null)
        {
          excelDoc.setValue(rowIndex, 14, diagCard.Date.ToShortDateString());
          excelDoc.setValue(rowIndex, 15, diagCard.Number);
        }

        rowIndex++;
      }

      return excelDoc;
    }

    public IDocument CreateReportInstractionDocument()
    {
      IExcelDoc document = new ExcelDoc();

      IDocumentsService documentsService = new DocumentsService();
      
      var instructionList = documentsService.GetList().Where(doc => doc.Instruction).ToList();

      WriteHeaderReportInstractionDocument(document, instructionList.OrderBy(ins => ins.Name).Select(ins => ins.Name).ToList());

      var driverList = DriverList.getInstance().GetList().Where(driver => driver.ID != Consts.ReserveDriverId);
      IDriverInstructionService driverInstructionService = new DriverInstructionService();
      var driverInstructionList = driverInstructionService.GetDriverInstructions();

      var i = 2;
      foreach (var driver in driverList)
      {
        document.setValue(i, 1, driver.Name);
        var j = 2;
        var driverInstructions = driverInstructionList.Where(drIns => drIns.DriverId == driver.ID).ToList();
        foreach (var documentInstruction in instructionList.OrderBy(ins => ins.Name))
        {
          var driverInstruction = driverInstructions.FirstOrDefault(drIns => drIns.DocumentId == documentInstruction.Id);
          document.setValue(i, j, driverInstruction?.Date.ToShortDateString() ?? "-");
          j++;
        }
        i++;
      }

      return document;
    }

    private void WriteHeaderReportInstractionDocument(IExcelDoc excelDoc, IList<string> documentNames)
    {
      excelDoc.setValue(1, 1, "ФИО водителя");

      for (var i = 0; i < documentNames.Count; i++)
      {
        excelDoc.setValue(1, i + 2, documentNames[i]);
      }
    }

    public IDocument CreateReportMileage(IList<Car> carList, DateTime dateBegin, DateTime dateEnd)
    {
      IExcelDoc excelDoc = new ExcelDoc();

      WriteHeaderReportMileage(excelDoc, dateBegin, dateEnd);

      var mileageList = MileageList.getInstance();

      var i = 2;
      foreach (var car in carList)
      {
        excelDoc.setValue(i, 1, car.Grz);
        excelDoc.setValue(i, 2, car.Mark.Name);
        excelDoc.setValue(i, 3, car.info.Model);
        excelDoc.setValue(i, 4, car.info.Region);
        excelDoc.setValue(i, 5, car.info.Driver.Name);

        var beginMileage = mileageList.getItem(car.ID, dateBegin)?.Count ?? 0;
        var endMileage = mileageList.getItem(car.ID, dateEnd)?.Count ?? 0;
        var diff = endMileage - beginMileage;

        excelDoc.setValue(i, 6, MyString.GetFormatedDigitInteger(beginMileage.ToString()));
        excelDoc.setValue(i, 7, MyString.GetFormatedDigitInteger(endMileage.ToString()));
        excelDoc.setValue(i, 8, MyString.GetFormatedDigitInteger(diff.ToString()));

        i++;
      }

      return excelDoc;
    }
    
    public IDocument CreateReportLoadMileage(IList<MileageReport> mileageReportList)
    {
      IExcelDoc excelDoc = new ExcelDoc();

      excelDoc.setValue(1, 1, "ФИО");
      excelDoc.setValue(1, 2, "Марка и модель автомобиля");
      excelDoc.setValue(1, 3, "Гос. номер");
      excelDoc.setValue(1, 4, "Пробег");
      excelDoc.setValue(1, 5, "Имя файла");
      excelDoc.setValue(1, 6, "Описание проблемы");
      
      var i = 2;
      foreach (var mileageReport in mileageReportList)
      {
        excelDoc.setValue(i, 1, mileageReport.Fio);

        var mark = mileageReport.Car?.Mark?.Name ?? "не определено";
        var model = mileageReport.Car?.info?.Model ?? "не определено";
        excelDoc.setValue(i, 2, mark + model);

        excelDoc.setValue(i, 3, mileageReport.Grz);
        excelDoc.setValue(i, 4, mileageReport.Mileage);
        excelDoc.setValue(i, 5, mileageReport.Filename);
        excelDoc.setValue(i, 6, mileageReport.Message);
        
        i++;
      }
      return excelDoc;
    }

    private void WriteHeaderReportMileage(IExcelDoc excelDoc, DateTime dateBegin, DateTime dateEnd)
    {
      excelDoc.setValue(1, 1, "Гос. номер");
      excelDoc.setValue(1, 2, "Марка");
      excelDoc.setValue(1, 3, "Модель");
      excelDoc.setValue(1, 4, "Регион");
      excelDoc.setValue(1, 5, "ФИО сотрудника");
      excelDoc.setValue(1, 6, $"Показания одометра на конец {dateBegin.Month}.{dateBegin.Year}");
      excelDoc.setValue(1, 7, $"Показания одометра на конец {dateEnd.Month}.{dateEnd.Year}");
      excelDoc.setValue(1, 8, "Пробег");
    }

    private static IExcelDoc OpenDocumentExcel(string name)
    {
      var template = TemplateList.getInstance().getItem(name);
      return new ExcelDoc(template.File);
    }
    
    public void CreateHeader(ExcelDoc excelDoc, string text)
    {
      excelDoc.SetHeader(text);
    }
  }
}
