using BBAuto.Domain.Abstract;
using BBAuto.Domain.Entities;
using BBAuto.Domain.ForCar;
using BBAuto.Domain.Static;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BBAuto.Domain.Lists
{
  public class PolicyList : MainList
  {
    private List<Policy> list;
    private static PolicyList uniqueInstance;

    private PolicyList()
    {
      list = new List<Policy>();

      loadFromSql();
    }

    public static PolicyList getInstance()
    {
      if (uniqueInstance == null)
        uniqueInstance = new PolicyList();

      return uniqueInstance;
    }

    protected override void loadFromSql()
    {
      DataTable dt = _provider.Select("Policy");

      clearList();

      foreach (DataRow row in dt.Rows)
      {
        Policy policy = new Policy(row);

        Add(policy);
      }
    }

    public void Add(Policy policy)
    {
      if (list.Exists(item => item == policy))
        return;

      list.Add(policy);
    }

    private void clearList()
    {
      if (list.Count > 0)
        list.Clear();
    }

    public Policy getItem(int id)
    {
      return list.FirstOrDefault(p => p.ID == id);
    }

    public Policy getItem(Car car, PolicyType policyType)
    {
      var policyList = from policy in list
        where policy.Car.ID == car.ID && policy.Type == policyType
        orderby policy.DateEnd descending
        select policy;

      return (policyList.Count() > 0) ? policyList.First() : car.CreatePolicy();
    }

    internal DataTable ToDataTable()
    {
      var policies = list.Where(item => !item.IsCarSaleWithDate).OrderByDescending(item => item.DateEnd);

      return createTable(policies.ToList());
    }

    public DataTable ToDataTable(Car car)
    {
      var policies = from policy in list
        where policy.Car.ID == car.ID
        orderby policy.DateEnd descending
        select policy;

      return createTable(policies);
    }

    public DataTable ToDataTable(Account account)
    {
      return createTable(list.Where(p => p.IsInList(account)).OrderByDescending(p => p.DateEnd));
    }

    public DataTable ToDataTable(PolicyType policyType, string idOwner, int paymentNumber)
    {
      List<Policy> policies = new List<Policy>();

      policies = (from policy in list
        where !policy.IsCarSale && policy.Type == policyType
              && policy.IdOwner == idOwner && !policy.IsHaveAccountID(paymentNumber) && policy.IsActual()
        orderby policy.DateEnd descending
        select policy).ToList();

      return createTable(policies.ToList());
    }

    public double GetPaymentSum(Account account)
    {
      return list.Where(p => p.IsInList(account)).Sum(p => GetSum(p, account));
    }

    private double GetSum(Policy policy, Account account)
    {
      return (account.IsPolicyKaskoAndPayment2()) ? policy.Pay2ToDouble : policy.PayToDouble;
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
      dt.Columns.Add("Pay", Type.GetType("System.Double"));
      dt.Columns.Add("Начало действия", Type.GetType("System.DateTime"));
      dt.Columns.Add("Окончание действия", Type.GetType("System.DateTime"));
      dt.Columns.Add("LimitCost", Type.GetType("System.Double"));
      dt.Columns.Add("Pay2", Type.GetType("System.Double"));

      policies.ToList().ForEach(item => dt.Rows.Add(item.getRow()));

      return dt;
    }

    public void Delete(int idPolicy)
    {
      Policy police = getItem(idPolicy);

      list.Remove(police);

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
      return list.Where(police => (police.DateEnd.Month == date.Month && police.DateEnd.Year == date.Year &&
                                   !police.IsCarSale)).ToList();
    }

    public List<Car> GetCarListByPolicyList(List<Policy> list)
    {
      return list.OrderBy(policy => policy.Car.Grz).Select(policy => policy.Car).Distinct().ToList();
    }
  }
}
