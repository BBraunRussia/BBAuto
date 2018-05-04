using System;
using System.Data;
using BBAuto.Logic.Abstract;
using BBAuto.Logic.Common;
using BBAuto.Logic.Entities;
using BBAuto.Logic.Lists;

namespace BBAuto.Logic.ForDriver
{
  public class Passport : MainDictionary
  {
    private string _number;

    public string LastName { get; set; }
    public string FirstName { get; set; }
    public string SecondName { get; set; }
    public string GiveOrg { get; set; }
    public string Address { get; set; }
    public string File { get; set; }
    public DateTime GiveDate { get; set; }
    public Driver Driver { get; private set; }

    public string Number
    {
      get
      {
        return (_number.Length == 10)
          ? _number.Substring(0, 4) + " " + _number.Substring(4, 6)
          : (_number.Length == 9)
            ? _number.Substring(0, 2) + " " + _number.Substring(2, 7)
            : _number;
      }
      set { _number = value.Replace(" ", ""); }
    }

    public Passport(Driver driver)
    {
      Id = 0;
      Driver = driver;
      GiveDate = DateTime.Today;
      _number = string.Empty;
    }

    public Passport(DataRow row)
    {
      fillFields(row);
    }

    private void fillFields(DataRow row)
    {
      int id;
      int.TryParse(row.ItemArray[0].ToString(), out id);
      Id = id;

      int idDriver;
      int.TryParse(row.ItemArray[1].ToString(), out idDriver);
      Driver = DriverList.getInstance().getItem(idDriver);

      LastName = row.ItemArray[2].ToString();
      FirstName = row.ItemArray[3].ToString();
      SecondName = row.ItemArray[4].ToString();
      _number = row.ItemArray[5].ToString();
      GiveOrg = row.ItemArray[6].ToString();

      DateTime giveDate;
      DateTime.TryParse(row.ItemArray[7].ToString(), out giveDate);
      GiveDate = giveDate;

      Address = row.ItemArray[8].ToString();
      File = row.ItemArray[9].ToString();
      FileBegin = File;
    }

    public override void Save()
    {
      DeleteFile(File);

      File = WorkWithFiles.FileCopyById(File, "drivers", Driver.Id, "Passports", _number);

      int id;
      int.TryParse(
        Provider.Insert("Passport", Id, Driver.Id, LastName, FirstName, SecondName, Number, GiveOrg, GiveDate, Address,
          File), out id);
      Id = id;

      PassportList.getInstance().Add(this);
    }

    internal override object[] ToRow()
    {
      return new object[3] {Id, Number, GiveDate.ToShortDateString()};
    }

    internal override void Delete()
    {
      DeleteFile(File);

      Provider.Delete("Passport", Id);
    }

    public override string ToString()
    {
      return (Driver == null)
        ? "нет данных"
        : string.Concat("номер ", Number, "  выдан ", GiveDate.ToShortDateString());
    }
  }
}
