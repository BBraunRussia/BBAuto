using AutoMapper;
using BBAuto.Repositories.Entities;

namespace BBAuto.Logic.Services.Dictionary
{
  public class DictionaryMappingProfile : Profile
  {
    public DictionaryMappingProfile()
    {
      CreateMap<DbDictionary, DictionaryModel>().ReverseMap();
    }
  }
}