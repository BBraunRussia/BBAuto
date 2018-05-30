using AutoMapper;
using BBAuto.Repositories.Entities;

namespace BBAuto.Logic.Services.Model
{
  public class ModelMappingProfile : Profile
  {
    public ModelMappingProfile()
    {
      CreateMap<DbModel, ModelModel>().ReverseMap();
    }
  }
}
