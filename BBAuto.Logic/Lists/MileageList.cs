using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using BBAuto.Logic.Abstract;
using BBAuto.Logic.Entities;
using BBAuto.Logic.ForCar;

namespace BBAuto.Logic.Lists
{
  public class MileageList : MainList
  {
    private static MileageList uniqueInstance;
    private List<Mileage> list;

    private MileageList()
    {
      list = new List<Mileage>();

      LoadFromSql();
    }

    public static MileageList getInstance()
    {
      if (uniqueInstance == null)
        uniqueInstance = new MileageList();

      return uniqueInstance;
    }

    protected override void LoadFromSql()
    {
      DataTable dt = Provider.Select("Mileage");

      foreach (DataRow row in dt.Rows)
      {
        Mileage mileage = new Mileage(row);
        Add(mileage);
      }
    }

    public void Add(Mileage mileage)
    {
      if (list.Exists(item => item == mileage))
        return;

      list.Add(mileage);
    }

    public Mileage getItem(int id)
    {
      return getItem(list.Where(m => m.Id == id));
    }

    public Mileage getItem(Car car)
    {
      var mileages = list.Where(item => item.Car.Id == car.Id).OrderByDescending(item => item.Date);

      return getItem(mileages);
    }

    public Mileage getItem(Car car, Mileage current)
    {
      var mileages = list.Where(item => item.Car.Id == car.Id && item != current).OrderByDescending(item => item.Date);

      return getItem(mileages);
    }

    private Mileage getItem(IEnumerable<Mileage> mileages)
    {
      return mileages.FirstOrDefault();
    }

    public void Delete(int idMileage)
    {
      Mileage mileage = getItem(idMileage);

      list.Remove(mileage);

      mileage.Delete();
    }

    public DataTable ToDataTable(Car car)
    {
      DataTable dt = createTable();

      var mileages = list.Where(item => item.Car.Id == car.Id).OrderByDescending(item => item.Date);

      foreach (Mileage mileage in mileages)
        dt.Rows.Add(mileage.ToRow());

      return dt;
    }

    private DataTable createTable()
    {
      DataTable dt = new DataTable();
      dt.Columns.Add("id");
      dt.Columns.Add("Дата", Type.GetType("System.DateTime"));
      dt.Columns.Add("Пробег", Type.GetType("System.Int32"));

      return dt;
    }

    internal int GetDistance(int carId, DateTime date)
    {
      DateTime datePrev = (date.Month == 12)
        ? new DateTime(date.Year - 1, 11, 1)
        : (date.Month == 1)
          ? new DateTime(date.Year - 1, 12, 1)
          : new DateTime(date.Year, date.Month - 1, 1);

      var listPrev = (from item in list
        where item.Car.Id == carId && (item.Date.Year == datePrev.Year && item.Date.Month == datePrev.Month)
        orderby item.Count descending
        select Convert.ToInt32(item.Count)).ToList();

      var listCurrent = (from item in list
        where item.Car.Id == carId && (item.Date.Year == date.Year && item.Date.Month == date.Month)
        orderby item.Count descending
        select Convert.ToInt32(item.Count)).ToList();

      if ((listCurrent.Count == 0) && (listPrev.Count == 0))
        throw new NullReferenceException("Показания одометра не найдены");
      else if (listCurrent.Count > 1)
        return listCurrent.First() - listCurrent.Last();
      else if ((listCurrent.Count == 1) && (listPrev.Count == 0))
        return listCurrent.First();
      else if (listCurrent.Count == 0)
        throw new NullReferenceException("Текущие показания одометра не найдены");
      else
        return listCurrent.First() - listPrev.First();
    }

    internal int GetBeginDistance(int carId, DateTime date)
    {
      DateTime datePrev = (date.Month == 12)
        ? new DateTime(date.Year - 1, 11, 1)
        : (date.Month == 1)
          ? new DateTime(date.Year - 1, 12, 1)
          : new DateTime(date.Year, date.Month - 1, 1);

      var listPrev = (from item in list
        where item.Car.Id == carId && (item.Date.Year == datePrev.Year && item.Date.Month == datePrev.Month)
        orderby item.Count descending
        select Convert.ToInt32(item.Count)).ToList();

      var listCurrent = (from item in list
        where item.Car.Id == carId && (item.Date.Year == date.Year && item.Date.Month == date.Month)
        orderby item.Count descending
        select Convert.ToInt32(item.Count)).ToList();

      if ((listCurrent.Count == 0) && (listPrev.Count == 0))
        throw new NullReferenceException("Показания спидометра не найдены");
      else if (listCurrent.Count > 1)
        return listCurrent.Last();
      else if ((listCurrent.Count == 1) && (listPrev.Count == 0))
        return listCurrent.First();
      else if (listCurrent.Count == 0)
        throw new NullReferenceException("Текущие показания спидометра не найдены");
      else
        return listPrev.First();
    }

    internal int GetEndDistance(int carId, DateTime date)
    {
      DateTime datePrev = (date.Month == 12)
        ? new DateTime(date.Year - 1, 11, 1)
        : (date.Month == 1)
          ? new DateTime(date.Year - 1, 12, 1)
          : new DateTime(date.Year, date.Month - 1, 1);

      var listPrev = (from item in list
        where item.Car.Id == carId && (item.Date.Year == datePrev.Year && item.Date.Month == datePrev.Month)
        orderby item.Count descending
        select Convert.ToInt32(item.Count)).ToList();

      var listCurrent = (from item in list
        where item.Car.Id == carId && (item.Date.Year == date.Year && item.Date.Month == date.Month)
        orderby item.Count descending
        select Convert.ToInt32(item.Count)).ToList();

      if ((listCurrent.Count == 0) && (listPrev.Count == 0))
        throw new NullReferenceException("Показания спидометра не найдены");
      else if (listCurrent.Count > 1)
        return listCurrent.First();
      else if ((listCurrent.Count == 1) && (listPrev.Count == 0))
        return listCurrent.First();
      else if (listCurrent.Count == 0)
        throw new NullReferenceException("Текущие показания спидометра не найдены");
      else
        return listCurrent.First();
    }
  }
}
