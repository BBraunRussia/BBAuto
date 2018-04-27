using System;
using BBAuto.Logic.Common;
using BBAuto.Logic.Services.Car;

namespace BBAuto.Logic.Services.Mileage
{
  public interface IMileageService
  {
    MileageModel GetLastMileage(CarModel car);
    MileageReport AddMileage(CarModel car, string value, DateTime date);
  }
}
