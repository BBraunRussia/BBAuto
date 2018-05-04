using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using BBAuto.Logic.Abstract;
using BBAuto.Logic.ForCar;

namespace BBAuto.Logic.Lists
{
  public class AccountList : MainList
  {
    private static AccountList _uniqueInstance;
    private List<Account> list;

    private AccountList()
    {
      list = new List<Account>();

      LoadFromSql();
    }

    public static AccountList GetInstance()
    {
      if (_uniqueInstance == null)
        _uniqueInstance = new AccountList();

      return _uniqueInstance;
    }

    protected override void LoadFromSql()
    {
      DataTable dt = Provider.Select("Account");

      foreach (DataRow row in dt.Rows)
      {
        Account account = new Account(row);
        Add(account);
      }
    }

    public void Add(Account account)
    {
      if (list.Exists(item => item == account))
        return;

      list.Add(account);
    }

    public void Delete(int idAccount)
    {
      Account account = getItem(idAccount);

      list.Remove(account);

      account.Delete();
    }

    public Account getItem(int id)
    {
      return list.FirstOrDefault(a => a.Id == id);
    }

    public DataTable ToDataTable()
    {
      var accounts = list.OrderByDescending(item => item.Id);

      return CreateTable(accounts.ToList());
    }

    private DataTable CreateTable(List<Account> accounts)
    {
      DataTable dt = new DataTable();
      dt.Columns.Add("id");
      dt.Columns.Add("idCar");
      dt.Columns.Add("Номер счёта");
      dt.Columns.Add("Тип полиса");
      dt.Columns.Add("Собственник");
      dt.Columns.Add("Сумма", Type.GetType("System.Double"));
      dt.Columns.Add("Согласование");
      dt.Columns.Add("Файл");

      foreach (Account account in accounts)
        dt.Rows.Add(account.ToRow());

      return dt;
    }

    internal bool Exists(string name)
    {
      return list.Exists(item => item.Number == name);
    }

    public IEnumerable<Account> GetAccountForAgree()
    {
      return list.Where(a => a.CanAgree());
    }
  }
}
