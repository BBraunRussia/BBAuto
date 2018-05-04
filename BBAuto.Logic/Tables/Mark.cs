using System;
using System.Data;
using BBAuto.Logic.Abstract;

namespace BBAuto.Logic.Tables
{
  public class Mark : MainDictionary
  {
    public Mark(DataRow row)
    {
      int id;
      int.TryParse(row[0].ToString(), out id);
      Id = id;

      Name = row[1].ToString();
    }

    public string Name { get; private set; }

    internal override object[] ToRow()
    {
      throw new NotImplementedException();
    }
  }
}
