using System;
using System.Data;
using BBAuto.Logic.Dictionary;
using BBAuto.Logic.Entities;
using BBAuto.Logic.Lists;

namespace BBAuto.Logic.ForCar
{
  public class CarInfo
  {
    private const int MILEAGE_GUARANTEE = 100000;
    private Car _car;

    public CarInfo(Car car)
    {
      _car = car;
    }

    public string Model
    {
      get { return ModelList.getInstance().getItem(Convert.ToInt32(_car.ModelID)).Name; }
    }

    public string Color
    {
      get { return Colors.GetInstance().getItem(Convert.ToInt32(_car.ColorID)); }
    }

    public string Owner
    {
      get { return Owners.getInstance().getItem(Convert.ToInt32(_car.ownerID)); }
    }
    
    public bool IsSale
    {
      get { return CarSaleList.getInstance().getItem(_car.Id) != null; }
    }

    public Driver Driver
    {
      get { return DriverCarList.getInstance().GetDriver(_car.Id) ?? new Driver(); }
    }

    public PTS pts
    {
      get { return PTSList.getInstance().getItem(_car); }
    }

    public STS sts
    {
      get { return STSList.getInstance().getItem(_car); }
    }

    public DateTime Guarantee
    {
      get
      {
        MileageList mileageList = MileageList.getInstance();
        Mileage mileage = mileageList.getItem(_car);

        DateTime dateEnd = _car.dateGet.AddYears(3);

        int miles = 0;
        if (mileage != null)
        {
          int.TryParse(mileage.Count, out miles);
        }

        return ((miles < MILEAGE_GUARANTEE) && (DateTime.Today < dateEnd)) ? dateEnd : new DateTime(1, 1, 1);
      }
    }

    public DataTable ToDataTable()
    {
      DataTable dt = new DataTable();
      dt.Columns.Add("Название");
      dt.Columns.Add("Значение");

      if (_car.MarkId == 0)
        return dt;

      dt.Rows.Add("Марка", _car.MarkId);
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
