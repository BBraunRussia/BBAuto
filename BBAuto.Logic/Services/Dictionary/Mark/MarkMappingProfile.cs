using AutoMapper;
using BBAuto.Repositories.Entities;

namespace BBAuto.Logic.Services.Dictionary.Mark
{
  public class MarkMappingProfile : Profile
  {
    public MarkMappingProfile()
    {
      CreateMap<DbMark, MarkModel>().ReverseMap();
    }
  }
}
