using System.Collections.Generic;

namespace BBAuto.Logic.Services.Car.Sale
{
  public interface ISaleCarService
  {
    IList<SaleCarModel> GetSaleCars();
    void Save(SaleCarModel saleCar);
    void Delete(int carId);
  }
}
