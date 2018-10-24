using AutoMapper;
using BBAuto.Repository.Models;

namespace BBAuto.Domain.Services.CarSale
{
  public class CarSaleMappingProfile : Profile
  {
    public CarSaleMappingProfile()
    {
      CreateMap<DbCarSale, CarSale>().ReverseMap();
    }
  }
}
