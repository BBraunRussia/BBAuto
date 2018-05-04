using System.Data;
using BBAuto.Logic.Abstract;

namespace BBAuto.Logic.Common
{
  public class Template : MainDictionary
  {
    public string Name { get; set; }
    public string File { get; set; }

    public Template()
    {
      Id = 0;
    }

    public Template(DataRow row)
    {
      fillFields(row);
    }

    private void fillFields(DataRow row)
    {
      int id;
      int.TryParse(row.ItemArray[0].ToString(), out id);
      Id = id;

      Name = row.ItemArray[1].ToString();
      File = row.ItemArray[2].ToString();
      FileBegin = File;
    }

    public override void Save()
    {
      DeleteFile(File);

      File = WorkWithFiles.FileCopy(File, "Template", Name);

      Provider.Insert("Template", Id, Name, File);
    }

    internal override void Delete()
    {
      DeleteFile(File);

      Provider.Delete("Template", Id);
    }

    internal override object[] ToRow()
    {
      return new object[] {Id, Name, File};
    }
  }
}
