using AutoMapper;
using BBAuto.Repository.Models;

namespace BBAuto.Domain.Services.Customer
{
  public class CustomerMappingProfile : Profile
  {
    public CustomerMappingProfile()
    {
      CreateMap<DbCustomer, CustomerModel>().ReverseMap();
    }
  }
}
