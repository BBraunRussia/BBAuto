using System;

namespace BBAuto.Repository.Models
{
  public class DbDriverInstruction
  {
    public int Id { get; set; }
    public int DocumentId { get; set; }
    public int DriverId { get; set; }
    public DateTime Date { get; set; }
  }
}
