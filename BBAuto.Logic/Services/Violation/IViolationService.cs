using BBAuto.Logic.Services.Car;

namespace BBAuto.Logic.Services.Violation
{
  public interface IViolationService
  {
    ViolationModel Save(ViolationModel violation);
    void Agree(ViolationModel violation, CarModel car);
    ViolationModel GetById(int id);
  }
}
