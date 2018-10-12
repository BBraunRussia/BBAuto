using BBAuto.Domain.Abstract;
using BBAuto.Domain.Dictionary;
using BBAuto.Domain.ForCar;
using BBAuto.Domain.Lists;
using BBAuto.Domain.Static;
using BBAuto.Domain.Tables;
using System;
using System.Data;
using BBAuto.Domain.Services.CarSale;

namespace BBAuto.Domain.Entities
{
  public class Car : MainDictionary
  {
    public CarInfo info;

    private int _idGrade;
    private int _year;
    private int _idColor;
    private int _idRegionBuy;
    private int _idOwner;
    private int _idDriver;
    private int _isGet;
    private int _isLising;
    private DateTime _lisingDate;
    private string _invertoryNumber;

    private int _idModel;
    public string vin;
    public string Grz { get; set; }

    private int _number;

    public string eNumber;
    public string bodyNumber;
    public DateTime dateOrder;
    public DateTime dateGet;
    public string dop;
    public string events;
    public int idDiller;
    public double cost;

    public bool IsSale { get; set; }
    public int RegionUsingId { get; set; }

    public string GradeID
    {
      get { return _idGrade.ToString(); }
      set { int.TryParse(value, out _idGrade); }
    }

    public string ModelID
    {
      get { return _idModel.ToString(); }
      set { int.TryParse(value, out _idModel); }
    }

    public Mark Mark { get; set; }

    public string Year
    {
      get { return _year.ToString(); }
      set { _year = Convert.ToInt32(value); }
    }

    public object ColorID
    {
      get { return _idColor.ToString(); }
      set
      {
        if (value != null)
          int.TryParse(value.ToString(), out _idColor);
      }
    }

    public object RegionBuyID
    {
      get { return _idRegionBuy.ToString(); }
      set
      {
        if (value != null)
          int.TryParse(value.ToString(), out _idRegionBuy);
      }
    }

    public object ownerID
    {
      get { return _idOwner; }
      set
      {
        if (value != null)
          int.TryParse(value.ToString(), out _idOwner);
      }
    }
    
    public object driverID
    {
      get => _idDriver.ToString();
      set
      {
        if (value != null)
          int.TryParse(value.ToString(), out _idDriver);
      }
    }

    public bool IsGet
    {
      get => Convert.ToBoolean(_isGet);
      set => _isGet = Convert.ToInt32(value);
    }

    public string BBNumber => _number < 100 ? "АМ-0" + _number : "АМ-" + _number;

    public int BBNumberInt
    {
      get => _number;
      set => _number = value;
    }

    public string Lising
    {
      get => _isLising == 1 ? _lisingDate.Date.ToShortDateString() : string.Empty;
      set
      {
        if (DateTime.TryParse(value, out _lisingDate))
          _isLising = 1;
        else
        {
          _isLising = 0;
          _lisingDate = DateTime.Today;
        }
      }
    }

    public string InvertoryNumber
    {
      get { return _invertoryNumber; }
      set { _invertoryNumber = value; }
    }

    public Car()
    {
      ID = 0;

      CarList carList = CarList.GetInstance();
      _number = carList.getNextBBNumber();
      dateOrder = DateTime.Today;
      dateGet = DateTime.Today;
      _year = DateTime.Today.Year;

      Init();
    }

    public Car(int id)
    {
      ID = id;
    }

    public Car(DataRow row)
    {
      fillField(row);

      Init();
    }

    private void Init()
    {
      info = new CarInfo(this);
    }

    private void fillField(DataRow row)
    {
      int.TryParse(row.ItemArray[0].ToString(), out int id);
      ID = id;

      int.TryParse(row.ItemArray[1].ToString(), out _number);
      Grz = row.ItemArray[2].ToString();
      vin = row.ItemArray[3].ToString();
      Year = row.ItemArray[4].ToString();
      eNumber = row.ItemArray[5].ToString();
      bodyNumber = row.ItemArray[6].ToString();

      int.TryParse(row.ItemArray[7].ToString(), out int idMark);
      Mark = MarkList.getInstance().getItem(idMark);

      int.TryParse(row.ItemArray[8].ToString(), out _idModel);
      GradeID = row.ItemArray[9].ToString();
      ColorID = row.ItemArray[10];

      fillCarBuy(row);

      IsSale = Convert.ToBoolean(row.ItemArray[24]);
    }

    private void fillCarBuy(DataRow row)
    {
      ownerID = row.ItemArray[11].ToString();
      RegionBuyID = row.ItemArray[12].ToString();

      if (int.TryParse(row.ItemArray[13].ToString(), out int regionUsingId))
        RegionUsingId = regionUsingId;

      driverID = row.ItemArray[14].ToString();

      if (!DateTime.TryParse(row.ItemArray[15].ToString(), out dateOrder))
        dateOrder = DateTime.Today;

      _isGet = Convert.ToInt32(row.ItemArray[16]);

      if (!DateTime.TryParse(row.ItemArray[17].ToString(), out dateGet))
        dateGet = DateTime.Today;

      double.TryParse(row.ItemArray[18].ToString(), out cost);

      dop = row.ItemArray[19].ToString();
      events = row.ItemArray[20].ToString();

      int.TryParse(row.ItemArray[21].ToString(), out idDiller);

      Lising = row.ItemArray[22].ToString();
      InvertoryNumber = row.ItemArray[23].ToString();
    }

    public override void Save()
    {
      int.TryParse(
        _provider.Insert("Car", ID, _number, Grz, vin, Year, eNumber, bodyNumber, GradeID, ColorID, _isLising,
          _lisingDate, _invertoryNumber), out int id);
      ID = id;

      saveCarBuy();

      CarList.GetInstance().ReLoad();
    }

    private void saveCarBuy()
    {
      _provider.Insert("CarBuy", ID, _idOwner, _idRegionBuy, RegionUsingId, driverID, dateOrder, _isGet, dateGet, cost,
        dop, events, idDiller);
    }

    public DTP createDTP()
    {
      return new DTP(this);
    }

    public Policy CreatePolicy()
    {
      return new Policy(this);
    }

    public Violation createViolation()
    {
      return new Violation(this);
    }

    public ShipPart createShipPart()
    {
      return new ShipPart(this);
    }

    public DataTable getCarInfo()
    {
      DataTable dt2 = new DataTable();
      dt2.Columns.Add("Название");
      dt2.Columns.Add("Значение");

      dt2.Rows.Add("Год выпуска", Year);
      dt2.Rows.Add("Цвет", info.Color);
      dt2.Rows.Add("Собственник", info.Owner);
      dt2.Rows.Add("Дата покупки", dateGet.ToShortDateString());
      dt2.Rows.Add("Мощность двигателя", info.Grade.EPower);
      dt2.Rows.Add("Объем двигателя", info.Grade.EVol);
      dt2.Rows.Add("Разрешенная максимальная масса", info.Grade.MaxLoad);
      dt2.Rows.Add("Масса без нагрузки", info.Grade.NoLoad);
      dt2.Rows.Add("Модель № двигателя", eNumber);
      dt2.Rows.Add("№ кузова", bodyNumber);

      return dt2;
    }

    public DataTable getDataTableDiagCard()
    {
      DiagCardList diagCardList = DiagCardList.getInstance();

      return diagCardList.ToDataTable(this);
    }

    public DiagCard createDiagCard()
    {
      return new DiagCard(this);
    }
    
    public Invoice createInvoice()
    {
      return new Invoice(this);
    }

    public Repair createRepair()
    {
      return new Repair(this);
    }

    public PTS createPTS()
    {
      return new PTS(this);
    }

    public STS createSTS()
    {
      return new STS(this);
    }

    public TempMove createTempMove()
    {
      return new TempMove(this);
    }

    internal override object[] getRow()
    {
      MileageList mileageList = MileageList.getInstance();
      Mileage mileage = mileageList.getItemByCarId(ID);
      
      PTSList ptsList = PTSList.getInstance();
      PTS pts = ptsList.getItem(this);

      STSList stsList = STSList.getInstance();
      STS sts = stsList.getItem(this);

      int mileageInt = 0;
      DateTime mileageDate = DateTime.Today;
      if (mileage != null)
      {
        mileageInt = mileage.Count;
        mileageDate = mileage.MonthToString();
      }

      return new object[]
      {
        ID, ID, BBNumber, Grz, Mark.Name, info.Model, vin, info.Region,
        info.Driver.FullName, pts.Number, sts.Number, Year, mileageInt,
        mileageDate, info.Owner, info.Guarantee, GetStatus()
      };
    }

    public CarDoc createCarDoc(string file)
    {
      CarDoc carDoc = new CarDoc(this);
      carDoc.File = file;
      carDoc.Name = System.IO.Path.GetFileNameWithoutExtension(file);

      return carDoc;
    }

    public override string ToString()
    {
      return (ID == 0) ? "нет данных" : string.Concat(Mark.Name, " ", info.Model, " ", Grz);
    }

    internal override void Delete()
    {
      _provider.Delete("Car", ID);
    }

    public string GetStatus()
    {
      if (IsSale)
      {
        ICarSaleService carSaleService = new CarSaleService();
        var carSale = carSaleService.GetCarSaleByCarId(ID);
        if (carSale.Date.HasValue)
          return "продан";
      }
      if (IsSale)
        return "на продажу";

      if (!IsGet)
        return "покупка";

      DTPList dtpList = DTPList.getInstance();
      DTP dtp = dtpList.GetLast(this);

      if (_number == 170)
        _number = 170;

      var statusAfterDtPs = StatusAfterDTPs.getInstance();
      var statusAfterDtp = statusAfterDtPs.getItem(Convert.ToInt32(dtp.IDStatusAfterDTP));

      if (statusAfterDtp == "НЕ на ходу")
        return "в ремонте";
        
      return "на ходу";
    }
  }
}
