using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using BBAuto.Domain.Abstract;
using BBAuto.Domain.ForCar;
using BBAuto.Domain.Entities;

namespace BBAuto.Domain.Lists
{
  public class MileageList : MainList<Mileage>
  {
    private static MileageList _uniqueInstance;
    
    public static MileageList getInstance()
    {
      return _uniqueInstance ?? (_uniqueInstance = new MileageList());
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
    
    public Mileage getItem(int id)
    {
      return _list.FirstOrDefault(m => m.ID == id);
    }

    public Mileage getItemByCarId(int carId)
    {
      return _list.OrderByDescending(item => item.Date).FirstOrDefault(item => item.CarId == carId);
    }

    public Mileage getItem(int carId, DateTime date)
    {
      return _list.OrderByDescending(item => item.Date).FirstOrDefault(item => item.CarId == carId && item.Date.Year == date.Year && item.Date.Month == date.Month);
    }

    public Mileage getItem(int carId, Mileage current)
    {
      return _list.OrderByDescending(item => item.Date).FirstOrDefault(item => item.CarId == carId && item != current);
    }
    
    public void Delete(int idMileage)
    {
      var mileage = getItem(idMileage);

      _list.Remove(mileage);

      mileage.Delete();
    }

    public DataTable ToDataTable(Car car)
    {
      DataTable dt = createTable();

      var mileages = _list.Where(item => item.CarId == car.ID).OrderByDescending(item => item.Date);

      foreach (Mileage mileage in mileages)
        dt.Rows.Add(mileage.getRow());

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

    public int GetDistance(Car car, DateTime date)
    {
      DateTime datePrev = (date.Month == 12)
        ? new DateTime(date.Year - 1, 11, 1)
        : (date.Month == 1)
          ? new DateTime(date.Year - 1, 12, 1)
          : new DateTime(date.Year, date.Month - 1, 1);

      var listPrev = (from item in _list
        where item.CarId == car.ID && (item.Date.Year == datePrev.Year && item.Date.Month == datePrev.Month)
        orderby item.Count descending
        select Convert.ToInt32(item.Count)).ToList();

      var listCurrent = (from item in _list
        where item.CarId == car.ID && (item.Date.Year == date.Year && item.Date.Month == date.Month)
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

    public int GetBeginDistance(Car car, DateTime date)
    {
      DateTime datePrev = (date.Month == 12)
        ? new DateTime(date.Year - 1, 11, 1)
        : (date.Month == 1)
          ? new DateTime(date.Year - 1, 12, 1)
          : new DateTime(date.Year, date.Month - 1, 1);

      var listPrev = (from item in _list
        where item.CarId == car.ID && (item.Date.Year == datePrev.Year && item.Date.Month == datePrev.Month)
        orderby item.Count descending
        select Convert.ToInt32(item.Count)).ToList();

      var listCurrent = (from item in _list
        where item.CarId == car.ID && (item.Date.Year == date.Year && item.Date.Month == date.Month)
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

    public int GetEndDistance(Car car, DateTime date)
    {
      DateTime datePrev = (date.Month == 12)
        ? new DateTime(date.Year - 1, 11, 1)
        : (date.Month == 1)
          ? new DateTime(date.Year - 1, 12, 1)
          : new DateTime(date.Year, date.Month - 1, 1);

      var listPrev = (from item in _list
        where item.CarId == car.ID && (item.Date.Year == datePrev.Year && item.Date.Month == datePrev.Month)
        orderby item.Count descending
        select Convert.ToInt32(item.Count)).ToList();

      var listCurrent = (from item in _list
        where item.CarId == car.ID && (item.Date.Year == date.Year && item.Date.Month == date.Month)
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
