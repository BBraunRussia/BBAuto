using System.Collections.Generic;
using System.Linq;
using System.Data;
using BBAuto.Domain.Abstract;
using BBAuto.Domain.ForDriver;
using BBAuto.Domain.Entities;

namespace BBAuto.Domain.Lists
{
  public class MedicalCertList : MainList<MedicalCert>, INotificationList
  {
    private static MedicalCertList _uniqueInstance;
    
    private MedicalCertList()
    {
      _list = new List<MedicalCert>();

      LoadFromSql();
    }

    public static MedicalCertList getInstance()
    {
      return _uniqueInstance ?? (_uniqueInstance = new MedicalCertList());
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
    
    public DataTable ToDataTable()
    {
      return CreateTable(_list);
    }

    public DataTable ToDataTable(Driver driver)
    {
      var medicalCerts = from medicalCert in _list
        where medicalCert.Driver.ID == driver.ID
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
        dt.Rows.Add(medicalCert.getRow());

      return dt;
    }

    public MedicalCert getItem(int id)
    {
      return _list.FirstOrDefault(mc => mc.ID == id);
    }

    public MedicalCert getItem(Driver driver)
    {
      return _list.Where(m => m.Driver.ID == driver.ID).OrderByDescending(m => m.DateEnd).FirstOrDefault();
    }

    public void Delete(int idMedicalCert)
    {
      MedicalCert medicalCert = getItem(idMedicalCert);

      _list.Remove(medicalCert);

      medicalCert.Delete();
    }

    public List<INotification> ToList()
    {
      DriverList driverList = DriverList.getInstance();
      IEnumerable<MedicalCert> listNew =
        _list.Where(item => !driverList.getItem(item.Driver.ID).Fired && !driverList.getItem(item.Driver.ID).Decret &&
                           driverList.getItem(item.Driver.ID).IsDriver);

      var listNotification = new List<INotification>();

      foreach (INotification item in listNew)
        listNotification.Add(item);

      return listNotification;
    }
  }
}
