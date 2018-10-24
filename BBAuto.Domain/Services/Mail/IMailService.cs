using System.Collections.Generic;
using BBAuto.Domain.Entities;
using BBAuto.Domain.ForCar;
using BBAuto.Domain.Services.Documents;
using BBAuto.Domain.Static;

namespace BBAuto.Domain.Services.Mail
{
  public interface IMailService
  {
    void SendMailAccountViolation(Violation violation);
    void SendMailViolation(Violation violation);
    void SendMailPolicy(Car car, PolicyType type);
    void SendMailAccount(Account account);

    void SendNotification(Driver driver, string message, bool addTransportToCopy = true,
      List<string> fileNames = null);

    void SendDocuments(IList<Driver> drivers, List<Document> documentsForSend);
  }
}
