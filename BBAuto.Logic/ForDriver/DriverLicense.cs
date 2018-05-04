using System;
using System.Data;
using BBAuto.Logic.Abstract;
using BBAuto.Logic.Common;
using BBAuto.Logic.Entities;
using BBAuto.Logic.Lists;
using BBAuto.Logic.Static;

namespace BBAuto.Logic.ForDriver
{
  public class DriverLicense : MainDictionary, INotification, IActual
  {
    private int _notificationSent;

    public string Number { get; set; }
    public DateTime DateBegin { get; set; }
    public DateTime DateEnd { get; set; }
    public string File { get; set; }
    public Driver Driver { get; set; }

    public bool IsNotificationSent
    {
      get => Convert.ToBoolean(_notificationSent);
      private set => _notificationSent = Convert.ToInt32(value);
    }

    public DriverLicense(Driver driver)
    {
      Id = 0;
      Driver = driver;

      DateBegin = DateTime.Today;
      DateEnd = DateTime.Today;

      File = "";
    }

    public DriverLicense(DataRow row)
    {
      fillFields(row);
    }

    private void fillFields(DataRow row)
    {
      Id = Convert.ToInt32(row.ItemArray[0]);

      int idDriver;
      int.TryParse(row.ItemArray[1].ToString(), out idDriver);
      Driver = DriverList.getInstance().getItem(idDriver);

      Number = row.ItemArray[2].ToString();

      DateTime dateBegin;
      DateTime.TryParse(row.ItemArray[3].ToString(), out dateBegin);
      DateBegin = dateBegin;

      DateTime dateEnd;
      DateTime.TryParse(row.ItemArray[4].ToString(), out dateEnd);
      DateEnd = dateEnd;

      File = row.ItemArray[5].ToString();
      FileBegin = File;
      int.TryParse(row.ItemArray[6].ToString(), out _notificationSent);
    }

    public override void Save()
    {
      DeleteFile(File);

      File = WorkWithFiles.FileCopyById(File, "drivers", Driver.Id, "DriverLicense", Number);

      ExecSave();

      LicenseList licenseList = LicenseList.getInstance();
      licenseList.Add(this);
    }

    private void ExecSave()
    {
      int id;
      int.TryParse(
        Provider.Insert("DriverLicense", Id, Driver.Id, Number, DateBegin, DateEnd, File, _notificationSent), out id);
      Id = id;
    }

    internal override object[] ToRow()
    {
      return new object[] {Id, Number, DateEnd.ToShortDateString()};
    }

    internal override void Delete()
    {
      WorkWithFiles.Delete(File);

      Provider.Delete("DriverLicense", Id);
    }

    public override string ToString()
    {
      return Driver == null ? "нет данных" : string.Concat("№", Number, " до ", DateEnd.ToShortDateString());
    }

    public void SendNotification()
    {
      string message = CreateMessageNotification();

      EMail email = new EMail();
      email.SendNotification(Driver, message);

      IsNotificationSent = true;
      if (Id != 0)
        ExecSave();
    }

    private string CreateMessageNotification()
    {
      if (!IsHaveFile())
      {
        return "Добрый день, " + Driver.GetName(NameType.Full) +
               "!\r\n\r\nПросьба предоставить скан копию Вашего водительского удостоверения в транспортный отдел.\r\n\r\nС уважением,\r\nТранспортный отдел.";
      }

      MailTextList mailTextList = MailTextList.getInstance();
      MailText mailText = mailTextList.getItemByType(MailTextType.License);

      return mailText == null
        ? "Шаблон текста письма не найден"
        : mailText.Text.Replace("UserName", Driver.GetName(NameType.Full))
          .Replace("DateEnd", DateEnd.ToShortDateString());
    }

    public bool IsActual()
    {
      return IsHaveFile() && IsDateActual();
    }

    public bool IsDateActual()
    {
      return DateEnd > DateTime.Today;
    }

    public bool IsHaveFile()
    {
      return File != "";
    }
  }
}
