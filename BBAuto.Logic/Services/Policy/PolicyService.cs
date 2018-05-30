using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using BBAuto.Logic.Services.Account;
using BBAuto.Logic.Services.Car;
using BBAuto.Logic.Services.Car.Sale;
using BBAuto.Repositories;

namespace BBAuto.Logic.Services.Policy
{
  public class PolicyService : IPolicyService
  {
    private readonly IDbContext _dbContext;
    private readonly ISaleCarService _saleCarService;
    private readonly ICarService _carService;

    public PolicyService(
      IDbContext dbContext,
      ISaleCarService saleCarService,
      ICarService carService)
    {
      _dbContext = dbContext;
      _saleCarService = saleCarService;
      _carService = carService;
    }

    public IList<PolicyModel> GetPolicyListByAccountId(int accountId)
    {
      var dbPolicy = _dbContext.Policy.GetPolicyListByAccountId(accountId);

      return Mapper.Map<IList<PolicyModel>>(dbPolicy);
    }

    public decimal GetSumByAccountId(AccountModel account)
    {
      var policyList = GetPolicyListByAccountId(account.Id);

      return policyList.Sum(policy => GetSum(policy, account));
    }

    public IList<PolicyModel> GetPolicyList(DateTime date)
    {
      var carSaleList = _saleCarService.GetSaleCars().Select(carSale => carSale.Id);

      var dbPolicyList = _dbContext.Policy.GetPolicys();

      var list = Mapper.Map<IList<PolicyModel>>(dbPolicyList);

      return list.Where(policy => policy.DateEnd.Month == date.Month && policy.DateEnd.Year == date.Year
                                  && !carSaleList.Contains(policy.CarId)).ToList();
    }

    public IList<PolicyModel> GetPolicyEnds()
    {
      var policyList = GetPolicyList(DateTime.Today.AddMonths(1));

      return policyList.Where(policy => !policy.NotificationSent).ToList();
    }

    public string GetPolicyToMail(PolicyModel policy)
    {
      policy.NotificationSent = true;
      Save(policy);

      var car = _carService.GetCarById(policy.CarId);

      var sb = new StringBuilder();
      sb.Append(car.Grz);
      sb.Append(" ");
      sb.Append(policy.PolicyType);
      sb.Append(" ");
      sb.Append(policy.Number);
      sb.Append(" ");
      sb.Append(policy.DateEnd.ToShortDateString());
      return sb.ToString();
    }

    public PolicyModel Save(PolicyModel policy)
    {
      var dbModel = Mapper.Map<PolicyModel>(policy);

      return Mapper.Map<PolicyModel>(dbModel);
    }

    private static decimal GetSum(PolicyModel policy, AccountModel account)
    {
      return account.IsPolicyKaskoAndPayment2() 
        ? policy.Pay2 ?? 0 
        : policy.Pay1 ?? 0;
    }
  }
}
