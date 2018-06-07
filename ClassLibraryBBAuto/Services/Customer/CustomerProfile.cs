using AutoMapper;
using BBAuto.Repository.Models;

namespace BBAuto.Domain.Services.Customer
{
  public class CustomerProfile : Profile
  {
    public CustomerProfile()
    {
      CreateMap<DbCustomer, Customer>().ReverseMap();
    }
  }
}
