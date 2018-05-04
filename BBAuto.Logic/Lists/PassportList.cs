using System.Collections.Generic;
using System.Data;
using System.Linq;
using BBAuto.Logic.Abstract;
using BBAuto.Logic.Entities;
using BBAuto.Logic.ForDriver;

namespace BBAuto.Logic.Lists
{
  public class PassportList : MainList
  {
    private static PassportList uniqueInstance;
    private List<Passport> list;

    private PassportList()
    {
      list = new List<Passport>();

      LoadFromSql();
    }

    public static PassportList getInstance()
    {
      if (uniqueInstance == null)
        uniqueInstance = new PassportList();

      return uniqueInstance;
    }

    protected override void LoadFromSql()
    {
      DataTable dt = Provider.Select("Passport");

      foreach (DataRow row in dt.Rows)
      {
        Passport passport = new Passport(row);
        Add(passport);
      }
    }

    public void Add(Passport passport)
    {
      if (list.Exists(item => item == passport))
        return;

      list.Add(passport);
    }

    public void Delete(int idPassport)
    {
      Passport passport = getPassport(idPassport);

      list.Remove(passport);

      passport.Delete();
    }

    public Passport getPassport(int id)
    {
      return list.FirstOrDefault(p => p.Id == id);
    }

    public DataTable ToDataTable(Driver driver)
    {
      DataTable dt = createTable();

      foreach (Passport passport in list.Where(p => p.Driver.Id == driver.Id))
        dt.Rows.Add(passport.ToRow());

      return dt;
    }

    private DataTable createTable()
    {
      DataTable dt = new DataTable();
      dt.Columns.Add("id");
      dt.Columns.Add("Номер");
      dt.Columns.Add("Дата выдачи");

      return dt;
    }

    public Passport getLastPassport(Driver driver)
    {
      var passports = list.Where(item => item.Driver.Id == driver.Id).OrderByDescending(item => item.GiveDate);

      return passports.FirstOrDefault();
    }

    public Passport GetPassport(Driver driver, string number)
    {
      var newList = list.Where(item => item.Number.Replace(" ", "") == number.Replace(" ", "")).ToList();

      return (newList.Count == 0) ? driver.createPassport() : newList.First();
    }
  }
}
