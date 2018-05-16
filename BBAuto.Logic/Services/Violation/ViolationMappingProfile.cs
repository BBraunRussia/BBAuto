using AutoMapper;
using BBAuto.Repositories.Entities;

namespace BBAuto.Logic.Services.Violation
{
  public class ViolationMappingProfile : Profile
  {
    public ViolationMappingProfile()
    {
      CreateMap<DbViolation, ViolationModel>().ReverseMap();
    }
  }
}
