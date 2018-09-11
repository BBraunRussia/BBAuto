using BBAuto.Domain.Abstract;
using BBAuto.Domain.Entities;
using BBAuto.Domain.ForCar;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace BBAuto.Domain.Lists
{
  public class DTPList : MainList
  {
    private static DTPList uniqueInstance;
    private List<DTP> list;

    private DTPList()
    {
      list = new List<DTP>();

      loadFromSql();
    }

    public static DTPList getInstance()
    {
      if (uniqueInstance == null)
        uniqueInstance = new DTPList();

      return uniqueInstance;
    }

    protected override void loadFromSql()
    {
      list.Clear();

      DataTable dt = _provider.Select("DTP");

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
      return list.FirstOrDefault(dtp => dtp.ID == id);
    }

    public DataTable ToDataTable()
    {
      var dtps = list.OrderByDescending(item => item.Date).ToList();

      return CreateTable(dtps);
    }

    public DataTable ToDataTable(Driver driver)
    {
      if (driver.ID == 0)
        return null;

      var dtps = list.Where(item => item.IsEqualDriverId(driver))
        .OrderByDescending(item => item.Date).ToList();

      return CreateTable(dtps);
    }

    public DataTable ToDataTable(Car car)
    {
      List<DTP> dtps = ToList(car);

      return CreateTable(dtps);
    }

    public DTP GetLast(Car car)
    {
      List<DTP> dtps = ToList(car);

      return (dtps.Count > 0) ? dtps.First() : new DTP(new Car());
    }

    private List<DTP> ToList(Car car)
    {
      return list.Where(item => item.Car.ID == car.ID).OrderByDescending(item => item.Date).ToList();
    }

    private DataTable CreateTable(List<DTP> dtpList)
    {
      DataTable dt = new DataTable();
      dt.Columns.Add("id");
      dt.Columns.Add("idCar");
      dt.Columns.Add("Бортовой номер");
      dt.Columns.Add("Регистрационный знак");
      dt.Columns.Add("№ дела");
      dt.Columns.Add("Дата", typeof(DateTime));
      dt.Columns.Add("Место ДТП");
      dt.Columns.Add("Водитель");
      dt.Columns.Add("Дата обращения в страховую компанию", typeof(DateTime));
      dt.Columns.Add("Текущее состояние");
      dt.Columns.Add("Виновник происшествия");
      dt.Columns.Add("Сумма возмещения", typeof(double));
      dt.Columns.Add("Примечание");
      dt.Columns.Add("Обстоятельства ДТП (со слов участника)");
      dt.Columns.Add("Повреждения");
      dt.Columns.Add("Статус после ДТП");
      dt.Columns.Add("№ убытка страховой");

      foreach (DTP dtp in dtpList)
        dt.Rows.Add(dtp.getRow());

      return dt;
    }

    public void Delete(int idDTP)
    {
      DTP dtp = getItem(idDTP);

      list.Remove(dtp);

      dtp.Delete();
    }

    public DTP GetLastByDriver(Driver driver)
    {
      var dtps = list.Where(item => item.IsEqualDriverId(driver)).OrderByDescending(item => item.Date).ToList();

      return dtps.Any() ? dtps.First() : null;
    }

    public int GetMaxNumber()
    {
      return list.OrderByDescending(item => item.Number).First().Number;
    }
  }
}
