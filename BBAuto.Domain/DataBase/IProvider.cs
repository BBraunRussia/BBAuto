using System.Data;

namespace BBAuto.Domain.DataBase
{
  public interface IProvider
  {
    DataTable Select(string tableName);
    string Insert(string tableName, params object[] Params);
    DataTable DoOther(string sql, params object[] Params);
    string SelectOne(string tableName);
    void Delete(string tableName, int id);
  }
}
