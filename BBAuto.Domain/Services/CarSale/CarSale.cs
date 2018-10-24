using System;

namespace BBAuto.Domain.Services.CarSale
{
  public class CarSale
  {
    public int CarId { get; set; }
    public string Comment { get; set; }
    public DateTime? Date { get; set; }
    public int? CustomerId { get; set; }
  }
}
