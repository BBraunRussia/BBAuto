using BBAuto.Logic.Entities;
using BBAuto.Logic.Lists;
using BBAuto.Logic.Static;

namespace BBAuto.Logic.Common
{
  public static class MailPolicy
  {
    public static string Send(Car car, PolicyType type)
    {
      var mail = new EMail();

      mail.SendMailPolicy(car, type);

      var driverCarList = DriverCarList.getInstance();
      var driver = driverCarList.GetDriver(car.Id);

      return string.Concat("Полис ", type.ToString(), " отправлен на адрес ", driver.email);
    }
  }
}
