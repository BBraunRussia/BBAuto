using System.Collections.Generic;
using System.Data;

namespace BBAuto.Domain.Services.CarSale
{
  public interface ICarSaleService
  {
    CarSale GetCarSaleByCarId(int carId);

    void DeleteCarFromSale(int carId);
    CarSale SaveCarSale(CarSale carSale);

    DataTable ToDataTable();
    
    IList<CarSale> GetCarSaleList();
  }
}
