using BBAuto.Domain.Entities;
using BBAuto.Domain.Lists;
using BBAuto.Domain.Services.Mail;
using BBAuto.Domain.Static;

namespace BBAuto.Domain.Common
{
  public static class MailPolicy
  {
    public static string Send(Car car, PolicyType type)
    {
      IMailService mailService = new MailService();

      mailService.SendMailPolicy(car, type);

      DriverCarList driverCarList = DriverCarList.GetInstance();
      Driver driver = driverCarList.GetDriver(car);

      return string.Concat("Полис ", type.ToString(), " отправлен на адрес ", driver.Email);
    }
  }
}
