using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using BBAuto.Logic.Abstract;
using BBAuto.Logic.ForCar;

namespace BBAuto.Logic.Lists
{
  public class RepairList : MainList
  {
    private List<Repair> list;
    private static RepairList uniqueInstance;

    private RepairList()
    {
      list = new List<Repair>();

      LoadFromSql();
    }

    public static RepairList getInstance()
    {
      if (uniqueInstance == null)
        uniqueInstance = new RepairList();

      return uniqueInstance;
    }

    protected override void LoadFromSql()
    {
      DataTable dt = Provider.Select("Repair");

      foreach (DataRow row in dt.Rows)
      {
        Repair repair = new Repair(row);
        Add(repair);
      }
    }

    public void Add(Repair repair)
    {
      if (list.Exists(item => item == repair))
        return;

      list.Add(repair);
    }

    public Repair getItem(int id)
    {
      return list.FirstOrDefault(r => r.Id == id);
    }

    public void Delete(int idRepair)
    {
      Repair repair = getItem(idRepair);

      list.Remove(repair);

      repair.Delete();
    }

    public DataTable ToDataTable()
    {
      DataTable dt = createTable();

      var repairs = from repair in list
        orderby repair.Date
        select repair;

      foreach (Repair repair in repairs)
        dt.Rows.Add(repair.ToRow());

      return dt;
    }

    public DataTable ToDataTableByCar(int carId)
    {
      DataTable dt = createTable();

      var repairs = from repair in list
        where repair.CarId == carId
        orderby repair.Date
        select repair;

      foreach (Repair repair in repairs)
        dt.Rows.Add(repair.ToRow());

      return dt;
    }

    private DataTable createTable()
    {
      DataTable dt = new DataTable();
      dt.Columns.Add("id");
      dt.Columns.Add("idCar");
      dt.Columns.Add("Бортовой номер");
      dt.Columns.Add("ГРЗ");
      dt.Columns.Add("Вид ремонта");
      dt.Columns.Add("СТО");
      dt.Columns.Add("Дата", typeof(DateTime));
      dt.Columns.Add("Стоимость", typeof(double));
      dt.Columns.Add("Файл");

      return dt;
    }
  }
}
