using AutoMapper;
using BBAuto.Repositories.Entities;

namespace BBAuto.Logic.Services.Mileage
{
  public class MileageMappingProfile : Profile
  {
    public MileageMappingProfile()
    {
      CreateMap<DbMileage, MileageModel>().ReverseMap();
    }
  }
}
