using System;
using System.Text;
using BBAuto.Logic.Services.Car;

namespace BBAuto.Logic.Services.DiagCard
{
  public class DiagCardModel
  {
    public int Id { get; set; }
    public int CarId { get; set; }
    public string Number { get; set; }
    public DateTime DateEnd { get; set; }
    public string File { get; set; }
    public bool NotificationSent { get; set; }

    public object[] ToRow(CarModel car)
    {
      return new object[] { Id, CarId, car.BbNumberString, car.Grz, Number, DateEnd };
    }

    public string ToMail(ICarService carService)
    {
      NotificationSent = true;

      var car = carService.GetCarById(CarId);

      var sb = new StringBuilder();
      sb.Append(car.Grz);
      sb.Append(" ");
      sb.Append(Number);
      sb.Append(" ");
      sb.Append(DateEnd.ToShortDateString());
      return sb.ToString();
    }
  }
}
