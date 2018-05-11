using System;
using System.Data;
using BBAuto.Logic.Lists;

namespace BBAuto.Logic.Entities
{
  public class DriverCar
  {
    private DateTime dateBegin;
    public readonly DateTime dateEnd;
    public int Number { get; private set; }
    public Car Car { get; private set; }
    public Driver Driver { get; private set; }

    public DriverCar(DataRow row)
    {
      int idCar;
      int.TryParse(row.ItemArray[0].ToString(), out idCar);
      Car = CarList.getInstance().getItem(idCar);

      int idDriver;
      int.TryParse(row.ItemArray[1].ToString(), out idDriver);
      Driver = DriverList.getInstance().getItem(idDriver);

      DateTime.TryParse(row.ItemArray[2].ToString(), out dateBegin);
      DateTime.TryParse(row.ItemArray[3].ToString(), out dateEnd);

      int number;
      int.TryParse(row.ItemArray[4].ToString(), out number);
      Number = number;

      dateEnd = dateEnd.Date;
    }

    internal bool IsDriverCar(int carId, DateTime date)
    {
      if (date >= DateTime.Today && (dateEnd == DateTime.Today))
        return carId == Car.Id && date >= dateBegin;

      return carId == Car.Id && date >= dateBegin && date < dateEnd;
    }

    internal bool isCarsDriver(Driver driver, DateTime date)
    {
      if ((date >= DateTime.Today) && (dateEnd == DateTime.Today))
        return driver.Id == Driver.Id && date >= dateBegin;
      else
        return driver.Id == Driver.Id && date >= dateBegin && date < dateEnd;
    }
  }
}
