using BBAuto.Domain.Abstract;
using BBAuto.Domain.Common;
using BBAuto.Domain.Entities;
using BBAuto.Domain.Lists;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;

namespace BBAuto.Domain.ForCar
{
  public class PTS : MainDictionary
  {
    private string _number;

    public string Number
    {
      get { return _number; }
      set { _number = value.ToUpper(); }
    }

    public string GiveOrg { get; set; }
    public DateTime Date { get; set; }
    public string File { get; set; }

    public Car Car { get; private set; }

    internal PTS(Car car)
    {
      Car = car;
      Date = DateTime.Today;
      Number = string.Empty;
    }

    public PTS(DataRow row)
    {
      int idCar;
      int.TryParse(row.ItemArray[0].ToString(), out idCar);
      Car = CarList.GetInstance().getItem(idCar);

      Number = row.ItemArray[1].ToString();
      Date = Convert.ToDateTime(row.ItemArray[2]);
      GiveOrg = row.ItemArray[3].ToString();
      File = row.ItemArray[4].ToString();
      _fileBegin = File;
    }

    public override void Save()
    {
      DeleteFile(File);

      if (_fileBegin != File)
        File = WorkWithFiles.fileCopyByID(File, "cars", Car.ID, "", "PTS");

      _provider.Insert("PTS", Car.ID, Number, Date, GiveOrg, File);

      PTSList ptsList = PTSList.getInstance();
      ptsList.Add(this);
    }

    internal override void Delete()
    {
      DeleteFile(File);

      _provider.Delete("PTS", Car.ID);
    }

    internal override object[] getRow()
    {
      return null;
    }
  }
}
