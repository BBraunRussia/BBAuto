using System.Data;
using BBAuto.Logic.Abstract;
using BBAuto.Logic.Dictionary;
using BBAuto.Logic.Entities;
using BBAuto.Logic.Lists;
using BBAuto.Logic.Static;
using BBAuto.Logic.Tables;

namespace BBAuto.Logic.Common
{
  public class Employees : MainDictionary
  {
    private int _idEmployeesName;
    private int _idDriver;

    public string IdEmployeesName
    {
      get => _idEmployeesName.ToString();
      set => int.TryParse(value, out _idEmployeesName);
    }

    public string IdDriver
    {
      get => _idDriver.ToString();
      set => int.TryParse(value, out _idDriver);
    }

    public string Name
    {
      get
      {
        var driverList = DriverList.getInstance();
        var driver = driverList.getItem(_idDriver);

        return driver.GetName(NameType.Short);
      }
    }

    public string DriverName
    {
      get
      {
        var driverList = DriverList.getInstance();
        var driver = driverList.getItem(_idDriver);
        return driver.GetName(NameType.Full);
      }
    }

    public string EmployeeName
    {
      get
      {
        var employeesNames = EmployeesNames.getInstance();
        return employeesNames.getItem(_idEmployeesName);
      }
    }

    public Region Region { get; set; }

    public Employees()
    {
      Id = 0;
    }

    public Employees(DataRow row)
    {
      FillFields(row);
    }

    private void FillFields(DataRow row)
    {
      int.TryParse(row.ItemArray[0].ToString(), out int idRegion);
      var regionList = RegionList.getInstance();
      Region = regionList.getItem(idRegion);

      int.TryParse(row.ItemArray[1].ToString(), out _idEmployeesName);
      int.TryParse(row.ItemArray[2].ToString(), out _idDriver);
    }

    internal override void Delete()
    {
      Provider.DoOther("exec Employees_Delete @p1, @p2", Id, _idEmployeesName);
    }

    internal override object[] ToRow()
    {
      return new object[] {Region.Id, _idEmployeesName, Region.Name, EmployeeName, DriverName};
    }

    public override void Save()
    {
      Provider.Insert("Employees", Region.Id, _idEmployeesName, _idDriver);
    }
  }
}
