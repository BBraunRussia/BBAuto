using BBAuto.Domain.Abstract;
using BBAuto.Domain.Common;
using BBAuto.Domain.Lists;
using System;
using System.Data;

namespace BBAuto.Domain.ForCar
{
  public class PTS : MainDictionary
  {
    private string _number;

    internal PTS(int carId)
    {
      CarId = carId;

      Date = DateTime.Today;
      Number = string.Empty;
    }

    public PTS(DataRow row)
    {
      if (int.TryParse(row.ItemArray[0].ToString(), out int carId))
        CarId = carId;

      Number = row.ItemArray[1].ToString();
      Date = Convert.ToDateTime(row.ItemArray[2]);
      GiveOrg = row.ItemArray[3].ToString();
      File = row.ItemArray[4].ToString();
      _fileBegin = File;
    }

    public string Number
    {
      get => _number;
      set => _number = value.ToUpper();
    }

    public int CarId { get; set; }
    public string GiveOrg { get; set; }
    public DateTime Date { get; set; }
    public string File { get; set; }
    
    public override void Save()
    {
      DeleteFile(File);

      if (_fileBegin != File)
        File = WorkWithFiles.fileCopyByID(File, "cars", CarId, "", "PTS");

      _provider.Insert("PTS", CarId, Number, Date, GiveOrg, File);

      PTSList ptsList = PTSList.getInstance();
      ptsList.Add(this);
    }

    internal override void Delete()
    {
      DeleteFile(File);

      _provider.Delete("PTS", CarId);
    }

    internal override object[] getRow()
    {
      return null;
    }
  }
}
