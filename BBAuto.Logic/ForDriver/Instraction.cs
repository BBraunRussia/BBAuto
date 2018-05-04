using System;
using System.Data;
using BBAuto.Logic.Abstract;
using BBAuto.Logic.Common;
using BBAuto.Logic.Entities;
using BBAuto.Logic.Lists;

namespace BBAuto.Logic.ForDriver
{
  public class Instraction : MainDictionary
  {
    private DateTime date;

    public string Name { get; set; }
    public string File { get; set; }
    public Driver Driver { get; private set; }

    public string Date
    {
      get => date.ToShortDateString();
      set
      {
        if (!DateTime.TryParse(value, out date))
          date = DateTime.Today.Date;
      }
    }

    public Instraction(Driver driver)
    {
      Driver = driver;
      Date = "";
      Id = 0;
    }

    public Instraction(DataRow row)
    {
      fillFields(row);
    }

    private void fillFields(DataRow row)
    {
      Id = Convert.ToInt32(row.ItemArray[0]);
      Name = row.ItemArray[1].ToString();
      Date = row.ItemArray[2].ToString();

      int idDriver;
      int.TryParse(row.ItemArray[3].ToString(), out idDriver);
      Driver = DriverList.getInstance().getItem(idDriver);

      File = row.ItemArray[4].ToString();
      FileBegin = File;
    }

    internal override void Delete()
    {
      DeleteFile(File);

      Provider.Delete("Instraction", Id);
    }

    public override void Save()
    {
      DeleteFile(File);

      File = WorkWithFiles.FileCopyById(File, "drivers", Driver.Id, "Instraction", Name);

      Id = Convert.ToInt32(Provider.Insert("Instraction", Id, Driver.Id, Name, date, File));

      InstractionList instractionList = InstractionList.getInstance();
      instractionList.Add(this);
    }

    internal override object[] ToRow()
    {
      return new object[] {Id, Name, Date};
    }

    public override string ToString()
    {
      return (Driver == null) ? "нет данных" : string.Concat("№", Name, " дата ", Date);
    }
  }
}
