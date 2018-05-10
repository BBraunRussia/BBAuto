using System;

namespace BBAuto.Logic.Services.DiagCard
{
  public class DiagCardModel
  {
    public int Id { get; set; }
    public int CarId { get; set; }
    public string Number { get; set; }
    public DateTime Date { get; set; }
    public string File { get; set; }
    public bool NotificationSent { get; set; }
  }
}
