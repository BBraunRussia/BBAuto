using System.Collections.Generic;
using System.Data;
using System.Linq;
using BBAuto.Logic.Abstract;
using BBAuto.Logic.Entities;
using BBAuto.Logic.ForDriver;

namespace BBAuto.Logic.Lists
{
  public class MedicalCertList : MainList, INotificationList
  {
    private static MedicalCertList _uniqueInstance;
    private List<MedicalCert> list;

    private MedicalCertList()
    {
      list = new List<MedicalCert>();

      LoadFromSql();
    }

    public static MedicalCertList getInstance()
    {
      if (_uniqueInstance == null)
        _uniqueInstance = new MedicalCertList();

      return _uniqueInstance;
    }

    protected override void LoadFromSql()
    {
      DataTable dt = Provider.Select("MedicalCert");

      foreach (DataRow row in dt.Rows)
      {
        MedicalCert medicalCert = new MedicalCert(row);
        Add(medicalCert);
      }
    }

    public void Add(MedicalCert medicalCert)
    {
      if (list.Exists(item => item.Id == medicalCert.Id))
        return;

      list.Add(medicalCert);
    }

    public DataTable ToDataTable()
    {
      return CreateTable(list);
    }

    public DataTable ToDataTable(Driver driver)
    {
      var medicalCerts = from medicalCert in list
        where medicalCert.Driver.Id == driver.Id
        orderby medicalCert.DateEnd descending
        select medicalCert;

      return CreateTable(medicalCerts);
    }

    private DataTable CreateTable(IEnumerable<MedicalCert> medicalCerts)
    {
      DataTable dt = new DataTable();
      dt.Columns.Add("id");
      dt.Columns.Add("Номер");
      dt.Columns.Add("Дата окончания действия");

      foreach (MedicalCert medicalCert in medicalCerts)
        dt.Rows.Add(medicalCert.ToRow());

      return dt;
    }

    public MedicalCert getItem(int id)
    {
      return list.FirstOrDefault(mc => mc.Id == id);
    }

    public MedicalCert getItemByDriverId(int driverId)
    {
      return list.Where(m => m.Driver.Id == driverId).OrderByDescending(m => m.DateEnd).FirstOrDefault();
    }

    public void Delete(int idMedicalCert)
    {
      MedicalCert medicalCert = getItem(idMedicalCert);

      list.Remove(medicalCert);

      medicalCert.Delete();
    }

    public List<INotification> ToList()
    {
      DriverList driverList = DriverList.getInstance();
      IEnumerable<MedicalCert> listNew =
        list.Where(item => !driverList.getItem(item.Driver.Id).Fired && !driverList.getItem(item.Driver.Id).Decret &&
                           driverList.getItem(item.Driver.Id).IsDriver);

      var listNotification = new List<INotification>();

      foreach (INotification item in listNew)
        listNotification.Add(item);

      return listNotification;
    }
  }
}
