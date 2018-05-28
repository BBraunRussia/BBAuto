using System;

namespace BBAuto.Logic.Services.License
{
  public class LicenseModel
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
