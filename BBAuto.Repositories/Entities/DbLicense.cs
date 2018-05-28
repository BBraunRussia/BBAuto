using System;

namespace BBAuto.Repositories.Entities
{
  public class DbLicense
  {
    public int Id { get; set; }
    public int DriverId { get; set; }
    public string Number { get; set; }
    public DateTime DateBegin { get; set; }
    public DateTime DateEnd { get; set; }
    public string File { get; set; }
    public bool NotificationSent { get; set; }
  }
}
