using System.Collections.Generic;

namespace BBAuto.Domain.Services.Customer
{
  public interface ICustomerService
  {
    IList<Customer> GetCustomerList();

    Customer SaveCustomer(Customer customer);
    void DeleteCustomer(int id);
    Customer GetCustomerById(int id);
  }
}
