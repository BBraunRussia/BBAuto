using BBAuto.Domain.Dictionary;
using BBAuto.Domain.Entities;
using BBAuto.Domain.ForCar;
using BBAuto.Domain.ForDriver;
using BBAuto.Domain.Lists;
using BBAuto.Domain.Static;
using BBAuto.Domain.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BBAuto.Domain.Common
{
  public class CreateDocument
  {
    private Invoice _invoice;
    private Car _car;
    private ExcelDoc _excelDoc;

    private DriverList driverList;

    public CreateDocument()
    {
      Init();
    }

    public CreateDocument(Car car, Invoice invoice = null)
    {
      Init();
      _car = car;
      _invoice = invoice;
    }

    private void Init()
    {
      driverList = DriverList.getInstance();
    }

    public void CreateExcelFromDGV(DataGridView dgv)
    {
      int minRowIndex = 0;
      int maxRowIndex = 0;
      int minColumnIndex = 0;
      int maxColumnIndex = 0;

      foreach (DataGridViewCell cell in dgv.SelectedCells)
      {
        if (cell.RowIndex > maxRowIndex)
          maxRowIndex = cell.RowIndex;
        if ((cell.RowIndex < minRowIndex) || (minRowIndex == 0))
          minRowIndex = cell.RowIndex;

        if (cell.ColumnIndex > maxColumnIndex)
          maxColumnIndex = cell.ColumnIndex;
        if ((cell.ColumnIndex < minColumnIndex) || (minColumnIndex == 0))
          minColumnIndex = cell.ColumnIndex;
      }

      int rowCount = maxRowIndex + 1;
      int columnCount = maxColumnIndex + 1;

      CreateExcelFromDGV(dgv, minRowIndex, rowCount, minColumnIndex, columnCount);
    }

    public void CreateExcelFromAllDGV(DataGridView dgv)
    {
      CreateExcelFromDGV(dgv, 0, dgv.Rows.Count, 0, dgv.Columns.Count);
    }

    private void CreateExcelFromDGV(DataGridView dgv, int minRow, int rowCount, int minColumn, int columnCount)
    {
      _excelDoc = new ExcelDoc();
      WriteHeader(dgv, minColumn, columnCount);

      int diffRow = GetDiffRows(minRow);
      int diffColumn = GetDiffColumns(minColumn);

      int index = 2;

      for (int i = minRow; i < rowCount; i++)
      {
        bool IsAdded = false;
        for (int j = minColumn; j < columnCount; j++)
        {
          if (dgv.Rows[i].Cells[j].Visible)
          {
            _excelDoc.setValue(index, dgv.Rows[i].Cells[j].ColumnIndex - diffColumn,
              dgv.Rows[i].Cells[j].Value.ToString());
            IsAdded = true;
          }

        }
        if (IsAdded)
          index++;
      }
    }

    private void WriteHeader(DataGridView dgv, int minColumn, int columnCount)
    {
      int diffColumn = GetDiffColumns(minColumn);

      int index = 1;

      for (int j = minColumn; j < columnCount; j++)
      {
        if (dgv.Columns[j].Visible)
        {
          _excelDoc.setValue(1, index, dgv.Columns[j].HeaderText);
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

    public void ShowInvoice()
    {
      _excelDoc = openDocumentExcel("Накладная");

      _excelDoc.setValue(7, 2, _car.info.Owner);

      _excelDoc.setValue(16, 82, _invoice.Number);
      _excelDoc.setValue(16, 98, _invoice.Date.ToShortDateString());

      string fullNameAuto = string.Concat("Автомобиль ", _car.Mark.Name, " ", _car.info.Model, ", ", _car.Grz);

      _excelDoc.setValue(22, 10, fullNameAuto);
      _excelDoc.setValue(22, 53, _car.dateGet.ToShortDateString());

      GradeList grades = GradeList.getInstance();

      Grade grade = grades.getItem(Convert.ToInt32(_car.GradeID));

      PTSList ptsList = PTSList.getInstance();
      PTS pts = ptsList.getItem(_car);

      string fullDetailAuto = string.Concat("VIN ", _car.vin, ", Двигатель ", _car.eNumber, ", № кузова ",
        _car.bodyNumber, ", Год выпуска ", _car.Year, " г., Паспорт ",
        pts.Number, " от ", pts.Date.ToShortDateString(), ", мощность двигателя ", grade.EPower, " л.с.");

      _excelDoc.setValue(47, 2, fullDetailAuto);

      Driver driver1 = driverList.getItem(Convert.ToInt32(_invoice.DriverFromID));
      Driver driver2 = driverList.getItem(Convert.ToInt32(_invoice.DriverToID));

      _excelDoc.setValue(9, 10, driver1.Dept);
      _excelDoc.setValue(56, 11, driver1.Position);
      _excelDoc.setValue(56, 63, driver1.GetName(NameType.Full));

      _excelDoc.setValue(11, 13, driver2.Dept);
      _excelDoc.setValue(60, 11, driver2.Position);
      _excelDoc.setValue(60, 63, driver2.GetName(NameType.Full));

      _excelDoc.Show();
    }

    public void showNotice(DTP dtp)
    {
      _excelDoc = openDocumentExcel("Извещение о страховом случае");

      Owners owners = Owners.getInstance();

      _excelDoc.setValue(6, 5, owners.getItem(Convert.ToInt32(_car.ownerID))); //страхователь
      _excelDoc.setValue(7, 6, "а/я 34, 196128"); //почтовый адрес
      _excelDoc.setValue(8, 7, "320-40-04"); //телефон

      DriverCarList driverCarList = DriverCarList.getInstance();
      Driver driver = driverCarList.GetDriver(_car, dtp.Date);

      PassportList passportList = PassportList.getInstance();
      Passport passport = passportList.getLastPassport(driver);

      if (passport.Number != string.Empty)
      {
        string number = passport.Number;
        string[] numbers = number.Split(' ');

        _excelDoc.setValue(10, 3, numbers[0]); //серия
        _excelDoc.setValue(10, 6, numbers[1]); //номер

        _excelDoc.setValue(11, 3, passport.GiveOrg); //кем выдан
        _excelDoc.setValue(12, 4, passport.GiveDate.ToShortDateString()); //дата выдачи
      }

      PolicyList policyList = PolicyList.getInstance();
      Policy policy = policyList.getItem(_car, PolicyType.КАСКО);
      _excelDoc.setValue(14, 6, policy.Number); //полис

      _excelDoc.setValue(16, 6, string.Concat(_car.Mark.Name, " ", _car.info.Model)); //марка а/м
      _excelDoc.setValue(18, 6, _car.Grz); //рег номер а/м
      _excelDoc.setValue(20, 6, _car.vin); //вин

      _excelDoc.setValue(22, 6, dtp.Date.ToShortDateString()); //дата дтп

      _excelDoc.setValue(27, 2, driver.GetName(NameType.Full)); //водитель фио

      Regions regions = Regions.getInstance();

      _excelDoc.setValue(29, 3, regions.getItem(Convert.ToInt32(dtp.IDRegion))); //город
      _excelDoc.setValue(31, 14, dtp.Damage); //повреждения
      _excelDoc.setValue(33, 2, dtp.Facts); //обстоятельства происшествия

      //SsDTP ssDTP = SsDTPList.getInstance().getItem(_car.Mark);

      //_excelDoc.setValue(63, 11, ssDTP.ServiceStantion);

      //DateTime date = DateTime.Today;
      //MyDateTime myDate = new MyDateTime(date.ToShortDateString());

      //_excelDoc.setValue(71, 3, string.Concat("« ", date.Day.ToString(), " »"));
      //_excelDoc.setValue(71, 4, myDate.MonthToStringGenitive());
      //_excelDoc.setValue(71, 8, date.Year.ToString().Substring(2, 2));

      _excelDoc.Show();
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

    public void createWaybill(DateTime date, Driver driver = null)
    {
      date = new DateTime(date.Year, date.Month, 1);

      if (driver == null)
      {
        DriverCarList driverCarList = DriverCarList.getInstance();
        driver = driverCarList.GetDriver(_car, date);

        if (driver == null)
        {
          driver = driverCarList.GetDriver(_car);
          InvoiceList invoiceList = InvoiceList.getInstance();
          Invoice invoice = invoiceList.getItem(_car);

          if ((invoice != null) && (!string.IsNullOrEmpty(invoice.DateMove)))
          {
            DateTime dateMove;
            DateTime.TryParse(invoice.DateMove, out dateMove);
            if ((dateMove.Year == date.Year) && (dateMove.Month == date.Month))
              date = new DateTime(date.Year, date.Month, dateMove.Day);
          }
        }
      }

      _excelDoc = openDocumentExcel("Путевой лист");

      _excelDoc.setValue(4, 28, _car.BBNumber.ToString());

      MyDateTime myDate = new MyDateTime(date.ToShortDateString());

      _excelDoc.setValue(4, 39, driver.ID.ToString() + "/01/" + myDate.MonthSlashYear());
      _excelDoc.setValue(6, 15, myDate.DaysRange);
      _excelDoc.setValue(6, 19, myDate.MonthToStringNominative());
      _excelDoc.setValue(6, 32, date.Year.ToString());

      _excelDoc.setValue(29, 35, _car.info.Grade.EngineType.ShortName);

      MileageMonthList mml = new MileageMonthList(_car.ID, date.Year + "-" + date.Month + "-01");
      /* Из файла Татьяны Мироновой пробег за месяц */
      _excelDoc.setValue(19, 39, mml.PSN);
      _excelDoc.setValue(33, 41, mml.Gas);
      _excelDoc.setValue(35, 41, mml.GasBegin);
      _excelDoc.setValue(36, 41, mml.GasEnd);
      _excelDoc.setValue(37, 41, mml.GasNorm);
      _excelDoc.setValue(38, 41, mml.GasNorm);
      _excelDoc.setValue(43, 39, mml.PSK);
      _excelDoc.setValue(41, 59, mml.Mileage);

      Owners owners = Owners.getInstance();
      string owner = owners.getItem(1);

      _excelDoc.setValue(8, 8, owner);

      _excelDoc.setValue(10, 11, string.Concat(_car.Mark.Name, " ", _car.info.Model));
      _excelDoc.setValue(11, 17, _car.Grz);

      _excelDoc.setValue(12, 6, driver.GetName(NameType.Full));
      _excelDoc.setValue(44, 16, driver.GetName(NameType.Short));
      _excelDoc.setValue(26, 40, driver.GetName(NameType.Short));

      LicenseList licencesList = LicenseList.getInstance();
      DriverLicense driverLicense = licencesList.getItem(driver);

      _excelDoc.setValue(14, 10, driverLicense.Number);

      _excelDoc.setValue(20, 9, owner);

      string suppyAddressName;

      if (driver.suppyAddress != string.Empty)
      {
        suppyAddressName = driver.suppyAddress;
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

      _excelDoc.setValue(25, 8, suppyAddressName);
      _excelDoc.setValue(26, 1, suppyAddressName2);

      string mechanicName;

      EmployeesList employeesList = EmployeesList.getInstance();
      Employees accountant = employeesList.getItem(driver.Region, "Бухгалтер Б.Браун");

      if (driver.IsOne)
      {
        mechanicName = driver.GetName(NameType.Short);
      }
      else
      {
        Employees mechanic = employeesList.getItem(driver.Region, "Механик", true);
        if (mechanic == null)
          mechanicName = driver.GetName(NameType.Short);
        else
          mechanicName = mechanic.Name;
      }

      Employees dispatcher = employeesList.getItem(driver.Region, "Диспечер-нарядчик");
      string dispatcherName = dispatcher.Name;

      _excelDoc.setValue(22, 40, mechanicName);
      _excelDoc.setValue(44, 40, mechanicName);

      _excelDoc.setValue(31, 18, dispatcherName);
      _excelDoc.setValue(35, 18, dispatcherName);

      _excelDoc.setValue(43, 72, accountant.Name);
    }

    public void AddRouteInWayBill(DateTime date, Fields fields)
    {
      WayBillDaily wayBillDaily = new WayBillDaily(_car, date);
      wayBillDaily.Load();

      CopyWayBill(wayBillDaily);

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

          _excelDoc.setValue(i, 59, pointBegin.Name);
          _excelDoc.setValue(i, 64, pointEnd.Name);
          _excelDoc.setValue(i, 78, route.Distance.ToString());
          i += 2;
        }

        _excelDoc.setValue(29 + (47 * k), 20, wayBillDay.Date.ToShortDateString());
        _excelDoc.setValue(19 + (47 * k), 39, curDistance.ToString());
        curDistance += wayBillDay.Distance;
        if (fields == Fields.All)
        {
          _excelDoc.setValue(43 + (47 * k), 39, curDistance.ToString());
          _excelDoc.setValue(41 + (47 * k), 59, wayBillDay.Distance.ToString());
          _excelDoc.setValue(33 + (47 * k), 20, wayBillDay.Date.ToShortDateString());
        }
        k++;
      }

      if ((k > 0) && (fields == Fields.All))
        _excelDoc.setValue(43 + (47 * (k - 1)), 39, endDistance.ToString());
    }

    private void CopyWayBill(WayBillDaily wayBillDaily)
    {
      int i = 0;
      foreach (WayBillDay item in wayBillDaily)
      {
        if (i > 0)
          _excelDoc.CopyRange("A1", "CF46", "A" + ((47 * i) + 1).ToString());

        _excelDoc.setValue(6 + (47 * i), 15, item.Day);

        _excelDoc.setValue(4 + (47 * i), 39, GetWaBillFullNumber(i + 1));

        _excelDoc.setValue(12 + (47 * i), 6, item.Driver.GetName(NameType.Full));
        _excelDoc.setValue(44 + (47 * i), 16, item.Driver.GetName(NameType.Short));
        _excelDoc.setValue(26 + (47 * i), 40, item.Driver.GetName(NameType.Short));

        i++;
      }
    }

    private string GetWaBillFullNumber(int currentNumber)
    {
      string[] wayBillFullNumber = _excelDoc.getValue("AM4").ToString().Split('/');

      wayBillFullNumber[1] = (currentNumber < 10) ? "0" : string.Empty;
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

    public void ShowAttacheToOrder()
    {
      _excelDoc = openDocumentExcel("Приложение к приказу");

      string fullNameAuto = string.Concat(_car.Mark.Name, " ", _car.info.Model);

      _excelDoc.setValue(18, 2, fullNameAuto);
      _excelDoc.setValue(18, 3, _car.Grz);

      Driver driver = driverList.getItem(Convert.ToInt32(_invoice.DriverToID));

      _excelDoc.setValue(18, 4, driver.GetName(NameType.Full));
      _excelDoc.setValue(18, 5, driver.Position);

      _excelDoc.Show();
    }

    public void ShowProxyOnSTO()
    {
      WordDoc wordDoc = CreateProxyOnSTO();

      wordDoc.Show();
    }

    public void PrintProxyOnSTO()
    {
      WordDoc wordDoc = CreateProxyOnSTO();

      wordDoc.setValue("до 31 декабря 2017 года", "до 31 декабря 2018 года");

      MyDateTime myDate = new MyDateTime(DateTime.Today.ToShortDateString());
      wordDoc.setValue(myDate.ToLongString(), "01 января 2018");

      //wordDoc.Show();
      wordDoc.Print();
    }

    private WordDoc CreateProxyOnSTO()
    {
      WordDoc wordDoc = openDocumentWord("Доверенность на предоставление интересов на СТО");

      DriverCarList driverCarList = DriverCarList.getInstance();

      Driver driver = (_invoice == null)
        ? driverCarList.GetDriver(_car)
        : driverList.getItem(Convert.ToInt32(_invoice.DriverToID));

      MyDateTime myDate = new MyDateTime(DateTime.Today.ToShortDateString());
      wordDoc.setValue("текущая дата", myDate.ToLongString());

      String fio = String.Empty;
      if (driver != null)
        fio = driver.GetName(NameType.Full);

      wordDoc.setValue("ФИО регионального представителя", fio);

      PassportList passportList = PassportList.getInstance();

      Passport passport = null;
      if (driver != null)
        passport = passportList.getLastPassport(driver);

      string passportToString = "нет данных";

      if (passport != null)
        passportToString = string.Concat(passport.Number, ", выдан ", passport.GiveDate.ToShortDateString(), ", ",
          passport.GiveOrg, ", Адрес: ", passport.Address);

      wordDoc.setValue("паспорт регионального представителя", passportToString);

      string fullNameAuto = string.Concat(_car.Mark.Name, " ", _car.info.Model);
      wordDoc.setValue("Название марки автомобиля", fullNameAuto);
      wordDoc.setValue("VIN-автомобиля", _car.vin);
      wordDoc.setValue("Модель и марка двигателя автомобиля", _car.eNumber);
      wordDoc.setValue("Номер кузова автомобиля", _car.bodyNumber);
      wordDoc.setValue("Год выпуска автомобиля", _car.Year);
      wordDoc.setValue("Цвет автомобиля", _car.info.Color);

      PTSList ptsList = PTSList.getInstance();
      PTS pts = ptsList.getItem(_car);

      string ptsName = string.Concat(pts.Number, ", выдан ", pts.Date.ToShortDateString(), " ", pts.GiveOrg);

      wordDoc.setValue("ПТС автомобиля", ptsName);
      wordDoc.setValue("ГРЗ автомобиля", _car.Grz);
      wordDoc.setValue("текущий год", DateTime.Today.Year.ToString());

      return wordDoc;
    }

    public void ShowActFuelCard()
    {
      WordDoc wordDoc = openDocumentWord("Акт передачи топливной карты");

      FuelCardDriverList fuelCardDriverList = FuelCardDriverList.getInstance();

      var driverId = 0;
      var regionId = 0;
      if (_invoice == null)
      {
        var driver = DriverCarList.getInstance().GetDriver(_car);
        driverId = driver.ID;
        regionId = driver.Region.ID;
      }
      else
      {
        driverId = Convert.ToInt32(_invoice.DriverToID);
        regionId = Convert.ToInt32(_invoice.RegionToID);
      }

      Driver driverTo = driverList.getItem(driverId);
      var list = fuelCardDriverList.ToList(driverTo).Where(driverCard => !driverCard.FuelCard.IsLost).ToList();

      Regions regions = Regions.getInstance();
      string regionName = regions.getItem(regionId);

      int i = 1;

      foreach (FuelCardDriver fuelCardDriver in list)
      {
        wordDoc.AddRowInTable(1, i.ToString(), driverTo.GetName(NameType.Full), regionName,
          fuelCardDriver.FuelCard.Number);
        wordDoc.AddRowInTable(2, i.ToString(), driverTo.GetName(NameType.Full), regionName,
          fuelCardDriver.FuelCard.Number, fuelCardDriver.FuelCard.Pin);

        i++;
      }

      switch (list.Count)
      {
        case 1:
          wordDoc.setValue("Количество карт", "1 (одна) карта.");
          break;
        case 2:
          wordDoc.setValue("Количество карт", "2 (две) карты.");
          break;
        default:
          if (list.Count != 0)
            wordDoc.setValue("Количество карт", list.Count + "карт(ы).");
          break;
      }

      wordDoc.Show();
    }

    public void CreatePolicyTable()
    {
      var date = DateTime.Today.AddMonths(1);

      _excelDoc = openDocumentExcel("Таблица страхования");

      MyDateTime myDate = new MyDateTime(date.ToShortDateString());

      _excelDoc.setValue(2, 1, "Страхуем в " + myDate.MonthToStringPrepositive() + " " + myDate.Year + " г.");

      PolicyList policyList = PolicyList.getInstance();
      List<Policy> list = policyList.GetPolicyList(date);
      List<Car> listCar = policyList.GetCarListByPolicyList(list);

      DiagCardList diagCardList = DiagCardList.getInstance();

      int rowIndex = 6;

      foreach (Car car in listCar)
      {
        _excelDoc.setValue(rowIndex, 2, car.Grz);
        _excelDoc.setValue(rowIndex, 3, car.Mark.Name);
        _excelDoc.setValue(rowIndex, 4, car.info.Model);
        _excelDoc.setValue(rowIndex, 5, car.vin);
        _excelDoc.setValue(rowIndex, 6, car.Year);
        _excelDoc.setValue(rowIndex, 7, GetPolicyBeginDate(list, car, PolicyType.ОСАГО));
        _excelDoc.setValue(rowIndex, 8, GetPolicyBeginDate(list, car, PolicyType.КАСКО));
        _excelDoc.setValue(rowIndex, 9, car.info.Owner);
        _excelDoc.setValue(rowIndex, 10, car.info.Owner);
        _excelDoc.setValue(rowIndex, 11, car.info.Owner);

        DiagCard diagCard = diagCardList.getItem(car);

        if (diagCard != null)
        {
          _excelDoc.setValue(rowIndex, 12, diagCard.Date.ToShortDateString());
          _excelDoc.setValue(rowIndex, 13, diagCard.Number);
        }

        rowIndex++;
      }

      _excelDoc.Show();
    }

    private static string GetPolicyBeginDate(List<Policy> list, Car car, PolicyType policyType)
    {
      List<Policy> newList = list.Where(policy => policy.Car.ID == car.ID && policy.Type == policyType).ToList();

      string osagoBeginDate = "не надо";

      if (newList.Count > 0)
        osagoBeginDate = newList.First().DateBegin.ToShortDateString();

      return osagoBeginDate;
    }

    private ExcelDoc openDocumentExcel(string name)
    {
      TemplateList templateList = TemplateList.getInstance();
      Template template = templateList.getItem(name);
      return new ExcelDoc(template.File);
    }

    private WordDoc openDocumentWord(string name)
    {
      TemplateList templateList = TemplateList.getInstance();
      Template template = templateList.getItem(name);
      return new WordDoc(template.File);
    }

    public void Print()
    {
      _excelDoc.Print();
    }

    public void Show()
    {
      _excelDoc.Show();
    }

    public void CreateHeader(string text)
    {
      _excelDoc.SetHeader(text);
    }

    public void Exit()
    {
      _excelDoc.Dispose();
    }
  }
}
