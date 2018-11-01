using BBAuto.Domain.Entities;

namespace BBAuto.Domain.Common
{
  public class MileageReport
  {
    public Car Car { get; set; }
    public string Grz { get; set; }
    public string Fio { get; set; }
    public string Mileage { get; set; }
    public string Filename { get; set; }
    public string Message { get; set; }

    public MileageReport()
    {
      Car = null;
      Grz = "не определено";
      Fio = "не определено";
      Mileage = "не определено";
    }

    public override string ToString()
    {
      return Car == null ? Message : Message + " " + Car;
    }

    public bool IsFailed => Car == null;
  }
}
