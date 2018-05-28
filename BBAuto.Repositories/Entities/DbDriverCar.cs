using System;

namespace BBAuto.Repositories.Entities
{
  public class DbDriverCar
  {
    public int CarId { get; set; }
    public int DriverId { get; set; }
    public DateTime Date1 { get; set; }
    public DateTime Date2 { get; set; }
    public string Number { get; set; }
  }
}
