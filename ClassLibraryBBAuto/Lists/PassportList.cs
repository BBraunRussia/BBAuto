using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using BBAuto.Domain.ForDriver;
using BBAuto.Domain.Abstract;
using BBAuto.Domain.Entities;

namespace BBAuto.Domain.Lists
{
  public class PassportList : MainList
  {
    private static PassportList uniqueInstance;
    private List<Passport> list;

    private PassportList()
    {
      list = new List<Passport>();

      loadFromSql();
    }

    public static PassportList getInstance()
    {
      if (uniqueInstance == null)
        uniqueInstance = new PassportList();

      return uniqueInstance;
    }

    protected override void loadFromSql()
    {
      DataTable dt = _provider.Select("Passport");

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
      return list.FirstOrDefault(p => p.ID == id);
    }

    public DataTable ToDataTable(Driver driver)
    {
      var dt = createTable();

      list.Where(p => p.Driver.ID == driver.ID).OrderByDescending(p => p.GiveDate).ToList()
        .ForEach(passport => dt.Rows.Add(passport.getRow()));

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
      var passports = list.Where(item => item.Driver.ID == driver.ID).OrderByDescending(item => item.GiveDate);

      return passports.FirstOrDefault();
    }

    public Passport GetPassport(Driver driver, string number)
    {
      var newList = list.Where(item => item.Number.Replace(" ", "") == number.Replace(" ", "")).ToList();

      return (newList.Count == 0) ? driver.createPassport() : newList.First();
    }
  }
}
