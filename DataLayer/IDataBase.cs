using System;
using System.Data;

namespace DataLayer
{
  public interface IDataBase
  {
    DataTable GetRecords(String SQL, params Object[] Params);
    string GetRecordsOne(String SQL, params Object[] Params);
  }
}
