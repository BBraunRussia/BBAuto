using BBAuto.Domain.Abstract;
using System.Collections.Generic;
using BBAuto.Domain.ForCar;
using System.Data;
using System.Linq;
using System;

namespace BBAuto.Domain.Lists
{
  public class AccountList : MainList<Account>
  {
    private static AccountList _uniqueInstance;
    
    public static AccountList GetInstance()
    {
      return _uniqueInstance ?? (_uniqueInstance = new AccountList());
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

    public void Delete(int idAccount)
    {
      Account account = getItem(idAccount);

      _list.Remove(account);

      account.Delete();
    }

    public Account getItem(int id)
    {
      return _list.FirstOrDefault(a => a.ID == id);
    }

    public DataTable ToDataTable()
    {
      var accounts = _list.OrderByDescending(item => item.ID);

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
        dt.Rows.Add(account.getRow());

      return dt;
    }

    internal bool Exists(string name)
    {
      return _list.Exists(item => item.Number == name);
    }

    public IEnumerable<Account> GetAccountForAgree()
    {
      return _list.Where(a => a.CanAgree());
    }
  }
}
