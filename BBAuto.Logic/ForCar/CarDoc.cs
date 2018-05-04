using System.Data;
using BBAuto.Logic.Abstract;
using BBAuto.Logic.Common;
using BBAuto.Logic.Entities;
using BBAuto.Logic.Lists;

namespace BBAuto.Logic.ForCar
{
  public class CarDoc : MainDictionary
  {
    public string Name { get; set; }
    public string File { get; set; }

    public Car Car { get; set; }

    public CarDoc(Car car)
    {
      Car = car;
      Id = 0;
    }

    public CarDoc(DataRow row)
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

      Name = row.ItemArray[2].ToString();
      File = row.ItemArray[3].ToString();
      FileBegin = File;
    }

    public override void Save()
    {
      DeleteFile(File);

      File = WorkWithFiles.FileCopyById(File, "cars", Car.Id, "Documents", Name);

      int id;
      int.TryParse(Provider.Insert("CarDoc", Id, Car.Id, Name, File), out id);
      Id = id;
    }

    internal override object[] ToRow()
    {
      return new object[] {Id, Name, (File == string.Empty) ? string.Empty : "Показать"};
    }

    internal override void Delete()
    {
      DeleteFile(File);

      Provider.Delete("CarDoc", Id);
    }
  }
}
