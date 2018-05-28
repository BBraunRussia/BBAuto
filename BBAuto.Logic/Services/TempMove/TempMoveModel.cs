using System;

namespace BBAuto.Logic.Services.TempMove
{
  public class TempMoveModel
  {
    public int Id { get; set; }
    public int CarId { get; set; }
    public int DriverId { get; set; }
    public DateTime DateBegin { get; set; }
    public DateTime DateEnd { get; set; }
  }
}
