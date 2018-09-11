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

namespace BBAuto.Domain.Services.OfficeDocument
{
  public class WordDocumentService : IWordDocumentService
  {
    private const string ProxyOnSto = "Доверенность на предоставление интересов на СТО";
    private const string ActFuelCard = "Акт передачи топливной карты";
    private const string ContractOfSale = "Договор купли-продажи ТС";
    private const string TransferCarAct = "Акт приема-передачи ТС";
    private const string TerminationOsago = "Заявление о расторжении ОСАГО";
    private const string TerminationKasko = "Заявление о расторжении КАСКО";
    private const string ExtraTerminationOsago = "Доп. cоглашение о расторжении ОСАГО";
    private const string ExtraTerminationKasko = "Доп. cоглашение о расторжении КАСКО";
    
    public IDocument CreateProxyOnSto(Car car, Invoice invoice)
    {
      var doc = OpenDocumentWord(ProxyOnSto);
      if (doc == null)
        return null;

      var driverCarList = DriverCarList.GetInstance();

      var driver = invoice == null
        ? driverCarList.GetDriver(car)
        : DriverList.getInstance().getItem(Convert.ToInt32(invoice.DriverToID));

      var myDate = new MyDateTime(DateTime.Today.ToShortDateString());
      doc.SetValue("текущая дата", myDate.ToLongString());

      var fio = string.Empty;
      if (driver != null)
        fio = driver.GetName(NameType.Full);

      doc.SetValue("ФИО регионального представителя", fio);

      var passportList = PassportList.getInstance();

      Passport passport = null;
      if (driver != null)
        passport = passportList.getLastPassport(driver);

      var passportToString = "нет данных";

      if (passport != null)
        passportToString = string.Concat(passport.Number, ", выдан ", passport.GiveDate.ToShortDateString(), ", ",
          passport.GiveOrg, ", Адрес: ", passport.Address);

      doc.SetValue("паспорт регионального представителя", passportToString);

      var fullNameAuto = string.Concat(car.Mark.Name, " ", car.info.Model);
      doc.SetValue("Название марки автомобиля", fullNameAuto);
      doc.SetValue("VIN-автомобиля", car.vin);
      doc.SetValue("Модель и марка двигателя автомобиля", car.eNumber);
      doc.SetValue("Номер кузова автомобиля", car.bodyNumber);
      doc.SetValue("Год выпуска автомобиля", car.Year);
      doc.SetValue("Цвет автомобиля", car.info.Color);

      var ptsList = PTSList.getInstance();
      var pts = ptsList.getItem(car);

      var ptsName = string.Concat(pts.Number, ", выдан ", pts.Date.ToShortDateString(), " ", pts.GiveOrg);

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

      var fuelCardDriverList = FuelCardDriverList.getInstance();

      int driverId;
      int regionId;
      if (invoice == null)
      {
        var driver = DriverCarList.GetInstance().GetDriver(car);
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

    public IDocument CreateContractOfSale(Car car)
    {
      return CreateDocumentForSale(ContractOfSale, car);
    }

    public IDocument CreateTransferCarAct(Car car)
    {
      return CreateDocumentForSale(TransferCarAct, car);
    }

    public IDocument CreateTermination(Policy policy)
    {
      if (policy.Type != PolicyType.ОСАГО && policy.Type != PolicyType.КАСКО)
        return null;

      var templateName = policy.Type == PolicyType.ОСАГО ? TerminationOsago : TerminationKasko;

      return FillTermination(policy, templateName);
    }

    public IDocument CreateExtraTermination(Policy policy)
    {
      if (policy.Type != PolicyType.ОСАГО && policy.Type != PolicyType.КАСКО)
        return null;

      var templateName = policy.Type == PolicyType.ОСАГО ? ExtraTerminationOsago : ExtraTerminationKasko;

      return FillTermination(policy, templateName);
    }

    private static IDocument FillTermination(Policy policy, string templateName)
    {
      var doc = OpenDocumentWord(templateName);
      if (doc == null)
        return null;

      var model = ModelList.getInstance().getItem(Convert.ToInt32(policy.Car.ModelID));

      doc.SetValue("Текущая дата", DateTime.Today.ToShortDateString());
      doc.SetValue("Название марки автомобиля", $"{policy.Car.Mark.Name} {model.Name}");
      doc.SetValue("ГРЗ автомобиля", policy.Car.Grz);
      doc.SetValue("VIN-автомобиля", policy.Car.vin);
      doc.SetValue("Номер полиса", policy.Number);
      doc.SetValue("Дата начала действия полиса", policy.DateCreate.ToShortDateString());

      return doc;
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
