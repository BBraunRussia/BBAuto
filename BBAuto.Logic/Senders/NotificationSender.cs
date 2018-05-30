using System;
using System.Collections.Generic;
using System.Linq;
using BBAuto.Logic.Abstract;
using BBAuto.Logic.ForDriver;
using BBAuto.Logic.Lists;

namespace BBAuto.Logic.Senders
{
  public class NotificationSender : INotificationSender
  {
    private readonly INotificationList _list;

    public NotificationSender(INotificationList list)
    {
      _list = list;
    }

    public void SendNotification()
    {
      var list = GetList(DateTime.Today.AddDays(31));

      foreach (INotification item in list)
      {
        item.SendNotification();
      }
    }

    private IEnumerable<INotification> GetList(DateTime date)
    {
      var list = _list.ToList()
        .Where(item => item.DateEnd == date && !item.IsNotificationSent && !IsExistNewItem(item));

      return list.ToList();
    }

    private bool IsExistNewItem(INotification notification)
    {
      return _list.ToList().Exists(item => item.Driver.Id == notification.Driver.Id &&
                                           item.DateEnd > notification.DateEnd);
    }

    public void SendNotificationOverdue()
    {
      if (DateTime.Today.Day % 7 != 0)
        return;

      var list = GetListOverdue(DateTime.Today);

      foreach (var item in list)
      {
        item.SendNotification();
      }
    }

    static DateTime? Max(DateTime? a, DateTime? b)
    {
      if (!a.HasValue && !b.HasValue) return a; // doesn't matter

      if (!a.HasValue) return b;
      if (!b.HasValue) return a;

      return a.Value > b.Value ? a : b;
    }

    //НЕПРАВИЛЬНО!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
    //По 2 раза отправляются письма, тем, у кого нет справки (кого нет в list2)
    private List<INotification> GetListOverdue(DateTime date)
    {
      var list = _list.ToList();

      /* select driver_fio, max(MedicalCert_dateEnd) from [dbo].[MedicalCert] mc
       * join Driver d on d.driver_id = mc.driver_id
       * where MedicalCert_dateEnd < date//'2017-10-24'
       * group by driver_fio
       * order by driver_fio
       */

      var temp = (from item in list
        group item by item.Driver.Id
        into t
        orderby t.Key
        select t.OrderByDescending(y => y.DateEnd).FirstOrDefault()).ToList();

      /* тут будет несколько прошлогодних справок */
      //var list1 = (list.Where(item => (item.DateEnd < date))).ToList();
      var list2 = (list.Where(item => (item.DateEnd >= date))).ToList();


      DriverList driverList = DriverList.getInstance();

      return (from item1 in temp
        join item2 in list2 on item1.Driver.Id equals item2.Driver.Id into table1
        from item3 in table1.DefaultIfEmpty()
        where item3 == null && (!driverList.getItem(item1.Driver.Id).NotificationStop)
        select item1).ToList();
    }

    public void SendNotificationNotExist()
    {
      if ((DateTime.Today.Day % 7) != 0)
        return;

      var list = GetListNotExist();
      var list2 = GetListWithoutFile();

      list.AddRange(list2);

      foreach (INotification item in list)
      {
        item.SendNotification();
      }
    }

    private List<INotification> GetListNotExist()
    {
      var driverList = DriverList.getInstance();
      var listDriver = driverList.ToList()
        .Where(item => (!item.Fired && !item.Decret && !item.NotificationStop && item.IsDriver)).ToList();

      var list = _list.ToList();

      var driversWithoutActualDocuments = (from itemDriver in listDriver
        join itemMc in list on itemDriver.Id equals itemMc.Driver.Id into table1
        from itemRes in table1.DefaultIfEmpty()
        where itemRes == null
        select itemDriver).ToList();

      var listNotification = new List<INotification>();

      foreach (var driver in driversWithoutActualDocuments)
      {
        if (list.First() is MedicalCert)
          listNotification.Add(new MedicalCert(driver));
        else if (list.First() is DriverLicense)
          listNotification.Add(new DriverLicense(driver));
      }

      return listNotification;
    }

    private IEnumerable<INotification> GetListWithoutFile()
    {
      var driverList = DriverList.getInstance();
      var listDriver = driverList.ToList()
        .Where(item => (!item.Fired && !item.Decret && !item.NotificationStop && item.IsDriver)).ToList();

      var list = _list.ToList();

      var listExist = (from itemMc in list
        join itemDriver in listDriver on itemMc.Driver.Id equals itemDriver.Id into table1
        from itemRes in table1.DefaultIfEmpty()
        where itemRes != null
        select itemMc).ToList();

      var listNotification = new List<INotification>();

      foreach (IActual item in listExist)
      {
        if (item.IsDateActual() && !item.IsHaveFile())
        {
          if (list.First() is MedicalCert)
            listNotification.Add(item as MedicalCert);
          else if (list.First() is DriverLicense)
            listNotification.Add(item as DriverLicense);
        }
      }

      return listNotification;
    }

    public void ClearStopIfNeed()
    {
      var driverList = DriverList.getInstance();
      var listDriver = driverList.ToList()
        .Where(item => (item.NotificationStop && item.DateStopNotification == DateTime.Today)).ToList();

      foreach (var driver in listDriver)
        driver.DateStopNotification = new DateTime(1, 1, 1);
    }
  }
}
