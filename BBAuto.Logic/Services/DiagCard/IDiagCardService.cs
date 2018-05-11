namespace BBAuto.Logic.Services.DiagCard
{
  public interface IDiagCardService
  {
    DiagCardModel GetByCarId(int idCar);
    void Save(DiagCardModel diagCard);
    void Delete(int id);
  }
}
