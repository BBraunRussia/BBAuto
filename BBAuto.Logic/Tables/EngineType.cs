using System;
using System.Data;
using BBAuto.Logic.Abstract;

namespace BBAuto.Logic.Tables
{
  public class EngineType : MainDictionary
  {
    public EngineType(DataRow row)
    {
      Id = Convert.ToInt32(row[0]);
      Name = row[1].ToString();
      ShortName = row[2].ToString();
    }

    public string Name { get; private set; }
    public string ShortName { get; private set; }

    internal override object[] ToRow()
    {
      throw new NotImplementedException();
    }
  }
}
