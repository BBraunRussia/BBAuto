using BBAuto.Domain.Abstract;
using System.Data;

namespace BBAuto.Domain.ForCar
{
  public class Diler : MainDictionary, IDictionaryMVC
  {
    public string Name { get; set; }
    public string Text { get; set; }

    public Diler()
    {
      ID = 0;
      Text = string.Empty;
    }

    public Diler(DataRow row)
    {
      int.TryParse(row.ItemArray[0].ToString(), out int id);
      ID = id;

      Name = row.ItemArray[1].ToString();
      Text = row.ItemArray[2].ToString();
    }

    public override void Save()
    {
      _provider.Insert("Diller", ID, Name, Text);
    }

    internal override void Delete()
    {
      _provider.Delete("Diller", ID);
    }

    internal override object[] getRow()
    {
      return new object[] {ID, Name, Text};
    }
  }
}
