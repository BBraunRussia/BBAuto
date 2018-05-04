using System;
using System.Data;
using BBAuto.Logic.Abstract;

namespace BBAuto.Logic.Tables
{
  public class Region : MainDictionary
  {
    public string Name { get; private set; }

    public Region(DataRow row)
    {
      int id;
      int.TryParse(row[0].ToString(), out id);
      Id = id;

      Name = row[1].ToString();
    }

    public Region(string name)
    {
      Name = name;
    }

    internal override object[] ToRow()
    {
      throw new NotImplementedException();
    }

    public override void Save()
    {
      int id;
      int.TryParse(Provider.Insert("Region", Id, Name), out id);

      Id = id;
    }
  }
}
