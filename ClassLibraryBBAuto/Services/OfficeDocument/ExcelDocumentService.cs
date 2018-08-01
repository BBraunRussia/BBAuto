using System;
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
using BBAuto.Domain.Static;
using BBAuto.Domain.Tables;

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
      excelDoc.setValue(56, 63, driver1.GetName(NameType.Full));

      excelDoc.setValue(11, 13, driver2.Dept);
      excelDoc.setValue(60, 11, driver2.Position);
      excelDoc.setValue(60, 63, driver2.GetName(NameType.Full));

      return excelDoc;
    }

    public IDocument CreateNotice(Car car, DTP dtp)
    {
      var excelDoc = OpenDocumentExcel("Извещение о страховом случае");

      Owners owners = Owners.getInstance();

      excelDoc.setValue(6, 5, owners.getItem(Convert.ToInt32(car.ownerID))); //страхователь
      excelDoc.setValue(7, 6, "а/я 34, 196128"); //почтовый адрес
      excelDoc.setValue(8, 7, "320-40-04"); //телефон

      DriverCarList driverCarList = DriverCarList.getInstance();
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

      excelDoc.setValue(27, 2, driver.GetName(NameType.Full)); //водитель фио

      Regions regions = Regions.getInstance();

      excelDoc.setValue(29, 3, regions.getItem(Convert.ToInt32(dtp.IDRegion))); //город
      excelDoc.setValue(31, 14, dtp.Damage); //повреждения
      excelDoc.setValue(33, 2, dtp.Facts); //обстоятельства происшествия

      //SsDTP ssDTP = SsDTPList.getInstance().getItem(_car.Mark);

      //_excelDoc.setValue(63, 11, ssDTP.ServiceStantion);

      //DateTime date = DateTime.Today;
      //MyDateTime myDate = new MyDateTime(date.ToShortDateString());

      //_excelDoc.setValue(71, 3, string.Concat("« ", date.Day.ToString(), " »"));
      //_excelDoc.setValue(71, 4, myDate.MonthToStringGenitive());
      //_excelDoc.setValue(71, 8, date.Year.ToString().Substring(2, 2));

      return excelDoc;
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

    public IExcelDoc CreateWaybill(Car car, DateTime date, Driver driver = null)
    {
      date = new DateTime(date.Year, date.Month, 1);

      if (driver == null)
      {
        DriverCarList driverCarList = DriverCarList.getInstance();
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

      excelDoc.setValue(4, 28, car.BBNumber);

      MyDateTime myDate = new MyDateTime(date.ToShortDateString());

      excelDoc.setValue(4, 39, driver.ID + "/01/" + myDate.MonthSlashYear());
      excelDoc.setValue(6, 15, myDate.DaysRange);
      excelDoc.setValue(6, 19, myDate.MonthToStringNominative());
      excelDoc.setValue(6, 32, date.Year.ToString());

      excelDoc.setValue(29, 35, car.info.Grade.EngineType.ShortName);

      MileageMonthList mml = new MileageMonthList(car.ID, date.Year + "-" + date.Month + "-01");
      /* Из файла Татьяны Мироновой пробег за месяц */
      excelDoc.setValue(19, 39, mml.PSN);
      excelDoc.setValue(33, 41, mml.Gas);
      excelDoc.setValue(35, 41, mml.GasBegin);
      excelDoc.setValue(36, 41, mml.GasEnd);
      excelDoc.setValue(37, 41, mml.GasNorm);
      excelDoc.setValue(38, 41, mml.GasNorm);
      excelDoc.setValue(43, 39, mml.PSK);
      excelDoc.setValue(41, 59, mml.Mileage);

      Owners owners = Owners.getInstance();
      string owner = owners.getItem(1);

      excelDoc.setValue(8, 8, owner);

      excelDoc.setValue(10, 11, string.Concat(car.Mark.Name, " ", car.info.Model));
      excelDoc.setValue(11, 17, car.Grz);

      excelDoc.setValue(12, 6, driver.GetName(NameType.Full));
      excelDoc.setValue(44, 16, driver.GetName(NameType.Short));
      excelDoc.setValue(26, 40, driver.GetName(NameType.Short));

      LicenseList licencesList = LicenseList.getInstance();
      DriverLicense driverLicense = licencesList.getItem(driver);

      excelDoc.setValue(14, 10, driverLicense.Number);

      excelDoc.setValue(20, 9, owner);

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

      excelDoc.setValue(25, 8, suppyAddressName);
      excelDoc.setValue(26, 1, suppyAddressName2);

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

      excelDoc.setValue(22, 40, mechanicName);
      excelDoc.setValue(44, 40, mechanicName);

      excelDoc.setValue(31, 18, dispatcherName);
      excelDoc.setValue(35, 18, dispatcherName);

      excelDoc.setValue(43, 72, accountant.Name);

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

        excelDoc.setValue(12 + (47 * i), 6, item.Driver.GetName(NameType.Full));
        excelDoc.setValue(44 + (47 * i), 16, item.Driver.GetName(NameType.Short));
        excelDoc.setValue(26 + (47 * i), 40, item.Driver.GetName(NameType.Short));

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

      excelDoc.setValue(18, 4, driver.GetName(NameType.Full));
      excelDoc.setValue(18, 5, driver.Position);

      return excelDoc;
    }
    
    public IDocument CreatePolicyTable()
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

      foreach (var car in listCar)
      {
        var policyOsago = list.FirstOrDefault(policy => policy.Car.ID == car.ID && policy.Type == PolicyType.ОСАГО);
        var policyKasko = list.FirstOrDefault(policy => policy.Car.ID == car.ID && policy.Type == PolicyType.КАСКО);

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
