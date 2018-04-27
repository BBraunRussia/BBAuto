using BBAuto.Logic.Entities;
using BBAuto.Logic.Services.Car;

namespace BBAuto.Logic.Common
{
  public class MileageReport
  {
    private readonly Car _car;
    private readonly CarModel _carmodel;
    private readonly string _message;

    public MileageReport(Car car, string message)
    {
      _car = car;
      _message = message;
    }

    public MileageReport(CarModel carmodel, string message, bool isNew)
    {
      _carmodel = carmodel;
      _message = message;
    }

    public override string ToString()
    {
      return _car == null ? _message : _message + " " + _car;
    }

    public bool IsFailed => _car == null;
  }
}
