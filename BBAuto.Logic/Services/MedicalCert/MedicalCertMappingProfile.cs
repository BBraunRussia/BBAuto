using AutoMapper;
using BBAuto.Repositories.Entities;

namespace BBAuto.Logic.Services.MedicalCert
{
  public class MedicalCertMappingProfile : Profile
  {
    public MedicalCertMappingProfile()
    {
      CreateMap<DbMedicalCert, MedicalCertModel>().ReverseMap();
    }
  }
}
