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
    public Car Car { get; set; }
    public Driver Driver { get; set; }

    public string DateRequest
    {
      get { return (_dateRequest == new DateTime(1, 1, 1)) ? string.Empty : _dateRequest.Date.ToShortDateString(); }
      set { DateTime.TryParse(value, out _dateRequest); }
    }

    private string DateRequestForSQL
    {
      get
      {
        return (_dateRequest == new DateTime(1, 1, 1))
          ? string.Empty
          : _dateRequest.Year.ToString() + "-" + _dateRequest.Month.ToString() + "-" + _dateRequest.Day.ToString();
      }
    }

    public string DateSent
    {
      get { return (_dateSent == new DateTime(1, 1, 1)) ? string.Empty : _dateSent.Date.ToShortDateString(); }
      set { DateTime.TryParse(value, out _dateSent); }
    }

    private string DateSentForSQL
    {
      get
      {
        return (_dateSent == new DateTime(1, 1, 1))
          ? string.Empty
          : _dateSent.Year.ToString() + "-" + _dateSent.Month.ToString() + "-" + _dateSent.Day.ToString();
      }
    }

    internal ShipPart(Car car)
    {
      Car = car;
    }

    internal ShipPart(DataRow row)
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
        {Id, Car.Id, Car.BBNumber, Car.Grz, Driver.GetName(NameType.Full), Number, _dateRequest, _dateSent};
    }

    internal override void Delete()
    {
      Provider.Delete("ShipPart", Id);
    }

    public override void Save()
    {
      DeleteFile(File);

      File = WorkWithFiles.FileCopyById(File, "cars", Car.Id, "ShipPart", Number);

      int id;
      int.TryParse(
        Provider.Insert("ShipPart", Id, Car.Id, Driver.Id, Number, DateRequestForSQL, DateSentForSQL, File).ToString(),
        out id);
      Id = id;
    }
  }
}
