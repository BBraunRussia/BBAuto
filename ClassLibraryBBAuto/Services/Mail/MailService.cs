using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Mail;
using System.Text;
using BBAuto.Domain.Dictionary;
using BBAuto.Domain.Entities;
using BBAuto.Domain.ForCar;
using BBAuto.Domain.Lists;
using BBAuto.Domain.Logger;
using BBAuto.Domain.Services.Documents;
using BBAuto.Domain.Static;
using Serilog;

namespace BBAuto.Domain.Services.Mail
{
  public class MailService : IMailService
  {
    private const string ServerHost = "212.0.16.135";
    private const int ServerPort = 25;
    private const string ServerPassword = "";
    private const string ServerUserName = "";
    private const string RobotEmail = "ru.robot@bbraun.com";

    private string _authorEmail;

    private string _subject;
    private string _body;

    private static readonly ILogger Logger = LogManager.Logger;

    public MailService()
    {
      Driver driver = User.GetDriver();
      DriverList driverList = DriverList.getInstance();
      Driver employeeTransport = driverList.GetDriverListByRole(RolesList.Editor).First();
      _authorEmail = driver == null ? employeeTransport == null ? RobotEmail : employeeTransport.Email : driver.Email;
    }

    public void SendMailAccountViolation(Violation violation)
    {
      _subject = $"Штраф по а/м {violation.Car.Grz}";

      _body = "Здравствуйте, коллеги!\n"
              + violation.getDriver().GetName(NameType.Full) + " совершил нарушение ПДД.\n"
              + "Оплачиваем, удерживаем.";

      string owner = Owners.getInstance().getItem(Convert.ToInt32(violation.Car.ownerID));
      var drivers = GetAccountants(owner);

      var list = new List<Attachment> {new Attachment(violation.File)};
      var transportEmployee = DriverList.getInstance().GetDriverListByRole(RolesList.Editor).First();

      /* TO DO: добавила в копию Шелякову Марию */
      Send(drivers,
        new[]
          {_authorEmail, /*transportEmployee.email - не работает так, не отправляется*/ "maria.shelyakova@bbraun.com"},
        list);
    }

    public void SendMailViolation(Violation violation)
    {
      _subject = $"Штраф по а/м {violation.Car.Grz}";

      CreateMailAndSendViolation(violation);
    }

    private void CreateMailAndSendViolation(Violation violation)
    {
      List<Driver> drivers;

      if (violation.NoDeduction)
      {
        CreateBodyViolationNoDeduction(violation);
        var owner = Owners.getInstance().getItem(Convert.ToInt32(violation.Car.ownerID));
        drivers = GetAccountants(owner);
      }
      else
      {
        CreateBodyViolation(violation);
        drivers = new List<Driver> {violation.getDriver()};
      }

      var list = new List<Attachment> {new Attachment(violation.File)};

      Send(drivers, new[] {_authorEmail}, list);
    }

    private void CreateBodyViolation(Violation violation)
    {
      var driver = violation.getDriver();

      var appeal = driver.Sex == "мужской" ? "Уважаемый" : "Уважаемая";

      _body = $"{appeal} {driver.GetName(NameType.Full)}!\n\n" +
              "Информирую Вас о том, что пришло постановление о штрафе за нарушения ПДД.\n" +
              "Оплатить штраф можно самостоятельно и в течении 5 дней предоставить документ об оплате.\n" +
              "После указанного срока штраф автоматически уйдет в оплату в бухгалтерию без возможности льготной оплаты 50%\n" +
              $"Документ об оплате штрафа следует присылать на эл. почту {User.GetDriver().GetName(NameType.Genetive)} в виде вложенного файла.\n" +
              $"Если есть возражения по данному штрафу, то необходимо сообщить об этом {User.GetDriver().GetName(NameType.Short)}.\n" +
              "Скан копия постановления во вложении.";
    }

    private void CreateBodyViolationNoDeduction(Violation violation)
    {
      var driver = violation.getDriver();

      var sb = new StringBuilder();
      sb.AppendLine("Добрый день!");
      sb.AppendLine("");
      sb.AppendLine("Сообщаю о том, что произошло нарушение ПДД.");
      sb.AppendLine("Прошу оплатить данное постановление.");
      sb.AppendLine("Постановление в приложении.");
      sb.AppendLine("");
      sb.AppendLine("С уважением,");
      sb.AppendLine(User.GetDriver().GetName(NameType.Full));
      sb.AppendLine(User.GetDriver().Position);
      sb.AppendLine(User.GetDriver().Mobile);

      _body = sb.ToString();
    }

    public void SendMailPolicy(Car car, PolicyType type)
    {
      PolicyList policyList = PolicyList.getInstance();
      Policy policy = policyList.getItem(car, type);

      if (string.IsNullOrEmpty(policy.File))
        throw new Exception("Не найден файл полиса");

      _subject = "Полис " + type;

      CreateBodyPolicy(type);

      DriverCarList driverCarList = DriverCarList.getInstance();
      Driver driver = driverCarList.GetDriver(car);

      Send(new List<Driver> {driver}, new[] {_authorEmail},
        new List<Attachment> {new Attachment(policy.File)});
    }

    private void CreateBodyPolicy(PolicyType type)
    {
      StringBuilder sb = new StringBuilder();
      sb.AppendLine("Добрый день!");
      sb.AppendLine("");
      if (type == PolicyType.КАСКО)
        sb.AppendLine(string.Concat("Высылаю новый полис ", type.ToString(), " в эл. виде."));
      else
        sb.AppendLine(string.Concat("Вам был отправлен полис ", type.ToString(),
          ", прошу проинформировать меня о его получении."));
      sb.AppendLine("");
      sb.AppendLine("С уважением,");
      sb.AppendLine(User.GetDriver().GetName(NameType.Full));
      sb.AppendLine(User.GetDriver().Position);
      sb.AppendLine(User.GetDriver().Mobile);

      _body = sb.ToString();
    }

    public void SendMailAccount(Account account)
    {
      _subject = "Согласование счета " + account.Number;

      CreateMailAndSendAccount(account);
    }

    private void CreateMailAndSendAccount(Account account)
    {
      CreateBodyAccount(account);

      var driverList = DriverList.getInstance();

      var accountants = GetAccountants(account.Owner);

      if (accountants.Count == 0)
        throw new NullReferenceException("Не найдены e-mail адреса бухгалтеров");

      Driver boss = driverList.GetDriverListByRole(RolesList.Boss).First();

      Send(accountants, new[] {boss.Email}, new List<Attachment>() {new Attachment(account.File)});
    }

    private List<Driver> GetAccountants(string owner)
    {
      DriverList driverList = DriverList.getInstance();

      if (owner == "ООО \"Б.Браун Медикал\"")
        return driverList.GetDriverListByRole(RolesList.AccountantBBraun);
      if (owner == "ООО \"ГЕМАТЕК\"")
        return driverList.GetDriverListByRole(RolesList.AccountantGematek);
      
      throw new NotImplementedException("Не заданы бухгалтеры для данной фирмы.");
    }

    private void CreateBodyAccount(Account account)
    {
      var sb = new StringBuilder();
      sb.AppendLine("Добрый день!");
      sb.AppendLine("");

      Driver driver = account.GetDriver();
      string employeeSex = string.Empty;

      if (driver != null)
      {
        employeeSex = (driver.Sex == "мужской") ? "сотрудника" : "сотрудницы";
      }

      PolicyType policyType = (PolicyType) Convert.ToInt32(account.IDPolicyType);

      if ((policyType == PolicyType.расш_КАСКО) && ((!account.BusinessTrip)))
      {
        sb.AppendLine("В связи с личной поездкой за пределы РФ был сделан полис по расширению КАСКО.");
        sb.Append("Прошу оплатить счет с удержанием из заработной платы ");
        sb.Append(employeeSex);
        sb.Append(" ");
        sb.Append(driver.GetName(NameType.Genetive));
        sb.Append(" сумму в размере ");
      }
      else if ((policyType == PolicyType.расш_КАСКО) && ((account.BusinessTrip)))
      {
        sb.Append("В связи с командировкой ");
        sb.Append(employeeSex);
        sb.Append(" ");
        sb.Append(driver.GetName(NameType.Genetive));
        sb.AppendLine(" за пределы РФ, был сделан полис по расширению КАСКО.");
        sb.Append("Прошу оплатить счет в размере ");
      }
      else
      {
        sb.Append("Прошу оплатить счёт № ");
        sb.AppendLine(account.Number);

        sb.Append("Cумма оплаты ");
      }

      sb.Append(account.Sum);
      sb.AppendLine(" р..");

      if (policyType == PolicyType.ДСАГО)
      {
        sb.Append("Данную сумму необходимо удержать из заработной платы ");
        sb.Append(driver.GetName(NameType.Short));
        sb.AppendLine(".");
      }

      sb.AppendLine("Скан копия счёта в приложении.");

      sb.AppendLine("");
      sb.AppendLine("С уважением,");
      sb.AppendLine(User.GetDriver().GetName(NameType.Full));
      sb.AppendLine(User.GetDriver().Position);
      sb.AppendLine(User.GetDriver().Dept);
      sb.AppendLine(User.GetDriver().Mobile);

      _body = sb.ToString();
    }
    
    public static void OpenEmailProgram(string conEmails)
    {
      if (string.IsNullOrEmpty(conEmails))
        return;

      using (var process = new Process())
      {
        process.StartInfo.FileName = "mailto:" + conEmails;
        process.Start();
      }
    }

    public void SendNotification(Driver driver, string message, bool addTransportToCopy = true,
      List<string> fileNames = null)
    {
      if (driver == null)
        Logger.Error("Не найден водитель для отправки уведомления", message);

      _subject = "Уведомление";
      _body = message;

      string[] copyEmails = null;
      if (addTransportToCopy)
      {
        var transportEmployee = DriverList.getInstance().GetDriverListByRole(RolesList.Editor).First();
        copyEmails = new[] {transportEmployee.Email};
      }

      var listAttachment = new List<Attachment>();
      fileNames?.ForEach(item => listAttachment.Add(new Attachment(item)));


      Send(new List<Driver> {driver}, copyEmails, listAttachment);
      LogManager.Logger.Debug(message);
    }

    public void SendDocuments(IList<Driver> drivers, List<Document> documentsForSend)
    {
      _subject = "Документы";

      CreateBodyDocuments();

      var transportEmployee = DriverList.getInstance().GetDriverListByRole(RolesList.Editor).First();
      var copyEmails = new[] { transportEmployee.Email };

      var listAttachment = new List<Attachment>();
      documentsForSend.ForEach(document => listAttachment.Add(new Attachment(document.Path)));

      Send(drivers, copyEmails, listAttachment);
    }

    private void CreateBodyDocuments()
    {
      var sb = new StringBuilder();
      sb.AppendLine("Добрый день!");
      sb.AppendLine("");
      sb.AppendLine("Высылаю документы для ознакомления");
      sb.AppendLine("");
      sb.AppendLine("С уважением,");
      sb.AppendLine(User.GetDriver().GetName(NameType.Full));
      sb.AppendLine(User.GetDriver().Position);
      sb.AppendLine(User.GetDriver().Mobile);

      _body = sb.ToString();
    }

    internal void SendMailToAdmin(string message)
    {
      var admin = DriverList.getInstance().getItem("kasytaru");

      if (admin == null)
        return;

      _subject = "Ошибка в программе BBAuto";
      _authorEmail = RobotEmail;
      _body = message;

      Send(new List<Driver> {admin});
    }

    private void Send(IList<Driver> drivers, string[] copyEmails = null, List<Attachment> files = null)
    {
      if (string.IsNullOrEmpty(_authorEmail))
        throw new Exception("ваш email не найден");

      using (var msg = new MailMessage())
      {
        msg.From = new MailAddress(_authorEmail);

        foreach (var driver in drivers)
        {
          if (string.IsNullOrEmpty(driver.Email))
            _subject += " не найден email сотрудника " + driver.GetName(NameType.Genetive);
          else
            msg.To.Add(new MailAddress(driver.Email));
        }

        if (msg.To.Count == 0)
          msg.To.Add(new MailAddress(_authorEmail));

        if (copyEmails != null)
        {
          foreach (var copyEmail in copyEmails)
            msg.CC.Add(new MailAddress(copyEmail));
        }

        msg.Subject = _subject;
        msg.SubjectEncoding = Encoding.UTF8;
        msg.Body = _body;

        files?.ForEach(item => msg.Attachments.Add(item));

        msg.BodyEncoding = Encoding.UTF8;
        msg.IsBodyHtml = false;
        var client = new SmtpClient(ServerHost, ServerPort)
        {
          Credentials = new System.Net.NetworkCredential(ServerUserName, ServerPassword)
        };


        client.Send(msg);
      }
    }
  }
}
