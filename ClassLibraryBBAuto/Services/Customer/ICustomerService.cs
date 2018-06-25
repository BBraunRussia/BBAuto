using System.Collections.Generic;

namespace BBAuto.Domain.Services.Customer
{
  public interface ICustomerService
  {
    IList<CustomerModel> GetCustomerList();

    CustomerModel SaveCustomer(CustomerModel customer);
    void DeleteCustomer(int id);
    CustomerModel GetCustomerById(int id);
  }
}
