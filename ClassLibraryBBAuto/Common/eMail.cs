using BBAuto.Domain.Dictionary;
using BBAuto.Domain.Entities;
using BBAuto.Domain.ForCar;
using BBAuto.Domain.Lists;
using BBAuto.Domain.Logger;
using BBAuto.Domain.Static;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Mail;
using System.Text;

namespace BBAuto.Domain.Common
{
    public class EMail
    {
        private const string SERVER_HOST = "212.0.16.135";
        private const int SERVER_PORT = 25;
        private const string SERVER_PASSWORD = "";
        private const string SERVER_USER_NAME = "";

        private const string ROBOT_EMAIL = "ru.robot@bbraun.com";

        private string _authorEmail;

        private string _subject;
        private string _body;

        public EMail()
        {
            Driver driver = User.getDriver();
            DriverList driverList = DriverList.getInstance();
            Driver employeeTransport = driverList.GetDriverListByRole(RolesList.Editor).First();
            _authorEmail = driver == null ? employeeTransport == null ? ROBOT_EMAIL : employeeTransport.Email : driver.Email;
        }

        public void SendMailAccountViolation(Violation violation)
        {
            _subject = string.Format("Штраф по а/м {0}", violation.Car.Grz);

            _body = "Здравствуйте, коллеги!\n"
                + violation.getDriver().GetName(NameType.Full) + " совершил нарушение ПДД.\n"
                + "Оплачиваем, удерживаем.";

            string owner = Owners.getInstance().getItem(Convert.ToInt32(violation.Car.ownerID));
            var drivers = GetAccountants(owner);
            
            List<Attachment> list = new List<Attachment>();
            list.Add(new Attachment(violation.File));
            Driver transportEmployee = DriverList.getInstance().GetDriverListByRole(RolesList.Editor).First();
            
            /* TO DO: добавила в копию Шелякову Марию */
            Send(drivers, new string[] { _authorEmail, /*transportEmployee.email - не работает так, не отправляется*/ "maria.shelyakova@bbraun.com" }, list);
        }
        
        public void sendMailViolation(Violation violation)
        {
            _subject = string.Format("Штраф по а/м {0}", violation.Car.Grz);

            CreateMailAndSendViolation(violation);
        }

        private void CreateMailAndSendViolation(Violation violation)
        {
            List<Driver> drivers;

            if (violation.NoDeduction)
            {
                CreateBodyViolationNoDeduction(violation);
                string owner = Owners.getInstance().getItem(Convert.ToInt32(violation.Car.ownerID));
                drivers = GetAccountants(owner);
            }
            else
            {
                CreateBodyViolation(violation);
                drivers = new List<Driver>() { violation.getDriver() };
            }
            
            List<Attachment> list = new List<Attachment>();
            list.Add(new Attachment(violation.File));

            Send(drivers, new string[] { _authorEmail }, list);
        }

        private void CreateBodyViolation(Violation violation)
        {
            Driver driver = violation.getDriver();

            string appeal;
            appeal = (driver.Sex == "мужской") ? "Уважаемый" : "Уважаемая";

            _body = string.Format("{0} {1}!\n\n"
                + "Информирую Вас о том, что пришло постановление о штрафе за нарушения ПДД.\n"
                + "Оплатить штраф можно самостоятельно и в течении 5 дней предоставить документ об оплате.\n"
                + "После указанного срока штраф автоматически уйдет в оплату в бухгалтерию без возможности льготной оплаты 50%\n"
                + "Документ об оплате штрафа следует присылать на эл. почту {2} в виде вложенного файла.\n"
                + "Если есть возражения по данному штрафу, то необходимо сообщить об этом {3}.\n"
                + "Скан копия постановления во вложении.",
                appeal,
                driver.GetName(NameType.Full),
                User.getDriver().GetName(NameType.Genetive),
                User.getDriver().GetName(NameType.Short));
        }

        private void CreateBodyViolationNoDeduction(Violation violation)
        {
            Driver driver = violation.getDriver();

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Добрый день!");
            sb.AppendLine("");
            sb.AppendLine("Сообщаю о том, что произошло нарушение ПДД.");
            sb.AppendLine("Прошу оплатить данное постановление.");
            sb.AppendLine("Постановление в приложении.");
            sb.AppendLine("");
            sb.AppendLine("С уважением,");
            sb.AppendLine(User.getDriver().GetName(NameType.Full));
            sb.AppendLine(User.getDriver().Position);
            sb.AppendLine(User.getDriver().Mobile);

            _body = sb.ToString();
        }

        public void sendMailPolicy(Car car, PolicyType type)
        {
            PolicyList policyList = PolicyList.getInstance();
            Policy policy = policyList.getItem(car, type);
            
            if (string.IsNullOrEmpty(policy.File))
                throw new Exception("Не найден файл полиса");
            else
            {
                _subject = "Полис " + type.ToString();

                CreateBodyPolicy(type);

                DriverCarList driverCarList = DriverCarList.getInstance();
                Driver driver = driverCarList.GetDriver(car);
                
                Send(new List<Driver> { driver }, new string[] { _authorEmail }, new List<Attachment>() { new Attachment(policy.File) });
            }
        }

        private void CreateBodyPolicy(PolicyType type)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Добрый день!");
            sb.AppendLine("");
            if (type == PolicyType.КАСКО)
                sb.AppendLine(string.Concat("Высылаю новый полис ", type.ToString(), " в эл. виде."));
            else
                sb.AppendLine(string.Concat("Вам был отправлен полис ", type.ToString(), ", прошу проинформировать меня о его получении."));
            sb.AppendLine("");
            sb.AppendLine("С уважением,");
            sb.AppendLine(User.getDriver().GetName(NameType.Full));
            sb.AppendLine(User.getDriver().Position);
            sb.AppendLine(User.getDriver().Mobile);

            _body = sb.ToString();
        }
        
        internal void sendMailAccount(Account account)
        {
            _subject = "Согласование счета " + account.Number;

            createMailAndSendAccount(account);
        }

        private void createMailAndSendAccount(Account account)
        {
            createBodyAccount(account);

            var driverList = DriverList.getInstance();

            var accountants = GetAccountants(account.Owner);

            if (accountants.Count == 0)
                throw new NullReferenceException("Не найдены e-mail адреса бухгалтеров");

            Driver boss = driverList.GetDriverListByRole(RolesList.Boss).First();
            
            Send(accountants, new string[] { boss.Email }, new List<Attachment>() { new Attachment(account.File) });
        }

        private List<Driver> GetAccountants(string owner)
        {
            DriverList driverList = DriverList.getInstance();
            
            if (owner == "ООО \"Б.Браун Медикал\"")
                return driverList.GetDriverListByRole(RolesList.AccountantBBraun);
            else if (owner == "ООО \"ГЕМАТЕК\"")
                return driverList.GetDriverListByRole(RolesList.AccountantGematek);
            else
                throw new NotImplementedException("Не заданы бухгалтеры для данной фирмы.");
        }

        private void createBodyAccount(Account account)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Добрый день!");
            sb.AppendLine("");
            
            Driver driver = account.GetDriver();
            string employeeSex = string.Empty;

            if (driver != null)
            {
                employeeSex = (driver.Sex == "мужской") ? "сотрудника" : "сотрудницы";
            }

            PolicyType policyType = (PolicyType)Convert.ToInt32(account.IDPolicyType);

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
            sb.AppendLine(User.getDriver().GetName(NameType.Full));
            sb.AppendLine(User.getDriver().Position);
            sb.AppendLine(User.getDriver().Dept);
            sb.AppendLine(User.getDriver().Mobile);

            _body = sb.ToString();
        }
        
        private void Send(List<Driver> drivers, string[] copyEmails = null, List<Attachment> files = null)
        {
            if (string.IsNullOrEmpty(_authorEmail))
                throw new Exception("ваш email не найден");

            using (var msg = new MailMessage())
            {
                msg.From = new MailAddress(_authorEmail);

                foreach (Driver driver in drivers)
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
                msg.SubjectEncoding = System.Text.Encoding.UTF8;
                msg.Body = _body;

                if (files != null)
                    files.ForEach(item => msg.Attachments.Add(item));

                msg.BodyEncoding = System.Text.Encoding.UTF8;
                msg.IsBodyHtml = false;
                var client = new SmtpClient(SERVER_HOST, SERVER_PORT);

                client.Credentials = new System.Net.NetworkCredential(SERVER_USER_NAME, SERVER_PASSWORD);

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
        
        internal void SendNotification(Driver driver, string message, bool addTransportToCopy = true, List<string> fileNames = null)
        {
            _subject = "Уведомление";
            _body = message;

            string[] copyEmails = null;
            if (addTransportToCopy)
            {
                Driver transportEmployee = DriverList.getInstance().GetDriverListByRole(RolesList.Editor).First();
                copyEmails = new string[] { transportEmployee.Email };
            }

            var listAttachment = new List<Attachment>();
            if (fileNames != null)
                fileNames.ForEach(item => listAttachment.Add(new Attachment(item)));
                               
            
            Send(new List<Driver> { driver }, copyEmails, listAttachment);
            LogManager.Logger.Debug(message);    
        }

        internal void sendMailToAdmin(string message)
        {
            Driver admin = DriverList.getInstance().getItem("kasytaru");

            if (admin == null)
                return;

            _subject = "Ошибка в программе BBAuto";
            _authorEmail = ROBOT_EMAIL;
            _body = message;

            Send(new List<Driver>() { admin });
        }
    }
}
