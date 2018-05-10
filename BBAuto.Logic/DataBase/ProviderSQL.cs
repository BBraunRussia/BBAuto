using System;
using System.Data;
using System.Linq;
using System.Text;
using BBAuto.DataLayer;

namespace BBAuto.Logic.DataBase
{
  public class ProviderSQL : IProvider
  {
    private static IDataBase _db;

    public ProviderSQL()
    {
      _db = DataBase.GetDataBase();
    }

    public DataTable Select(string tableName)
    {
      var spName = tableName.ToLower().Last() == 's'
        ? tableName + "es"
        : tableName + "s";

      return _db.GetRecords("exec Get" + spName);
    }

    public string SelectOne(string tableName)
    {
      DataTable dt = Select(tableName);

      if (dt.Rows.Count > 0)
        return dt.Rows[0].ItemArray[0].ToString();

      throw new Exception("Пустое значение");
    }

    public string Insert(string tableName, params object[] Params)
    {
      StringBuilder paramList = new StringBuilder();

      for (int i = 1; i <= Params.Length; i++)
      {
        if (paramList.ToString() != string.Empty)
          paramList.Append(", ");
        paramList.Append("@p" + i);
      }

      return _db.GetRecordsOne("exec Upsert" + tableName + " " + paramList, Params);
    }

    public void Delete(string tableName, int id)
    {
      _db.GetRecords("exec Delete" + tableName + " @p1", id);
    }


    public DataTable DoOther(string sql, params object[] Params)
    {
      return _db.GetRecords(sql, Params);
    }
  }
}
