using System;
using System.Data;
using BBAuto.Logic.Abstract;
using BBAuto.Logic.Common;
using BBAuto.Logic.Entities;
using BBAuto.Logic.Lists;

namespace BBAuto.Logic.ForCar
{
  public class PTS : MainDictionary
  {
    private string _number;

    public string Number
    {
      get => _number;
      set => _number = value.ToUpper();
    }

    public string GiveOrg { get; set; }
    public DateTime Date { get; set; }
    public string File { get; set; }

    public int CarId { get; }

    internal PTS(Car car)
    {
      CarId = car.Id;
      Date = DateTime.Today;
      Number = string.Empty;
    }

    public PTS(int carId)
    {
      CarId = carId;
      Date = DateTime.Today;
      Number = string.Empty;
    }

    public PTS(DataRow row)
    {
      int.TryParse(row.ItemArray[0].ToString(), out int idCar);
      CarId = idCar;
      
      Number = row.ItemArray[1].ToString();
      Date = Convert.ToDateTime(row.ItemArray[2]);
      GiveOrg = row.ItemArray[3].ToString();
      File = row.ItemArray[4].ToString();
      FileBegin = File;
    }

    public override void Save()
    {
      DeleteFile(File);

      File = WorkWithFiles.FileCopyById(File, "cars", CarId, "", "PTS");

      Provider.Insert("PTS", CarId, Number, Date, GiveOrg, File);

      var ptsList = PTSList.getInstance();
      ptsList.Add(this);
    }

    internal override void Delete()
    {
      DeleteFile(File);

      Provider.Delete("PTS", CarId);
    }

    internal override object[] GetRow()
    {
      return null;
    }
  }
}
