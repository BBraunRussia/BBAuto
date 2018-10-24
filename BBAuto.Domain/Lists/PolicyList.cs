using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using BBAuto.Domain.Abstract;
using BBAuto.Domain.Entities;
using BBAuto.Domain.ForCar;
using BBAuto.Domain.Services.Comp;
using BBAuto.Domain.Static;

namespace BBAuto.Domain.Lists
{
  public class PolicyList : MainList
  {
    private readonly List<Policy> _list;
    private static PolicyList _uniqueInstance;

    private PolicyList()
    {
      _list = new List<Policy>();

      loadFromSql();
    }

    public static PolicyList getInstance()
    {
      return _uniqueInstance ?? (_uniqueInstance = new PolicyList());
    }

    protected override void loadFromSql()
    {
      var dt = _provider.Select("Policy");

      clearList();

      foreach (DataRow row in dt.Rows)
      {
        Add(new Policy(row));
      }
    }

    public void Add(Policy policy)
    {
      if (_list.Exists(item => item == policy))
        return;

      _list.Add(policy);
    }

    private void clearList()
    {
      if (_list.Count > 0)
        _list.Clear();
    }

    public Policy getItem(int id)
    {
      return _list.FirstOrDefault(p => p.ID == id);
    }

    public Policy getItem(Car car, PolicyType policyType)
    {
      var policyList = from policy in _list
        where policy.Car.ID == car.ID && policy.Type == policyType
        orderby policy.DateEnd descending
        select policy;

      return policyList.Any() ? policyList.First() : car.CreatePolicy();
    }

    internal DataTable ToDataTable()
    {
      var policies = _list.Where(item => !item.IsCarSaleWithDate).OrderByDescending(item => item.DateEnd);

      return createTable(policies.ToList());
    }

    public DataTable ToDataTable(Car car)
    {
      var policies = from policy in _list
        where policy.Car.ID == car.ID
        orderby policy.DateEnd descending
        select policy;

      return createTable(policies);
    }

    public DataTable ToDataTable(Account account)
    {
      return createTable(_list.Where(p => p.IsInList(account)).OrderByDescending(p => p.DateEnd));
    }

    public DataTable ToDataTable(PolicyType policyType, string idOwner, int paymentNumber)
    {
      List<Policy> policies = new List<Policy>();

      policies = (from policy in _list
        where !policy.IsCarSale && policy.Type == policyType
              && policy.IdOwner == idOwner && !policy.IsHaveAccountID(paymentNumber) && policy.IsActual()
        orderby policy.DateEnd descending
        select policy).ToList();

      return createTable(policies.ToList());
    }

    public double GetPaymentSum(Account account)
    {
      return _list.Where(p => p.IsInList(account)).Sum(p => GetSum(p, account));
    }

    private static double GetSum(Policy policy, Account account)
    {
      return account.IsPolicyKaskoAndPayment2() ? policy.Pay2 : policy.Pay;
    }

    private DataTable createTable(IEnumerable<Policy> policies)
    {
      DataTable dt = new DataTable();
      dt.Columns.Add("id");
      dt.Columns.Add("idCar");
      dt.Columns.Add("Бортовой номер");
      dt.Columns.Add("Регистрационный знак");
      dt.Columns.Add("Тип полиса");
      dt.Columns.Add("Страхователь");
      dt.Columns.Add("Страховщик");
      dt.Columns.Add("Номер полиса");
      dt.Columns.Add("Pay", typeof(double));
      dt.Columns.Add("Начало действия", typeof(DateTime));
      dt.Columns.Add("Окончание действия", typeof(DateTime));
      dt.Columns.Add("LimitCost", typeof(double));
      dt.Columns.Add("Pay2", typeof(double));

      ICompService compService = new CompService();
      var compList = compService.GetCompList();

      policies.ToList().ForEach(item => dt.Rows.Add(item.CreateRow(compList)));

      return dt;
    }

    public void Delete(int idPolicy)
    {
      Policy police = getItem(idPolicy);

      _list.Remove(police);

      police.Delete();
    }

    public IEnumerable<Policy> GetPolicyEnds()
    {
      IEnumerable<Policy> policyList = GetPolicyList(DateTime.Today.AddMonths(1));

      return policyList.Where(item => !item.IsNotificationSent);
    }

    /*
    public IEnumerable<Policy> GetPolicyAccount()
    {
        return list.Where(p => p.DateCreate == DateTime.Today.AddDays(-1) && !p.IsAgreed(1));
    }
    */
    public List<Policy> GetPolicyList(DateTime date)
    {
      return _list.Where(police => (police.DateEnd.Month == date.Month && police.DateEnd.Year == date.Year &&
                                   !police.IsCarSale)).ToList();
    }

    public List<Car> GetCarListByPolicyList(List<Policy> list)
    {
      return list.OrderBy(policy => policy.Car.Grz).Select(policy => policy.Car).Distinct().ToList();
    }
  }
}
