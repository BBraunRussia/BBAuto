using BBAuto.Domain.Abstract;
using BBAuto.Domain.Common;
using System.Data;

namespace BBAuto.Domain.ForCar
{
  public class CarDoc : MainDictionary
  {
    public string Name { get; set; }
    public string File { get; set; }

    public int CarId { get; set; }

    public CarDoc() { }

    public CarDoc(DataRow row)
    {
      FillFields(row);
    }

    private void FillFields(DataRow row)
    {
      if (int.TryParse(row.ItemArray[0].ToString(), out int id))
        ID = id;

      if (int.TryParse(row.ItemArray[1].ToString(), out int carId))
        CarId = carId;

      Name = row.ItemArray[2].ToString();
      File = row.ItemArray[3].ToString();
      _fileBegin = File;
    }

    public override void Save()
    {
      DeleteFile(File);

      File = WorkWithFiles.fileCopyByID(File, "cars", CarId, "Documents", Name);

      if (int.TryParse(_provider.Insert("CarDoc", ID, CarId, Name, File), out int id))
        ID = id;
    }

    internal override object[] getRow()
    {
      return new object[] {ID, Name, (File == string.Empty) ? string.Empty : "Показать"};
    }

    internal override void Delete()
    {
      DeleteFile(File);

      _provider.Delete("CarDoc", ID);
    }
  }
}
