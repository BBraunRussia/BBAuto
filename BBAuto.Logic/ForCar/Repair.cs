using System;
using System.Data;
using BBAuto.Logic.Abstract;
using BBAuto.Logic.Common;
using BBAuto.Logic.Dictionary;
using BBAuto.Logic.Entities;
using BBAuto.Logic.Lists;

namespace BBAuto.Logic.ForCar
{
  public class Repair : MainDictionary
  {
    private int _idRepairType;
    private int _idServiceStantion;
    private double _cost;

    public int CarId { get; private set; }
    public DateTime Date { get; set; }
    public string File { get; set; }

    public string RepairTypeID
    {
      get { return _idRepairType.ToString(); }
      set { int.TryParse(value, out _idRepairType); }
    }

    public string ServiceStantionID
    {
      get { return _idServiceStantion.ToString(); }
      set { int.TryParse(value, out _idServiceStantion); }
    }

    public string Cost
    {
      get { return _cost.ToString(); }
      set { double.TryParse(value, out _cost); }
    }

    public Repair(int carId)
    {
      Id = 0;
      CarId = carId;
      Date = DateTime.Today;
    }

    public Repair(int carId, DataRow row)
    {
      CarId = carId;

      fillFields(row);
    }

    public Repair(DataRow row)
    {
      fillFields(row);
    }

    private void fillFields(DataRow row)
    {
      int id;
      int.TryParse(row.ItemArray[0].ToString(), out id);
      Id = id;

      int.TryParse(row.ItemArray[1].ToString(), out int idCar);
      CarId = idCar;

      int.TryParse(row.ItemArray[2].ToString(), out _idRepairType);
      int.TryParse(row.ItemArray[3].ToString(), out _idServiceStantion);

      DateTime date;
      DateTime.TryParse(row.ItemArray[4].ToString(), out date);
      Date = date;

      Cost = row.ItemArray[5].ToString();
      File = row.ItemArray[6].ToString();
      FileBegin = File;
    }

    internal override object[] ToRow()
    {
      string show = "";
      if (!string.IsNullOrEmpty(File))
        show = "Показать";

      RepairTypes repairTypes = RepairTypes.getInstance();
      ServiceStantions serviceStantions = ServiceStantions.getInstance();

      return new object[]
      {
        Id, CarId, "Car.BBNumber", "Car.Grz", repairTypes.getItem(_idRepairType),
        serviceStantions.getItem(_idServiceStantion),
        Date, _cost, show
      };
    }

    public override void Save()
    {
      int id;

      if (Id == 0)
      {
        int.TryParse(Provider.Insert("Repair", Id, CarId, _idRepairType, _idServiceStantion, Date, _cost, File),
          out id);
        Id = id;
      }

      DeleteFile(File);

      File = WorkWithFiles.FileCopyById(File, "cars", CarId, "Repair", Id.ToString());
      int.TryParse(Provider.Insert("Repair", Id, CarId, _idRepairType, _idServiceStantion, Date, _cost, File),
        out id);
      Id = id;
    }

    internal override void Delete()
    {
      DeleteFile(File);

      Provider.Delete("Repair", Id);
    }
  }
}
