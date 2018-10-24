using BBAuto.Domain.Lists;
using System;
using System.Data;

namespace BBAuto.Domain.Entities
{
  public class DriverCar
  {
    public DateTime DateBegin { get; }
    public DateTime DateEnd { get; }
    public int Number { get; }
    public Car Car { get; }
    public Driver Driver { get; }
    public bool IsMain { get; }
    public int RegionId { get; }

    public DriverCar(DataRow row)
    {
      int.TryParse(row.ItemArray[0].ToString(), out int idCar);
      Car = CarList.GetInstance().getItem(idCar);

      int.TryParse(row.ItemArray[1].ToString(), out int idDriver);
      Driver = DriverList.getInstance().getItem(idDriver);

      DateTime.TryParse(row.ItemArray[2].ToString(), out DateTime dateBegin);
      DateBegin = dateBegin;

      DateTime.TryParse(row.ItemArray[3].ToString(), out DateTime dateEnd);
      DateEnd = dateEnd;

      int.TryParse(row.ItemArray[4].ToString(), out int number);
      Number = number;

      bool.TryParse(row.ItemArray[5].ToString(), out bool isMain);
      IsMain = isMain;

      int.TryParse(row.ItemArray[6].ToString(), out int regionId);
      RegionId = regionId;

      DateEnd = DateEnd.Date;
    }

    internal bool IsDriverCar(Car car, DateTime date)
    {
      if (date >= DateTime.Today && DateEnd == DateTime.Today)
        return car.ID == Car.ID && date >= DateBegin;

      return car.ID == Car.ID && date >= DateBegin && date < DateEnd;
    }

    internal bool IsCarsDriver(Driver driver, DateTime date)
    {
      if (date >= DateTime.Today && DateEnd == DateTime.Today)
        return driver.ID == Driver.ID && date >= DateBegin;

      return driver.ID == Driver.ID && date >= DateBegin && date < DateEnd;
    }
  }
}
