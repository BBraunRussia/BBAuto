using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using BBAuto.Logic.Entities;
using BBAuto.Logic.Lists;

namespace BBAuto.Logic.Common
{
  public class WayBillDaily : IEnumerable
  {
    private const int MinDailyMileage = 100;

    private readonly int _carId;
    private DateTime _date;
    private readonly Dictionary<int, WayBillDay> _list;

    private readonly MileageList _mileageList;

    public WayBillDaily(int carId, DateTime date)
    {
      _carId = carId;
      _date = date;

      _mileageList = MileageList.getInstance();

      if (_list == null)
        _list = new Dictionary<int, WayBillDay>();

      LoadWayBillDay();
    }

    private void LoadWayBillDay()
    {
      WayBillDayList wayBillDayList = WayBillDayList.getInstance();

      foreach (var item in wayBillDayList.getList(_carId, _date))
      {
        _list.Add(item.Date.Day, item);
        //wayBillDayList.getList(_car, _date);
      }
    }

    public int Count
    {
      get { return _list.Count; }
    }

    public int BeginDistance
    {
      get { return _mileageList.GetBeginDistance(_carId, _date); }
    }

    public int EndDistance
    {
      get { return _mileageList.GetEndDistance(_carId, _date); }
    }

    public void Load()
    {
      if (_list.Count > 0)
        return;

      Dictionary<int, Driver> driverWithDay = GetDriversDictionary();

      if (driverWithDay.Count == 0)
        return;

      int count = _mileageList.GetDistance(_carId, _date);

      Random random = new Random();

      int workDays = driverWithDay.Count;

      bool isShortMonth = ((count / workDays) < MinDailyMileage);
      if (isShortMonth)
        workDays /= 2;
      int div = random.Next(1);


      var fuelList = FuelList.getInstance().GetListFiltred(_carId, _date);

      foreach (var item in fuelList)
      {
        DateTime date = new DateTime(_date.Year, _date.Month, item.Date.Day);

        int curCount = count - GetDistance();
        if ((workDays - _list.Count) == 0)
          break;

        int everyDayCount = curCount / (workDays - _list.Count);

        if (driverWithDay.ContainsKey(item.Date.Day))
        {
          WayBillDay wayBillDay = CreateWayBillDaily(driverWithDay[item.Date.Day], date, everyDayCount, random);
          AddToList(wayBillDay, item.Date.Day);
        }
        if (curCount < 10)
          break;
      }

      foreach (var item in driverWithDay)
      {
        DateTime date = new DateTime(_date.Year, _date.Month, item.Key);

        if ((isShortMonth) && (item.Key % 2 == div))
          continue;

        int curCount = count - GetDistance();
        if ((workDays - _list.Count) == 0)
          break;

        int everyDayCount = curCount / (workDays - _list.Count);

        WayBillDay wayBillDay = CreateWayBillDaily(item.Value, date, everyDayCount, random);
        AddToList(wayBillDay, item.Key);

        if (curCount < 10)
          break;
      }
    }

    private WayBillDay CreateWayBillDaily(Driver driver, DateTime date, int everyDayCount, Random random)
    {
      if (_list.ContainsKey(date.Day))
        return null;

      WayBillDay wayBillDay = new WayBillDay(_carId, driver, date, everyDayCount);
      wayBillDay.Save();
      wayBillDay.ReadRoute(random);

      return wayBillDay;
    }

    private void AddToList(WayBillDay wayBillDay, int day)
    {
      if ((wayBillDay != null) && (wayBillDay.Distance > 0))
      {
        _list.Add(day, wayBillDay);
      }
    }

    private Dictionary<int, Driver> GetDriversDictionary()
    {
      DriverCarList driverCarList = DriverCarList.getInstance();
      //TODO: EF??
      TabelList tabelList = TabelList.GetInstance();

      Dictionary<int, Driver> drivers = new Dictionary<int, Driver>();

      for (int day = 1; day <= _date.AddMonths(1).AddDays(-1).Day; day++)
      {
        DateTime curDate = new DateTime(_date.Year, _date.Month, day);

        Driver driver = driverCarList.GetDriver(_carId, curDate);

        List<int> days = tabelList.GetDays(driver, curDate);

        if (days.Count == 0)
          break;
        //throw new NullReferenceException("Нет табельных листов на выбранный месяц для водителя " + driver.GetName(NameType.Short));

        if (!days.Exists(item => item == day))
          continue;

        drivers.Add(day, driver);
      }

      return drivers;
    }

    private int GetDistance()
    {
      int count = 0;

      foreach (WayBillDay item in this)
        count += item.Distance;

      return count;
    }

    public DataTable ToDataTable()
    {
      DataTable dt = new DataTable();
      dt.Columns.Add("Дата");
      dt.Columns.Add("Водитель");

      foreach (WayBillDay item in this)
      {
        dt.Rows.Add(item.ToRow());
      }

      return dt;
    }

    public IEnumerator GetEnumerator()
    {
      foreach (var item in _list.OrderBy(w => w.Value.Day))
      {
        yield return item.Value;
      }
    }

    public void Clear()
    {
      foreach (var item in _list)
      {
        item.Value.Delete();
      }

      _list.Clear();
    }
  }
}
