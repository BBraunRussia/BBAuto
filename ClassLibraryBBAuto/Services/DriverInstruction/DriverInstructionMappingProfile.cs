using AutoMapper;
using BBAuto.Repository.Models;

namespace BBAuto.Domain.Services.DriverInstruction
{
  public class DriverInstructionMappingProfile : Profile
  {
    public DriverInstructionMappingProfile()
    {
      CreateMap<DbDriverInstruction, DriverInstruction>().ReverseMap();
    }
  }
}
