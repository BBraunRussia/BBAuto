using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using BBAuto.Domain.Abstract;
using BBAuto.Domain.ForDriver;
using BBAuto.Domain.Entities;

namespace BBAuto.Domain.Lists
{
  public class LicenseList : MainList<DriverLicense>, INotificationList
  {
    private static LicenseList _uniqueInstance;

    public static LicenseList getInstance()
    {
      return _uniqueInstance ?? (_uniqueInstance = new LicenseList());
    }

    protected override void LoadFromSql()
    {
      DataTable dt = Provider.Select("DriverLicense");

      foreach (DataRow row in dt.Rows)
      {
        DriverLicense driverLicense = new DriverLicense(row);
        Add(driverLicense);
      }
    }
    
    public DataTable ToDataTable(Driver driver)
    {
      var driverLicenses = _list.Where(item => item.Driver.ID == driver.ID).ToList();

      driverLicenses.Sort(Compare);

      return CreateTable(driverLicenses);
    }

    private DataTable CreateTable(IEnumerable<DriverLicense> driverLicenses)
    {
      DataTable dt = new DataTable();
      dt.Columns.Add("id");
      dt.Columns.Add("Номер");
      dt.Columns.Add("Дата окончания действия");

      foreach (DriverLicense driverLicense in driverLicenses)
        dt.Rows.Add(driverLicense.getRow());

      return dt;
    }

    public DriverLicense getItem(int id)
    {
      return _list.FirstOrDefault(l => l.ID == id);
    }

    public DriverLicense getItem(Driver driver)
    {
      var driverLicenses = _list.Where(item => item.Driver.ID == driver.ID).ToList();

      driverLicenses.Sort(Compare);

      return driverLicenses.FirstOrDefault();
    }

    public List<INotification> ToList()
    {
      DriverList driverList = DriverList.getInstance();
      List<DriverLicense> listNew = _list
        .Where(item => !driverList.getItem(item.Driver.ID).Fired && !driverList.getItem(item.Driver.ID).Decret &&
                       driverList.getItem(item.Driver.ID).IsDriver).ToList();

      List<INotification> listNotification = new List<INotification>();
      foreach (INotification item in listNew)
        listNotification.Add(item);

      return listNotification;
    }

    public void Delete(int idLicence)
    {
      DriverLicense licence = getItem(idLicence);

      _list.Remove(licence);

      licence.Delete();
    }

    private int Compare(DriverLicense license1, DriverLicense license2)
    {
      if (license1.DateEnd == license2.DateEnd)
        return DateTime.Compare(license1.DateBegin, license2.DateBegin) * -1;

      return DateTime.Compare(license1.DateEnd, license2.DateEnd) * -1;
    }
  }
}
