using System;
using System.Data;
using System.Text;
using BBAuto.Logic.Abstract;
using BBAuto.Logic.Common;
using BBAuto.Logic.Entities;
using BBAuto.Logic.Lists;

namespace BBAuto.Logic.ForCar
{
  public class DiagCard : MainDictionary
  {
    private int _notifacationSent;

    public string Number { get; set; }
    public DateTime Date { get; set; }
    public string File { get; set; }
    public Car Car { get; set; }

    public bool IsNotificationSent
    {
      get { return Convert.ToBoolean(_notifacationSent); }
      private set { _notifacationSent = Convert.ToInt32(value); }
    }

    internal DiagCard(Car car)
    {
      Id = 0;
      Car = car;
      Number = string.Empty;
      Date = DateTime.Today;
      File = string.Empty;
    }

    public DiagCard(DataRow row)
    {
      fillFields(row);
    }

    private void fillFields(DataRow row)
    {
      int id;
      int.TryParse(row.ItemArray[0].ToString(), out id);
      Id = id;

      int idCar;
      int.TryParse(row.ItemArray[1].ToString(), out idCar);
      Car = CarList.getInstance().getItem(idCar);

      Number = row.ItemArray[2].ToString();

      DateTime date;
      DateTime.TryParse(row.ItemArray[3].ToString(), out date);
      Date = date;

      File = row.ItemArray[4].ToString();
      FileBegin = File;
      int.TryParse(row.ItemArray[5].ToString(), out _notifacationSent);
    }

    public override void Save()
    {
      DeleteFile(File);

      File = WorkWithFiles.FileCopyById(File, "cars", Car.Id, "DiagCard", Number);

      ExecSave();
    }

    private void ExecSave()
    {
      int id;
      int.TryParse(Provider.Insert("DiagCard", Id, Car.Id, Number, Date, File, _notifacationSent), out id);
      Id = id;
    }

    internal override void Delete()
    {
      DeleteFile(File);

      Provider.Delete("DiagCard", Id);
    }

    internal override object[] ToRow()
    {
      return new object[] {Id, Car.Id, Car.BBNumber, Car.Grz, Number, Date};
    }

    internal string ToMail()
    {
      IsNotificationSent = true;
      ExecSave();

      StringBuilder sb = new StringBuilder();
      sb.Append(Car.Grz);
      sb.Append(" ");
      sb.Append(Number);
      sb.Append(" ");
      sb.Append(Date.ToShortDateString());
      return sb.ToString();
    }
  }
}
