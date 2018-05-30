using System.Collections.Generic;
using BBAuto.Logic.Services.Account;
using BBAuto.Logic.Services.Car;
using BBAuto.Logic.Services.Driver;
using BBAuto.Logic.Services.Violation;
using BBAuto.Logic.Static;

namespace BBAuto.Logic.Services.MailService
{
  public interface IMailService
  {
    void SendMailAccountViolation(string driverName, string file, CarModel car);
    void SendMailViolation(ViolationModel violation, CarModel car, DriverModel driver);
    void SendMailPolicy(int carId, PolicyType type);
    void SendMailAccount(AccountModel account);
    void SendNotification(DriverModel driver, string message, bool addTransportToCopy = true,
      IList<string> fileNames = null);
  }
}
