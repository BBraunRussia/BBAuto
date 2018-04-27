using System;

namespace BBAuto.Repositories.Entities
{
  public class DbMileage
  {
    public int Id { get; set; }
    public int CarId { get; set; }
    public DateTime Date { get; set; }
    public int Count { get; set; }
  }
}
