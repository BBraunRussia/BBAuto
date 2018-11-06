using System.Linq;
using System.Data;
using BBAuto.Domain.ForCar;
using BBAuto.Domain.Abstract;
using BBAuto.Domain.Entities;

namespace BBAuto.Domain.Lists
{
  public class CarDocList : MainList<CarDoc>
  {
    private static CarDocList _uniqueInstance;
    
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
    
    public void Delete(int idCarDoc)
    {
      CarDoc carDoc = getItem(idCarDoc);

      _list.Remove(carDoc);

      carDoc.Delete();
    }

    public CarDoc getItem(int id)
    {
      return _list.FirstOrDefault(c => c.ID == id);
    }

    public DataTable ToDataTableByCar(Car car)
    {
      try
      {
        DataTable dt = createTable();

        foreach (CarDoc carDoc in _list.Where(c => c.CarId == car.ID))
          dt.Rows.Add(carDoc.getRow());

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
