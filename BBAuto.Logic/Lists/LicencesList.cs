using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using BBAuto.Logic.Abstract;
using BBAuto.Logic.Entities;
using BBAuto.Logic.ForDriver;

namespace BBAuto.Logic.Lists
{
  public class LicenseList : MainList, INotificationList
  {
    private List<DriverLicense> _list;
    private static LicenseList _uniqueInstance;

    private LicenseList()
    {
      _list = new List<DriverLicense>();

      LoadFromSql();
    }

    public static LicenseList getInstance()
    {
      if (_uniqueInstance == null)
        _uniqueInstance = new LicenseList();

      return _uniqueInstance;
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

    public void Add(DriverLicense driverLicense)
    {
      if (_list.Exists(item => item == driverLicense))
        return;

      _list.Add(driverLicense);
    }

    public DataTable ToDataTable(Driver driver)
    {
      var driverLicenses = _list.Where(item => item.Driver.Id == driver.Id).ToList();

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
        dt.Rows.Add(driverLicense.ToRow());

      return dt;
    }

    public DriverLicense getItem(int id)
    {
      return _list.FirstOrDefault(l => l.Id == id);
    }

    public DriverLicense getItemByDriverId(int driverId)
    {
      var driverLicenses = _list.Where(item => item.Driver.Id == driverId).ToList();

      driverLicenses.Sort(Compare);

      return driverLicenses.FirstOrDefault();
    }

    public List<INotification> ToList()
    {
      DriverList driverList = DriverList.getInstance();
      List<DriverLicense> listNew = _list
        .Where(item => !driverList.getItem(item.Driver.Id).Fired && !driverList.getItem(item.Driver.Id).Decret &&
                       driverList.getItem(item.Driver.Id).IsDriver).ToList();

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
