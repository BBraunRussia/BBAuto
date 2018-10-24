using AutoMapper;
using BBAuto.Repository.Models;

namespace BBAuto.Domain.Services.Transponder
{
  public class TransponderMappingProfile : Profile
  {
    public TransponderMappingProfile()
    {
      CreateMap<DbTransponder, Transponder>().ReverseMap();
      CreateMap<DbReportTransponder, ReportTransponder>();
    }
  }
}
