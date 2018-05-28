using System;
using BBAuto.Logic.Common;

namespace BBAuto.Logic.Services.Mileage
{
  public class MileageModel
  {
    private MileageModel() { }

    public MileageModel(int carId)
    {
      CarId = carId;
      Date = DateTime.Today;
    }

    public int Id { get; private set; }
    public int Count { get; private set; }
    public DateTime Date { get; set; }
    public int CarId { get; private set; }

    public void SetCount(string value)
    {
      if (!int.TryParse(value.Replace(" ", ""), out int count))
        throw new InvalidCastException();

      if (count >= 1000000)
        throw new OverflowException();

      Count = count;
    }

    public override string ToString()
    {
      return string.Concat(MyString.GetFormatedDigitInteger(Count.ToString()), " км от ", Date.ToShortDateString());
    }

    public object[] ToRow()
    {
      return new object[] { Id, Date, Count };
    }
  }
}
