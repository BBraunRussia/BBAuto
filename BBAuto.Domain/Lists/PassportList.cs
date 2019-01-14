using System.Linq;
using System.Data;
using BBAuto.Domain.ForDriver;
using BBAuto.Domain.Abstract;
using BBAuto.Domain.Entities;

namespace BBAuto.Domain.Lists
{
  public class PassportList : MainList<Passport>
  {
    private static PassportList _uniqueInstance;

    public static PassportList getInstance()
    {
      return _uniqueInstance ?? (_uniqueInstance = new PassportList());
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

    public void Delete(int idPassport)
    {
      Passport passport = getPassport(idPassport);

      _list.Remove(passport);

      passport.Delete();
    }

    public Passport getPassport(int id)
    {
      return _list.FirstOrDefault(p => p.ID == id);
    }

    public DataTable ToDataTable(Driver driver)
    {
      var dt = createTable();

      _list.Where(p => p.Driver.ID == driver.ID).OrderByDescending(p => p.GiveDate).ToList()
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
      var passports = _list.Where(item => item.Driver.ID == driver.ID).OrderByDescending(item => item.GiveDate);

      return passports.FirstOrDefault();
    }

    public Passport GetPassport(Driver driver, string number)
    {
      var newList = _list.Where(item => item.Number.Replace(" ", "") == number.Replace(" ", "") && item.Driver.ID == driver.ID).ToList();

      return newList.Count == 0 ? driver.createPassport() : newList.First();
    }
  }
}
