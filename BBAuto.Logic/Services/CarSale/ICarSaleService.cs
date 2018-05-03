using System.Collections.Generic;

namespace BBAuto.Logic.Services.CarSale
{
  public interface ICarSaleService
  {
    IList<CarSaleModel> GetCars();
  }
}
