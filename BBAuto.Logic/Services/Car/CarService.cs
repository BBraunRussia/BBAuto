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
using BBAuto.Logic.Services.Dictionary.Color;
using BBAuto.Logic.Services.Dictionary.Mark;
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
    private readonly IMarkService _markService;
    private readonly IColorService _colorService;

    public CarService(
      IDbContext dbContext,
      ISaleCarService carSaleService,
      IDiagCardService diagCardService,
      IMarkService markService,
      IColorService colorService)
    {
      _dbContext = dbContext;
      _carSaleService = carSaleService;
      _diagCardService = diagCardService;
      _markService = markService;
      _colorService = colorService;
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
      var dt = new DataTable();
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
      var mark = _markService.GetItemById(car.MarkId ?? 0);
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
        car.Id, car.Id, car.BbNumber, car.Grz, mark.Name ?? "отсутствует", model.Name, car.Vin, regionName,
        driver.GetName(NameType.Full), pts.Number, sts.Number, car.Year, mileageInt,
        mileageDate, owner, guaranteeEndDate, GetStatus(car)
      };
    }

    public string GetStatus(CarModel car)
    {
      var dtpList = DTPList.getInstance();
      var dtp = dtpList.GetLast(car.Id);

      var statusAfterDTPs = StatusAfterDTPs.getInstance();
      var statusAfterDTP = statusAfterDTPs.getItem(Convert.ToInt32(dtp.IDStatusAfterDTP));
      
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

    public DataTable GetDataTableInfoByCarId(int id)
    {
      var car = GetCarById(id);
      
      var dt = new DataTable();
      dt.Columns.Add("Название");
      dt.Columns.Add("Значение");

      if (car == null)
        return dt;

      if (!car.MarkId.HasValue)
        return dt;

      var mark = _markService.GetItemById(car.MarkId.Value);
      var model = ModelList.getInstance().getItem(car.ModelId ?? 0);
      var color = _colorService.GetItemById(car.ColorId ?? 0);
      var owner = Owners.getInstance().getItem(car.OwnerId ?? 0);
      var pts = PTSList.getInstance().getItem(car.Id);
      var sts = STSList.getInstance().getItem(car.Id);

      dt.Rows.Add("Марка", mark.Name);
      dt.Rows.Add("Модель", model.Name);
      dt.Rows.Add("Год выпуска", car.Year);
      dt.Rows.Add("Цвет", color);
      dt.Rows.Add("Собственник", owner);
      dt.Rows.Add("Дата покупки", car.DateGet.Value.ToShortDateString());
      dt.Rows.Add("Модель № двигателя", car.ENumber);
      dt.Rows.Add("№ кузова", car.BodyNumber);
      dt.Rows.Add("Дата выдачи ПТС:", pts.Date.ToShortDateString());
      dt.Rows.Add("Дата выдачи СТС:", sts.Date.ToShortDateString());

      return dt;
    }

    public string CarToString(int carId)
    {
      var car = GetCarById(carId);

      if (car?.MarkId == null  || car.ModelId == null)
        return string.Empty;

      var model = ModelList.getInstance().getItem(car.ModelId.Value);
      var mark = _markService.GetItemById(car.MarkId ?? 0);

      return car.Id == 0
        ? "нет данных"
        : string.Concat(mark.Name ?? "отсутствует", " ", model.Name, " ", car.Grz);
    }
  }
}
