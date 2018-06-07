using System;

namespace BBAuto.Repository.Models
{
  public class DbCarSale
  {
    public int CarId { get; set; }
    public string Comment { get; set; }
    public DateTime? Date { get; set; }
    public int? CustomerId { get; set; }
  }
}
