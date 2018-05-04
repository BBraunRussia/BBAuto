using BBAuto.Logic.Entities;

namespace BBAuto.Logic.Common
{
  public class MileageReport
  {
    private readonly Car _car;
    private readonly int _carId;
    private readonly string _message;

    public MileageReport(Car car, string message)
    {
      _car = car;
      _message = message;
    }

    public MileageReport(int carId, string message, bool isNew)
    {
      _carId = carId;
      _message = message;
    }

    public override string ToString()
    {
      return _car == null ? _message : _message + " " + _car;
    }

    public bool IsFailed => _car == null;
  }
}
