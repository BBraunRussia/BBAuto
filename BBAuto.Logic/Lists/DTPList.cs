using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using BBAuto.Logic.Abstract;
using BBAuto.Logic.Entities;
using BBAuto.Logic.ForCar;
using BBAuto.Logic.Services.Car;

namespace BBAuto.Logic.Lists
{
  public class DTPList : MainList
  {
    private static DTPList uniqueInstance;
    private List<DTP> list;

    private DTPList()
    {
      list = new List<DTP>();

      LoadFromSql();
    }

    public static DTPList getInstance()
    {
      if (uniqueInstance == null)
        uniqueInstance = new DTPList();

      return uniqueInstance;
    }

    protected override void LoadFromSql()
    {
      list.Clear();

      DataTable dt = Provider.Select("DTP");

      foreach (DataRow row in dt.Rows)
      {
        DTP dtp = new DTP(row);
        Add(dtp);
      }
    }

    public void Add(DTP dtp)
    {
      if (list.Exists(item => item == dtp))
        return;

      list.Add(dtp);
    }

    public DTP getItem(int id)
    {
      return list.FirstOrDefault(dtp => dtp.Id == id);
    }

    public DataTable ToDataTable()
    {
      List<DTP> dtps = list.OrderByDescending(item => item.Date).ToList();

      return createTable(dtps);
    }

    public DataTable ToDataTable(Driver driver)
    {
      if (driver.Id == 0)
        return null;

      List<DTP> dtps = list.Where(item => item.isEqualDriverID(driver)).ToList();

      return createTable(dtps);
    }

    public DataTable ToDataTable(Car car)
    {
      List<DTP> dtps = ToList(car);

      return createTable(dtps);
    }

    public DTP GetLast(Car car)
    {
      List<DTP> dtps = ToList(car);

      return (dtps.Count > 0) ? dtps.First() : new DTP(new Car());
    }
    
    private List<DTP> ToList(Car car)
    {
      return list.Where(item => item.Car.Id == car.Id).OrderByDescending(item => item.Date).ToList();
    }

    public DTP GetLast(int carId)
    {
      var dtps = list.Where(item => item.Car.Id == carId).OrderByDescending(item => item.Date).ToList();

      return dtps.FirstOrDefault();
    }

    private DataTable createTable(List<DTP> dtpList)
    {
      DataTable dt = new DataTable();
      dt.Columns.Add("id");
      dt.Columns.Add("idCar");
      dt.Columns.Add("Бортовой номер");
      dt.Columns.Add("Регистрационный знак");
      dt.Columns.Add("№ дела");
      dt.Columns.Add("Дата", Type.GetType("System.DateTime"));
      dt.Columns.Add("Место ДТП");
      dt.Columns.Add("Водитель");
      dt.Columns.Add("Дата обращения в страховую компанию", Type.GetType("System.DateTime"));
      dt.Columns.Add("Текущее состояние");
      dt.Columns.Add("Виновник происшествия");
      dt.Columns.Add("Сумма возмещения", Type.GetType("System.Double"));
      dt.Columns.Add("Примечание");
      dt.Columns.Add("Обстоятельства ДТП (со слов участника)");
      dt.Columns.Add("Повреждения");
      dt.Columns.Add("Статус после ДТП");
      dt.Columns.Add("№ убытка страховой");

      foreach (DTP dtp in dtpList)
        dt.Rows.Add(dtp.GetRow());

      return dt;
    }

    public void Delete(int idDTP)
    {
      DTP dtp = getItem(idDTP);

      list.Remove(dtp);

      dtp.Delete();
    }

    public DTP getLastByDriver(Driver driver)
    {
      if (driver.Id == driver.Id)
        return new DTP(new Car());

      List<DTP> dtps = list.Where(item => item.isEqualDriverID(driver)).OrderByDescending(item => item.Date).ToList();

      return (dtps.Count() > 0) ? dtps.First() : new DTP(new Car());
    }

    public int GetMaxNumber()
    {
      return list.OrderByDescending(item => item.Number).First().Number;
    }
  }
}
