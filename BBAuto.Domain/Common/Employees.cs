using System.Data;
using BBAuto.Domain.Tables;
using BBAuto.Domain.Abstract;
using BBAuto.Domain.Lists;
using BBAuto.Domain.Dictionary;
using BBAuto.Domain.Entities;
using BBAuto.Domain.Static;

namespace BBAuto.Domain.Common
{
  public class Employees : MainDictionary
  {
    public int EmployeesNameId { get; set; }
    public int DriverId { get; set; }
    public Region Region { get; set; }

    public string EmployeeName => EmployeesNames.getInstance().getItem(EmployeesNameId);
    public string Name => DriverList.getInstance().getItem(DriverId).Name;
    
    public Driver Driver => DriverList.getInstance().getItem(DriverId);

    public Employees()
    {
      ID = 0;
    }

    public Employees(DataRow row)
    {
      FillFields(row);
    }

    private void FillFields(DataRow row)
    {
      int.TryParse(row.ItemArray[0].ToString(), out int idRegion);
      Region = RegionList.getInstance().getItem(idRegion);

      if (int.TryParse(row.ItemArray[1].ToString(), out int employeesNameId))
        EmployeesNameId = employeesNameId;

      if (int.TryParse(row.ItemArray[2].ToString(), out int driverId))
        DriverId = driverId;
    }

    internal override void Delete()
    {
      _provider.DoOther("exec Employees_Delete @p1, @p2", ID, EmployeesNameId);
    }

    internal override object[] getRow()
    {
      return new object[] {Region.ID, EmployeesNameId, Region.Name, EmployeeName, Driver.FullName};
    }

    public override void Save()
    {
      _provider.Insert("Employees", Region.ID, EmployeesNameId, DriverId);
    }
  }
}
