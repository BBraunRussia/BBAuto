using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using AutoMapper;
using BBAuto.Logic.Dictionary;
using BBAuto.Logic.ForCar;
using BBAuto.Logic.Lists;
using BBAuto.Logic.Services.Car.Sale;
using BBAuto.Logic.Services.DiagCard;
using BBAuto.Logic.Services.Violation;
using BBAuto.Logic.Static;
using BBAuto.Repositories;
using BBAuto.Repositories.Entities;

namespace BBAuto.Logic.Services.Car
{
  public class CarService : ICarService
  {
    private readonly IDbContext _dbContext;
    private readonly ISaleCarService _carSaleService;
    private readonly IDiagCardService _diagCardService;

    public CarService(
      IDbContext dbContext,
      ISaleCarService carSaleService,
      IDiagCardService diagCardService)
    {
      _dbContext = dbContext;
      _carSaleService = carSaleService;
      _diagCardService = diagCardService;
    }

    public CarModel GetCarByGrz(string grz)
    {
      var list = _dbContext.Car.GetCars();

      return Mapper.Map<CarModel>(list.FirstOrDefault(c => c.Grz == grz));
    }
    
    public CarModel GetCarById(int id)
    {
      if (id == 0)
        return null;

      var dbModel = _dbContext.Car.GetCarById(id);

      return Mapper.Map<CarModel>(dbModel);
    }

    public CarModel Save(CarModel car)
    {
      var dbModel = Mapper.Map<DbCar>(car);
      var result = _dbContext.Car.UpsertCar(dbModel);

      return Mapper.Map<CarModel>(result);
    }

    public IList<CarModel> GetCars()
    {
      return Mapper.Map<IList<CarModel>>(_dbContext.Car.GetCars());
    }

    public DataTable ToDataTable(Status status)
    {
      switch (status)
      {
        case Status.Buy:
          return ToDataTableBuy();
        case Status.Actual:
          return ToDataTableActual();
        case Status.Repair:
          return RepairList.getInstance().ToDataTable();
        case Status.Sale:
          return CarSaleList.getInstance().ToDataTable();
        case Status.Invoice:
          return InvoiceList.getInstance().ToDataTable();
        case Status.Policy:
          return PolicyList.getInstance().ToDataTable();
        case Status.DTP:
          return DTPList.getInstance().ToDataTable();
     //   case Status.Violation:
      //    return ViolationList.getInstance().ToDataTable();
        case Status.DiagCard:
          return _diagCardService.GetDataTable(this);
        case Status.TempMove:
          return TempMoveList.getInstance().ToDataTable();
        case Status.ShipPart:
          return ShipPartList.getInstance().ToDataTable();
        case Status.Account:
          return AccountList.GetInstance().ToDataTable();
        case Status.AccountViolation:
          return ViolationList.getInstance().ToDataTableAccount();
        case Status.FuelCard:
          return FuelCardList.getInstance().ToDataTable();
        case Status.Driver:
          return DriverList.getInstance().ToDataTable();
        default:
          return ToDataTable();
      }
    }
    
    private DataTable ToDataTableBuy()
    {
      var cars = GetCars();

      var list = cars.Where(car => !car.IsGet).ToList();

      return CreateTable(list);
    }

    private DataTable ToDataTableActual()
    {
      var cars = GetCars();

      List<CarModel> list;

      if (User.GetRole() == RolesList.Employee)
      {
        var driverCarList = DriverCarList.getInstance();
        var myCar = driverCarList.GetCar(User.GetDriver());

        list = cars.Where(car => car.Id == myCar.Id).ToList();
      }
      else
      {
        var saleCars = _carSaleService.GetSaleCars();
        
        list = cars.Where(car => car.IsGet && saleCars.FirstOrDefault(carSale => carSale.Id == car.Id) == null).ToList();
      }

      return CreateTable(list);
    }

    private DataTable ToDataTable()
    {
      var cars = GetCars().ToList();

      return CreateTable(cars);
    }

    internal DataTable CreateTable(List<CarModel> cars)
    {
      DataTable dt = new DataTable();
      dt.Columns.Add("id");
      dt.Columns.Add("idCar");
      dt.Columns.Add("Бортовой номер");
      dt.Columns.Add("Регистрационный знак");
      dt.Columns.Add("Марка");
      dt.Columns.Add("Модель");
      dt.Columns.Add("VIN");
      dt.Columns.Add("Регион");
      dt.Columns.Add("Водитель");
      dt.Columns.Add("№ ПТС");
      dt.Columns.Add("№ СТС");
      dt.Columns.Add("Год выпуска");
      dt.Columns.Add("Пробег", typeof(int));
      dt.Columns.Add("Дата последней записи о пробеге", typeof(DateTime));
      dt.Columns.Add("Собственник");
      dt.Columns.Add("Дата окончания гарантии", typeof(DateTime));
      dt.Columns.Add("Статус");

      foreach (var car in cars)
        dt.Rows.Add(GetRowByCar(car));

      return dt;
    }

    public DateTime? GetGuaranteeEndDate(int carId)
    {
      const int mileageGuarantee = 100000;

      var mileageList = MileageList.getInstance();
      var mileage = mileageList.getItem(carId);

      var dbCar = _dbContext.Car.GetCarById(carId);
      var car = Mapper.Map<CarModel>(dbCar);
      
      var dateEnd = car.DateGet?.AddYears(3);

      var miles = 0;
      if (mileage != null)
      {
        int.TryParse(mileage.Count, out miles);
      }

      if (miles < mileageGuarantee && DateTime.Today < dateEnd)
        return dateEnd;

      return null;
    }

    public object[] GetRowByCar(CarModel car)
    {
      var mark = MarkList.getInstance().getItem(car.MarkId);
      var model = ModelList.getInstance().getItem(car.ModelId);

      var mileageList = MileageList.getInstance();
      ForCar.Mileage mileage = mileageList.getItem(car.Id);
      InvoiceList invoiceList = InvoiceList.getInstance();
      Invoice invoice = invoiceList.GetItem(car.Id);

      PTSList ptsList = PTSList.getInstance();
      PTS pts = ptsList.getItem(car.Id);

      STSList stsList = STSList.getInstance();
      STS sts = stsList.getItem(car.Id);
      
      Regions regions = Regions.getInstance();
      string regionName = invoice == null
        ? regions.getItem(car.RegionIdUsing.Value)
        : regions.getItem(Convert.ToInt32(invoice.RegionToId));

      int mileageInt = 0;
      DateTime mileageDate = DateTime.Today;
      if (mileage != null)
      {
        int.TryParse(mileage.Count, out mileageInt);
        mileageDate = mileage.MonthToString();
      }

      var driver = DriverCarList.getInstance().GetDriver(car.Id);
      var owner = Owners.getInstance().getItem(car.OwnerId.Value);

      var guaranteeEndDate = GetGuaranteeEndDate(car.Id);

      return new object[]
      {
        car.Id, car.Id, car.BbNumber, car.Grz, mark.Name, model.Name, car.Vin, regionName,
        driver.GetName(NameType.Full), pts.Number, sts.Number, car.Year, mileageInt,
        mileageDate, owner, guaranteeEndDate, GetStatus(car)
      };
    }

    public string GetStatus(CarModel car)
    {
      DTPList dtpList = DTPList.getInstance();
      DTP dtp = dtpList.GetLast(car.Id);

      StatusAfterDTPs statusAfterDTPs = StatusAfterDTPs.getInstance();
      string statusAfterDTP = statusAfterDTPs.getItem(Convert.ToInt32(dtp.IDStatusAfterDTP));
      
      var carSale = _carSaleService.GetSaleCars().FirstOrDefault(с => с.Id == car.Id);

      if (carSale?.Date != null)
        return "продан";
      if (carSale != null)
        return "на продажу";
      
      if (!car.IsGet)
        return "покупка";

      if (statusAfterDTP == "А/м НЕ на ходу")
        return "в ремонте";

      return "на ходу";
    }
  }
}
