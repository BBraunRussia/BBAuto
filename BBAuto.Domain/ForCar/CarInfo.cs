using System;
using System.Data;
using BBAuto.Domain.Dictionary;
using BBAuto.Domain.Lists;
using BBAuto.Domain.Entities;

namespace BBAuto.Domain.ForCar
{
  public class CarInfo
  {
    private const int MileageGuarantee = 100000;
    private readonly Car _car;

    public CarInfo(Car car)
    {
      _car = car;
    }

    public string Model => ModelList.getInstance().getItem(Convert.ToInt32(_car.ModelID)).Name;
    public string Color => Colors.getInstance().getItem(Convert.ToInt32(_car.ColorID));
    public string Owner => Owners.getInstance().getItem(Convert.ToInt32(_car.ownerID));
    public Grade Grade => GradeList.getInstance().getItem(Convert.ToInt32(_car.GradeID));

    public string Region
    {
      get
      {
        var invoice = InvoiceList.getInstance().getItem(_car);

        var regions = Regions.getInstance();
        return invoice == null
          ? regions.getItem(_car.RegionUsingId)
          : regions.getItem(Convert.ToInt32(invoice.RegionToID));
      }
    }

    /*
    public bool IsSale
    {
      get
      {
        ICarSaleService carSaleService = new CarSaleService();
        return carSaleService.GetCarSaleByCarId(_car.ID) != null;
      }
    } 
    */
    public Driver Driver => DriverCarList.GetInstance().GetDriver(_car) ?? new Driver();

    public PTS pts => PTSList.getInstance().getItem(_car);

    public STS sts => STSList.getInstance().getItem(_car);

    public DateTime Guarantee
    {
      get
      {
        MileageList mileageList = MileageList.getInstance();
        Mileage mileage = mileageList.getItemByCarId(_car.ID);

        DateTime dateEnd = _car.dateGet.AddYears(3);

        var miles = 0;
        if (mileage != null)
        {
          miles = mileage.Count;
        }

        return ((miles < MileageGuarantee) && (DateTime.Today < dateEnd)) ? dateEnd : new DateTime(1, 1, 1);
      }
    }

    public DataTable ToDataTable()
    {
      DataTable dt = new DataTable();
      dt.Columns.Add("Название");
      dt.Columns.Add("Значение");

      if (_car.Mark == null)
        return dt;

      dt.Rows.Add("Марка", _car.Mark.Name);
      dt.Rows.Add("Модель", Model);
      dt.Rows.Add("Год выпуска", _car.Year);
      dt.Rows.Add("Цвет", Color);
      dt.Rows.Add("Собственник", Owner);
      dt.Rows.Add("Дата покупки", _car.dateGet.ToShortDateString());
      dt.Rows.Add("Модель № двигателя", _car.eNumber);
      dt.Rows.Add("№ кузова", _car.bodyNumber);
      dt.Rows.Add("Дата выдачи ПТС:", pts.Date.ToShortDateString());
      dt.Rows.Add("Дата выдачи СТС:", sts.Date.ToShortDateString());

      return dt;
    }
  }
}
