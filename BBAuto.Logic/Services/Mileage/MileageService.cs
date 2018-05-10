using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using AutoMapper;
using BBAuto.Logic.Common;
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
    
    public MileageReport AddMileage(int carId, string value, DateTime date)
    {
      int.TryParse(value, out int count);

      var lastMileage = GetLastMileage(carId);

      if (count < lastMileage?.Count)
      {
        return new MileageReport(carId, "Значение пробега меньше, чем уже внесён в систему.", true);
      }

      var mileage = new MileageModel(carId)
      {
        Date = new DateTime(date.Year, date.Month, DateTime.DaysInMonth(date.Year, date.Month))
      };

      mileage.SetCount(value);
      Save(mileage);

      return new MileageReport(carId, "Пробег загружен", true);
    }
    
    public MileageModel GetLastMileage(int carId)
    {
      return carId == 0
        ? null
        : Mapper.Map<MileageModel>(_dbContext.Mileage.GetMileageByCarId(carId).OrderByDescending(m => m.Date).First());
    }

    public MileageModel GetMileage(int id)
    {
      return Mapper.Map<MileageModel>(_dbContext.Mileage.GetMileageById(id));
    }

    public IList<MileageModel> GetMileageByCarId(int carId)
    {
      return Mapper.Map<IList<MileageModel>>(_dbContext.Mileage.GetMileageByCarId(carId));
    }

    public DataTable ToDataTable(int carId)
    {
      var dt = CreateTable();

      if (carId == 0)
        return dt;

      var dbMileages = _dbContext.Mileage.GetMileageByCarId(carId).Where(item => item.CarId == carId).OrderByDescending(item => item.Date);
      var mileages = Mapper.Map<IList<MileageModel>>(dbMileages);

      foreach (var mileage in mileages)
        dt.Rows.Add(mileage.ToRow());

      return dt;
    }
    
    public MileageModel Save(MileageModel mileage)
    {
      var dbMileage = Mapper.Map<DbMileage>(mileage);

      var result = _dbContext.Mileage.UpsertMileage(dbMileage);

      return Mapper.Map<MileageModel>(result);
    }

    public void Delete(int mileageId)
    {
      _dbContext.Mileage.DeleteMileage(mileageId);
    }

    private static DataTable CreateTable()
    {
      var dt = new DataTable();
      dt.Columns.Add("id");
      dt.Columns.Add("Дата", typeof(DateTime));
      dt.Columns.Add("Пробег", typeof(int));

      return dt;
    }
  }
}
