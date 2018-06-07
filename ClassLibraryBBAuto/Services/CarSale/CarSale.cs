using System;
using BBAuto.Domain.Dictionary;
using BBAuto.Domain.ForCar;
using BBAuto.Domain.Lists;
using BBAuto.Domain.Services.Customer;

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
