using System;
using System.ComponentModel;

namespace BBAuto.Domain.Services.DriverTransponder
{
  public class DriverTransponder
  {
    [Browsable(false)]
    public int Id { get; set; }
    [Browsable(false)]
    public int TransponderId { get; set; }
    [Browsable(false)]
    public int DriverId { get; set; }

    [DisplayName("Водитель")]
    public string DriverFio { get; set; }
    [DisplayName("Начало использования")]
    public DateTime? DateBegin { get; set; }
    [DisplayName("Окончание использования")]
    public DateTime? DateEnd { get; set; }
  }
}
