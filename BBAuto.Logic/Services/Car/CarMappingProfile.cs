using AutoMapper;
using BBAuto.Logic.Services.Car.Sale;
using BBAuto.Repositories.Entities;

namespace BBAuto.Logic.Services.Car
{
  public class CarMappingProfile : Profile
  {
    public CarMappingProfile()
    {
      CreateMap<DbCar, CarModel>().ReverseMap();
      CreateMap<DbSaleCar, SaleCarModel>().ReverseMap();
    }
  }
}
