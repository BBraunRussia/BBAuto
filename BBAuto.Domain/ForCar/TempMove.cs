using BBAuto.Domain.Abstract;
using BBAuto.Domain.Entities;
using BBAuto.Domain.Lists;
using BBAuto.Domain.Static;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BBAuto.Domain.ForCar
{
  public class TempMove : MainDictionary
  {
    public DateTime DateBegin { get; set; }
    public DateTime DateEnd { get; set; }
    public Car Car { get; set; }
    public Driver Driver { get; set; }

    internal TempMove(Car car)
    {
      ID = 0;
      Car = car;
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
      ID = id;

      int idCar;
      int.TryParse(row.ItemArray[1].ToString(), out idCar);
      Car = CarList.GetInstance().getItem(idCar);

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
      int.TryParse(_provider.Insert("TempMove", ID, Car.ID, Driver.ID, DateBegin, DateEnd), out id);
      ID = id;
    }

    internal override object[] getRow()
    {
      return new object[] {ID, Car.ID, Car.BBNumber, Car.Grz, Driver.FullName, DateBegin, DateEnd};
    }

    internal bool IsDriverCar(Car car, DateTime date)
    {
      return Car.ID == car.ID && date >= DateBegin && date <= DateEnd;
    }
  }
}
