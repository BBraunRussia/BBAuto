using System;
using System.Data;

namespace BBAuto.Repository
{
  public interface IDataBase
  {
    DataTable GetRecords(String SQL, params Object[] Params);
    string GetRecordsOne(String SQL, params Object[] Params);
  }
}
