using System.Data;

namespace BBAuto.Logic.Services.Car.Doc
{
  public interface ICarDocService
  {
    CarDocModel Save(CarDocModel carDoc);

    void Delete(int id);

    CarDocModel GetCarDocById(int id);
    DataTable GetDataTableByCarId(int carId);
  }
}
