using System.Data;

namespace BBAuto.Repository.Transaction
{
  public interface ITransaction
  {
    DataTable GetDataTable(string query);
    string GetString(string query);
  }
}
