using System;
using System.Data;
using BBAuto.Logic.Abstract;
using BBAuto.Logic.Common;
using BBAuto.Logic.Entities;
using BBAuto.Logic.Lists;
using BBAuto.Logic.Static;

namespace BBAuto.Logic.ForDriver
{
  public class MedicalCert : MainDictionary, INotification, IActual
  {
    private int _notifacationSent;

    public string Number { get; set; }
    public DateTime DateBegin { get; set; }
    public DateTime DateEnd { get; set; }
    public string File { get; set; }

    public Driver Driver { get; set; }

    public bool IsNotificationSent
    {
      get => Convert.ToBoolean(_notifacationSent);
      private set => _notifacationSent = Convert.ToInt32(value);
    }

    public MedicalCert(Driver driver)
    {
      Driver = driver;
      Id = 0;
      DateBegin = DateTime.Today;
      DateEnd = DateTime.Today;

      File = string.Empty;
    }

    public MedicalCert(DataRow row)
    {
      fillFields(row);
    }

    private void fillFields(DataRow row)
    {
      int id;
      int.TryParse(row.ItemArray[0].ToString(), out id);
      Id = id;

      Number = row.ItemArray[1].ToString();

      DateTime dateBegin;
      DateTime.TryParse(row.ItemArray[2].ToString(), out dateBegin);
      DateBegin = dateBegin;

      DateTime dateEnd;
      DateTime.TryParse(row.ItemArray[3].ToString(), out dateEnd);
      DateEnd = dateEnd;

      int idDriver;
      int.TryParse(row.ItemArray[4].ToString(), out idDriver);
      Driver = DriverList.getInstance().getItem(idDriver);

      File = row.ItemArray[5].ToString();
      FileBegin = File;
      int.TryParse(row.ItemArray[6].ToString(), out _notifacationSent);
    }

    public override void Save()
    {
      DeleteFile(File);

      File = WorkWithFiles.FileCopyById(File, "drivers", Driver.Id, "MedicalCert", Number);

      ExecSave();

      MedicalCertList.getInstance().Add(this);
    }

    private void ExecSave()
    {
      int id;
      int.TryParse(Provider.Insert("MedicalCert", Id, Driver.Id, Number, DateBegin, DateEnd, File, _notifacationSent),
        out id);
      Id = id;
    }

    internal override void Delete()
    {
      DeleteFile(File);

      Provider.Delete("MedicalCert", Id);
    }

    internal override object[] ToRow()
    {
      return new object[3] {Id, Number, DateEnd.ToShortDateString()};
    }

    public override string ToString()
    {
      return (Driver == null) ? "нет данных" : string.Concat(Number, " до ", DateEnd.ToShortDateString());
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
      if (Id == 0)
      {
        return "Добрый день, " + Driver.GetName(NameType.Full)
               + "!\r\n\r\nНапоминаем, что Вы своевременно не оформили водительскую медицинскую справку.\r\nПросим оформить данную справку.\r\nОригинал необходимо прислать в отдел кадров, а скан копию в транспортный отдел.\r\n\r\nС уважением,\r\nТранспортный отдел.";
      }

      MailTextList mailTextList = MailTextList.getInstance();
      MailText mailText = mailTextList.getItemByType(MailTextType.MedicalCert);

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
      return File != string.Empty;
    }
  }
}
