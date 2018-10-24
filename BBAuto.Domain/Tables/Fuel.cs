using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using BBAuto.Domain.Abstract;
using BBAuto.Domain.Lists;
using BBAuto.Domain.ForDriver;

namespace BBAuto.Domain.Tables
{
    public class Fuel : MainDictionary
    {
        public Fuel(DataRow row)
        {
            ID = Convert.ToInt32(row[0].ToString());

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
            ID = Convert.ToInt32(_provider.Insert("Fuel", FuelCard.ID, Date, Value, EngineType.ID));
        }

        internal override object[] getRow()
        {
            return new object[] { ID, Date, Value };
        }
    }
}
