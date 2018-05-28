using AutoMapper;
using BBAuto.Repositories.Entities;

namespace BBAuto.Logic.Services.License
{
  public class LicenseMappingProfile : Profile
  {
    public LicenseMappingProfile()
    {
      CreateMap<DbLicense, LicenseModel>().ReverseMap();
    }
  }
}
