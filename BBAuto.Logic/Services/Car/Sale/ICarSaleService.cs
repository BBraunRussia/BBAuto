using System.Collections.Generic;

namespace BBAuto.Logic.Services.Car.Sale
{
  public interface ISaleCarService
  {
    IList<SaleCarModel> GetCars();
  }
}
