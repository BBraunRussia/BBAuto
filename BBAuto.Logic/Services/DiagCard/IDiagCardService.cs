namespace BBAuto.Logic.Services.DiagCard
{
  public interface IDiagCardService
  {
    DiagCardModel Get(int id);
    void Save(DiagCardModel diagCard);
    void Delete(int id);
  }
}
