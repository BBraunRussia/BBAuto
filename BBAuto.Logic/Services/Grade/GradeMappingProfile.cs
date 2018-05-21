using AutoMapper;
using BBAuto.Repositories.Entities;

namespace BBAuto.Logic.Services.Grade
{
  public class GradeMappingProfile : Profile
  {
    public GradeMappingProfile()
    {
      CreateMap<DbGrade, GradeModel>().ReverseMap();
    }
  }
}
