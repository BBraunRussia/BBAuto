using System;

namespace BBAuto.Repositories.Entities
{
  public class DbDiagCard
  {
    public int Id { get; set; }
    public int CarId { get; set; }
    public string Number { get; set; }
    public DateTime DateEnd { get; set; }
    public string File { get; set; }
    public bool NotificationSent { get; set; }
  }
}
