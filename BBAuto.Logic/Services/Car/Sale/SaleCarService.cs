using System.Collections.Generic;
using AutoMapper;
using BBAuto.Repositories;

namespace BBAuto.Logic.Services.Car.Sale
{
  public class SaleCarService : ISaleCarService
  {
    private readonly IDbContext _dbContext;

    public SaleCarService(IDbContext dbContext)
    {
      _dbContext = dbContext;
    }

    public IList<SaleCarModel> GetCars()
    {
      var dbSaleCars = _dbContext.SaleCar.GetSaleCars();
      return Mapper.Map<IList<SaleCarModel>>(dbSaleCars);
    }
  }
}
