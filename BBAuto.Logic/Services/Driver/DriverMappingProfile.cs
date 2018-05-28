using AutoMapper;
using BBAuto.Logic.Services.Driver.DriverCar;
using BBAuto.Repositories.Entities;

namespace BBAuto.Logic.Services.Driver
{
  public class DriverMappingProfile : Profile
  {
    public DriverMappingProfile()
    {
      CreateMap<DbDriver, DriverModel>().ReverseMap();
      CreateMap<DbDriverCar, DriverCarModel>().ReverseMap();
    }
  }
}
