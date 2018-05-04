using System.Collections.Generic;
using System.Data;
using System.Linq;
using BBAuto.Logic.Abstract;
using BBAuto.Logic.Entities;
using BBAuto.Logic.ForCar;

namespace BBAuto.Logic.Lists
{
  public class CarDocList : MainList
  {
    private static CarDocList _uniqueInstance;
    private List<CarDoc> list;

    private CarDocList()
    {
      list = new List<CarDoc>();

      LoadFromSql();
    }

    public static CarDocList getInstance()
    {
      if (_uniqueInstance == null)
        _uniqueInstance = new CarDocList();

      return _uniqueInstance;
    }

    protected override void LoadFromSql()
    {
      DataTable dt = Provider.Select("CarDoc");

      foreach (DataRow row in dt.Rows)
      {
        CarDoc carDoc = new CarDoc(row);
        Add(carDoc);
      }
    }

    public void Add(CarDoc carDoc)
    {
      if (list.Exists(item => item == carDoc))
        return;

      list.Add(carDoc);
    }

    public void Delete(int idCarDoc)
    {
      CarDoc carDoc = getItem(idCarDoc);

      list.Remove(carDoc);

      carDoc.Delete();
    }

    public CarDoc getItem(int id)
    {
      return list.FirstOrDefault(c => c.Id == id);
    }

    public DataTable ToDataTableByCar(Car car)
    {
      try
      {
        DataTable dt = createTable();

        foreach (CarDoc carDoc in list.Where(c => c.Car.Id == car.Id))
          dt.Rows.Add(carDoc.ToRow());

        return dt;
      }
      catch
      {
        return null;
      }
    }

    private DataTable createTable()
    {
      DataTable dt = new DataTable();
      dt.Columns.Add("id");
      dt.Columns.Add("Название");
      dt.Columns.Add("Файл");

      return dt;
    }
  }
}
