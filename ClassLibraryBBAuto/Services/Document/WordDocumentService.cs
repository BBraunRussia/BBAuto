using System;
using System.Linq;
using System.Windows.Forms;
using BBAuto.Domain.Common;
using BBAuto.Domain.Dictionary;
using BBAuto.Domain.Entities;
using BBAuto.Domain.ForCar;
using BBAuto.Domain.ForDriver;
using BBAuto.Domain.Lists;
using BBAuto.Domain.Static;

namespace BBAuto.Domain.Services.Document
{
  public class WordDocumentService : IWordDocumentService
  {
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

    public WordDoc CreateProxyOnSto(Car car, Invoice invoice)
    {
      WordDoc wordDoc = OpenDocumentWord("Доверенность на предоставление интересов на СТО");

      DriverCarList driverCarList = DriverCarList.getInstance();

      Driver driver = (invoice == null)
        ? driverCarList.GetDriver(car)
        : DriverList.getInstance().getItem(Convert.ToInt32(invoice.DriverToID));

      MyDateTime myDate = new MyDateTime(DateTime.Today.ToShortDateString());
      wordDoc.SetValue("текущая дата", myDate.ToLongString());

      String fio = String.Empty;
      if (driver != null)
        fio = driver.GetName(NameType.Full);

      wordDoc.SetValue("ФИО регионального представителя", fio);

      PassportList passportList = PassportList.getInstance();

      Passport passport = null;
      if (driver != null)
        passport = passportList.getLastPassport(driver);

      string passportToString = "нет данных";

      if (passport != null)
        passportToString = string.Concat(passport.Number, ", выдан ", passport.GiveDate.ToShortDateString(), ", ",
          passport.GiveOrg, ", Адрес: ", passport.Address);

      wordDoc.SetValue("паспорт регионального представителя", passportToString);

      string fullNameAuto = string.Concat(car.Mark.Name, " ", car.info.Model);
      wordDoc.SetValue("Название марки автомобиля", fullNameAuto);
      wordDoc.SetValue("VIN-автомобиля", car.vin);
      wordDoc.SetValue("Модель и марка двигателя автомобиля", car.eNumber);
      wordDoc.SetValue("Номер кузова автомобиля", car.bodyNumber);
      wordDoc.SetValue("Год выпуска автомобиля", car.Year);
      wordDoc.SetValue("Цвет автомобиля", car.info.Color);

      PTSList ptsList = PTSList.getInstance();
      PTS pts = ptsList.getItem(car);

      string ptsName = string.Concat(pts.Number, ", выдан ", pts.Date.ToShortDateString(), " ", pts.GiveOrg);

      wordDoc.SetValue("ПТС автомобиля", ptsName);
      wordDoc.SetValue("ГРЗ автомобиля", car.Grz);
      wordDoc.SetValue("текущий год", DateTime.Today.Year.ToString());

      return wordDoc;
    }

    public WordDoc CreateActFuelCard(Car car, Invoice invoice)
    {
      WordDoc wordDoc = OpenDocumentWord("Акт передачи топливной карты");

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
        wordDoc.Dispose();
        return null;
      }

      var regions = Regions.getInstance();
      var regionName = regions.getItem(regionId);

      var i = 1;

      foreach (var fuelCardDriver in driverCards)
      {
        wordDoc.AddRowInTable(1, i.ToString(), driverTo.GetName(NameType.Full), regionName,
          fuelCardDriver.FuelCard.Number);
        wordDoc.AddRowInTable(2, i.ToString(), driverTo.GetName(NameType.Full), regionName,
          fuelCardDriver.FuelCard.Number, fuelCardDriver.FuelCard.Pin);

        i++;
      }

      switch (driverCards.Count)
      {
        case 1:
          wordDoc.SetValue("Количество карт", "1 (одна) карта.");
          break;
        case 2:
          wordDoc.SetValue("Количество карт", "2 (две) карты.");
          break;
        default:
          if (driverCards.Count != 0)
            wordDoc.SetValue("Количество карт", driverCards.Count + "карт(ы).");
          break;
      }

      return wordDoc;
    }

    private static WordDoc OpenDocumentWord(string name)
    {
      var template = TemplateList.getInstance().getItem(name);
      return new WordDoc(template.File);
    }
  }
}
