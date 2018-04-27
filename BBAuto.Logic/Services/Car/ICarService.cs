using System.Collections.Generic;

namespace BBAuto.Logic.Services.Car
{
  public interface ICarService
  {
    CarModel GetCarByGrz(string grz);
    
    IList<CarModel> GetCars();
  }
}
