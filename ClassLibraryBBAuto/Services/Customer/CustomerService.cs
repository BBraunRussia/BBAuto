using System;
using System.Collections.Generic;
using System.Windows.Forms;
using AutoMapper;
using BBAuto.Repository;
using BBAuto.Repository.Models;

namespace BBAuto.Domain.Services.Customer
{
  public class CustomerService : ICustomerService
  {
    private readonly IDbContext _dbContext;

    public CustomerService()
    {
      _dbContext = new DbContext();
    }

    public IList<Customer> GetCustomerList()
    {
      var list = _dbContext.Customer.GetCustomerList();

      return Mapper.Map<IList<Customer>>(list);
    }

    public Customer SaveCustomer(Customer customer)
    {
      var dbModel = Mapper.Map<DbCustomer>(customer);

      var result = _dbContext.Customer.UpsertCustomer(dbModel);

      return Mapper.Map<Customer>(result);
    }

    public void DeleteCustomer(int id)
    {
      if (id == 0)
        throw new NullReferenceException();

      _dbContext.Customer.DeleteCustomer(id);
    }

    public Customer GetCustomerById(int id)
    {
      var dbModel = _dbContext.Customer.GetCustomerById(id);

      return Mapper.Map<Customer>(dbModel);
    }
  }
}
