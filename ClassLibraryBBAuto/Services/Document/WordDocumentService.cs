using System;
using System.Linq;
using System.Windows.Forms;
using BBAuto.Domain.Common;
using BBAuto.Domain.Dictionary;
using BBAuto.Domain.Entities;
using BBAuto.Domain.ForCar;
using BBAuto.Domain.ForDriver;
using BBAuto.Domain.Lists;
using BBAuto.Domain.Services.CarSale;
using BBAuto.Domain.Services.Customer;
using BBAuto.Domain.Static;

namespace BBAuto.Domain.Services.Document
{
  public class WordDocumentService : IWordDocumentService
  {
    private const string ProxyOnSto = "Доверенность на предоставление интересов на СТО";
    private const string ActFuelCard = "Акт передачи топливной карты";
    private const string ContractOfSale = "Договор купли-продажи ТС";
    private const string TransferAct = "Акт приема-передачи ТС";

    /*
    public void ShowProxyOnSTO(Car car, Invoice invoice)
    {
      WordDoc wordDoc = CreateProxyOnSTO();

      wordDoc.Show();
    }*/
    /*
    public void PrintProxyOnSTO(Car car, Invoice invoice)
    {
      WordDoc wordDoc = CreateProxyOnSTO();

      wordDoc.SetValue("до 31 декабря 2017 года", "до 31 декабря 2018 года");

      MyDateTime myDate = new MyDateTime(DateTime.Today.ToShortDateString());
      wordDoc.SetValue(myDate.ToLongString(), "01 января 2018");

      //wordDoc.Show();
      wordDoc.Print();
    }*/

    public IDocument CreateProxyOnSto(Car car, Invoice invoice)
    {
      var doc = OpenDocumentWord(ProxyOnSto);
      if (doc == null)
        return null;

      var driverCarList = DriverCarList.getInstance();

      var driver = invoice == null
        ? driverCarList.GetDriver(car)
        : DriverList.getInstance().getItem(Convert.ToInt32(invoice.DriverToID));

      var myDate = new MyDateTime(DateTime.Today.ToShortDateString());
      doc.SetValue("текущая дата", myDate.ToLongString());

      var fio = string.Empty;
      if (driver != null)
        fio = driver.GetName(NameType.Full);

      doc.SetValue("ФИО регионального представителя", fio);

      PassportList passportList = PassportList.getInstance();

      Passport passport = null;
      if (driver != null)
        passport = passportList.getLastPassport(driver);

      string passportToString = "нет данных";

      if (passport != null)
        passportToString = string.Concat(passport.Number, ", выдан ", passport.GiveDate.ToShortDateString(), ", ",
          passport.GiveOrg, ", Адрес: ", passport.Address);

      doc.SetValue("паспорт регионального представителя", passportToString);

      string fullNameAuto = string.Concat(car.Mark.Name, " ", car.info.Model);
      doc.SetValue("Название марки автомобиля", fullNameAuto);
      doc.SetValue("VIN-автомобиля", car.vin);
      doc.SetValue("Модель и марка двигателя автомобиля", car.eNumber);
      doc.SetValue("Номер кузова автомобиля", car.bodyNumber);
      doc.SetValue("Год выпуска автомобиля", car.Year);
      doc.SetValue("Цвет автомобиля", car.info.Color);

      PTSList ptsList = PTSList.getInstance();
      PTS pts = ptsList.getItem(car);

      string ptsName = string.Concat(pts.Number, ", выдан ", pts.Date.ToShortDateString(), " ", pts.GiveOrg);

      doc.SetValue("ПТС автомобиля", ptsName);
      doc.SetValue("ГРЗ автомобиля", car.Grz);
      doc.SetValue("текущий год", DateTime.Today.Year.ToString());

      return doc;
    }

    public IDocument CreateActFuelCard(Car car, Invoice invoice)
    {
      var doc = OpenDocumentWord(ActFuelCard);
      if (doc == null)
        return null;

      FuelCardDriverList fuelCardDriverList = FuelCardDriverList.getInstance();

      int driverId;
      int regionId;
      if (invoice == null)
      {
        var driver = DriverCarList.getInstance().GetDriver(car);
        driverId = driver.ID;
        regionId = driver.Region.ID;
      }
      else
      {
        driverId = Convert.ToInt32(invoice.DriverToID);
        regionId = Convert.ToInt32(invoice.RegionToID);
      }

      var driverTo = DriverList.getInstance().getItem(driverId);
      var driverCards = fuelCardDriverList.ToList(driverTo).Where(driverCard => !driverCard.FuelCard.IsLost).ToList();

      if (!driverCards.Any())
      {
        MessageBox.Show("Формирование акта невозможно, за водителем не закреплены топливные карты", "Информация",
          MessageBoxButtons.OK, MessageBoxIcon.Information);
        doc.Dispose();
        return null;
      }

      var regions = Regions.getInstance();
      var regionName = regions.getItem(regionId);

      var i = 1;

      foreach (var fuelCardDriver in driverCards)
      {
        doc.AddRowInTable(1, i.ToString(), driverTo.GetName(NameType.Full), regionName,
          fuelCardDriver.FuelCard.Number);
        doc.AddRowInTable(2, i.ToString(), driverTo.GetName(NameType.Full), regionName,
          fuelCardDriver.FuelCard.Number, fuelCardDriver.FuelCard.Pin);

        i++;
      }

      switch (driverCards.Count)
      {
        case 1:
          doc.SetValue("Количество карт", "1 (одна) карта.");
          break;
        case 2:
          doc.SetValue("Количество карт", "2 (две) карты.");
          break;
        default:
          if (driverCards.Count != 0)
            doc.SetValue("Количество карт", driverCards.Count + "карт(ы).");
          break;
      }

      return doc;
    }

    public IDocument CreateTransferAct(Car car)
    {
      return CreateDocumentForSale(ContractOfSale, car);
    }

    public IDocument CreateContractOfSale(Car car)
    {
      return CreateDocumentForSale(TransferAct, car);
    }

    private static IDocument CreateDocumentForSale(string templateName, Car car)
    {
      var doc = OpenDocumentWord(templateName);
      if (doc == null)
        return null;

      ICustomerService customerService = new CustomerService();
      var customer = customerService.GetCustomerByCarId(car.ID);

      if (customer == null)
      {
        MessageBox.Show("Покупатель не назначен для данного автомобиля", "Ошибка", MessageBoxButtons.OK,
          MessageBoxIcon.Error);
        doc.Dispose();
        return null;
      }

      ICarSaleService carSaleService = new CarSaleService();
      var carSale = carSaleService.GetCarSaleByCarId(car.ID);

      if (!carSale.Date.HasValue)
      {
        MessageBox.Show("Дата продажи не установлена для данного автомобиля", "Ошибка", MessageBoxButtons.OK,
          MessageBoxIcon.Error);
        doc.Dispose();
        return null;
      }

      var myDate = new MyDateTime(carSale.Date.Value.ToShortDateString());
      doc.SetValue("дата продажи", myDate.ToLongString());

      doc.SetValue("ФИО покупателя", customer.FullName);
      doc.SetValue("Ф ИО покупателя", customer.ShortName);

      var passportToString = $"{customer.PassportNumber} выдан {customer.PassportGiveDate.ToShortDateString()} {customer.PassportGiveOrg} Адрес: {customer.Address}";

      doc.SetValue("паспорт покупателя", passportToString);
      doc.SetValue("Инн покупателя", customer.Inn);

      var fullNameAuto = $"{car.Mark.Name} {car.info.Model}";
      doc.SetValue("Название марки автомобиля", fullNameAuto);
      doc.SetValue("VIN-автомобиля", car.vin);
      doc.SetValue("Год выпуска автомобиля", car.Year);
      doc.SetValue("Модель и марка двигателя автомобиля", car.eNumber);
      doc.SetValue("Объем двигателя автомобиля", car.info.Grade.EVol);
      doc.SetValue("Цвет автомобиля", car.info.Color);
      doc.SetValue("Номер кузова автомобиля", car.bodyNumber);

      var ptsList = PTSList.getInstance();
      var pts = ptsList.getItem(car);
      var ptsName = $"{pts.Number} выдан {pts.Date.ToShortDateString()} {pts.GiveOrg}";
      doc.SetValue("ПТС автомобиля", ptsName);

      var stsList = STSList.getInstance();
      var sts = stsList.getItem(car);
      var stsName = $"{sts.Number} выдан {sts.Date.ToShortDateString()} {sts.GiveOrg}";
      doc.SetValue("СТС автомобиля", stsName);

      doc.SetValue("ГРЗ автомобиля", car.Grz);

      return doc;
    }

    private static WordDoc OpenDocumentWord(string name)
    {
      try
      {
        var template = TemplateList.getInstance().getItem(name);
        return new WordDoc(template.File);
      }
      catch (NullReferenceException)
      {
        MessageBox.Show($"Шаблон документа с именем {name} не найден", "Ошибка", MessageBoxButtons.OK,
          MessageBoxIcon.Error);
        return null;
      }
    }
  }
}
