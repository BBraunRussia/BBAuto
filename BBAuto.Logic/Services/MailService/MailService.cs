using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Mail;
using System.Text;
using BBAuto.Logic.Dictionary;
using BBAuto.Logic.Lists;
using BBAuto.Logic.Logger;
using BBAuto.Logic.Services.Account;
using BBAuto.Logic.Services.Car;
using BBAuto.Logic.Services.Dictionary.Owner;
using BBAuto.Logic.Services.Driver;
using BBAuto.Logic.Services.Driver.DriverCar;
using BBAuto.Logic.Services.Policy;
using BBAuto.Logic.Services.Violation;
using BBAuto.Logic.Static;

namespace BBAuto.Logic.Services.MailService
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

    private readonly IDriverService _driverService;
    private readonly IDriverCarService _driverCarService;
    private readonly IOwnerService _ownerService;
    private readonly IPolicyService _policyService;

    public MailService(
      IDriverService driverService,
      IDriverCarService driverCarService,
      IOwnerService ownerService,
      IPolicyService policyService)
    {
      _driverService = driverService;
      _driverCarService = driverCarService;
      _ownerService = ownerService;
      _policyService = policyService;

      var driver = User.GetDriver();
      var employeeTransport = _driverService.GetDriversByRole(RolesList.Editor).First();
      _authorEmail = driver == null ? employeeTransport == null ? RobotEmail : employeeTransport.Email : driver.email;
    }

    public void SendMailAccountViolation(string driverName, string file, CarModel car)
    {
      _subject = $"Штраф по а/м {car.Grz}";

      _body = "Здравствуйте, коллеги!\n"
              + driverName + " совершил нарушение ПДД.\n"
              + "Оплачиваем, удерживаем.";

      var drivers = GetAccountants(car.OwnerId ?? 0);

      var list = new List<Attachment> {new Attachment(file)};
      var transportEmployee = DriverList.getInstance().GetDriverListByRole(RolesList.Editor).First();

      /* TO DO: добавила в копию Шелякову Марию */
      Send(drivers,
        new[]
          {_authorEmail, /*transportEmployee.email - не работает так, не отправляется*/ "maria.shelyakova@bbraun.com"},
        list);
    }

    public void SendMailViolation(ViolationModel violation, CarModel car, DriverModel driver)
    {
      _subject = $"Штраф по а/м {car.Grz}";

      CreateMailAndSendViolation(violation, car, driver);
    }

    private void CreateMailAndSendViolation(ViolationModel violation, CarModel car, DriverModel driver)
    {
      IList<DriverModel> drivers;

      if (violation.NoDeduction)
      {
        CreateBodyViolationNoDeduction(violation, driver);
        drivers = GetAccountants(car.OwnerId ?? 0);
      }
      else
      {
        CreateBodyViolation(violation, driver);
        drivers = new List<DriverModel> {driver};
      }

      var list = new List<Attachment>();
      list.Add(new Attachment(violation.File));

      Send(drivers, new[] {_authorEmail}, list);
    }

    private void CreateBodyViolation(ViolationModel violation, DriverModel driver)
    {
      var appeal = driver.SexString == "мужской"
        ? "Уважаемый"
        : "Уважаемая";

      _body = $"{appeal} {driver.GetName(NameType.Full)}!\n\n" +
              "Информирую Вас о том, что пришло постановление о штрафе за нарушения ПДД.\n" +
              "Оплатить штраф можно самостоятельно и в течении 5 дней предоставить документ об оплате.\n" +
              "После указанного срока штраф автоматически уйдет в оплату в бухгалтерию без возможности льготной оплаты 50%\n" +
              $"Документ об оплате штрафа следует присылать на эл. почту {User.GetDriver().GetName(NameType.Genetive)} в виде вложенного файла.\n" +
              $"Если есть возражения по данному штрафу, то необходимо сообщить об этом {User.GetDriver().GetName(NameType.Short)}.\n" +
              "Скан копия постановления во вложении.";
    }

    private void CreateBodyViolationNoDeduction(ViolationModel violation, DriverModel driver)
    {
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

    public void SendMailPolicy(int carId, PolicyType type)
    {
      var policyList = PolicyList.getInstance();
      var policy = policyList.getItem(carId, type);

      if (string.IsNullOrEmpty(policy.File))
        throw new Exception("Не найден файл полиса");

      _subject = "Полис " + type;

      CreateBodyPolicy(type);

      var driver = _driverCarService.GetDriver(carId);

      Send(new List<DriverModel> {driver}, new [] {_authorEmail},
        new List<Attachment> {new Attachment(policy.File)});
    }

    private void CreateBodyPolicy(PolicyType type)
    {
      var sb = new StringBuilder();
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

    public void SendMailAccount(AccountModel account)
    {
      _subject = "Согласование счета " + account.Number;

      CreateMailAndSendAccount(account);
    }

    private void CreateMailAndSendAccount(AccountModel account)
    {
      CreateBodyAccount(account);

      var driverList = DriverList.getInstance();

      var accountants = GetAccountants(account.OwnerId);

      if (accountants.Count == 0)
        throw new NullReferenceException("Не найдены e-mail адреса бухгалтеров");

      var boss = driverList.GetDriverListByRole(RolesList.Boss).First();

      Send(accountants, new [] {boss.email}, new List<Attachment> {new Attachment(account.File)});
    }

    private IList<DriverModel> GetAccountants(int ownerId)
    {
      var owner = _ownerService.GetItemById(ownerId);

      switch (owner.Name)
      {
        case "ООО \"Б.Браун Медикал\"":
          return _driverService.GetDriversByRole(RolesList.AccountantBBraun);
        case "ООО \"ГЕМАТЕК\"":
          return _driverService.GetDriversByRole(RolesList.AccountantGematek);
        default:
          throw new NotImplementedException("Не заданы бухгалтеры для данной фирмы.");
      }
    }

    private void CreateBodyAccount(AccountModel account)
    {
      var sb = new StringBuilder();
      sb.AppendLine("Добрый день!");
      sb.AppendLine("");

      var driver = _driverCarService.GetDriverByAccountId(account.Id);
      var employeeSex = string.Empty;

      if (driver != null)
      {
        employeeSex = (driver.SexString == "мужской") ? "сотрудника" : "сотрудницы";
      }

      var policyType = (PolicyType)account.PolicyTypeId;

      if (policyType == PolicyType.расш_КАСКО && !account.BusinessTrip)
      {
        sb.AppendLine("В связи с личной поездкой за пределы РФ был сделан полис по расширению КАСКО.");
        sb.Append("Прошу оплатить счет с удержанием из заработной платы ");
        sb.Append(employeeSex);
        sb.Append(" ");
        sb.Append(driver.GetName(NameType.Genetive));
        sb.Append(" сумму в размере ");
      }
      else if (policyType == PolicyType.расш_КАСКО && account.BusinessTrip)
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

      sb.Append(_policyService.GetSumByAccountId(account));
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

    private void Send(IEnumerable<DriverModel> drivers, string[] copyEmails = null, List<Attachment> files = null)
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
          foreach (string copyEmail in copyEmails)
            msg.CC.Add(new MailAddress(copyEmail));
        }

        msg.Subject = _subject;
        msg.SubjectEncoding = Encoding.UTF8;
        msg.Body = _body;

        files?.ForEach(item => msg.Attachments.Add(item));

        msg.BodyEncoding = Encoding.UTF8;
        msg.IsBodyHtml = false;
        var client =
          new SmtpClient(ServerHost, ServerPort)
          {
            Credentials = new System.Net.NetworkCredential(ServerUserName, ServerPassword)
          };
        
        client.Send(msg);
      }
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

    public void SendNotification(DriverModel driver, string message, bool addTransportToCopy = true,
      IList<string> fileNames = null)
    {
      _subject = "Уведомление";
      _body = message;

      string[] copyEmails = null;
      if (addTransportToCopy)
      {
        Entities.Driver transportEmployee = DriverList.getInstance().GetDriverListByRole(RolesList.Editor).First();
        copyEmails = new [] {transportEmployee.email};
      }

      var listAttachment = new List<Attachment>();
      fileNames?.ToList().ForEach(item => listAttachment.Add(new Attachment(item)));


      Send(new List<DriverModel> {driver}, copyEmails, listAttachment);
      LogManager.Logger.Debug(message);
    }

    internal void SendMailToAdmin(string message)
    {
      var admin = _driverService.GetDriverByLogin("kasytaru");

      if (admin == null)
        return;

      _subject = "Ошибка в программе BBAuto";
      _authorEmail = RobotEmail;
      _body = message;

      Send(new List<DriverModel> {admin});
    }
  }
}
