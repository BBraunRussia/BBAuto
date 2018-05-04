using System;
using System.Data;
using BBAuto.Logic.Abstract;

namespace BBAuto.Logic.ForCar
{
  public class Model : MainDictionary
  {
    public int MarkId { get; private set; }
    public string Name { get; set; }

    public Model(int idMark)
    {
      Id = 0;
      MarkId = idMark;
    }

    public Model(DataRow row)
    {
      fillFields(row);
    }

    private void fillFields(DataRow row)
    {
      Id = Convert.ToInt32(row.ItemArray[0]);
      Name = row.ItemArray[1].ToString();

      int idMark;
      int.TryParse(row.ItemArray[2].ToString(), out idMark);
      MarkId = idMark;
    }

    internal override void Delete()
    {
      Provider.Delete("Model", Id);
    }

    public override void Save()
    {
      Provider.Insert("Model", Id, Name, MarkId);
    }

    internal override object[] ToRow()
    {
      return new object[2] {Id, Name};
    }
  }
}
