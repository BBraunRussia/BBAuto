using System;
using System.Collections.Generic;
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

    public IList<CustomerModel> GetCustomerList()
    {
      var list = _dbContext.Customer.GetCustomerList();

      return Mapper.Map<IList<CustomerModel>>(list);
    }

    public CustomerModel SaveCustomer(CustomerModel customer)
    {
      var dbModel = Mapper.Map<DbCustomer>(customer);

      var result = _dbContext.Customer.UpsertCustomer(dbModel);

      return Mapper.Map<CustomerModel>(result);
    }

    public void DeleteCustomer(int id)
    {
      if (id == 0)
        throw new NullReferenceException();

      _dbContext.Customer.DeleteCustomer(id);
    }

    public CustomerModel GetCustomerById(int id)
    {
      var dbModel = _dbContext.Customer.GetCustomerById(id);

      return Mapper.Map<CustomerModel>(dbModel);
    }
  }
}
