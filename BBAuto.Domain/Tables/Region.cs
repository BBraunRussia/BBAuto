using System;
using System.Data;
using BBAuto.Domain.Abstract;

namespace BBAuto.Domain.Tables
{
  public class Region : MainDictionary
  {
    public string Name { get; }

    public Region(DataRow row)
    {
      int.TryParse(row[0].ToString(), out int id);
      ID = id;

      Name = row[1].ToString();
    }

    public Region(string name)
    {
      Name = name;
    }

    internal override object[] getRow()
    {
      throw new NotImplementedException();
    }

    public override void Save()
    {
      int.TryParse(_provider.Insert("Region", ID, Name), out int id);

      ID = id;
    }
  }
}
