using System;

namespace BBAuto.Repository.Models
{
  public class DbDriverTransponder
  {
    public int Id { get; set; }
    public int TransponderId { get; set; }
    public int DriverId { get; set; }
    public string DriverFio { get; set; }
    public DateTime DateBegin { get; set; }
    public DateTime? DateEnd { get; set; }
  }
}
