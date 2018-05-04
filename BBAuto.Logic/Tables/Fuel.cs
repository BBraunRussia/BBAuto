using System;
using System.Data;
using BBAuto.Logic.Abstract;
using BBAuto.Logic.ForDriver;
using BBAuto.Logic.Lists;

namespace BBAuto.Logic.Tables
{
  public class Fuel : MainDictionary
  {
    public Fuel(DataRow row)
    {
      Id = Convert.ToInt32(row[0].ToString());

      int idFuelCard;
      int.TryParse(row[1].ToString(), out idFuelCard);
      FuelCard = FuelCardList.getInstance().getItem(idFuelCard);

      Date = Convert.ToDateTime(row[2].ToString());
      Value = Convert.ToDouble(row[3].ToString());

      int idEngineType;
      int.TryParse(row[4].ToString(), out idEngineType);
      EngineType = EngineTypeList.getInstance().getItem(idFuelCard);
    }

    internal Fuel(FuelCard fuelCard, DateTime date, EngineType engineType)
    {
      FuelCard = fuelCard;
      Date = date;
      EngineType = engineType;
      Value = 0;
    }

    public FuelCard FuelCard { get; private set; }
    public DateTime Date { get; private set; }
    public double Value { get; private set; }
    public EngineType EngineType { get; private set; }

    public void AddValue(double value)
    {
      Value += Math.Round(value, 2);
    }

    public override void Save()
    {
      Id = Convert.ToInt32(Provider.Insert("Fuel", FuelCard.Id, Date, Value, EngineType.Id));
    }

    internal override object[] ToRow()
    {
      return new object[] {Id, Date, Value};
    }
  }
}
