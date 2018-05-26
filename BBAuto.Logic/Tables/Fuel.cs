using System;
using System.Data;
using BBAuto.Logic.Abstract;
using BBAuto.Logic.ForDriver;
using BBAuto.Logic.Lists;

namespace BBAuto.Logic.Tables
{
  public class Fuel : MainDictionary
  {
    public FuelCard FuelCard { get; private set; }
    public DateTime Date { get; private set; }
    public double Value { get; private set; }
    public int EngineTypeId { get; private set; }

    public Fuel(DataRow row)
    {
      Id = Convert.ToInt32(row[0].ToString());

      int.TryParse(row[1].ToString(), out var idFuelCard);
      FuelCard = FuelCardList.getInstance().getItem(idFuelCard);

      Date = Convert.ToDateTime(row[2].ToString());
      Value = Convert.ToDouble(row[3].ToString());

      int.TryParse(row[4].ToString(), out int engineTypeId);
      EngineTypeId = engineTypeId;
    }

    internal Fuel(FuelCard fuelCard, DateTime date, int engineTypeId)
    {
      FuelCard = fuelCard;
      Date = date;
      EngineTypeId = engineTypeId;
      Value = 0;
    }
    
    public void AddValue(double value)
    {
      Value += Math.Round(value, 2);
    }

    public override void Save()
    {
      Id = Convert.ToInt32(Provider.Insert("Fuel", FuelCard.Id, Date, Value, EngineTypeId));
    }

    internal override object[] ToRow()
    {
      return new object[] {Id, Date, Value};
    }
  }
}
