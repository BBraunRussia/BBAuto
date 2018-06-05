using System;
using System.Data;
using BBAuto.Repository;

namespace BBAuto.Domain.DataBase
{
  public class MockProvider : IProvider
  {
    private static MockDataBase _db;

    public MockProvider()
    {
      IDataBase db = DataBase.GetDataBase();
      _db = db as MockDataBase;
    }

    public DataTable Select(string tableName)
    {
      return _db.Select(tableName);
    }

    public string Insert(string tableName, params object[] Params)
    {
      return _db.Insert(tableName, Params);
    }

    public string SelectOne(string tableName)
    {
      DataTable dt = Select(tableName);

      if (dt.Rows.Count > 0)
        return dt.Rows[0].ItemArray[0].ToString();
      else
        throw new Exception("Пустое значение");
    }

    public void Delete(string tableName, int id)
    {
      throw new NotImplementedException();
    }


    public DataTable DoOther(string sql, params object[] Params)
    {
      throw new NotImplementedException();
    }
  }
}
