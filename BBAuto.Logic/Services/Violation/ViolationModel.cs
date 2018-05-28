using System;
using BBAuto.Logic.Dictionary;
using BBAuto.Logic.Entities;
using BBAuto.Logic.Lists;
using BBAuto.Logic.Services.Car;
using BBAuto.Logic.Services.Driver;
using BBAuto.Logic.Static;

namespace BBAuto.Logic.Services.Violation
{
  public class ViolationModel
  {
    private ViolationModel() { }

    public ViolationModel(int carId)
    {
      CarId = carId;
    }

    public int Id { get; set; }
    public int CarId { get; private set; }
    public DateTime? Date { get; set; }
    public string Number { get; set; }
    public string File { get; set; }
    public DateTime? DatePay { get; set; }
    public string FilePay { get; set; }
    public int ViolationTypeId { get; set; }
    public int Sum { get; set; }
    public bool Sent { get; set; }
    public bool NoDeduction { get; set; }
    public bool Agreed { get; set; }
    public DateTime DateCreate { get; set; }

    public object[] GetRow(CarModel car)
    {
      var driver = GetDriver();

      var violationType = ViolationTypes.getInstance();

      var invoiceList = InvoiceList.getInstance();
      var invoice = invoiceList.GetItemByCarId(CarId);
      var regions = Regions.getInstance();
      var regionName = (invoice == null)
        ? regions.getItem(car.RegionIdUsing.Value)
        : regions.getItem(Convert.ToInt32(invoice.RegionToId));

      return new object[]
      {
        Id, CarId, car.BbNumberString, car.Grz, regionName, Date, driver.GetName(NameType.Full), Number, DatePay,
        violationType.getItem(ViolationTypeId), Sum
      };
    }

    public DriverModel GetDriver()
    {
      var driverCarList = DriverCarList.getInstance();
      var driver = driverCarList.GetDriver(CarId, Date);

      return driver ?? new DriverModel();
    }
  }
}
