using AutoMapper;
using BBAuto.Repository.Models;

namespace BBAuto.Domain.Services.Comp
{
  public class CompMappingProfile : Profile
  {
    public CompMappingProfile()
    {
      CreateMap<DbComp, Comp>().ReverseMap();
    }
  }
}
