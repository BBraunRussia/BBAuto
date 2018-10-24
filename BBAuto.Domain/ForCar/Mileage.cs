using BBAuto.Domain.Abstract;
using BBAuto.Domain.Common;
using BBAuto.Domain.Entities;
using BBAuto.Domain.Lists;
using System;
using System.Data;

namespace BBAuto.Domain.ForCar
{
  public class Mileage : MainDictionary
  {
    public int Count { get; set; }

    public DateTime Date { get; set; }
    public int CarId { get; }

    public Mileage(int carId)
    {
      CarId = carId;
      Date = DateTime.Today;
      ID = 0;
    }

    public Mileage(int carId, DateTime date)
    {
      CarId = carId;
      Date = date;
      ID = 0;
    }

    public Mileage(DataRow row)
    {
      ID = Convert.ToInt32(row.ItemArray[0]);

      if (int.TryParse(row.ItemArray[1].ToString(), out int carId))
        CarId = carId;

      DateTime.TryParse(row.ItemArray[2].ToString(), out DateTime date);
      Date = date;

      if (int.TryParse(row.ItemArray[3].ToString(), out int count))
        Count = count;
    }
    
    public override void Save()
    {
      if (!int.TryParse(_provider.Insert("Mileage", ID, CarId, Date, Count), out int id))
        return;

      ID = id;
      MileageList.getInstance().Add(this);
    }

    internal override object[] getRow()
    {
      return new object[] {ID, Date, Count};
    }

    internal override void Delete()
    {
      _provider.Delete("Mileage", ID);
    }

    internal DateTime MonthToString()
    {
      MyDateTime myDate = new MyDateTime(Date.ToShortDateString());

      return (Count == 0) ? new DateTime(DateTime.Today.Year, 1, 31) : Date;
    }

    public void SetCount(string value)
    {
      Mileage mileage = GetPrev();


      if (!int.TryParse(value.Replace(" ", ""), out int count))
        throw new InvalidCastException();

      var prevCount = mileage?.Count;

      if (count < prevCount && (Date > mileage.Date))
        throw new InvalidConstraintException();

      if (count >= 1000000)
        throw new OverflowException();

      Count = count;
    }

    public string PrevToString()
    {
      var mileage = GetPrev();

      return mileage?.ToString() ?? "0";
    }

    private Mileage GetPrev()
    {
      return MileageList.getInstance().getItem(CarId, this);
    }

    public override string ToString()
    {
      return Count == 0
        ? "(нет данных)"
        : string.Concat(MyString.GetFormatedDigitInteger(Count.ToString()), " км от ", Date.ToShortDateString());
    }
  }
}
