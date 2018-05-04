using System;
using System.Collections.Generic;
using System.Data;
using BBAuto.Logic.Common;

namespace BBAuto.Logic.Services.Mileage
{
  public interface IMileageService
  {
    MileageReport AddMileage(int carId, string value, DateTime date);
    MileageModel GetLastMileage(int carId);
    MileageModel GetMileage(int id);

    DataTable ToDataTable(int carId);
    IList<MileageModel> GetMileageByCarId(int carId);
    MileageModel Save(MileageModel mileage);
    void Delete(int mileageId);
  }
}
