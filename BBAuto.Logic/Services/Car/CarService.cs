using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using BBAuto.Repositories;

namespace BBAuto.Logic.Services.Car
{
  public class CarService : ICarService
  {
    private readonly IDbContext _dbContext;

    public CarService(IDbContext dbContext)
    {
      _dbContext = dbContext;
    }

    public CarModel GetCarByGrz(string grz)
    {
      var list = _dbContext.Car.GetCars();

      return Mapper.Map<CarModel>(list.FirstOrDefault(c => c.Grz == grz));
    }

    public IList<CarModel> GetCars()
    {
      return Mapper.Map<IList<CarModel>>(_dbContext.Car.GetCars());
    }
  }
}
