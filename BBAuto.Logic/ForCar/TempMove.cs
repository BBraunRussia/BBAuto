using System;
using System.Data;
using BBAuto.Logic.Abstract;
using BBAuto.Logic.Entities;
using BBAuto.Logic.Lists;
using BBAuto.Logic.Static;

namespace BBAuto.Logic.ForCar
{
  public class TempMove : MainDictionary
  {
    public DateTime DateBegin { get; set; }
    public DateTime DateEnd { get; set; }
    public Car Car { get; set; }
    public Driver Driver { get; set; }

    public TempMove(int carId)
    {
      Id = 0;
      Car = CarList.getInstance().getItem(carId);
      DateBegin = DateTime.Today;
      DateEnd = DateTime.Today;
    }

    public TempMove(DataRow row)
    {
      fillFields(row);
    }

    private void fillFields(DataRow row)
    {
      int id;
      int.TryParse(row.ItemArray[0].ToString(), out id);
      Id = id;

      int idCar;
      int.TryParse(row.ItemArray[1].ToString(), out idCar);
      Car = CarList.getInstance().getItem(idCar);

      int idDriver;
      int.TryParse(row.ItemArray[2].ToString(), out idDriver);
      Driver = DriverList.getInstance().getItem(idDriver);

      DateTime dateBegin;
      DateTime.TryParse(row.ItemArray[3].ToString(), out dateBegin);
      DateBegin = dateBegin;

      DateTime dateEnd;
      DateTime.TryParse(row.ItemArray[4].ToString(), out dateEnd);
      DateEnd = dateEnd;
    }

    public override void Save()
    {
      int id;
      int.TryParse(Provider.Insert("TempMove", Id, Car.Id, Driver.Id, DateBegin, DateEnd), out id);
      Id = id;
    }

    internal override object[] ToRow()
    {
      return new object[] {Id, Car.Id, Car.BBNumber, Car.Grz, Driver.GetName(NameType.Full), DateBegin, DateEnd};
    }

    public bool IsDriverCar(int carId, DateTime date)
    {
      return Car.Id == carId && date >= DateBegin && date <= DateEnd;
    }
  }
}
