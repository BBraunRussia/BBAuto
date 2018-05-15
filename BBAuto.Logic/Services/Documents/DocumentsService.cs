using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BBAuto.Logic.Common;
using BBAuto.Logic.Dictionary;
using BBAuto.Logic.Entities;
using BBAuto.Logic.ForCar;
using BBAuto.Logic.ForDriver;
using BBAuto.Logic.Lists;
using BBAuto.Logic.Services.Car;
using BBAuto.Logic.Services.DiagCard;
using BBAuto.Logic.Static;
using BBAuto.Logic.Tables;
using Common.Resources;

namespace BBAuto.Logic.Services.Documents
{
  public class DocumentsService : IDocumentsService
  {
    private readonly DriverList _driverList;

    private readonly IDiagCardService _diagCardService;
    private readonly ICarService _carService;

    public DocumentsService(
      IDiagCardService diagCardService,
      ICarService carService)
    {
      _diagCardService = diagCardService;
      _carService = carService;

      _driverList = DriverList.getInstance();
    }
    /*
    public DocumentsService(Entities.Car car, IDiagCardService diagCardService, Invoice invoice = null)
    {
      Init();
      _car = car;
      _diagCardService = diagCardService;
      _invoice = invoice;
    }
    */
    
    public ExcelDocument CreateExcelFromDgv(DataGridView dgv)
    {
      var minRowIndex = 0;
      var maxRowIndex = 0;
      var minColumnIndex = 0;
      var maxColumnIndex = 0;

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

      var rowCount = maxRowIndex + 1;
      var columnCount = maxColumnIndex + 1;

      return CreateExcelFromDGV(dgv, minRowIndex, rowCount, minColumnIndex, columnCount);
    }

    public ExcelDocument CreateExcelFromAllDgv(DataGridView dgv)
    {
      return CreateExcelFromDGV(dgv, 0, dgv.Rows.Count, 0, dgv.Columns.Count);
    }

    private ExcelDocument CreateExcelFromDGV(DataGridView dgv, int minRow, int rowCount, int minColumn, int columnCount)
    {
      var document = new ExcelDocument();
      document.WriteHeader(dgv, minColumn, columnCount);

      int diffRow = GetDiffRows(minRow);
      int diffColumn = GetDiffColumns(minColumn);

      int index = 2;

      for (int i = minRow; i < rowCount; i++)
      {
        var isAdded = false;
        for (int j = minColumn; j < columnCount; j++)
        {
          if (dgv.Rows[i].Cells[j].Visible)
          {
            document.SetValue(index, dgv.Rows[i].Cells[j].ColumnIndex - diffColumn,
              dgv.Rows[i].Cells[j].Value.ToString());
            isAdded = true;
          }
        }
        if (isAdded)
          index++;
      }

      return document;
    }
    
    private static int GetDiffColumns(int minColumn)
    {
      var diff = 1;
      if (minColumn > 1)
        diff = minColumn - 1;

      return diff;
    }

    private static int GetDiffRows(int minRow)
    {
      var diff = 0;
      if (minRow > 0)
        diff = minRow - 1;

      return diff;
    }

    public ExcelDocument CreateDocumentInvoice(int carId, int invoiceId)
    {
      var invoice = InvoiceList.getInstance().GetItem(invoiceId);
      if (invoice == null)
        return null;
      
      var car = _carService.GetCarById(carId);

      var owner = Owners.getInstance().getItem(car.OwnerId);
      var mark = Marks.getInstance().getItem(car.MarkId);
      var model = ModelList.getInstance().getItem(car.ModelId);

      var document = new ExcelDocument("Накладная");

      document.SetValue(7, 2, owner);

      document.SetValue(16, 82, invoice.Number);
      document.SetValue(16, 98, invoice.Date.ToShortDateString());

      string fullNameAuto = string.Concat("Автомобиль ", mark, " ", model.Name, ", ", car.Grz);

      document.SetValue(22, 10, fullNameAuto);
      document.SetValue(22, 53, car.DateGet.ToShortDateString());

      var grades = GradeList.getInstance();

      var grade = grades.getItem(car.GradeId);

      var ptsList = PTSList.getInstance();
      var pts = ptsList.getItem(car.Id);

      var fullDetailAuto = string.Concat("VIN ", car.Vin, ", Двигатель ", car.ENumber, ", № кузова ",
        car.BodyNumber, ", Год выпуска ", car.Year, " г., Паспорт ",
        pts.Number, " от ", pts.Date.ToShortDateString(), ", мощность двигателя ", grade.EPower, " л.с.");

      document.SetValue(47, 2, fullDetailAuto);

      Driver driver1 = _driverList.getItem(Convert.ToInt32(invoice.DriverFromID));
      Driver driver2 = _driverList.getItem(Convert.ToInt32(invoice.DriverToID));

      document.SetValue(9, 10, driver1.Dept);
      document.SetValue(56, 11, driver1.Position);
      document.SetValue(56, 63, driver1.GetName(NameType.Full));

      document.SetValue(11, 13, driver2.Dept);
      document.SetValue(60, 11, driver2.Position);
      document.SetValue(60, 63, driver2.GetName(NameType.Full));

      return document;
    }

    public ExcelDocument CreateNotice(int carId, DTP dtp)
    {
      var car = _carService.GetCarById(carId);
      var mark = MarkList.getInstance().getItem(car.MarkId);
      var model = ModelList.getInstance().getItem(car.ModelId);

      var document = new ExcelDocument("Извещение о страховом случае");

      var owners = Owners.getInstance();

      document.SetValue(6, 5, owners.getItem(Convert.ToInt32(car.OwnerId))); //страхователь
      document.SetValue(7, 6, "а/я 34, 196128"); //почтовый адрес
      document.SetValue(8, 7, "320-40-04"); //телефон

      var driverCarList = DriverCarList.getInstance();
      var driver = driverCarList.GetDriver(car.Id, dtp.Date);

      var passportList = PassportList.getInstance();
      var passport = passportList.getLastPassport(driver);

      if (passport.Number != string.Empty)
      {
        var number = passport.Number;
        var numbers = number.Split(' ');

        document.SetValue(10, 3, numbers[0]); //серия
        document.SetValue(10, 6, numbers[1]); //номер

        document.SetValue(11, 3, passport.GiveOrg); //кем выдан
        document.SetValue(12, 4, passport.GiveDate.ToShortDateString()); //дата выдачи
      }

      var policyList = PolicyList.getInstance();
      var policy = policyList.getItem(car.Id, PolicyType.КАСКО);
      document.SetValue(14, 6, policy.Number); //полис

      document.SetValue(16, 6, string.Concat(mark.Name, " ", model.Name)); //марка а/м
      document.SetValue(18, 6, car.Grz); //рег номер а/м
      document.SetValue(20, 6, car.Vin); //вин

      document.SetValue(22, 6, dtp.Date.ToShortDateString()); //дата дтп

      document.SetValue(27, 2, driver.GetName(NameType.Full)); //водитель фио

      var regions = Regions.getInstance();

      document.SetValue(29, 3, regions.getItem(Convert.ToInt32(dtp.IDRegion))); //город
      document.SetValue(31, 14, dtp.Damage); //повреждения
      document.SetValue(33, 2, dtp.Facts); //обстоятельства происшествия

      //SsDTP ssDTP = SsDTPList.getInstance().getItem(_car.Mark);

      //_excelDoc.setValue(63, 11, ssDTP.ServiceStantion);

      //DateTime date = DateTime.Today;
      //MyDateTime myDate = new MyDateTime(date.ToShortDateString());

      //_excelDoc.setValue(71, 3, string.Concat("« ", date.Day.ToString(), " »"));
      //_excelDoc.setValue(71, 4, myDate.MonthToStringGenitive());
      //_excelDoc.setValue(71, 8, date.Year.ToString().Substring(2, 2));

      return document;
    }

    /* Старое извещение
      public void showNotice(DTP dtp)
    {
        _excelDoc = openDocumentExcel("Извещение о страховом случае");

        Owners owners = Owners.getInstance();

        _excelDoc.setValue(7, 4, owners.getItem(Convert.ToInt32(_car.ownerID)));
        _excelDoc.setValue(8, 5, "а/я 34, 196128");
        _excelDoc.setValue(9, 6, "320-40-04");

        DriverCarList driverCarList = DriverCarList.getInstance();
        Driver driver = driverCarList.GetDriver(_car, dtp.Date);

        PassportList passportList = PassportList.getInstance();
        Passport passport = passportList.getLastPassport(driver);

        if (passport.Number != string.Empty)
        {
            string number = passport.Number;
            string[] numbers = number.Split(' ');

            _excelDoc.setValue(11, 2, numbers[0]);
            _excelDoc.setValue(11, 5, numbers[1]);

            _excelDoc.setValue(12, 2, passport.GiveOrg);
            _excelDoc.setValue(13, 3, passport.GiveDate.ToShortDateString());
        }

        PolicyList policyList = PolicyList.getInstance();
        Policy policy = policyList.getItem(_car, PolicyType.КАСКО);
        _excelDoc.setValue(15, 5, policy.Number);

        _excelDoc.setValue(17, 5, string.Concat(_car.Mark.Name, " ", _car.info.Model));
        _excelDoc.setValue(19, 5, _car.Grz);
        _excelDoc.setValue(21, 5, _car.vin);

        _excelDoc.setValue(23, 5, dtp.Date.ToShortDateString());

        _excelDoc.setValue(28, 1, driver.GetName(NameType.Full));

        Regions regions = Regions.getInstance();

        _excelDoc.setValue(30, 2, regions.getItem(Convert.ToInt32(dtp.IDRegion)));
        _excelDoc.setValue(32, 13, dtp.Damage);
        _excelDoc.setValue(34, 1, dtp.Facts);

        SsDTP ssDTP = SsDTPList.getInstance().getItem(_car.Mark);

        _excelDoc.setValue(63, 11, ssDTP.ServiceStantion);

        DateTime date = DateTime.Today;
        MyDateTime myDate = new MyDateTime(date.ToShortDateString());

        _excelDoc.setValue(71, 3, string.Concat("« ", date.Day.ToString(), " »"));
        _excelDoc.setValue(71, 4, myDate.MonthToStringGenitive());
        _excelDoc.setValue(71, 8, date.Year.ToString().Substring(2, 2));

        _excelDoc.Show();
    }
     
     */

    public ExcelDocument CreateWaybill(int carId, DateTime date, Driver driver = null)
    {
      var car = _carService.GetCarById(carId);
      var mark = MarkList.getInstance().getItem(car.MarkId);
      var model = ModelList.getInstance().getItem(car.ModelId);
      var grade = GradeList.getInstance().getItem(car.GradeId);

      date = new DateTime(date.Year, date.Month, 1);

      if (driver == null)
      {
        var driverCarList = DriverCarList.getInstance();
        driver = driverCarList.GetDriver(carId, date);

        if (driver == null)
        {
          driver = driverCarList.GetDriver(carId);
          var invoiceList = InvoiceList.getInstance();
          var invoice = invoiceList.GetItem(carId);

          if (!string.IsNullOrEmpty(invoice?.DateMove))
          {
            DateTime.TryParse(invoice.DateMove, out DateTime dateMove);
            if (dateMove.Year == date.Year && dateMove.Month == date.Month)
              date = new DateTime(date.Year, date.Month, dateMove.Day);
          }
        }
      }
      
      var document = new ExcelDocument("Путевой лист");

      document.SetValue(4, 28, car.BbNumber);

      var myDate = new MyDateTime(date.ToShortDateString());

      document.SetValue(4, 39, driver.Id + "/01/" + myDate.MonthSlashYear());
      document.SetValue(6, 15, myDate.DaysRange);
      document.SetValue(6, 19, myDate.MonthToStringNominative());
      document.SetValue(6, 32, date.Year.ToString());

      document.SetValue(29, 35, grade.EngineType.ShortName);

      var mml = new MileageMonthList(carId, date.Year + "-" + date.Month + "-01");
      /* Из файла Татьяны Мироновой пробег за месяц */
      document.SetValue(19, 39, mml.PSN);
      document.SetValue(33, 41, mml.Gas);
      document.SetValue(35, 41, mml.GasBegin);
      document.SetValue(36, 41, mml.GasEnd);
      document.SetValue(37, 41, mml.GasNorm);
      document.SetValue(38, 41, mml.GasNorm);
      document.SetValue(43, 39, mml.PSK);
      document.SetValue(41, 59, mml.Mileage);

      var owners = Owners.getInstance();
      var owner = owners.getItem(1);

      document.SetValue(8, 8, owner);

      document.SetValue(10, 11, string.Concat(mark.Name, " ", model.Name));
      document.SetValue(11, 17, car.Grz);

      document.SetValue(12, 6, driver.GetName(NameType.Full));
      document.SetValue(44, 16, driver.GetName(NameType.Short));
      document.SetValue(26, 40, driver.GetName(NameType.Short));

      var licencesList = LicenseList.getInstance();
      var driverLicense = licencesList.getItem(driver);

      document.SetValue(14, 10, driverLicense.Number);

      document.SetValue(20, 9, owner);

      string suppyAddressName;

      if (driver.suppyAddress != string.Empty)
      {
        suppyAddressName = driver.suppyAddress;
      }
      else
      {
        var suppyAddressList = SuppyAddressList.getInstance();
        var suppyAddress = suppyAddressList.getItemByRegion(driver.Region.Id);

        if (suppyAddress != null)
          suppyAddressName = suppyAddress.ToString();
        else
        {
          var passportList = PassportList.getInstance();
          var passport = passportList.getLastPassport(driver);
          suppyAddressName = passport.Address;
        }
      }

      var suppyAddressName2 = string.Empty;

      if (suppyAddressName.Length > 40)
      {
        for (var i = 30; i < suppyAddressName.Length; i++)
        {
          if (suppyAddressName[i] == ' ')
          {
            suppyAddressName2 = suppyAddressName.Substring(i, suppyAddressName.Length - i);
            suppyAddressName = suppyAddressName.Substring(0, i);
          }
        }
      }

      document.SetValue(25, 8, suppyAddressName);
      document.SetValue(26, 1, suppyAddressName2);

      string mechanicName;

      var employeesList = EmployeesList.getInstance();
      var accountant = employeesList.getItem(driver.Region, "Бухгалтер Б.Браун");

      if (driver.IsOne)
      {
        mechanicName = driver.GetName(NameType.Short);
      }
      else
      {
        var mechanic = employeesList.getItem(driver.Region, "Механик", true);
        mechanicName = mechanic == null
          ? driver.GetName(NameType.Short)
          : mechanic.Name;
      }

      var dispatcher = employeesList.getItem(driver.Region, "Диспечер-нарядчик");
      var dispatcherName = dispatcher.Name;

      document.SetValue(22, 40, mechanicName);
      document.SetValue(44, 40, mechanicName);

      document.SetValue(31, 18, dispatcherName);
      document.SetValue(35, 18, dispatcherName);

      document.SetValue(43, 72, accountant.Name);

      return document;
    }

    public void AddRouteInWayBill(ExcelDocument document, int carId, DateTime date, Fields fields)
    {
      var wayBillDaily = new WayBillDaily(carId, date);
      wayBillDaily.Load();

      CopyWayBill(document, wayBillDaily);

      var k = 0;
      var beginDistance = wayBillDaily.BeginDistance;
      var endDistance = wayBillDaily.EndDistance;

      var curDistance = beginDistance;

      foreach (WayBillDay wayBillDay in wayBillDaily)
      {
        var i = 6 + 47 * k;
        foreach (Route route in wayBillDay)
        {
          var pointBegin = route.MyPoint1;
          var pointEnd = route.MyPoint2;

          document.SetValue(i, 59, pointBegin.Name);
          document.SetValue(i, 64, pointEnd.Name);
          document.SetValue(i, 78, route.Distance.ToString());
          i += 2;
        }

        document.SetValue(29 + (47 * k), 20, wayBillDay.Date.ToShortDateString());
        document.SetValue(19 + (47 * k), 39, curDistance.ToString());
        curDistance += wayBillDay.Distance;
        if (fields == Fields.All)
        {
          document.SetValue(43 + (47 * k), 39, curDistance.ToString());
          document.SetValue(41 + (47 * k), 59, wayBillDay.Distance.ToString());
          document.SetValue(33 + (47 * k), 20, wayBillDay.Date.ToShortDateString());
        }
        k++;
      }

      if (k > 0 && fields == Fields.All)
        document.SetValue(43 + (47 * (k - 1)), 39, endDistance.ToString());
    }

    private void CopyWayBill(ExcelDocument document, WayBillDaily wayBillDaily)
    {
      var i = 0;
      foreach (WayBillDay item in wayBillDaily)
      {
        if (i > 0)
          document.CopyRange("A1", "CF46", "A" + ((47 * i) + 1));

        document.SetValue(6 + (47 * i), 15, item.Day);

        document.SetValue(4 + (47 * i), 39, GetWaBillFullNumber(document, i + 1));

        document.SetValue(12 + (47 * i), 6, item.Driver.GetName(NameType.Full));
        document.SetValue(44 + (47 * i), 16, item.Driver.GetName(NameType.Short));
        document.SetValue(26 + (47 * i), 40, item.Driver.GetName(NameType.Short));

        i++;
      }
    }

    private string GetWaBillFullNumber(ExcelDocument document, int currentNumber)
    {
      string[] wayBillFullNumber = document.GetValue("AM4").ToString().Split('/');

      wayBillFullNumber[1] = currentNumber < 10 ? "0" : string.Empty;
      wayBillFullNumber[1] += currentNumber;

      StringBuilder sb = new StringBuilder();

      foreach (string item in wayBillFullNumber)
      {
        if (sb.Length > 0)
          sb.Append("/");

        sb.Append(item);
      }

      return sb.ToString();
    }

    public ExcelDocument CreateAttacheToOrder(int carId, int invoiceId)
    {
      var car = _carService.GetCarById(carId);
      var mark = MarkList.getInstance().getItem(car.MarkId);
      var model = ModelList.getInstance().getItem(car.ModelId);
      var invoice = InvoiceList.getInstance().GetItem(invoiceId);
      if (invoice == null)
        return null;

      var document = new ExcelDocument("Приложение к приказу");

      var fullNameAuto = string.Concat(mark.Name, " ", model.Name);

      document.SetValue(18, 2, fullNameAuto);
      document.SetValue(18, 3, car.Grz);

      var driver = _driverList.getItem(Convert.ToInt32(invoice.DriverToID));

      document.SetValue(18, 4, driver.GetName(NameType.Full));
      document.SetValue(18, 5, driver.Position);

      return document;
    }

    public void PrintProxyOnSto(int carId, int invoiceId)
    {
      var wordDoc = CreateProxyOnSto(carId, invoiceId);

      wordDoc.setValue("до 31 декабря 2017 года", "до 31 декабря 2018 года");

      var myDate = new MyDateTime(DateTime.Today.ToShortDateString());
      wordDoc.setValue(myDate.ToLongString(), "01 января 2018");

      wordDoc.Print();
    }

    public WordDocument CreateProxyOnSto(int carId, int invoiceId)
    {
      var car = _carService.GetCarById(carId);
      var mark = MarkList.getInstance().getItem(car.MarkId);
      var model = ModelList.getInstance().getItem(car.ModelId);
      var invoice = InvoiceList.getInstance().GetItem(invoiceId);
      if (invoice == null)
        return null;
      var color = Colors.GetInstance().getItem(car.ColorId);

      var wordDoc = openDocumentWord("Доверенность на предоставление интересов на СТО");

      var driverCarList = DriverCarList.getInstance();

      var driver = invoice == null
        ? driverCarList.GetDriver(carId)
        : _driverList.getItem(Convert.ToInt32(invoice.DriverToID));

      var myDate = new MyDateTime(DateTime.Today.ToShortDateString());
      wordDoc.setValue("текущая дата", myDate.ToLongString());

      var fio = string.Empty;
      if (driver != null)
        fio = driver.GetName(NameType.Full);

      wordDoc.setValue("ФИО регионального представителя", fio);

      var passportList = PassportList.getInstance();

      Passport passport = null;
      if (driver != null)
        passport = passportList.getLastPassport(driver);

      var passportToString = "нет данных";

      if (passport != null)
        passportToString = string.Concat(passport.Number, ", выдан ", passport.GiveDate.ToShortDateString(), ", ",
          passport.GiveOrg, ", Адрес: ", passport.Address);

      wordDoc.setValue("паспорт регионального представителя", passportToString);

      var fullNameAuto = string.Concat(mark.Name, " ", model.Name);
      wordDoc.setValue("Название марки автомобиля", fullNameAuto);
      wordDoc.setValue("VIN-автомобиля", car.Vin);
      wordDoc.setValue("Модель и марка двигателя автомобиля", car.ENumber);
      wordDoc.setValue("Номер кузова автомобиля", car.BodyNumber);
      wordDoc.setValue("Год выпуска автомобиля", car.Year.ToString());
      wordDoc.setValue("Цвет автомобиля", color);

      var ptsList = PTSList.getInstance();
      var pts = ptsList.getItem(car.Id);

      var ptsName = string.Concat(pts.Number, ", выдан ", pts.Date.ToShortDateString(), " ", pts.GiveOrg);

      wordDoc.setValue("ПТС автомобиля", ptsName);
      wordDoc.setValue("ГРЗ автомобиля", car.Grz);
      wordDoc.setValue("текущий год", DateTime.Today.Year.ToString());

      return wordDoc;
    }

    public WordDocument CreateActFuelCard(int invoiceId)
    {
      var invoice = InvoiceList.getInstance().GetItem(invoiceId);
      if (invoice == null)
      {
        MessageBox.Show("Для формирования акта необходимо перейти на страницу \"Перемещения\"", Captions.Warning,
          MessageBoxButtons.OK, MessageBoxIcon.Warning);
        return null;
      }

      var wordDoc = openDocumentWord("Акт передачи топливной карты");

      var fuelCardDriverList = FuelCardDriverList.getInstance();

      var driverTo = _driverList.getItem(Convert.ToInt32(invoice.DriverToID));
      var list = fuelCardDriverList.ToList(driverTo);

      var regions = Regions.getInstance();
      var regionName = regions.getItem(Convert.ToInt32(invoice.RegionToID));

      var i = 1;

      foreach (var fuelCardDriver in list)
      {
        wordDoc.AddRowInTable(1, i.ToString(), driverTo.GetName(NameType.Full), regionName,
          fuelCardDriver.FuelCard.Number);
        wordDoc.AddRowInTable(2, i.ToString(), driverTo.GetName(NameType.Full), regionName,
          fuelCardDriver.FuelCard.Number, fuelCardDriver.FuelCard.Pin);

        i++;
      }

      if (list.Count == 1)
        wordDoc.setValue("Количество карт", "1 (одна) карта.");
      else if (list.Count == 2)
        wordDoc.setValue("Количество карт", "2 (две) карты.");
      else if (list.Count != 0)
        wordDoc.setValue("Количество карт", list.Count + "карт(ы).");

      return wordDoc;
    }

    public ExcelDocument CreatePolicyTable()
    {
      const int indexBegin = 6;
      var date = DateTime.Today.AddMonths(1);

      var document = new ExcelDocument("Таблица страхования");

      var myDate = new MyDateTime(date.ToShortDateString());

      document.SetValue(2, 1, "Страхуем в " + myDate.MonthToStringPrepositive() + " " + myDate.Year + " г.");

      var policyList = PolicyList.getInstance();
      var list = policyList.GetPolicyList(date);
      var listCar = policyList.GetCarListByPolicyList(list);
      
      var rowIndex = indexBegin;

      foreach (var car in listCar)
      {
        document.SetValue(rowIndex, 2, car.Grz);
        document.SetValue(rowIndex, 3, car.Mark.Name);
        document.SetValue(rowIndex, 4, car.info.Model);
        document.SetValue(rowIndex, 5, car.vin);
        document.SetValue(rowIndex, 6, car.Year);
        document.SetValue(rowIndex, 7, GetPolicyBeginDate(list, car, PolicyType.ОСАГО));
        document.SetValue(rowIndex, 8, GetPolicyBeginDate(list, car, PolicyType.КАСКО));
        document.SetValue(rowIndex, 9, car.info.Owner);
        document.SetValue(rowIndex, 10, car.info.Owner);
        document.SetValue(rowIndex, 11, car.info.Owner);

        var diagCard = _diagCardService.GetByCarId(car.Id);

        if (diagCard != null)
        {
          document.SetValue(rowIndex, 12, diagCard.Date.ToShortDateString());
          document.SetValue(rowIndex, 13, diagCard.Number);
        }

        rowIndex++;
      }

      return document;
    }

    private static string GetPolicyBeginDate(IEnumerable<Policy> list, Entities.Car car, PolicyType policyType)
    {
      var newList = list.Where(policy => policy.Car.Id == car.Id && policy.Type == policyType).ToList();

      var osagoBeginDate = "не надо";

      if (newList.Count > 0)
        osagoBeginDate = newList.First().DateBegin.ToShortDateString();

      return osagoBeginDate;
    }
    
    private WordDocument openDocumentWord(string name)
    {
      var templateList = TemplateList.getInstance();
      var template = templateList.getItem(name);
      return new WordDocument(template.File);
    }
  }
}
