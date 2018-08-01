using System;
using System.Data;
using BBAuto.Domain.Abstract;
using BBAuto.Domain.Common;
using BBAuto.Domain.Lists;
using BBAuto.Domain.Entities;

namespace BBAuto.Domain.ForDriver
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
      ID = 0;
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
      ID = id;

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
      _fileBegin = File;
    }

    public override void Save()
    {
      DeleteFile(File);

      File = WorkWithFiles.fileCopyByID(File, "drivers", Driver.ID, "Passports", _number);

      int id;
      int.TryParse(
        _provider.Insert("Passport", ID, Driver.ID, LastName, FirstName, SecondName, Number, GiveOrg, GiveDate, Address,
          File), out id);
      ID = id;

      PassportList.getInstance().Add(this);
    }

    internal override object[] getRow()
    {
      return new object[3] {ID, Number, GiveDate.ToShortDateString()};
    }

    internal override void Delete()
    {
      DeleteFile(File);

      _provider.Delete("Passport", ID);
    }

    public override string ToString()
    {
      return (Driver == null)
        ? "нет данных"
        : string.Concat("номер ", Number, "  выдан ", GiveDate.ToShortDateString());
    }
  }
}
