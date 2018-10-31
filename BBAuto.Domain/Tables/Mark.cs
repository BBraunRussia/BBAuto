using System;
using System.Data;
using BBAuto.Domain.Abstract;

namespace BBAuto.Domain.Tables
{
  public class Mark : MainDictionary
  {
    public Mark(DataRow row)
    {
      int.TryParse(row[0].ToString(), out int id);
      ID = id;

      Name = row[1].ToString();
    }

    public string Name { get; private set; }

    internal override object[] getRow()
    {
      throw new NotImplementedException();
    }
  }
}
