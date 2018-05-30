using System.Collections.Generic;
using System.Data;
using BBAuto.Logic.Static;

namespace BBAuto.Logic.Services.Car
{
  public interface ICarService
  {
    CarModel GetCarByGrz(string grz);
    
    IList<CarModel> GetCars();
    IList<CarModel> GetCars(IList<int> ids);

    DataTable ToDataTable(Status status);
    DataTable GetDataTableInfoByCarId(int id);

    CarModel GetCarById(int id);
    CarModel Save(CarModel car);

    int GetNextBbNumber();
    string CarToString(int carId);
    DataTable GetDataTableCarsByIds(IList<int> ids);
  }
}
