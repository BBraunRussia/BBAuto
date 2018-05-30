using AutoMapper;
using BBAuto.Repositories.Entities;

namespace BBAuto.Logic.Services.Account
{
  public class AccountMappingProfile : Profile
  {
    public AccountMappingProfile()
    {
      CreateMap<DbAccount, AccountModel>().ReverseMap();
    }
  }
}
