using AutoMapper;
using BBAuto.Repositories.Entities;

namespace BBAuto.Logic.Services.Policy
{
  public class PolicyMappingProfile : Profile
  {
    public PolicyMappingProfile()
    {
      CreateMap<DbPolicy, PolicyModel>().ReverseMap();
    }
  }
}
