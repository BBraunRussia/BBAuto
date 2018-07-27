using AutoMapper;
using BBAuto.Repository.Models;

namespace BBAuto.Domain.Services.DriverTransponder
{
  public class DriverTransponderMappingProfile : Profile
  {
    public DriverTransponderMappingProfile()
    {
      CreateMap<DbDriverTransponder, DriverTransponder>().ReverseMap();
    }
  }
}
