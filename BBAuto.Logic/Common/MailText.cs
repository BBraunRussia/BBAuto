using System.Data;
using BBAuto.Logic.Abstract;
using BBAuto.Logic.Lists;

namespace BBAuto.Logic.Common
{
  public class MailText : MainDictionary, IDictionaryMvc
  {
    public string Name { get; set; }
    public string Text { get; set; }

    public MailText()
    {
      Id = 0;
      Name = string.Empty;
      Text = string.Empty;
    }

    public MailText(DataRow row)
    {
      FillFields(row);
    }

    private void FillFields(DataRow row)
    {
      int.TryParse(row.ItemArray[0].ToString(), out int id);
      Id = id;

      Name = row.ItemArray[1].ToString();
      Text = row.ItemArray[2].ToString();
    }

    public override void Save()
    {
      int.TryParse(Provider.Insert("MailText", Id, Name, Text), out int id);
      Id = id;

      var mailTextList = MailTextList.getInstance();
      mailTextList.Add(this);
    }

    internal override void Delete()
    {
      Provider.Delete("MailText", Id);
    }

    internal override object[] ToRow()
    {
      return new object[] {Id, Name};
    }
  }
}
