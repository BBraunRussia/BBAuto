using System;

namespace BBAuto.Repositories.Entities
{
  public class DbSaleCar
  {
    public int CarId { get; set; }
    public DateTime? CarSaleDate { get; set; }
    public string Comment { get; set; }
  }
}
