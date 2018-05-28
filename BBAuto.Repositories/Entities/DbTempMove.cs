using System;

namespace BBAuto.Repositories.Entities
{
  public class DbTempMove
  {
    public int Id { get; set; }
    public int CarId { get; set; }
    public int DriverId { get; set; }
    public DateTime DateBegin { get; set; }
    public DateTime DateEnd { get; set; }
  }
}
