using System.Data;
using BBAuto.Logic.Abstract;
using BBAuto.Logic.Common;
using BBAuto.Logic.Lists;

namespace BBAuto.Logic.ForCar
{
  public class DTPFile : MainDictionary
  {
    public DTP DTP { get; set; }
    public string Name { get; set; }
    public string File { get; set; }

    public DTPFile(DTP dtp)
    {
      DTP = dtp;
      File = string.Empty;
    }

    public DTPFile(DataRow row)
    {
      fillFields(row);
    }

    private void fillFields(DataRow row)
    {
      int id;
      int.TryParse(row.ItemArray[0].ToString(), out id);
      Id = id;

      int idDTP;
      int.TryParse(row.ItemArray[1].ToString(), out idDTP);
      DTP = DTPList.getInstance().getItem(idDTP);

      Name = row.ItemArray[2].ToString();
      File = row.ItemArray[3].ToString();
      FileBegin = File;
    }

    public override void Save()
    {
      int id;

      if (Id == 0)
      {
        int.TryParse(Provider.Insert("dtpFile", Id, DTP.Id, Name, File), out id);
        Id = id;
      }

      DeleteFile(File);

      File = WorkWithFiles.FileCopyById(File, "DTP", DTP.Id, string.Empty, Name);

      int.TryParse(Provider.Insert("dtpFile", Id, DTP.Id, Name, File), out id);
      Id = id;
    }

    internal override object[] ToRow()
    {
      return new object[] {Id, Name, File == string.Empty ? string.Empty : "Просмотр"};
    }

    internal override void Delete()
    {
      DeleteFile(File);

      Provider.Delete("DtpFile", Id);
    }
  }
}
