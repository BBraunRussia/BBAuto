using System.Collections.Generic;
using System.Data;
using BBAuto.Logic.Static;

namespace BBAuto.Logic.Services.Car
{
  public interface ICarService
  {
    CarModel GetCarByGrz(string grz);
    
    IList<CarModel> GetCars();

    DataTable ToDataTable(Status status);
  }
}
