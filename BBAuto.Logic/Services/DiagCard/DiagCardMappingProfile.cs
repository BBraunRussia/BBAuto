using AutoMapper;
using BBAuto.Repositories.Entities;

namespace BBAuto.Logic.Services.DiagCard
{
  public class DiagCardMappingProfile : Profile
  {
    public DiagCardMappingProfile()
    {
      CreateMap<DbDiagCard, DiagCardModel>().ReverseMap();
    }
  }
}
