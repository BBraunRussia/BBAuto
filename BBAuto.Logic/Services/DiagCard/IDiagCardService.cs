using System.Collections.Generic;
using System.Data;
using BBAuto.Logic.Services.Car;

namespace BBAuto.Logic.Services.DiagCard
{
  public interface IDiagCardService
  {
    DiagCardModel GetByCarId(int idCar);
    void Save(DiagCardModel diagCard);
    void Delete(int id);
    DataTable GetDataTable(ICarService carService);
    DataTable GetDataTableByCar(CarModel car);

    IList<DiagCardModel> GetDiagCardsForSend();
  }
}
