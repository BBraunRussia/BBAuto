using BBAuto.Domain.Abstract;
using System.Data;

namespace BBAuto.Domain.Common
{
  public class Template : MainDictionary
  {
    public string Name { get; set; }
    public string File { get; set; }

    public Template()
    {
      ID = 0;
    }

    public Template(DataRow row)
    {
      fillFields(row);
    }

    private void fillFields(DataRow row)
    {
      int.TryParse(row.ItemArray[0].ToString(), out int id);
      ID = id;

      Name = row.ItemArray[1].ToString();
      File = row.ItemArray[2].ToString();
      _fileBegin = File;
    }

    public override void Save()
    {
      DeleteFile(File);

      File = WorkWithFiles.fileCopy(File, "Template", Name);

      _provider.Insert("Template", ID, Name, File);
    }

    internal override void Delete()
    {
      DeleteFile(File);

      _provider.Delete("Template", ID);
    }

    internal override object[] getRow()
    {
      return new object[] {ID, Name, File};
    }
  }
}
