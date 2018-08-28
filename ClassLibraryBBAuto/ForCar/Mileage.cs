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

    public string CountString => Count == 0 ? string.Empty : Count.ToString();

    public DateTime Date { get; set; }
    public Car Car { get; private set; }

    internal Mileage(Car car)
    {
      Car = car;
      Date = DateTime.Today.Date;
      ID = 0;
    }

    public Mileage(DataRow row)
    {
      fillFields(row);
    }

    private void fillFields(DataRow row)
    {
      ID = Convert.ToInt32(row.ItemArray[0]);

      int.TryParse(row.ItemArray[1].ToString(), out int idCar);
      Car = CarList.GetInstance().getItem(idCar);

      DateTime.TryParse(row.ItemArray[2].ToString(), out DateTime date);
      Date = date;

      if (int.TryParse(row.ItemArray[3].ToString(), out int count))
        Count = count;

    }

    public override void Save()
    {
      ID = Convert.ToInt32(_provider.Insert("Mileage", ID, Car.ID, Date, Count));

      MileageList mileageList = MileageList.getInstance();
      mileageList.Add(this);
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
      return MileageList.getInstance().getItem(Car, this);
    }

    public override string ToString()
    {
      return (CountString == string.Empty)
        ? "(нет данных)"
        : string.Concat(MyString.GetFormatedDigitInteger(CountString), " км от ", Date.ToShortDateString());
    }
  }
}
