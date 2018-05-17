using System;
using System.Data;
using BBAuto.Logic.Abstract;
using BBAuto.Logic.Common;
using BBAuto.Logic.Entities;
using BBAuto.Logic.Lists;
using BBAuto.Logic.Static;

namespace BBAuto.Logic.ForCar
{
  public class ShipPart : MainDictionary
  {
    private DateTime _dateRequest;
    private DateTime _dateSent;

    public string Number { get; set; }
    public string File { get; set; }
    public int CarId { get; set; }
    public Driver Driver { get; set; }

    public string DateRequest
    {
      get => _dateRequest == new DateTime(1, 1, 1) ? string.Empty : _dateRequest.Date.ToShortDateString();
      set => DateTime.TryParse(value, out _dateRequest);
    }

    private string DateRequestForSql => _dateRequest == new DateTime(1, 1, 1)
      ? string.Empty
      : _dateRequest.Year + "-" + _dateRequest.Month + "-" + _dateRequest.Day;

    public string DateSent
    {
      get => _dateSent == new DateTime(1, 1, 1) ? string.Empty : _dateSent.Date.ToShortDateString();
      set => DateTime.TryParse(value, out _dateSent);
    }

    private string DateSentForSql => _dateSent == new DateTime(1, 1, 1)
      ? string.Empty
      : _dateSent.Year + "-" + _dateSent.Month + "-" + _dateSent.Day;

    public ShipPart(int carId)
    {
      CarId = carId;
    }

    internal ShipPart(DataRow row)
    {
      FillFields(row);
    }

    private void FillFields(DataRow row)
    {
      int id;
      int.TryParse(row.ItemArray[0].ToString(), out id);
      Id = id;

      int.TryParse(row.ItemArray[1].ToString(), out int idCar);
      CarId = idCar;

      int idDriver;
      int.TryParse(row.ItemArray[2].ToString(), out idDriver);
      Driver = DriverList.getInstance().getItem(idDriver);

      Number = row.ItemArray[3].ToString();
      DateRequest = row.ItemArray[4].ToString();
      DateSent = row.ItemArray[5].ToString();
      File = row.ItemArray[6].ToString();
      FileBegin = File;
    }

    internal override object[] ToRow()
    {
      return new object[]
        {Id, CarId, "Car.BBNumber", "Car.Grz", Driver.GetName(NameType.Full), Number, _dateRequest, _dateSent};
    }

    internal override void Delete()
    {
      Provider.Delete("ShipPart", Id);
    }

    public override void Save()
    {
      DeleteFile(File);

      File = WorkWithFiles.FileCopyById(File, "cars", CarId, "ShipPart", Number);

      int id;
      int.TryParse(
        Provider.Insert("ShipPart", Id, CarId, Driver.Id, Number, DateRequestForSql, DateSentForSql, File),
        out id);
      Id = id;
    }
  }
}
