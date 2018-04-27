using System;
using System.Linq;
using AutoMapper;
using BBAuto.Logic.Common;
using BBAuto.Logic.Services.Car;
using BBAuto.Repositories;
using BBAuto.Repositories.Entities;

namespace BBAuto.Logic.Services.Mileage
{
  public class MileageService : IMileageService
  {
    private readonly IDbContext _dbContext;

    public MileageService(IDbContext dbContext)
    {
      _dbContext = dbContext;
    }
    
    public MileageReport AddMileage(CarModel car, string value, DateTime date)
    {
      int.TryParse(value, out int count);

      var lastMileage = GetLastMileage(car);

      if (count < lastMileage?.Count)
      {
        return new MileageReport(car, "Значение пробега меньше, чем уже внесён в систему.", true);
      }

      var mileage = new MileageModel
      {
        Date = new DateTime(date.Year, date.Month, DateTime.DaysInMonth(date.Year, date.Month))
      };

      mileage.SetCount(value);
      SaveMileage(mileage);

      return new MileageReport(car, "Пробег загружен", true);
    }

    public MileageModel GetLastMileage(CarModel car)
    {
      return Mapper.Map<MileageModel>(_dbContext.Mileage.GetMileageByCarId(car.Id).OrderByDescending(m => m.Date).Last());
    }

    private MileageModel SaveMileage(MileageModel mileage)
    {
      var dbMileage = Mapper.Map<DbMileage>(mileage);

      var result = _dbContext.Mileage.UpsertMileage(dbMileage);

      return Mapper.Map<MileageModel>(result);
    }
  }
}
