using System.Collections.Generic;
using AutoMapper;
using BBAuto.Repositories;
using BBAuto.Repositories.Entities;

namespace BBAuto.Logic.Services.Car.Sale
{
  public class SaleCarService : ISaleCarService
  {
    private readonly IDbContext _dbContext;

    public SaleCarService(IDbContext dbContext)
    {
      _dbContext = dbContext;
    }

    public IList<SaleCarModel> GetSaleCars()
    {
      var dbSaleCars = _dbContext.SaleCar.GetSaleCars();
      return Mapper.Map<IList<SaleCarModel>>(dbSaleCars);
    }

    public void Save(SaleCarModel saleCar)
    {
      var dbModel = Mapper.Map<DbSaleCar>(saleCar);
      _dbContext.SaleCar.UpsertSaleCar(dbModel);
    }
  }
}
