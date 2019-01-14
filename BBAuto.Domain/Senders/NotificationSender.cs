using BBAuto.Domain.Abstract;
using BBAuto.Domain.Entities;
using BBAuto.Domain.ForDriver;
using BBAuto.Domain.Lists;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BBAuto.Domain.Senders
{
  public class NotificationSender
  {
    private readonly INotificationList _list;

    public NotificationSender(INotificationList list)
    {
      _list = list;
    }

    public bool SendNotification()
    {
      try
      {
        var list = GetList(DateTime.Today.AddDays(31));

        if(!list.Any())
        {
          Logger.LogManager.Logger.Debug("Документы для для отправки не найдены");
          return false;
        }

        Logger.LogManager.Logger.Information($"Найдено {list.Count} документов для отправки");

        foreach (INotification item in list)
        {
          item.SendNotification();
        }

        return true;
      }
      catch (Exception ex)
      {
        Logger.LogManager.Logger.Error(ex, ex.Message);
        return false;
      }
    }

    private List<INotification> GetList(DateTime date)
    {
      var list = _list.ToList()
        .Where((item) => (item.DateEnd == date && !item.IsNotificationSent && !IsExistNewItem(item)));

      return list.ToList();
    }

    private bool IsExistNewItem(INotification notification)
    {
      return _list.ToList().Exists(item => item.Driver.ID == notification.Driver.ID &&
                                           item.DateEnd > notification.DateEnd);
    }

    public bool SendNotificationOverdue()
    {
      try
      {
        if ((DateTime.Today.Day % 7) != 0)
        {
          Logger.LogManager.Logger.Debug("Сегодня рассылка по напониманию о просроченным документам не производится");
          return false;
        }

        List<INotification> list = GetListOverdue(DateTime.Today);

        if (!list.Any())
        {
          Logger.LogManager.Logger.Debug("Документы для для отправки не найдены");
          return false;
        }

        Logger.LogManager.Logger.Information($"Найдено {list.Count} документов для отправки");

        foreach (INotification item in list)
        {
          item.SendNotification();
        }

        return true;
      }
      catch (Exception ex)
      {
        Logger.LogManager.Logger.Error(ex, ex.Message);
        return false;
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
      List<INotification> list = _list.ToList();

      /* select driver_fio, max(MedicalCert_dateEnd) from [dbo].[MedicalCert] mc
       * join Driver d on d.driver_id = mc.driver_id
       * where MedicalCert_dateEnd < date//'2017-10-24'
       * group by driver_fio
       * order by driver_fio
       */

      var temp = (from item in list
                  where item.DateEnd < date
                  group item by item.Driver.ID
                  into t
                  orderby t.Key
                  select t.OrderByDescending(y => y.DateEnd).FirstOrDefault()).ToList();

      /* тут будет несколько прошлогодних справок */
      //var list1 = (list.Where(item => (item.DateEnd < date))).ToList();
      var actualDocuments = (list.Where(item => (item.DateEnd >= date))).ToList();

      DriverList driverList = DriverList.getInstance();

      return (from item1 in temp
              join item2 in actualDocuments on item1.Driver.ID equals item2.Driver.ID into table1
              from item3 in table1.DefaultIfEmpty()
              where item3 == null && (!driverList.getItem(item1.Driver.ID).NotificationStop)
              select item1).ToList();
    }

    public bool SendNotificationNotExist()
    {
      if ((DateTime.Today.Day % 7) != 0)
      {
        Logger.LogManager.Logger.Debug("Сегодня рассылка по напониманию об отсутствующих документах не производится");
        return false;
      }

      try
      {
        List<INotification> list = GetListNotExist();
        List<INotification> list2 = GetListWithoutFile();

        list.AddRange(list2);

        Logger.LogManager.Logger.Information($"Найдено {list.Count} документов для отправки");

        foreach (INotification item in list)
        {
          item.SendNotification();
        }

        return true;
      }
      catch (Exception ex)
      {
        Logger.LogManager.Logger.Error(ex, ex.Message);
        return false;
      }
    }

    private List<INotification> GetListNotExist()
    {
      DriverList driverList = DriverList.getInstance();
      List<Driver> listDriver = driverList.GetList()
        .Where(item => (!item.Fired && !item.Decret && !item.NotificationStop && item.IsDriver)).ToList();

      List<INotification> list = _list.ToList();

      List<Driver> listNotExist = (from itemDriver in listDriver
                                   join itemMC in list on itemDriver.ID equals itemMC.Driver.ID into table1
                                   from itemRes in table1.DefaultIfEmpty()
                                   where itemRes == null
                                   select itemDriver).ToList();

      List<INotification> listNotification = new List<INotification>();

      foreach (Driver item in listNotExist)
      {
        if (list.First() is MedicalCert)
          listNotification.Add(new MedicalCert(item));
        else if (list.First() is DriverLicense)
          listNotification.Add(new DriverLicense(item));
      }

      return listNotification;
    }

    private List<INotification> GetListWithoutFile()
    {
      DriverList driverList = DriverList.getInstance();
      List<Driver> listDriver = driverList.GetList()
        .Where(item => (!item.Fired && !item.Decret && !item.NotificationStop && item.IsDriver)).ToList();

      List<INotification> list = _list.ToList();

      List<INotification> listExist = (from itemMC in list
                                       join itemDriver in listDriver on itemMC.Driver.ID equals itemDriver.ID into table1
                                       from itemRes in table1.DefaultIfEmpty()
                                       where itemRes != null
                                       select itemMC).ToList();

      List<INotification> listNotification = new List<INotification>();

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
      DriverList driverList = DriverList.getInstance();
      List<Driver> listDriver = driverList.GetList()
        .Where(item => (item.NotificationStop && item.DateStopNotification == DateTime.Today)).ToList();

      foreach (Driver driver in listDriver)
        driver.DateStopNotification = new DateTime(1, 1, 1);
    }
  }
}