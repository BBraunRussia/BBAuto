using System.Data;
using BBAuto.Logic.Abstract;

namespace BBAuto.Logic.ForCar
{
  public class Dealer : MainDictionary, IDictionaryMvc
  {
    public string Name { get; set; }
    public string Text { get; set; }

    public Dealer()
    {
      Id = 0;
      Text = string.Empty;
    }

    public Dealer(DataRow row)
    {
      int id;
      int.TryParse(row.ItemArray[0].ToString(), out id);
      Id = id;

      Name = row.ItemArray[1].ToString();
      Text = row.ItemArray[2].ToString();
    }

    public override void Save()
    {
      Provider.Insert("Diller", Id, Name, Text);
    }

    internal override void Delete()
    {
      Provider.Delete("Diller", Id);
    }

    internal override object[] ToRow()
    {
      return new object[3] {Id, Name, Text};
    }
  }
}
